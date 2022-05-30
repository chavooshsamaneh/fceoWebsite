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

public partial class Employee_Message_AnsweredMessageForPublicUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (IsPostBack == false)
        {
            if (Utility.IsUserAdministratore(Utility.GetCurrentUser_UserId()) == false)
                ObjectDataSource1.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
            else
                ObjectDataSource1.SelectParameters["MeId"].DefaultValue = "-1";
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.FileName = "Message";
        ASPxGridViewExporter1.WriteXlsToResponse(true);
    }

    protected void CallBackMails_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        try
        {
            if (String.IsNullOrEmpty(e.Parameter) == false)
            {
                TSP.DataManager.PublicMessagesManager MessageManage = new TSP.DataManager.PublicMessagesManager();
                String MessageId = grdMails.GetDataRow(Int32.Parse(e.Parameter))["MessageId"].ToString();
                MessageManage.FindByCode(Int32.Parse(MessageId));
                DataTable Dt = MessageManage.DataTable;

                lblMail.Text = "";
                lblMail.Text += "<div align=right style=\"BACKGROUND-COLOR: snow;\"><table width=\"100%\" cellspacing=5px> ";
                lblMail.Text += "<tr><td style=\"width:90px\">شماره ثبت :</td><td style=\"width:200px\">" + MessageId + "</td><td style=\"width:120px\"></td>";
                lblMail.Text += "<td style=\"width:90px\">تاریخ ارسال :</td><td style=\"width:200px\">" + Dt.Rows[0]["SendDate"] + "  " + Dt.Rows[0]["SendTime"] + "</td></tr>";
                lblMail.Text += "<tr><td>فرستنده :</td><td>" + Dt.Rows[0]["SenderName"] + "</td><td></td>";
                if (String.IsNullOrEmpty(Dt.Rows[0]["SenderUserId"].ToString()) == false)
                    lblMail.Text += "<td>نام کاربری :</td><td>" + "(" + Dt.Rows[0]["SenderUserId"] + ") " + Dt.Rows[0]["SenderUsername"] + "</td>";
                lblMail.Text += "</tr><tr><td >Email :</td><td>" + Dt.Rows[0]["SenderEmail"] + "</td><td></td>";
                lblMail.Text += "<td>شماره تماس :</td><td>" + Dt.Rows[0]["SenderTelNo"] + "</td></tr>";
                lblMail.Text += "</tr><tr><td >بخش گیرنده :</td><td>" + Dt.Rows[0]["GroupName"] + "</td><td></td>";
                lblMail.Text += "<td>نوع پیغام :</td><td>" + Dt.Rows[0]["TypeName"] + "</td></tr>";
                //lblMail.Text += "<tr><td>HostName :</td><td dir=ltr>" + Dt.Rows[0]["SenderHostName"] + "</td><td></td>";
                //lblMail.Text += "<td>Address :</td><td dir=ltr>" + Dt.Rows[0]["SenderAddress"] + "</td></tr>";
                lblMail.Text += "<tr><td>موضوع :</td><td colspan=4>" + Dt.Rows[0]["SendMessageSubject"] + "</td></tr>";
                lblMail.Text += "<tr><td>پاسخ دهنده :</td><td>" + "(" + Dt.Rows[0]["AnswererUserId"] + ") " + Dt.Rows[0]["AnswererUsername"] + "</td><td></td>";
                lblMail.Text += "<td>تاریخ پاسخ :</td><td>" + Dt.Rows[0]["AnswerDate"] + "  " + Dt.Rows[0]["AnswerTime"] + "</td></tr>";
                lblMail.Text += "</table></div><hr>";
                lblMail.Text += "<div align=right style=\"BACKGROUND-COLOR: gainsboro; font-style:italic\"><table width=\"100%\" cellspacing=5px><tr><td>";
                lblMail.Text += "<a style=\"text-decoration:none\" href=\"javascript:ShowHideMessageBox()\"><div id=MessageHeader>+ شرح پیغام :</div></a></td></tr>";
                lblMail.Text += "<tr><td><div id=MessageBox style=\"overflow:hidden;display:none\">" + Dt.Rows[0]["SendMessageBody"].ToString().Replace("\n", "<br>") + "</div></td></tr>";
                lblMail.Text += "</table></div>";
                lblMail.Text += "<br><div align=right><table width=\"100%\" cellspacing=5px><tr><td>پاسخ :</td></tr>";
                lblMail.Text += "<tr><td style=\"BACKGROUND-COLOR: aliceblue;\">" + Dt.Rows[0]["AnswerText"].ToString().Replace("\n", "<br>") + "</td></tr>";
                lblMail.Text += "</table></div>";
            }
        }
        catch (Exception err) { }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (grdMails.FocusedRowIndex > -1)
            {
                TSP.DataManager.PublicMessagesManager MessageManage = new TSP.DataManager.PublicMessagesManager();
                String MessageId = grdMails.GetDataRow(grdMails.FocusedRowIndex)["MessageId"].ToString();
                MessageManage.FindByCode(Int32.Parse(MessageId));
                MessageManage[0].BeginEdit();
                MessageManage[0]["InActive"] = true;
                MessageManage[0]["AnswererUserId"] = Utility.GetCurrentUser_UserId();
                MessageManage[0]["ModifiedDate"] = DateTime.Today;
                MessageManage[0].EndEdit();
                if (MessageManage.Save() > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد.";
                }
                MessageManage.DataTable.AcceptChanges();
                grdMails.DataBind();
                if (grdMails.VisibleRowCount > 0)
                {
                    grdMails.FocusedRowIndex = 0;
                    CallBackMails_Callback(this, new DevExpress.Web.CallbackEventArgsBase(grdMails.GetDataRow(0)["MessageId"].ToString()));
                }
            }
        }
        catch (Exception err) { }
    }
}
