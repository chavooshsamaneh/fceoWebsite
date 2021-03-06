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
using System.IO;
using DevExpress.Web;

public partial class Employee_ExGroup_ExGroupPeriodInsert : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["PeriodAttach"] = null;
            SetKeys();
            SetPermission();
            SetMode();
        }

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
        TSP.DataManager.Permission per = TSP.DataManager.ExGroupPeriodManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
           (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanNew)
        {
            PageMode = "insert";
            SetMode();
        }

    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        if (PageMode != "view")
        {
            ShowMessage("پرونده ای انتخاب نشده است");
            return;
        }
        if (Utility.IsDBNullOrNullValue(ID))
        {
            Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        TSP.DataManager.ExGroupPeriodManager egpManager = new TSP.DataManager.ExGroupPeriodManager();
        egpManager.FindByCode(ID);
        if (egpManager.Count == 1)
        {
            if (Convert.ToBoolean(egpManager[0]["InActive"]))
            {
                ShowMessage("امکان ویرایش رکورد غیرفعال وجود ندارد");
                return;
            }
        }
        TSP.DataManager.Permission per = TSP.DataManager.ExGroupPeriodManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
           (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanEdit)
        {
            PageMode = "edit";
            SetMode();
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (PageMode == "insert")
            Insert();
        else if (PageMode == "edit")
        {
            if (Utility.IsDBNullOrNullValue(ID))
            {
                Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            Edit(ID);
        }
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Period":
                Response.Redirect("~/Employee/ExGroup/ExGroupPeriodInsert.aspx?id=" + Utility.EncryptQS(ID.ToString()) + "&mode=" + Utility.EncryptQS("edit"));
                break;
            case "Candid":
                Response.Redirect("~/Employee/ExGroup/Candidate.aspx?ExGroupPeriodId=" + Utility.EncryptQS(ID.ToString()));
                break;
        }
    }

    protected void flpcAttachment_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveAttachment(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
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

    #region FormMethods
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
                Response.Redirect("ExGroupPeriod.aspx");
            }
        }
        catch
        {
            Response.Redirect("ExGroupPeriod.aspx");
        }
    }
    void SetMode()
    {
        switch (PageMode)
        {
            case "view":
                DisableControls(true);
                FillControls(ID);
                PageMode = "view";
                RoundPanelMain.HeaderText = "مشاهده";
                SetButtoms(true, true, false);
                break;
            case "edit":
                PageMode = "edit";
                EnableControls(true);
                FillControls(ID);
                RoundPanelMain.HeaderText = "ویرایش";
                SetButtoms(true, false, true);
                break;
            case "insert":
                PageMode = "insert";
                EnableControls(false);
                ResetForm();
                cmbExGroup.Focus();
                RoundPanelMain.HeaderText = "جدید";
                SetButtoms(true, false, true);
                break;
        }
        this.ViewState["btnnew"] = btnnew.Enabled;
        this.ViewState["btnedit"] = btnedit.Enabled;
        this.ViewState["btnsave"] = btnsave.Enabled;
    }
    void SetPermission()
    {
        TSP.DataManager.Permission per = TSP.DataManager.ExGroupPeriodManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
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
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        MemoDescription.Text = string.Empty;
        txtName.Text = string.Empty;
        txtStartPropagation.Text = string.Empty;
        txtEndPropagation.Text = string.Empty;
        cmbExGroup.SelectedIndex = -1;
        ID = -1;
        RoundPanelMain.HeaderText = "";
        PageMode = "insert";
    }
    void DisableControls(bool menu)
    {
        RoundPanelMain.Enabled = false;
        ASPxMenu1.Enabled = menu;
    }
    void EnableControls(bool menu)
    {
        RoundPanelMain.Enabled = true;
        ASPxMenu1.Enabled = menu;
    }
    void FillControls(int id)
    {
        TSP.DataManager.ExGroupPeriodManager ExGroupPeriodManager = new TSP.DataManager.ExGroupPeriodManager();

        //---------------fill main data
        ExGroupPeriodManager.FindByCode(id);
        if (ExGroupPeriodManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(ExGroupPeriodManager[0]["ExGroupId"]))
            {
                cmbExGroup.DataBind();
                cmbExGroup.SelectedIndex = cmbExGroup.Items.FindByValue(ExGroupPeriodManager[0]["ExGroupId"].ToString()).Index;
            }
            txtStartDate.Text = ExGroupPeriodManager[0]["StartDate"].ToString();
            txtEndDate.Text = ExGroupPeriodManager[0]["EndDate"].ToString();
            txtEndPropagation.Text = ExGroupPeriodManager[0]["EndDatePropagation"].ToString();
            txtStartPropagation.Text = ExGroupPeriodManager[0]["StartDatePropagation"].ToString();
            MemoDescription.Text = ExGroupPeriodManager[0]["Description"].ToString();
            txtName.Text = ExGroupPeriodManager[0]["PeriodName"].ToString();
            AttachmentImage.ImageUrl = ExGroupPeriodManager[0]["Attachment"].ToString();
            chkIsGrouping.Checked = Convert.ToBoolean(ExGroupPeriodManager[0]["IsGrouping"]);
        }
    }
    void SetButtoms(bool newb, bool editb, bool saveb)
    {
        TSP.DataManager.Permission per = TSP.DataManager.ExGroupPeriodManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
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

    protected string SaveAttachment(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/Association/Periods/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["PeriodAttach"] = tempFileName;
        }
        return ret;
    }
    #endregion

    #region DataMethods
    public void Insert()
    {
        TSP.DataManager.ExGroupPeriodManager egpManager = new TSP.DataManager.ExGroupPeriodManager();
        try
        {
            DataRow row = egpManager.NewRow();
            if (txtStartDate.Text != string.Empty)
                row["StartDate"] = txtStartDate.Text.Trim();
            if (txtEndDate.Text != string.Empty)
                row["EndDate"] = txtEndDate.Text.Trim();

            if (txtStartPropagation.Text != string.Empty)
                row["StartDatePropagation"] = txtStartPropagation.Text.Trim();
            if (txtEndPropagation.Text != string.Empty)
                row["EndDatePropagation"] = txtEndPropagation.Text.Trim();

            row["Description"] = MemoDescription.Text.Trim();
            row["PeriodName"] = txtName.Text.Trim();
            row["ExGroupId"] = cmbExGroup.Value != null ? cmbExGroup.SelectedItem.Value : DBNull.Value;
            row["InActive"] = 0;
            if (chkIsGrouping.Checked)
            {
                row["IsGrouping"] = 1;
            }
            else row["IsGrouping"] = 0;
            if (Session["PeriodAttach"] != null)
            {
                try
                {
                    string FileName = System.IO.Path.GetFileName(Session["PeriodAttach"].ToString());
                    row["Attachment"] = "~/Image/Association/Periods/" + FileName;
                    string ImgSoource = Server.MapPath("~/image/Temp/") + FileName;
                    string ImgTarget = Server.MapPath("~/Image/Association/Periods/") + FileName;
                    File.Move(ImgSoource, ImgTarget);
                    AttachmentImage.ImageUrl = "~/Image/Association/Periods/" + FileName;
                    Session["PeriodAttach"] = null;
                }
                catch (Exception ex)
                {
                    Utility.SaveWebsiteError(ex);
                    string Message = "خطایی در ذخیره فایل انجام گرفته است";
                    if (Utility.ShowExceptionError())
                    {
                        Message = Message + ex.Message;
                    }
                    ShowMessage(Message);
                    return;
                }
            }
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            egpManager.AddRow(row);
            if (egpManager.Save() > 0)
            {
                ID = Convert.ToInt32(egpManager[0]["ExGroupPeriodId"].ToString());
                egpManager.DataTable.AcceptChanges();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                PageMode = "edit";
                RoundPanelMain.HeaderText = "ویرایش";
                SetButtoms(true, false, true);
                EnableControls(true);
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }
    public void Edit(int id)
    {
        TSP.DataManager.ExGroupPeriodManager egpManager = new TSP.DataManager.ExGroupPeriodManager();
        try
        {
            string PreAttachment = "";
            egpManager.FindByCode(id);
            if (egpManager.Count == 1)
            {
                if (Convert.ToBoolean(egpManager[0]["InActive"]))
                {
                    ShowMessage("امکان ویرایش رکورد غیرفعال وجود ندارد");
                    return;
                }
                egpManager[0].BeginEdit();
                if (txtStartDate.Text != string.Empty)
                    egpManager[0]["StartDate"] = txtStartDate.Text.Trim();
                if (txtEndDate.Text != string.Empty)
                    egpManager[0]["EndDate"] = txtEndDate.Text.Trim();

                if (txtStartPropagation.Text != string.Empty)
                    egpManager[0]["StartDatePropagation"] = txtStartPropagation.Text.Trim();
                if (txtEndPropagation.Text != string.Empty)
                    egpManager[0]["EndDatePropagation"] = txtEndPropagation.Text.Trim();

                egpManager[0]["Description"] = MemoDescription.Text.Trim();
                egpManager[0]["PeriodName"] = txtName.Text.Trim();
                egpManager[0]["ExGroupId"] = cmbExGroup.Value != null ? cmbExGroup.SelectedItem.Value : DBNull.Value;
                if (chkIsGrouping.Checked)
                {
                    egpManager[0]["IsGrouping"] = 1;
                }
                else egpManager[0]["IsGrouping"] = 0;
                if (Session["PeriodAttach"] != null)
                {
                    try
                    {
                        if (!Utility.IsDBNullOrNullValue(egpManager[0]["Attachment"]))
                        {
                            PreAttachment = egpManager[0]["Attachment"].ToString();
                        }
                        string FileName = System.IO.Path.GetFileName(Session["PeriodAttach"].ToString());
                        egpManager[0]["Attachment"] = "~/Image/Association/Periods/" + FileName;
                        string ImgSoource = Server.MapPath("~/image/Temp/") + FileName;
                        string ImgTarget = Server.MapPath("~/Image/Association/Periods/") + FileName;
                        File.Move(ImgSoource, ImgTarget);
                        AttachmentImage.ImageUrl = "~/Image/Association/Periods/" + FileName; 
                        Session["PeriodAttach"] = null;
                    }
                    catch (Exception ex)
                    {
                        Utility.SaveWebsiteError(ex);
                        string Message = "خطایی در ذخیره فایل انجام گرفته است";
                        if (Utility.ShowExceptionError())
                        {
                            Message = Message + ex.Message;
                        }
                        ShowMessage(Message);
                        return;
                    }
                }
                egpManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                egpManager[0]["ModifiedDate"] = DateTime.Now;
                egpManager[0].EndEdit();
                if (egpManager.Save() > 0)
                {
                    egpManager.DataTable.AcceptChanges();
                    PageMode = "edit";
                    RoundPanelMain.HeaderText = "ویرایش";
                    SetButtoms(true, false, true);
                    EnableControls(true);
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(PreAttachment))
                        {
                            File.Delete(MapPath(PreAttachment));
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("خطایی در حذف فایل قبلی ایجاد شده است");
                        Utility.SaveWebsiteError(ex);
                    }
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
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
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
            }
            else if (se.Number == 2627)
            {
                ShowMessage("کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.RelatedDataIsNotValid));
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
        }
        else
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }
    #endregion
    protected void ASPxMenu2_ItemClick(object source, MenuItemEventArgs e)
    {

    }
}
