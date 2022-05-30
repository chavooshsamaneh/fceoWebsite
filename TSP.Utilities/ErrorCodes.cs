using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ErrorCodes
/// </summary>
/// 

public class ErrorCodes
{
    public enum ErrorType : int
    {
        GeneralErr = 1000,
        PageInputsNotValid,
        NewNotAllowed,
        EditNotAlowed,
        ViewNotAllowed,
        DelNotAllowed,
        QueryTimeOut,
        FileHandlerTimeOut
    }
    private static System.Collections.Hashtable errCollection = new System.Collections.Hashtable();
    private static bool isInit = false;
    private static string[] ErrorMsg ={ "خطایی در صفحات پیش آمده است",
            "مقادیر صفحه نامعتبر است",
            "شما سطح دسترسی ایجاد ندارید",
        "شما سطح دسترسی ویرایش ندارید",
            "شما سطح دسترسی مشاهده ندارید",
            "شما سطح دسترسی حذف ندارید",
       "مهلت زمانی درخواست صفحه شما به پایان رسیده است.<br><br> لطفا صفحه را دوباره بازخوانی نمایید.",
       "مهلت زمان درخواست فایل شما به پایان رسیده است."};
    public ErrorCodes()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private static void InitErrors()
    {
        if (isInit)
            return;
        for (int i = 0, k = (int)ErrorType.GeneralErr; i < ErrorMsg.Length; i++, k++)
            errCollection.Add(k, ErrorMsg[i]);
        isInit = true;
    }
    public static string GetErrorDescription(ErrorType err)
    {
        InitErrors();
        object key = errCollection[(int)err];
        string errMsg = null;
        if (key != null)
        {
            //errNo=(int)errname;
            errMsg = key.ToString();
        }
        else
            errMsg = errCollection[(int)ErrorType.GeneralErr].ToString();
        return errMsg;
    }
}

