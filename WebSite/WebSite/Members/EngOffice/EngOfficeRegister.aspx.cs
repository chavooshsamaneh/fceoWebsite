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

public partial class Members_EngOffice_EngOfficeRegister : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
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
            OdbEOffType.FilterParameters[0].DefaultValue = "1";
            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["EngOfId"]))
            {
                Response.Redirect("EngOffice.aspx");
                return;
            }

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                EngOfficeId.Value = Server.HtmlDecode(Request.QueryString["EngOfId"]).ToString();
                EngFileId.Value = Server.HtmlDecode(Request.QueryString["EOfId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string EngOfId = Utility.DecryptQS(EngOfficeId.Value);
            string EOfId = Utility.DecryptQS(EngFileId.Value);


            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();
            TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();

            //TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();
            //ASPxTextBoxAmount.Text = GetFirstMembershipCost(CostSettingsManager).ToString("#,#");


            switch (PageMode)
            {
                case "View":
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(EOfId));

                    txtdExpDate.Visible = true;
                    txtdLastRegDate.Visible = true;
                    txtdSerialNo.Visible = true;
                    cmbdIsTemporary.Visible = true;
                    ASPxLabeld1.Visible = true;
                    ASPxLabeld2.Visible = true;
                    ASPxLabeld3.Visible = true;
                    ASPxLabeld4.Visible = true;

                    txtFileNo.Enabled = false;
                    ASPxRoundPanel2.Enabled = false;

                    if (string.IsNullOrEmpty(EOfId) || string.IsNullOrEmpty(EngOfId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }


                    break;


                case "New":

                    txtFileNo.Visible = false;
                    lblFileNo.Visible = false;

                    ASPxRoundPanel2.HeaderText = "درخواست صدور پروانه";

                    ASPxMenu1.Enabled = false;
                    break;

                case "Edit":

                    //ASPxTextBoxFicheCode.Enabled = false;

                    if (string.IsNullOrEmpty(EOfId) || string.IsNullOrEmpty(EngOfId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(EOfId));



                    ASPxRoundPanel2.HeaderText = "ویرایش";

                    break;

                case "Change":

                    // DisableForReq();
                    FillForm(int.Parse(EOfId));
                    //txtdExpDate.Visible = true;
                    //txtdLastRegDate.Visible = true;
                    //txtdSerialNo.Visible = true;
                    //cmbdIsTemporary.Visible = true;
                    //ASPxLabeld1.Visible = true;
                    //ASPxLabeld2.Visible = true;
                    //ASPxLabeld3.Visible = true;
                    //ASPxLabeld4.Visible = true;
                    ASPxRoundPanel2.HeaderText = "درخواست تغییرات پروانه";
                    ASPxMenu1.Enabled = false;
                    txtFileLetterDate.Text = "";
                    txtFileLetterDate.Text = "";
                    txtDesc.Text = "";
                    break;

                case "Revival":

                    // DisableForReq();
                    FillForm(int.Parse(EOfId));

                    ASPxRoundPanel2.HeaderText = "درخواست تمدید پروانه";
                    ASPxMenu1.Enabled = false;
                    txtFileLetterDate.Text = "";
                    txtFileLetterDate.Text = "";
                    txtDesc.Text = "";
                    break;

                case "Reduplicate":

                    //   DisableForReq();
                    FillForm(int.Parse(EOfId));

                    ASPxRoundPanel2.HeaderText = "درخواست صدور المثنی";
                    ASPxMenu1.Enabled = false;
                    txtFileLetterDate.Text = "";
                    txtFileLetterDate.Text = "";
                    txtDesc.Text = "";
                    //ComboEOfTId.Enabled = false;
                    break;
            }


        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (!CheckLocker()) return;

        string PageMode = Utility.DecryptQS(PgMode.Value);
        string EOfId = Utility.DecryptQS(EngFileId.Value);
        string EngOfId = Utility.DecryptQS(EngOfficeId.Value);
        if (string.IsNullOrEmpty(EOfId) || string.IsNullOrEmpty(EngOfId))
        {
            Response.Redirect("EngOffice.aspx");
        }
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            switch (PageMode)
            {
                case "New":
                    Insert();
                    break;
                case "Edit":
                    Edit(int.Parse(EOfId));
                    break;
                case "Change":
                    Change(int.Parse(EngOfId), int.Parse(EOfId));
                    break;
                case "Revival":
                    Revival(int.Parse(EngOfId), int.Parse(EOfId));
                    break;
                case "Reduplicate":
                    Reduplicate(int.Parse(EngOfId), int.Parse(EOfId));
                    break;
            }

        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EngOffice.aspx");
    }

    private void FillForm(int EOfId)
    {
        TSP.DataManager.OfficeMemberManager OffMemberManager = (TSP.DataManager.OfficeMemberManager)Session["OfficeMemberManager"];
        TSP.DataManager.EngOffFileManager fileManager = new TSP.DataManager.EngOffFileManager();

        //TSP.DataManager.AccountingDocOperationManager DocManager = new TSP.DataManager.AccountingDocOperationManager();
        //ASPxTextBoxFicheCode.Text = DocManager.GetBankDocNum(EOfId, (int)TSP.DataManager.AccountingTT.MembershipConfirmation);

        fileManager.FindByCode(EOfId);
        if (fileManager.Count == 1)
        {
            //txtEOfCode.Text = fileManager[0]["EOfCode"].ToString();
            if (!Utility.IsDBNullOrNullValue(fileManager[0]["EOfTId"]))
            {
                ComboEOfTId.DataBind();
                ComboEOfTId.SelectedIndex = ComboEOfTId.Items.IndexOfValue(fileManager[0]["EOfTId"].ToString());
            }
            txtEngOffName.Text = fileManager[0]["EngOffName"].ToString();
            txtLetterNo.Text = fileManager[0]["ParticipateLetterNo"].ToString();
            txtLetterDate.Text = fileManager[0]["ParticipateLetterDate"].ToString();
            txtDaftarNo.Text = fileManager[0]["EngOffNo"].ToString();
            txtDaftarLoc.Text = fileManager[0]["EngOffLoc"].ToString();
            txtFileNo.Text = fileManager[0]["FileNo"].ToString();
            txtDesc.Text = fileManager[0]["Description"].ToString();
            txtTel.Text = fileManager[0]["TellNo"].ToString();
            txtFax.Text = fileManager[0]["FaxNo"].ToString();
            txtMobileNo.Text = fileManager[0]["MobileNo"].ToString();
            txtAddress.Text = fileManager[0]["Address"].ToString();

            if (Convert.ToBoolean(fileManager[0]["Requester"]))//Emp
            {
                ASPxLabelddate.Visible = true;
                ASPxLabeldno.Visible = true;
                txtFileLetterDate.Visible = true;
                txtFileLetterNo.Visible = true;
                txtFileLetterDate.Text = fileManager[0]["FileLetterDate"].ToString();
                txtFileLetterNo.Text = fileManager[0]["FileLetterNo"].ToString();
            }

            txtdExpDate.Text = fileManager[0]["ExpireDate"].ToString();
            txtdLastRegDate.Text = fileManager[0]["RegDate"].ToString();
            txtdSerialNo.Text = fileManager[0]["SerialNo"].ToString();
            if (Convert.ToBoolean(fileManager[0]["IsTemp"]))
                cmbdIsTemporary.SelectedIndex = 1;
            else
                cmbdIsTemporary.SelectedIndex = 0;

        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }

    }
    protected void Insert()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();
        TSP.DataManager.EngOffFileManager fileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();


        trans.Add(EngOffManager);
        trans.Add(fileManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(OfMeManager);


        try
        {
            int MeId = Utility.GetCurrentUser_MeId();

            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";
            string MFCode = TSP.DataManager.EngOfficeManager.MFType.ToString();
            string MFMjCode = "0000000";

            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات انجام گرفته است";
                return;
            }

            string PerDate = Utility.GetDateOfToday();

            DataRow Offrow = EngOffManager.NewRow();
            if (ComboEOfTId.Value != null)
                Offrow["EOfTId"] = ComboEOfTId.Value;
            Offrow["ParticipateLetterNo"] = txtLetterNo.Text;
            Offrow["ParticipateLetterDate"] = txtLetterDate.Text;
            Offrow["EngOffName"] = txtEngOffName.Text.Trim();
            Offrow["EngOffName"] = txtEngOffName.Text.Trim();
            Offrow["TellNo"] = txtTel.Text.Trim();
            Offrow["FaxNo"] = txtFax.Text.Trim();
            Offrow["MobileNo"] = txtMobileNo.Text.Trim();
            Offrow["Address"] = txtAddress.Text.Trim();
            Offrow["EngOffNo"] = txtDaftarNo.Text;
            Offrow["EngOffLoc"] = txtDaftarLoc.Text;
            Offrow["Description"] = txtDesc.Text;
            Offrow["CreateDate"] = PerDate;
            Offrow["InActive"] = 0;
            Offrow["UserId"] = Utility.GetCurrentUser_UserId();
            Offrow["ModifiedDate"] = DateTime.Now;
            Offrow["IsConfirm"] = 0;


            //Offrow["FileNo"] = txtFileNo.Text; // "5-17-" + CrsCode + "-" + txtDaftarNo.Text;

            EngOffManager.AddRow(Offrow);

            trans.BeginSave();
            int cn = EngOffManager.Save();
            if (cn > 0)
            {
                int EngOfId = int.Parse(EngOffManager[0]["EngOfId"].ToString());
                int EOfId = -1;

                EngOfficeId.Value = Utility.EncryptQS(EngOfId.ToString());

                DataRow drFile = fileManager.NewRow();
                drFile["EngOfId"] = EngOfId;
                drFile["Type"] = (int)TSP.DataManager.EngOffFileType.SaveFileDocument;//صدور
                if (ComboEOfTId.Value != null)
                    drFile["EOfTId"] = ComboEOfTId.Value;
                drFile["ParticipateLetterNo"] = txtLetterNo.Text;
                drFile["ParticipateLetterDate"] = txtLetterDate.Text;

                drFile["EngOffName"] = txtEngOffName.Text.Trim();
                drFile["TellNo"] = txtTel.Text.Trim();
                drFile["FaxNo"] = txtFax.Text.Trim();
                drFile["MobileNo"] = txtMobileNo.Text.Trim();
                drFile["Address"] = txtAddress.Text.Trim();

                drFile["EngOffNo"] = txtDaftarNo.Text;
                drFile["EngOffLoc"] = txtDaftarLoc.Text;
                drFile["Description"] = txtDesc.Text;
                drFile["CreateDate"] = PerDate;
                drFile["Requester"] = 0;
                drFile["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.EngOffice);
                drFile["InActive"] = 0;
                drFile["UserId"] = Utility.GetCurrentUser_UserId();
                drFile["ModifiedDate"] = DateTime.Now;
                drFile["IsConfirm"] = 0;

                drFile["RegPlaceId"] = Utility.GetCurrentProvinceId();//استان فارس
                drFile["PrId"] = Utility.GetCurrentProvinceId();//استان فارس

                fileManager.AddRow(drFile);
                if (fileManager.Save() > 0)
                {
                    fileManager.DataTable.AcceptChanges();
                    string MFSerialNo = fileManager[0]["MFSerialNo"].ToString();
                    while (MFSerialNo.Length < 5)
                    {
                        MFSerialNo = "0" + MFSerialNo;
                    }
                    //fileManager[0]["FileNo"] = MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;
                    //fileManager.Save();


                    #region Member
                    //   MeManager.FindByCode(MeId);

                    DataRow drMembers = OfMeManager.NewRow();

                    drMembers["OfReId"] = EOfId;
                    drMembers["OfId"] = EngOfId;
                    drMembers["OfKind"] = 1;
                    drMembers["OfmType"] = 1;
                    drMembers["PersonId"] = MeId;
                    drMembers["OfpId"] = (int)TSP.DataManager.OfficePosition.EngOfficeManager;//مسئول دفتر
                    drMembers["IsFullTime"] = 1;
                    drMembers["HasSignRight"] = 1;
                    if (!Utility.IsDBNullOrNullValue(MeManager[0]["SignUrl"]))
                        drMembers["SignUrl"] = MeManager[0]["SignUrl"];
                    drMembers["StartDate"] = Utility.GetDateOfToday();
                    drMembers["UserId"] = Utility.GetCurrentUser_UserId();
                    drMembers["ModifiedDate"] = DateTime.Now;
                    drMembers["IsConfirm"] = 1;
                    drMembers["ConfirmDate"] = Utility.GetDateOfToday();
                    OfMeManager.AddRow(drMembers);
                    OfMeManager.Save();

                    #endregion

                    #region SetMFNo

                    TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
                    DataTable dtMj = MeMjManager.SelectMemberMasterMajor(Utility.GetCurrentUser_MeId());
                    if (dtMj.Rows.Count > 0)
                    {

                        int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
                        //string MFSerialNo = ReqManager[0]["MFSerialNo"].ToString();
                        int i = -1;
                        //string MFNo = fileManager[0]["FileNo"].ToString();
                        //string[] MFNoMajor = MFNo.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                        char[] Code = MFMjCode.ToCharArray();

                        switch (MjId)
                        {
                            case (int)TSP.DataManager.MainMajors.Architecture:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Architecture;
                                Code[i] = MjId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Civil:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Civil;
                                Code[i] = MjId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Electronic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Electronic;
                                Code[i] = MjId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Mapping:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mapping;
                                Code[i] = MjId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Mechanic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mechanic;
                                Code[i] = MjId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Traffic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Traffic;
                                Code[i] = MjId.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Urbanism:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Urbanism;
                                Code[i] = MjId.ToString()[0];
                                break;
                            default:
                                i = -1;
                                break;

                        }
                        if (i != -1)
                        {
                            // Code[i] = '1';
                            fileManager[0]["FileNo"] = MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;
                            fileManager.Save();

                        }
                    }

                    #endregion


                    lblFileNo.Visible = true;
                    txtFileNo.Visible = true;
                    txtFileNo.Text = fileManager[0]["FileNo"].ToString();


                    int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
                    int CurrentNmcId = Utility.GetCurrentUser_MeId();
                    int WfStart = WorkFlowStateManager.StartWorkFlow(EOfId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 1);

                    if (WfStart > 0)
                    {
                        trans.EndSave();
                        ASPxMenu1.Enabled = true;

                        EngFileId.Value = Utility.EncryptQS(EOfId.ToString());
                        PgMode.Value = Utility.EncryptQS("Edit");
                        ASPxRoundPanel2.HeaderText = "ویرایش";
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد";
                    }
                    else
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    }
                }
                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }

            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }


    }
    protected void Edit(int EOfId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TelManager telManager = new TSP.DataManager.TelManager();
        TSP.DataManager.AddressManager AddManager = new TSP.DataManager.AddressManager();
        TSP.DataManager.EngOffFileManager fileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        trans.Add(fileManager);
        trans.Add(telManager);
        trans.Add(telManager);
        trans.Add(AddManager);
        trans.Add(WorkFlowStateManager);

        try
        {
            fileManager.FindByCode(EOfId);

            if (fileManager.Count == 1)
            {
                trans.BeginSave();
                fileManager[0].BeginEdit();
                if (ComboEOfTId.Value != null)
                    fileManager[0]["EOfTId"] = ComboEOfTId.Value;
                fileManager[0]["ParticipateLetterNo"] = txtLetterNo.Text;
                fileManager[0]["ParticipateLetterDate"] = txtLetterDate.Text;
                fileManager[0]["EngOffName"] = txtEngOffName.Text.Trim();
                fileManager[0]["TellNo"] = txtTel.Text.Trim();
                fileManager[0]["FaxNo"] = txtFax.Text.Trim();
                fileManager[0]["MobileNo"] = txtMobileNo.Text.Trim();
                fileManager[0]["Address"] = txtAddress.Text.Trim();
                fileManager[0]["EngOffNo"] = txtDaftarNo.Text;
                fileManager[0]["EngOffLoc"] = txtDaftarLoc.Text;
                fileManager[0]["Description"] = txtDesc.Text;
                fileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                fileManager[0]["ModifiedDate"] = DateTime.Now;
                fileManager[0].EndEdit();
                int cnt = fileManager.Save();

                if (cnt > 0)
                {

                    telManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.EngOffFile, EOfId);
                    if (telManager.Count > 0)
                    {
                        for (int t = 0; t < telManager.Count; t++)
                        {
                            if (telManager[t]["Kind"].ToString() == "0")
                            {
                                telManager[t].BeginEdit();
                                telManager[t]["Number"] = txtTel.Text;
                                telManager[t]["UserId"] = Utility.GetCurrentUser_UserId();
                                telManager[t]["ModifiedDate"] = DateTime.Now;
                                telManager[t].EndEdit();

                            }
                            else
                            {
                                telManager[t].BeginEdit();
                                telManager[t]["Number"] = txtFax.Text;
                                telManager[t]["UserId"] = Utility.GetCurrentUser_UserId();
                                telManager[t]["ModifiedDate"] = DateTime.Now;
                                telManager[t].EndEdit();

                            }
                            telManager.Save();
                            telManager.DataTable.AcceptChanges();
                        }

                    }
                    AddManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.EngOffFile, EOfId);
                    if (AddManager.Count > 0)
                    {
                        AddManager[0].BeginEdit();
                        AddManager[0]["Address"] = txtAddress.Text;
                        AddManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        AddManager[0]["ModifiedDate"] = DateTime.Now;
                        AddManager[0].EndEdit();
                        AddManager.Save();
                    }

                    trans.EndSave();
                    PgMode.Value = Utility.EncryptQS("Edit");

                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    this.LabelWarning.Text = err.Message;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                this.LabelWarning.Text = err.Message;
            }

        }


    }
    private void Revival(int EngOfId, int EOfId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.EngOffFileManager FileManager2 = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        WorkFlowTaskManager.ClearBeforeFill = true;
        FileManager.ClearBeforeFill = true;

        trans.Add(WorkFlowStateManager);
        trans.Add(FileManager);
        trans.Add(FileManager2);
        try
        {
            FileManager.FindByCode(EOfId);
            if (FileManager.Count == 1)
            {

                int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
                DataTable dtWfState = WorkFlowStateManager.SelectLastState(TableType, EOfId);
                if (dtWfState.Rows.Count > 0)
                {
                    int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
                    int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectDocumentOfEngOfficeAndEndProcess;
                    int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfEngOfficeAndEndProccess;
                    int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;

                    int RejectTaskId = -1;
                    int ConfirmTaskId = -1;
                    int SaveInfoTaskId = -1;

                    WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }


                    WorkFlowTaskManager.FindByTaskCode(RejectTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        RejectTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }

                    WorkFlowTaskManager.FindByTaskCode(ConfirmTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        ConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }

                    if (CurrentTaskId == RejectTaskId || CurrentTaskId == ConfirmTaskId)
                    {

                        if (FileManager[0]["IsConfirm"].ToString() != "0")
                        {
                            string CrtEndDate = FileManager[0]["ExpireDate"].ToString();
                            Utility.Date objDate = new Utility.Date(CrtEndDate);
                            string LastMonth = objDate.AddMonths(-1);
                            string Today = Utility.GetDateOfToday();
                            int IsDocExp = string.Compare(Today, LastMonth);
                            if (IsDocExp > 0)
                            {
                                trans.BeginSave();

                                DataRow Offrow = FileManager2.NewRow();

                                Offrow["EngOfId"] = EngOfId;
                                if (ComboEOfTId.Value != null)
                                    Offrow["EOfTId"] = ComboEOfTId.Value;
                                Offrow["ParticipateLetterNo"] = txtLetterNo.Text;
                                Offrow["ParticipateLetterDate"] = txtLetterDate.Text;
                                Offrow["EngOffName"] = txtEngOffName.Text.Trim();
                                Offrow["TellNo"] = txtTel.Text.Trim();
                                Offrow["FaxNo"] = txtFax.Text.Trim();
                                Offrow["MobileNo"] = txtMobileNo.Text.Trim();
                                Offrow["Address"] = txtAddress.Text.Trim();
                                Offrow["EngOffNo"] = txtDaftarNo.Text;
                                Offrow["EngOffLoc"] = txtDaftarLoc.Text;
                                Offrow["Description"] = txtDesc.Text;
                                Offrow["CreateDate"] = Utility.GetDateOfToday();
                                Offrow["InActive"] = 0;
                                Offrow["UserId"] = Utility.GetCurrentUser_UserId();
                                Offrow["ModifiedDate"] = DateTime.Now;
                                Offrow["IsConfirm"] = 0;
                                //Offrow["FileLetterNo"] = txtFileLetterNo.Text;
                                //Offrow["FileLetterDate"] = txtFileLetterDate.Text;

                                if (!Utility.IsDBNullOrNullValue(FileManager[0]["MFSerialNo"]))
                                    Offrow["MFSerialNo"] = FileManager[0]["MFSerialNo"].ToString();
                                if (!Utility.IsDBNullOrNullValue(FileManager[0]["RegDate"]))
                                    Offrow["RegDate"] = FileManager[0]["RegDate"].ToString();
                                Offrow["ExpireDate"] = "";
                                Offrow["Type"] = (int)TSP.DataManager.EngOffFileType.Revival;//تمدید
                                if (!Utility.IsDBNullOrNullValue(FileManager[0]["PrId"]))
                                    Offrow["PrId"] = FileManager[0]["PrId"].ToString();
                                if (!Utility.IsDBNullOrNullValue(FileManager[0]["RegPlaceId"]))
                                    Offrow["RegPlaceId"] = FileManager[0]["RegPlaceId"].ToString();

                                if (!Utility.IsDBNullOrNullValue(FileManager[0]["FileNo"]))
                                    Offrow["FileNo"] = FileManager[0]["FileNo"].ToString();

                                Offrow["CreateDate"] = Utility.GetDateOfToday();
                                Offrow["Requester"] = 0;
                                Offrow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.EngOffice);

                                FileManager2.AddRow(Offrow);
                                int cn = FileManager2.Save();
                                if (cn > 0)
                                {
                                    txtFileNo.Text = FileManager2[0]["FileNo"].ToString();

                                    int EOfId2 = int.Parse(FileManager2[FileManager2.Count - 1]["EOfId"].ToString());


                                    DataRow WFStateRow = WorkFlowStateManager.NewRow();
                                    int NmcId = Utility.GetCurrentUser_MeId();
                                    if (NmcId > 0)
                                    {

                                        WFStateRow["TaskId"] = SaveInfoTaskId;
                                        WFStateRow["TableId"] = EOfId2;
                                        WFStateRow["NmcIdType"] = 1;
                                        WFStateRow["NmcId"] = NmcId;
                                        WFStateRow["SubOrder"] = 1;
                                        WFStateRow["StateType"] = 0;
                                        WFStateRow["Description"] = "شروع جریان کار تمدید پروانه دفتر توسط عضو حقیقی";
                                        WFStateRow["Date"] = Utility.GetDateOfToday();
                                        WFStateRow["UserId"] = Utility.GetCurrentUser_UserId();
                                        WFStateRow["ModifiedDate"] = DateTime.Now;

                                        WorkFlowStateManager.AddRow(WFStateRow);

                                        int count = WorkFlowStateManager.Save();
                                        if (count > 0)
                                        {

                                            trans.EndSave();
                                            DivReport.Visible = true;
                                            LabelWarning.Text = "ذخیره انجام شد.";
                                            EngFileId.Value = Utility.EncryptQS(EOfId2.ToString());
                                            //EngFileId.Value = Utility.EncryptQS(FileManager2[FileManager2.Count - 1]["EOfId"].ToString());
                                            PgMode.Value = Utility.EncryptQS("Edit");
                                            ASPxRoundPanel2.HeaderText = "ویرایش";
                                            //DisableForReq();
                                            ASPxMenu1.Enabled = true;
                                        }
                                        else
                                        {
                                            trans.CancelSave();
                                            DivReport.Visible = true;
                                            LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                                        }
                                    }
                                    else
                                    {
                                        trans.CancelSave();
                                        DivReport.Visible = true;
                                        LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                                    }
                                }
                                else
                                {
                                    trans.CancelSave();
                                    DivReport.Visible = true;
                                    LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                                }
                            }
                            else
                            {
                                trans.CancelSave();
                                DivReport.Visible = true;
                                LabelWarning.Text = "تاریخ اعتبار پروانه انتخاب شده به پایان نرسیده است.";
                            }
                        }
                        else
                        {
                            DivReport.Visible = true;
                            LabelWarning.Text = "امکان تمدید برای پروانه تایید نشده وجود ندارد.";
                        }

                    }
                    else
                    {
                        DivReport.Visible = true;
                        LabelWarning.Text = "به دلیل به پایان نرسیدن جریان کار پروانه انتخاب شده امکان درخواست تمدید وجود ندارد.";
                    }

                }
                else
                {
                    DivReport.Visible = true;
                    LabelWarning.Text = "برای پرونده انتخاب شده جریان کاری تعریف نشده است.";
                }
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }
    private void Change(int EngOfId, int EOfId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.EngOffFileManager FileManager2 = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TelManager telManager = new TSP.DataManager.TelManager();
        TSP.DataManager.AddressManager AddManager = new TSP.DataManager.AddressManager();

        WorkFlowTaskManager.ClearBeforeFill = true;
        FileManager.ClearBeforeFill = true;

        trans.Add(WorkFlowStateManager);
        trans.Add(FileManager);
        trans.Add(FileManager2);
        trans.Add(telManager);
        trans.Add(AddManager);

        try
        {

            FileManager.FindByCode(EOfId);
            if (FileManager.Count == 1)
            {
                int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
                DataTable dtWfState = WorkFlowStateManager.SelectLastState(TableType, EOfId);
                if (dtWfState.Rows.Count > 0)
                {
                    int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
                    int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectDocumentOfEngOfficeAndEndProcess;
                    int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfEngOfficeAndEndProccess;
                    int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;

                    int RejectTaskId = -1;
                    int ConfirmTaskId = -1;
                    int SaveInfoTaskId = -1;

                    WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }


                    WorkFlowTaskManager.FindByTaskCode(RejectTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        RejectTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }

                    WorkFlowTaskManager.FindByTaskCode(ConfirmTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        ConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }

                    if (CurrentTaskId == RejectTaskId || CurrentTaskId == ConfirmTaskId)
                    {

                        if (FileManager[0]["IsConfirm"].ToString() == "1")
                        {
                            trans.BeginSave();

                            DataRow Offrow = FileManager2.NewRow();
                            Offrow["EngOfId"] = EngOfId;
                            if (ComboEOfTId.Value != null)
                                Offrow["EOfTId"] = ComboEOfTId.Value;
                            Offrow["ParticipateLetterNo"] = txtLetterNo.Text;
                            Offrow["ParticipateLetterDate"] = txtLetterDate.Text;
                            Offrow["EngOffName"] = txtEngOffName.Text.Trim();
                            Offrow["TellNo"] = txtTel.Text.Trim();
                            Offrow["FaxNo"] = txtFax.Text.Trim();
                            Offrow["MobileNo"] = txtMobileNo.Text.Trim();
                            Offrow["Address"] = txtAddress.Text.Trim();
                            Offrow["EngOffNo"] = txtDaftarNo.Text;
                            Offrow["EngOffLoc"] = txtDaftarLoc.Text;
                            Offrow["Description"] = txtDesc.Text;
                            Offrow["CreateDate"] = Utility.GetDateOfToday();
                            Offrow["InActive"] = 0;
                            Offrow["UserId"] = Utility.GetCurrentUser_UserId();
                            Offrow["ModifiedDate"] = DateTime.Now;
                            Offrow["IsConfirm"] = 0;
                            //Offrow["FileLetterNo"] = txtFileLetterNo.Text;
                            //Offrow["FileLetterDate"] = txtFileLetterDate.Text;

                            if (!Utility.IsDBNullOrNullValue(FileManager[0]["MFSerialNo"]))
                                Offrow["MFSerialNo"] = FileManager[0]["MFSerialNo"].ToString();
                            if (!Utility.IsDBNullOrNullValue(FileManager[0]["RegDate"]))
                                Offrow["RegDate"] = FileManager[0]["RegDate"].ToString();
                            if (!Utility.IsDBNullOrNullValue(FileManager[0]["ExpireDate"]))
                                Offrow["ExpireDate"] = FileManager[0]["ExpireDate"].ToString();
                            Offrow["Type"] = (int)TSP.DataManager.EngOffFileType.Change;//
                            if (!Utility.IsDBNullOrNullValue(FileManager[0]["PrId"]))
                                Offrow["PrId"] = FileManager[0]["PrId"].ToString();
                            if (!Utility.IsDBNullOrNullValue(FileManager[0]["RegPlaceId"]))
                                Offrow["RegPlaceId"] = FileManager[0]["RegPlaceId"].ToString();

                            if (!Utility.IsDBNullOrNullValue(FileManager[0]["FileNo"]))
                                Offrow["FileNo"] = FileManager[0]["FileNo"].ToString();

                            Offrow["CreateDate"] = Utility.GetDateOfToday();
                            Offrow["Requester"] = 0;
                            Offrow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.EngOffice);

                            FileManager2.AddRow(Offrow);
                            int cn = FileManager2.Save();
                            if (cn > 0)
                            {
                                int EOfId2 = int.Parse(FileManager2[FileManager2.Count - 1]["EOfId"].ToString());

                                //txtFileNo.Text = FileManager2[0]["FileNo"].ToString();

                                DataRow drAdr = AddManager.NewRow();
                                drAdr["TtType"] = EOfId2;
                                drAdr["TtId"] = (int)TSP.DataManager.TableCodes.EngOffFile;
                                drAdr["Address"] = txtAddress.Text;
                                drAdr["InActive"] = 0;
                                drAdr["UserId"] = Utility.GetCurrentUser_UserId();
                                drAdr["ModifiedDate"] = DateTime.Now;
                                AddManager.AddRow(drAdr);
                                AddManager.Save();

                                DataRow drTel = telManager.NewRow();
                                drTel["TtType"] = EOfId2;
                                drTel["TtId"] = (int)TSP.DataManager.TableCodes.EngOffFile;
                                drTel["Kind"] = 0;
                                drTel["Number"] = txtTel.Text;
                                drTel["UserId"] = Utility.GetCurrentUser_UserId();
                                drTel["ModifiedDate"] = DateTime.Now;
                                telManager.AddRow(drTel);

                                DataRow drFax = telManager.NewRow();
                                drFax["TtType"] = EOfId2;
                                drFax["TtId"] = (int)TSP.DataManager.TableCodes.EngOffFile;
                                drFax["Kind"] = 1;
                                drFax["Number"] = txtFax.Text;
                                drFax["UserId"] = Utility.GetCurrentUser_UserId();
                                drFax["ModifiedDate"] = DateTime.Now;
                                telManager.AddRow(drFax);
                                telManager.Save();

                                DataRow WFStateRow = WorkFlowStateManager.NewRow();
                                int NmcId = Utility.GetCurrentUser_MeId();
                                if (NmcId > 0)
                                {

                                    WFStateRow["TaskId"] = SaveInfoTaskId;
                                    WFStateRow["TableId"] = EOfId2;//ReqManager2[ReqManager2.Count - 1]["OfReId"];
                                    WFStateRow["NmcIdType"] = 1;
                                    WFStateRow["NmcId"] = NmcId;
                                    WFStateRow["SubOrder"] = 1;
                                    WFStateRow["StateType"] = 0;
                                    WFStateRow["Description"] = "شروع جریان کار درخواست تغییرات پروانه دفتر توسط عضو حقیقی";
                                    WFStateRow["Date"] = Utility.GetDateOfToday();
                                    WFStateRow["UserId"] = Utility.GetCurrentUser_UserId();
                                    WFStateRow["ModifiedDate"] = DateTime.Now;

                                    WorkFlowStateManager.AddRow(WFStateRow);

                                    int count = WorkFlowStateManager.Save();
                                    if (count > 0)
                                    {
                                        trans.EndSave();
                                        EngFileId.Value = Utility.EncryptQS(EOfId2.ToString());
                                        PgMode.Value = Utility.EncryptQS("Edit");

                                        DivReport.Visible = true;
                                        LabelWarning.Text = "ذخیره انجام شد.";

                                        ASPxRoundPanel2.HeaderText = "ویرایش";
                                        //DisableForReq();
                                        ASPxMenu1.Enabled = true;

                                    }
                                    else
                                    {
                                        trans.CancelSave();
                                        DivReport.Visible = true;
                                        LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                                    }
                                }
                                else
                                {
                                    trans.CancelSave();
                                    DivReport.Visible = true;
                                    LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                                }
                            }
                            else
                            {
                                trans.CancelSave();
                                DivReport.Visible = true;
                                LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                            }
                        }
                        else
                        {
                            DivReport.Visible = true;
                            LabelWarning.Text = "امکان درخواست تغییرات برای پروانه تایید نشده وجود ندارد.";
                        }

                    }
                    else
                    {
                        DivReport.Visible = true;
                        LabelWarning.Text = "به دلیل به پایان نرسیدن جریان کار پروانه انتخاب شده امکان درخواست تغییرات وجود ندارد.";
                    }

                }
                else
                {
                    DivReport.Visible = true;
                    LabelWarning.Text = "برای پرونده انتخاب شده جریان کاری تعریف نشده است.";
                }
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }
    private void Reduplicate(int EngOfId, int EOfId)
    {

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


        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.EngOffFileManager FileManager2 = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TelManager telManager = new TSP.DataManager.TelManager();
        TSP.DataManager.AddressManager AddManager = new TSP.DataManager.AddressManager();

        WorkFlowTaskManager.ClearBeforeFill = true;
        FileManager.ClearBeforeFill = true;

        trans.Add(WorkFlowStateManager);
        trans.Add(FileManager);
        trans.Add(FileManager2);
        trans.Add(telManager);
        trans.Add(AddManager);

        try
        {

            FileManager.FindByCode(EOfId);
            if (FileManager.Count == 1)
            {
                int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
                DataTable dtWfState = WorkFlowStateManager.SelectLastState(TableType, EOfId);
                if (dtWfState.Rows.Count > 0)
                {
                    int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
                    int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectDocumentOfEngOfficeAndEndProcess;
                    int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfEngOfficeAndEndProccess;
                    int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;

                    int RejectTaskId = -1;
                    int ConfirmTaskId = -1;
                    int SaveInfoTaskId = -1;

                    WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }


                    WorkFlowTaskManager.FindByTaskCode(RejectTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        RejectTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }

                    WorkFlowTaskManager.FindByTaskCode(ConfirmTaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        ConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    }

                    if (CurrentTaskId == RejectTaskId || CurrentTaskId == ConfirmTaskId)
                    {

                        if (FileManager[0]["IsConfirm"].ToString() == "1")
                        {
                            trans.BeginSave();

                            DataRow Offrow = FileManager2.NewRow();
                            Offrow["EngOfId"] = EngOfId;
                            if (ComboEOfTId.Value != null)
                                Offrow["EOfTId"] = ComboEOfTId.Value;
                            Offrow["ParticipateLetterNo"] = txtLetterNo.Text;
                            Offrow["ParticipateLetterDate"] = txtLetterDate.Text;
                            Offrow["EngOffName"] = txtEngOffName.Text.Trim();
                            Offrow["TellNo"] = txtTel.Text.Trim();
                            Offrow["FaxNo"] = txtFax.Text.Trim();
                            Offrow["MobileNo"] = txtMobileNo.Text.Trim();
                            Offrow["Address"] = txtAddress.Text.Trim();
                            Offrow["EngOffNo"] = txtDaftarNo.Text;
                            Offrow["EngOffLoc"] = txtDaftarLoc.Text;
                            Offrow["Description"] = txtDesc.Text;
                            Offrow["CreateDate"] = Utility.GetDateOfToday();
                            Offrow["InActive"] = 0;
                            Offrow["UserId"] = Utility.GetCurrentUser_UserId();
                            Offrow["ModifiedDate"] = DateTime.Now;
                            Offrow["IsConfirm"] = 0;
                            //Offrow["FileLetterNo"] = txtFileLetterNo.Text;
                            //Offrow["FileLetterDate"] = txtFileLetterDate.Text;

                            if (!Utility.IsDBNullOrNullValue(FileManager[0]["MFSerialNo"]))
                                Offrow["MFSerialNo"] = FileManager[0]["MFSerialNo"].ToString();
                            if (!Utility.IsDBNullOrNullValue(FileManager[0]["RegDate"]))
                                Offrow["RegDate"] = FileManager[0]["RegDate"].ToString();
                            if (!Utility.IsDBNullOrNullValue(FileManager[0]["ExpireDate"]))
                                Offrow["ExpireDate"] = FileManager[0]["ExpireDate"].ToString();
                            Offrow["Type"] = (int)TSP.DataManager.EngOffFileType.Reduplicate;//المثنی
                            if (!Utility.IsDBNullOrNullValue(FileManager[0]["PrId"]))
                                Offrow["PrId"] = FileManager[0]["PrId"].ToString();
                            if (!Utility.IsDBNullOrNullValue(FileManager[0]["RegPlaceId"]))
                                Offrow["RegPlaceId"] = FileManager[0]["RegPlaceId"].ToString();

                            if (!Utility.IsDBNullOrNullValue(FileManager[0]["FileNo"]))
                                Offrow["FileNo"] = FileManager[0]["FileNo"].ToString();

                            Offrow["CreateDate"] = Utility.GetDateOfToday();
                            Offrow["Requester"] = 0;
                            Offrow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.EngOffice);

                            FileManager2.AddRow(Offrow);
                            int cn = FileManager2.Save();
                            if (cn > 0)
                            {
                                int EOfId2 = int.Parse(FileManager2[FileManager2.Count - 1]["EOfId"].ToString());

                                //txtFileNo.Text = FileManager2[0]["FileNo"].ToString();

                                DataRow drAdr = AddManager.NewRow();
                                drAdr["TtType"] = EOfId2;
                                drAdr["TtId"] = (int)TSP.DataManager.TableCodes.EngOffFile;
                                drAdr["Address"] = txtAddress.Text;
                                drAdr["InActive"] = 0;
                                drAdr["UserId"] = Utility.GetCurrentUser_UserId();
                                drAdr["ModifiedDate"] = DateTime.Now;
                                AddManager.AddRow(drAdr);
                                AddManager.Save();

                                DataRow drTel = telManager.NewRow();
                                drTel["TtType"] = EOfId2;
                                drTel["TtId"] = (int)TSP.DataManager.TableCodes.EngOffFile;
                                drTel["Kind"] = 0;
                                drTel["Number"] = txtTel.Text;
                                drTel["UserId"] = Utility.GetCurrentUser_UserId();
                                drTel["ModifiedDate"] = DateTime.Now;
                                telManager.AddRow(drTel);

                                DataRow drFax = telManager.NewRow();
                                drFax["TtType"] = EOfId2;
                                drFax["TtId"] = (int)TSP.DataManager.TableCodes.EngOffFile;
                                drFax["Kind"] = 1;
                                drFax["Number"] = txtFax.Text;
                                drFax["UserId"] = Utility.GetCurrentUser_UserId();
                                drFax["ModifiedDate"] = DateTime.Now;
                                telManager.AddRow(drFax);
                                telManager.Save();

                                DataRow WFStateRow = WorkFlowStateManager.NewRow();
                                int NmcId = Utility.GetCurrentUser_MeId();
                                if (NmcId > 0)
                                {

                                    WFStateRow["TaskId"] = SaveInfoTaskId;
                                    WFStateRow["TableId"] = EOfId2; ;
                                    WFStateRow["NmcIdType"] = 1;
                                    WFStateRow["NmcId"] = NmcId;
                                    WFStateRow["SubOrder"] = 1;
                                    WFStateRow["StateType"] = 0;
                                    WFStateRow["Description"] = "شروع جریان کار درخواست المثنی پروانه دفتر توسط عضو حقیقی";
                                    WFStateRow["Date"] = Utility.GetDateOfToday();
                                    WFStateRow["UserId"] = Utility.GetCurrentUser_UserId();
                                    WFStateRow["ModifiedDate"] = DateTime.Now;

                                    WorkFlowStateManager.AddRow(WFStateRow);

                                    int count = WorkFlowStateManager.Save();
                                    if (count > 0)
                                    {
                                        trans.EndSave();
                                        EngFileId.Value = Utility.EncryptQS(EOfId2.ToString());
                                        PgMode.Value = Utility.EncryptQS("Edit");

                                        DivReport.Visible = true;
                                        LabelWarning.Text = "ذخیره انجام شد.";

                                        ASPxRoundPanel2.HeaderText = "ویرایش";
                                        //DisableForReq();
                                        ASPxMenu1.Enabled = true;

                                    }
                                    else
                                    {
                                        trans.CancelSave();
                                        DivReport.Visible = true;
                                        LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                                    }
                                }
                                else
                                {
                                    trans.CancelSave();
                                    DivReport.Visible = true;
                                    LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                                }
                            }
                            else
                            {
                                trans.CancelSave();
                                DivReport.Visible = true;
                                LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                            }
                        }
                        else
                        {
                            DivReport.Visible = true;
                            LabelWarning.Text = "امکان درخواست المثنی برای پروانه تایید نشده وجود ندارد.";
                        }

                    }
                    else
                    {
                        DivReport.Visible = true;
                        LabelWarning.Text = "به دلیل به پایان نرسیدن جریان کار پروانه انتخاب شده امکان درخواست المثنی وجود ندارد.";
                    }

                }
                else
                {
                    DivReport.Visible = true;
                    LabelWarning.Text = "برای پرونده انتخاب شده جریان کاری تعریف نشده است.";
                }
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

    bool CheckLocker()
    {
        int Meid = Utility.GetCurrentUser_MeId();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(Meid);
        if (Convert.ToBoolean(MemberManager[0]["IsLock"]))
        {
            TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
            string MemberLockers = lockHistoryManager.FindLockers(Meid, 0, 1);

            string lockers = Utility.GetFormattedObject(MemberLockers);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
            return false;
        }
        return true;
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Member":
                Response.Redirect("EngOfficeMember.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;

            case "Attach":
                Response.Redirect("EngOfficeAttachment.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;

            case "Job":
                Response.Redirect("EngOfficeJob.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;
        }

    }
}
