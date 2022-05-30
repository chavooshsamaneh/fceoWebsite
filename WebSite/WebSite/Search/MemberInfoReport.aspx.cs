using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Search_MemberInfoReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("style", "display:block");
        this.DivReport.Attributes.Add("style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {

        }
    }


    protected void CallbackPanelMain_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        CallbackPanelMain.JSProperties["cpMsg"] ="";
        CallbackPanelMain.JSProperties["cpError"] = 0;
        CallbackPanelMain.JSProperties["cpPrintMe"] = 0;
        CallbackPanelMain.JSProperties["cpPrintURL"] = "";
        CallbackPanelMain.JSProperties["cpSearch"] = 0;

        switch (e.Parameter)
        {
            case "search":
                Search();
                break;
            case "clear":
                Clear();
                break;
            case "PrintMe":
                if (!Utility.IsDBNullOrNullValue(txtMeIdSearch.Text))
                {
                    CallbackPanelMain.JSProperties["cpPrintMe"] = 1;
                    CallbackPanelMain.JSProperties["cpPrintURL"] = "../ReportForms/MemberInfoReport.aspx?MeId="+Utility.EncryptQS(txtMeIdSearch.Text);
                    Search();
                    CallbackPanelMain.JSProperties["cpSearch"] = 0;
                }
                else
                {
                    CallbackPanelMain.JSProperties["cpPrintMe"] = 0;
                    CallbackPanelMain.JSProperties["cpPrintURL"] = "";
                }
                break;
        }
    }

    #region Methods
    private void Search()
    {
        CallbackPanelMain.JSProperties["cpMsg"] = "";
        CallbackPanelMain.JSProperties["cpError"] = 0;
        CallbackPanelMain.JSProperties["cpSearch"] =1 ;
        if (string.IsNullOrEmpty(txtMeIdSearch.Text) && string.IsNullOrEmpty(txtFileNo.Text))
        {
            ShowCallBackMessage("لطفا کد عضویت یا شماره پروانه را وارد نمایید");
            return;
        }
        int MemberId;
        if (!int.TryParse(txtMeIdSearch.Text, out MemberId))
        {
            ShowCallBackMessage("کد عضویت را با فرمت صحیح وارد نمایید");
            return;
        }
        string MeId = txtMeIdSearch.Text.Trim();
        string MfNo = txtFileNo.Text.Trim();

        try
        {
            if (!string.IsNullOrEmpty(MeId))
            {
                FillMember(int.Parse(MeId));
            }

            else if (!string.IsNullOrEmpty(MfNo))
            {
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DocMemberFileManager.SelectMainRequestByMfNo(MfNo, 0);
                txtMeIdSearch.Text = MeId = DocMemberFileManager[0]["MeId"].ToString();
                FillMember(int.Parse(MeId));
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            Clear();
            ShowCallBackMessage("اطلاعات وارد شده معتبر نمی باشد");
            return;
        }
    }

    void FillMember(int MeId)
    {
        UserControlMeMembershipLicenceInfo.FillMemberLicence(MeId);
        UserControlMeMembershipInfo.FillMember(MeId);
        UserControlMeDocumentInfo.FillMember(MeId);
        UserControlMeOfficeInfoUserControl.FillInfo(MeId);
        UserControlMeEngOfficeInfoUserControl.FillInfo(MeId);
        UserControlMemberImplementDocInfo.FillInfo(MeId);
        UserControlMemberObservationDocInfo.FillInfo(MeId);
    }

    private void Clear()
    {
        txtMeIdSearch.Text = "";
        txtFileNo.Text = "";
        UserControlMeMembershipLicenceInfo.Clear();
        UserControlMeMembershipInfo.Clear();
        UserControlMeDocumentInfo.Clear();
        UserControlMeOfficeInfoUserControl.Clear();
        UserControlMeEngOfficeInfoUserControl.Clear();
        UserControlMemberImplementDocInfo.Clear();
        UserControlMemberObservationDocInfo.Clear();
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    void ShowCallBackMessage(string Msg)
    {
        CallbackPanelMain.JSProperties["cpMsg"] = Msg;
        CallbackPanelMain.JSProperties["cpError"] = 1;
    }
    #endregion
}