using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

[ToolboxData("Tsp")]
public partial class SessionAgendaUserControl : System.Web.UI.UserControl
{
    //#region Properties
    //private int _TableType;
    //[Browsable(true), Category("TSP")]
    //public int TableType
    //{
    //    get
    //    {
    //        return this._TableType;
    //    }
    //    set
    //    {
    //        this._TableType = value;
    //        hiddenData["TableType"] = value.ToString();
    //    }
    //}

    //private int _TableId;
    //[Browsable(true), Category("TSP")]
    //public int TableId
    //{
    //    get
    //    {
    //        return this._TableId;
    //    }
    //    set
    //    {
    //        this._TableId = value;
    //        hiddenData["TableId"] = value.ToString();
    //    }
    //}

    //private TSP.DataManager.Session.AgendaTypesManager.Groups _AgendaTypeGroup;
    //[Browsable(true), Category("TSP")]
    //public TSP.DataManager.Session.AgendaTypesManager.Groups AgendaTypeGroup
    //{
    //    get
    //    {
    //        return this._AgendaTypeGroup;
    //    }
    //    set
    //    {
    //        this._AgendaTypeGroup = value;
    //        ObjectDataSourceAgendaTypes.SelectParameters["Group"].DefaultValue = ((int)value).ToString();
    //    }
    //}

    //private String _ClientGridName;
    //[Browsable(true), Category("TSP")]
    //public String ClientGridName
    //{
    //    get
    //    {
    //        return this._ClientGridName;
    //    }
    //    set
    //    {
    //        this._ClientGridName = value;
    //        if (String.IsNullOrWhiteSpace(value) == false)
    //            CallbackPanelSessionAgenda.ClientSideEvents.EndCallback = "function(s, e) {	if(s.cpSaveComplete==1){ " + value + ".PerformCallback('DataBind'); s.cpSaveComplete=0; }}";
    //        else
    //            CallbackPanelSessionAgenda.ClientSideEvents.EndCallback = "function(s, e) { s.cpSaveComplete=0; }";
    //    }
    //}
    //#endregion

    //#region Events
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    txtSessionNumber.Attributes["onkeyup"] = "return ltr_override(event)";
    //    if (IsPostBack == false)
    //    {
    //        Session["UserControlSessionAgendaAttachmentFileAddress"] = null;
    //        hiddenData["TableId"] = "";
    //        hiddenData["TableType"] = "";
    //        hiddenData["SessionId"] = "";
    //        hiddenData["RequestId"] = "";
    //    }
    //}

    //protected void CallbackPanelSessionAgenda_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    //{
    //    CallbackPanelSessionAgenda.JSProperties["cpSaveComplete"] = "0";
    //    PanelMessage.ClientVisible = false;
    //    switch (e.Parameter)
    //    {
    //        case "SessionNumber":
    //            Load_SessionData();
    //            break;
    //        case "Save":
    //            Save();
    //            break;
    //    }
    //}

    //protected void UploadAttach_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    //{
    //    try
    //    {
    //        e.CallbackData = SaveAttachment(e.UploadedFile);
    //    }
    //    catch (Exception ex)
    //    {
    //        e.IsValid = false;
    //        e.ErrorText = ex.Message;
    //    }
    //}
    //#endregion

    //#region Methods
    //private string SaveAttachment(DevExpress.Web.UploadedFile uploadedFile)
    //{
    //    string ret = "";
    //    if (uploadedFile.IsValid)
    //    {
    //        do
    //        {
    //            System.IO.FileInfo ImageType = new System.IO.FileInfo(uploadedFile.PostedFile.FileName);
    //            ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
    //        } while (System.IO.File.Exists(MapPath("~/Image/Session/Agenda/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
    //        string tempFileName = MapPath("~/Image/Temp/") + ret;
    //        uploadedFile.SaveAs(tempFileName, true);
    //        Session["UserControlSessionAgendaAttachmentFileAddress"] = tempFileName;
    //    }
    //    return "../../../Image/Temp/" + ret;
    //}

    //private void Load_SessionData()
    //{
    //    PanelMessage.ClientVisible = false;

    //    TSP.DataManager.Session.SessionRequestsManager SessionRequestsManager = new TSP.DataManager.Session.SessionRequestsManager();
    //    String Result = SessionRequestsManager.FindBySessionNumber(txtSessionNumber.Text);
    //    if (Result == String.Empty)
    //    {
    //        Result = CheckSession(Convert.ToInt32(SessionRequestsManager[0]["SessionId"]), Convert.ToInt32(SessionRequestsManager[0]["RequestType"]));
    //        if (Result == String.Empty)
    //        {
    //            hiddenData["SessionId"] = SessionRequestsManager[0]["SessionId"].ToString();
    //            hiddenData["RequestId"] = SessionRequestsManager[0]["RequestId"].ToString();
    //            txtSessionDateTime.Text = SessionRequestsManager[0]["StartDateTime"].ToString();
    //            txtSessionTitle.Text = SessionRequestsManager[0]["SessionTitle"].ToString();
    //            PanelInputAgenda.ClientVisible = true;
    //            lblSesionError.ClientVisible = false;
    //            return;
    //        }
    //    }

