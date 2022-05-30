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

public partial class Members_EngOffice_EngOfficeAttachment : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}

        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["OffAttachUpload"] = null;
            Session["OffAttachUploadName"] = null;


            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["EOfId"]))
            {
                Response.Redirect("EngOffice.aspx");
                return;
            }
            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                EngFileId.Value = Server.HtmlDecode(Request.QueryString["EOfId"]).ToString();
                EngOfficeId.Value = Server.HtmlDecode(Request.QueryString["EngOfId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }


            string EOfId = Utility.DecryptQS(EngFileId.Value);

            TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
            FileManager.FindByCode(int.Parse(EOfId));
            if (FileManager.Count > 0)
            {
                if (Convert.ToBoolean(FileManager[0]["Requester"]))//FromEmployee
                {
                    btnDelete.Enabled = false;
                    btnDelete2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnSave.Enabled = false;
                }
                if (FileManager[0]["IsConfirm"].ToString() != "0") //answered
                {
                    btnDelete.Enabled = false;
                    btnDelete2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnSave.Enabled = false;

                }
            }

            int MeId = Utility.GetCurrentUser_MeId();
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            OfMeManager.FindEngOfficeMemberByPersonId(MeId);
            if (OfMeManager.Count > 0)
            {
                if (Convert.ToInt32(OfMeManager[0]["OfpId"]) == (int)TSP.DataManager.OfficePosition.EngOfficeEmployed)//عضو دفتر
                {
                    btnDelete.Enabled = false;
                    btnDelete2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnSave.Enabled = false;
                    
                }

            }

            ObjectDataSource1.SelectParameters[0].DefaultValue = ((int)TSP.DataManager.TableCodes.EngOffFile).ToString();
            ObjectDataSource1.SelectParameters[1].DefaultValue = EOfId;


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
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Member":
                Response.Redirect("EngOfficeMember.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;

            case "EngOffice":
                Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;

            case "Job":
                Response.Redirect("EngOfficeJob.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.MemberManager MemberManager = Session["MemberManager"] as TSP.DataManager.MemberManager;
        if (MemberManager == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }
        if ((bool)MemberManager[0]["IsLock"] == true)
        {
            string lockers = Utility.GetFormattedObject(Session["MemberLockers"]);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
            return;

        }

        string fileNameImg = "";
        bool AxImg = false;
        try
        {
            TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();

            DataRow dr = attManager.NewRow();
            dr["TtId"] = (int)TSP.DataManager.TableCodes.EngOffFile;
            dr["RefTable"] = Utility.DecryptQS(EngFileId.Value);
            dr["AttId"] = 1;
            dr["IsValid"] = 1;
            dr["Description"] = txtDesc.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModfiedDate"] = DateTime.Now;

            try
            {
                if (Session["OffAttachUpload"] != null)
                {
                    fileNameImg = Path.GetFileName(Session["OffAttachUpload"].ToString());

                    dr["FilePath"] = "~/Image/EngOffice/Attachments/" + fileNameImg;
                    dr["FileName"] = Session["OffAttachUploadName"];

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

                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";
                txtDesc.Text = "";
                //CustomAspxDevGridView1.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.EngOffFile, int.Parse(Utility.DecryptQS(EngFileId.Value)));
                CustomAspxDevGridView1.DataBind();
                Session["MeAttachUpload"] = null;
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
        if (AxImg == true)
        {
            try
            {
                string ImgSoource = Session["OffAttachUpload"].ToString();
                string ImgTarget = Server.MapPath("~/image/EngOffice/Attachments/") + fileNameImg;
                File.Copy(ImgSoource, ImgTarget, true);
            }
            catch (Exception)
            {
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (IsPageRefresh)
            return;
        TSP.DataManager.MemberManager MemberManager = Session["MemberManager"] as TSP.DataManager.MemberManager;
        if (MemberManager == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }
        if ((bool)MemberManager[0]["IsLock"] == true)
        {
            string lockers = Utility.GetFormattedObject(Session["MemberLockers"]);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
            return;

        }

        int AttachId = -1;
        TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();

        //CustomAspxDevGridView1.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.EngOffFile, int.Parse(Utility.DecryptQS(EngFileId.Value)));
        //CustomAspxDevGridView1.DataBind();

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
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
                    attManager[0].Delete();

                    int cn = attManager.Save();
                    if (cn == 1)
                    {
                        CustomAspxDevGridView1.DataBind();

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
        Session["OffAttachUpload"] = null;
        Session["OffAttachUploadName"] = null;

        Response.Redirect("EngOfficeRegister.aspx?EOfId=" + EngFileId.Value + "&PageMode=" + PgMode.Value + "&EngOfId=" + EngOfficeId.Value);
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
    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                Session["OffAttachUploadName"] = Path.GetFileName(uploadedFile.PostedFile.FileName);

                ret = Path.GetFileNameWithoutExtension(uploadedFile.PostedFile.FileName) + "_" + Utility.GenRandomNum() + ImageType.Extension;

                //ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/EngOffice/Attachments/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["OffAttachUpload"] = tempFileName;
        }
        return ret;
    }
    protected void ASPxHyperLink1_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxHyperLink hp = (DevExpress.Web.ASPxHyperLink)sender;
        hp.Text = Path.GetFileName(hp.Value.ToString());
    }

    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;
        if (EngFileId.Value != null)
        {
            string EOfId = Utility.DecryptQS(EngFileId.Value);
            if (e.GetValue("RefTable") == null)
                return;
            string CurretnEOfId = e.GetValue("RefTable").ToString();
            if (EOfId == CurretnEOfId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }
}
