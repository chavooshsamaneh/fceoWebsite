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
using DevExpress.Web;
using System.IO;

public partial class Employee_Amoozesh_CourseAttachments : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["CrsAttachUpload"] = null;

            TSP.DataManager.Permission per = TSP.DataManager.CourseManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnDelete.Enabled = per.CanEdit;
            btnDelete2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew;
            GridViewAttachment.Visible = per.CanView;

            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["CrsId"]))
            {
                Response.Redirect("AddCourses.aspx");
                return;
            }
            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                CourseId.Value = Server.HtmlDecode(Request.QueryString["CrsId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string CrsId = Utility.DecryptQS(CourseId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            GridViewAttachment.DataSource = attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Course, int.Parse(CrsId));
            GridViewAttachment.DataBind();


            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        btnBack.PostBackUrl = "AddCourses.aspx?CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"];
        ASPxButton1.PostBackUrl = "AddCourses.aspx?CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"];

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string fileNameImg = "";

        bool AxImg = false;
        try
        {
            TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();
            DataRow dr = attManager.NewRow();
            dr["TtId"] = (int)TSP.DataManager.TableCodes.Course;
            dr["RefTable"] = Utility.DecryptQS(CourseId.Value);
            dr["AttId"] = 3;
            dr["IsValid"] = 1;
            dr["Description"] = txtDesc.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModfiedDate"] = DateTime.Now;

            try
            {

                if (Session["CrsAttachUpload"] != null)
                {

                    fileNameImg = Path.GetFileName(Session["CrsAttachUpload"].ToString());

                    dr["FilePath"] = "~/Image/Employee/Amoozesh/CourseAttachments/" + Path.GetFileName(Session["CrsAttachUpload"].ToString());

                    AxImg = true;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "فایل مورد نظر را انتخاب نمایید";
                    return;

                }

                imgEndUploadImg.ClientVisible = false;
            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " خطایی در ذخیره رخ داده است";
            }

            attManager.AddRow(dr);
            int cnt = attManager.Save();
            if (cnt == 1)
            {
                if (AxImg == true)
                {
                    try
                    {
                        string ImgSoource = Session["CrsAttachUpload"].ToString();
                        string ImgTarget = Server.MapPath("~/Image/Employee/Amoozesh/CourseAttachments/") + fileNameImg;
                        File.Move(ImgSoource, ImgTarget);
                    }
                    catch
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = " خطایی در ذخیره رخ داده است";

                    }
                }
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";
                Session["CrsAttachUpload"] = null;

                txtDesc.Text = "";
                GridViewAttachment.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Course, int.Parse(Utility.DecryptQS(CourseId.Value)));
                GridViewAttachment.DataBind();

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    txtDesc.Text = "";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                txtDesc.Text = "";
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int AttachId = -1;
        TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();

        GridViewAttachment.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Course, int.Parse(Utility.DecryptQS(CourseId.Value)));
        GridViewAttachment.DataBind();

        if (GridViewAttachment.FocusedRowIndex > -1)
        {
            DataRow row = GridViewAttachment.GetDataRow(GridViewAttachment.FocusedRowIndex);
            AttachId = (int)row["AttachId"];
        }
        if (AttachId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {

            attManager.FindByCode(AttachId);
            if (attManager.Count == 1)
            {
                try
                {
                    string url = attManager[0]["FilePath"].ToString();

                    attManager[0].Delete();

                    int cn = attManager.Save();
                    if (cn == 1)
                    {
                        if ((!string.IsNullOrEmpty(url)) && (File.Exists(Server.MapPath(url))))
                            File.Delete(Server.MapPath(url));


                        GridViewAttachment.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Course, int.Parse(Utility.DecryptQS(CourseId.Value)));
                        GridViewAttachment.DataBind();

                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام شد";

                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                    }

                }
                catch (Exception err)
                {

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

            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddCourses.aspx?CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
    }

    protected void flp_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void GridViewAttachment_PageIndexChanged(object sender, EventArgs e)
    {
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        GridViewAttachment.DataSource = attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Course, int.Parse(Utility.DecryptQS(CourseId.Value)));
        GridViewAttachment.DataBind();

    }

    protected void ASPxHyperLink1_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxHyperLink hp = (DevExpress.Web.ASPxHyperLink)sender;
        hp.Text = Path.GetFileName(hp.Value.ToString());
    }

    protected void MenuCourseDetail_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Refrences":
                Response.Redirect("CourseRefrences.aspx?CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "CourseDetail":
                Response.Redirect("AddCourseDetails.aspx?CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Course":
                Response.Redirect("AddCourses.aspx?CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Group":
                Response.Redirect("CourseGroups.aspx?CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Prerequisite":
                Response.Redirect("CoursePrerequisite.aspx?CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
        }
    }
    #endregion

    #region Methods
    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetFileNameWithoutExtension(uploadedFile.PostedFile.FileName) + "_" + Utility.GenRandomNum() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/Employee/Amoozesh/CourseAttachments/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["CrsAttachUpload"] = tempFileName;
        }
        return ret;
    }
    #endregion
}
