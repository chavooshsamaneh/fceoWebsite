using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Message_PrintMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["MessageId"]))
        {
            // Response.Redirect("MessageFromPublicUsers.aspx");
            lblMail.Text = string.Empty;
            lblMail.Text = "خطایی در بازخوانی اطلاعات انجام گرفته است";
            return;
        }

        int MessageId = -1;
        try
        {
            MessageId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MessageId"]).ToString());
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        LoadMessage(MessageId);
    }

    void LoadMessage(int MessageId)
    {
        try
        {
            TSP.DataManager.PublicMessagesManager MessageManage = new TSP.DataManager.PublicMessagesManager();
            MessageManage.FindByCode(MessageId);
            System.Data.DataTable Dt = MessageManage.DataTable;
            lblMail.Text = string.Empty;
            lblMail.Text += "<div align=right style=\"BACKGROUND-COLOR: snow;\"><table width=\"100%\" cellspacing=5px> ";
            lblMail.Text += "<tr><td style=\"width:90px\">شماره ثبت :</td><td style=\"width:200px\">" + MessageId + "</td><td style=\"width:120px\"></td>";
            if (String.IsNullOrEmpty(Dt.Rows[0]["SenderUserId"].ToString()) == false)
                lblMail.Text += "<td>نام کاربری :</td><td>" + "(" + Dt.Rows[0]["SenderUserId"] + ") " + Dt.Rows[0]["SenderUsername"] + "</td>";

            lblMail.Text += "</tr><tr><td>فرستنده :</td><td>" + Dt.Rows[0]["SenderName"] + "</td><td></td>";
            lblMail.Text += "<td style=\"width:90px\">تاریخ ارسال :</td><td style=\"width:200px\">" + Dt.Rows[0]["SendDate"] + "&nbsp;&nbsp;" + Dt.Rows[0]["SendTime"] + "</td></tr>";
            lblMail.Text += "<tr><td >Email :</td><td>" + Dt.Rows[0]["SenderEmail"] + "</td><td></td>";
            lblMail.Text += "<td>شماره تماس :</td><td>" + Dt.Rows[0]["SenderTelNo"] + "</td></tr>";
            lblMail.Text += "</tr><tr><td >بخش گیرنده :</td><td>" + Dt.Rows[0]["GroupName"] + "</td><td></td>";
            lblMail.Text += "<td>نوع پیغام :</td><td>" + Dt.Rows[0]["TypeName"] + "</td></tr>";
            lblMail.Text += "<tr><td>موضوع :</td><td colspan=4>" + Dt.Rows[0]["SendMessageSubject"] + "</td></tr>";
            lblMail.Text += "</table></div><hr><div align=right><table width=\"100%\" cellspacing=5px> ";
            lblMail.Text += "<tr><td >شرح پیغام :</td></tr>";
            lblMail.Text += "<tr><td style=\"BACKGROUND-COLOR: aliceblue;\">" + Dt.Rows[0]["SendMessageBody"].ToString().Replace("\n", "<br>") + "</td></tr>";
            lblMail.Text += "</table></div>";
        }
        catch (Exception err) { }
    }
}