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
using System.Drawing;
using DevExpress.Web;
using System.IO;

public partial class Employee_Document_AddGovManagerName : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["GmnId"])))
            {
                Response.Redirect("GovManagerName.aspx");
                return;
            }
            TSP.DataManager.Permission per = TSP.DataManager.GovManagerNameManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                ManagerId.Value = Server.HtmlDecode(Request.QueryString["GmnId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            string PageMode = Utility.DecryptQS(PgMode.Value);
            string GmnId = Utility.DecryptQS(ManagerId.Value);
            ObjectDataSourcePrintAssigner.SelectParameters["GmnId"].DefaultValue = GmnId;
            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "View":
                    Disable();

                    if (string.IsNullOrEmpty(GmnId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;

                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(GmnId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    btnRemoveFile.ClientVisible = false;
                    break;


                case "New":
                    Enable();

                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    ASPxRoundPanel2.HeaderText = "جدید";

                    btnRemoveFile.ClientVisible = false;
                    ClearForm();
                    break;
                case "Edit":
                    Enable();

                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    if (string.IsNullOrEmpty(GmnId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    btnRemoveFile.ClientVisible = true;
                    FillForm(int.Parse(GmnId));
                    ASPxRoundPanel2.Enabled = true;
                    ASPxRoundPanel2.HeaderText = "ویرایش";


                    break;


            }

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }
    protected void FillForm(int GmnId)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.GovManagerNameManager manager = new TSP.DataManager.GovManagerNameManager();
        manager.FindByCode(GmnId);
        if (manager.Count == 1)
        {
            txtDesc.Text = manager[0]["Description"].ToString();
            txtGmnName.Text = manager[0]["GmnName"].ToString();
            ComboName.DataBind();
            ComboName.SelectedIndex = ComboName.Items.IndexOfValue(manager[0]["GmtId"]);
            if ((!string.IsNullOrEmpty(manager[0]["SignUrl"].ToString())))
            {
                ImgSign.ImageUrl = manager[0]["SignUrl"].ToString();//+ "?ImageType=" + DateTime.Now.Ticks.ToString();
                HDFlpSign["name"] = 1;
            }
            if (!Utility.IsDBNullOrNullValue(manager[0]["NmcId"]))
            {
                checkboxHaveNmcId.Checked =
                cmbNezamChart.ClientVisible = true;
                txtGmnName.ClientVisible = false;
                cmbNezamChart.DataBind();
                cmbNezamChart.SelectedIndex = cmbNezamChart.Items.FindByValue(manager[0]["NmcId"].ToString()).Index;
            }
            else
            {
                checkboxHaveNmcId.Checked =  cmbNezamChart.ClientVisible=false;
                txtGmnName.ClientVisible = true;
            }
         
        }
    }
    protected void ClearForm()
    {
        txtGmnName.Text = "";
        txtDesc.Text = "";
        ComboName.DataBind();
        ComboName.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
        ComboName.SelectedIndex = -1;
        cmbNezamChart.SelectedIndex = -1;
        checkboxHaveNmcId.Checked = false;
    }
    protected void Disable()
    {
        //txtDate.Enabled = false;
        cmbNezamChart.ClientEnabled=
        checkboxHaveNmcId.ClientEnabled =
        txtDesc.Enabled = 
        txtGmnName.Enabled =
        ComboName.Enabled = false;


    }
    protected void Enable()
    {
        //txtDate.Enabled = true;
        cmbNezamChart.ClientEnabled=
        checkboxHaveNmcId.ClientEnabled =
       txtDesc.Enabled = 
        txtGmnName.Enabled =
        ComboName.Enabled = true;
    }
    protected void Edit(int GmnId)
    {
        TSP.DataManager.GovManagerNameManager manager = new TSP.DataManager.GovManagerNameManager();
        TSP.DataManager.GovManagerNameManager manager2 = new TSP.DataManager.GovManagerNameManager();

        string DelUrl = "";
        manager.FindByCode(GmnId);
        if (manager.Count == 1)
        {
            try
            {

                if (Convert.ToInt32(manager[0]["GmtId"].ToString()) != Convert.ToInt32(ComboName.Value))
                {
                    manager2.FindByTitle(Convert.ToInt32(ComboName.Value));
                    if (manager2.Count != 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "بیش از این برای این سمت مدیر تعریف شده است";
                        return;
                    }
                }

                manager[0].BeginEdit();
                manager[0]["GmtId"] = ComboName.Value;
                //manager[0]["GmnName"] = txtGmnName.Text;
                if (checkboxHaveNmcId.Checked)
                {
                     manager[0]["NmcId"] = cmbNezamChart.SelectedItem.Value;
                    TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
                    NezamMemberChartManager.FindByNmcId(Convert.ToInt32(cmbNezamChart.SelectedItem.Value));
                    if (NezamMemberChartManager.Count != 0)
                    {
                        manager[0]["GmnName"] = NezamMemberChartManager[0]["FullName"].ToString();
                    }
                    else manager[0]["GmnName"] = cmbNezamChart.SelectedItem.Text;
                }
                else
                {
                     manager[0]["GmnName"] = txtGmnName.Text;
                     manager[0]["NmcId"] = DBNull.Value;
                }
                if (Session["SignUpload"] != null)
                {
                    if (Session["SignUpload"].ToString().Contains("Temp"))
                    {
                        {
                            manager[0]["SignUrl"] = "~/Image/GovManagerSign/" + GmnId.ToString() + Path.GetExtension(Session["SignUpload"].ToString());
                        }
                    }
                }
                else
                {
                    DelUrl = manager[0]["SignUrl"].ToString();
                    manager[0]["SignUrl"] = DBNull.Value;
                }
                manager[0]["Description"] = txtDesc.Text;
                manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                manager[0]["ModifiedDate"] = DateTime.Now;
                manager[0].EndEdit();

                int cn = manager.Save();
                if (cn == 1)
                {

                    ManagerId.Value = Utility.EncryptQS(manager[0]["GmnId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام نشد";
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
        }
        else
        {
        }
        try
        {
            if (Session["SignUpload"] != null)
            {
                string ImgTarget = Server.MapPath("~/Image/GovManagerSign/") + GmnId.ToString() + Path.GetExtension(Session["SignUpload"].ToString());
                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["SignUpload"].ToString());
                if (File.Exists(ImgSoource))
                {
                    File.Delete(ImgTarget);
                    File.Copy(ImgSoource, ImgTarget, true);
                    File.Delete(ImgSoource);
                }
                ImgSign.ImageUrl = "~/Image/GovManagerSign/" + GmnId.ToString() + Path.GetExtension(Session["SignUpload"].ToString());
                Session["SignUpload"] = null;
            }


            if (DelUrl != "")//---delete
            {
                File.Delete(Server.MapPath(DelUrl));
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }
    protected void Insert()
    {
        //DateTime dt = new DateTime();
        //dt = DateTime.Now;
        //System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
        //string PerDate = string.Format("{0},{1},{2}", pDate.GetYear(dt).ToString().PadLeft(4, '0'), pDate.GetMonth(dt).ToString().PadLeft(2, '0'), pDate.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));

        TSP.DataManager.GovManagerNameManager manager = new TSP.DataManager.GovManagerNameManager();
        TSP.DataManager.GovManagerNameManager manager2 = new TSP.DataManager.GovManagerNameManager();
        try
        {
            if (checkboxHaveNmcId.Checked && cmbNezamChart.SelectedItem==null && cmbNezamChart.SelectedItem.Value == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "سمت شخص مورد نظر را انتخاب نمایید";
                return;
            }
            manager2.FindByTitle(Convert.ToInt32(ComboName.Value));
            if (manager2.Count != 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "بیش از این برای این سمت مدیر تعریف شده است";
                return;
            }
            DataRow row = manager.NewRow();
            row["GmtId"] = ComboName.Value;
           
            row["CreateDate"] = Utility.GetDateOfToday();

            row["Description"] = txtDesc.Text;
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            if (checkboxHaveNmcId.Checked)
            {
                row["NmcId"] = cmbNezamChart.SelectedItem.Value;
                TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
                NezamMemberChartManager.FindByNmcId(Convert.ToInt32(cmbNezamChart.SelectedItem.Value));
                if (NezamMemberChartManager.Count != 0)
                {
                    row["GmnName"] = NezamMemberChartManager[0]["FullName"].ToString();
                }
                else row["GmnName"] = cmbNezamChart.SelectedItem.Text;

                cmbNezamChart.ClientVisible = true;
                txtGmnName.ClientVisible = false;
            }
            else
            {
                row["GmnName"] = txtGmnName.Text;
                row["NmcId"] = DBNull.Value;
                cmbNezamChart.ClientVisible = false;
                txtGmnName.ClientVisible = true;
            }
            manager.AddRow(row);
            
            if (manager.Save() == 1)
            {
                manager.DataTable.AcceptChanges();
                ManagerId.Value = Utility.EncryptQS(manager[0]["GmnId"].ToString());

                if (Session["SignUpload"] != null)
                {
                    manager[0]["SignUrl"] = "~/Image/GovManagerSign/" + Utility.DecryptQS(ManagerId.Value) + Path.GetExtension(Session["SignUpload"].ToString());
                }
                manager.Save();

                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";

                btnEdit2.Enabled = false;
                btnEdit.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
            }


            else
            {

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام نشد";
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

        if (Session["SignUpload"] != null)
        {
            try
            {
                string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["SignUpload"].ToString());
                string ImgTarget = Server.MapPath("~/Image/GovManagerSign/") + Utility.DecryptQS(ManagerId.Value) + Path.GetExtension(Session["SignUpload"].ToString());
                File.Copy(ImgSoource, ImgTarget, true);
                File.Delete(ImgSoource);
                ImgSign.ImageUrl = "~/Image/GovManagerSign/" + Utility.DecryptQS(ManagerId.Value) + Path.GetExtension(Session["SignUpload"].ToString());
                Session["SignUpload"] = ImgSign.ImageUrl;
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
            }
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

            } while (File.Exists(MapPath("~/Image/GovManagerSign/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;

            uploadedFile.SaveAs(tempFileName, true);
            Session["SignUpload"] = tempFileName;
        }
        return ret;
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.GovManagerNameManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        ManagerId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
        btnRemoveFile.ClientVisible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        string GmnId = Utility.DecryptQS(ManagerId.Value);

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

                //Response.Redirect("AddCourse.aspx?GmnId=" + Utility.EncryptQS(CrId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));

            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(GmnId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(GmnId));
                }

            }

        }



    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("GovManagerName.aspx?");

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string GmnId = Utility.DecryptQS(ManagerId.Value);

        if (string.IsNullOrEmpty(GmnId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (string.IsNullOrEmpty(pageMode) && pageMode != "View")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else
            {
                Enable();

                TSP.DataManager.Permission per = TSP.DataManager.GovManagerNameManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());


                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                btnRemoveFile.ClientVisible = true;
            }
        }
    }
    protected void btnRemoveFile_Click(object sender, EventArgs e)
    {
        Session["SignUpload"] = null;
        ImgSign.ImageUrl = null;
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
}

