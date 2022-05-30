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
using DevExpress.Web;
using System.IO;

public partial class Employee_TechnicalServices_Project_ImplementerAgentInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.ClientScript.RegisterStartupScript(GetType(), "key1", "<script> but.DoClick();</script>");
        Page.ClientScript.RegisterStartupScript(GetType(), "key2", "<script> ibut.DoClick();</script>");

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        txtFileNo.Attributes["onkeyup"] = "return ltr_override(event)";
        txtFileNoDate.Attributes["onkeyup"] = "return ltr_override(event)";

        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }

        if (!IsPostBack)
        {
            Session["AttachJobName"] = null;
            Session["AttachJob"] = null;

            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Project.aspx");
            }

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ImplementerAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;

            if (Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName] != null)
            {
                //String QueryValue = Utility.DecryptQS(Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName]);
                String QueryValue = Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName];
                if (TSP.DataManager.Automation.AttachPageToLetter.CheckPageParameterValue(QueryValue) == false)
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode2"])) || (string.IsNullOrEmpty(Request.QueryString["PrjImpId"])) || (string.IsNullOrEmpty(Request.QueryString["Mode"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode2"].ToString())) != "New"))
            {
                Response.Redirect("Implementer.aspx?ProjectId=" + Server.HtmlDecode(Request.QueryString["ProjectId"]) + "&PageMode=" + Request.QueryString["PageMode"] + "&PrjReId=" + Server.HtmlDecode(Request.QueryString["PrjReId"]));
                return;
            }

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnLetter"] = btnShowPpcAttachPageToAutomationLetter.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnLetter"] != null)
            this.btnShowPpcAttachPageToAutomationLetter.Enabled = this.btnShowPpcAttachPageToAutomationLetter2.Enabled = (bool)this.ViewState["BtnLetter"];
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Insert();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect("ImplementerAgent.aspx?ProjectId=" + HDProjectId.Value
            + "&PageMode=" + Request.QueryString["PageMode"]
            + "&PrjReId=" + RequestId.Value + "&Mode=" + HDMode.Value
            + "&PrjImpId=" + HDImpId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
    }

    /***************************************************************************************************************************************************************/
    private void SetKeys()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode2"].ToString());
            HDProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"]).ToString();
            HDImpId.Value = Server.HtmlDecode(Request.QueryString["PrjImpId"]).ToString();
            RequestId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();
            HDImpAgentId.Value = Server.HtmlDecode(Request.QueryString["ImpAgentId"]).ToString();
            HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"]).ToString();

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string ProjectId = Utility.DecryptQS(HDProjectId.Value);
            string ImpId = Utility.DecryptQS(HDImpId.Value);
            string PrjReId = Utility.DecryptQS(RequestId.Value);
            string ImpAgentId = Utility.DecryptQS(HDImpAgentId.Value);
            string Mode = Utility.DecryptQS(HDMode.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(Mode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            FillProjectInfo(int.Parse(PrjReId));
            FillImplementerInfo();
            SetMode();
            CheckWorkFlowPermission();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    private void SetMode()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        switch (PageMode)
        {
            case "View":
                SetViewMode();
                break;

            case "New":
                SetNewMode();
                break;
        }
    }

    private void SetViewMode()
    {
        string ImpAgentId = Utility.DecryptQS(HDImpAgentId.Value);

        if (string.IsNullOrEmpty(ImpAgentId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        CheckAccess();

        btnShowPpcAttachPageToAutomationLetter.Enabled = true;
        btnShowPpcAttachPageToAutomationLetter2.Enabled = true;

        Disable();

        FillForm(int.Parse(ImpAgentId));

        ASPxRoundPanel2.HeaderText = "مشاهده";
    }

    private void SetNewMode()
    {
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        CheckAccess();

        btnShowPpcAttachPageToAutomationLetter.Enabled = false;
        btnShowPpcAttachPageToAutomationLetter2.Enabled = false;

        Enable();
        ClearForm();

        ASPxRoundPanel2.HeaderText = "جدید";

    }

    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ImplementerAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        if (Utility.DecryptQS(PgMode.Value) == "New" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;
        }
        if (Utility.DecryptQS(PgMode.Value) == "Edit" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    /***************************************************************************************************************************************************************/
    private void Disable()
    {
        ComboType.Enabled = false;
        txtFatherName.Enabled = false;
        txtFileNo.Enabled = false;
        txtFileNoDate.Enabled = false;
        txtFileNoDate.Enabled = false;
        txtFirstName.Enabled = false;
        txtLastName.Enabled = false;
        txtMeNo.Enabled = false;
        txtSSN.Enabled = false;
        txtImpRes.Enabled = false;
    }

    private void Enable()
    {
        ComboType.Enabled = true;
        txtFatherName.Enabled = true;
        txtFileNo.Enabled = true;
        txtFileNoDate.Enabled = true;
        txtFileNoDate.Enabled = true;
        txtFirstName.Enabled = true;
        txtLastName.Enabled = true;
        txtMeNo.Enabled = true;
        txtSSN.Enabled = true;
        txtImpRes.Enabled = true;
    }

    private void ClearForm()
    {
        txtFatherName.Text = "";
        txtFileNo.Text = "";
        txtFileNoDate.Text = "";
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtMeNo.Text = "";
        txtSSN.Text = "";

    }

    private void FillForm(int ImpAgentId)
    {
        TSP.DataManager.TechnicalServices.ImplementerAgentManager ImpAgentManager = new TSP.DataManager.TechnicalServices.ImplementerAgentManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();

        try
        {
            ImpAgentManager.FindByImplementerAgentId(ImpAgentId);
            if (ImpAgentManager.Count > 0)
            {
                int TypeValue = int.Parse(ImpAgentManager[0]["MemberTypeId"].ToString());
                if (TypeValue == (int)TSP.DataManager.TSMemberType.Member)
                {
                    SetMember();
                }
                else if (TypeValue == (int)TSP.DataManager.TSMemberType.OtherPerson)
                {
                    SetKardan();

                    AttachManager.FindByTableTypeId(ImpAgentId, (int)TSP.DataManager.TableCodes.TSImplementerAgent, (int)TSP.DataManager.TSAttachType.ExperimentInformation);
                    if (AttachManager.Count > 0)
                    {
                        Hp_Job.ClientVisible = true;
                        Hp_Job.NavigateUrl = AttachManager[0]["FilePath"].ToString();
                        HD_Job["name"] = 1;
                    }

                }
                else if (TypeValue == (int)TSP.DataManager.TSMemberType.ExpArchitect)
                {
                    SetMemar();

                    AttachManager.FindByTableTypeId(ImpAgentId, (int)TSP.DataManager.TableCodes.TSImplementerAgent, (int)TSP.DataManager.TSAttachType.ExperimentInformation);
                    if (AttachManager.Count > 0)
                    {
                        Hp_Job.ClientVisible = true;
                        Hp_Job.NavigateUrl = AttachManager[0]["FilePath"].ToString();
                        HD_Job["name"] = 1;
                    }
                }

                txtFatherName.Text = ImpAgentManager[0]["FatherName"].ToString();
                txtFileNo.Text = ImpAgentManager[0]["No"].ToString();
                txtFileNoDate.Text = ImpAgentManager[0]["NoDate"].ToString();
                txtFirstName.Text = ImpAgentManager[0]["FirstName"].ToString();
                txtLastName.Text = ImpAgentManager[0]["LastName"].ToString();
                //txtMeNo.Text = ImpAgentManager[0]["MeOPersonId"].ToString();
                txtMeNo.Text = ImpAgentManager[0]["Code"].ToString();
                txtSSN.Text = ImpAgentManager[0]["SSN"].ToString();
            }
            else
            {
                SetLabelWarning("امکان مشاهده اطلاعات وجود ندارد.اطلاعات توسط کاربر دیگری تغییر یافته است");
            }
        }
        catch (Exception)
        {
            SetLabelWarning("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
    }

    private void FillImplementerInfo()
    {
        //if (Mode == "Project")
        //{
        string PrjImpId = Utility.DecryptQS(HDImpId.Value);
        if (string.IsNullOrEmpty(PrjImpId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImpManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        ProjectImpManager.FindByPrjImpId(int.Parse(PrjImpId));
        if (ProjectImpManager.Count > 0)
        {
            ASPxRoundPanelImp.Visible = true;
            txtImpName.Text = ProjectImpManager[0]["Name"].ToString();
            txtImpId.Text = ProjectImpManager[0]["MeOfficeId"].ToString();
            txtImpType.Text = ProjectImpManager[0]["MemberTypeTitle"].ToString();
        }
        //}

    }

    private void SetMember()
    {
        ComboType.SelectedIndex = 0;
        ASPxLabelJob.ClientVisible = false;
        //Page.ClientScript.RegisterStartupScript(GetType(), "key2", "<script> ibut.DoClick();</script>");
        flp_Job.ClientVisible = false;
    }

    private void SetKardan()
    {
        ComboType.SelectedIndex = 1;
        ASPxLabelJob.ClientVisible = true;
        flp_Job.ClientVisible = true;

    }

    private void SetMemar()
    {
        ComboType.SelectedIndex = 2;
        ASPxLabelJob.ClientVisible = true;
        flp_Job.ClientVisible = true;
    }

    private void SetError(Exception err, char Flag)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                if (Flag == 'D')
                {
                    SetLabelWarning("به علت وجود اطلاعات وابسته امکان حذف نمی باشد");
                }
                else
                {
                    SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
                }
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    /***************************************************************************************************************************************************************/
    private void Insert()
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.ImplementerAgentManager ImpAgentManager = new TSP.DataManager.TechnicalServices.ImplementerAgentManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();

        trans.Add(ImpAgentManager);
        trans.Add(AttachManager);

        TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();

        bool IsAttachJob = false;

        try
        {
            if (ComboType.Value == null)
            {
                SetLabelWarning("نوع نماینده مجری را انتخاب نمایید");
                return;
            }
            if (string.IsNullOrEmpty(txtMeNo.Text))
            {
                SetLabelWarning("کد عضویت را وارد نمایید");
                return;
            }

            int Id = int.Parse(txtMeNo.Text);
            int OtpId = -1;

            if (ComboType.Value.ToString() == "1") //kardan            
                OthpManager.FindKardanAndMemarByOtpCode(Id.ToString(), (int)TSP.DataManager.OtherPersonType.Kardan);
            else if (ComboType.Value.ToString() == "2")//Memar            
                OthpManager.FindKardanAndMemarByOtpCode(Id.ToString(), (int)TSP.DataManager.OtherPersonType.Memar);

            if (OthpManager.Count > 0)
                OtpId = Convert.ToInt32(OthpManager[0]["OtpId"]);

            if (!CheckConditions())
                return;

            DataRow dr = ImpAgentManager.NewRow();
            dr["PrjImpId"] = Utility.DecryptQS(HDImpId.Value);
            dr["PrjReId"] = Utility.DecryptQS(RequestId.Value);

            if (ComboType.Value.ToString() == "0")//Member
            {
                dr["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.Member;
                dr["MeOPersonId"] = Id;

            }
            if (ComboType.Value.ToString() == "1")//Kardan
            {
                dr["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.OtherPerson;
                dr["MeOPersonId"] = OtpId;

            }
            else if (ComboType.Value.ToString() == "2")//Memar
            {
                dr["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.ExpArchitect;
                dr["MeOPersonId"] = OtpId;
            }
            if (!string.IsNullOrEmpty(txtFileNo.Text))
                dr["JobLicenseNo"] = txtFileNo.Text;
            else
                dr["JobLicenseNo"] = DBNull.Value;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            ImpAgentManager.AddRow(dr);
            trans.BeginSave();
            int cnt = ImpAgentManager.Save();
            if (cnt > 0)
            {
                if (Session["AttachJob"] != null && Session["AttachJobName"] != null)
                {
                    DataRow drAtt = AttachManager.NewRow();
                    drAtt["TableTypeId"] = ImpAgentManager[0]["ImplementerAgentId"];
                    drAtt["TableType"] = (int)TSP.DataManager.TableCodes.TSImplementerAgent;
                    drAtt["AttachTypeId"] = (int)TSP.DataManager.TSAttachType.ExperimentInformation;
                    drAtt["FilePath"] = "~/Image/TechnicalServices/ImplementerAgent/" + Path.GetFileName(Session["AttachJob"].ToString());
                    drAtt["FileName"] = Session["AttachJobName"];
                    drAtt["UserId"] = Utility.GetCurrentUser_UserId();
                    drAtt["ModifiedDate"] = DateTime.Now;
                    AttachManager.AddRow(drAtt);
                    if (AttachManager.Save() > 0)
                    {
                        AttachManager.DataTable.AcceptChanges();
                        IsAttachJob = true;
                    }
                }


                trans.EndSave();

                HDImpAgentId.Value = Utility.EncryptQS(ImpAgentManager[0]["ImplementerAgentId"].ToString());
                PgMode.Value = Utility.EncryptQS("View");

                SetLabelWarning("ذخیره انجام شد");

                SetViewMode();


            }
            else
            {
                trans.CancelSave();
                SetLabelWarning("خطایی در ذخیره اطلاعات انجام گرفته است");
            }


        }
        catch (Exception err)
        {
            trans.CancelSave();
            SetError(err, 'I');
        }
        if (ComboType.Value.ToString() == "0")
            SetMember();
        else if (ComboType.Value.ToString() == "1")
            SetKardan();
        else
            SetMemar();

        if (IsAttachJob)
        {
            try
            {
                string ImgSoource = Session["AttachJob"].ToString();
                string ImgTarget = Server.MapPath("~/Image/TechnicalServices/ImplementerAgent/") + Path.GetFileName(Session["AttachJob"].ToString());
                File.Copy(ImgSoource, ImgTarget, true);
                Hp_Job.ClientVisible = true;
                Hp_Job.NavigateUrl = "~/Image/TechnicalServices/ImplementerAgent/" + Path.GetFileName(Session["AttachJob"].ToString());

                Session["AttachJob"] = null;
                Session["AttachJobName"] = null;

            }
            catch (Exception)
            {
            }
        }
    }

    private string FindLockers(int Id, int MemberTypeId, int IsLock)
    {
        TSP.DataManager.LockHistoryManager LockHistoryManager = new TSP.DataManager.LockHistoryManager();
        return LockHistoryManager.FindLockers(Id, MemberTypeId, IsLock);

    }

    /***************************************************************************************************************************************************************/
    private bool CheckConditions()
    {
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.ControlAndEvaluation.ProjectIngridientPerformanceManager ProjectIngridientPerformanceManager = new TSP.DataManager.ControlAndEvaluation.ProjectIngridientPerformanceManager();

        int Id = int.Parse(txtMeNo.Text);
        int OtpId = -1;

        switch (ComboType.Value.ToString())
        {
            case "0": //Member
                MeManager.FindByCode(Id);

                if (!CheckLock(Id, Convert.ToBoolean(MeManager[0]["IsLock"]), (int)TSP.DataManager.LockMemberType.Member))
                    return false;

                if (!CheckFileNo(Id))
                    return false;

                if (!CheckGrade(Id, (int)TSP.DataManager.TSMemberType.Member))
                    return false;

                if (!CheckAgent(Id, (int)TSP.DataManager.TSMemberType.Member))
                    return false;

                if (!CheckOffice(Id, 2))
                    return false;

                if (!CheckEngOffice(Id))
                    return false;

                if (!CheckIfIsImplementer(Id, (int)TSP.DataManager.TSMemberType.Member))
                    return false;

                if (!CheckIfIsObserver(Id, (int)TSP.DataManager.TSMemberType.Member))
                    return false;

                if (!CheckIfIsDesigner(Id, (int)TSP.DataManager.TSMemberType.Member))
                    return false;

                break;

            case "1": //kardan
                OthpManager.FindKardanAndMemarByOtpCode(Id.ToString(), (int)TSP.DataManager.OtherPersonType.Kardan);
                if (OthpManager.Count > 0)
                    OtpId = Convert.ToInt32(OthpManager[0]["OtpId"]);

                if (!CheckLock(OtpId, Convert.ToBoolean(OthpManager[0]["IsLock"]), (int)TSP.DataManager.LockMemberType.Kardan))
                    return false;

                if (OthpManager.Count > 0)
                    if (!CheckFileNoForOthPerson(OthpManager[0]))
                        return false;

                if (!CheckGrade(OtpId, (int)TSP.DataManager.TSMemberType.OtherPerson))
                    return false;

                if (!CheckStageLimitation(OtpId, (int)TSP.DataManager.TSMemberType.OtherPerson))
                    return false;

                if (!CheckAgent(OtpId, (int)TSP.DataManager.TSMemberType.OtherPerson))
                    return false;

                if (!CheckOffice(OtpId, 1))
                    return false;

                if (!CheckEngOffice(OtpId))
                    return false;

                if (!CheckIfIsImplementer(OtpId, (int)TSP.DataManager.TSMemberType.OtherPerson))
                    return false;

                if (!CheckIfIsObserver(OtpId, (int)TSP.DataManager.TSMemberType.OtherPerson))
                    return false;

                if (!CheckIfIsDesigner(OtpId, (int)TSP.DataManager.TSMemberType.OtherPerson))
                    return false;

                break;

            case "2": //Memar
                OthpManager.FindKardanAndMemarByOtpCode(Id.ToString(), (int)TSP.DataManager.OtherPersonType.Memar);
                if (OthpManager.Count > 0)
                    OtpId = Convert.ToInt32(OthpManager[0]["OtpId"]);

                if (!CheckLock(OtpId, Convert.ToBoolean(OthpManager[0]["IsLock"]), (int)TSP.DataManager.LockMemberType.Memar))
                    return false;

                if (OthpManager.Count > 0)
                    if (!CheckFileNoForOthPerson(OthpManager[0]))
                        return false;

                if (!CheckGrade(OtpId, (int)TSP.DataManager.TSMemberType.ExpArchitect))
                    return false;

                if (!CheckStageLimitation(OtpId, (int)TSP.DataManager.TSMemberType.ExpArchitect))
                    return false;

                if (!CheckAgent(OtpId, (int)TSP.DataManager.TSMemberType.ExpArchitect))
                    return false;

                if (!CheckOffice(OtpId, 1))
                    return false;

                if (!CheckEngOffice(OtpId))
                    return false;

                if (!CheckIfIsImplementer(OtpId, (int)TSP.DataManager.TSMemberType.OtherPerson))
                    return false;

                if (!CheckIfIsObserver(OtpId, (int)TSP.DataManager.TSMemberType.OtherPerson))
                    return false;

                if (!CheckIfIsDesigner(OtpId, (int)TSP.DataManager.TSMemberType.OtherPerson))
                    return false;

                break;
        }
        return true;
    }

    private bool CheckLock(int Id, bool IsLock, int LockMemberType)
    {
        if (IsLock)
        {
            string LockName = FindLockers(Id, LockMemberType, 1);

            SetLabelWarning("امکان ثبت عضو مورد نظر وجود ندارد.عضو مورد نظر توسط " + LockName + " قفل می باشد ");
            return false;
        }
        return true;
    }

    private bool CheckFileNo(int Id)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.ClearBeforeFill = true;

        DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(Id, 0);
        if (dtMeFile.Rows.Count > 0)
        {
            int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());

            if (Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) || dtMeFile.Rows[0]["IsConfirm"].ToString() != "1")
            {
                SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان نماینده مجری وجود ندارد.وضعیت پروانه اشتغال عضو نا مشخص می باشد.");
                return false;
            }
        }
        else
        {
            SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان نماینده مجری وجود ندارد.عضو انتخاب شده دارای پروانه اشتغال به کار نمی باشد.");
            return false;
        }
        return true;
    }

    private bool CheckFileNoForOthPerson(DataRow OthpManager)
    {
        if (Utility.IsDBNullOrNullValue(OthpManager["FileNo"]))
        {
            SetLabelWarning("امکان ثبت شخص مورد نظر وجود ندارد.شخص دارای پروانه اشتغال نمی باشد");
            return false;
        }
        return true;
    }

    private bool CheckStageLimitation(int OtpId, int TSMemberType)
    {
        TSP.DataManager.DocOffImpAgentStageLimitationManager StageLimitationManager = new TSP.DataManager.DocOffImpAgentStageLimitationManager();
        TSP.DataManager.DocOffMemberAcceptedGradeManager MemberGradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();

        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));

        //int GrdId = MemberGradeManager.GetMaxGradeId(OtpId);
        int GrdId = GetGrade(OtpId, TSMemberType);
        if (GrdId != 0)
        {
            int ProjectStage = BlockManager.GetMaxStageNum(ProjectId);
            if (ProjectStage != 0)
            {
                int Stage = StageLimitationManager.FindStageByMemberTypeIdGrdId(TSMemberType, GrdId);
                if (ProjectStage >= Stage)
                {
                    SetLabelWarning("امکان ثبت شخص مورد نظر وجود ندارد.تعداد طبقات پروژه از حداکثر تعداد طبقات مجاز برای این شخص بیشر می باشد");
                    return false;
                }
            }
            else
            {
                SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است");
                return false;
            }
        }
        else
        {
            SetLabelWarning("امکان ثبت شخص مورد نظر وجود ندارد.شخص دارای پروانه اشتغال نمی باشد");
            return false;
        }
        return true;
    }

    private bool CheckAgent(int ID, int MemberType)
    {
        TSP.DataManager.TechnicalServices.ImplementerAgentManager ImpAgentManager = new TSP.DataManager.TechnicalServices.ImplementerAgentManager();

        ImpAgentManager.FindNotEndByMemberIdTypeId(ID, MemberType);
        for (int i = 0; i < ImpAgentManager.Count; i++)
        {
            if (ChbCheck.Checked = false && Convert.ToInt32(ImpAgentManager[i]["InActive"]) == 0)
            {
                SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان نماینده مجری وجود ندارد.این عضو نماینده مجری پروژه دیگری می باشد.");
                return false;
            }
        }
        return true;
    }

    private bool CheckIfIsImplementer(int Id, int MemberType)
    {
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));

        TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImpManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        ProjectImpManager.FindNotEndByMemberIdTypeId(Id, MemberType);
        for (int i = 0; i < ProjectImpManager.Count; i++)
        {
            if (Convert.ToInt32(ProjectImpManager[i]["ProjectId"]) != ProjectId && Convert.ToInt32(ProjectImpManager[i]["InActive"]) == 0)
            {
                SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان نماینده مجری وجود ندارد.این شخص مجری پروژه دیگری می باشد.");
                return false;
            }
        }
        return true;
    }

    private bool CheckIfIsObserver(int Id, int MemberType)
    {
        TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObsManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        ProjectObsManager.FindNotEndByMemberIdTypeId(Id, MemberType);
        for (int i = 0; i < ProjectObsManager.Count; i++)
        {
            if (Convert.ToInt32(ProjectObsManager[i]["InActive"]) == 0)
            {
                SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان نماینده مجری وجود ندارد.این شخص ناظر پروژه می باشد.");
                return false;
            }
        }
        return true;
    }

    private bool CheckIfIsDesigner(int Id, int MemberType)
    {
        TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        ProjectDesignerManager.FindNotEndByMemberIdTypeId(Id, MemberType);
        for (int i = 0; i < ProjectDesignerManager.Count; i++)
        {
            if (Convert.ToInt32(ProjectDesignerManager[i]["InActive"]) == 0)
            {
                SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان نماینده مجری وجود ندارد.این شخص طراح پروژه می باشد.");
                return false;
            }
        }
        return true;
    }

    private bool CheckOffice(int MeId, int PersonType)
    {
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));

        TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImpManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();

        ProjectImpManager.FindByProjectId(ProjectId);

        OfMeManager.FindOffMemberByPersonId(MeId, PersonType);
        if (OfMeManager.Count > 0)
        {
            bool Flag = false;
            int OfficeId = Convert.ToInt32(OfMeManager[0]["OfId"]);
            for (int i = 0; i < ProjectImpManager.Count; i++)
                if (Convert.ToInt32(ProjectImpManager[i]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.Office && OfficeId == Convert.ToInt32(ProjectImpManager[i]["MeOfficeId"]))
                    Flag = true;

            if (!Flag)
            {
                SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان نماینده مجری وجود ندارد.این شخص عضو یک شرکت می باشد.");
                return false;
            }
        }
        return true;
    }

    private bool CheckEngOffice(int MeId)
    {
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();

        OfMeManager.FindEngOfficeMemberByPersonId(MeId);
        if (OfMeManager.Count > 0)
        {
            SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان نماینده مجری وجود ندارد.این شخص عضو یک دفتر می باشد.");
            return false;
        }

        return true;
    }

    private bool CheckGrade(int Id, int MemberType)
    {
        int Grade = GetGrade(Id, MemberType);
        if (Grade == 0)
        {
            SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان نماینده مجری وجود ندارد.این شخص صلاحیت اجرا ندارد.");
            return false;
        }
        return true;
    }

    private int GetGrade(int Id, int MemberType)
    {
        int Grade = 0;
        Capacity Cpty = new Capacity();

        switch (MemberType)
        {
            case (int)TSP.DataManager.TSMemberType.Member:
                Grade = Cpty.GetMemGrade(Id, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                break;

            case (int)TSP.DataManager.TSMemberType.OtherPerson:
            case (int)TSP.DataManager.TSMemberType.ExpArchitect:
                //???????????????
                //Grade = Cpty.GetTechniciansGrade(Id, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                //???????????????
                break;
        }
        return Grade;
    }

    /***************************************************************************************************************************************************************/
    protected void flp_Job_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageJob(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected string SaveImageJob(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                Session["AttachJobName"] = Path.GetFileName(uploadedFile.PostedFile.FileName);

                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/TechnicalServices/ImplementerAgent/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["AttachJob"] = tempFileName;
            //Session["FileOfArm2"] = ret;

        }
        return ret;
    }

    /***************************************************************************************************************************************************************/
    protected void txtMeNo_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.TechnicalServices.ImplementerAgentManager ImpAgentManager = new TSP.DataManager.TechnicalServices.ImplementerAgentManager();
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();

        int ID = -1;
        if (!string.IsNullOrEmpty(txtMeNo.Text))
        {
            ID = int.Parse(txtMeNo.Text);

            if (ComboType.Value != null)
            {
                string TypeValue = ComboType.Value.ToString();

                switch (TypeValue)
                {
                    case "0"://Member

                        MeManager.FindByCode(ID);
                        if (MeManager.Count == 1)
                        {
                            if (Convert.ToInt32(MeManager[0]["MrsId"]) != 1)
                            {
                                ClearForm();
                                SetMember();
                                SetLabelWarning("وضعیت عضو مورد نظر تایید شده نمی باشد");
                                return;
                            }
                            if (Convert.ToBoolean(MeManager[0]["InActive"]))
                            {
                                ClearForm();
                                SetMember();
                                SetLabelWarning("عضو مورد نظر غیر فعال می باشد");
                                return;
                            }
                            txtFatherName.Text = MeManager[0]["FatherName"].ToString();
                            txtFirstName.Text = MeManager[0]["FirstName"].ToString();
                            txtLastName.Text = MeManager[0]["LastName"].ToString();
                            txtSSN.Text = MeManager[0]["SSN"].ToString();
                            txtFileNoDate.Text = MeManager[0]["FileDate"].ToString();
                            txtFileNo.Text = MeManager[0]["FileNo"].ToString();
                            txtImpRes.Text = MeManager[0]["ImpGrdName"].ToString();
                            SetMember();

                            ImpAgentManager.FindByMemberIdTypeId(ID, (int)TSP.DataManager.TSMemberType.Member);
                            if (ImpAgentManager.Count > 0)
                                ChbCheck.Visible = true;
                            else
                                ChbCheck.Visible = false;
                        }
                        else
                        {
                            ClearForm();
                            SetMember();
                            SetLabelWarning("چنین کد عضویتی وجود ندارد.مجدداً وارد نمایید");
                            return;
                        }
                        break;

                    case "1"://Kardan

                        OthpManager.FindKardanAndMemarByOtpCode(ID.ToString(), (int)TSP.DataManager.OtherPersonType.Kardan);
                        //OthpManager.SelectOtherPersonKardanAndMemar(ID);
                        if (OthpManager.Count == 1)
                        {
                            int OtpId = Convert.ToInt32(OthpManager[0]["OtpId"]);

                            if (Convert.ToBoolean(OthpManager[0]["InActive"]))
                            {
                                // ClearForm();
                                SetKardan();
                                SetLabelWarning("عضو مورد نظر غیر فعال می باشد");
                                return;
                            }

                            txtFatherName.Text = OthpManager[0]["FatherName"].ToString();
                            txtFirstName.Text = OthpManager[0]["FirstName"].ToString();
                            txtLastName.Text = OthpManager[0]["LastName"].ToString();
                            txtSSN.Text = OthpManager[0]["SSN"].ToString();
                            txtFileNoDate.Text = OthpManager[0]["FileNoDate"].ToString();
                            txtFileNo.Text = OthpManager[0]["FileNo"].ToString();
                            SetKardan();

                            ImpAgentManager.FindByMemberIdTypeId(OtpId, (int)TSP.DataManager.TSMemberType.OtherPerson);
                            if (ImpAgentManager.Count > 0)
                                ChbCheck.Visible = true;
                            else
                                ChbCheck.Visible = false;

                        }
                        else
                        {
                            ClearForm();
                            SetKardan();
                            SetLabelWarning("چنین کد عضویتی وجود ندارد.مجدداً وارد نمایید");
                            return;
                        }
                        break;

                    case "2"://Memar

                        OthpManager.FindKardanAndMemarByOtpCode(ID.ToString(), (int)TSP.DataManager.OtherPersonType.Memar);
                        if (OthpManager.Count == 1)
                        {
                            int OtpId = Convert.ToInt32(OthpManager[0]["OtpId"]);

                            if (Convert.ToBoolean(OthpManager[0]["InActive"]))
                            {
                                // ClearForm();
                                SetMemar();
                                SetLabelWarning("عضو مورد نظر غیر فعال می باشد");
                                return;
                            }

                            txtFatherName.Text = OthpManager[0]["FatherName"].ToString();
                            txtFirstName.Text = OthpManager[0]["FirstName"].ToString();
                            txtLastName.Text = OthpManager[0]["LastName"].ToString();
                            txtSSN.Text = OthpManager[0]["SSN"].ToString();
                            txtFileNoDate.Text = OthpManager[0]["FileNoDate"].ToString();
                            txtFileNo.Text = OthpManager[0]["FileNo"].ToString();
                            SetMemar();

                            ImpAgentManager.FindByMemberIdTypeId(OtpId, (int)TSP.DataManager.TSMemberType.ExpArchitect);
                            if (ImpAgentManager.Count > 0)
                                ChbCheck.Visible = true;
                            else
                                ChbCheck.Visible = false;

                        }
                        else
                        {
                            ClearForm();
                            SetMemar();
                            SetLabelWarning("چنین کد عضویتی وجود ندارد.مجدداً وارد نمایید");
                            return;
                        }
                        break;
                }
            }
            else
            {
                SetLabelWarning("نوع مجری را انتخاب نمایید");
            }
        }
        else
        {
            SetLabelWarning("کد عضویت را وارد نمایید");
        }

    }

    protected void CallbackAttachPageToAutomationLetter_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (String.IsNullOrEmpty(txtLetterNumber_AttachPageToAutomationLetter.Text.Trim()))
        {
            lblErrorInputAttachPageToAutomationLetter.ClientVisible = true;
            lblErrorInputAttachPageToAutomationLetter.Text = "شماره سند وارد نشده است";
            return;
        }

        String PageAddress = "~/Employee/TechnicalServices/Project/ImplementerAgentInsert.aspx";
        String QuerySting = "?ProjectId=" + HDProjectId.Value + "&PrjImpId=" + HDImpId.Value + "&ImpAgentId=" + HDImpAgentId.Value + "&Mode=" + HDMode.Value + "&PrjReId=" + RequestId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&PageMode2=" + Utility.EncryptQS("View");

        TSP.DataManager.Automation.AttachPageToLetter objAttachPageToLetter = new TSP.DataManager.Automation.AttachPageToLetter();
        objAttachPageToLetter.AttachPage(txtLetterNumber_AttachPageToAutomationLetter.Text, PageAddress, QuerySting, txtLinkName_AttachPageToAutomationLetter.Text,
            int.Parse(txtTimeOut_AttachPageToAutomationLetter.Text), Utility.GetCurrentUser_UserId());
        if (objAttachPageToLetter.SaveState == true)
        {
            PanelAttachPageToAutomationLetterInputData.ClientVisible = false;
            PanelAttachPageToAutomationLetterFinish.ClientVisible = true;
            lblMessageAttachPageToAutomationLetter.Text = objAttachPageToLetter.Message;
        }
        else
        {
            lblErrorInputAttachPageToAutomationLetter.ClientVisible = true;
            lblErrorInputAttachPageToAutomationLetter.Text = objAttachPageToLetter.Message;
        }
    }

    /********************************************************************* WorkFlow ********************************************************************************/
    private void CheckWorkFlowPermission()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementerOfProject;
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);
        btnSave.Enabled = WFPer.BtnSave;
        btnSave2.Enabled = WFPer.BtnSave;

        this.ViewState["BtnSave"] = btnSave.Enabled;
    }
}
