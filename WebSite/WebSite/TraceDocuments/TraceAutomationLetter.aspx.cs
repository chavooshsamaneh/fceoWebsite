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
using TSP.DataManager.Automation;

public partial class TraceDocuments_TraceAutomationLetter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Load_PageData(int.Parse(Utility.DecryptQS(Request.QueryString["LId"])));
        }
        catch (Exception)
        {
            Response.Redirect("~/TraceDocuments.aspx");
        }
    }

    void Load_PageData(int LetterId)
    {
        LettersManager LetterManager = new LettersManager();
        LetterManager.FindById(LetterId);
        lblDocumentNo.Text = LetterManager[0]["LetterNumber"].ToString();
        lblSerialNo.Text = LetterManager[0]["LetterSerialNumber"].ToString();
        lblDocumentDate.Text = LetterManager[0]["LetterDate"].ToString();
        lblSecretariat.Text = LetterManager[0]["SecretariatName"].ToString();
        lblGroup.Text = LetterManager[0]["CreationTypeName"].ToString();
        lblDocumentType.Text = LetterManager[0]["TypeName"].ToString();
        lblSendRecieveType.Text = LetterManager[0]["SendRecieveTypeName"].ToString();
        lblTitleType.Text = LetterManager[0]["TitleTypeName"].ToString();
        lblTittle.Text = LetterManager[0]["Title"].ToString().Replace("\n", "<br>");
        lblSender.Text = LetterManager[0]["SenderFullName"].ToString();

        #region Recivers
        LetterRecieversManager LetterRecieverManager = new LetterRecieversManager();
        LetterRecieverManager.FindByLetterId(LetterId);
        String StrPerson = "", StrOrganization = "", StrNezamMembers = "", StrNezamChartMembers = "";

        foreach (DataRow drRecievers in LetterRecieverManager.DataTable.Rows)
        {
            switch ((LetterRecieverTypesManager.Types)int.Parse(drRecievers["RecieverType"].ToString()))
            {
                case LetterRecieverTypesManager.Types.NewPerson:
                    StrPerson += (String.IsNullOrEmpty(StrPerson)) ? "" : "،";
                    StrPerson += drRecievers["Name"].ToString();
                    break;
                case LetterRecieverTypesManager.Types.Organization:
                    StrOrganization += (String.IsNullOrEmpty(StrOrganization)) ? "" : "،";
                    StrOrganization += drRecievers["OrgName"].ToString();
                    break;
                case LetterRecieverTypesManager.Types.NezamMembers:
                    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                    DataTable dtMember = MemberManager.SelectMembersName(int.Parse(drRecievers["MemberId"].ToString()));
                    StrNezamMembers += (String.IsNullOrEmpty(StrNezamMembers)) ? "" : "،";
                    StrNezamMembers += dtMember.Rows[0]["FirstName"] + " " + dtMember.Rows[0]["LastName"];
                    break;
                case LetterRecieverTypesManager.Types.NezamChartMembers:
                    TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
                    NezamMemberChartManager.FindByNmcId(int.Parse(drRecievers["NezamMemberChartId"].ToString()));
                    StrNezamChartMembers += (String.IsNullOrEmpty(StrNezamChartMembers)) ? "" : "،";
                    StrNezamChartMembers += NezamMemberChartManager[0]["FullName"].ToString() + " (" + NezamMemberChartManager[0]["NcName"].ToString() + ")";
                    break;
            }
        }

        if (String.IsNullOrEmpty(StrPerson) == false)
            lblRecievers.Text = "اشخاص: " + StrPerson;
        if (String.IsNullOrEmpty(StrOrganization) == false)
        {
            lblRecievers.Text += (String.IsNullOrEmpty(lblRecievers.Text)) ? "" : "<br>";
            lblRecievers.Text += "سازمان ها: " + StrOrganization;
        }
        if (String.IsNullOrEmpty(StrNezamMembers) == false)
        {
            lblRecievers.Text += (String.IsNullOrEmpty(lblRecievers.Text)) ? "" : "<br>";
            lblRecievers.Text += "اعضا: " + StrNezamMembers;
        }
        if (String.IsNullOrEmpty(StrNezamChartMembers) == false)
        {
            lblRecievers.Text += (String.IsNullOrEmpty(lblRecievers.Text)) ? "" : "<br>";
            lblRecievers.Text += "کارمندان : " + StrNezamChartMembers;
        }
        #endregion

        #region LetterRefernceTrace;
        ObjectDataSourceLetterReferenceTrace.SelectParameters["LetterId"].DefaultValue = LetterId.ToString();
        #endregion
    }

    protected void lblReferenceDescription_DataBinding(object sender, EventArgs e)
    {
        Label lblReferenceDescription = (Label)sender;
        if (String.IsNullOrEmpty(lblReferenceDescription.Text.Trim()) == false)
            lblReferenceDescription.Text = "شرح ارجاع : " + lblReferenceDescription.Text.Replace("\n", "<br>") + "<br>";
    }

    protected void lblActionDescription_DataBinding(object sender, EventArgs e)
    {
        Label lblActionDescription = (Label)sender;
        if (String.IsNullOrEmpty(lblActionDescription.Text.Trim()) == false)
            lblActionDescription.Text = "شرح اقدام : " + lblActionDescription.Text.Replace("\n", "<br>") + "<br>";
    }
}
