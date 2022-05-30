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

public partial class Employee_Nezam_Nezam : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["SignUpload"] = null;
            Session["HeaderUpload"] = null;

            TSP.DataManager.Permission per = TSP.DataManager.NezamManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            PgMode.Value = "New";
            TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
            CityManager.FindByCode(Utility.GetCurrentCitId());
            txtCitName.Text = CityManager[0]["CitName"].ToString();

            TSP.DataManager.NezamManager NezamManager = new TSP.DataManager.NezamManager();
            NezamManager.Fill();
            if (NezamManager.Count > 0)
            {
                PgMode.Value = "Edit";
                ASPxRoundPanel2.HeaderText = "ویرایش";
                FillForm(NezamManager);
            }
        }
    }
    protected void FillForm(TSP.DataManager.NezamManager NezamManager)
    {
        try
        {
            txtAddress.Text = NezamManager[0]["Address"].ToString();
            txtCodePO.Text = NezamManager[0]["CodePO"].ToString();
            txtDesc.Html = NezamManager[0]["Description"].ToString();
            txtEmail.Text = NezamManager[0]["Email"].ToString();
            txtFax.Text = NezamManager[0]["Fax"].ToString();
            txtName.Text = NezamManager[0]["NezamName"].ToString();
            txtTel1.Text = NezamManager[0]["Tel1"].ToString();
            txtTel2.Text = NezamManager[0]["Tel2"].ToString();
            txtWebsite.Text = NezamManager[0]["Website"].ToString();
            if (!string.IsNullOrEmpty(NezamManager[0]["SignUrl"].ToString()))
            {
                HpSign.ClientVisible = true;
                HpSign.NavigateUrl = NezamManager[0]["SignUrl"].ToString();
                HDFlpSign["name"] = 1;
            }
            else
                HpSign.ClientVisible = false;
            if (!string.IsNullOrEmpty(NezamManager[0]["HeaderUrl"].ToString()))
            {
                HpHeader.ClientVisible = true;
                HpHeader.NavigateUrl = NezamManager[0]["HeaderUrl"].ToString();
            }
            else
                HpHeader.ClientVisible = false;
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }

    }
    protected void Insert()
    {
        TSP.DataManager.NezamManager NezamManager = new TSP.DataManager.NezamManager();

        try
        {
            DataRow dr = NezamManager.NewRow();
            dr["Address"] = txtAddress.Text;
            dr["CitId"] = Utility.GetCurrentCitId();
            if (!string.IsNullOrEmpty(txtCodePO.Text))
                dr["CodePO"] = txtCodePO.Text;
            else
                dr["CodePO"] = DBNull.Value;
            dr["Description"] = txtDesc.Html;
            if (!string.IsNullOrEmpty(txtEmail.Text))
                dr["Email"] = txtEmail.Text;
            else
                dr["Email"] = DBNull.Value;
            dr["Fax"] = txtFax.Text;
            dr["NezamName"] = txtName.Text;
            dr["Tel1"] = txtTel1.Text;
            dr["Tel2"] = txtTel2.Text;
            if (!string.IsNullOrEmpty(txtWebsite.Text))
                dr["Website"] = txtWebsite.Text;
            else
                dr["Website"] = DBNull.Value;
            if (Session["SignUpload"] != null)
            {
                dr["SignUrl"] = "~/image/Nezam/" + Path.GetFileName(Session["SignUpload"].ToString());
                HDFlpSign["name"] = 1;

            }
            else
                dr["SignUrl"] = DBNull.Value;
            if (Session["HeaderUpload"] != null)
                dr["HeaderUrl"] = "~/image/Nezam/" + Path.GetFileName(Session["HeaderUpload"].ToString());
            else
                dr["HeaderUrl"] = DBNull.Value;

            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            NezamManager.AddRow(dr);
            if (NezamManager.Save() > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                PgMode.Value = "Edit";
                ASPxRoundPanel2.HeaderText = "ویرایش";

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
        if (Session["SignUpload"] != null)
        {
            try
            {
                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["SignUpload"].ToString());
                string ImgTarget = Server.MapPath("~/image/Nezam/") + Path.GetFileName(Session["SignUpload"].ToString());
                System.IO.File.Copy(ImgSoource, ImgTarget, true);
                Session["SignUpload"] = null;
                HpSign.ClientVisible = true;
                HpSign.NavigateUrl = NezamManager[0]["SignUrl"].ToString();
            }
            catch (Exception)
            { }
        }
        if (Session["HeaderUpload"] != null)
        {
            try
            {
                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["HeaderUpload"].ToString());
                string ImgTarget = Server.MapPath("~/image/Nezam/") + Path.GetFileName(Session["HeaderUpload"].ToString());
                System.IO.File.Copy(ImgSoource, ImgTarget, true);
                Session["HeaderUpload"] = null;
                HpHeader.ClientVisible = true;
                HpHeader.NavigateUrl = NezamManager[0]["HeaderUrl"].ToString();
            }
            catch (Exception)
            { }
        }
    }
    protected void Edit()
    {
        TSP.DataManager.NezamManager NezamManager = new TSP.DataManager.NezamManager();

        try
        {
            NezamManager.Fill();
            NezamManager[0].BeginEdit();
            NezamManager[0]["Address"] = txtAddress.Text;
            if (!string.IsNullOrEmpty(txtCodePO.Text))
                NezamManager[0]["CodePO"] = txtCodePO.Text;
            NezamManager[0]["Description"] = txtDesc.Html;
            if (!string.IsNullOrEmpty(txtEmail.Text))
                NezamManager[0]["Email"] = txtEmail.Text;
            NezamManager[0]["Fax"] = txtFax.Text;
            NezamManager[0]["NezamName"] = txtName.Text;
            NezamManager[0]["Tel1"] = txtTel1.Text;
            NezamManager[0]["Tel2"] = txtTel2.Text;
            if (!string.IsNullOrEmpty(txtWebsite.Text))
                NezamManager[0]["Website"] = txtWebsite.Text;
            if (Session["SignUpload"] != null)
                NezamManager[0]["SignUrl"] = "~/image/Nezam/" + Path.GetFileName(Session["SignUpload"].ToString());
            if (Session["HeaderUpload"] != null)
                NezamManager[0]["HeaderUrl"] = "~/image/Nezam/" + Path.GetFileName(Session["HeaderUpload"].ToString());
            NezamManager[0].EndEdit();
            NezamManager.Save() ;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
        if (Session["SignUpload"] != null)
        {
            try
            {
                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["SignUpload"].ToString());
                string ImgTarget = Server.MapPath("~/image/Nezam/") + Path.GetFileName(Session["SignUpload"].ToString());
                System.IO.File.Copy(ImgSoource, ImgTarget, true);
                Session["SignUpload"] = null;
                HpSign.ClientVisible = true;
                HpSign.NavigateUrl = NezamManager[0]["SignUrl"].ToString();
            }
            catch (Exception)
            { }
        }
        if (Session["HeaderUpload"] != null)
        {
            try
            {
                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["HeaderUpload"].ToString());
                string ImgTarget = Server.MapPath("~/image/Nezam/") + Path.GetFileName(Session["HeaderUpload"].ToString());
                System.IO.File.Copy(ImgSoource, ImgTarget, true);
                Session["HeaderUpload"] = null;
                HpHeader.ClientVisible = true;
                HpHeader.NavigateUrl = NezamManager[0]["HeaderUrl"].ToString();
            }
            catch (Exception)
            { }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = PgMode.Value;
        if (PageMode == "New")
            Insert();
        else if (PageMode == "Edit")
            Edit();
    }
    protected void flpSign_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageSign(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected string SaveImageSign(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Nezam/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["SignUpload"] = tempFileName;

        }
        return ret;
    }
    protected void flpHeader_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageHeader(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected string SaveImageHeader(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Nezam/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["HeaderUpload"] = tempFileName;

        }
        return ret;
    }
}
