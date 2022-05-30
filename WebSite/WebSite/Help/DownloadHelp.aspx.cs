using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Help_DownloadHelp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            try
            {
                int Id = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["Id"].ToString())));
                if (String.IsNullOrEmpty(Id.ToString()))
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                Utility.Help objHelp = new Utility.Help((Utility.Help.HelpFiles)Id);

                ViewState["DownloadFile"] = objHelp.DownloadFileUrl;
            }
            catch (Exception)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }
        }
    }

    protected void btnDownloadHelpFile_Click(object sender, EventArgs e)
    {
        if (ViewState["DownloadFile"] != null && String.IsNullOrEmpty(ViewState["DownloadFile"].ToString()) == false)
        {
            if (ViewState["DownloadFile"].ToString() != "#")
            {
                //Response.Redirect(ViewState["DownloadFile"].ToString());

                //Response.ContentType = "Application/pdf";
                //string FilePath = MapPath(ViewState["DownloadFile"].ToString());
                //Response.WriteFile(FilePath);
                //Response.End();

                Boolean forceDownload = true;
                string path = MapPath(ViewState["DownloadFile"].ToString());
                //string name = Request.Path.GetFileName(path);
                //string ext = Request.Path.GetExtension(path);
                string type = "Application/pdf";

                //if (ext != null)
                //{
                //    switch (ext.ToLower())
                //    {
                //        case ".htm":
                //        case ".html":
                //            type = "text/HTML";
                //            break;

                //        case ".txt":
                //            type = "text/plain";
                //            break;

                //        case ".doc":
                //        case ".rtf":
                //            type = "Application/msword";
                //            break;
                //    }
                //}

                if (forceDownload)
                {
                    Response.AppendHeader("content-disposition", "attachment; filename=Help.pdf");
                }
                if (type != "")
                    Response.ContentType = type;
                Response.WriteFile(path);
                Response.End();
            }
        }
        else
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
    }
}
