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

public partial class Members_Documents_MemberFiles : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
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


        if (!IsPostBack)
        {
            SetDocumentReuestMenueVisibility();
            SetHelpAddress();
            Session["SendBackDataTable_MeFile"] = "";


            btnView.Enabled = true;
            btnView2.Enabled = true;
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;

            ObjdsMemberFile.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
            //TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            //DocMemberFileManager.FindByMeId(Utility.GetCurrentUser_MeId());
            //if (DocMemberFileManager.Count > 0)
            //{
            //    BtnNew.Enabled = false;
            //    btnNew2.Enabled = false;
            //}
            LoadWfHelpPrint();
            SetPageFilter();
            SetGridRowIndex();

            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
        }

        ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming).ToString();
        GridViewMemberFile.DataBind();

        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
        if (MemberManager.Count == 0)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }

        ArrayList DeletedColumnsName = new ArrayList();
        DeletedColumnsName.Add("WFState");

        Session["DeletedColumnsName"] = DeletedColumnsName;
        Session["DataTable"] = GridViewMemberFile.Columns;
        Session["DataSource"] = ObjdsMemberFile;
        Session["Title"] = "پروانه اشتغال به کار شخص حقیقی";
        Session["Header"] = MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString() + " (" + "کد عضویت: " + MemberManager[0]["MeId"].ToString() + ")";
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>Blink('bkImgWarningMsg');</script>");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        if (CheckConditions())
        {
            ResetDocSessions();

            TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
            DataTable dtMemberFileMajor = MemberLicenceManager.SelectByMemberId(Utility.GetCurrentUser_MeId(), 0);
            dtMemberFileMajor.DefaultView.RowFilter = "LicenceCode=" + (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste + " or " + "LicenceCode=" + (int)TSP.DataManager.Licence.kardani;
            if (dtMemberFileMajor.DefaultView.Count > 0)
            {
                Session["WizardHasConnKard"] = true;
            }
            else
            {
                Session["WizardHasConnKard"] = false;
            }

            Response.Redirect("WizardDocOath.aspx");
        }
    }
    protected void btnQualification_Click(object sender, EventArgs e)
    {
        if (CheckDocQualificationConditions())
        {
            ResetQualification();

            Response.Redirect("WizardQualificationOath.aspx");
        }
    }

    protected void btnRevival_Click(object sender, EventArgs e)
    {
        if (CheckDocRevivalConditions())
        {
            ResetRevival();

            Response.Redirect("WizardRevivalOath.aspx");
        }
    }

    protected void btnUpgrade_ServerClick(object sender, EventArgs e)
    {
        try
        {
            ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForDocumentUpgrade(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
            if (!Convert.ToBoolean(Result[0]))
            {
                ShowMessage(Result[1].ToString());
                return;
            }
            ResetUpgrad();

            Response.Redirect("WizardUpgradeOath.aspx");
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازیابی اطلاعات ایجاد شده است.");
            return;
        }


    }


    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int MfId = -1;
        int focucedIndex = GridViewMemberFile.FocusedRowIndex;

        if (focucedIndex <= -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "جهت ویرایش اطلاعات یک  رکورد را انتخاب نمایید.";
            return;
        }
        DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
        MfId = (int)row["MfId"];
        //////////int RequesterType = int.Parse(row["RequesterType"].ToString());
        int IsConfirm = int.Parse(row["IsConfirm"].ToString());
        if (IsConfirm != 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "بدلیل پایان روند بررسی درخواست انتخاب شده امکان ویرایش اطلاعات مربوط به آن وجود ندارد";
            return;
        }
        ////////////if (RequesterType != (int)TSP.DataManager.DocumentRequesterType.Member)
        ////////////{
        ////////////    this.DivReport.Visible = true;
        ////////////    this.LabelWarning.Text = "بدلیل ثبت درخواست توسط کارمندان سازمان شما قادر به ویرایش اطلاعات نمی باشید.";
        ////////////    return;
        ////////////}
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
        if (MemberManager.Count != 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            return;
        }
        //if ((bool)MemberManager[0]["IsLock"] == true)
        //{
        //    TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
        //    string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 0, 1);
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
        //    return;
        //}              
        int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
        if (MRsId != 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان ویرایش برای شما وجود ندارد.عضویت شما در وضعیت لغو شده یا در جریان می باشد.";
            return;
        }
        if (!CheckPermitionForEdit(MfId))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان ویرایش برای شما وجود ندارد.";
            return;
        }
        NextPage("Edit");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        int MfId = -1;
        int focucedIndex = GridViewMemberFile.FocusedRowIndex;

        if (focucedIndex > -1)
        {

            DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
            MfId = (int)row["MfId"];
            if (CheckPermitionForDelete(MfId))
            {

                DeleteRequest(MfId);
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان لغو درخواست برای شما وجود ندارد.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "جهت لغو درخواست یک  رکورد را انتخاب نمایید.";
        }
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        //if (GridViewMemberFile.FocusedRowIndex > -1)
        //{
        //    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        //    DataRow DocMeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        //    int TableId = int.Parse(DocMeFileRow["MfId"].ToString());
        //    int WorkFlowCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
        //    Response.Redirect("WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()));
        //}
        //else
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        //}
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
            DataRow DocMeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            int TableId = int.Parse(DocMeFileRow["MfId"].ToString());
            int WorkFlowCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
            int PostId = int.Parse(GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex)["MfId"].ToString());
            string GridFilterString = GridViewMemberFile.FilterExpression;
            String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" +
                "&PostId=" + Utility.EncryptQS(PostId.ToString());
            Response.Redirect("../WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
        }
        else
            ShowMessage("ابتدا یک درخواست را انتخاب نمایید");
    }
    

    //درخواست تغییرات
    protected void btnChange_Click(object sender, EventArgs e)
    {


        if (IsPageRefresh)
            return;
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
        if (MemberManager.Count != 1)
        {
            ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
            return;
        }
        if ((bool)MemberManager[0]["IsLock"] == true)
        {
            string lockers = Utility.GetFormattedObject(Session["MemberLockers"]);
            ShowMessage("به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.");
            return;
        }
        int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
        if (MRsId != 1)
        {
            ShowMessage("امکان درخواست ارتقاء پایه برای شما وجود ندارد.عضویت شما در وضعیت لغو شده یا در جریان می باشد.");
            return;
        }
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), 0, 0);
        if (DocMemberFileManager.Count > 0)
        {
            ShowMessage("بدلیل وجود درخواست درجریان برای پروانه انتخاب شده،امکان ثبت درخواست جدید وجود ندارد.");
            return;
        }
        DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), 0, 1);
        if (dtDocMeFile.Rows.Count <= 0)
        {
            ShowMessage("بدلیل نامشخص بودن وضعیت پروانه امکان ثبت درخواست جدید وجود ندارد.");
            return;
        }
        NextPage("Change");
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewMemberFile.Columns["WFState"].Visible = false;
        GridViewExporter.FileName = "MemberDocument";

        GridViewExporter.WriteXlsToResponse(true);
        GridViewMemberFile.Columns["WFState"].Visible = true;
    }

    //*************************************************************************************************************
    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            int MfId = int.Parse(MeFileRow["MfId"].ToString());
            int MeFileTableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
            if (e.Parameter == "Send")
            {
                SendMemberFileDocToNextStep(MfId);
                GridViewMemberFile.DataBind();
            }
            else
            {
                SelectSendBackTask(MeFileTableType, MfId);
            }
        }
        else
        {
            lblError.Text = "ردیفی انتخاب نشده است.";
        }
    }

    //*************************************************************************************************************
    protected void GridViewMemberFile_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "RegDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "ExpireDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "MFNo":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewMemberFile_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "RegDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "ExpireDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "MFNo":
                e.Cell.Style["direction"] = "ltr";
                break;
        }

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMemberFile.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMemberFile.Columns["WFState"], "btnWFState");
            if (btnWFState != null)
            {
                if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
                {
                    btnWFState.ToolTip = "تعریف نشده";
                    btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
                    return;
                }
                else if (!Utility.IsDBNullOrNullValue(e.GetValue("TaskName")))
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                }

                //if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
                //{
                //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                //    btnWFState.ImageUrl = "~/Images/WFStart.png";
                //}
                //else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                //{
                //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                //    btnWFState.ImageUrl = "~/Images/WFInProcess.png";
                //}
                //else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                //{
                //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                //    btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
                //}
                //else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                //{
                //    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                //    btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
                //}
                //else
                //{
                //}
            }
        }
    }

    protected void GridViewMemberFile_PageIndexChanged(object sender, EventArgs e)
    {
        SetGridRowIndex();
    }

    protected void GridViewMemberFile_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridViewDetailRowEventArgs e)
    {
        GridViewMemberFile.FocusedRowIndex = e.VisibleIndex;
    }
    #endregion

    #region Methods
    //private void NextPage(string Mode)
    //{
    //    int MfId = -1;
    //    int focucedIndex = GridViewMemberFile.FocusedRowIndex;

    //    if (focucedIndex > -1)
    //    {
    //        DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
    //        MfId = (int)row["MfId"];
    //    }
    //    if (MfId == -1 && Mode != "New")
    //    {
    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
    //    }
    //    else
    //    {
    //        string GridFilterString = GridViewMemberFile.FilterExpression;

    //        if (Mode == "New")
    //        {
    //            MfId = -1;
    //            Response.Redirect("AddMemberFile.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
    //            //  Response.Redirect("AddMemberFile.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
    //        }
    //        else
    //        {
    //            Response.Redirect("AddMemberFile.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
    //        }
    //    }
    //}

    private void NextPage(string Mode)
    {
        int MfId = -1;
        int MeId = -1;
        int focucedIndex = -1;
        int PostId = -1;
        // string SearchFilterString = GenerateFilterString();
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            PostId = (int)GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex)["MfId"];
        }
        if (Mode == "View")
        {
            if (PostId == -1)
            {
                ShowMessage("برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            MfId = PostId;
            MeId = Utility.GetCurrentUser_MeId();
        }
        else
        {
            focucedIndex = GridViewMemberFile.FocusedRowIndex;
            if (focucedIndex > -1)
            {
                DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
                MeId = (int)row["MeId"];
                if (Mode != "New" && Mode != "View" && Mode != "Edit")
                {
                    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                    DocMemberFileManager.FindByDocumentType(MeId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
                    if (DocMemberFileManager.Count == 1)
                    {
                        if (Convert.ToInt32(DocMemberFileManager[0]["IsConfirm"]) != 2)//درخواست اولیه تایید نشده
                        {
                            DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
                            if (dtDocMeFile.Rows.Count > 0)
                            {
                                MfId = (int)dtDocMeFile.Rows[0]["MfId"];
                            }
                        }
                        else
                            MfId = MfId = Convert.ToInt32(DocMemberFileManager[0]["MfId"]);
                    }
                    else
                    {
                        DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
                        if (dtDocMeFile.Rows.Count <= 0)
                        {
                            ShowMessage("بدلیل نامشخص بودن وضعیت پروانه امکان ثبت درخواست جدید وجود ندارد.");
                            return;
                        }
                        MfId = (int)dtDocMeFile.Rows[0]["MfId"];
                    }
                }
                if (Mode == "Edit")
                {
                    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                    DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 0);
                    if (dtDocMeFile.Rows.Count > 0)
                    {
                        MfId = (int)dtDocMeFile.Rows[0]["MfId"];
                    }
                }

            }
        }

        if (MfId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            string GridFilterString = GridViewMemberFile.FilterExpression;
            if (Mode == "New")
            {
                MeId = -1;
                MfId = -1;
                Response.Redirect("AddMemberFile.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MeId=" + Utility.EncryptQS(MeId.ToString()));
            }
            else
            {
                Response.Redirect("AddMemberFile.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MeId=" + Utility.EncryptQS(MeId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&PostId=" + Utility.EncryptQS(PostId.ToString()));
            }
        }
    }

    private void SelectSendBackTask(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
        TSP.DataManager.DocMemberExamManager DocMemberExamManager = new TSP.DataManager.DocMemberExamManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count <= 0)
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "عملیاتی برای پرونده پروانه انتخاب شده انجام نشده است.";
            return;
        }
        int CurrentTaskOrder = int.Parse(dtWorkFlowLastState.Rows[0]["TaskOrder"].ToString());
        int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
        int CurrentSubOrder = int.Parse(dtWorkFlowLastState.Rows[0]["SubOrder"].ToString());
        int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
        int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
        int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
        int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
        int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;


        if (CurrentTaskCode != DocMeFileSaveInfoTaskCode && CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.CompleteMemebershipData)
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
            return;
        }
        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count <= 0)
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "عملیاتی برای پرونده پروانه انتخاب شده انجام نشده است.";
            return;
        }
        int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
        int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
        int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());

        if (FirstNmcIdType != 1)
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
            return;
        }
        if (FirstNmcId != Utility.GetCurrentUser_MeId())
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
            return;
        }
        DocMemberFileManager.FindByCode(TableId, 0);
        if (DocMemberFileManager.Count != 1)
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            return;
        }
        if (DocMemberFileManager[0]["Type"].ToString() == "0")
        {
            DataTable dtMeExam = DocMemberExamManager.SelectByMemberFile(TableId);
            if (dtMeExam.Rows.Count <= 0)
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "اطلاعات مربوط به آزمون های پذیرفته شده وارد نشده است.";
                return;
            }
        }

        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.FindAccountingFishForMeDocument(TableId, Utility.GetCurrentUser_MeId());
        AccountingManager.DataTable.DefaultView.RowFilter = "TableTypeId=" + TableId.ToString();
        if (AccountingManager.DataTable.DefaultView.Count == 0)
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "خطایی در بازیابی اطلاعات ایجاد شده است.";
            return;
        }
        if (Convert.ToInt32(AccountingManager[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.SaveInDB)
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "به دلیل عدم پرداخت فیش قادر به ارسال درخواست به مراحل بعد نمی باشید.جهت پرداخت از طریق منوی ''واحدامور مالی==> مدیریت فیش های پرداخت نشده پرداخت الکترونیکی'' اقدام نمایید.";
            return;
        }

        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument);
        if (WorkFlowTaskManager.DataTable.Rows.Count > 0)
        {
            Session["SendBackDataTable_MeFile"] = WorkFlowTaskManager.DataTable;
            cmbSendBackTask.DataSource = WorkFlowTaskManager.DataTable;
            cmbSendBackTask.ValueField = "TaskId";
            cmbSendBackTask.TextField = "TaskName";
            cmbSendBackTask.DataBind();
            cmbSendBackTask.SelectedIndex = 0;
            PanelSaveSuccessfully.Visible = false;
            PanelMain.Visible = true;
        }
        else
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "عملیات بعد در گردش کار نامشخص است.";
        }

    }

    private void SendMemberFileDocToNextStep(int MfId)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager = new TSP.DataManager.DocMemberExamDetailManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();

        DocMemberFileManager.FindDocument(Utility.GetCurrentUser_MeId(), MfId);
        DocMemberExamDetailManager.SelectByMFId(MfId);
        if (DocMemberFileManager.Count != 1)
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "خطایی در بازیابی اطلاعات رخ داده است.";
            return;
        }
        ////int MfType = Convert.ToInt32(DocMemberFileManager[0]["Type"].ToString());
        ////if (MfType == (int)TSP.DataManager.DocumentOfMemberRequestType.New)
        ////{
        ////    if (DocMemberExamDetailManager.Count > 0)
        ////    {

        ////        DocMemberExamDetailManager.DataTable.DefaultView.RowFilter = "TTypeId=" + (int)TSP.DataManager.DocTestType.Implement;
        ////        if (DocMemberExamDetailManager.DataTable.DefaultView.Count > 0)
        ////        {
        ////            DocMemberExamDetailManager.DataTable.DefaultView.RowFilter = "TTypeId<>" + (int)TSP.DataManager.DocTestType.Implement;
        ////            if (DocMemberExamDetailManager.DataTable.DefaultView.Count == 0)
        ////            {
        ////                PanelSaveSuccessfully.ClientVisible = true;
        ////                PanelMain.ClientVisible = false;
        ////                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
        ////                lblInstitueWarning.Text = "امکان ثبت زمینه آزمون اجرا به تنهایی وجود ندراد.ثبت نمودن سایر زمینه های آزمون بجز اجرا که در آن قبول شده اید الزامی می باشد. ";
        ////                return;
        ////            }
        ////        }
        ////        DocMemberExamDetailManager.DataTable.DefaultView.RowFilter = "";
        ////    }
        ////}
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        //int WorkflowCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
        //int MeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        //int NextStepTaskId = -1;

        //DataTable dtNextTopTask = WorkFlowTaskManager.SelectNextTopSteps(TableType, MeFileSaveInfoTaskCode, (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming);
        //if (dtNextTopTask.Rows.Count > 0)
        //{
        //int NextStepTaskCode = int.Parse(dtNextTopTask.Rows[0]["TaskCode"].ToString());
        //WorkFlowTaskManager.FindByTaskCode(NextStepTaskCode);
        //if (WorkFlowTaskManager.Count == 1)
        //{
        //    NextStepTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        //}

        DataTable dtSendBack = (DataTable)Session["SendBackDataTable_MeFile"];
        cmbSendBackTask.DataSource = dtSendBack;
        cmbSendBackTask.ValueField = "TaskId";
        cmbSendBackTask.TextField = "TaskName";
        cmbSendBackTask.DataBind();

        int SelectedTaskId = int.Parse(cmbSendBackTask.SelectedItem.Value.ToString());
        //if (SelectedTaskId == NextStepTaskId)
        //{
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);
        //TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();

        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocMemberFileManager);
        // DocMemberFileManager.ClearBeforeFill = true;

        int NmcId = Utility.GetCurrentUser_MeId();
        int NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId;
        if (NmcId < 0)
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "به علت نامشخص بودن سمت شما در چارت سازمانی قادر به ارسال پرونده به مرحله بعد نمی باشید.";
            return;
        }
        TransactionManager.BeginSave();
        string Url = "<a href='../Document/AddMemberFile.aspx?MeId=" + "" + "&MFId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS("View") + "' target=_blank>اینجا کلیک کنید</a>";
        string MsgContent = "";
        int SendDocStateId = WorkFlowStateManager.SendDocToNextStep(TableType, MfId, SelectedTaskId, txtDescription.Text, NmcId, NmcIdType, Utility.GetCurrentUser_UserId(), MsgContent, Url);
        switch (SendDocStateId)
        {
            case -6:
                TransactionManager.CancelSave();
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "امکان ارسال پرونده پروانه به مرحله جاری وجود ندارد.";
                break;
            case -4:
                TransactionManager.CancelSave();
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "خطایی در ذخیره انجام شد.";
                break;
            case -5:
                TransactionManager.CancelSave();
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "برای پرونده انتخاب شده هیچ عملیاتی انجام نشده است.";
                break;
            case -8:
                TransactionManager.CancelSave();
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "انجام دهنده عملیات بعد نامشخص می باشد.";
                break;
            default:
                DocMemberFileManager[0].BeginEdit();
                DocMemberFileManager[0]["CurrentWFStateId"] = SendDocStateId;
                DocMemberFileManager[0].EndEdit();
                DocMemberFileManager.Save();
                TransactionManager.EndSave();
                lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
                lblInstitueWarning.Text = "ذخیره انجام شد.";
                PanelMain.ClientVisible = false;
                PanelSaveSuccessfully.ClientVisible = true;
                //  GridViewMemberFile.DataBind();
                break;
        }


        //}
        //else
        //{
        //    PanelSaveSuccessfully.ClientVisible = true;
        //    PanelMain.ClientVisible = false;
        //    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
        //    lblInstitueWarning.Text = "شما قادر به ارسال پرونده به مرحله انتخاب شده نیستید.";

        //}
        GridViewMemberFile.DataBind();
        //}
        //else
        //{

        //}
    }

    #region Reset Session
    private void ResetDocSessions()
    {
        Session["WizardDocOath"] =
        Session["WizardDocExam"] =
        Session["WizardDocMajor"] =
        Session["WizardDocResposblity"] =
        Session["WizardDocPeriods"] =
        Session["WizardDocJob"] =
        Session["WizardDocSummary"] =
        Session["JobFileURL"] =
        Session["WizardDocJobConfirm"] =
        Session["JobGrdURL"] =
        Session["ACCFileURL"] =
        Session["FishFileURL"] =
        Session["chbIAgree"] =
        Session["ExamFileImgURLSoource"] =
        Session["ImgTaxOfficeLetter"] = null;
    }
    private void ResetQualification()
    {
        Session["WizardDocQualificationExam"] =
        Session["WizardDocQualificationSummary"] =
        Session["WizardDocQualificationOath"] =
        Session["WizardQualificationJobConfirm"] =
        Session["DocQualificationJobFileURL"] =
        Session["DocQualificationJobGrdURL"] =
        Session["WizardQualificationchbIAgree"] =
        Session["WizardQualificationImgTaxOfficeLetter"] =
        Session["WizardQualificationImgfrontDoc"] =
Session["WizardQualificationImgBackDoc"] =
        null;
    }

    private void ResetRevival()
    {
        Session["WizardDocRevivalSummary"] =
        Session["WizardDocRevivalOath"] =
        Session["WizardRevivalJobConfirm"] =
        Session["DocRevivalJobFileURL"] =
        Session["DocRevivalJobGrdURL"] =
        Session["WizardRevivalImgTaxOfficeLetter"] =
        Session["WizardRevivalImgfrontDoc"] =
        Session["WizardRevivalImgBackDoc"] =
        Session["ImgPeriodImage"] =
        Session["ImgTaxOfficeLetter"] =
        Session["WizardRevivalCivilLicence"] = null;
    }

    private void ResetUpgrad()
    {
        Session["WizardUpgradeHasConnKard"] =
       Session["WizardDocUpgradeOath"] =
       Session["WizardUpgradeImgfrontDoc"] =
       Session["WizardUpgradeImgBackDoc"] =
       Session["WizardUpgradeImgTaxOfficeLetter"] =
       Session["WizardDocUpgradeSummary"] =
       Session["WizardUpgradeJobConfirm"] =
       Session["WizardUpgradeKardFileURL"] =
       Session["ImgPeriodImage"] =
       Session["DocUpgradeJobFileURL"] =
       Session["DocUpgradeJobGrdURL"] = null;
    }
    #endregion
    #region Request CheckConditions
    private Boolean CheckConditions()
    {
        try
        {
            ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForNewDocument(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
            if (!Convert.ToBoolean(Result[0]))
            {
                ShowMessage(Result[1].ToString());
                return false;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازیابی اطلاعات تصاویر مربوطه ایجاد شده است.");
            return false;
        }

        return true;
    }

    private Boolean CheckDocQualificationConditions()
    {
        try
        {
            ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForDocumentQualification(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
            if (!Convert.ToBoolean(Result[0]))
            {
                ShowMessage(Result[1].ToString());
                return false;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازیابی اطلاعات تصاویر مربوطه ایجاد شده است.");
            return false;
        }

        return true;
    }

    private Boolean CheckDocRevivalConditions()
    {
        try
        {
            ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForDocumentRevival(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
            if (!Convert.ToBoolean(Result[0]))
            {
                ShowMessage(Result[1].ToString());
                return false;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازیابی اطلاعات تصاویر مربوطه ایجاد شده است.");
            return false;
        }

        return true;
    }
    #endregion

    private void SetError(Exception err)
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

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count <= 0)
        {
            return false;
        }
        TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        if (TaskOrder == 0)
        {
            return false;
        }
        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count <= 0)
        {
            return false;
        }
        int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
        //int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
        //int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
        //int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
        //int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
        int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;

        if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
        {
            return true;
            #region Comment
            //DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
            //if (dtWorkFlowState.Rows.Count > 0)
            //{
            //int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
            //int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
            //int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
            //if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
            //{
            //if (FirstNmcIdType == 1)
            //{
            //if (FirstNmcId == Utility.GetCurrentUser_MeId())
            //{
            //return true;
            //}
            //else
            //    return false;
            //}
            //else
            //{
            //    return false;
            //}
            //}
            //else
            //{
            //    return false;
            //}
            //}
            //else
            //{
            //    return false;
            //}
            #endregion

        }
        else
        {
            return false;
        }
    }

    private Boolean CheckPermitionForDelete(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
                int WfCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
                DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
                if (dtState.Rows.Count == 1)
                {
                    int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;

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
                                if (FirstNmcId == Utility.GetCurrentUser_MeId() && FirstNmcIdType == 1)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void DeleteRequest(int MfId)
    {
        int MeId = Utility.GetCurrentUser_MeId();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager = new TSP.DataManager.DocMemberExamDetailManager();
        TSP.DataManager.DocMemberExamManager DocMemberExamManager = new TSP.DataManager.DocMemberExamManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.DocOffJobHistoryQualityManager DocOffJobHistoryQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();
        TransactionManager.Add(RequestInActivesManager);
        TransactionManager.Add(DocMemberExamDetailManager);
        TransactionManager.Add(DocMemberExamManager);
        TransactionManager.Add(DocMemberFileDetailManager);
        TransactionManager.Add(DocMemberFileMajorManager);
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(AccountingManager);
        TransactionManager.Add(DocMemberFileJobConfirmationManager);
        try
        {
            TransactionManager.BeginSave();

            #region DocDetails
            DataTable dtDocDetail = DocMemberFileDetailManager.SelectById(MfId, MeId, -1);
            if (dtDocDetail.Rows.Count > 0)
            {
                for (int i = 0; i < dtDocDetail.Rows.Count; i++)
                {
                    int MfdId = (int)dtDocDetail.Rows[i]["MfdId"];
                    DocMemberFileDetailManager.FindByCode(MfdId);
                    if (DocMemberFileDetailManager.Count == 1)
                    {
                        if (Convert.ToInt32(DocMemberFileDetailManager[0]["MfId"]) == MfId)
                        {
                            DocMemberFileDetailManager[0].Delete();
                            if (DocMemberFileDetailManager.Save() <= 0)
                            {
                                TransactionManager.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                        return;
                    }
                }
            }
            #endregion

            #region Delete Majors
            DataTable dtDocMajor = DocMemberFileMajorManager.SelectMemberFileById(MfId, MeId, -1);
            if (dtDocMajor.Rows.Count > 0)
            {
                for (int i = 0; i < dtDocMajor.Rows.Count; i++)
                {
                    int MFMjId = (int)dtDocMajor.Rows[i]["MFMjId"];
                    DocMemberFileMajorManager.FindByCode(MFMjId);
                    if (DocMemberFileMajorManager.Count == 1)
                    {
                        if (MfId == Convert.ToInt32(DocMemberFileMajorManager[0]["MFId"]))
                        {
                            DocMemberFileMajorManager[0].Delete();
                            if (DocMemberFileMajorManager.Save() <= 0)
                            {
                                TransactionManager.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                        return;
                    }
                }
            }
            #endregion

            #region DocMemberFileJobConfirmationManager
            DocMemberFileJobConfirmationManager.FindByMfIdForDelete(MfId);
            int cntJobConfirm=DocMemberFileJobConfirmationManager.Count;
            for(int i=0;i<cntJobConfirm;i++)
            {
                DocMemberFileJobConfirmationManager[0].Delete();
                if (DocMemberFileJobConfirmationManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    return;
                }
                DocMemberFileJobConfirmationManager.DataTable.AcceptChanges();
            }
            #endregion

            #region DeleteExams
            DataTable dtMeExam = DocMemberExamManager.SelectDocMemberFileExamForDelete(MfId);
            if (dtMeExam.Rows.Count > 0)
            {
                for (int i = 0; i < dtMeExam.Rows.Count; i++)
                {
                    int MExmId = (int)dtMeExam.Rows[i]["MExmId"];
                    DocMemberExamDetailManager.SelectByExam(MExmId);
                    int cntDetail = DocMemberExamDetailManager.Count;
                    for (int j = 0; j < cntDetail; j++)
                    {
                        DocMemberExamDetailManager[0].Delete();
                        if (DocMemberExamDetailManager.Save() <= 0)
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                            return;
                        }
                        DocMemberExamDetailManager.DataTable.AcceptChanges();
                    }

                    DocMemberExamManager.FindByCode(MExmId);
                    if (DocMemberExamManager.Count == 1)
                    {
                        DocMemberExamManager[0].Delete();
                        if (DocMemberExamManager.Save() <= 0)
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                            return;
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                        return;
                    }
                }
            }
            #endregion

            #region Delete WFState
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete((int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming, MfId);
            if (WorkFlowStateManager.Count > 0)
            {
                int count = WorkFlowStateManager.Count;
                for (int i = 0; i < count; i++)
                {
                    WorkFlowStateManager[0].Delete();
                    if (WorkFlowStateManager.Save() <= 0)
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                        return;
                    }
                    WorkFlowStateManager.DataTable.AcceptChanges();
                }
            }

            #endregion

            #region Delete MemeberFile
            Boolean CheckHasInActiveDoc = false;
            DocMemberFileManager.FindByCode(MfId, 0);
            if (DocMemberFileManager.Count == 1)
            {
                if (Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.New
                    || Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.TransferAndRevival
                    || Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.Transfer)
                    CheckHasInActiveDoc = true;

                DocMemberFileManager[0].Delete();
                if (DocMemberFileManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    return;
                }
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                return;
            }
            if (CheckHasInActiveDoc)
            {
                DocMemberFileManager.DataTable.AcceptChanges();
                DocMemberFileManager.FindByMeId(MeId);
                if (DocMemberFileManager.Count > 0
                  && Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.New
                    && Convert.ToInt32(DocMemberFileManager[0]["InActive"]) == 1)
                {
                    DocMemberFileManager[DocMemberFileManager.Count - 1].BeginEdit();
                    DocMemberFileManager[DocMemberFileManager.Count - 1]["InActive"] = 0;
                    DocMemberFileManager[DocMemberFileManager.Count - 1].EndEdit();
                    DocMemberFileManager.Save();
                }
            }
            #endregion

            int TableType = -1;
            TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
            #region Delete Fish

            AccountingManager.FindByTableTypeId(MfId, TableType);
            int cnt = AccountingManager.Count;
            for (int i = 0; i < cnt; i++)
            {
                AccountingDetailManager.FindByAccountingId(Convert.ToInt32(AccountingManager[0]["AccountingId"]));
                int cntAccDetail = AccountingDetailManager.Count;
                for (int j = 0; j < cntAccDetail; j++)
                {
                    AccountingDetailManager[0].Delete();
                    AccountingDetailManager.Save();
                    AccountingDetailManager.DataTable.AcceptChanges();
                }
                AccountingManager[0].Delete();
                AccountingManager.Save();
                AccountingManager.DataTable.AcceptChanges();
            }
            #endregion
            
            #region DeleteInActives
            RequestInActivesManager.FindByReqId(MfId, TableType);
            int cntInActive = RequestInActivesManager.Count;
            for (int i = 0; i < cntInActive; i++)
            {
                RequestInActivesManager[0].Delete();
                RequestInActivesManager.Save();
                RequestInActivesManager.DataTable.AcceptChanges();
            }
            #endregion
            TransactionManager.EndSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لغو در خواست انجام گرفت.";

            GridViewMemberFile.DataBind();
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetDeleteError(err);
        }
    }

    private void SetDeleteError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
        }
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.PortalDocumentMemberFile).ToString());
    }

    #region Set Grid Index

    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());

                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewMemberFile.FilterExpression = GrdFlt;
            }
        }

    }

    private int SetGridRowIndex()
    {
        int Index = -1;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["PostId"]))
            {

                int PostKeyValue = int.Parse(Utility.DecryptQS(Request.QueryString["PostId"].ToString()));

                GridViewMemberFile.DataBind();
                Index = GridViewMemberFile.FindVisibleIndexByKeyValue(PostKeyValue);
                int PageIndex = -1;
                if (Index >= 0)
                    PageIndex = Index / GridViewMemberFile.SettingsPager.PageSize;
                if (PageIndex >= 0)
                    GridViewMemberFile.PageIndex = PageIndex;
                if (Index >= 0)
                {
                    GridViewMemberFile.FocusedRowIndex = Index;

                }
            }
        }
        return Index;
    }
    #endregion

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }


    private void SetDocumentReuestMenueVisibility()
    {
        TSP.DataManager.DocMemberRequestVisibilityManager DocMemberRequestVisibilityManager = new TSP.DataManager.DocMemberRequestVisibilityManager();
        TSP.DataManager.DocMemberRequestVisibilityExceptionManager DocMemberRequestVisibilityExceptionManager = new TSP.DataManager.DocMemberRequestVisibilityExceptionManager();

        DocMemberRequestVisibilityManager.SelectDocMemberRequest(TSP.DataManager.DocMemberRequestVisibilityReqType.NewDocumentRequest, Utility.GetDateOfToday(), 0);
        if (DocMemberRequestVisibilityManager.Count > 0)
        {
            BtnNew.Visible = btnNew2.Visible = false;
            DocMemberRequestVisibilityExceptionManager.SelectDocMemberRequestException(Utility.GetCurrentUser_MeId(), TSP.DataManager.DocMemberRequestVisibilityReqType.NewDocumentRequest, Utility.GetDateOfToday(), 0);
            if (DocMemberRequestVisibilityExceptionManager.Count > 0)
                btnNew2.Visible = BtnNew.Visible = true;
        }
        else
            BtnNew.Visible = btnNew2.Visible = true;

        //DocMemberRequestVisibilityManager.SelectDocMemberRequest(TSP.DataManager.DocMemberRequestVisibilityReqType.QualificationRequest, Utility.GetDateOfToday(), 0);
        //if (DocMemberRequestVisibilityManager.Count > 0)
        //{
        //    btnQualification.Visible = linkbtnQualification.Visible = false;
        //    DocMemberRequestVisibilityExceptionManager.SelectDocMemberRequestException(Utility.GetCurrentUser_MeId(), TSP.DataManager.DocMemberRequestVisibilityReqType.QualificationRequest, Utility.GetDateOfToday(), 0);
        //    if (DocMemberRequestVisibilityExceptionManager.Count > 0)
        //        btnQualification.Visible = linkbtnQualification.Visible = true;
        //}
        //else
        //    btnQualification.Visible = linkbtnQualification.Visible = true;
    }

    void LoadWfHelpPrint()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        DataTable dt = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming));
        DataTable dt1 = dt.Clone();
        DataTable dt2 = dt.Clone();
        DataTable dt3 = dt.Clone();
        int count = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (count % 3 == 0) { dt1.Rows.Add(dr.ItemArray); }
            if (count % 3 == 1) { dt2.Rows.Add(dr.ItemArray); }
            if (count % 3 == 2) { dt3.Rows.Add(dr.ItemArray); }
            count++;
        }
        if (dt1.Rows.Count != 0)
        {
            RepeaterWfHepPrint1.DataSource = dt1;
            RepeaterWfHepPrint1.DataBind();
        }
        if (dt2.Rows.Count != 0)
        {
            RepeaterWfHepPrint2.DataSource = dt2;
            RepeaterWfHepPrint2.DataBind();
        }
        if (dt3.Rows.Count != 0)
        {
            RepeaterWfHepPrint3.DataSource = dt3;
            RepeaterWfHepPrint3.DataBind();
        }
    }
    #endregion
}
