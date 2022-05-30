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

public partial class ImageGallery_ViewImageGallery : System.Web.UI.Page
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

        Load_SlidShow(AlbumId);
    }

    void Load_SlidShow(int AlbumId)
    {
        TSP.DataManager.GalleryImagesManager ImagesManager = new TSP.DataManager.GalleryImagesManager();
        ImagesManager.FindByAlbum(AlbumId);

        String Images = "";
        String[] FileNames;
        int TotalPicCount = CopyImages(AlbumId, out FileNames);
        if (TotalPicCount > 0)
        {
            lblAlbumName.Text = "آلبوم: " + ImagesManager[0]["AlbumName"].ToString();
            lblDescription.Text = "";

            lblSlidShow.Text = "<div id=\"thumbwrapper\"><div id=\"thumbarea\">";
            lblSlidShow.Text += "<ul id=\"thumbs\">";
            for (int i = 0; i < TotalPicCount; i++)
                if (String.IsNullOrEmpty(FileNames[i]) == false && System.IO.File.Exists(MapPath("~/ImageGallery/thumbs/" + FileNames[i] + ".jpg")))
                {
                    Images += (String.IsNullOrEmpty(Images.ToString())) ? FileNames[i] : ";" + FileNames[i];
                    lblSlidShow.Text += "<li value=\"" + (i + 1) + "\" text=\"" + FileNames[i] + "\"><img src=\"thumbs/" + FileNames[i] + ".jpg\" width=\"179\" height=\"100\" alt=\"\" /></li>";
                }
            lblSlidShow.Text += "</ul></div></div>";
            lblSlidShow.Text += "<script type=\"text/javascript\">";
            lblSlidShow.Text += "var imgid = 'image';";
            lblSlidShow.Text += "var imgdir = 'fullsize';";
            lblSlidShow.Text += "var imgext = '.jpg';";
            lblSlidShow.Text += "var thumbid = 'thumbs';";
            lblSlidShow.Text += "var auto = true;";
            lblSlidShow.Text += "var autodelay = 5;";
            lblSlidShow.Text += "</script><script type=\"text/javascript\" src=\"slide.js\"></script>";
            hiddenImageInfo["Images"] = Images;
        }
        else
        {
            panelNoPic.Visible = true;
            panelGallery.Visible = false;
        }
    }

    int CopyImages(int AlbumId, out String[] FileNames)
    {
        TSP.DataManager.GalleryImagesManager ImagesManager = new TSP.DataManager.GalleryImagesManager();
        ImagesManager.FindByAlbum(AlbumId);

        FileNames = new String[ImagesManager.Count];

        hiddenImageInfo.Clear();
        for (int i = 0; i < ImagesManager.Count; i++)
        {
            String Info = ImagesManager[i]["ImageName"].ToString();
            hiddenImageInfo.Add("img" + (i + 1), Info);

            FileNames[i] = System.IO.Path.GetFileName(MapPath(ImagesManager[i]["ImgUrl"].ToString()));
            if (System.IO.File.Exists(MapPath(ImagesManager[i]["ImgUrl"].ToString())) == true && System.IO.File.Exists(MapPath("~/ImageGallery/thumbs/" + System.IO.Path.GetFileName(MapPath(ImagesManager[i]["ImgUrl"].ToString())) + ".jpg")) == false)
            {
                System.IO.File.Copy(MapPath(ImagesManager[i]["ImgUrl"].ToString().Replace("Fullsize", "Thumbs")), MapPath("~/ImageGallery/thumbs/" + FileNames[i] + ".jpg"), false);
                System.IO.File.Copy(MapPath(ImagesManager[i]["ImgUrl"].ToString()), MapPath("~/ImageGallery/fullsize/" + FileNames[i] + ".jpg"), false);
            }
        }

        return ImagesManager.Count;
    }

    protected void CallBackImageInfo_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        lblImageInfo.Text = hiddenImageInfo["img" + e.Parameter].ToString();
    }
}
