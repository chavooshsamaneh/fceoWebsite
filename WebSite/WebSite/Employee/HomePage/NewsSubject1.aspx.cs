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

public partial class Employee_News_NewsSubject1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.NewsSubjectManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;

            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;


            //    ViewState["PMode"] = "";
            PageMode = "";


            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        //    string PageMode = Utility.DecryptQS(ViewState["PMode"].ToString());

        int SubId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            SubId = (int)row["SubId"];
        }

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {

            if (PageMode == "New")
            {

                Insert();

            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(SubId.ToString()))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {

                    Edit(SubId);
                }

            }

        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int SubId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            SubId = (int)row["SubId"];
        }
        if (SubId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.NewsSubjectManager SubManager = new TSP.DataManager.NewsSubjectManager();

            SubManager.FindByCode(SubId);
            if (SubManager.Count == 1)
            {
                try
                {
                    if (DeletePicture(SubManager))
                    {
                        SubManager[0].Delete();
                        int cn = SubManager.Save();
                        if (cn == 1)
                        {
                            CustomAspxDevGridView1.DataBind();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "حذف انجام شد";
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "حذف انجام نشد";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف تصویر انجام گرفته است";
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
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        //  ViewState["PMode"] = Utility.EncryptQS("New");
        PageMode = "New";
        ASPxPopupControl1.HeaderText = "جدید";
        ClearForm();
        Enable();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int SubId = -1;
        string Subject = "";

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            SubId = (int)row["SubId"];
            Subject = row["Name"].ToString();
            if (!Utility.IsDBNullOrNullValue(row["ImageUrl"]))
            {
                imgNewsSubject.ClientVisible = true;
                imgNewsSubject.ImageUrl = row["ImageUrl"].ToString();
                Session["filename"] = row["ImageUrl"].ToString();
            }
            else
            {
                imgNewsSubject.ClientVisible = false;
                Session["filename"] = "";
            }
        }
        if (SubId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Enable();
            btnSave.Visible = true;
            txtSub.Text = Subject;
            //  ViewState["PMode"] = Utility.EncryptQS("Edit"); 
            PageMode = "Edit";
            ASPxPopupControl1.HeaderText = "ویرایش";

        }


    }
    protected void FileUploadIcon_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SavePostedFile(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected void CallbackPanelPopup_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] parameter = e.Parameter.Split(';');
        CallbackPanelPopup.JSProperties["cpIsFormOK"] = 0;
        switch (parameter[0])
        {
            case "New":
                BtnNew_Click(null, null);
                CallbackPanelPopup.JSProperties["cpIsFormOK"] = 1;
                break;
            case "Edit":
                btnEdit_Click(null, null);
                CallbackPanelPopup.JSProperties["cpIsFormOK"] = 1;
                break;
            default:
                CallbackPanelPopup.JSProperties["cpIsFormOK"] = 0;
                break;
        }
    }

    #region Properties
    public string PageMode
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldModeID["PgMode"].ToString());
        }
        set
        {
            HiddenFieldModeID["PgMode"] = Utility.EncryptQS(value.ToString());
        }
    }
    #endregion

    protected void ClearForm()
    {
        txtSub.Text = "";
        imgNewsSubject.ClientVisible = false;
        imgNewsSubject.ImageUrl = "";
    }
    protected void Enable()
    {
        txtSub.Enabled = true;
    }
    protected void Disable()
    {
        txtSub.Enabled = false;
    }
    protected void Insert()
    {
        TSP.DataManager.NewsSubjectManager SubManager = new TSP.DataManager.NewsSubjectManager();
        DataRow dr = SubManager.NewRow();

        try
        {
            dr["Name"] = txtSub.Text;
            if (!Utility.IsDBNullOrNullValue(Session["filename"]))
                dr["ImageUrl"] = "~/Image/HomePage/NewsSubject/" + System.IO.Path.GetFileName(Session["filename"].ToString());
            //    else dr["ImageUrl"] = "~/Image/HomePage/NewsSubject/7784.png";
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;

            SubManager.AddRow(dr);
            int cnt = SubManager.Save();

            if (cnt > 0)
            {
                CustomAspxDevGridView1.DataBind();
                this.txtSub.Text = "";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
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
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }


        try
        {
            if (!Utility.IsDBNullOrNullValue(Session["filename"]))
            {
                System.IO.File.Move(Session["filename"].ToString(), MapPath("~/Image/HomePage/NewsSubject/") + System.IO.Path.GetFileName(Session["filename"].ToString()));
                imgNewsSubject.ImageUrl = MapPath("~/Image/HomePage/NewsSubject/") + System.IO.Path.GetFileName(Session["filename"].ToString());
                Session["filename"] = null;
            }
        }
        catch
        {
        }

    }
    protected void Edit(int SubId)
    {
        string preImageUrl = null;
        TSP.DataManager.NewsSubjectManager SubManager = new TSP.DataManager.NewsSubjectManager();
        SubManager.FindByCode(SubId);

        if (SubManager.Count == 1)
            try
            {
                SubManager[0].BeginEdit();
                SubManager[0]["Name"] = txtSub.Text;
                if (!Utility.IsDBNullOrNullValue(Session["filename"]))
                {
                    preImageUrl = SubManager[0]["ImageUrl"].ToString();
                    SubManager[0]["ImageUrl"] = "~/Image/HomePage/NewsSubject/" + System.IO.Path.GetFileName(Session["filename"].ToString());
                }
                //   else SubManager[0]["ImageUrl"] = "~/Image/HomePage/NewsSubject/7784.png";
                SubManager[0]["UserId"] =Utility.GetCurrentUser_UserId();
                SubManager[0].EndEdit();
                int cnt = SubManager.Save();
                if (cnt == 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    if (preImageUrl != null)
                        if (System.IO.File.Exists(Server.MapPath(preImageUrl)))
                            System.IO.File.Delete(Server.MapPath(preImageUrl));

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                    CustomAspxDevGridView1.DataBind();
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
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "چنین ردیفی وجود ندارد.مجددا بازخوانی نمایید";
        }

        try
        {
            if (!Utility.IsDBNullOrNullValue(Session["filename"]))
            {
                System.IO.File.Move(Session["filename"].ToString(), MapPath("~/Image/HomePage/NewsSubject/") + System.IO.Path.GetFileName(Session["filename"].ToString()));
                imgNewsSubject.ImageUrl = MapPath("~/Image/HomePage/NewsSubject/") + System.IO.Path.GetFileName(Session["filename"].ToString());
                Session["filename"] = null;
            }
        }
        catch
        {
        }
    }
    public string SavePostedFile(UploadedFile uploadedFile)
    {
        string ret = "";
        Session["filename"] = null;
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new System.IO.FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/HomePage/NewsSubject/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["filename"] = tempFileName;
        }
        return ret;
    }
    bool DeletePicture(TSP.DataManager.NewsSubjectManager SubManager)
    {
        string ImageUrl = SubManager[0]["ImageUrl"].ToString();
        if (System.IO.File.Exists(Server.MapPath(ImageUrl)))
        {
            System.IO.File.Delete(Server.MapPath(ImageUrl));
            return true;
        }
        return false;
    }
}
