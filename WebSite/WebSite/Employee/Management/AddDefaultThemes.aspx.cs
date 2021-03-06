using System;
using System.Data;
using System.IO;
using DevExpress.Web;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Employee_Management_AddDefaultThemes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!Page.IsPostBack)
        {
            SetKey();
            SetPermission();
            SetMode();
        }

        KeepPageState();
        if (this.ViewState["btnnew"] != null)
            this.btnnew.Enabled = this.btnnew2.Enabled = (bool)this.ViewState["btnnew"];
        if (this.ViewState["btnedit"] != null)
            this.btnedit.Enabled = this.btnedit2.Enabled = (bool)this.ViewState["btnedit"];
        if (this.ViewState["btnsave"] != null)
            this.btnsave.Enabled = this.btnsave2.Enabled = (bool)this.ViewState["btnsave"];
    }

    #region Functions
    void SetKey()
    {
        try
        {
            ID = int.Parse(Utility.DecryptQS(Request.QueryString["id"].ToString()));
            PageMode = Utility.DecryptQS(Request.QueryString["mode"].ToString());
        }
        catch (Exception)
        {
            Response.Redirect("DefaultThemes.aspx");
        }
    }
    void SetPermission()
    {
        TSP.DataManager.Permission per = TSP.DataManager.DefaultThemesManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
            (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnsave.Enabled = per.CanEdit || per.CanNew;
        btnsave2.Enabled = per.CanEdit || per.CanNew;
        btnnew.Enabled = per.CanNew;
        btnedit.Enabled = per.CanEdit;
        btnnew2.Enabled = per.CanNew;
        btnedit2.Enabled = per.CanEdit;
    }
    void SetMode()
    {

        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            if (PageMode == "view")//---view mode
            {
                DisableControls();
                FillControls(ID);
                RoundPanelTheme.HeaderText = "مشاهده";
                PageMode = "view";
                SetButtoms(true, true, false);
            }
            else if (PageMode == "edit") //------edit mode
            {
                PageMode = "edit";
                EnableControls();
                FillControls(ID);
                RoundPanelTheme.HeaderText = "ویرایش";
                SetButtoms(true, false, true);
            }
            else if (PageMode == "insert") //------insert mode
            {
                PageMode = "insert";
                ResetForm();
                EnableControls();
                CBDefaultThemeType.Focus();
                lblfilename.ClientVisible = false;
                RoundPanelTheme.HeaderText = "جدید";
                txtdate.Text = Utility.GetDateOfToday();
                HiddenFieldUploadControl["HasFile"] = "0";
                SetButtoms(true, false, true);
            }
        }

        this.ViewState["btnnew"] = btnnew.Enabled;
        this.ViewState["btnedit"] = btnedit.Enabled;
        this.ViewState["btnsave"] = btnsave.Enabled;
    }
    void KeepPageState()
    {
        if (!Utility.IsDBNullOrNullValue(Session["filename"]))
            lblfilename.NavigateUrl = Session["filename"].ToString();
    }
    public void ResetForm()
    {
        txtdate.Text = string.Empty;
        txtname.Text = string.Empty;
        memodescr.Text = string.Empty;
        CBDefaultThemeType.SelectedIndex = -1;
        lblfilename.ClientVisible = false;
        lblfilename.Text = string.Empty;
        ID = -1;
        PageMode = "insert";
    }
    public void DisableControls()
    {
        //txtdate.Enabled = false;
        //txtname.Enabled = false;
        //memodescr.Enabled = false;
        //CBDefaultThemeType.Enabled = false;
        //lblfilename.Visible = true;
        //CustomAspxUploadControl1.Visible = false;
        RoundPanelTheme.Enabled = false;

    }
    public void EnableControls()
    {
        //txtdate.Enabled = true;
        //txtname.Enabled = true;
        //memodescr.Enabled = true;
        //CBDefaultThemeType.Enabled = true;
        //CustomAspxUploadControl1.Enabled = true;
        //lblfilename.Visible = false;
        //CustomAspxUploadControl1.Visible = true;
        RoundPanelTheme.Enabled = true;
    }
    public void FillControls(int id)
    {
        TSP.DataManager.DefaultThemesManager dtm = new TSP.DataManager.DefaultThemesManager();
        dtm.FindByCode(id);
        if (dtm.Count == 1)
        {
            txtname.Text = dtm[0]["DtName"].ToString();
            txtdate.Text = dtm[0]["Date"].ToString();
            memodescr.Text = dtm[0]["Descripion"].ToString();
            if (!Utility.IsDBNullOrNullValue(dtm[0]["FileUrl"]))
            {
                lblfilename.ClientVisible = true;
                lblfilename.NavigateUrl = dtm[0]["FileUrl"].ToString();
                lblfilename.Text = "مسیر فایل";
                HiddenFieldUploadControl["HasFile"] = "1";
                Session["filename"] = dtm[0]["FileUrl"].ToString();
            }
            else
            {
                lblfilename.ClientVisible = false;
                HiddenFieldUploadControl["HasFile"] = "0";
                Session["filename"] = "";
            }
            if (!Utility.IsDBNullOrNullValue(dtm[0]["TypeId"]))
            {
                CBDefaultThemeType.DataBind();
                CBDefaultThemeType.SelectedIndex = CBDefaultThemeType.Items.FindByValue(dtm[0]["TypeId"].ToString()).Index;
            }
        }
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
            } while (System.IO.File.Exists(MapPath("~/Image/DefaultTheme/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["filename"] = tempFileName;
        }
        return ret;
    }
    public void Insert()
    {
        try
        {
            int typeid = Convert.ToInt32(CBDefaultThemeType.SelectedItem.Value);
            if (!CheckCountByTypeID(typeid))
            {
                ShowMessage("تعریف دو طرح برای یک نوع قالب امکانپذیر نمی باشد");
                return;
            }
            TSP.DataManager.DefaultThemesManager DefaultThemesManager = new TSP.DataManager.DefaultThemesManager();
            DataRow row = DefaultThemesManager.NewRow();
            row["DtName"] = txtname.Text.Trim();
            row["Descripion"] = memodescr.Text.Trim();
            row["Date"] = Utility.GetDateOfToday();
            row["InActive"] = false;
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            row["TypeId"] = CBDefaultThemeType.Value != null ? CBDefaultThemeType.SelectedItem.Value : DBNull.Value;
            if (Session["filename"] != null && String.IsNullOrWhiteSpace(Session["filename"].ToString()) == false)
            {
                row["FileUrl"] = "~/Image/DefaultTheme/" + System.IO.Path.GetFileName(Session["filename"].ToString());
                row["ThemeFile"] = Utility.GetFileBytes(Session["filename"].ToString());
            }

            DefaultThemesManager.AddRow(row);
            if (DefaultThemesManager.Save() > 0)
            {
                ID = Convert.ToInt32(DefaultThemesManager[0]["DtId"].ToString());
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                RoundPanelTheme.HeaderText = "ویرایش";
                SetButtoms(true, false, true);
                PageMode = "edit";
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            String Error = Utility.Messages.GetExceptionError(err);
            if (String.IsNullOrWhiteSpace(Error) == false)
            {
                ShowMessage(Error);
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
            return;
        }

        try
        {
            if (!Utility.IsDBNullOrNullValue(Session["filename"]))
                System.IO.File.Move(Session["filename"].ToString(), MapPath("~/Image/DefaultTheme/") + System.IO.Path.GetFileName(Session["filename"].ToString()));
            lblfilename.NavigateUrl = MapPath("~/Image/DefaultTheme/") + System.IO.Path.GetFileName(Session["filename"].ToString());
        }
        catch (Exception)
        {
        }
        Session["filename"] = null;
    }
    public void Edit(int id)
    {
        try
        {
            TSP.DataManager.DefaultThemesManager dtm = new TSP.DataManager.DefaultThemesManager();
            dtm.FindByCode(id);
            if (dtm.Count == 1)
            {
                //--------------chechk repeat by typeid---
                int typeid = Convert.ToInt32(CBDefaultThemeType.SelectedItem.Value);
                if (Convert.ToInt32(dtm[0]["TypeId"].ToString()) != typeid)
                {
                    if (!CheckCountByTypeID(typeid))
                    {
                        ShowMessage("تعریف دو طرح برای یک نوع قالب امکانپذیر نمی باشد");
                        return;
                    }
                }
                //-------------------------------------------

                dtm[0].BeginEdit();
                dtm[0]["DtName"] = txtname.Text.Trim();
                dtm[0]["Descripion"] = memodescr.Text.Trim();
                //  dtm[0]["Date"] = Utility.GetDateOfToday();
                if (Session["filename"] != null && String.IsNullOrWhiteSpace(Session["filename"].ToString()) == false)
                {
                    dtm[0]["FileUrl"] = "~/Image/DefaultTheme/" + System.IO.Path.GetFileName(Session["filename"].ToString());
                    dtm[0]["ThemeFile"] = Utility.GetFileBytes(Session["filename"].ToString());
                }
                dtm[0]["UserId"] = Utility.GetCurrentUser_UserId();
                dtm[0]["ModifiedDate"] = DateTime.Now;
                dtm[0]["TypeId"] = CBDefaultThemeType.Value != null ? CBDefaultThemeType.SelectedItem.Value : DBNull.Value;
                dtm[0].EndEdit();
                if (dtm.Save() > 0)
                {
                    ID = Convert.ToInt32(dtm[0]["DtId"].ToString());
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                    PageMode = "edit";
                    RoundPanelTheme.HeaderText = "ویرایش";
                    SetButtoms(true, false, true);
                }
                else
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    return;
                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            String Error = Utility.Messages.GetExceptionError(err);
            if (String.IsNullOrWhiteSpace(Error) == false)
            {
                ShowMessage(Error);
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
            return;
        }
        try
        {
            if (!Utility.IsDBNullOrNullValue(Session["filename"]))
                System.IO.File.Move(Session["filename"].ToString(), MapPath("~/Image/DefaultTheme/") + System.IO.Path.GetFileName(Session["filename"].ToString()));
            lblfilename.NavigateUrl = MapPath("~/Image/DefaultTheme/") + System.IO.Path.GetFileName(Session["filename"].ToString());
            Session["filename"] = lblfilename.NavigateUrl;
        }
        catch
        {
        }
        Session["filename"] = null;
    }
    public string PageMode
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldPageMode["PgMode"].ToString());
        }
        set
        {
            HiddenFieldPageMode["PgMode"] = Utility.EncryptQS(value.ToString());
        }
    }
    public int ID
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HiddenFieldPageMode["Id"].ToString()));
        }
        set
        {
            HiddenFieldPageMode["Id"] = Utility.EncryptQS(value.ToString());
        }
    }
    public bool CheckCountByTypeID(int TypeID)
    {
        TSP.DataManager.DefaultThemesManager dtm = new TSP.DataManager.DefaultThemesManager();
        DataTable dt = dtm.FindByTypeID(TypeID);
        if (dt.Rows.Count == 0)
        {
            return true;
        }
        else return false;
    }
    public void SetButtoms(bool newb, bool editb, bool saveb)
    {
        TSP.DataManager.Permission per = TSP.DataManager.DefaultThemesManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
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
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion

    #region Methods
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (PageMode == "insert")
            Insert();
        else if (PageMode == "edit")
            Edit(ID);
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.DefaultThemesManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
            (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanNew)
        {
            PageMode = "insert";
            SetMode();
        }
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.DefaultThemesManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
             (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanEdit)
        {
            PageMode = "edit";
            SetMode();
        }
    }
    protected void CustomAspxUploadControl1_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

}
