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

public partial class GlobalError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Utility.Date objDate = new Utility.Date();
        lblDate.Text = objDate.Get_LongDate();
        try
        {
            int ErrorNo = int.Parse(Request.QueryString["Error"]);
            switch (ErrorNo)
            {
                case 0:
                    lblError.Text = "سایت در حال بروز رسانی می باشد";
                    ImgError.ImageUrl = "~/Images/GlobalError/under-construct.png";
                    ImgError.Height = Unit.Pixel(230);
                    PanelSpaceImgError.Visible = false;
                    break;
                case 404:
                    lblError.Text = "چنین صفحه ای در سایت وجود ندارد";
                    ImgError.ImageUrl = "~/Images/GlobalError/404.png";
                    break;
                case 500:
                    lblError.Text = "به دلیل وجود خطا، سرور قادر به پاسخگویی نیست";
                    ImgError.ImageUrl = "~/Images/GlobalError/500.png";
                    break;
                case 502:
                    lblError.Text = "به دلیل ترافیک زیاد، سرور قادر به پاسخگویی نیست";
                    ImgError.ImageUrl = "~/Images/GlobalError/502.png";
                    break;
                default:
                    lblError.Text = "خطایی در سرور بوجود آمده است";
                    ImgError.Visible = false;
                    break;
            }
        }
        catch (Exception)
        {
            lblError.Text = "به دلیل وجود خطا، سرور قادر به پاسخگویی نیست";
            ImgError.ImageUrl = "~/Images/GlobalError/500.png";
            //Response.Redirect("Default.aspx");
        }
    }

}
