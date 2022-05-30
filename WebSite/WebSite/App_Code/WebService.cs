using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public AjaxControlToolkit.Slide[] GetSlides()
    {

        int NewsId;

        TSP.DataManager.NewsImgManager ImgManager = new TSP.DataManager.NewsImgManager();
        //int NewsId = 12;// int.Parse(System.Web.HttpContext.Current.Server.HtmlEncode(Request.QueryString["NewsId"]).ToString());
        if (Session["NewsId"] != null)
        {
            NewsId = int.Parse(Session["NewsId"].ToString());
        }
        else
            return null;
        ImgManager.FindByNewsCode(NewsId);
        ImgManager.DataTable.DefaultView.RowFilter = "Type = 0";
        AjaxControlToolkit.Slide[] sl = new AjaxControlToolkit.Slide[ImgManager.Count];
        for (int i = 0; i < ImgManager.Count; i++)
        {
            sl[i] = new AjaxControlToolkit.Slide(ImgManager[i]["ImgUrl"].ToString().Replace("~/", ""), "", "");
        }
        return sl;
        //return new AjaxControlToolkit.Slide[] { 
        //    new AjaxControlToolkit.Slide("image/icon-2.gif", "Blue Hills", "Go Blue"),
        //    new AjaxControlToolkit.Slide("image/icon-3.gif", "Sunset", "Setting sun"),
        //    new AjaxControlToolkit.Slide("image/icon-4.gif", "Winter", "Wintery..."),
        //    new AjaxControlToolkit.Slide("image/play.png", "Sedona", "Portrait style picture")};
    }



    [WebMethod(EnableSession = true)]
    public AjaxControlToolkit.Slide[] GetSlidesForViewFromManagePage()
    {

        int NewsId;

        TSP.DataManager.NewsImgManager ImgManager = new TSP.DataManager.NewsImgManager();
        //int NewsId = 12;// int.Parse(System.Web.HttpContext.Current.Server.HtmlEncode(Request.QueryString["NewsId"]).ToString());
        if (Session["NewsId"] != null)
        {
            NewsId = int.Parse(Session["NewsId"].ToString());
        }
        else
            return null;
        ImgManager.FindByNewsCode(NewsId);
        ImgManager.DataTable.DefaultView.RowFilter = "Type = 0";
        AjaxControlToolkit.Slide[] sl = new AjaxControlToolkit.Slide[ImgManager.Count];
        for (int i = 0; i < ImgManager.Count; i++)
        {
            sl[i] = new AjaxControlToolkit.Slide(ImgManager[i]["ImgUrl"].ToString().Replace("~/", "../../"), "", "");
        }
        return sl;
        //return new AjaxControlToolkit.Slide[] { 
        //    new AjaxControlToolkit.Slide("image/icon-2.gif", "Blue Hills", "Go Blue"),
        //    new AjaxControlToolkit.Slide("image/icon-3.gif", "Sunset", "Setting sun"),
        //    new AjaxControlToolkit.Slide("image/icon-4.gif", "Winter", "Wintery..."),
        //    new AjaxControlToolkit.Slide("image/play.png", "Sedona", "Portrait style picture")};
    }


    [WebMethod(EnableSession = true)]
    public AjaxControlToolkit.Slide[] GetCondolences()
    {
        TSP.DataManager.CondolenceManager CondolenceManager = new TSP.DataManager.CondolenceManager();
        DataTable dt = CondolenceManager.SelectAvailableCondolence(Utility.GetDateOfToday(), (int)TSP.DataManager.CondolenceManager.Type.Condolence);
        AjaxControlToolkit.Slide[] sl = new AjaxControlToolkit.Slide[dt.Rows.Count];
        string sb;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb = "<a style='text-decoration: none;color: #000000'; href='CandolenceDetail.aspx?CdlId=" + Utility.EncryptQS(dt.Rows[i]["CdlId"].ToString()) + "'>" +
                  dt.Rows[i]["Summary"].ToString() + "<br /><b>" + dt.Rows[i]["CdlFrom"].ToString() + "</b></a>";
            // sl[i] = new AjaxControlToolkit.Slide(dt.Rows[i]["CdlImage"].ToString().Replace("~/", ""), "", sb);
            sl[i] = new AjaxControlToolkit.Slide("Images/noimage.gif", "", sb);
        }
        return sl;
    }


    [WebMethod(EnableSession = true)]
    public AjaxControlToolkit.Slide[] GetCongratulations()
    {
        TSP.DataManager.CondolenceManager CondolenceManager = new TSP.DataManager.CondolenceManager();
        DataTable dt = CondolenceManager.SelectAvailableCondolence(Utility.GetDateOfToday(), (int)TSP.DataManager.CondolenceManager.Type.Congratulation);
        AjaxControlToolkit.Slide[] sl = new AjaxControlToolkit.Slide[dt.Rows.Count];
        string sb;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb = "<a style='text-decoration: none;color: #000000;' href='CandolenceDetail.aspx?CdlId=" + Utility.EncryptQS(dt.Rows[i]["CdlId"].ToString()) + "'>" +
                   dt.Rows[i]["Summary"].ToString() + "<br /><b>" + dt.Rows[i]["CdlFrom"].ToString() + "</b></a>";
            sl[i] = new AjaxControlToolkit.Slide("Images/noimage.gif", "", sb);
        }
        return sl;
    }
}

