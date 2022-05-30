using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using DevExpress.Web;
public partial class Employee_Amoozesh_HomePageFiles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            TSP.DataManager.Permission per = TSP.DataManager.HomePageAttachmentManager.GetUserPermissionAmoozeshFiles(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            BtnSave.Enabled = per.CanEdit;
            BtnSave2.Enabled = per.CanEdit;

            this.ViewState["BtnSave"] = BtnSave.Enabled;

            Session["HomePageAttachHelpLmsAddress"] = "";
            Session["HomePageAttachScheduleAddress"] = "";
            Load_Attach();

        }

        if (this.ViewState["BtnSave"] != null)
            this.BtnSave.Enabled = this.BtnSave2.Enabled = (bool)this.ViewState["BtnSave"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {

               //Session["HomePageAttachHelpLmsAddress"] 
               //     Session["HomePageAttachScheduleAddress"] 
        String OldFileAddressAttachInformationHelpLmsAddress = "", OldFileAttachScheduleFileAddress = "";
        String FileAddressAttachInformationHelpLmsAddress = "", FileAddressAttachScheduleFileAddress = "";
        try
        {
            System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
            XmlDoc.Load(MapPath("~/App_Data/OtherAttachments.xml"));
            int Index = 0;
            System.Xml.XmlNodeList File = XmlDoc.GetElementsByTagName("File");
            System.Xml.XmlNodeList Enabled = XmlDoc.GetElementsByTagName("Enabled");
            System.Xml.XmlNodeList Text = XmlDoc.GetElementsByTagName("Text");

            #region HelpLMS
            Index = FindXmlItemIndex(XmlDoc, "HelpLMS");
            if (Index > -1)
            {
                if (Session["HomePageAttachHelpLmsAddress"] != null && String.IsNullOrEmpty(Session["HomePageAttachHelpLmsAddress"].ToString()) == false)
                {
                    OldFileAddressAttachInformationHelpLmsAddress = File.Item(Index).InnerText;
                    File.Item(Index).InnerText = "../../Help/Amoozesh/" + System.IO.Path.GetFileName(Session["HomePageAttachHelpLmsAddress"].ToString());
                    FileAddressAttachInformationHelpLmsAddress = "~/Help/Amoozesh/" + System.IO.Path.GetFileName(Session["HomePageAttachHelpLmsAddress"].ToString());
                }
            }
            #endregion

            #region Schedule
            Index = FindXmlItemIndex(XmlDoc, "AmoozeshSchedule");
            if (Index > -1)
            {
                if (Session["HomePageAttachScheduleAddress"] != null && String.IsNullOrEmpty(Session["HomePageAttachScheduleAddress"].ToString()) == false)
                {
                    OldFileAttachScheduleFileAddress = File.Item(Index).InnerText;
                    File.Item(Index).InnerText = "../../Help/Amoozesh/" + System.IO.Path.GetFileName(Session["HomePageAttachScheduleAddress"].ToString());
                    FileAddressAttachScheduleFileAddress = "~/Help/Amoozesh/" + System.IO.Path.GetFileName(Session["HomePageAttachScheduleAddress"].ToString());
                }             
            }
            #endregion

            XmlDoc.Save(MapPath("~/App_Data/OtherAttachments.xml"));
            ShowMessage("ذخیره انجام شد.");
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            return;
        }
        ////Move
        try
        {
            if (Session["HomePageAttachHelpLmsAddress"] != null && String.IsNullOrEmpty(Session["HomePageAttachHelpLmsAddress"].ToString()) == false)
            {
                System.IO.File.Move(Session["HomePageAttachHelpLmsAddress"].ToString(), MapPath("~/Help/Homepage/") + System.IO.Path.GetFileName(Session["HomePageAttachHelpLmsAddress"].ToString()));
                Session["HomePageAttachHelpLmsAddress"] = null;
            }
        }
        catch (Exception) { }

        try
        {
            if (Session["HomePageAttachScheduleAddress"] != null && String.IsNullOrEmpty(Session["HomePageAttachScheduleAddress"].ToString()) == false)
            {
                System.IO.File.Move(Session["HomePageAttachScheduleAddress"].ToString(), MapPath("~/Help/Homepage/") + System.IO.Path.GetFileName(Session["HomePageAttachScheduleAddress"].ToString()));
                Session["HomePageAttachScheduleAddress"] = null;
            }
        }
        catch (Exception) { }
          ////Delete Old
        try
        {
            if (String.IsNullOrEmpty(FileAddressAttachInformationHelpLmsAddress) == false)
                System.IO.File.Delete(MapPath(OldFileAddressAttachInformationHelpLmsAddress));
        }
        catch (Exception) { }

        try
        {
            if (String.IsNullOrEmpty(FileAddressAttachScheduleFileAddress) == false)
                System.IO.File.Delete(MapPath(OldFileAttachScheduleFileAddress));
        }
        catch (Exception) { }
        ////Set ImageURL
        //if (String.IsNullOrEmpty(FileAddressAttachInformationHelpLmsAddress) == false)
        //{            
        //    imgEndUploadHelpLms.ImageUrl = FileAddressAttachInformationHelpLmsAddress;
        //}

        //if (String.IsNullOrEmpty(FileAddressAttachScheduleFileAddress) == false)
        //{            
        //    imgEndUploadHelpLms.ImageUrl = FileAddressAttachScheduleFileAddress;
        //}

        Load_Attach();
    }

    protected void UploadAttachHelpLms_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveAttach(e.UploadedFile, "HelpLms");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected void UploadAttachSchedule_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveAttach(e.UploadedFile,"Schedule");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    void Load_Attach()
    {
        try
        {
            System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
            XmlDoc.Load(MapPath("~/App_Data/OtherAttachments.xml"));
            System.Xml.XmlNodeList File = XmlDoc.GetElementsByTagName("File");
            System.Xml.XmlNodeList Enabled = XmlDoc.GetElementsByTagName("Enabled");
            System.Xml.XmlNodeList Text = XmlDoc.GetElementsByTagName("Text");
            int Index = -1;
            #region HelpLMS
            Index = FindXmlItemIndex(XmlDoc, "HelpLMS");
            if (Index > -1)
            {

                if (String.IsNullOrEmpty(File.Item(Index).InnerText) == false)
                {
                    HyperLinkhHelpLms.NavigateUrl = File.Item(Index).InnerText;
                    //imgEndUploadHelpLms.ToolTip = Text.Item(Index).InnerText;
                }
            }

            #endregion

            #region AmoozeshSchedule
            Index = FindXmlItemIndex(XmlDoc, "AmoozeshSchedule");
            if (Index > -1)
            {

                if (String.IsNullOrEmpty(File.Item(Index).InnerText) == false)
                {
                    HyperLinkSchedule.NavigateUrl = File.Item(Index).InnerText;
                    //imgEndUploadSchedule.ToolTip = Text.Item(Index).InnerText;
                }
            }

            #endregion
        }
        catch (Exception)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    int FindXmlItemIndex(System.Xml.XmlDocument XmlDoc, String ItemName)
    {
        System.Xml.XmlNodeList Items = XmlDoc.GetElementsByTagName("Name");
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items.Item(i).InnerText == ItemName)
                return i;
        }
        return -1;
    }

    protected string SaveAttach(UploadedFile uploadedFile,string Type)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new System.IO.FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Help/Amoozesh/") + ret) == true);
            string tempFileName = MapPath("~/Help/Amoozesh/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            switch(Type)
            {
                case"HelpLms":
                    Session["HomePageAttachHelpLmsAddress"] = tempFileName;
                    break;
                case "Schedule":
                    Session["HomePageAttachScheduleAddress"] = tempFileName;
                    break;
            }
        }
        return ret;
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
}