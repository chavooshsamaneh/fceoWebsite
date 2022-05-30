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

public partial class TraceDocuments_TraceDocuments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String DocumentNo = "";
        try
        {
            DocumentNo = Utility.DecryptQS(Request.QueryString["No"]);
            if (DocumentNo == null)
                return;
        }
        catch (Exception)
        {
            return;
        }

        if (IsPostBack == false)
        {
           // if (Session["ShowDocumentSecurityCode"] != null && (Boolean)Session["ShowDocumentSecurityCode"] == true)
          //  {
           //     ShowInputError("کد امنیتی وارد شده اشتباه است");
          //  }
          //  else
                FindDocument(DocumentNo);
        }

        //txtDocumentNo.Focus();
    }

    protected void btnTraceDocument_Click(object sender, EventArgs e)
    {
        if (CheckSecurityCode())
            FindDocument(txtDocumentNo.Text);
        else
            ShowInputError("کد امنیتی وارد شده اشتباه است");

        txtDocumentNo.Text = "";
        //txtSecurityCode.Text = "";
    }

    void FindDocument(String DocumentNo)
    {
        try
        {
            switch (DocumentNo[0])
            {
                case 'A': //Automation
                    TSP.DataManager.Automation.LettersManager LetterManager = new TSP.DataManager.Automation.LettersManager();
                    LetterManager.FindByLetterSerialNumber(DocumentNo);
                    if (LetterManager.Count > 0)
                    {
                        Session["ShowDocumentSecurityCode"] = false;
                        Response.Redirect("~/TraceDocuments/TraceAutomationLetter.aspx?LId=" + Utility.EncryptQS(LetterManager[0]["LetterId"].ToString()));
                    }
                    break;
                case 'D'://Document
                    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                    DocMemberFileManager.SelectDocumentMemberFile(DocumentNo,0);
                    //DocMemberFileManager.SelectMainRequest(DocumentNo);
                    if (DocMemberFileManager.Count== 1)
                    {
                        //if (Convert.ToInt32(DocMemberFileManager[0]["IsConfirm"]) == 1)
                        //{
                            Session["ShowDocumentSecurityCode"] = false;
                            Response.Redirect("~/TraceDocuments/TraceDocMemberFile.aspx?MfId=" + Utility.EncryptQS(DocMemberFileManager[0]["MfId"].ToString()));
                        //}
                        //else
                        //{
                        //    ShowInputError("این پروانه هنوز مورد تایید سازمان قرار نگرفته است");
                        //    Session["ShowDocumentSecurityCode"] = true;
                        //    return;
                        //}
                    }
                    break;
            }
        }
        catch (Exception ex) { }

        ShowInputError("کد ده رقمی وارد شده صحیح نمی باشد");
        Session["ShowDocumentSecurityCode"] = true;
    }

    Boolean CheckSecurityCode()
    {
        return Captcha.IsValid;
    }

    void ShowInputError(String Error)
    {
        lblError.Text = "<img src='../Images/edtError.png'/>&nbsp;";
        lblError.Text += Error;
        lblError.Visible = true;
       // panelSecurityCode.Visible = true;
    }
}
