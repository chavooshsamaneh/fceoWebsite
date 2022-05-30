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

public partial class Office_OfficeInfo_OfficeFinancialStausShow : System.Web.UI.Page
{
    DataTable dtOfImg = null;
    private bool IsPageRefresh = false;

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
            if (string.IsNullOrEmpty(Request.QueryString["OfId"]) || string.IsNullOrEmpty(Request.QueryString["OfsId"]))
            {
                Response.Redirect("OfficeFinancialStatus.aspx");
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
                HDMode.Value = Server.HtmlDecode(Request.QueryString["Dprt"]).ToString();
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
            string Department = Utility.DecryptQS(HDMode.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(Department))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
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
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(OfsId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;
                case "New":
                    Enable();
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    ASPxRoundPanel2.HeaderText = "جدید";
                    ClearForm();
                    break;
                case "Edit":
                    Enable();
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

            }

            ObjectDataSource1.FilterParameters[0].DefaultValue = "1";

            switch (Department)
            {
                case "Home":
                    SetEnabled(false);
                    break;

                case "Document":
                    if (!CheckPermitionForEditForDoc(int.Parse(OfReId)))
                        SetEnabled(false);
                    else SetEnabled(true);

                    TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
                    ReqManager.FindByCode(int.Parse(OfReId));
                    if (ReqManager.Count > 0)
                    {
                        if (Convert.ToBoolean(ReqManager[0]["Requester"]) || (ReqManager[0]["IsConfirm"].ToString() != "0"))
                            SetEnabled(false);
                        else SetEnabled(true);
                    }
                    TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
                    if (!string.IsNullOrEmpty(OfsId))
                    {
                        FinManager.FindByCode(int.Parse(OfsId));
                        if (FinManager.Count == 1)
                        {
                            if (FinManager[0]["OfReId"].ToString() != OfReId)
                                SetEnabled(false);
                            else SetEnabled(true);
                        }
                    }
                    break;
                case "MemberShip":
                    if (string.IsNullOrEmpty(OfReId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    if (!CheckPermitionForEditForOffice(int.Parse(OfReId)))
                        SetEnabled(false);
                    else SetEnabled(true);
                    break;
            }


            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["TblOfImgd"] = null;
        Response.Redirect("OfficeFinancialStatus.aspx?OfId=" + OfficeId.Value + "&PageMode=" + Request.QueryString["PageMode"]
            + "&OfReId=" + OfficeRequest.Value + "&Dprt=" + HDMode.Value);
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

        Enable();
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        PgMode.Value = Utility.EncryptQS("Edit");
        ASPxRoundPanel2.HeaderText = "ویرایش";
        TblFile.Visible = true;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        string PageMode = Utility.DecryptQS(PgMode.Value);

        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OfsId = Utility.DecryptQS(FinancialId.Value);

        if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(OfId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
        OfManager.FindByCode(int.Parse(OfId));
        if ((bool)OfManager[0]["IsLock"] == true)
        {
            TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
            string OfficeLockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 1, 1);

            string lockers = Utility.GetFormattedObject(OfficeLockers);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
            return;
        }

        switch (PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                int OfReId = Convert.ToInt32(Utility.DecryptQS(OfficeRequest.Value));
                string Department = Utility.DecryptQS(HDMode.Value);
                if (Utility.IsDBNullOrNullValue(OfsId) || Utility.IsDBNullOrNullValue(OfReId) || Utility.IsDBNullOrNullValue(Department))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                if ((Department == "Document" && !CheckPermitionForEditForDoc(OfReId)) || (Department == "MemberShip" && !CheckPermitionForEditForOffice(OfReId)))
                    return;
                Edit(int.Parse(OfsId));
                break;
        }
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TblFile.Visible = true;

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;


        FinancialId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
    }

    void Insert()
    {
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

                trans.EndSave();

                btnEdit2.Enabled = true;
                btnEdit.Enabled = true;
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
                    arr[4] = 1;
                    Session["OffMenuArrayList"] = arr;
                }
                else
                {
                    CheckMenuImage(int.Parse(OfReId));
                    ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
                    arr[4] = 1;
                    Session["OffMenuArrayList"] = arr;
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
    void Edit(int OfsId)
    {
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

                trans.EndSave();
                //FinancialId.Value = Utility.EncryptQS(OfsId.ToString());
                //PgMode.Value = Utility.EncryptQS("Edit");
                //ASPxRoundPanel2.HeaderText = "ویرایش";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                Session["IsEdited_OffFin"] = true;
                HDComboValue.Value = CmbName.Value.ToString();
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
    void Enable()
    {
        CmbName.Enabled = true;
        txtDesc.Enabled = true;
        txtValue.Enabled = true;
        AspxGridFlp.Columns[2].Visible = true;
        flp.Enabled = true;

    }
    void Disable()
    {
        CmbName.Enabled = false;
        txtDesc.Enabled = false;
        txtValue.Enabled = false;
        AspxGridFlp.Columns[2].Visible = false;
        flp.Enabled = false;

    }
    void FillForm(int OfsId)
    {
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        FinManager.FindByCode(OfsId);
        if (FinManager.Count == 1)
        {

            CmbName.DataBind();
            CmbName.SelectedIndex = CmbName.Items.IndexOfValue(int.Parse(FinManager[0]["OfdId"].ToString()));
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
    void SetEnabled(bool Enabled)
    {
        btnEdit.Enabled = Enabled;
        btnEdit2.Enabled = Enabled;
        btnSave.Enabled = Enabled;
        btnSave2.Enabled = Enabled;
        BtnNew.Enabled = Enabled;
        BtnNew2.Enabled = Enabled;
    }
    void ClearForm()
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

    private Boolean CheckPermitionForEditForDoc(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        string Message = "";
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId, -1, 0);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
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
                                if (FirstNmcIdType == (int)TSP.DataManager.WorkFlowStateNmcIdType.OfficId && FirstNmcId == Utility.GetCurrentUser_MeId())
                                    return true;
                                else
                                    Message = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان ویرایش درخواست برای شما وجود ندارد";
                            }
                        }
                    }
                }
            }
        }
        if (!string.IsNullOrEmpty(Message))
            Message = "امکان ویرایش درخواست در این مرحله از جریان کار برای شما وجود ندارد";
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
        return false;
    }
    private Boolean CheckPermitionForEditForOffice(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        string Message = "";
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId, -1, 0);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
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
                                if (FirstNmcIdType == (int)TSP.DataManager.WorkFlowStateNmcIdType.OfficId && FirstNmcId == Utility.GetCurrentUser_MeId())
                                    return true;
                                else
                                    Message = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان ویرایش درخواست برای شما وجود ندارد";
                            }
                        }
                    }
                }
            }
        }
        if (!string.IsNullOrEmpty(Message))
            Message = "امکان ویرایش درخواست در این مرحله از جریان کار برای شما وجود ندارد";
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
        return false;
    }
}
