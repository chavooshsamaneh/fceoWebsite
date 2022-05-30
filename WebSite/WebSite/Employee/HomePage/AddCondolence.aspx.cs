using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class Employee_HomePage_AddCondolence : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetKeys();
            SetPermission();
            SetMode();
        }

        KeepPageState();
        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (this.ViewState["btnnew"] != null)
            this.btnnew.Enabled = this.btnnew2.Enabled = (bool)this.ViewState["btnnew"];
        if (this.ViewState["btnedit"] != null)
            this.btnedit.Enabled = this.btnedit2.Enabled = (bool)this.ViewState["btnedit"];
        if (this.ViewState["btnsave"] != null)
            this.btnsave.Enabled = this.btnsave2.Enabled = (bool)this.ViewState["btnsave"];
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.CondolenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
           (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanNew)
        {
            PageMode = "insert";
            SetMode();
        }
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.CondolenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
           (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanEdit)
        {
            //-------------check inactive----
            TSP.DataManager.CondolenceManager CondolenceManager = new TSP.DataManager.CondolenceManager();
            CondolenceManager.FindByCode(ID);
            if (CondolenceManager.Count == 1)
            {
                if (Convert.ToBoolean(CondolenceManager[0]["InActive"]))
                {
                    ShowMessage("رکورد مورد نظر غیر فعال بوده و قابل ویرایش نمی باشد");
                    return;
                }
            }
            //-----------------------------------
            PageMode = "edit";
            SetMode();
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (PageMode == "insert")
            Insert();
        else if (PageMode == "edit")
            Edit(ID);
    }
    protected void FileUploadDocument_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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
    #endregion

    #region FormFunctions
    void SetKeys()
    {
        try
        {
            if (Request.QueryString.Count != 0)
            {
                ID = int.Parse(Utility.DecryptQS(Request.QueryString["id"].ToString()));
                PageMode = Utility.DecryptQS(Request.QueryString["mode"].ToString());
            }
            else
            {
                Response.Redirect("Condolence.aspx");
            }
        }
        catch
        {
            Response.Redirect("Condolence.aspx");
        }
    }
    void SetMode()
    {
        switch (PageMode)
        {
            case "view":
                DisableControls();
                FillControls(ID);
                PageMode = "view";
                RoundPanelMain.HeaderText = "مشاهده";
                SetButtoms(true, true, false);
                break;
            case "edit":
                PageMode = "edit";
                EnableControls();
                FillControls(ID);
                RoundPanelMain.HeaderText = "ویرایش";
                SetButtoms(true, false, true);
                break;
            case "insert":
                PageMode = "insert";
                EnableControls();
                ResetForm();
                RoundPanelMain.HeaderText = "جدید";
                SetButtoms(true, false, true);
                txtStartDate.Text = Utility.GetDateOfToday();
                imgCondolence.ClientVisible = false;
                break;
        }
        this.ViewState["btnnew"] = btnnew.Enabled;
        this.ViewState["btnedit"] = btnedit.Enabled;
        this.ViewState["btnsave"] = btnsave.Enabled;
    }
    void SetPermission()
    {
        TSP.DataManager.Permission per = TSP.DataManager.CondolenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
            (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnsave.Enabled = per.CanEdit || per.CanNew;
        btnsave2.Enabled = per.CanEdit || per.CanNew;
        btnnew.Enabled = per.CanNew;
        btnedit.Enabled = per.CanEdit;
        btnnew2.Enabled = per.CanNew;
        btnedit2.Enabled = per.CanEdit;
    }
    void ResetForm()
    {
        MemoDescription.Text = string.Empty;
        MemoFrom.Text = string.Empty;
        MemoSummery.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        imgCondolence.ClientVisible = false;
        imgCondolence.ImageUrl = "";
        ID = -1;
        RoundPanelMain.HeaderText = "";
        PageMode = "insert";
    }
    void DisableControls()
    {
        RoundPanelMain.Enabled = false;
    }
    void EnableControls()
    {
        RoundPanelMain.Enabled = true;
    }
    void FillControls(int id)
    {
        TSP.DataManager.CondolenceManager CondolenceManager = new TSP.DataManager.CondolenceManager();
        //---------------fill main data
        CondolenceManager.FindByCode(id);
        if (CondolenceManager.Count == 1)
        {
            txtStartDate.Text = CondolenceManager[0]["StartDate"].ToString();
            txtEndDate.Text = CondolenceManager[0]["EndDate"].ToString();
            MemoSummery.Text = CondolenceManager[0]["Summary"].ToString();
            MemoFrom.Text = CondolenceManager[0]["CdlFrom"].ToString();
            MemoDescription.Text = CondolenceManager[0]["Description"].ToString();
            if (!Utility.IsDBNullOrNullValue(CondolenceManager[0]["Type"]))
                cmbType.SelectedIndex = cmbType.Items.FindByValue(CondolenceManager[0]["Type"].ToString()).Index;
            if (!Utility.IsDBNullOrNullValue(CondolenceManager[0]["CdlImage"]))
            {
                imgCondolence.ClientVisible = true;
                imgCondolence.ImageUrl = CondolenceManager[0]["CdlImage"].ToString();
                Session["filename"] = CondolenceManager[0]["CdlImage"].ToString();
            }
            else
            {
                imgCondolence.ClientVisible = false;
                Session["filename"] = "";
            }
        }
    }
    void SetButtoms(bool newb, bool editb, bool saveb)
    {
        TSP.DataManager.Permission per = TSP.DataManager.CondolenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
          (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if ((!per.CanNew) && (newb))
            newb = false;
        if ((!per.CanEdit) && (editb))
            editb = false;

        btnnew.Enabled = newb;
        btnnew2.Enabled = newb;
        btnedit.Enabled = editb;
        btnedit2.Enabled = editb;
        btnsave.Enabled = saveb;
        btnsave2.Enabled = saveb;
    }
    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("Style", "display:visible");
        this.LabelWarning.Text = Message;
    }
    public string SavePostedFile(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new System.IO.FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/HomePage/Condolence/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["filename"] = tempFileName;
        }
        return ret;
    }
    void KeepPageState()
    {
        if (!Utility.IsDBNullOrNullValue(Session["filename"]))
            imgCondolence.ImageUrl = Session["filename"].ToString();
    }
    #endregion

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
    public int ID
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HiddenFieldModeID["Id"].ToString()));
        }
        set
        {
            HiddenFieldModeID["Id"] = Utility.EncryptQS(value.ToString());
        }
    }
    #endregion

    #region DataFunctions
    public void Insert()
    {
        TSP.DataManager.CondolenceManager CondolenceManager = new TSP.DataManager.CondolenceManager();
        try
        {
            DataRow row = CondolenceManager.NewRow();
            row["InActive"] = 0;
            if (txtStartDate.Text != string.Empty)
                row["StartDate"] = txtStartDate.Text.Trim();
            if (txtEndDate.Text != string.Empty)
                row["Enddate"] = txtEndDate.Text.Trim();
            row["Description"] = MemoDescription.Text.Trim();
            row["CdlFrom"] = MemoFrom.Text.Trim();
            row["Summary"] = MemoSummery.Text.Trim();
            row["Type"] = cmbType.Value != null ? cmbType.SelectedItem.Value : DBNull.Value;
            if (!Utility.IsDBNullOrNullValue(Session["filename"]))
                row["CdlImage"] = "~/Image/HomePage/Condolence/" + System.IO.Path.GetFileName(Session["filename"].ToString());
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            CondolenceManager.AddRow(row);
            if (CondolenceManager.Save() > 0)
            {
                ID = Convert.ToInt32(CondolenceManager[0]["CdlId"].ToString());
                CondolenceManager.DataTable.AcceptChanges();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                PageMode = "edit";
                RoundPanelMain.HeaderText = "ویرایش";
                SetButtoms(true, false, true);
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
        try
        {
            if (!Utility.IsDBNullOrNullValue(Session["filename"]))
                System.IO.File.Move(Session["filename"].ToString(), MapPath("~/Image/HomePage/Condolence/") + System.IO.Path.GetFileName(Session["filename"].ToString()));
            imgCondolence.ImageUrl = MapPath("~/Image/HomePage/Condolence/") + System.IO.Path.GetFileName(Session["filename"].ToString());
            Session["filename"] = imgCondolence.ImageUrl;
        }
        catch
        {
        }
    }
    public void Edit(int id)
    {
        TSP.DataManager.CondolenceManager CondolenceManager = new TSP.DataManager.CondolenceManager();
        try
        {
            CondolenceManager.FindByCode(id);
            if (CondolenceManager.Count == 1)
            {
                //-----------------chec inactive--------------
                if (Convert.ToBoolean(CondolenceManager[0]["InActive"]))
                {
                    ShowMessage("رکورد مورد نظر غیر فعال بوده و قابل ویرایش نمی باشد");
                    return;
                }
                //---------------------------------------------
                CondolenceManager[0].BeginEdit();
                if (txtStartDate.Text != string.Empty)
                    CondolenceManager[0]["StartDate"] = txtStartDate.Text.Trim();
                if (txtEndDate.Text != string.Empty)
                    CondolenceManager[0]["Enddate"] = txtEndDate.Text.Trim();
                CondolenceManager[0]["Description"] = MemoDescription.Text.Trim();
                CondolenceManager[0]["CdlFrom"] = MemoFrom.Text;
                CondolenceManager[0]["Summary"] = MemoSummery.Text;
                CondolenceManager[0]["Type"] = cmbType.Value != null ? cmbType.SelectedItem.Value : DBNull.Value;
                if (!Utility.IsDBNullOrNullValue(Session["filename"]))
                    CondolenceManager[0]["CdlImage"] = "~/Image/HomePage/Condolence/" + System.IO.Path.GetFileName(Session["filename"].ToString());
                CondolenceManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                CondolenceManager[0]["ModifiedDate"] = DateTime.Now;
                CondolenceManager[0].EndEdit();
                if (CondolenceManager.Save() > 0)
                {
                    CondolenceManager.DataTable.AcceptChanges();
                    PageMode = "edit";
                    RoundPanelMain.HeaderText = "ویرایش";
                    SetButtoms(true, false, true);
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                }
            }
        }
        catch (Exception err)
        {
            SetError(err);
            return;
        }
        try
        {
            if (!Utility.IsDBNullOrNullValue(Session["filename"]))
                System.IO.File.Move(Session["filename"].ToString(), MapPath("~/Image/HomePage/Condolence/") + System.IO.Path.GetFileName(Session["filename"].ToString()));
            Session["filename"] = MapPath("~/Image/HomePage/Condolence/") + System.IO.Path.GetFileName(Session["filename"].ToString());
            imgCondolence.ImageUrl = Session["filename"].ToString();
        }
        catch
        {
        }
    }
    private void SetError(Exception err)
    {
        Utility.SaveWebsiteError(err);
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                ShowMessage("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                ShowMessage("شماره پرونده تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                ShowMessage("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است");
        }
    }
    #endregion
}