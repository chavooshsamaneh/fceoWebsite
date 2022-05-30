using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Members_Documents_WizardDocOath : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDisable();
        if (!IsPostBack)
        {
            FillMembershipInfo();
            SetHelpAddress();
        }
        if (IsPostBack == false)
        {
            //decimal Cost = TSP.DataManager.AccountingCostSettingsManager.FindCostByTypePersian(TSP.DataManager.CostSettingsSData.MemberFileRegistrationCost);
            //lblDocCost1.Text = "<u><b>پرداخت آنلاین</b></u> مبلغ " + Cost.ToString("#,#") + " ریال به حساب بانک تجارت شعبه نظام مهندسی بابت صدور پروانه اشتغال از طریق همین سایت";
        }

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {

        if (chbIAgree.Checked == false)
        {
            ShowInputError("موارد اعلام شده مورد موافقت قرار نگرفته است");
            return;
        }
        Session["WizardDocOath"] = true;
        Response.Redirect("WizardDocExam.aspx");
 
    }

    protected void btnChangeBaseInfo_Click(object sender, EventArgs e)
    {
        int MeId = Utility.GetCurrentUser_MeId();
        if (!CheckLocker(MeId)) return;

        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        ReqManager.FindByMemberId(MeId, 0, 0);
        if (ReqManager.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
            return;
        }

        Response.Redirect("../MemberInfo/MemberInsertBaseInfo.aspx?PageMode=" + Utility.EncryptQS("ChangeBaseInfo") + "&UrlRef=" + Utility.EncryptQS("MemberHome"));
    }

    protected void btnLicenceRequest_Click(object sender, EventArgs e)
    {
        int MeId = Utility.GetCurrentUser_MeId();
        if (!CheckLocker(MeId)) return;

        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        ReqManager.FindByMemberId(MeId, 0, 0);
        if (ReqManager.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
            return;
        }

        Response.Redirect("../MemberInfo/MemberLicenceRequest.aspx?PageMode=" + Utility.EncryptQS("NewReq") + "&UrlRef=" + Utility.EncryptQS("MemberHome"));
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetDocSessions();
        Response.Redirect("../MemberHome.aspx");
    }

    #endregion

    #region Methods

    private void ResetDocSessions()
    {
        Session["WizardDocOath"] = null;
        Session["WizardDocExam"] = null;
        Session["WizardDocMajor"] = null;
        Session["WizardDocResposblity"] = null;
        Session["WizardDocPeriods"] = null;
        Session["WizardDocJob"] = null;
        Session["WizardDocSummary"] = null;
        Session["JobFileURL"] = null;
        Session["WizardDocJobConfirm"] = null;
        Session["JobGrdURL"] = null;
        Session["ACCFileURL"] = null;
        Session["FishFileURL"] = null;
    }

    private void SetWarningLableDisable()
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    void SetHelpAddress()
    {
        //HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.PortalDocumentMemberFile).ToString());
    }

    private void FillMembershipInfo()
    {
        ObjdsMemberLicence.SelectParameters["MemberId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        int MReId = -2;
        int MeId = Utility.GetCurrentUser_MeId();
        ReqManager.FindByMemberId(MeId, 0, 1, -1);
        if (ReqManager.Count <= 0)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        MReId = Convert.ToInt32(ReqManager[0]["MReId"]);
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
        {
            if (File.Exists(Server.MapPath(MemberManager[0]["ImageUrl"].ToString())))
                imgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(MemberManager[0]["NezamKardanConfirmURL"]))
        {
            if (File.Exists(Server.MapPath(MemberManager[0]["NezamKardanConfirmURL"].ToString())))
                HpKardani.NavigateUrl = MemberManager[0]["NezamKardanConfirmURL"].ToString();
        }
        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
        if (attachManager.Count > 0)
        {
            HpIdNo.NavigateUrl = attachManager[0]["FilePath"].ToString();
        }
        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNoP2);
        if (attachManager.Count > 0)
        {
            HIdNoP2.NavigateUrl = attachManager[0]["FilePath"].ToString();
        }
        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNoPDes);
        if (attachManager.Count > 0)
        {
            HIdNoPDes.NavigateUrl = attachManager[0]["FilePath"].ToString();
        }
        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
        if (attachManager.Count > 0)
        {
            HpSSN.NavigateUrl = attachManager[0]["FilePath"].ToString();
        }
        attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSNBack);
        if (attachManager.Count > 0)
        {
            HssnBack.NavigateUrl = attachManager[0]["FilePath"].ToString();
        }


        if (Convert.ToInt32( MemberManager[0]["SexId"]) == (int)TSP.DataManager.SexManager.Sex.Male)
        {
            lblSoldire.ClientVisible = true;
            HpSoldire.ClientVisible = true;
            lblSoldireBack.ClientVisible = true;
            HpSoldierBack.ClientVisible = true;

            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
            if (attachManager.Count > 0)
            {
                HpSoldire.NavigateUrl = attachManager[0]["FilePath"].ToString();
            }
            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCardBack);
            if (attachManager.Count > 0)
            {
                HpSoldierBack.NavigateUrl = attachManager[0]["FilePath"].ToString();
            }
        }
        else
        {
            lblSoldire.ClientVisible = false;
            HpSoldire.ClientVisible = false;
            lblSoldireBack.ClientVisible = false;
            HpSoldierBack.ClientVisible = false;
        }
        lblMobileNo.Text = MemberManager[0]["MobileNo"].ToString();
        lblAddress.Text = MemberManager[0]["HomeAdr"].ToString();

    }

    void ShowInputError(String Error)
    {
        lblError.Text = "<img src='../Images/edtError.png'/>&nbsp;";
        lblError.Text += Error;
        lblError.Visible = true;
    }

    bool CheckLocker(int MeId)
    {
        string IsMeTemp;
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            IsMeTemp = "1";
        else IsMeTemp = "0";

        if (IsMeTemp == "0")
        {
            TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
            MeManager.FindByCode(MeId);
            if ((bool)MeManager[0]["IsLock"] == true)
            {
                TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 0, 1);
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
                return false;
            }
        }

        return true;
    }  
    #endregion
}
