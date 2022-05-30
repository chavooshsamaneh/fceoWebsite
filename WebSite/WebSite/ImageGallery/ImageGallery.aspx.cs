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

public partial class ImageGallery_ImageGallery : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  GridViewAlbum.DataBind();
           DataViewAlbum.DataBind();
           Search();
        }
        DataViewAlbum.JSProperties["cpMessage"] = "";
      //  GridViewAlbum.JSProperties["cpMessage"] = "";
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        this.DivReport.Visible = false;
    }

    protected void btnImages_Click(object sender, EventArgs e)
    {
        //if (GridViewAlbum.FocusedRowIndex > -1)
        //{
        //    int AlbumId = Convert.ToInt32(GridViewAlbum.GetDataRow(GridViewAlbum.FocusedRowIndex)["AlbumId"].ToString());
        //    TSP.DataManager.GalleryAlbumsManager AlbumsManager = new TSP.DataManager.GalleryAlbumsManager();
        //    AlbumsManager.FindById(AlbumId);
        //    if (AlbumsManager.Count > 0)
        //    {
        //        if ((Boolean)AlbumsManager[0]["InActive"] == false)
        //        {
        //            TSP.DataManager.GalleryImagesManager ImagesManager = new TSP.DataManager.GalleryImagesManager();
        //            ImagesManager.FindByAlbum(AlbumId);
        //            if (ImagesManager.Count > 0)
        //                Response.Redirect("ImageGalleryFrame.aspx?album=" + Utility.EncryptQS(AlbumId.ToString()));
        //            else
        //                ShowMessage("تصویری برای این آلبوم ثبت نشده است");
        //        }
        //        else
        //            ShowMessage("آلبوم غیر فعال می باشد");
        //    }
        //    else
        //    {
        //        ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
        //    }
        //}
        //else
        //{
        //    ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
        //}
    }

    protected void btnViewGallery_DataBinding(object sender, EventArgs e)
    {
        LinkButton btnViewGallery = (LinkButton)sender;
        btnViewGallery.PostBackUrl = "ImageGalleryFrame.aspx?album=" + Utility.EncryptQS(btnViewGallery.ToolTip);
        btnViewGallery.ToolTip = "";
    }
    protected void Image_DataBinding(object sender, EventArgs e)
    {
        Image img = (Image)sender;
        if (string.IsNullOrEmpty(img.ImageUrl))
            img.ImageUrl = "~/images/noimage.gif";
        else
            img.ImageUrl = img.ImageUrl;
    }
    protected void DataViewAlbum_CustomCallback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "search") Search();
    }
    #endregion

    #region Methods
    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    void Search()
    {
        if (!string.IsNullOrEmpty(txtAlbumName.Text))
            ObjdsAlbums.SelectParameters["AlbumName"].DefaultValue = txtAlbumName.Text.Trim();

        if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text.Trim()))
        {
            ObjdsAlbums.SelectParameters["FromDate"].DefaultValue = txtFromDate.Text.Trim();
            ObjdsAlbums.SelectParameters["ToDate"].DefaultValue = txtToDate.Text.Trim();
        }
        else
            if (!string.IsNullOrEmpty(txtFromDate.Text) && string.IsNullOrEmpty(txtToDate.Text))
                ObjdsAlbums.SelectParameters["FromDate"].DefaultValue = txtFromDate.Text.Trim();
            else
                if (string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                    ObjdsAlbums.SelectParameters["ToDate"].DefaultValue = txtToDate.Text.Trim();
        DataViewAlbum.DataBind();
    }
    #endregion

}
