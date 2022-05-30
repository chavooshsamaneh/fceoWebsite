using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.IO;
using System.Data;

public partial class Employee_Poll_PollInsert : System.Web.UI.Page
{
    #region Properties
    string PageMode
    {
        set
        {
            HiddenFieldPoll["PageMode"] = value;
        }
        get
        {
            return HiddenFieldPoll["PageMode"].ToString();
        }
    }
    int PollId
    {
        set
        {
            HiddenFieldPoll["PollId"] = value;
        }
        get
        {
            return Convert.ToInt32(HiddenFieldPoll["PollId"]);
        }
    }


    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {

        SetWarningLableWarning();

        if (!IsPostBack)
        {
            Session["PollImage"] = null;
            Session["FileAttach"] = null;
            TSP.DataManager.Permission Per = TSP.DataManager.PollPollManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission PerAnswer = TSP.DataManager.PollAnswerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnNew.Enabled = btnNew2.Enabled = Per.CanNew;
            btnEdit2.Enabled = btnEdit.Enabled = Per.CanEdit;
            btnSave.Enabled = btnSave2.Enabled = Per.CanNew || Per.CanEdit;

            SetKey();
            SetMode();

            this.ViewState["btnNew"] = btnNew.Enabled;
            this.ViewState["btnEdit"] = btnEdit.Enabled;
            this.ViewState["btnSave"] = btnSave.Enabled;
        }

        if (this.ViewState["btnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["btnNew"];
        if (this.ViewState["btnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["btnSave"];
        if (this.ViewState["btnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnEdit"];
        BindAgent();
        //SetImageAndFile();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        PageMode = "New";
        SetMode();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PageMode = "Edit";
        SetMode();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!CheckExistQuestion())
        {
            return;

        }
        if (chkDisplayLocations.SelectedItem == null)
        {
            SetMessage("حداقل یک محل نمایش برای نظر سنجی مشخص نمایید");
            return;
        }
        if (PageMode == "New")
            Insert();
        else if (PageMode == "Edit")
        {
            Update(PollId);
        }
        BindAgent();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("poll.aspx");
    }

    protected void flpImage_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

    protected void btnRemoveFile_Click(object sender, EventArgs e)
    {
        Session["NewsAttach"] = null;
        HyperLinkAttachment.NavigateUrl = "";
    }

    protected void MenuDetail_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Questions":
                Response.Redirect("PollQuestion.aspx?PlId=" + Utility.EncryptQS(PollId.ToString()) + "&PrePgMd=" + Utility.EncryptQS(PageMode));
                break;

        }

    }
    #endregion

    #region Method

    private void BindAgent()
    {

        ((ASPxListBox)(cmbAgent.FindControl("ListBoxAgent"))).DataBind();
        ((ASPxListBox)(cmbAgent.FindControl("ListBoxAgent"))).Items.Insert(0, new ListEditItem("<همه>", null));

        if (((ASPxListBox)(cmbAgent.FindControl("ListBoxAgent"))).SelectedItems.Count
            == ((ASPxListBox)(cmbAgent.FindControl("ListBoxAgent"))).Items.Count - 1)
        {
            ((ASPxListBox)(cmbAgent.FindControl("ListBoxAgent"))).Items[0].Selected = true;
            cmbAgent.Text = "<همه>";
        }
        if (!IsPostBack && PageMode == "New")
        {
            ((ASPxListBox)(cmbAgent.FindControl("ListBoxAgent"))).SelectAll();
            cmbAgent.Text = "<همه>";
        }
    }

    private void SetWarningLableWarning()
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetKey()
    {
        if (Request.QueryString["PgMd"] == null || Request.QueryString["PlId"] == null)
        {
            Response.Redirect("Poll.aspx");
            return;
        }
        PageMode = Utility.DecryptQS(Request.QueryString["PgMd"].ToString());
        PollId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PlId"].ToString()));
    }

    private void SetMode()
    {
        switch (PageMode)
        {
            case "New":
                SetNewMode();
                break;
            case "Edit":
                SetEditMode();
                break;
            case "View":
                SetViweMode();
                break;
        }
    }

    private void SetNewMode()
    {
        RoundPanelMain.HeaderText = "جدید";
        SetControlEnable(true);
        //Set Btn Enable
        btnSave.Enabled = btnSave2.Enabled = true;
        btnNew.Enabled = btnNew2.Enabled = true;
        btnEdit.Enabled = btnEdit2.Enabled = false;
        MenuDetail.Enabled = false;
        this.ViewState["btnSave"] = btnSave.Enabled;
        this.ViewState["btnNew"] = btnNew.Enabled;
        this.ViewState["btnEdit"] = btnEdit.Enabled;

        //Clear Form
        ClearForm();
    }

    private void SetEditMode()
    {
        //Set RoundPanel
        RoundPanelMain.HeaderText = "ویرایش";
        //FillForm
        FillForm();
        //Set Btn Enable
        btnSave.Enabled = btnSave2.Enabled = true;
        btnNew.Enabled = btnNew2.Enabled = true;
        btnEdit.Enabled = btnEdit2.Enabled = false;
        MenuDetail.Enabled = true;
        this.ViewState["btnSave"] = btnSave.Enabled;
        this.ViewState["btnNew"] = btnNew.Enabled;
        this.ViewState["btnEdit"] = btnEdit.Enabled;
        PageMode = "Edit";
        //Clear Form
        //Set ControlEnable
        SetControlEnable(true);
    }

    private void SetViweMode()
    {
        //Set RoundPanel
        RoundPanelMain.HeaderText = "مشاهده";
        //FillForm
        FillForm();
        //Set Btn Enable
        btnSave.Enabled = btnSave2.Enabled = false;
        btnNew.Enabled = btnNew2.Enabled = true;
        btnEdit.Enabled = btnEdit2.Enabled = true;
        this.ViewState["btnSave"] = btnSave.Enabled;
        this.ViewState["btnNew"] = btnNew.Enabled;
        this.ViewState["btnEdit"] = btnEdit.Enabled;
        //Clear Form
        //Set ControlEnable
        SetControlEnable(false);
    }

    private void ClearForm()
    {
        txtTittle.Text = "";
        txtStartDate.Text = "";
        txtValidatDate.Text = "";
        txtDescription.Text = "";
        cmbIsResultPublic.SelectedIndex = 0;
        cmbType.SelectedIndex = 0;
        ((ASPxListBox)(cmbAgent.FindControl("ListBoxAgent"))).DataBind();
        ((ASPxListBox)(cmbAgent.FindControl("ListBoxAgent"))).Items.Insert(0, new ListEditItem("<همه>", null));
        ((ASPxListBox)(cmbAgent.FindControl("ListBoxAgent"))).SelectAll();
        ((ASPxListBox)(cmbAgent.FindControl("ListBoxAgent"))).Items[0].Selected = true;
        cmbAgent.Text = "<همه>";
        //Upload image clear
        chkDisplayLocations.UnselectAll();
        Session["PollImage"] = null;
        AttachmentImage.ImageUrl = "~/Images/noImage.gif";
        Session["FileAttach"] = null;
        HyperLinkAttachment.NavigateUrl = "";
    }

    private void SetControlEnable(Boolean Enabled)
    {
        txtTittle.Enabled = Enabled;
        txtTittle.Enabled = Enabled;
        txtStartDate.Enabled = Enabled;
        txtValidatDate.Enabled = Enabled;
        txtDescription.Enabled = Enabled;
        cmbIsResultPublic.Enabled = Enabled;
        cmbType.Enabled = Enabled;
        flpImage.Enabled = Enabled;
        chkDisplayLocations.Enabled = Enabled;
        cmbAgent.ReadOnly = !Enabled;

    }

    private void FillForm()
    {
        TSP.DataManager.PollPollManager PollPollManager = new TSP.DataManager.PollPollManager();
        PollPollManager.FindByCode(PollId);
        if (PollPollManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        if (!Utility.IsDBNullOrNullValue(PollPollManager[0]["Tittle"]))
            txtTittle.Text = PollPollManager[0]["Tittle"].ToString();

        if (!Utility.IsDBNullOrNullValue(PollPollManager[0]["IsResultPublic"]))
            cmbIsResultPublic.SelectedIndex = cmbIsResultPublic.Items.FindByValue(PollPollManager[0]["IsResultPublic"].ToString()).Index;

        if (!Utility.IsDBNullOrNullValue(PollPollManager[0]["StartShowDate"]))
            txtStartDate.Text = PollPollManager[0]["StartShowDate"].ToString();

        if (!Utility.IsDBNullOrNullValue(PollPollManager[0]["ValidDate"]))
            txtValidatDate.Text = PollPollManager[0]["ValidDate"].ToString();

        if (!Utility.IsDBNullOrNullValue(PollPollManager[0]["QuestionCountType"]))
            cmbType.SelectedIndex = cmbType.Items.FindByValue(PollPollManager[0]["QuestionCountType"].ToString()).Index;

        if (!Utility.IsDBNullOrNullValue(PollPollManager[0]["Description"]))
            txtDescription.Text = PollPollManager[0]["Description"].ToString();

        if (!Utility.IsDBNullOrNullValue(PollPollManager[0]["ImageURL"]))
        {
            // Session["PollImage"] =
            AttachmentImage.ImageUrl = PollPollManager[0]["ImageURL"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(PollPollManager[0]["FileURL"]))
        {
            //Session["FileAttach"] =
            HyperLinkAttachment.NavigateUrl = PollPollManager[0]["FileURL"].ToString();
        }


        #region Fill DisplayLocation


        chkDisplayLocations.DataBind();
        TSP.DataManager.PollDisplayLocationsManager PollDisplayLocationsManager = new TSP.DataManager.PollDisplayLocationsManager();
        PollDisplayLocationsManager.FindByPollId(PollId);
        for (int i = 0; i < PollDisplayLocationsManager.Count; i++)
            chkDisplayLocations.Items.FindByValue(PollDisplayLocationsManager[i]["TypeId"].ToString()).Selected = true;

        #endregion

        #region Agent
        ((ASPxListBox)(cmbAgent.FindControl("ListBoxAgent"))).DataBind();
        string AgentListName = "";
        TSP.DataManager.pollAgentPoll pollAgentPoll = new TSP.DataManager.pollAgentPoll();
        pollAgentPoll.FindByPollId(PollId);
        for (int i = 0; i < pollAgentPoll.Count; i++)
        {
            ((ASPxListBox)(cmbAgent.FindControl("ListBoxAgent"))).Items.FindByValue(pollAgentPoll[i]["AgentId"].ToString()).Selected = true;
            if (AgentListName != "")
                AgentListName += ";";
            AgentListName += ((ASPxListBox)(cmbAgent.FindControl("ListBoxAgent"))).Items.FindByValue(pollAgentPoll[i]["AgentId"].ToString()).Text;
        }
        cmbAgent.Text = AgentListName;
        #endregion
    }

    #region Save Images and Attachments
    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        string ret2 = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
                ret2 = Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/Poll/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["PollImage"] = tempFileName;
        }
        return ret;
    }
    #endregion

    #region File Attachment

    protected string SaveAttachment(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/Poll/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["FileAttach"] = tempFileName;
        }
        return ret;
    }

    #endregion

    #region Insert-Update
    private void Insert()
    {
        TSP.DataManager.PollPollManager PollPollManager = new TSP.DataManager.PollPollManager();
        TSP.DataManager.PollDisplayLocationsManager PollDisplayLocationsManager = new TSP.DataManager.PollDisplayLocationsManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.pollAgentPoll pollAgentPoll = new TSP.DataManager.pollAgentPoll();
        TransactionManager.Add(PollDisplayLocationsManager);
        TransactionManager.Add(PollPollManager);
        TransactionManager.Add(pollAgentPoll);
        try
        {
            TransactionManager.BeginSave();

            DataRow dr = PollPollManager.NewRow();
            if (txtTittle.Text != null)
                dr["Tittle"] = txtTittle.Text;
            if (cmbIsResultPublic.SelectedItem != null)
                dr["IsResultPublic"] = cmbIsResultPublic.SelectedItem.Value;
            dr["CreateDate"] = Utility.GetDateOfToday();
            if (cmbType.SelectedItem != null)
                dr["QuestionCountType"] = cmbType.SelectedItem.Value;
            if (txtStartDate.Text != null)
                dr["StartShowDate"] = txtStartDate.Text;
            if (txtValidatDate.Text != null)
                dr["ValidDate"] = txtValidatDate.Text;
            if (txtDescription.Text != null)
                dr["Description"] = txtDescription.Text;
            #region SaveImage
            if (Session["PollImage"] != null)
            {
                try
                {
                    string FileName = System.IO.Path.GetFileName(Session["PollImage"].ToString());
                    dr["ImageURL"] = "~/Image/Poll/" + FileName;
                    //  if (!System.IO.File.Exists("~/Image/Poll/" + FileName))
                    //  {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + FileName;
                    string ImgTarget = Server.MapPath("~/Image/Poll/") + FileName;
                    File.Move(ImgSoource, ImgTarget);
                    // }
                    // Session["PollImage"] =
                    AttachmentImage.ImageUrl = "~/Image/Poll/" + FileName;
                    Session["PollImage"] = null;
                }
                catch (Exception ex)
                {
                    TransactionManager.CancelSave();
                    Utility.SaveWebsiteError(ex);
                    string Message = "خطایی در ذخیره تصویر انجام گرفته است";
                    if (Utility.ShowExceptionError())
                    {
                        Message = Message + ex.Message;
                    }
                    SetMessage(Message);
                    return;
                }
            }
            #endregion

            #region SaveFile
            if (Session["FileAttach"] != null)
            {
                try
                {
                    string FileName = System.IO.Path.GetFileName(Session["FileAttach"].ToString());
                    dr["FileURL"] = "~/Image/Poll/" + FileName;
                    //  if (!System.IO.File.Exists("~/Image/Poll/" + FileName))
                    // {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + FileName;
                    string ImgTarget = Server.MapPath("~/Image/Poll/") + FileName;
                    File.Move(ImgSoource, ImgTarget);
                    //   }
                    //    Session["FileAttach"] =
                    HyperLinkAttachment.NavigateUrl = "~/Image/Poll/" + FileName;
                    Session["FileAttach"] = null;
                }
                catch (Exception ex)
                {
                    TransactionManager.CancelSave();
                    Utility.SaveWebsiteError(ex);
                    string Message = "خطایی در ذخیره فایل انجام گرفته است";
                    if (Utility.ShowExceptionError())
                    {
                        Message = Message + ex.Message;
                    }
                    SetMessage(Message);
                    return;
                }
            }
            #endregion

            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;

            PollPollManager.AddRow(dr);
            PollPollManager.Save();
            PollPollManager.DataTable.AcceptChanges();
            int PId = Convert.ToInt32(PollPollManager[PollPollManager.Count - 1]["PollId"]);

            #region DisplayLocations
            // int selected = 0;
            for (int i = 0; i < chkDisplayLocations.Items.Count; i++)
            {
                if (chkDisplayLocations.Items[i].Selected)
                {
                    //selected++;
                    DataRow drLocation = PollDisplayLocationsManager.NewRow();
                    drLocation["PollId"] = PId;
                    drLocation["TypeId"] = chkDisplayLocations.Items[i].Value.ToString();
                    drLocation["UserId"] = Utility.GetCurrentUser_UserId();
                    drLocation["ModifiedDate"] = DateTime.Now;
                    PollDisplayLocationsManager.AddRow(drLocation);
                    if (PollDisplayLocationsManager.Save() > 0)
                    {
                        PollDisplayLocationsManager.DataTable.AcceptChanges();
                    }
                }
                //if (selected==0)
                //{

                //}
            }

            #endregion

            #region Agents
            ASPxListBox ListBoxGroupAgent = (ASPxListBox)cmbAgent.FindControl("ListBoxAgent");
            if (ListBoxGroupAgent == null)
            {
                TransactionManager.CancelSave();
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            ListBoxGroupAgent.DataBind();
            if (ListBoxGroupAgent.SelectedItems.Count == 0)
            {
                SetMessage("نمایندگی را انتخاب نمایید");
                TransactionManager.CancelSave();
                return;
            }
            for (int i = 0; i < ListBoxGroupAgent.SelectedItems.Count; i++)
            {
                if (ListBoxGroupAgent.SelectedItems[i].Value != null)
                {
                    DataRow drAgent = pollAgentPoll.NewRow();
                    drAgent["PollId"] = PId;
                    drAgent["AgentId"] = Convert.ToInt32(ListBoxGroupAgent.SelectedItems[i].Value);
                    drAgent["UserId"] = Utility.GetCurrentUser_UserId();
                    drAgent["ModifiedDate"] = DateTime.Now;

                    pollAgentPoll.AddRow(drAgent);
                    pollAgentPoll.Save();
                    pollAgentPoll.DataTable.AcceptChanges();
                }
            }
            #endregion

            TransactionManager.EndSave();
            PollId = PId;
            SetEditMode();

            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }

    private void Update(int PollId)
    {
        TSP.DataManager.PollPollManager PollPollManager = new TSP.DataManager.PollPollManager();
        TSP.DataManager.PollDisplayLocationsManager PollDisplayLocationsManager = new TSP.DataManager.PollDisplayLocationsManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.pollAgentPoll pollAgentPoll = new TSP.DataManager.pollAgentPoll();
        TransactionManager.Add(PollDisplayLocationsManager);
        TransactionManager.Add(PollPollManager);
        TransactionManager.Add(pollAgentPoll);

        try
        {
            string PreAttachment = "";
            string PreFilettachment = "";
            TransactionManager.BeginSave();

            PollPollManager.FindByCode(PollId);
            if (PollPollManager.Count != 1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                TransactionManager.CancelSave();
                return;
            }
            PollPollManager[0].BeginEdit();

            if (txtTittle.Text != null)
                PollPollManager[0]["Tittle"] = txtTittle.Text;
            if (cmbIsResultPublic.SelectedItem != null)
                PollPollManager[0]["IsResultPublic"] = cmbIsResultPublic.SelectedItem.Value;
            if (cmbType.SelectedItem != null)
                PollPollManager[0]["QuestionCountType"] = cmbType.SelectedItem.Value;
            if (txtStartDate.Text != null)
                PollPollManager[0]["StartShowDate"] = txtStartDate.Text;

            PollPollManager[0]["ValidDate"] = txtValidatDate.Text;

            PollPollManager[0]["Description"] = txtDescription.Text;

            if (Session["PollImage"] != null)
            {
                try
                {
                    if (!Utility.IsDBNullOrNullValue(PollPollManager[0]["ImageURL"]))
                    {
                        PreAttachment = PollPollManager[0]["ImageURL"].ToString();
                    }

                    string FileName = System.IO.Path.GetFileName(Session["PollImage"].ToString());
                    //if (!System.IO.File.Exists("~/Image/Poll/" + FileName))
                    //{
                    PollPollManager[0]["ImageURL"] = "~/Image/Poll/" + FileName;
                    string ImgSoource = Server.MapPath("~/image/Temp/") + FileName;
                    string ImgTarget = Server.MapPath("~/Image/Poll/") + FileName;
                    File.Move(ImgSoource, ImgTarget);
                    // }
                    // Session["PollImage"] =
                    AttachmentImage.ImageUrl = "~/Image/Poll/" + FileName;
                }
                catch (Exception ex)
                {
                    Utility.SaveWebsiteError(ex);
                    string Message = "خطایی در ذخیره فایل انجام گرفته است";
                    if (Utility.ShowExceptionError())
                    {
                        Message = Message + ex.Message;
                    }
                    SetMessage(Message);
                    return;
                }
            }

            if (Session["FileAttach"] != null)
            {
                try
                {
                    if (!Utility.IsDBNullOrNullValue(PollPollManager[0]["FileURL"]))
                    {
                        PreFilettachment = PollPollManager[0]["FileURL"].ToString();
                    }
                    string FileName = System.IO.Path.GetFileName(Session["FileAttach"].ToString());
                    //if (!System.IO.File.Exists("~/Image/Poll/" + FileName))
                    //{
                    PollPollManager[0]["FileURL"] = "~/Image/Poll/" + FileName;
                    string ImgSoource = Server.MapPath("~/image/Temp/") + FileName;
                    string ImgTarget = Server.MapPath("~/Image/Poll/") + FileName;
                    File.Move(ImgSoource, ImgTarget);
                    // }
                    // Session["FileAttach"] = 
                    HyperLinkAttachment.NavigateUrl = "~/Image/Poll/" + FileName;
                }
                catch (Exception ex)
                {
                    Utility.SaveWebsiteError(ex);
                    string Message = "خطایی در ذخیره فایل انجام گرفته است";
                    if (Utility.ShowExceptionError())
                    {
                        Message = Message + ex.Message;
                    }
                    SetMessage(Message);
                    return;
                }
            }
            else
            {
                if (!Utility.IsDBNullOrNullValue(PollPollManager[0]["FileURL"]) && !string.IsNullOrEmpty(HyperLinkAttachment.NavigateUrl))
                {
                    PreFilettachment = PollPollManager[0]["FileURL"].ToString();
                }
                PollPollManager[0]["FileURL"] = DBNull.Value;
            }

            PollPollManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            PollPollManager[0]["ModifiedDate"] = DateTime.Now;


            PollPollManager[0].EndEdit();
            PollPollManager.Save();

            #region DisplayLocations
            PollDisplayLocationsManager.FindByPollId(PollId);
            int DisplayLocationsCount = PollDisplayLocationsManager.Count;
            for (int i = 0; i < DisplayLocationsCount; i++)
            {
                PollDisplayLocationsManager[0].Delete();
                PollDisplayLocationsManager.Save();
                PollDisplayLocationsManager.DataTable.AcceptChanges();
            }

            for (int i = 0; i < chkDisplayLocations.Items.Count; i++)
            {
                if (chkDisplayLocations.Items[i].Selected)
                {
                    DataRow drLocation = PollDisplayLocationsManager.NewRow();
                    drLocation["PollId"] = PollId;
                    drLocation["TypeId"] = chkDisplayLocations.Items[i].Value.ToString();
                    drLocation["UserId"] = Utility.GetCurrentUser_UserId();
                    drLocation["ModifiedDate"] = DateTime.Now;
                    PollDisplayLocationsManager.AddRow(drLocation);
                    if (PollDisplayLocationsManager.Save() > 0)
                    {
                        PollDisplayLocationsManager.DataTable.AcceptChanges();
                    }
                }
            }
            #endregion


            #region Agent
            ASPxListBox ListBoxGroupAgent = (ASPxListBox)cmbAgent.FindControl("ListBoxAgent");
            if (ListBoxGroupAgent == null)
            {
                TransactionManager.CancelSave();
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            ListBoxGroupAgent.DataBind();
            if (ListBoxGroupAgent.SelectedItems.Count == 0)
            {
                SetMessage("نمایندگی را انتخاب نمایید");
                TransactionManager.CancelSave();
                return;
            }
            pollAgentPoll.FindByPollId(PollId);
            ListBoxGroupAgent.DataBind();
            for (int i = 0; i < ListBoxGroupAgent.Items.Count; i++)
            {
                if (ListBoxGroupAgent.Items[i].Value == null)
                    continue;
                if (ListBoxGroupAgent.Items[i].Selected)//If Selected
                {
                    pollAgentPoll.CurrentFilter = "AgentId= " + ListBoxGroupAgent.Items[i].Value.ToString();
                    if (pollAgentPoll.DataTable.DefaultView.Count == 0)//If Not In
                    {
                        DataRow drAgent = pollAgentPoll.NewRow();
                        drAgent["PollId"] = PollId;
                        drAgent["AgentId"] = Convert.ToInt32(ListBoxGroupAgent.SelectedItems[i].Value);
                        drAgent["UserId"] = Utility.GetCurrentUser_UserId();
                        drAgent["ModifiedDate"] = DateTime.Now;
                        pollAgentPoll.AddRow(drAgent);
                        pollAgentPoll.Save();
                        pollAgentPoll.DataTable.AcceptChanges();
                    }
                    pollAgentPoll.CurrentFilter = "";
                }
                if (!ListBoxGroupAgent.Items[i].Selected)//If Not Selected
                {
                    pollAgentPoll.CurrentFilter = "AgentId= " + ListBoxGroupAgent.Items[i].Value.ToString();
                    if (pollAgentPoll.DataTable.DefaultView.Count > 0)//But If Inserted
                    {
                        int cnt = pollAgentPoll.DataTable.DefaultView.Count;
                        for (int j = 0; j < cnt; j++)
                        {
                            pollAgentPoll.DataTable.DefaultView[j].Delete();
                            pollAgentPoll.Save();
                            pollAgentPoll.DataTable.AcceptChanges();
                        }
                    }
                    pollAgentPoll.CurrentFilter = "";
                }
            }
            #endregion
            TransactionManager.EndSave();
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));

            try
            {
                if (!string.IsNullOrWhiteSpace(PreAttachment))
                {
                    File.Delete(MapPath(PreAttachment));
                }
            }
            catch (Exception ex)
            {
                SetMessage("خطایی در حذف تصویر قبلی ایجاد شده است");
                Utility.SaveWebsiteError(ex);
            }
              try
            {
                if (!string.IsNullOrWhiteSpace(PreFilettachment))
                {
                    File.Delete(MapPath(PreFilettachment));
                }
            }
            catch (Exception ex)
            {
                SetMessage("خطایی در حذف فایل قبلی ایجاد شده است");
                Utility.SaveWebsiteError(ex);
            }
            
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }

    private bool CheckExistQuestion()
    {
        if (cmbType.SelectedItem.Value == null)
        {
            SetMessage("تعداد سوالات نظر سنجی را انتخاب کنید");
            return false;
        }

        TSP.DataManager.PollQuestionManager PollQuestionManager = new TSP.DataManager.PollQuestionManager();
        PollQuestionManager.FindByPollId(PollId);
        if (PollQuestionManager.Count > 0 && PollId != -1)
        {
            Boolean isCompulsory;

            int value = Convert.ToInt32(cmbType.SelectedItem.Value.ToString());
            if (PollQuestionManager.Count > 1 && value == 0)
            {
                SetMessage("تعداد سوالات تعریف شده برای این نظر سنجی بیش از یک مورداست");
                return false;
            }

            if (!Utility.IsDBNullOrNullValue(PollQuestionManager[0]["compulsory"]))
            {
                isCompulsory = Convert.ToBoolean(PollQuestionManager[0]["compulsory"]);
                if (PollQuestionManager.Count == 1 && value == 0 && isCompulsory == false)
                {
                    SetMessage("پاسخ به سوال نظر سنجی با یک سوال بایداجباری باشد");
                    return false;
                }
            }
            else
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return false;
            }
        }
        return true;
    }

    #endregion

    //private void SetImageAndFile()
    //{
    //    //if (Session["FileAttach"] != null)
    //    //    AttachmentImage.ImageUrl = Server.MapPath(Session["FileAttach"].ToString());
    //    //if (Session["PollImage"] != null)
    //    //    AttachmentImage.ImageUrl = Server.MapPath(Session["PollImage"].ToString());
    //}
    #endregion
}