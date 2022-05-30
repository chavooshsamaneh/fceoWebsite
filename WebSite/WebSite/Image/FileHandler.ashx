<%@ WebHandler Language="C#" Class="FileHandler" %>

using System;
using System.Web;

public class FileHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        long FTO = 0;
        try
        {
            FTO = long.Parse(Utility.DecryptQS(context.Request.QueryString["FTO"]));
        }
        catch (Exception err)
        {
            context.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
        if (IsFileRequestTimeOut(FTO) == true)
            context.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.FileHandlerTimeOut).ToString());

        try
        {
            if (int.Parse(Utility.DecryptQS(context.Request.QueryString["User"])) > 0)
            {
                if (context.User.Identity.IsAuthenticated == false)
                    context.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString());
                else if (1 != int.Parse(Utility.DecryptQS(context.Request.QueryString["User"])))
                    context.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString());
            }
        }
        catch (Exception)
        {
            context.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }

        string File = Utility.DecryptQS(context.Request.QueryString["File"]);
        context.Response.Buffer = true;
        context.Response.Clear();
        System.IO.FileInfo FileInfo = new System.IO.FileInfo(context.Server.MapPath(File));
        String DownlaodFileName = FileInfo.Name;
        try
        {
            if (context.Request.QueryString["Name"] != null && String.IsNullOrEmpty(context.Request.QueryString["Name"]) == false)
                DownlaodFileName = Utility.DecryptQS(context.Request.QueryString["Name"]);
        }
        catch (Exception) { }
        context.Response.AddHeader("content-disposition", "attachment; filename=" + DownlaodFileName);
        context.Response.ContentType = "octet/stream";

        //Generate File name
        String NewFileName = "";
        do
        {
            NewFileName = System.IO.Path.GetRandomFileName() + FileInfo.Extension;
        } while (System.IO.File.Exists(context.Server.MapPath("~/Image/FileHandler/") + NewFileName) == true);

        System.IO.File.Copy(context.Server.MapPath(File), context.Server.MapPath("~/Image/FileHandler/") + NewFileName);

        context.Response.WriteFile("~/Image/FileHandler/" + NewFileName);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private Boolean IsFileRequestTimeOut(long RequestTime)
    {
        try
        {
            Double TimeOut = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["FileHandlerTimeOut"]);
            DateTime PageRequestTime = DateTime.FromFileTime(RequestTime);
            TimeSpan ts = DateTime.Now.Subtract(PageRequestTime);
            if (ts.TotalMinutes > TimeOut)
                return true;
            else
                return false;
        }
        catch (Exception)
        {
            HttpContext.Current.Response.Redirect("ErrorPage.aspx");
        }
        return true;
    }
}