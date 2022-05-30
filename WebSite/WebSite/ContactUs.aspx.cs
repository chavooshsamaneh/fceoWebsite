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

public partial class ContactUs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Employee)
        {
            PanelPublicContact.Visible = false;
        }

        if (!IsPostBack)
        {
            TSP.DataManager.NezamManager NezamManager = new TSP.DataManager.NezamManager();
            NezamManager.Fill();
            if (NezamManager.Count == 1)
            {
                labelNezam.Text = NezamManager[0]["Description"].ToString();
            }           
        }
        txtName.Focus();

        if (IsPostBack == false)
        {
            cmbMessageGroup.DataBind();
            if (cmbMessageGroup.Items.Count > 0)
                cmbMessageGroup.SelectedIndex = 0;
            cmbMessageType.DataBind();
            if (cmbMessageType.Items.Count > 0)
                cmbMessageType.SelectedIndex = 0;
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        lblSendError.Visible = false;
        try
        {
            TSP.DataManager.PublicMessagesManager MessageManage = new TSP.DataManager.PublicMessagesManager();
            DataRow dr = MessageManage.NewRow();
            dr["SenderName"] = txtName.Text;
            dr["SenderEmail"] = txtEmail.Text;
            dr["SenderTelNo"] = txtTelNo.Text;
            dr["SendMessageSubject"] = txtSubject.Text;
            dr["SendMessageBody"] = txtBody.Text;//.Replace("\n", "<br>");
            dr["InActive"] = false;
            dr["SendDate"] = Utility.GetDateOfToday();
            dr["SendTime"] = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');//DateTime.Now.ToShortTimeString();
            dr["AnswerState"] = false;
            dr["SenderHostName"] = Request.UserHostName;
            dr["SenderAddress"] = Request.UserHostAddress;
            dr["GroupId"] = cmbMessageGroup.Value.ToString();
            dr["TypeId"] = cmbMessageType.Value.ToString();
            if (Page.User.Identity.IsAuthenticated == true)
            {
                dr["SenderUserId"] = Utility.GetCurrentUser_UserId();
            }
            dr["ModifiedDate"] = DateTime.Now;
            MessageManage.AddRow(dr);
            int SaveState = MessageManage.Save();
            MessageManage.DataTable.AcceptChanges();
            if (SaveState > 0)
            {
                panelSendFinish.Visible = true;
                panelSend.Visible = false;
            }
            else
                lblSendError.Visible = true;
        }
        catch (Exception err)
        {
            lblSendError.Visible = true;
            Utility.SaveWebsiteError(err);
        }
    }
}
