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

public partial class ImageGallery_ImageGalleryFrame : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["Album"]))
        {
            Response.Redirect("ImageGallery.aspx");
            return;
        }

        int AlbumId = -1;
        try
        {
            AlbumId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["Album"]).ToString());
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        FrameHmlFile.Attributes["src"] = "ViewImageGallery.aspx?Album=" + Request.QueryString["Album"];
    }
}
