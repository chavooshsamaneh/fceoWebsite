using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;

public partial class Employee_News_News : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
        {
            ObjectDataSourceNews.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentAgentCode().ToString();
            GridViewNews.DataBind();
        }
        else
        {
            ObjectDataSourceNews.SelectParameters["AgentId"].DefaultValue = "-1";
            GridViewNews.DataBind();
        }
        if (!IsPostBack)
        {

            TSP.DataManager.Permission per = TSP.DataManager.NewsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled =
            btnDelete2.Enabled = per.CanDelete;
            btnActive.Enabled = btnActive2.Enabled = btnInActive.Enabled = btnInActive2.Enabled = btnEdit.Enabled =
            btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled =
            BtnNew2.Enabled = per.CanNew;
            btnView.Enabled =
            btnView2.Enabled =
            GridViewNews.Visible = per.CanView;

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewsInsert1.aspx?NewsId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int NewsId = -1;
        if (GridViewNews.FocusedRowIndex > -1)
        {
            DataRow row = GridViewNews.GetDataRow(GridViewNews.FocusedRowIndex);
            NewsId = (int)row["NewsId"];
        }
        if (NewsId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("NewsInsert1.aspx?NewsId=" + Utility.EncryptQS(NewsId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int NewsId = -1;
        if (GridViewNews.FocusedRowIndex > -1)
        {
            DataRow row = GridViewNews.GetDataRow(GridViewNews.FocusedRowIndex);
            NewsId = (int)row["NewsId"];
        }
        if (NewsId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("ShowNews.aspx?NewsId=" + Utility.EncryptQS(NewsId.ToString()) + "&show=" + Utility.EncryptQS("1"));
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int NewsId = -1;
        if (GridViewNews.FocusedRowIndex > -1)
        {
            DataRow row = GridViewNews.GetDataRow(GridViewNews.FocusedRowIndex);
            NewsId = (int)row["NewsId"];
        }
        if (NewsId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
            TSP.DataManager.NewsManager NsManager = new TSP.DataManager.NewsManager();
            TSP.DataManager.NewsImgManager ImgManager = new TSP.DataManager.NewsImgManager();
            TSP.DataManager.NewsIdeaManager IdeaManager = new TSP.DataManager.NewsIdeaManager();

            trans.Add(ImgManager);
            trans.Add(IdeaManager);
            trans.Add(NsManager);

            NsManager.FindByCode(NewsId);
            ImgManager.FindByNewsCode(NewsId);
            IdeaManager.FindByNewsCode(NewsId);
            string ImgUrl = "";
            System.Collections.ArrayList DeleteFiles = new System.Collections.ArrayList();

            if (NsManager.Count == 1)
                try
                {
                    trans.BeginSave();
                    if (ImgManager.Count > 0)
                    {
                        int y = ImgManager.Count;
                        for (int i = 0; i < y; i++)
                        {
                            DeleteFiles.Add(ImgManager[0]["ImgUrl"].ToString());
                            ImgManager[0].Delete();
                        }
                        ImgManager.Save();
                    }

                    if (IdeaManager.Count > 0)
                    {
                        int z = IdeaManager.Count;
                        for (int i = 0; i < z; i++)
                        {
                            IdeaManager[0].Delete();
                        }
                        IdeaManager.Save();
                    }

                    if (!Utility.IsDBNullOrNullValue(NsManager[0]["AttachmentUrl"]))
                    {
                        string FileName = System.IO.Path.GetFileName(NsManager[0]["AttachmentUrl"].ToString());
                        ImgUrl = Server.MapPath("~/image/News/") + FileName;
                    }
                    NsManager[0].Delete();

                    int cnt = NsManager.Save();
                    trans.EndSave();

                    if (cnt == 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    }
                    else
                    {
                        GridViewNews.DataBind();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام شد";
                    }
                }
                catch (Exception err)
                {
                    trans.CancelSave();

                    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                        if (se.Number == 547)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                    }
                }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "چنین ردیفی وجود ندارد.مجددا بازخوانی نمایید";
            }

            try
            {
                if (ImgUrl == "") return;
                if (File.Exists(ImgUrl))
                    File.Delete(ImgUrl);
            }
            catch (Exception ex)
            {
                trans.CancelSave();
                Utility.SaveWebsiteError(ex);
                string Message = "ذخیره انجام شد. خطایی در حذف فایل انجام گرفته است";
                if (Utility.ShowExceptionError())
                {
                    Message = Message + ex.Message;
                }
                this.DivReport.Visible = true;
                this.LabelWarning.Text = Message;
            }

            //------------delete file-------------
            if (!Utility.IsDBNullOrNullValue(DeleteFiles))
            {
                for (int i = 0; i < DeleteFiles.Count; i++)
                {
                    try
                    {
                        string filename = Server.MapPath(DeleteFiles[i].ToString());
                        if (System.IO.File.Exists(filename))
                            System.IO.File.Delete(filename);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        UpdateInActive(true);
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        UpdateInActive(false);
    }


    protected void GridViewNews_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
        {
            e.Editor.Style["direction"] = "ltr";
        }
        if (e.Column.FieldName == "ExpireDate")
        {
            e.Editor.Style["direction"] = "ltr";
        }
    }

    protected void GridViewNews_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
        {
            e.Cell.Style["direction"] = "ltr";
        }
        if (e.DataColumn.FieldName == "ExpireDate")
        {
            e.Cell.Style["direction"] = "ltr";
        }
    }
    #endregion

   

    private void UpdateInActive(Boolean InActive)
    {
         try
        {
            int NewsId = -1;
            if (GridViewNews.FocusedRowIndex > -1)
            {
                DataRow row = GridViewNews.GetDataRow(GridViewNews.FocusedRowIndex);
                NewsId = (int)row["NewsId"];
            }
            if (NewsId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";
                return;
            }

            TSP.DataManager.NewsManager NewsManager = new TSP.DataManager.NewsManager();
            NewsManager.FindByCode(NewsId);
            if (NewsManager.Count != 1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " خطایی در ذخیره انجام گرفته است";
                return;
            }
            NewsManager[0].BeginEdit();
            NewsManager[0]["InActive"] = InActive;
            NewsManager[0].EndEdit();
            NewsManager.Save();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
            GridViewNews.DataBind();
            
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = " خطایی در ذخیره انجام گرفته است";
        }
    }
}
