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
using System.IO;
using DevExpress.Web;

public partial class Employee_MembersRegister_MemberAttachment : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
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


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["MeAttachUpload"] = null;
            Session["MeAttachUploadName"] = null;


            TSP.DataManager.Permission per = TSP.DataManager.MemberManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnDelete.Enabled = per.CanEdit;
            btnDelete2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew;
            CustomAspxDevGridView1.Visible = per.CanView;

            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["MeId"]))
            {
                Response.Redirect("Members.aspx");
                return;
            }
            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                MemberId.Value = Server.HtmlDecode(Request.QueryString["MeId"]).ToString();
                HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"]).ToString();
                MemberRequest.Value = Server.HtmlDecode(Request.QueryString["MReId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string MeId = Utility.DecryptQS(MemberId.Value);
            string Mode = Utility.DecryptQS(HDMode.Value);
            string MReId = Utility.DecryptQS(MemberRequest.Value);


            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            CheckWorkFlowPermission();

            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            CustomAspxDevGridView1.DataSource = attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, int.Parse(MReId), (short)TSP.DataManager.AttachType.Attachments);
            CustomAspxDevGridView1.DataBind();


            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }

        FillMemberName();
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (!flp.HasFile)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = " فایل مورد نظر را انتخاب نمایید";
        //    return;
        //}

        if (IsPageRefresh)
            return;

        string fileNameImg = "";

        bool AxImg = false;
        try
        {
            TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();
            DataRow dr = attManager.NewRow();
            dr["TtId"] = (int)TSP.DataManager.TableCodes.MemberRequest;
            dr["RefTable"] = Utility.DecryptQS(MemberRequest.Value);
            dr["AttId"] = (int)TSP.DataManager.AttachType.Attachments;
            dr["IsValid"] = 1;
            dr["Description"] = txtDesc.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModfiedDate"] = DateTime.Now;

            if (!Utility.IsDBNullOrNullValue(Session["MeAttachUpload"]))
            {
                dr["FilePath"] = "~/Image/Members/Attachments/" + System.IO.Path.GetFileName(Session["MeAttachUpload"].ToString());
                dr["FileName"] = System.IO.Path.GetFileName(Session["MeAttachUpload"].ToString());
                AxImg = true;
            }

            //try
            //{
            //    //extension = Path.GetExtension(Session["MeAttachUpload"].ToString());
            //    //extension = extension.ToLower();

            //    // if (extension == ".jpg" || extension == ".gif")
            //    // {
            //    if (Session["MeAttachUpload"] != null)
            //    {
            //        //img = flp.FileBytes;
            //        //fileNameImg = Path.GetFileNameWithoutExtension(Session["MeAttachUpload"].ToString()) + "_" + Utility.GenRandomNum() + extension;
            //        fileNameImg = Path.GetFileName(Session["MeAttachUpload"].ToString());
            //        //pathAx = Server.MapPath("~/image/Temp/");
            //        //flp.SaveAs(pathAx + fileNameImg);

            //        // dr["AtContent"] = img;
            //        //dr["AtContent"] = DBNull.Value;
            //        //dr["FilePath"] = "~/Image/Members/Attachments/" + fileNameImg;
            //        dr["FilePath"] = "~/Image/Members/Attachments/" + Path.GetFileName(Session["MeAttachUpload"].ToString());
            //        dr["FileName"] = Session["MeAttachUploadName"];

            //        AxImg = true;
            //    }
            //    else
            //    {
            //        this.DivReport.Visible = true;
            //        this.LabelWarning.Text = "فایل مورد نظر را انتخاب نمایید";
            //        return;

            //    }
            //    // }
            //    imgEndUploadImg.ClientVisible = false;
            //}
            //catch
            //{
            //    this.DivReport.Visible = true;
            //    this.LabelWarning.Text = " خطایی در ذخیره رخ داده است";
            //}

            attManager.AddRow(dr);
            int cnt = attManager.Save();
            if (cnt == 1)
            {

                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";

                txtDesc.Text = "";
                CustomAspxDevGridView1.DataSource = attManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, int.Parse(Utility.DecryptQS(MemberRequest.Value)), (short)TSP.DataManager.AttachType.Attachments);
                CustomAspxDevGridView1.DataBind();

                if (Session["MenuArrayList"] != null)
                {
                    ArrayList arr = (ArrayList)Session["MenuArrayList"];
                    MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Url = "~/Images/icons/Check.png";
                    MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Width = Utility.MenuImgSize;
                    MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Height = Utility.MenuImgSize;
                    arr[4] = 1;
                    Session["MenuArrayList"] = arr;
                }
                else
                    CheckMenuImageCurrentPage(int.Parse(Utility.DecryptQS(MemberId.Value)), int.Parse(Utility.DecryptQS(MemberRequest.Value)));


            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
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
                    txtDesc.Text = "";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                txtDesc.Text = "";
            }
        }
        if (AxImg == true)
        {
            try
            {
                System.IO.File.Move(Session["MeAttachUpload"].ToString(), MapPath("~/Image/Members/Attachments/") + System.IO.Path.GetFileName(Session["MeAttachUpload"].ToString()));
            }
            catch (Exception)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد. خطایی در انتقال فایل صورت گرفته است";
            }
        }
        Session["MeAttachUpload"] = null;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int AttachId = -1;
        int MReId = -1;


        TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();

        CustomAspxDevGridView1.DataSource = attManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, int.Parse(Utility.DecryptQS(MemberRequest.Value)), (short)TSP.DataManager.AttachType.Attachments);
        CustomAspxDevGridView1.DataBind();

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            AttachId = (int)row["AttachId"];
            MReId = (int)row["RefTable"];
        }
        if (AttachId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
            if (MReId == CurrentMReId)
            {
                if (CheckPermitionForEdit(MReId))
                {
                    Delete(AttachId);
                    CheckMenuImageCurrentPage(int.Parse(Utility.DecryptQS(MemberId.Value)), CurrentMReId);

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان حذف اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان حذف اطلاعات مربوط به درخواست های قبل وجود ندارد.";
            }

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["MeAttachUpload"] = null;
        Session["MeAttachUploadName"] = null;

        string Mode = Utility.DecryptQS(HDMode.Value);
        string TempMe = "";
        if (Mode == "Request")
            TempMe = "0";
        else if (Mode == "TempMe")
            TempMe = "1";
        if (Mode == "Home")
        {
            Response.Redirect("MemberRegister.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

        }
        else
            Response.Redirect("MemberInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + Request.QueryString["MReId"]  + "&PageMode=" + PgMode.Value
                + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
         + "&TMe=" + Utility.EncryptQS(TempMe) + "&Pt=" + Utility.EncryptQS("2"));
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        BackToManagementPage();
    }

    protected void MenuTop_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string TempMe = "";
        string Mode = Utility.DecryptQS(HDMode.Value);
        if (Mode == "Request")
            TempMe = "0";
        else if (Mode == "TempMe")
            TempMe = "1";
        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("MemberJob.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Language":
                Response.Redirect("MemberLanguage.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"]  + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Member":
                Response.Redirect("MemberRegister.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Activity":
                Response.Redirect("MemberActivity.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"]   + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Madrak":
                Response.Redirect("MemberLicence.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"]+ "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Message":
                Response.Redirect("MemberMessage.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Group":
                Response.Redirect("MemberGroups.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Request":
                Response.Redirect("MemberInsert.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"]
                     + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                  + "&TMe=" + Utility.EncryptQS(TempMe) + "&Pt=" + Utility.EncryptQS("2"));
                break;
            case "PollAnswer":
                Response.Redirect("ReportMemberPollAnswers.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "AccFish":
                Response.Redirect("MembersAccounting.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&Mode=" + Utility.EncryptQS(Mode)  + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (Utility.DecryptQS(HDMode.Value) == "Request")
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
                return;
            if (MemberRequest.Value != null)
            {
                string MReId = Utility.DecryptQS(MemberRequest.Value);
                if (e.GetValue("RefTable") == null)
                    return;
                string CurretnMReId = e.GetValue("RefTable").ToString();
                if (MReId == CurretnMReId)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }
        //if (e.RowType != DevExpress.Web.GridViewRowType.Data)
        //  return;
        //if (e.Row.Cells.Count > 1)
        //  e.Row.Cells[0].Text = Path.GetFileName(e.GetValue("FilePath").ToString());

    }

    protected void CustomAspxDevGridView1_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {

    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        //DevExpress.Web.Internal.HyperLinkDisplayControl hp = (DevExpress.Web.Internal.HyperLinkDisplayControl)e.Cell.Controls[0];
    }

    protected void ASPxHyperLink1_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxHyperLink hp = (DevExpress.Web.ASPxHyperLink)sender;
        hp.Text = Path.GetFileName(hp.Value.ToString());
    }

    protected void flp_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void CustomAspxDevGridView1_PageIndexChanged(object sender, EventArgs e)
    {
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        CustomAspxDevGridView1.DataSource = attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, int.Parse(Utility.DecryptQS(MemberRequest.Value)), (short)TSP.DataManager.AttachType.Attachments);
        CustomAspxDevGridView1.DataBind();
    }

    #endregion

    #region Methods

    #region WF Permissions
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        string MReId = Utility.DecryptQS(MemberRequest.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        int WFCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, int.Parse(MReId), Utility.GetCurrentUser_UserId());
        this.ViewState["BtnNew"] = BtnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew;
        // this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit;        
        this.ViewState["BtnDelete"] = btnDelete.Enabled = btnDelete2.Enabled = WFPer.BtnInactive; ;
    }


    //private void CheckWorkFlowPermissionForSave()
    //{
    //    TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
    //    TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

    //    int CurrentTaskOrder = -1;
    //    int TaskOrder = -1;
    //    //****TableId
    //    // string MeId = Utility.DecryptQS(MemberId.Value);
    //    //int MReId = -1;
    //    //???????????????????????????
    //    int MReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
    //    //****TableType
    //    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;

    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

    //    DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, MReId);
    //    if (dtWorkFlowState.Rows.Count > 0)
    //    {
    //        CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
    //    }
    //    int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
    //    WorkFlowTaskManager.FindByTaskCode(TaskCode);
    //    if (WorkFlowTaskManager.Count > 0)
    //    {
    //        TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
    //    }

    //    if (TaskOrder != 0 && CurrentTaskOrder == TaskOrder)
    //    {
    //        WorkFlowTaskManager.FindByTaskCode(TaskCode);
    //        int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
    //        TaskDoerManager.FindByTaskId(TaskId);

    //        if (TaskDoerManager.Count > 0)
    //        {
    //            int NcId = int.Parse(TaskDoerManager[0]["NcId"].ToString());
    //            NezamMemberChartManager.FindByNcId(NcId);

    //            int EmpId = int.Parse(NezamMemberChartManager[0]["EmpId"].ToString());

    //            LoginManager.FindByMeIdUltId(EmpId, 4);
    //            if (LoginManager.Count > 0)
    //            {
    //                int userId = int.Parse(LoginManager[0]["UserId"].ToString());
    //                int CurrentUserId = Utility.GetCurrentUser_UserId();
    //                if (CurrentUserId == userId)
    //                {
    //                    BtnNew.Enabled = true;
    //                    BtnNew2.Enabled = true;                        
    //                    btnDelete.Enabled = true;
    //                    btnDelete2.Enabled = true;

    //                }
    //                else
    //                {

    //                    BtnNew.Enabled = false;
    //                    BtnNew2.Enabled = false;
    //                    btnDelete.Enabled = false;
    //                    btnDelete2.Enabled = false;

    //                }
    //            }
    //            else
    //            {
    //                BtnNew.Enabled = false;
    //                BtnNew2.Enabled = false;
    //                btnDelete.Enabled = false;
    //                btnDelete2.Enabled = false;

    //            }
    //        }
    //        else
    //        {
    //            BtnNew.Enabled = false;
    //            BtnNew2.Enabled = false;
    //            btnDelete.Enabled = false;
    //            btnDelete2.Enabled = false;

    //        }
    //    }
    //    else
    //    {
    //        BtnNew.Enabled = false;
    //        BtnNew2.Enabled = false;
    //        btnDelete.Enabled = false;
    //        btnDelete2.Enabled = false;

    //    }
    //    this.ViewState["BtnNew"] = BtnNew.Enabled;
    //    this.ViewState["BtnDelete"] = btnDelete.Enabled;

    //}

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    //int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

                    if (CurrentTaskCode == TaskCode)
                    {
                        return true;
                        //DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        //if (dtWorkFlowState.Rows.Count > 0)
                        //{
                        //    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                        //    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                        //    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                        //    if (FirstTaskCode == TaskCode)
                        //    {
                        //        if (FirstNmcIdType == 0)
                        //        {
                        //            int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                        //            if (Permission > 0)
                        //                return true;
                        //        }
                        //    }
                        //}
                    }
                }
            }
        }
        return false;

    }

    #endregion

    protected void CheckMenuImage(int MeId, int MReId)
    {
        TSP.DataManager.MemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.MemberActivitySubjectManager();
        TSP.DataManager.MemberLanguageManager MemberLanguageManager = new TSP.DataManager.MemberLanguageManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();

        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Licence
        arr.Add(0);//arr[1]-->Job
        arr.Add(0);//arr[2]-->Language
        arr.Add(0);//arr[3]-->Activity
        arr.Add(0);//arr[4]-->Attachment
        arr.Add(0);//arr[5]-->Request

        MemberActivitySubjectManager.FindForDelete(MeId, MReId);
        if (MemberActivitySubjectManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        MemberLanguageManager.FindForDelete(MeId, MReId);
        if (MemberLanguageManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }

        MemberLicenceManager.FindForDelete(MeId, MReId);
        if (MemberLicenceManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(0, MReId, (int)TSP.DataManager.TableCodes.MemberRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            arr[1] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Height = Utility.MenuImgSize;
            arr[4] = 1;
        }
        Session["MenuArrayList"] = arr;
    }

    protected void CheckMenuImageCurrentPage(int MeId, int MReId)
    {
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.Attachments);

        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (AttachmentsManager.Count > 0)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Height = Utility.MenuImgSize;
                arr[4] = 1;
            }
            else
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Url = "";
                arr[4] = 0;

            }
            Session["MenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(MeId, MReId);
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (AttachmentsManager.Count > 0)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Height = Utility.MenuImgSize;
                arr[4] = 1;
            }
            else
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Url = "";
                arr[4] = 0;

            }
            Session["MenuArrayList"] = arr;
        }

    }

    protected void SetMenuItem()
    {
        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];

            if ((int)arr[0] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[1] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[2] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[3] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[4] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Attachment")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[5] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Height = Utility.MenuImgSize;
            }
        }
        else
        {
            CheckMenuImage(int.Parse(Utility.DecryptQS(MemberId.Value)), int.Parse(Utility.DecryptQS(MemberRequest.Value)));

        }
    }

    protected void Delete(int AttachId)
    {
        TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();
        try
        {
            attManager.FindByCode(AttachId);
            if (attManager.Count == 1)
            {
                string url = attManager[0]["FilePath"].ToString();

                attManager[0].Delete();

                int cn = attManager.Save();
                if (cn == 1)
                {
                    if ((!string.IsNullOrEmpty(url)) && (File.Exists(Server.MapPath(url))))
                        File.Delete(Server.MapPath(url));

                    CustomAspxDevGridView1.DataSource = attManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, int.Parse(Utility.DecryptQS(MemberRequest.Value)), (short)TSP.DataManager.AttachType.Attachments);
                    CustomAspxDevGridView1.DataBind();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد";

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }

            }
        }
        catch (Exception err)
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
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
            }
        }
    }

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        Session["MeAttachUpload"] = null;
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new System.IO.FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/Members/Attachments/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["MeAttachUpload"] = tempFileName;
        }
        return ret;
    }

    private void FillMemberName()
    {
        string MeId = Utility.DecryptQS(MemberId.Value);
        string Mode = Utility.DecryptQS(HDMode.Value);
        MemberInfoUserControl1.MeId = Convert.ToInt32(MeId);
        if (Mode == "TempMe")
        {
            MemberInfoUserControl1.IsMeTemp = true;
        }
        MemberInfoUserControl1.MReId = Convert.ToInt32(Utility.DecryptQS(MemberRequest.Value));
    }

    private void BackToManagementPage()
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        string UserName = "";
        if (Mode == "Request")
            UserName = Utility.DecryptQS(MemberId.Value);
        else if (Mode == "TempMe")
            UserName = "M" + Utility.DecryptQS(MemberId.Value);
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("Members.aspx?PostId=" + Utility.EncryptQS(UserName) + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt + "&MReId=" + MemberRequest.Value);
        }
        else
        {
            Response.Redirect("Members.aspx?MReId=" + MemberRequest.Value);
        }
    }
    #endregion
}
