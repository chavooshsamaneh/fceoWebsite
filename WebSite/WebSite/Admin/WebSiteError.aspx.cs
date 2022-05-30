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
using TSP.DataManager;
public partial class Admin_websiteError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (Utility.GetCurrentUser_IsTspAdmin() == false)
            Response.Redirect("~/Login.aspx");
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "WebsiteErrors";
        GridViewExporter.WriteXlsToResponse(true);
    }
    protected void grvWebsiteErrrs_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Print":

                    grvWebsiteErrrs.JSProperties["cpPrint"] = 1;

                    Session["DataTable"] = grvWebsiteErrrs.Columns;
                    Session["DataSource"] = ObjectDataSource1;

                    Session["Title"] = "کارت های عضویت";
                    break;
            }
        }
    }

    protected void btnIsValide_Click(object sender, EventArgs e)
    {
        if (grvWebsiteErrrs.FocusedRowIndex > -1)
        {
            DataRow dr = grvWebsiteErrrs.GetDataRow(grvWebsiteErrrs.FocusedRowIndex);
            if(dr!=null)
            {
                int Id = (int)dr["Id"];
                TSP.DataManager.WebsiteErrorsManager WebsiteErrorsManager = new WebsiteErrorsManager();

                WebsiteErrorsManager.FindByCode(Id);
                if (WebsiteErrorsManager.Count != 1)
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    return;
                }
                WebsiteErrorsManager[0].Delete();
                if(WebsiteErrorsManager.Save()>0)                
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));                
                else
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));

                grvWebsiteErrrs.DataBind();
            }
        }
        else
        {
            ShowMessage("ردیفی انتخاب نشده است");
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

}

