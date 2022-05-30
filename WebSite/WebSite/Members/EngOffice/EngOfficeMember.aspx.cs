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

public partial class Members_EngOffice_EngOfficeMember : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}

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
            if (string.IsNullOrEmpty(Request.QueryString["EOfId"]) || string.IsNullOrEmpty(Request.QueryString["EngOfId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("EngOffice.aspx");
                return;
            }
            try
            {
                EngOfficeId.Value = Server.HtmlDecode(Request.QueryString["EngOfId"]).ToString();
                EngFileId.Value = Server.HtmlDecode(Request.QueryString["EOfId"]).ToString();
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string EngOfId = Utility.DecryptQS(EngOfficeId.Value);
            string EOfId = Utility.DecryptQS(EngFileId.Value);


            if (string.IsNullOrEmpty(EngOfId) || string.IsNullOrEmpty(EOfId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            ObjectDataSource1.SelectParameters[0].DefaultValue = EOfId;
            ObjectDataSource1.SelectParameters[1].DefaultValue = EngOfId;


            TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
            FileManager.FindByCode(int.Parse(EOfId));
            if (FileManager.Count > 0)
            {
                if (Convert.ToBoolean(FileManager[0]["Requester"]))//FromEmployee
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew2.Enabled = false;
                    BtnNew.Enabled = false;
                    btnActive.Enabled = false;
                    btnActive1.Enabled = false;
                }
                if (FileManager[0]["IsConfirm"].ToString() != "0") //answered
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew2.Enabled = false;
                    BtnNew.Enabled = false;
                    btnConfirm.Enabled = false;
                    btnConfirm1.Enabled = false;
                    btnReject.Enabled = false;
                    btnReject.Enabled = false;
                }
            }


            int MeId = Utility.GetCurrentUser_MeId();
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            OfMeManager.FindEngOfficeMemberByPersonId(MeId);
            if (OfMeManager.Count > 0)
            {
                if (Convert.ToInt32(OfMeManager[0]["OfpId"]) == (int)TSP.DataManager.OfficePosition.EngOfficeEmployed)//عضو دفتر
                {
                    btnActive.Enabled = false;
                    btnActive1.Enabled = false;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;


                }

            }

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnActive"] = btnActive.Enabled;
            this.ViewState["BtnConfirm"] = btnConfirm.Enabled;
            this.ViewState["BtnReject"] = btnReject.Enabled;



        }
        if (this.ViewState["BtnActive"] != null)
            this.btnActive1.Enabled = this.btnActive.Enabled = (bool)this.ViewState["BtnActive"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnConfirm"] != null)
            this.btnConfirm.Enabled = this.btnConfirm1.Enabled = (bool)this.ViewState["BtnConfirm"];
        if (this.ViewState["BtnReject"] != null)
            this.btnReject.Enabled = this.btnReject1.Enabled = (bool)this.ViewState["BtnReject"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("EngOfficeMemberInsert.aspx?OfmId=" + Utility.EncryptQS("") + "&PersonId=" + Utility.EncryptQS("")
            + "&aPageMode=" + Utility.EncryptQS("New") + "&EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int OfmId = -1;
        int PersonId = -1;
        int EOfId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            PersonId = (int)row["PersonId"];
            EOfId = (int)row["OfReId"];
        }
        if (OfmId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }

        int CurrentEOfId = int.Parse(Utility.DecryptQS(EngFileId.Value));
        if (EOfId != CurrentEOfId)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
            return;
        }

        if (!CheckPermitionForEdit(EOfId))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
            return;
        }
        Response.Redirect("EngOfficeMemberInsert.aspx?OfmId=" + Utility.EncryptQS(OfmId.ToString()) + "&PersonId=" + Utility.EncryptQS(PersonId.ToString())
            + "&aPageMode=" + Utility.EncryptQS("Edit") + "&EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int OfmId = -1;
        int PersonId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            PersonId = (int)row["PersonId"];
        }
        if (OfmId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }

        Response.Redirect("EngOfficeMemberInsert.aspx?OfmId=" + Utility.EncryptQS(OfmId.ToString()) + "&PersonId=" + Utility.EncryptQS(PersonId.ToString())
            + "&aPageMode=" + Utility.EncryptQS("View") + "&EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EngOfficeRegister.aspx?PageMode=" + PgMode.Value + "&EngOfId=" + EngOfficeId.Value + "&EOfId=" + EngFileId.Value);
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (!CheckLocker()) return;

        int OfmId = -1;
        int EOfId = -1;
        int PersonId = -1;

        string EngOfId = Utility.DecryptQS(EngOfficeId.Value);

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            EOfId = (int)row["OfReId"];
            PersonId = (int)row["PersonId"];
        }
        if (OfmId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای غیر فعال کردن ابتدا یک عضو را انتخاب نمائید";
            return;
        }

        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        try
        {
            OfMeManager.FindByCode(OfmId);
            if (OfMeManager.Count == 1)
            {
                int CurrentOfReId = int.Parse(Utility.DecryptQS(EngFileId.Value));

                if (EOfId == CurrentOfReId)
                {
                    Delete(OfmId, PersonId, EOfId);
                }
                else
                {
                    if (Convert.ToBoolean(OfMeManager[0]["InActive"]))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "عضو مورد نظر غیر فعال می باشد";
                        return;
                    }
                    TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
                    RequestInActivesManager.FindByTableIdTableType(OfmId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember), -1, 0);
                    if (RequestInActivesManager.Count > 0)
                    {
                        if (Convert.ToInt32(RequestInActivesManager[0]["ReqId"]) == CurrentOfReId)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = ("عضو مورد نظر غیر فعال می باشد");
                            return;
                        }
                    }

                    InsertInActive(OfmId, CurrentOfReId, PersonId);
                    CustomAspxDevGridView1.DataBind();
                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
        }

    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        if (!CheckLocker()) return;

        int OfmId = -1;
        int PersonId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            PersonId = (int)row["PersonId"];

        }
        if (OfmId == -1 || PersonId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای تائید درخواست ابتدا یک رکورد را انتخاب نمائید";
            return;
        }

        TSP.DataManager.OfficeMemberManager MemManager = new TSP.DataManager.OfficeMemberManager();
        try
        {
            if (PersonId == Utility.GetCurrentUser_MeId())
            {
                MemManager.FindEngOfficeMemberByOfmCode(OfmId);
                if (!Utility.IsDBNullOrNullValue(MemManager[0]["IsConfirm"]))
                {
                    if (!Convert.ToBoolean(MemManager[0]["IsConfirm"]))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "درخواست مورد نظر قبلاً رد شده است";
                        return;
                    }
                    if (Convert.ToBoolean(MemManager[0]["IsConfirm"]))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "درخواست مورد نظر قبلاً تأیید شده است";
                        return;
                    }
                }

                MemManager[0].BeginEdit();
                MemManager[0]["IsConfirm"] = 1;//تائید درخواست
                MemManager[0]["ConfirmDate"] = Utility.GetDateOfToday();
                MemManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                MemManager[0].EndEdit();
                MemManager.Save();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                CustomAspxDevGridView1.DataBind();
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان پاسخ درخواست برای شما وجود ندارد";
            }
        }
        catch (Exception err)
        {


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
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (!CheckLocker()) return;

        int OfmId = -1;
        int PersonId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            PersonId = (int)row["PersonId"];
        }
        if (OfmId == -1 || PersonId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای رد درخواست ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        TSP.DataManager.OfficeMemberManager MemManager = new TSP.DataManager.OfficeMemberManager();
        try
        {
            if (PersonId == Utility.GetCurrentUser_MeId())
            {
                MemManager.FindEngOfficeMemberByOfmCode(OfmId);
                if (!Utility.IsDBNullOrNullValue(MemManager[0]["IsConfirm"]))
                {
                    if (!Convert.ToBoolean(MemManager[0]["IsConfirm"]))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "درخواست مورد نظر قبلاً رد شده است";
                        return;
                    }
                    if (Convert.ToBoolean(MemManager[0]["IsConfirm"]))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "درخواست مورد نظر قبلاً تأیید شده است";
                        return;
                    }
                }

                MemManager[0].BeginEdit();
                MemManager[0]["IsConfirm"] = 0;//رد درخواست
                MemManager[0]["ConfirmDate"] = Utility.GetDateOfToday();
                MemManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                MemManager[0].EndEdit();
                MemManager.Save();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                CustomAspxDevGridView1.DataBind();
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان پاسخ درخواست برای شما وجود ندارد";
            }
        }
        catch (Exception err)
        {


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

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {

        switch (e.Item.Name)
        {

            case "Attach":
                Response.Redirect("EngOfficeAttachment.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;
            case "EngOffice":
                Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;

            case "Job":
                Response.Redirect("EngOfficeJob.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;
        }
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;

                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                            {
                                if (FirstNmcIdType == 1)
                                {
                                    if (FirstNmcId == Utility.GetCurrentUser_MeId())
                                        return true;
                                }

                            }

                        }

                    }

                }

            }

        }
        return false;


    }

    protected void Delete(int OfmId, int PersonId, int EOfId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        trans.Add(OfMeManager);
        trans.Add(FileManager);
        try
        {
            OfMeManager.FindEngOfficeMemberByPersonId(OfmId);
            trans.BeginSave();

            OfMeManager[0].Delete();
            OfMeManager.Save();

            #region SetMFNo
            TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
            FileManager.FindByCode(EOfId);
            if (FileManager.Count == 1)
            {

                DataTable dtMj = MeMjManager.SelectMemberMasterMajor(PersonId);
                if (dtMj.Rows.Count > 0)
                {
                    int Del = 0;
                    int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
                    //string MFSerialNo = FileManager[0]["MFSerialNo"].ToString();
                    int i = -1;
                    string MFNo = FileManager[0]["MFNo"].ToString();
                    string[] MFNoMajor = MFNo.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                    char[] Code = MFNoMajor[2].ToCharArray();

                    switch (MjId)
                    {
                        case (int)TSP.DataManager.MainMajors.Architecture:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Architecture;
                            Code[i] = Del.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Civil:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Civil;
                            Code[i] = Del.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Electronic:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Electronic;
                            Code[i] = Del.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Mapping:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mapping;
                            Code[i] = Del.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Mechanic:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mechanic;
                            Code[i] = Del.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Traffic:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Traffic;
                            Code[i] = Del.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Urbanism:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Urbanism;
                            Code[i] = Del.ToString()[0];
                            break;
                        default:
                            i = -1;
                            break;

                    }
                    if (i != -1)
                    {
                        // Code[i] = '1';

                        MFNoMajor[2] = new string(Code);//Code.ToString();
                        FileManager[0].BeginEdit();
                        FileManager[0]["MFNo"] = string.Join("-", MFNoMajor);
                        FileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        FileManager[0].EndEdit();
                        FileManager.Save();
                    }
                }


            }

            #endregion

            trans.EndSave();
            CustomAspxDevGridView1.DataBind();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";

        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
        }
    }
    protected void InsertInActive(int OfmId, int EOfId, int PersonId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
        trans.Add(OfMeManager);
        trans.Add(FileManager);
        trans.Add(Manager);
        trans.Add(MeMjManager);
        try
        {
            trans.BeginSave();
            DataRow dr = Manager.NewRow();
            dr["TableId"] = OfmId;
            dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember);
            dr["ReqId"] = EOfId;
            dr["ReqType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);
            dr["InActive"] = 1;
            dr["InActiveRow"] = 0;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            Manager.AddRow(dr);
            Manager.Save();
            #region SetMFNo
            FileManager.FindByCode(EOfId);
            if (FileManager.Count == 1)
            {
                DataTable dtMj = MeMjManager.SelectMemberMasterMajor(PersonId);
                if (dtMj.Rows.Count > 0)
                {
                    int Del = 0;
                    int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
                    int ParentId = int.Parse(dtMj.Rows[0]["FMjParentId"].ToString());

                    //string MFSerialNo = FileManager[0]["MFSerialNo"].ToString();
                    int i = -1;
                    if (!Utility.IsDBNullOrNullValue(FileManager[0]["FileNo"]))
                    {
                        string MFNo = FileManager[0]["FileNo"].ToString();
                        string[] MFNoMajor = MFNo.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                        char[] Code = MFNoMajor[2].ToCharArray();

                        switch (ParentId)
                        {
                            case (int)TSP.DataManager.MainMajors.Architecture:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Architecture;
                                Code[i] = Del.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Civil:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Civil;
                                Code[i] = Del.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Electronic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Electronic;
                                Code[i] = Del.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Mapping:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mapping;
                                Code[i] = Del.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Mechanic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mechanic;
                                Code[i] = Del.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Traffic:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Traffic;
                                Code[i] = Del.ToString()[0];
                                break;
                            case (int)TSP.DataManager.MainMajors.Urbanism:
                                i = (int)TSP.DataManager.DocumentOfficeMeMajor.Urbanism;
                                Code[i] = Del.ToString()[0];
                                break;
                            default:
                                i = -1;
                                break;
                        }
                        if (i != -1)
                        {
                            // Code[i] = '1';

                            MFNoMajor[2] = new string(Code);//Code.ToString();
                            FileManager[0].BeginEdit();
                            FileManager[0]["FileNo"] = string.Join("-", MFNoMajor);
                            FileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            FileManager[0].EndEdit();
                            FileManager.Save();
                        }
                    }
                }
            }

            #endregion
            trans.EndSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
        }

    }
    protected int DeleteInActive(int OfmId, int EOfId, int PersonId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
        trans.Add(OfMeManager);
        trans.Add(FileManager);
        trans.Add(RequestInActivesManager);
        trans.Add(MeMjManager);
        try
        {
            trans.BeginSave();
            int result = 0;  // 0 successful 1 not exist 2 error
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember);
            RequestInActivesManager.FindByTableIdTableType(OfmId, TableType, -1, 0); //(MReId, MlId);
            if (RequestInActivesManager.Count == 1)
            {
                RequestInActivesManager[0].Delete();
                if (RequestInActivesManager.Save() > 0)
                {
                    #region SetMFNo
                    FileManager.FindByCode(EOfId);
                    if (FileManager.Count == 1)
                    {
                        DataTable dtMj = MeMjManager.SelectMemberMasterMajor(PersonId);
                        if (dtMj.Rows.Count > 0)
                        {
                            int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
                            int ParentId = int.Parse(dtMj.Rows[0]["FMjParentId"].ToString());

                            int i = -1;
                            string MFNo = FileManager[0]["FileNo"].ToString();
                            if (string.IsNullOrEmpty(MFNo))
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = ("شماره پروانه دفتر نامشخص می باشد");
                                trans.CancelSave();
                            }
                            string[] MFNoMajor = MFNo.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                            char[] Code = MFNoMajor[2].ToCharArray();

                            switch (ParentId)
                            {
                                case (int)TSP.DataManager.MainMajors.Architecture:
                                    i = (int)TSP.DataManager.DocumentOfficeMeMajor.Architecture;
                                    Code[i] = ParentId.ToString()[0];
                                    break;
                                case (int)TSP.DataManager.MainMajors.Civil:
                                    i = (int)TSP.DataManager.DocumentOfficeMeMajor.Civil;
                                    Code[i] = ParentId.ToString()[0];
                                    break;
                                case (int)TSP.DataManager.MainMajors.Electronic:
                                    i = (int)TSP.DataManager.DocumentOfficeMeMajor.Electronic;
                                    Code[i] = ParentId.ToString()[0];
                                    break;
                                case (int)TSP.DataManager.MainMajors.Mapping:
                                    i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mapping;
                                    Code[i] = ParentId.ToString()[0];
                                    break;
                                case (int)TSP.DataManager.MainMajors.Mechanic:
                                    i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mechanic;
                                    Code[i] = ParentId.ToString()[0];
                                    break;
                                case (int)TSP.DataManager.MainMajors.Traffic:
                                    i = (int)TSP.DataManager.DocumentOfficeMeMajor.Traffic;
                                    Code[i] = ParentId.ToString()[0];
                                    break;
                                case (int)TSP.DataManager.MainMajors.Urbanism:
                                    i = (int)TSP.DataManager.DocumentOfficeMeMajor.Urbanism;
                                    Code[i] = ParentId.ToString()[0];
                                    break;
                                default:
                                    i = -1;
                                    break;
                            }
                            if (i != -1)
                            {
                                MFNoMajor[2] = new string(Code);//Code.ToString();
                                FileManager[0].BeginEdit();
                                FileManager[0]["FileNo"] = string.Join("-", MFNoMajor);
                                FileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                FileManager[0].EndEdit();
                                FileManager.Save();
                            }
                        }
                    }

                    #endregion
                    trans.EndSave();
                    result = 0;
                }
                else result = 2;
            }
            else result = 1;

            return result;
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
            return 2;
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

    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;
        if (EngFileId.Value != null)
        {
            string EOfId = Utility.DecryptQS(EngFileId.Value);
            if (e.GetValue("OfReId") == null)
                return;
            string CurretnEOfId = e.GetValue("OfReId").ToString();
            if (EOfId == CurretnEOfId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "ConfirmDate")
            e.Cell.Style["direction"] = "ltr";
    }
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "ConfirmDate")
            e.Editor.Style["direction"] = "ltr";
    }
}