    //    lblSesionError.Text = Result;
    //    lblSesionError.ClientVisible = true;
    //    hiddenData["SessionId"] = "";
    //    hiddenData["RequestId"] = "";
    //    txtSessionDateTime.Text = "";
    //    txtSessionTitle.Text = "";
    //    PanelInputAgenda.ClientVisible = false;
    //}

    //String CheckSession(int SessionId, int RequestType)
    //{
    //    String Result = String.Empty;

    //    if (RequestType == (int)TSP.DataManager.Session.RequestTypesManager.Types.MeetingMinute)
    //    {
    //        Result = "جلسه برگزار شده است و امکان ثبت دستورجلسه وجود ندارد";
    //    }
    //    else if (RequestType == (int)TSP.DataManager.Session.RequestTypesManager.Types.Cancel)
    //    {
    //        Result = "جلسه لغو شده است و امکان ثبت دستورجلسه وجود ندارد";
    //    }
    //    else if (RequestType == (int)TSP.DataManager.Session.RequestTypesManager.Types.ChangeDateTime)
    //    {
    //        Result = "امکان ثبت دستورجلسه برای این جلسه وجود ندارد";
    //    }
    //    else if ((new TSP.DataManager.Session.SessionsManager()).CheckSessionDateTimeHolding(SessionId) == false)
    //    {
    //        Result = "امکان ثبت دستورجلسه بعد از شروع و برگزاری جلسه وجود ندارد";
    //    }

    //    return Result;
    //}

    //private void Save()
    //{
    //    try
    //    {
    //        TSP.DataManager.Session.AgendaManager AgendaManager = new TSP.DataManager.Session.AgendaManager();
    //        DataRow dr = AgendaManager.NewRow();
    //        dr["SessionId"] = Convert.ToInt32(hiddenData["SessionId"]);
    //        dr["RequestId"] = Convert.ToInt32(hiddenData["RequestId"]);
    //        dr["Title"] = txtAgendaTitle.Text;
    //        dr["Details"] = txtDetail.Text;
    //        dr["Description"] = txtDescription.Text;
    //        if (Session["UserControlSessionAgendaAttachmentFileAddress"] != null && String.IsNullOrEmpty(Session["UserControlSessionAgendaAttachmentFileAddress"].ToString()) == false)
    //            dr["AttachFile"] = "~/Image/Session/Agenda/" + System.IO.Path.GetFileName(Session["UserControlSessionAgendaAttachmentFileAddress"].ToString());
    //        dr["TableType"] = hiddenData["TableType"].ToString();
    //        dr["TableId"] = hiddenData["TableId"].ToString();
    //        dr["AgendaTypeId"] = Convert.ToInt32(cmbAgendaType.Value);
    //        dr["UserId"] = Utility.GetCurrentUser_UserId();
    //        dr["ModifiedDate"] = DateTime.Now;
    //        AgendaManager.AddRow(dr);
    //        if (AgendaManager.Save() > 0)
    //        {
    //            AgendaManager.DataTable.AcceptChanges();
    //            PanelInput.ClientVisible = false;
    //            PanelMessage.ClientVisible = true;
    //            lblMessage.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete);
    //            lblMessage.ForeColor = System.Drawing.Color.DarkGreen;
    //            CallbackPanelSessionAgenda.JSProperties["cpSaveComplete"] = "1";
    //        }
    //        else
    //        {
    //            lblErrorInSave.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave) + "<br><br>";
    //            lblErrorInSave.Visible = true;
    //            CallbackPanelSessionAgenda.JSProperties["cpSaveComplete"] = "0";
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        Utility.SaveWebsiteError(err);
    //        lblErrorInSave.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave) + "<br><br>";
    //        lblErrorInSave.Visible = true;
    //        CallbackPanelSessionAgenda.JSProperties["cpSaveComplete"] = "0";
    //        return;
    //    }
    //    try
    //    {
    //        if (Session["UserControlSessionAgendaAttachmentFileAddress"] != null && String.IsNullOrEmpty(Session["UserControlSessionAgendaAttachmentFileAddress"].ToString()) == false)
    //        {
    //            System.IO.File.Move(Session["UserControlSessionAgendaAttachmentFileAddress"].ToString(), MapPath("~/Image/Session/Agenda/") + System.IO.Path.GetFileName(Session["UserControlSessionAgendaAttachmentFileAddress"].ToString()));
    //            Session["UserControlSessionAgendaAttachmentFileAddress"] = null;
    //        }
    //    }
    //    catch (Exception) { }
    //}
    //#endregion
}