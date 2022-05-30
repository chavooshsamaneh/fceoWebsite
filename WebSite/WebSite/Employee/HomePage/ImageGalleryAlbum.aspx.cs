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

public partial class Employee_HomePage_ImageGalleryAlbums : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            TSP.DataManager.Permission per = TSP.DataManager.GalleryAlbumsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnActive.Enabled = per.CanEdit;
            btnActive.Enabled = per.CanEdit;
            btnInActive.Enabled = per.CanEdit;
            btnInActive.Enabled = per.CanEdit;
            GridViewAlbum.Visible = per.CanView;
            btnImages.Enabled = per.CanView;
            btnImages2.Enabled = per.CanView;

            this.ViewState["BtnImages"] = btnImages.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnActive"] = btnActive.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            GridViewAlbum.DataBind();

        }

        if (this.ViewState["BtnImages"] != null)
            this.btnImages.Enabled = this.btnImages2.Enabled = (bool)this.ViewState["BtnImages"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnActive"] != null)
            this.btnActive.Enabled = this.btnActive2.Enabled = (bool)this.ViewState["BtnActive"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        GridViewAlbum.JSProperties["cpMessage"] = "";

        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnImages_Click(object sender, EventArgs e)
    {
        if (GridViewAlbum.FocusedRowIndex > -1)
        {
            int AlbumId = Convert.ToInt32(GridViewAlbum.GetDataRow(GridViewAlbum.FocusedRowIndex)["AlbumId"].ToString());
            TSP.DataManager.GalleryAlbumsManager AlbumsManager = new TSP.DataManager.GalleryAlbumsManager();
            AlbumsManager.FindById(AlbumId);
            if (AlbumsManager.Count > 0)
            {
                if ((Boolean)AlbumsManager[0]["InActive"] == false)
                    Response.Redirect("ImageGalleryImages.aspx?album=" + Utility.EncryptQS(AlbumId.ToString()));
                else
                    ShowMessage("آلبوم غیر فعال می باشد");
            }
            else
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
            }
        }
        else
        {
            ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int AlbumId = -1;
        if (GridViewAlbum.FocusedRowIndex > -1)
        {
            try
            {
                DataRow Row = GridViewAlbum.GetDataRow(GridViewAlbum.FocusedRowIndex);
                AlbumId = Convert.ToInt32(Row["AlbumId"]);
                TSP.DataManager.GalleryAlbumsManager AlbumsManager = new TSP.DataManager.GalleryAlbumsManager();
                AlbumsManager.FindById(AlbumId);
                if (AlbumsManager.Count > 0)
                {
                    DataRow EditRow = AlbumsManager[0];

                    if (EditRow != null)
                    {
                        EditRow.BeginEdit();

                        EditRow["InActive"] = true;
                        EditRow["UserId"] = Utility.GetCurrentUser_UserId();
                        EditRow["ModifiedDate"] = DateTime.Now;
                        EditRow.EndEdit();

                        int cn = AlbumsManager.Save();
                        if (cn > 0)
                        {
                            GridViewAlbum.DataBind();
                            ShowMessage("ذخیره انجام شد.");
                        }
                        else
                        {
                            ShowMessage("خطایی در ذخیره انجام گرفته است.");
                        }
                    }
                }
                else
                {
                    ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                }
            }
            catch (Exception err)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
            }
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        int AlbumId = -1;
        if (GridViewAlbum.FocusedRowIndex > -1)
        {
            try
            {
                DataRow Row = GridViewAlbum.GetDataRow(GridViewAlbum.FocusedRowIndex);
                AlbumId = Convert.ToInt32(Row["AlbumId"]);
                TSP.DataManager.GalleryAlbumsManager AlbumsManager = new TSP.DataManager.GalleryAlbumsManager();
                AlbumsManager.FindById(AlbumId);
                if (AlbumsManager.Count > 0)
                {
                    DataRow EditRow = AlbumsManager[0];

                    if (EditRow != null)
                    {
                        EditRow.BeginEdit();

                        EditRow["InActive"] = false;
                        EditRow["UserId"] = Utility.GetCurrentUser_UserId();
                        EditRow["ModifiedDate"] = DateTime.Now;
                        EditRow.EndEdit();

                        int cn = AlbumsManager.Save();
                        if (cn > 0)
                        {
                            GridViewAlbum.DataBind();
                            ShowMessage("ذخیره انجام شد.");
                        }
                        else
                        {
                            ShowMessage("خطایی در ذخیره انجام گرفته است.");
                        }
                    }
                }
                else
                {
                    ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                }
            }
            catch (Exception err)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
            }
        }
    }

    protected void GridViewAlbum_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        if (Page.IsValid)
        {
            InsertAlbum(e);
        }
    }

    protected void GridViewAlbum_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditAlbum(e);
    }
    #endregion

    #region Methods
    private void InsertAlbum(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        TSP.DataManager.GalleryAlbumsManager AlbumsManager = new TSP.DataManager.GalleryAlbumsManager();
        try
        {
            DataRow Row = AlbumsManager.NewRow();

            Row["AlbumName"] = e.NewValues["AlbumName"].ToString();
            Row["ValidDate"] = e.NewValues["ValidDate"].ToString();
            Row["InActive"] = false;
            Row["UserId"] = Utility.GetCurrentUser_UserId();
            Row["ModifiedDate"] = DateTime.Now;
            AlbumsManager.AddRow(Row);
            int cn = AlbumsManager.Save();
            if (cn > 0)
            {
                GridViewAlbum.JSProperties["cpMessage"] = "ذخیره انجام شد.";
            }
            else
            {
                GridViewAlbum.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
            }
            GridViewAlbum.CancelEdit();
            GridViewAlbum.DataBind();
        }
        catch (Exception err)
        {
            GridViewAlbum.CancelEdit();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    GridViewAlbum.JSProperties["cpMessage"] = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    GridViewAlbum.JSProperties["cpMessage"] = "کد درس تکراری می باشد.";
                }
                else
                {
                    GridViewAlbum.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                GridViewAlbum.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
            }
        }

    }

    private void EditAlbum(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            TSP.DataManager.GalleryAlbumsManager AlbumsManager = new TSP.DataManager.GalleryAlbumsManager();
            AlbumsManager.FindById(Convert.ToInt32(e.Keys[0]));
            DataRow Row = AlbumsManager[0];

            if (Row != null)
            {
                Row.BeginEdit();

                Row["AlbumName"] = e.NewValues["AlbumName"];
                Row["ValidDate"] = e.NewValues["ValidDate"].ToString();
                Row["UserId"] = Utility.GetCurrentUser_UserId();
                Row["ModifiedDate"] = DateTime.Now;
                Row.EndEdit();
                int cn = AlbumsManager.Save();
                if (cn > 0)
                {
                    GridViewAlbum.JSProperties["cpMessage"] = "ذخیره انجام شد.";
                }
                else
                {
                    GridViewAlbum.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است.";
                }

            }
            GridViewAlbum.CancelEdit();
            GridViewAlbum.DataBind();
        }
        catch (Exception err)
        {
            GridViewAlbum.CancelEdit();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    GridViewAlbum.JSProperties["cpMessage"] = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    GridViewAlbum.JSProperties["cpMessage"] = "کد درس تکراری می باشد.";
                }
                else
                {
                    GridViewAlbum.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                GridViewAlbum.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}
