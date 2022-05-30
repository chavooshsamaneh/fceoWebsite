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

public partial class Employee_Message_MessageFromPublicUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (Utility.IsUserAdministratore(Utility.GetCurrentUser_UserId()) == false)
            ObjectDataSource1.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
        else
            ObjectDataSource1.SelectParameters["MeId"].DefaultValue = "-1";
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
                HiddenNewsData["PrintAddress"] = "PrintMessage.aspx?MessageId=" + Utility.EncryptQS(MessageId);

                lblMail.Text = "";
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

    protected void callBackAnswer_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        grdMails.DataBind();
        if (Page.User.Identity.IsAuthenticated == true)
        {
            TSP.DataManager.LoginManager LoginManage = new TSP.DataManager.LoginManager();
            DataTable dtCurrentUser = LoginManage.FindByUsername(Page.User.Identity.Name);

            TSP.DataManager.TransactionManager objTransaction = new TSP.DataManager.TransactionManager();
            TSP.DataManager.PublicMessagesManager MessageManage = new TSP.DataManager.PublicMessagesManager();
            objTransaction.Add(MessageManage);
            objTransaction.BeginSave();

            try
            {
                String Email = grdMails.GetDataRow(grdMails.FocusedRowIndex)["SenderEmail"].ToString();
                String MessageId = grdMails.GetDataRow(grdMails.FocusedRowIndex)["MessageId"].ToString();

                MessageManage.FindByCode(Int32.Parse(MessageId));
                MessageManage[0].BeginEdit();
                MessageManage[0]["AnswerState"] = true;
                MessageManage[0]["AnswerText"] = txtAnswerBody.Text;
                MessageManage[0]["AnswerDate"] = Utility.GetDateOfToday();
                MessageManage[0]["AnswerTime"] = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
                //LoginManage.FindByLoginUsername(Page.User.Identity.Name);
                MessageManage[0]["AnswererUserId"] = dtCurrentUser.Rows[0]["UserId"].ToString();
                MessageManage[0]["ModifiedDate"] = DateTime.Today;
                MessageManage[0].EndEdit();
                int saveState = MessageManage.Save();
                MessageManage.DataTable.AcceptChanges();
                if (saveState > 0)
                {
                    if (SendEmail(Email) == true)
                    {
                        panelSendAnswer.ClientVisible = false;
                        panelSendAnswerFinish.ClientVisible = true;
                        txtAnswerBody.Text = "";

                        objTransaction.EndSave();
                    }
                    else
                    {
                        lblErrorInSendMessage.ClientVisible = true;
                        objTransaction.CancelSave();
                    }
                }
                else
                {
                    lblErrorInSendMessage.ClientVisible = true;
                    objTransaction.CancelSave();
                }
            }
            catch (Exception err)
            {
                lblErrorInSendMessage.ClientVisible = true;
                objTransaction.CancelSave();
                Utility.SaveWebsiteError(err);
            }
        }
        else
            lblErrorInSendMessage.ClientVisible = true;
    }

    protected void callBackSend_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter != null)
        {
            //int GroupId = Convert.ToInt32(e.Parameter);
            int NcId = Convert.ToInt32(e.Parameter);
            try
            {
                TSP.DataManager.PublicMessagesManager MessageManage = new TSP.DataManager.PublicMessagesManager();
                String MessageId = grdMails.GetDataRow(grdMails.FocusedRowIndex)["MessageId"].ToString();
                MessageManage.FindByCode(Int32.Parse(MessageId));
                MessageManage[0].BeginEdit();
                MessageManage[0]["GroupId"] = NcId;// GroupId;
                MessageManage[0]["ModifiedDate"] = DateTime.Now;
                MessageManage[0].EndEdit();
                if (MessageManage.Save() > 0)
                {
                    panelSend.ClientVisible = false;
                    panelSendFinish.ClientVisible = true;
                    cmbMessageGroup.SelectedIndex = -1;
                    grdMails.DataBind();
                }
                else
                {
                    lblErrorInSendMessage1.ClientVisible = true;
                }
            }
            catch (Exception err)
            {
                lblErrorInSendMessage1.ClientVisible = true;
                Utility.SaveWebsiteError(err);
            }
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.FileName = "Message";
        ASPxGridViewExporter1.WriteXlsToResponse(true);
    }
    Boolean SendEmail(String Email)
    {
        Utility.Notifications Notification = new Utility.Notifications(Utility.Notifications.SendTypes.Email, Utility.Notifications.NotificationTypes.AnswerMessageFromPublicUsers);

        DataRow dr = Notification.NotificationData.NewRow();
        dr["EmailAddress"] = Email;
        dr["SenderName"] = grdMails.GetDataRow(grdMails.FocusedRowIndex)["SenderName"].ToString();
        dr["TypeName"] = grdMails.GetDataRow(grdMails.FocusedRowIndex)["TypeName"].ToString();
        dr["GroupName"] = grdMails.GetDataRow(grdMails.FocusedRowIndex)["GroupName"].ToString();
        dr["SendMessageSubject"] = grdMails.GetDataRow(grdMails.FocusedRowIndex)["SendMessageSubject"].ToString();
        dr["AnswerBody"] = txtAnswerBody.Text;
        dr["SendMessageBody"] = grdMails.GetDataRow(grdMails.FocusedRowIndex)["SendMessageBody"].ToString();
        Notification.NotificationData.Rows.Add(dr);
        Notification.NotificationData.AcceptChanges();

        return Notification.Send();
    }

    protected void grdMails_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        grdMails.DataBind();
    }
}

