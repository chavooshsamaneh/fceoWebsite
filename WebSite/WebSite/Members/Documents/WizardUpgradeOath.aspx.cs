using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Members_Documents_WizardUpgradeOath : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDisable();
        if (!IsPostBack)
        {
            FillMembershipInfo();
            HasCivilLicence();
            HasValidObserverWorkReq();
        }

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {

        if (chbIAgree.Checked == false)
        {
            ShowInputError("موارد اعلام شده مورد موافقت قرار نگرفته است");
            return;
        }
    
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        System.Collections.ArrayList Result = DocMemberFileManager.CheckUpgradeConditionsDateAndPeriodsForUpgrade(Utility.GetCurrentUser_MeId(), Utility.GetDateOfToday());
        if (!Convert.ToBoolean(Result[0]))
        {
            ShowMessage(Result[1].ToString());
            return;
        }
        if (string.IsNullOrWhiteSpace(Result[2].ToString()))
        {
            ShowMessage("با توجه به پایه های شما ، پایه های قابل ارتقا در سیستم تعریف نشده است.");
            return;
        }
        Session["WizardDocUpgradeOath"] = true;
        Response.Redirect("WizardUpgradeUploaddocs.aspx");
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
        Response.Redirect("../MemberHome.aspx");
    }

    #endregion

    #region Methods

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


    private void FillMembershipInfo()
    {
        ObjdsMemberLicence.SelectParameters["MemberId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
        int MReId = -2;
        int MeId = Utility.GetCurrentUser_MeId();
        #region Membership
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

        if (Convert.ToInt32(MemberManager[0]["SexId"]) == (int)TSP.DataManager.SexManager.Sex.Male)
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
        #endregion

        #region Document
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
        if (DocMemberFileManager.Count == 1)
        {
            ObjdsMemberFileDetail.SelectParameters["MfId"].DefaultValue = DocMemberFileManager[0]["MfId"].ToString();
            ObjdsMemberFileDetail.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
            ObjdsMemberFileDetail.SelectParameters["InActive"].DefaultValue = "0";
            GridViewResponsibility.DataBind();
        }
        else
        {
            ObjdsMemberFileDetail.SelectParameters["MfId"].DefaultValue = "-2";
            ObjdsMemberFileDetail.SelectParameters["MeId"].DefaultValue = "-2";
        }
        #endregion
        #region Periods
        objdsPeriods.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
        objdsPeriods.SelectParameters["IsMemberPeriod"].DefaultValue = "1";
        objdsPeriods.SelectParameters["RequestDate"].DefaultValue = Utility.GetDateOfToday();

        #endregion

        ObjectDataSourceJobConfirm.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();

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

    private void HasCivilLicence()
    {

        DataTable dtMemberFileMajor;
        var ds = new DataSet();
        var dv = (DataView)ObjdsMemberLicence.Select();
        if (dv != null && dv.Count > 0)
        {
            dtMemberFileMajor = dv.ToTable();
            dtMemberFileMajor.DefaultView.RowFilter = "LicenceCode<>" + (int)TSP.DataManager.Licence.kardani
                 + " and " + "MjParentId=" + (int)TSP.DataManager.MainMajors.Civil;
            if (dtMemberFileMajor.DefaultView.Count > 0)
            {
                Session["WizardUpgradeCivilLicence"] = true;
            }
            else
            {
                Session["WizardUpgradeCivilLicence"] = false;
            }
            dtMemberFileMajor.DefaultView.RowFilter = "";
        }
    }
    private void HasValidObserverWorkReq()
    {
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        if (ObserverWorkRequestChangesManager.HasValidObserverWorkReq(Utility.GetCurrentUser_MeId()))
        {
            ImgHasObserverWorkReq.ClientVisible = lblHasObserverWorkReq.Visible = true;
        }
    }
    //private Boolean CheckCanUpgrad()
    //{
    //    Boolean Result = true;
    //    TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
    //    PeriodRegisterManager.selectPeriodRegisterForMemberUpgrade(Utility.GetCurrentUser_MeId(), -1, -1, -1);
    //    return Result;
    //}

    /// <summary>
    /// پایه 3 به 2 نیاز به 4 سال سابقه کار
    ///پایه 2 به 1 نیاز به 5 سال سابقه کار
    /// </summary>
    /// <returns></returns>
    //private Boolean CheckUpgradeConditionsDate()
    //{
    //    string Msg = "";
    //    Boolean CanUpgrade = false;

    //    TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
    //    TSP.DataManager.DocUpGradePointManager DocUpGradePointManager = new TSP.DataManager.DocUpGradePointManager();
    //    TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
    //    DataTable dtRes = DocMemberFileDetailManager.SelectMeFileMaxGradForAllResponsiblity(Utility.GetCurrentUser_MeId(), -1);
    //    if (dtRes.Rows.Count == 0)
    //    {
    //        ShowInputError("پایه های اخذ شده توسط شما در سیستم تعریف نشده است. لطفا به واحد پروانه مراجعه نمایید");
    //        return false;

    //    }
    //    int MjId = -2;
    //    string ResDate = "عضو محترم ";
    //    for (int i = 0; i < dtRes.Rows.Count; i++)
    //    {
    //        if (CanUpgrade)
    //            break;
    //        MjId = Convert.ToInt32(dtRes.Rows[i]["FMjId"]);
    //        ResDate = dtRes.Rows[i]["Date"].ToString();
    //        //Utility.Date objDate = new Utility.Date(ResDate);
    //        Utility.Date objDate = new Utility.Date(Utility.GetDateOfToday());
    //        switch (Convert.ToInt32(dtRes.Rows[i]["GrdId"]))
    //        {
    //            case (int)TSP.DataManager.DocumentGrads.Grade1:
    //                CanUpgrade = false;
    //                Msg += "شما دارای پایه یک " + dtRes.Rows[i]["ResName"].ToString() + " می باشید و قادر به ثبت درخواست ارتقاء پایه نمی باشید.";
    //                break;
    //            case (int)TSP.DataManager.DocumentGrads.Grade2:
    //                CanUpgrade = true;
    //                if (string.Compare(objDate.AddMonths(-60), ResDate) <= 0)
    //                {
    //                    Msg += "با توجه به عدم گذشت 5 سال از پایه 2 " + dtRes.Rows[i]["ResName"].ToString() + " شما مجوز ثبت درخواست ارتقاء پایه ندارید";
    //                    CanUpgrade = false;
    //                }
    //                break;
    //            case (int)TSP.DataManager.DocumentGrads.Grade3:
    //                CanUpgrade = true;
    //                //if (string.Compare(Utility.GetDateOfToday(), objDate.AddMonths(-48)) <= 0)
    //                if (string.Compare(objDate.AddMonths(-48), ResDate) <= 0)
    //                {
    //                    Msg += "با توجه به عدم گذشت 4 سال از پایه 3" + dtRes.Rows[i]["ResName"].ToString() + " شما مجوز ثبت درخواست ارتقاء پایه ندارید";
    //                    CanUpgrade = false;
    //                }
    //                break;
    //        }
    //    }
    //    if (!CanUpgrade)
    //        ShowMessage(Msg);
    //    return CanUpgrade;
    //}


    //private Boolean CheckUpgradePeriod()
    //{
    //    string Msg = "عضو محترم ";
    //    Boolean CanUpgrade = false;

    //    TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
    //    TSP.DataManager.DocUpGradePointManager DocUpGradePointManager = new TSP.DataManager.DocUpGradePointManager();
    //    TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
    //    DataTable dtRes = DocMemberFileDetailManager.SelectMeFileMaxGradForAllResponsiblity(Utility.GetCurrentUser_MeId(), -1);
    //    if (dtRes.Rows.Count == 0)
    //    {
    //        ShowInputError("پایه های اخذ شده توسط شما در سیستم تعریف نشده است. لطفا به واحد پروانه مراجعه نمایید");
    //        return false;

    //    }
    //    for (int i = 0; i < dtRes.Rows.Count; i++)
    //    {
    //        DataTable dtUpgradePoint = DocUpGradePointManager.SelectActiveUpgradePoint(Convert.ToInt32(dtRes.Rows[i]["GrdId"]), Convert.ToInt32(dtRes.Rows[i]["MjParentId"]), Convert.ToInt32(dtRes.Rows[i]["ResId"]));
    //        if (dtUpgradePoint.Rows.Count > 0)
    //        {
    //            int MinPeriodNeed = Convert.ToInt32(dtUpgradePoint.Rows[0]["MinPeriodNeed"]);
    //            DataTable dtPeriodRegister = PeriodRegisterManager.selectPeriodRegisterForMemberReport(Utility.GetCurrentUser_MeId(), Convert.ToInt32(dtRes.Rows[i]["MjParentId"]), Convert.ToInt32(dtRes.Rows[i]["ResId"]), Convert.ToInt32(dtRes.Rows[i]["GrdId"]));
    //            if (dtPeriodRegister.Rows.Count < MinPeriodNeed)
    //            {
    //                Msg += "جهت ارتقا " + dtRes.Rows[i]["GrdName"].ToString() + " " + dtRes.Rows[i]["ResName"].ToString() + " گذراندن حداقل " + MinPeriodNeed.ToString() + " دوره آموزشی الزامی می باشد.جهت مشاهده لیست  دوره های مورد نیاز جهت ارتقاء پایه به لینک مربوطه در همین صفحه مراجعه نمایید ";
    //                CanUpgrade = false;
    //            }
    //            else
    //            {
    //                CanUpgrade = true;
    //                break;
    //            }
    //        }
    //    }
    //    if (!CanUpgrade)
    //        ShowMessage(Msg);
    //    return CanUpgrade;
    //}


    ///// <summary>
    ///// پایه 3 به 2 نیاز به 4 سال سابقه کار
    /////پایه 2 به 1 نیاز به 5 سال سابقه کار
    ///// </summary>
    ///// <returns></returns>
    //private Boolean CheckUpgradeConditionsDateAndPeriods()
    //{       
    //    string Msg = "عضو محترم ";
    //    Boolean CanUpgrade = false;

    //    TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
    //    TSP.DataManager.DocUpGradePointManager DocUpGradePointManager = new TSP.DataManager.DocUpGradePointManager();
    //    TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
    //    DataTable dtRes = DocMemberFileDetailManager.SelectMeFileMaxGradForAllResponsiblity(Utility.GetCurrentUser_MeId(), -1);
    //    if (dtRes.Rows.Count == 0)
    //    {
    //        ShowInputError("پایه های اخذ شده توسط شما در سیستم تعریف نشده است. لطفا به واحد پروانه مراجعه نمایید");
    //        return false;

    //    }
    //    int MjId = -2;
    //    string ResDate = "";
    //    for (int i = 0; i < dtRes.Rows.Count; i++)
    //    {
    //        if (CanUpgrade)
    //            break;
    //        MjId = Convert.ToInt32(dtRes.Rows[i]["FMjId"]);
    //        ResDate = dtRes.Rows[i]["Date"].ToString();
    //        Utility.Date objDate = new Utility.Date(Utility.GetDateOfToday());
    //        switch (Convert.ToInt32(dtRes.Rows[i]["GrdId"]))
    //        {
    //            case (int)TSP.DataManager.DocumentGrads.Grade1:
    //                CanUpgrade = false;
    //                Msg += "شما دارای پایه یک " + dtRes.Rows[i]["ResName"].ToString() + " می باشید و قادر به ثبت درخواست ارتقاء پایه نمی باشید.";
    //                break;
    //            case (int)TSP.DataManager.DocumentGrads.Grade2:
    //                CanUpgrade = true;
    //                if (string.Compare(objDate.AddMonths(-60), ResDate) <= 0)
    //                {
    //                    Msg += "با توجه به عدم گذشت 5 سال از پایه 2 " + dtRes.Rows[i]["ResName"].ToString() + " شما مجوز ثبت درخواست ارتقاء پایه ندارید";
    //                    CanUpgrade = false;
    //                }

    //                break;
    //            case (int)TSP.DataManager.DocumentGrads.Grade3:
    //                CanUpgrade = true;
    //                if (string.Compare(objDate.AddMonths(-48), ResDate) <= 0)
    //                {
    //                    Msg += "با توجه به عدم گذشت 4 سال از پایه 3" + dtRes.Rows[i]["ResName"].ToString() + " شما مجوز ثبت درخواست ارتقاء پایه ندارید";
    //                    CanUpgrade = false;
    //                }
    //                break;
    //        }
    //        if (CanUpgrade)
    //        {
    //            #region CheckPeriods
    //            DataTable dtUpgradePoint = DocUpGradePointManager.SelectActiveUpgradePoint(Convert.ToInt32(dtRes.Rows[i]["GrdId"]), Convert.ToInt32(dtRes.Rows[i]["MjParentId"]), Convert.ToInt32(dtRes.Rows[i]["ResId"]));
    //            if (dtUpgradePoint.Rows.Count > 0)
    //            {
    //                int MinPeriodNeed = Convert.ToInt32(dtUpgradePoint.Rows[0]["MinPeriodNeed"]);
    //                DataTable dtPeriodRegister = PeriodRegisterManager.selectPeriodRegisterForMemberReport(Utility.GetCurrentUser_MeId(), Convert.ToInt32(dtRes.Rows[i]["MjParentId"]), Convert.ToInt32(dtRes.Rows[i]["ResId"]), Convert.ToInt32(dtRes.Rows[i]["GrdId"]));
    //                if (dtPeriodRegister.Rows.Count < MinPeriodNeed)
    //                {
    //                    Msg += "جهت ارتقا " + dtRes.Rows[i]["GrdName"].ToString() + " " + dtRes.Rows[i]["ResName"].ToString() + " گذراندن حداقل " + MinPeriodNeed.ToString() + " دوره آموزشی الزامی می باشد.جهت مشاهده لیست  دوره های مورد نیاز جهت ارتقاء پایه به لینک مربوطه در همین صفحه مراجعه نمایید ";
    //                    CanUpgrade = false;
    //                }
    //                else
    //                {
    //                    CanUpgrade = true;
    //                }
    //            }
    //            #endregion
    //        }

    //    }
    //    if (!CanUpgrade)
    //        ShowMessage(Msg);
    //    return CanUpgrade;
    //}

    #endregion
}