using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class Utility
{
    public class Messages
    {
        public enum MessageTypes
        {
            SaveComplete, DeleteComplete, InActiveComplete,
            ErrorInSave, ErrorInDelete, ErrorInInActive, ErrorInLoadingData, SessionHasBeenExpired, MemberIsNotAccepted, MemberIsNotValid,
            MemberInvoiceIsNotZero, InsertMeId, DuplicateDataError, CanNotFindInformations, AutomationLetterIsNotValid,
            CanNotDeleteBecauseOfRelatedData, RelatedDataIsNotValid, UserNotExistInNezamChart,ErrorInProcess
        }

        public static String GetMessage(MessageTypes Type)
        {
            String Message = "";

            switch (Type)
            {
                case MessageTypes.SaveComplete:
                    Message = "ذخیره انجام شد";
                    break;
                case MessageTypes.DeleteComplete:
                    Message = "حذف انجام شد";
                    break;
                case MessageTypes.InActiveComplete:
                    Message = "رکورد غیرفعال شد";
                    break;
                case MessageTypes.ErrorInSave:
                    Message = "خطایی در ذخیره انجام گرفته است";
                    break;
                case MessageTypes.ErrorInLoadingData:
                    Message = "خطایی در بازخوانی اطلاعات انجام گرفته است";
                    break;
                case MessageTypes.ErrorInDelete:
                    Message = "خطایی در حذف انجام گرفته است";
                    break;
                case MessageTypes.ErrorInInActive:
                    Message = "خطایی در غیرفعال کردن انجام گرفته است";
                    break;
                case MessageTypes.SessionHasBeenExpired:
                    Message = "مدت زمان اعتبار اطلاعات به پایان رسیده است.اطلاعات را مجددا وارد نمایید";
                    break;
                case MessageTypes.MemberIsNotAccepted:
                    Message = "عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد";
                    break;
                case MessageTypes.MemberIsNotValid:
                    Message = "کد عضویت وارد شده معتبر نمی باشد";
                    break;
                case MessageTypes.MemberInvoiceIsNotZero:
                    Message = "مانده حساب عضو مورد نظر صفر نمی باشد";
                    break;
                case MessageTypes.InsertMeId:
                    Message = "کد عضویت را وارد نمایید";
                    break;
                case MessageTypes.DuplicateDataError:
                    Message = "اطلاعات وارد شده تکراری می باشد";
                    break;
                case MessageTypes.CanNotFindInformations:
                    Message = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است";
                    break;
                case MessageTypes.AutomationLetterIsNotValid:
                    Message = "شماره نامه معتبر نمی باشد";
                    break;
                case MessageTypes.CanNotDeleteBecauseOfRelatedData:
                    Message = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد";
                    break;
                case MessageTypes.RelatedDataIsNotValid:
                    Message = "اطلاعات وابسته معتبر نمی باشد";
                    break;
                case MessageTypes.UserNotExistInNezamChart:
                    Message = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
                    break;
                case MessageTypes.ErrorInProcess:
                    Message = "خطایی در انجام عملیات ایجاد شده است.";
                    break;                    
            }

            return Message;
        }

        public static String GetExceptionError(Exception err)
        {
            String Error = GetMessage(MessageTypes.ErrorInSave);

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

                if (se.Number == 2601)
                {
                    Error = GetMessage(MessageTypes.DuplicateDataError);
                }
                else if (se.Number == 547)
                {
                    Error = GetMessage(MessageTypes.CanNotDeleteBecauseOfRelatedData);
                }
                else if (se.Number == 2627)
                {
                    Error = GetMessage(MessageTypes.DuplicateDataError);
                }
            }

            return Error;
        }
    }
}
