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

public partial class Employee_OfficeRegister_OfficeFinancialStausInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    DataTable dtOfImg = null;
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
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["Dprt"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            HiddenFieldOffice["Department"] = Request.QueryString["Dprt"];

            if (string.IsNullOrEmpty(Request.QueryString["OfId"]) || string.IsNullOrEmpty(Request.QueryString["OfsId"]))
            {
                Response.Redirect("Office.aspx");
                return;
            }

            Session["DocUpload"] = null;
            Session["TblOfImgd"] = null;
            Session["IsEdited_OffFin"] = false;

            if (Session["TblOfImgd"] == null)
            {
                dtOfImg = new DataTable();
                dtOfImg.Columns.Add("ImgUrl");
                dtOfImg.Columns.Add("TempImgUrl");
                dtOfImg.Columns.Add("fileName");
                dtOfImg.Columns.Add("Mode");
                dtOfImg.Columns.Add("Code");
                dtOfImg.Columns.Add("Description");
                dtOfImg.Columns.Add("Id");
                dtOfImg.Columns["Id"].AutoIncrement = true;
                dtOfImg.Columns["Id"].AutoIncrementSeed = 1;
                dtOfImg.Constraints.Add("PK_ID", dtOfImg.Columns["Id"], true);

                Session["TblOfImgd"] = dtOfImg;
            }
            else
                dtOfImg = (DataTable)Session["TblOfImgd"];

            AspxGridFlp.DataSource = dtOfImg;
            AspxGridFlp.DataBind();

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["APageMode"].ToString());
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                FinancialId.Value = Server.HtmlDecode(Request.QueryString["OfsId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string OfId = Utility.DecryptQS(OfficeId.Value);
            string OfsId = Utility.DecryptQS(FinancialId.Value);
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);
            OfficeInfoUserControl.OfReId = int.Parse(OfReId);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            TSP.DataManager.Permission per = FindPermissionClass();
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;

            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;
            if (PageMode != "New" && !per.CanView)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString());
            }
            switch (PageMode)
            {
                case "View":
                    Disable();

                    if (string.IsNullOrEmpty(OfsId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    TblFile.Visible = false;

                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;
                    //btnDelete.Enabled = false;
                    //btnDelete2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(OfsId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";


                    break;


                case "New":
                    Enable();
                    //btnDelete.Enabled = false;
                    //btnDelete2.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    ASPxRoundPanel2.HeaderText = "جدید";

                    ClearForm();
                    break;

                case "Edit":
                    Enable();

                    // btnDelete.Enabled = per.CanDelete;
                    // btnDelete2.Enabled = per.CanDelete;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    if (string.IsNullOrEmpty(OfsId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(OfsId));
                    ASPxRoundPanel2.Enabled = true;
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    break;

                case "Judge":
                    //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
                    //int GradingImplementDocTaskCode = (int)TSP.DataManager.WorkFlowTask.ExpertConfirmingDocumentOff;
                    //int GradingImplementDocTaskId = -1;

                    //WorkFlowTaskManager.FindByTaskCode(GradingImplementDocTaskCode);
                    //if (WorkFlowTaskManager.Count == 1)
                    //{
                    //    GradingImplementDocTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                    //}

                    //if (GradingImplementDocTaskId == -1)
                    //{
                    //    this.DivReport.Visible = true;
                    //    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                    //    return;
                    //}
                    //int NmcId = FindNmcId(GradingImplementDocTaskId);
                    //if (NmcId == -1)
                    //{
                    //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    //    return;
                    //}
                    //TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();

                    //TrainingJudgmentManager.FindByNmcId(NmcId, int.Parse(OfsId), 3);
                    //if (TrainingJudgmentManager.Count > 0)
                    //{
                    //    string JudgeId = TrainingJudgmentManager[0][TrainingJudgmentManager.Count - 1].ToString();
                    //    HDJudgeId.Value = Utility.EncryptQS(JudgeId);
                    //    FillJugde(int.Parse(JudgeId));
                    //}
                    //Disable();
                    //if (string.IsNullOrEmpty(OfsId))
                    //{
                    //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    //    return;
                    //}

                    //btnEdit.Enabled = per.CanEdit;
                    //btnEdit2.Enabled = per.CanEdit;
                    //TblFile.Visible = false;

                    //FillForm(int.Parse(OfsId));
                    ////hpFilePath.Visible = true;

                    //ASPxRoundPanel2.HeaderText = "مشاهده";
                    //RoundPanelJudge.Visible = true;
                    break;

            }

            ObjectDataSource1.FilterParameters[0].DefaultValue = "1";

            string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
            if (Department == "MemberShip")
            {
                CheckWorkFlowPermissionForOffice();
            }
            else if (Department == "Document")
            {
                CheckWorkFlowPermissionForDoc();
            }

            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
            ReqManager.FindByCode(int.Parse(OfReId));
            if (ReqManager.Count > 0)
            {
                if ((Convert.ToBoolean(ReqManager[0]["Requester"]) == false) || (ReqManager[0]["IsConfirm"].ToString() != "0"))//Request From Member
                {
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                }
            }
            TSP.DataManager.DocOffOfficeFinancialStatusManager StatusManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
            if (!string.IsNullOrEmpty(OfsId))
            {
                StatusManager.FindByCode(int.Parse(OfsId));
                if (StatusManager.Count == 1)
                {
                    if (StatusManager[0]["OfReId"].ToString() != OfReId)
                    {
                        BtnNew.Enabled = false;
                        BtnNew2.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave2.Enabled = false;
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                    }
                }
            }

            #region ModeComment
            //if (Mode == "Home")
            //{
            //    ReqManager.FindByOfficeId(int.Parse(OfId), -1, 0);
            //    if (ReqManager.Count > 0)
            //    {
            //        if (!Convert.ToBoolean(ReqManager[0]["Requester"]))//FromMember
            //        {
            //            btnEdit.Enabled = false;
            //            btnEdit2.Enabled = false;
            //            BtnNew.Enabled = false;
            //            BtnNew2.Enabled = false;
            //            btnSave.Enabled = false;
            //            btnSave2.Enabled = false;

            //        }
            //    }
            //    if (OfManager[0]["MrsId"].ToString() == "1")//تایید شده
            //    {
            //        btnEdit.Enabled = false;
            //        btnEdit2.Enabled = false;
            //        BtnNew.Enabled = false;
            //        BtnNew2.Enabled = false;
            //        btnSave.Enabled = false;
            //        btnSave2.Enabled = false;
            //    }
            //}
            //else if (Mode == "Request")
            //{
            //    btnEdit.Enabled = false;
            //    btnEdit2.Enabled = false;

            //    string ReqestMode = Server.HtmlDecode(Request.QueryString["TP"]).ToString();
            //    string TPType = Utility.DecryptQS(ReqestMode);
            //    if (!string.IsNullOrEmpty(TPType))
            //    {
            //        if (TPType == "0")//Menu
            //        {

            //            BtnNew.Enabled = false;
            //            BtnNew2.Enabled = false;

            //        }
            //        else
            //        {
            //            ReqManager.FindByCode(int.Parse(OfReId));
            //            if (ReqManager.Count > 0)
            //            {
            //                if (!Convert.ToBoolean(ReqManager[0]["Requester"]))//FromMember
            //                {
            //                    BtnNew.Enabled = false;
            //                    BtnNew2.Enabled = false;
            //                    btnSave.Enabled = false;
            //                    btnSave2.Enabled = false;
            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (!string.IsNullOrEmpty(txtCmbName.Text))
        {
            CmbName.DataBind();
            CmbName.SelectedIndex = Convert.ToInt32(txtCmbName.Text);

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }
    protected void btnAddFlp_Click(object sender, EventArgs e)
    {

        if (Session["TblOfImgd"] != null)
        {
            dtOfImg = (DataTable)Session["TblOfImgd"];

            DataRow dr = dtOfImg.NewRow();

            try
            {

                if (Session["DocUpload"] != null)
                {

                    dr[0] = "~/Image/Office/FinancialStatus/" + System.IO.Path.GetFileName(Session["DocUpload"].ToString());
                    dr[2] = System.IO.Path.GetFileName(Session["DocUpload"].ToString());
                    dr[1] = "~/Image/temp/" + System.IO.Path.GetFileName(Session["DocUpload"].ToString());
                    dr[5] = txtDescImg.Text;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "فایل مورد نظر را انتخاب نمایید";
                    return;
                }

                dr[3] = 0;
                dtOfImg.Rows.Add(dr);
                AspxGridFlp.DataSource = dtOfImg;
                AspxGridFlp.DataBind();

                Session["DocUpload"] = null;

                txtDescImg.Text = "";
                imgEndUploadImg.ClientVisible = false;


            }
            catch (Exception)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }

        }
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["TblOfImgd"] = null;
        Response.Redirect("OfficeFinancialStatus.aspx?OfId=" + OfficeId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
             + "&Dprt=" + HiddenFieldOffice["Department"].ToString());

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OfsId = Utility.DecryptQS(FinancialId.Value);

        if (string.IsNullOrEmpty(OfId) || string.IsNullOrEmpty(OfsId) || string.IsNullOrEmpty(pageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        else
        {

            Enable();

            TSP.DataManager.Permission per = FindPermissionClass();

            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
            this.ViewState["BtnSave"] = btnSave.Enabled;

            PgMode.Value = Utility.EncryptQS("Edit");
            ASPxRoundPanel2.HeaderText = "ویرایش";
            TblFile.Visible = true;

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OfsId = Utility.DecryptQS(FinancialId.Value);

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

                    if (string.IsNullOrEmpty(OfsId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    else
                        Edit(int.Parse(OfsId));

                    break;

                case "Judge":
                    string JudgeId = Utility.DecryptQS(HDJudgeId.Value);
                    if (string.IsNullOrEmpty(JudgeId))
                        InsertJudge();

                    else
                        EditJudge(int.Parse(JudgeId));

                    break;
            }


        }



    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = FindPermissionClass();

        TblFile.Visible = true;

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;


        FinancialId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
    }
    protected void AspxGridFlp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        AspxGridFlp.DataSource = (DataTable)Session["TblOfImgd"];
        AspxGridFlp.DataBind();

        int Id = -1;
        if (AspxGridFlp.FocusedRowIndex > -1)
        {
            Id = AspxGridFlp.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {

            dtOfImg = (DataTable)Session["TblOfImgd"];
            dtOfImg.Rows.Find(e.Keys["Id"]).Delete();
            //dtOfImg.Rows[Id].Delete();
            Session["TblOfImgd"] = dtOfImg;
            AspxGridFlp.DataSource = (DataTable)Session["TblOfImgd"];
            AspxGridFlp.DataBind();
            dtOfImg = (DataTable)Session["TblOfImgd"];


        }

    }

    #endregion

    #region Methods
    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/Office/FinancialStatus/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["DocUpload"] = tempFileName;
        }
        return ret;
    }
    protected void FillForm(int OfsId)
    {
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        FinManager.FindByCode(OfsId);
        if (FinManager.Count == 1)
        {

            CmbName.DataBind();
            CmbName.SelectedIndex = CmbName.Items.IndexOfValue(int.Parse(FinManager[0]["OfdId"].ToString()));
            txtCmbName.Text = CmbName.SelectedIndex.ToString();
            HDComboValue.Value = FinManager[0]["OfdId"].ToString();

            //CmbName.Value = FinManager[0]["OfdId"].ToString();
            txtDesc.Text = FinManager[0]["Description"].ToString();
            decimal Cost = Convert.ToDecimal(FinManager[0]["Value"].ToString());
            txtValue.Text = Cost.ToString("#,#");
        }

        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.OfficeFinancialStatus, OfsId);
        dtOfImg = (DataTable)Session["TblOfImgd"];
        for (int i = 0; i < attachManager.Count; i++)
        {
            DataRow dr = dtOfImg.NewRow();
            dr[0] = attachManager[i]["FilePath"].ToString();
            dr[1] = attachManager[i]["FilePath"].ToString();
            dr[5] = attachManager[i]["Description"].ToString();

            dr[2] = Path.GetFileName(attachManager[0]["FilePath"].ToString());
            dr[3] = 1;
            dr[4] = attachManager[i][0];
            dtOfImg.Rows.Add(dr);

        }
        dtOfImg.AcceptChanges();
        AspxGridFlp.DataSource = dtOfImg;
        AspxGridFlp.DataBind();

    }

    protected void Insert()
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager2 = new TSP.DataManager.DocOffOfficeFinancialStatusManager();

        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(FinManager);
        trans.Add(attachManager);
        trans.Add(WorkFlowStateManager);


        try
        {

            string OfId = Utility.DecryptQS(OfficeId.Value);
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);

            if (string.IsNullOrEmpty(OfId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            FinManager2.FindByOfCode(int.Parse(OfId));

            for (int i = 0; i < FinManager2.Count; i++)
            {
                if (FinManager2[i]["OfdId"].ToString() == CmbName.Value.ToString() && FinManager2[i]["InActiveName"].ToString() == "فعال")
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }
            }

            DataRow dr = FinManager.NewRow();
            dr["OfReId"] = OfReId;

            if (CmbName.Value != null)
                dr["OfdId"] = CmbName.Value;
            dr["Value"] = txtValue.Text;

            dr["Description"] = txtDesc.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            dr["CreateDate"] = Utility.GetDateOfToday();
            FinManager.AddRow(dr);
            trans.BeginSave();
            if (FinManager.Save() == 1)
            {

                dtOfImg = (DataTable)Session["TblOfImgd"];
                int OfsId = int.Parse(FinManager[0]["OfsId"].ToString());
                if (dtOfImg.DefaultView.Count > 0)
                {
                    for (int i = 0; i < dtOfImg.DefaultView.Count; i++)
                    {
                        DataRow drImg = attachManager.NewRow();
                        drImg["TtId"] = (int)TSP.DataManager.TableCodes.OfficeFinancialStatus;
                        drImg["RefTable"] = OfsId;
                        drImg["AttId"] = 1;
                        //drImg["AtContent"] = dtOfImg.Rows[i]["Image"];
                        drImg["FilePath"] = dtOfImg.Rows[i]["ImgUrl"].ToString();
                        drImg["IsValid"] = 1;
                        drImg["Description"] = dtOfImg.Rows[i]["Description"].ToString();
                        drImg["UserId"] = Utility.GetCurrentUser_UserId();
                        drImg["ModfiedDate"] = DateTime.Now;
                        attachManager.AddRow(drImg);
                        int imgcnt = attachManager.Save();
                        attachManager.DataTable.AcceptChanges();
                        if (imgcnt == 1)
                        {
                            dtOfImg.Rows[i].BeginEdit();
                            dtOfImg.Rows[i]["Code"] = attachManager[attachManager.Count - 1]["AttachId"].ToString();
                            dtOfImg.Rows[i].EndEdit();

                            if (!string.IsNullOrEmpty(dtOfImg.Rows[i]["ImgUrl"].ToString()))
                            {

                                string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImg.Rows[i]["fileName"].ToString();
                                string ImgTarget = Server.MapPath(dtOfImg.Rows[i]["ImgUrl"].ToString());
                                File.Copy(ImgSoource, ImgTarget, true);
                                // grdv_Img.Columns[1].Visible = true;
                            }

                        }
                    }

                }
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_OffFin"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeFinancialStatus;
                    string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
                    int WfCode = -1;
                    if (Department == "Document")
                        WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                    else if (Department == "MemberShip")
                        WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                    if (WfCode == -1)
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                        return;
                    }
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, int.Parse(OfReId), UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    trans.EndSave();
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                    FinancialId.Value = Utility.EncryptQS(OfsId.ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    Session["IsEdited_OffFin"] = true;
                    HDComboValue.Value = CmbName.Value.ToString();


                    if (Session["OffMenuArrayList"] != null)
                    {
                        ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
                        arr[5] = 1;
                        Session["OffMenuArrayList"] = arr;
                    }
                    else
                    {
                        CheckMenuImage(int.Parse(OfReId));
                        ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
                        arr[5] = 1;
                        Session["OffMenuArrayList"] = arr;
                    }
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
    protected void Edit(int OfsId)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager2 = new TSP.DataManager.DocOffOfficeFinancialStatusManager();

        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(FinManager);
        trans.Add(attachManager);
        trans.Add(WorkFlowStateManager);


        try
        {
            int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
            FinManager2.FindByOfCode(OfId);
            if (HDComboValue == null && string.IsNullOrEmpty(HDComboValue.Value))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            string SelectedOfdId = HDComboValue.Value;
            for (int i = 0; i < FinManager2.Count; i++)
            {
                if (FinManager2[i]["OfdId"].ToString() == CmbName.Value.ToString() && SelectedOfdId != CmbName.Value.ToString() && FinManager2[i]["InActiveName"].ToString() == "فعال")
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                    return;
                }
            }

            FinManager.FindByCode(OfsId);
            FinManager[0].BeginEdit();
            if (CmbName.Value != null)
                FinManager[0]["OfdId"] = CmbName.Value;
            FinManager[0]["Description"] = txtDesc.Text;
            FinManager[0]["Value"] = txtValue.Text;
            FinManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            FinManager[0]["ModifiedDate"] = DateTime.Now;
            FinManager[0].EndEdit();
            trans.BeginSave();
            if (FinManager.Save() == 1)
            {
                dtOfImg = (DataTable)Session["TblOfImgd"];

                if (dtOfImg.GetChanges() != null)
                {
                    DataRow[] delRows = dtOfImg.Select("Mode='1'", null, DataViewRowState.Deleted);
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        attachManager.FindByCode(int.Parse(delRows[i]["Code", DataRowVersion.Original].ToString()));
                        attachManager[0].Delete();
                        attachManager.Save();
                    }
                    attachManager.DataTable.AcceptChanges();
                    DataRow[] insRows = dtOfImg.Select(null, null, DataViewRowState.Added);

                    if (insRows.Length > 0)
                    {
                        for (int i = 0; i < insRows.Length; i++)
                        {
                            DataRow drImg = attachManager.NewRow();
                            drImg["TtId"] = (int)TSP.DataManager.TableCodes.OfficeFinancialStatus;
                            drImg["RefTable"] = OfsId;
                            drImg["AttId"] = 1;
                            drImg["FilePath"] = insRows[i]["ImgUrl"].ToString();
                            drImg["IsValid"] = 1;
                            drImg["Description"] = insRows[i]["Description"].ToString();
                            drImg["UserId"] = Utility.GetCurrentUser_UserId();
                            drImg["ModfiedDate"] = DateTime.Now;
                            attachManager.AddRow(drImg);
                            int imgcnt = attachManager.Save();
                            attachManager.DataTable.AcceptChanges();
                            if (imgcnt == 1)
                            {
                                if (!string.IsNullOrEmpty(insRows[i]["ImgUrl"].ToString()))
                                {
                                    string ImgSoource = Server.MapPath("~/image/Temp/") + insRows[i]["fileName"].ToString();
                                    string ImgTarget = Server.MapPath(insRows[i]["ImgUrl"].ToString());
                                    File.Copy(ImgSoource, ImgTarget, true);
                                }
                            }
                        }
                    }
                }
                int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_OffFin"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    int UpdateTableType = (int)TSP.DataManager.TableCodes.OfficeFinancialStatus;
                    string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
                    int WfCode = -1;
                    if (Department == "Document")
                        WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                    else if (Department == "MemberShip")
                        WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                    if (WfCode == -1)
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                        return;
                    }
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateStateByWfCode(WfCode, OfReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
                }
                if (UpdateState == -4)
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                }
                else
                {
                    trans.EndSave();
                    //FinancialId.Value = Utility.EncryptQS(OfsId.ToString());
                    //PgMode.Value = Utility.EncryptQS("Edit");
                    //ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                    Session["IsEdited_OffFin"] = true;
                    HDComboValue.Value = CmbName.Value.ToString();

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
    protected void Enable()
    {
        CmbName.Enabled = true;
        txtDesc.Enabled = true;
        txtValue.Enabled = true;
        AspxGridFlp.Columns[2].Visible = true;
        flp.Enabled = true;

    }
    protected void Disable()
    {
        CmbName.Enabled = false;
        txtDesc.Enabled = false;
        txtValue.Enabled = false;
        AspxGridFlp.Columns[2].Visible = false;
        flp.Enabled = false;

    }
    protected void ClearForm()
    {
        CmbName.DataBind();
        CmbName.SelectedIndex = -1;
        txtDesc.Text = "";
        txtDescImg.Text = "";
        txtValue.Text = "";
        dtOfImg = (DataTable)Session["TblOfImgd"];
        dtOfImg.Rows.Clear();
        AspxGridFlp.DataSource = dtOfImg;
        AspxGridFlp.DataBind();
        txtCmbName.Text = "";

    }

    private void CheckWorkFlowPermissionForDoc()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (PageMode != "New")
            CheckWorkFlowPermissionForEditForDoc(PageMode);
    }

    private void CheckWorkFlowPermissionForSaveForDoc(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff, Utility.GetCurrentUser_UserId());
        if (Permission > 0 || Permission2 > 0)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;

                    break;
                case "View":
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        else
        {
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جهت درخواست صدور پروانه را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEditForDoc(string PageMode)
    {
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, int.Parse(OfReId), TaskCode, Utility.GetCurrentUser_UserId());
        int Permisssion2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, int.Parse(OfReId), (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0 || Permisssion2 > 0)
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;

                    break;
            }
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;

        }
        else
        {

            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForOffice()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (PageMode != "New")
            CheckWorkFlowPermissionForEditForOffice(PageMode);
    }

    private void CheckWorkFlowPermissionForSaveForOffice(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        int Permission = -1;
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());

        int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, Utility.GetCurrentUser_UserId());
        if (Permission > 0 || Permission2>0)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;

                    break;
                case "View":
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        else
        {
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جهت درخواست صدور پروانه را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEditForOffice(string PageMode)
    {
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, int.Parse(OfReId), TaskCode, Utility.GetCurrentUser_UserId());
        int Permisssion2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, int.Parse(OfReId), (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0 || Permisssion2>0)
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;

                    break;
            }
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;

        }
        else
        {

            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    //private int FindNmcId()
    //{
    //    int NcId = -1;
    //    int UserId = Utility.GetCurrentUser_UserId();
    //    TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
    //    TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

    //    int NmcId = -1;

    //    //NmcId = NezamChartManager.FindNmcIdByNcId(NcId, UserId, LoginManager);
    //    NmcId = NezamChartManager.FindNmcId(UserId, LoginManager);
    //    if (NmcId > 0)
    //    {
    //        return NmcId;
    //    }
    //    else
    //    {
    //        DivReport.Visible = true;
    //        LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
    //        return (-1);
    //    }
    //}

    private int FindNmcId(int TaskId)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            DivReport.Visible = true;
            LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            return (-1);
        }
    }

    private void FillJugde(int JudgeId)
    {
        TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
        TrainingJudgmentManager.FindByCode(JudgeId);
        if (TrainingJudgmentManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(TrainingJudgmentManager[0]["JudgeViewPoint"]))
                txtViewPoint.Text = TrainingJudgmentManager[0]["JudgeViewPoint"].ToString();
            if (!Utility.IsDBNullOrNullValue(TrainingJudgmentManager[0]["FinancialValue"]))
                txtGrade.Text = TrainingJudgmentManager[0]["FinancialValue"].ToString();
            if (!Utility.IsDBNullOrNullValue(TrainingJudgmentManager[0]["MeetingDate"]))
                txtMeetingDate.Text = TrainingJudgmentManager[0]["MeetingDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(TrainingJudgmentManager[0]["MeetingId"]))
                txtMeetingId.Text = TrainingJudgmentManager[0]["MeetingId"].ToString();
            if (!Utility.IsDBNullOrNullValue(TrainingJudgmentManager[0]["IsConfirmed"]))
                rdbtnIsConfirm.SelectedIndex = int.Parse(TrainingJudgmentManager[0]["IsConfirmed"].ToString());
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
        }
    }
    private void InsertJudge()
    {
        if (IsPageRefresh)
            return;
        //TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        //try
        //{
        //    int GradingImplementDocTaskCode = (int)TSP.DataManager.WorkFlowTask.ExpertConfirmingDocumentOff;
        //    int GradingImplementDocTaskId = -1;

        //    WorkFlowTaskManager.FindByTaskCode(GradingImplementDocTaskCode);
        //    if (WorkFlowTaskManager.Count == 1)
        //    {
        //        GradingImplementDocTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        //    }

        //    if (GradingImplementDocTaskId == -1)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
        //        return;
        //    }
        //    int NmcId = FindNmcId(GradingImplementDocTaskId);
        //    if (NmcId == -1)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "اطلاعات شما در چارت سازماني ثبت نشده است.";
        //        return;
        //    }

        //    int OfsId = int.Parse(Utility.DecryptQS(FinancialId.Value));
        //    DataRow JudgeRow = TrainingJudgmentManager.NewRow();
        //    JudgeRow["PkId"] = OfsId;
        //    JudgeRow["CreateDate"] = Utility.GetDateOfToday();
        //    JudgeRow["MeetingId"] = txtMeetingId.Text;
        //    JudgeRow["MeetingDate"] = txtMeetingDate.Text;
        //    JudgeRow["JudgeViewPoint"] = txtViewPoint.Text;
        //    JudgeRow["FinancialValue"] = txtGrade.Text;
        //    //JudgeRow["EmpId"] = Utility.GetCurrentUser_MeId();
        //    //JudgeRow["UltId"] = 4;          
        //    JudgeRow["NmcId"] = NmcId;
        //    JudgeRow["Type"] = 3;
        //    JudgeRow["IsConfirmed"] = rdbtnIsConfirm.SelectedItem.Value.ToString();
        //    JudgeRow["UserId"] = Utility.GetCurrentUser_UserId();
        //    JudgeRow["ModifiedDate"] = DateTime.Now;

        //    TrainingJudgmentManager.AddRow(JudgeRow);
        //    int cn = TrainingJudgmentManager.Save();
        //    if (cn > 0)
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "ذخیره انجام شد.";
        //        HDJudgeId.Value = Utility.EncryptQS(TrainingJudgmentManager[0]["JudgeId"].ToString());
        //    }
        //    else
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
        //    }
        //}
        //catch (Exception err)
        //{
        //    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        //    {
        //        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
        //        if (se.Number == 2601)
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "اطلاعات تکراری می باشد";
        //        }
        //        else
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        //        }
        //    }
        //    else
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        //    }
        //}
    }

    private void EditJudge(int JudgeId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager = new TSP.DataManager.TrainingJudgmentManager();
        try
        {
            TrainingJudgmentManager.FindByCode(JudgeId);
            if (TrainingJudgmentManager.Count == 1)
            {
                TrainingJudgmentManager[0].BeginEdit();

                TrainingJudgmentManager[0]["MeetingId"] = txtMeetingId.Text;
                TrainingJudgmentManager[0]["MeetingDate"] = txtMeetingDate.Text;
                TrainingJudgmentManager[0]["JudgeViewPoint"] = txtViewPoint.Text;
                TrainingJudgmentManager[0]["FinancialValue"] = txtGrade.Text;
                TrainingJudgmentManager[0]["IsConfirmed"] = rdbtnIsConfirm.SelectedItem.Value.ToString();

                TrainingJudgmentManager[0].EndEdit();
                int cn = TrainingJudgmentManager.Save();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                    HDJudgeId.Value = Utility.EncryptQS(TrainingJudgmentManager[0]["JudgeId"].ToString());
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
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

    protected void CheckMenuImage(int OfReId)
    {
        TSP.DataManager.OfficeAgentManager OffAgentManager = new TSP.DataManager.OfficeAgentManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficialLetterManager OffLetterManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager OffFinancialManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();


        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Agent
        arr.Add(0);//arr[1]-->Member
        arr.Add(0);//arr[2]-->Letters
        arr.Add(0);//arr[3]-->Job
        arr.Add(0);//arr[4]-->Attach
        arr.Add(0);//arr[5]-->Financial
        arr.Add(0);//arr[6]-->Office

        OffAgentManager.FindForDelete(OfReId);
        if (OffAgentManager.Count > 0)
        {
            arr[0] = 1;
        }
        OffMemberManager.FindForDelete(OfReId, 0);
        if (OffMemberManager.Count > 0)
        {
            arr[1] = 1;
        }

        OffLetterManager.FindForDelete(OfReId);
        if (OffLetterManager.Count > 0)
        {
            arr[2] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(1, OfReId, (int)TSP.DataManager.TableCodes.OfficeRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            arr[3] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.OfficeRequest, OfReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            arr[4] = 1;
        }
        OffFinancialManager.FindForDelete(OfReId);
        if (OffFinancialManager.Count > 0)
        {
            arr[5] = 1;
        }

        Session["OffMenuArrayList"] = arr;
    }

    private TSP.DataManager.Permission FindPermissionClass()
    {
        string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
        if (Department == "MemberShip")
        {
            return (TSP.DataManager.DocOffOfficeFinancialStatusManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
        }
        else if (Department == "Document")
        {
            return (TSP.DataManager.DocOffOfficeFinancialStatusManager.GetUserPermissionForOffDoc(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
        }
        return (TSP.DataManager.DocOffOfficeFinancialStatusManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType()));
    }
    #endregion
}
