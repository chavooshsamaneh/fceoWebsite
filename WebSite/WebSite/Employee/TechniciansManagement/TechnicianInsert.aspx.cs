using DevExpress.Web;
using System;
using System.Data;
using System.IO;

public partial class Employee_TechniciansManagement_TechnicianInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    DataTable dtObsArea = null;
    DataTable dtObsGrade = null;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
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
            ResetSession();

            #region Set DataTableGrade
            if (Session["gradeManager"] == null)
            {
                dtObsGrade = new DataTable();
                dtObsGrade.Columns.Add("Id");
                dtObsGrade.Columns["Id"].AutoIncrement = true;
                dtObsGrade.Columns["Id"].AutoIncrementSeed = 1;
                dtObsGrade.Constraints.Add("PK_ID", dtObsGrade.Columns["Id"], true);
                dtObsGrade.Columns.Add("GrdId");
                dtObsGrade.Columns.Add("ResId");
                dtObsGrade.Columns.Add("TnReId");
                dtObsGrade.Columns.Add("InActive");
                dtObsGrade.Columns.Add("Status");
                dtObsGrade.Columns.Add("ResName");
                dtObsGrade.Columns.Add("GrdName");
                dtObsGrade.Columns.Add("MjName");

                Session["gradeManager"] = dtObsGrade;
            }
            else
                dtObsGrade = (DataTable)Session["gradeManager"];
            #endregion

            #region DataTable Area
            if (Session["ObsArea"] == null)
            {
                dtObsArea = new DataTable();
                dtObsArea.Columns.Add("Id");
                dtObsArea.Columns["Id"].AutoIncrement = true;
                dtObsArea.Columns["Id"].AutoIncrementSeed = 1;
                dtObsArea.Constraints.Add("PK_ID", dtObsArea.Columns["Id"], true);
                dtObsArea.Columns.Add("CitCode");
                dtObsArea.Columns.Add("TnReId");
                dtObsArea.Columns.Add("CitName");

                dtObsArea.Columns.Add("CitId");
                dtObsArea.Columns.Add("InActiveName");
                dtObsArea.Columns["CitId"].AutoIncrement = true;
                dtObsArea.Columns["CitId"].AutoIncrementSeed = 1;

                Session["ObsArea"] = dtObsArea;
            }
            else
                dtObsArea = (DataTable)Session["ObsArea"];
            #endregion

            if (string.IsNullOrEmpty(Request.QueryString["TnReId"]) || string.IsNullOrEmpty(Request.QueryString["OtpId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Technicians.aspx");
                return;
            }
            SetPermission();

                 
            OdbResponsibility.SelectParameters["ResIdFilterString"].DefaultValue = "("
           
                + ((int)TSP.DataManager.DocumentResponsibilityType.Traffic).ToString() + ","
                + ((int)TSP.DataManager.DocumentResponsibilityType.Urbanism).ToString() + ","
                + ((int)TSP.DataManager.DocumentResponsibilityType.Mapping).ToString() + ")";

            SetKey();
            SetMode();
            CheckWorkFlowPermission();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;


        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave1.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        if (Session["OtpImage"] != null)
            imgMember.ImageUrl = Session["OtpImage"].ToString();
        if (Session["AttachCommit"] != null)
        {
            HpCommit.NavigateUrl = Session["AttachCommit"].ToString();
            HpCommit.ClientVisible = true;
        }
        if (int.Parse(ComboType.Value.ToString()) == 3)//معمار تجربی
        {
            lblOtpCode.ClientVisible = false;
            txtOtpCode.ClientVisible = false;
            ComboMjId.ClientEnabled = false;
          
        }
    }

    protected void flp_Image_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

    protected void flpCommit_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageLicense(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }

    }

    #region btn click
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Enable();
        ClearForm();
        lblWfState.Text = "وضعیت درخواست: " + "نامشخص";
        OtherPersonId.Value = Utility.EncryptQS("");
        TnReId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        RoundPanelCity.ClientVisible = true;
        TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_MeId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanNew;
        btnSave1.Enabled = per.CanNew;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        //MenuTechnician.Enabled = false;

        //  this.ViewState["BtnArea"] = btnArea.Enabled;

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_MeId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnSave.Enabled = per.CanEdit;
        btnSave1.Enabled = per.CanEdit;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        PgMode.Value = Utility.EncryptQS("Edit");
        ASPxRoundPanel2.HeaderText = "ویرایش";
        Enable();
        ComboType.Enabled = false;

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ResetSession();

        string OtpId = Utility.DecryptQS(OtherPersonId.Value);
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(OtpId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            Response.Redirect("Technicians.aspx?PostId=" + OtherPersonId.Value + "&GrdFlt=" + GrdFlt);
        }
        else
        {
            Response.Redirect("Technicians.aspx");
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["gradeManager"] == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired);
            return;
        }
        string PageMode = Utility.DecryptQS(PgMode.Value);

        if ((((DataTable)Session["gradeManager"]).Rows.Count == 0) &&
            PageMode != "InActive")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات برای ذخیره کامل نمی باشد.صلاحیت های تایید شده را وارد نمایید";
            return;
        }

        string OtpId = Utility.DecryptQS(OtherPersonId.Value);
        string TechnicianReId = Utility.DecryptQS(TnReId.Value);

        if (PageMode == "Edit")
        {
            Edit(int.Parse(TechnicianReId));
        }
        else if (PageMode == "New")
        {
            Insert();
        }
        else if (PageMode == "Change")
        {
            InsertChangeReq(int.Parse(OtpId));
        }
        else if (PageMode == "InActive")
        {
            InActive(int.Parse(OtpId), Convert.ToInt32(TechnicianReId));
        }
    }

    protected void btnArea_Click(object sender, EventArgs e)
    {
        Response.Redirect("TechnicianActivityAreas.aspx?OtpId=" + OtherPersonId.Value + "&PageMode=" + PgMode.Value + "&AgentId=" + Utility.EncryptQS(CmbAgent.Value.ToString()));
    }

    protected void btnAddCity_Click(object sender, EventArgs e)
    {
        if (Session["ObsArea"] != null)
        {
            dtObsArea = (DataTable)Session["ObsArea"];
            if (dtObsArea.Rows.Count == 2)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "شما قادر به انتخاب بیش از دو شهر نمی باشید.";
                return;
            }
            for (int i = 0; i < dtObsArea.Rows.Count; i++)
            {

                if (dtObsArea.Rows[i].RowState != DataRowState.Deleted && dtObsArea.Rows[i]["CitName"].ToString() == cmbRegionOfCity.SelectedItem.Text)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "شهر انتخاب شده تکراری می باشد.";
                    return;
                }
            }
            DataRow dr = dtObsArea.NewRow();

            try
            {
                TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
                CityManager.FindByCode(Convert.ToInt32(cmbRegionOfCity.SelectedItem.Value.ToString()));

                dr["CitId"] = CityManager[0]["CitId"].ToString();
                dr["CitName"] = CityManager[0]["CitName"].ToString();
                dr["CitCode"] = CityManager[0]["CitCode"].ToString();
                dr["TnReId"] = -1;
                dr["InActiveName"] = "فعال";

                dtObsArea.Rows.Add(dr);
                GridViewCity.DataSource = dtObsArea;
                GridViewCity.DataBind();

                cmbRegionOfCity.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Utility.SaveWebsiteError(ex);
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }
        }
    }

    protected void btnAddResponseblity_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ComboType.SelectedItem.Value) == 3)
        {
            ComboMjId.SelectedIndex = ComboMjId.Items.FindByValue("3").Index;
        }
        if (ComboMjId.Value == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ابتدا رشته فرد را انتخاب نمایید";
            return;
        }
        if (Session["gradeManager"] == null)
        {
            return;
        }
        dtObsGrade = (DataTable)Session["gradeManager"];

        try
        {
            if (Convert.ToInt32(ComboType.SelectedItem.Value) == 1 && ComboResp.Value == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "صلاحیت را انتخاب نمایید";
                return;
            }
            if (ComboGrade.Value == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "پایه را انتخاب نمایید";
                return;
            }

            for (int i = 0; i < dtObsGrade.Rows.Count; i++)
            {
                if (dtObsGrade.Rows[i].RowState != DataRowState.Deleted && dtObsGrade.Rows[i]["ResName"].ToString() == ComboResp.SelectedItem.Text && Convert.ToInt32(dtObsGrade.Rows[i]["GrdId"]) == Convert.ToInt32(ComboGrade.Value) &&
                    Convert.ToInt32(dtObsGrade.Rows[i]["InActive"]) == 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "صلاحیت وارد شده تکراری می باشد";
                    //FillGrid();
                    return;
                }
            }
            DataRow dr = dtObsGrade.NewRow();

            dr["ResId"] = ComboResp.Value;

            //if (int.Parse(ComboType.Value.ToString()) == 1)
            //    dr["ResId"] = ComboResp.Value;
            //else
            //    dr["ResId"] = DBNull.Value;

            dr["TnReId"] = -1;
            //dr["UserId"] = Utility.GetCurrentUser_UserId();
            //dr["ModifiedDate"] = DateTime.Now;
            //if (int.Parse(ComboType.Value.ToString()) == 3)
            //    dr["ResName"] = "- - -";
            //else
            dr["ResName"] = ComboResp.SelectedItem.Text;
            dr["GrdId"] = ComboGrade.Value;
            dr["GrdName"] = ComboGrade.SelectedItem.Text;
            if (ComboMjId.Value != null)
                dr["MjName"] = ComboMjId.SelectedItem.Text;

            dr["InActive"] = 0;
            dr["Status"] = "فعال";
            dr.EndEdit();
            dtObsGrade.Rows.Add(dr);
            GridViewResponseblity.DataSource = dtObsGrade;
            GridViewResponseblity.DataBind();

            Session["gradeManager"] = dtObsGrade;
            ComboGrade.SelectedIndex = -1;
            ComboResp.SelectedIndex = -1;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
        }

    }
    #endregion

    protected void GridViewResponseblity_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        GridViewResponseblity.DataSource = (DataTable)Session["gradeManager"];
        GridViewResponseblity.DataBind();

        int Id = -1;
        if (GridViewResponseblity.FocusedRowIndex > -1)
        {
            Id = GridViewResponseblity.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;
        }
        else
        {
            dtObsGrade = (DataTable)Session["gradeManager"];
            dtObsGrade.Rows.Find(e.Keys["Id"]).Delete();
            //dtObsGrade.AcceptChanges();

            Session["gradeManager"] = dtObsGrade;
            GridViewResponseblity.DataSource = (DataTable)Session["gradeManager"];
            GridViewResponseblity.DataBind();
           
        }
    }

    protected void GridViewResponseblity_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (TnReId.Value != null)
        {
            string TechnReId = Utility.DecryptQS(TnReId.Value);
            DataRow dr = GridViewResponseblity.GetDataRow(e.VisibleIndex);
            if (dr != null)
            {
                if (dr.RowState == DataRowState.Unchanged)
                {
                    string CurretnTnReId = e.GetValue("TnReId").ToString();
                    if (TechnReId == CurretnTnReId)
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }
                }
                if (dr.RowState == DataRowState.Added)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }
    }

    protected void GridViewResponseblity_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //gridGrade
        if (e.Parameters == "btnAdd")
        {
            btnAddResponseblity_Click(null, null);
        }
    }

    #region GridViewCity
    protected void GridViewCity_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        GridViewCity.DataSource = (DataTable)Session["ObsArea"];
        GridViewCity.DataBind();

        int Id = -1;
        if (GridViewCity.FocusedRowIndex > -1)
        {
            Id = GridViewCity.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {
            dtObsArea = (DataTable)Session["ObsArea"];
            dtObsArea.Rows.Find(e.Keys["Id"]).Delete();
           // dtObsArea.AcceptChanges();

             Session["ObsArea"] = dtObsArea;
            GridViewCity.DataSource = (DataTable)Session["ObsArea"];
            GridViewCity.DataBind();
          
        }
    }

    protected void CmbCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        RegionOfCity();
    }
 
    protected void CmbAgent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!Utility.IsDBNullOrNullValue(CmbAgent.SelectedItem.Value))
        {
            int AgentId = int.Parse(CmbAgent.SelectedItem.Value.ToString());
            ObjectDataSourceCity.SelectParameters["AgentId"].DefaultValue = AgentId.ToString();

        }
        CmbCity.DataBind();
        CmbCity.SelectedIndex = -1;
    }

    protected void GridViewCity_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "")
        {
            dtObsArea = (DataTable)Session["ObsArea"];
            GridViewCity.DataSource = dtObsArea;
            GridViewCity.DataBind();
        }
    }

    protected void GridViewCity_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (TnReId.Value != null)
        {
            string TechnReId = Utility.DecryptQS(TnReId.Value);
            DataRow dr = GridViewCity.GetDataRow(e.VisibleIndex);
            if (dr != null)
            {
                if (dr.RowState == DataRowState.Unchanged)
                {
                    string CurretnTnReId = e.GetValue("TnReId").ToString();
                    if (TechnReId == CurretnTnReId)
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }
                }
                if (dr.RowState == DataRowState.Added)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }
    }
    #endregion

    #endregion

    #region Methods
    private void SetPermission()
    {

        TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        BtnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        btnSave.Enabled = per.CanNew || per.CanEdit;
        btnSave1.Enabled = per.CanNew || per.CanEdit;
    }

    #region set Key-Mode
    private void SetKey()
    {
        #region SetKey
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            OtherPersonId.Value = Server.HtmlDecode(Request.QueryString["OtpId"]).ToString();
            TnReId.Value = Server.HtmlDecode(Request.QueryString["TnReId"]).ToString();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }


        string OtpId = Utility.DecryptQS(OtherPersonId.Value);
        string TechnicianReId = Utility.DecryptQS(TnReId.Value);
        TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
        TechnicianRequestManager.FindByCode(int.Parse(TechnicianReId));
        if (int.Parse(TechnicianReId) != -1 && TechnicianRequestManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(TechnicianRequestManager[0]["TaskName"]))
                lblWfState.Text = "وضعیت درخواست: " + TechnicianRequestManager[0]["TaskName"].ToString();
            else
                lblWfState.Text = "وضعیت درخواست: " + "نامشخص";
        }
        else if (int.Parse(TechnicianReId) == -1)
        {
            TechnicianRequestManager.FindByOtpId(int.Parse(OtpId));
            if (TechnicianRequestManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(TechnicianRequestManager[0]["TaskName"]))
                    lblWfState.Text = "وضعیت درخواست: " + TechnicianRequestManager[0]["TaskName"].ToString();
                else
                    lblWfState.Text = "وضعیت درخواست: " + "نامشخص";
            }
            else
            {
                lblWfState.Text = "وضعیت درخواست: " + "نامشخص";
            }
        }
        else
        {
            lblWfState.Text = "وضعیت درخواست: " + "نامشخص";

        }


        #endregion
    }

    private void SetMode()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string OtpId = Utility.DecryptQS(OtherPersonId.Value);
        string TechnicianReId = Utility.DecryptQS(TnReId.Value);
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        #region SetMode
        switch (PageMode)
        {
            case "View":
                Disable();

                if (string.IsNullOrEmpty(OtpId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                if (string.IsNullOrEmpty(TechnicianReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                btnSave.Enabled = false;
                btnSave1.Enabled = false;
                int TnReqId = int.Parse(Utility.DecryptQS(TnReId.Value));
                if (TnReqId == -1)
                {
                    FillForm(int.Parse(OtpId));
                }
                else
                {
                    FillFormByReguest(TnReqId);
                }
                ASPxRoundPanel2.HeaderText = "مشاهده";
                InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.TechnicianRequest, int.Parse(TechnicianReId));
                break;


            case "New":

                ASPxRoundPanel2.HeaderText = "جدید";
                RoundPanelCity.ClientVisible = true;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;

                break;

            case "Edit":

                if (string.IsNullOrEmpty(OtpId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                ComboType.Enabled = false;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;

                FillFormByReguest(int.Parse(TechnicianReId));
                ASPxRoundPanel2.HeaderText = "ویرایش";
                break;

            case "Change":

                if (string.IsNullOrEmpty(OtpId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                ComboType.Enabled = false;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                FillForm(int.Parse(OtpId));
                ASPxRoundPanel2.HeaderText = "درخواست تغییرات";
                break;
            case "InActive":
                Disable();

                if (string.IsNullOrEmpty(OtpId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                if (string.IsNullOrEmpty(TechnicianReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                FillForm(int.Parse(OtpId));
                ASPxRoundPanel2.HeaderText = "درخواست غیر فعال کردن";
                break;

        }
        #endregion
    }
    #endregion

    #region Insert
    protected void Insert()
    {
        if (IsPageRefresh)
        {
            return;
        }
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OtherPersonManager OthManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();

        TSP.DataManager.DocOffMemberAcceptedGradeManager gradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        TSP.DataManager.AttachmentsManager AttachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TechniciansActivityAreasManager TechniciansActivityAreasManager = new TSP.DataManager.TechniciansActivityAreasManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(OthManager);
     
        trans.Add(gradeManager);
        trans.Add(AttachManager);
        trans.Add(TechnicianRequestManager);
        trans.Add(TechniciansActivityAreasManager);

        int CurrentNmcId = -1;
        bool chAxImg = false;
        bool Licence = false;
        try
        {
            CurrentNmcId = FindNmcId();
            if (CurrentNmcId <= 0)
            {
                DivReport.Visible = true;
                LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
                return;
            }
            if (ComboType.Value == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "نوع عضو را انتخاب نمایید";
                return;
            }
            if (txtFileNo.Text == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "شماره پروانه اشتغال بکار را وارد نمایید";
                return;
            }
            if (int.Parse(ComboType.Value.ToString()) != 3)
            {
                OthManager.FindKardanAndMemarByOtpCode(txtOtpCode.Text.Trim(), (int)TSP.DataManager.OtherPersonType.Kardan);
                if (OthManager.Count > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد کانون کاردان ها تکراری می باشد";
                    return;
                }
            }
        }
        catch (Exception ex)
        {

            Utility.SaveWebsiteError(ex);
            return;
        }

        try
        {
            trans.BeginSave();
            #region Insert OtherPerson
            DataRow drOthers = OthManager.NewRow();

            drOthers["OtpType"] = int.Parse(ComboType.Value.ToString());
            if (txtOtpCode.Text != null)
                drOthers["OtpCode"] = txtOtpCode.Text;

            if (ComboMjId.Value != null)
                drOthers["MjId"] = ComboMjId.Value;
            drOthers["MjName"] = txtMjName.Text;

            drOthers["FirstName"] = txtFirstName.Text;
            drOthers["FatherName"] = txtFatherName.Text;
            drOthers["LastName"] = txtLastName.Text;
            drOthers["BankAccNo"] = txtBankAccNo.Text;
            drOthers["Bank"] = cmbBank.Value;

            drOthers["IdNo"] = txtIdNo.Text;
            drOthers["SSN"] = txtSSN.Text;
            drOthers["BirthPlace"] = txtBirthPlace.Text;
            drOthers["BirthDate"] = txtBirthDate.Text;
            drOthers["FileNo"] = txtFileNo.Text;
            drOthers["FileNoDate"] = txtFileNoDate.Text;
            drOthers["Description"] = txtDesc.Text;
            if (txtTel.Text != "" && txtTel_pre.Text != "")
                drOthers["Tel"] = txtTel_pre.Text + "-" + txtTel.Text;
            else if (txtTel.Text != "")
                drOthers["Tel"] = txtTel.Text;
            drOthers["MobileNo"] = txtMobile.Text;
            drOthers["Address"] = txtAddress.Text;
            drOthers["UserId"] = Utility.GetCurrentUser_UserId();
            if (CmbAgent.Value != null)
                drOthers["AgentId"] = CmbAgent.Value;
            if (CmbCity.Value != null)
                drOthers["CitId"] = CmbCity.Value;
            drOthers["ModifiedDate"] = DateTime.Now;
            if (Session["OtpImage"] != null)
            {
                drOthers["ImageUrl"] = "~/Image/OtherPerson/Person/" + Path.GetFileName(Session["OtpImage"].ToString());
                chAxImg = true;
            }
            else drOthers["ImageUrl"] = "";
            if (Session["AttachCommit"] != null)
            {
                drOthers["LicenceImgUrl"] = "~/Image/OtherPerson/License/" + Path.GetFileName(Session["AttachCommit"].ToString());
                Licence = true;
            }
            else drOthers["LicenceImgUrl"] = "";
            OthManager.AddRow(drOthers);
            if (OthManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            #endregion
            #region Insert TechnicianRequest
            DataRow TechReqRow = TechnicianRequestManager.NewRow();
            int OtpId = int.Parse(OthManager[OthManager.Count - 1]["OtpId"].ToString());
            TechReqRow["OtpId"] = OtpId;
            TechReqRow["Status"] = 0;
            TechReqRow["IsConfirmed"] = 0;
            TechReqRow["InActive"] = 0;
            if (txtOtpCode.Text != null)
                TechReqRow["OtpCode"] = txtOtpCode.Text;

            if (ComboMjId.Value != null)
                TechReqRow["MjId"] = ComboMjId.Value;
            TechReqRow["MjName"] = txtMjName.Text;

            TechReqRow["FirstName"] = txtFirstName.Text;
            TechReqRow["FatherName"] = txtFatherName.Text;
            TechReqRow["LastName"] = txtLastName.Text;
            TechReqRow["BankAccNo"] = txtBankAccNo.Text;
            TechReqRow["Bank"] = cmbBank.Value;
            TechReqRow["IdNo"] = txtIdNo.Text;
            TechReqRow["SSN"] = txtSSN.Text;
            TechReqRow["BirthPlace"] = txtBirthPlace.Text;
            TechReqRow["BirthDate"] = txtBirthDate.Text;
            TechReqRow["FileNo"] = txtFileNo.Text;
            TechReqRow["FileDate"] = txtFileNoDate.Text;
        
            if (txtTel.Text != "" && txtTel_pre.Text != "")
                TechReqRow["Tel"] = txtTel_pre.Text + "-" + txtTel.Text;
            else if (txtTel.Text != "")
                TechReqRow["Tel"] = txtTel.Text;
            TechReqRow["MobileNo"] = txtMobile.Text;
            TechReqRow["Address"] = txtAddress.Text;
            if (CmbAgent.Value != null)
                TechReqRow["AgentId"] = CmbAgent.Value;
            if (CmbCity.Value != null)
                TechReqRow["CitId"] = CmbCity.Value;

            if (Session["AttachCommit"] != null)
            {
                TechReqRow["LicenceImgUrl"] = "~/Image/OtherPerson/License/" + Path.GetFileName(Session["AttachCommit"].ToString());
                Licence = true;
            }
            else TechReqRow["LicenceImgUrl"] = "";

            if (Session["OtpImage"] != null)
            {
                TechReqRow["ImageUrl"] = "~/Image/OtherPerson/Person/" + Path.GetFileName(Session["OtpImage"].ToString());
                chAxImg = true;
            }
            else TechReqRow["ImageUrl"] = "";

            TechReqRow["UserId"] = Utility.GetCurrentUser_UserId();
            TechReqRow["ModifiedDate"] = DateTime.Now;
            TechnicianRequestManager.AddRow(TechReqRow);
            if (TechnicianRequestManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            int TnReId = Convert.ToInt32(TechnicianRequestManager[0]["TnReId"]);
            #endregion
            #region Grade
            dtObsGrade = (DataTable)Session["gradeManager"];
            if (dtObsGrade.Rows.Count > 0)
            {
                for (int i = 0; i < dtObsGrade.Rows.Count; i++)
                {
                    DataRow drGrade = gradeManager.NewRow();
                    drGrade["TnReId"] = TnReId;
                    drGrade["GrdId"] = int.Parse(dtObsGrade.Rows[i]["GrdId"].ToString());
                    drGrade["ResId"] = dtObsGrade.Rows[i]["ResId"].ToString();
                    drGrade["InActive"] = 0;
                    drGrade["Status"] = dtObsGrade.Rows[i]["Status"].ToString();
                    drGrade["ModifiedDate"] = DateTime.Now;
                    drGrade["UserId"] = Utility.GetCurrentUser_UserId();
                    gradeManager.AddRow(drGrade);
                    gradeManager.Save();
                    gradeManager.DataTable.AcceptChanges();
                }
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "پایه و صلاحیت را انتخاب ثبت نمایید";
                return;
            }
            #endregion

            #region ObsRegion
            dtObsArea = (DataTable)Session["ObsArea"];
            if (dtObsArea.DefaultView.Count > 0)
            {
                for (int i = 0; i < dtObsArea.DefaultView.Count; i++)
                {
                    DataRow drCity = TechniciansActivityAreasManager.NewRow();
                    drCity["TnReId"] = TnReId;
                    drCity["MGId"] = DBNull.Value;
                    drCity["CitId"] = dtObsArea.Rows[i]["CitId"].ToString();
                    drCity["CreateDate"] = Utility.GetDateOfToday();
                    drCity["UserId"] = Utility.GetCurrentUser_UserId();
                    drCity["ModifiedDate"] = DateTime.Now;
                    TechniciansActivityAreasManager.AddRow(drCity);
                }
                TechniciansActivityAreasManager.Save();
            }
            #endregion

            int TableId = int.Parse(TechnicianRequestManager[0]["TnReId"].ToString());
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTechnicianRequestInfo;
            int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
            if (WfStart > 0)
            {
                trans.EndSave();
                PgMode.Value = Utility.EncryptQS("Edit");
                this.TnReId.Value = Utility.EncryptQS(TableId.ToString());
                OtherPersonId.Value = Utility.EncryptQS(OthManager[0]["OtpId"].ToString());
                ASPxRoundPanel2.HeaderText = "ویرایش";
                TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
                WorkFlowTaskManager.FindByTaskCode(TaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[0]["TaskName"]))
                    {
                       
                        lblWfState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[0]["TaskName"].ToString();
                    }
                }
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

        if (chAxImg == true)
        {
            try
            {
                string Soource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["OtpImage"].ToString());
                string Target = Server.MapPath("~/Image/OtherPerson/Person/") + Path.GetFileName(Session["OtpImage"].ToString());
                System.IO.File.Copy(Soource, Target, true);
            }
            catch (Exception)
            {
            }
        }
        if (Licence)
        {
            try
            {
                string Soource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["AttachCommit"].ToString());
                string Target = Server.MapPath("~/Image/OtherPerson/License/") + Path.GetFileName(Session["AttachCommit"].ToString());
                System.IO.File.Copy(Soource, Target, true);
                HpCommit.ClientVisible = true;
                HpCommit.NavigateUrl = "~/Image/OtherPerson/License/" + Path.GetFileName(Session["AttachCommit"].ToString());
            }
            catch (Exception)
            {
            }
        }
        Session["AttachCommit"] = null;

        Session["OtpImage"] = null;

    }

    private void InsertChangeReq(int OtpId)
    {
        if (IsPageRefresh)
        {
            return;
        }
        int TnReId = -2;
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TechniciansActivityAreasManager TnActAreasManager = new TSP.DataManager.TechniciansActivityAreasManager();
        TSP.DataManager.DocOffMemberAcceptedGradeManager gradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        TSP.DataManager.RequestInActivesManager requestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.RequestInActivesManager requestInActives = new TSP.DataManager.RequestInActivesManager();
        TransactionManager.Add(requestInActives);
        TransactionManager.Add(requestInActivesManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(TechnicianRequestManager);
        TransactionManager.Add(TnActAreasManager);
        TransactionManager.Add(gradeManager);

        bool chAxImg = false;
        bool Licence = false;
        try
        {
            TransactionManager.BeginSave();
            int CurrentNmcId = FindNmcId();
            if (CurrentNmcId <= 0)
            {
                DivReport.Visible = true;
                LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
                return;
            }

            DataRow TechReqRow = TechnicianRequestManager.NewRow();
            TechReqRow["OtpId"] = OtpId;
            TechReqRow["Status"] = 1;
            TechReqRow["IsConfirmed"] = 0;
            TechReqRow["InActive"] = 0;
            if (txtOtpCode.Text != null)
                TechReqRow["OtpCode"] = txtOtpCode.Text;

            if (ComboMjId.Value != null)
                TechReqRow["MjId"] = ComboMjId.Value;
            TechReqRow["MjName"] = txtMjName.Text;

            TechReqRow["FirstName"] = txtFirstName.Text;
            TechReqRow["FatherName"] = txtFatherName.Text;
            TechReqRow["LastName"] = txtLastName.Text;
            TechReqRow["BankAccNo"] = txtBankAccNo.Text;
            TechReqRow["Bank"] = cmbBank.Value;
            TechReqRow["IdNo"] = txtIdNo.Text;
            TechReqRow["SSN"] = txtSSN.Text;
            TechReqRow["BirthPlace"] = txtBirthPlace.Text;
            TechReqRow["BirthDate"] = txtBirthDate.Text;
            TechReqRow["FileNo"] = txtFileNo.Text;
            TechReqRow["FileDate"] = txtFileNoDate.Text;

            if (txtTel.Text != "" && txtTel_pre.Text != "")
                TechReqRow["Tel"] = txtTel_pre.Text + "-" + txtTel.Text;
            else if (txtTel.Text != "")
                TechReqRow["Tel"] = txtTel.Text;
            TechReqRow["MobileNo"] = txtMobile.Text;
            TechReqRow["Address"] = txtAddress.Text;
            TechReqRow["AgentId"] = CmbAgent.Value;
            TechReqRow["CitId"] = CmbCity.Value;

            if (Session["AttachCommit"] != null)
            {
                TechReqRow["LicenceImgUrl"] = "~/Image/OtherPerson/License/" + Path.GetFileName(Session["AttachCommit"].ToString());
                Licence = true;
            }

            if (Session["OtpImage"] != null)
            {
                TechReqRow["ImageUrl"] = "~/Image/OtherPerson/Person/" + Path.GetFileName(Session["OtpImage"].ToString());
                chAxImg = true;
            }

            TechReqRow["UserId"] = Utility.GetCurrentUser_UserId();
            TechReqRow["ModifiedDate"] = DateTime.Now;
            TechnicianRequestManager.AddRow(TechReqRow);
            if (TechnicianRequestManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            TechnicianRequestManager.DataTable.AcceptChanges();
            TnReId = Convert.ToInt32(TechnicianRequestManager[0]["TnReId"]);



            #region Grade

            dtObsGrade = (DataTable)Session["gradeManager"];
            if (dtObsGrade.GetChanges() != null)
            {
                DataRow[] delRowsGrade = dtObsGrade.Select(null, null, DataViewRowState.Deleted);
                DataRow[] insRowsGrade = dtObsGrade.Select(null, null, DataViewRowState.Added);
                #region Delete/InActive
                for (int i = 0; i < delRowsGrade.Length; i++)
                {
                    gradeManager.FindByCode(int.Parse(delRowsGrade[i]["Id", DataRowVersion.Original].ToString()));
                    if (gradeManager.Count > 0)
                    {

                        requestInActives.FindByTableIdTableType(Convert.ToInt32(gradeManager[0]["MGId"]), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMemberAcceptedGrade), TnReId);
                        if (requestInActives.Count == 0)
                            InsertInActive(requestInActivesManager, Convert.ToInt32(gradeManager[0]["MGId"]), TnReId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMemberAcceptedGrade), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TechnicianRequest));
                    }
                }
                gradeManager.DataTable.AcceptChanges();
                #endregion
                #region  InsertNewRow
                if (insRowsGrade.Length > 0)
                {
                    for (int i = 0; i < insRowsGrade.Length; i++)
                    {
                        DataRow drGrade = gradeManager.NewRow();
                        drGrade["TnReId"] = TnReId;
                        drGrade["GrdId"] = int.Parse(insRowsGrade[i]["GrdId"].ToString());
                        drGrade["ResId"] = insRowsGrade[i]["ResId"].ToString();
                        drGrade["InActive"] = 0;
                        drGrade["Status"] = insRowsGrade[i]["Status"].ToString();
                        drGrade["ModifiedDate"] = DateTime.Now;
                        drGrade["UserId"] = Utility.GetCurrentUser_UserId();
                        gradeManager.AddRow(drGrade);
                        gradeManager.Save();
                        gradeManager.DataTable.AcceptChanges();

                    }
                    TnActAreasManager.Save();
                }
                #endregion

            }
            #endregion


            #region ObsArea            

            dtObsArea = (DataTable)Session["ObsArea"];
            if (dtObsArea.GetChanges() != null)
            {
                DataRow[] delRowsArea = dtObsArea.Select(null, null, DataViewRowState.Deleted);
                DataRow[] insRowsArea = dtObsArea.Select(null, null, DataViewRowState.Added);
                #region Delete/InActive
                for (int i = 0; i < delRowsArea.Length; i++)
                {
                    TnActAreasManager.FindByCode(int.Parse(delRowsArea[i]["Id", DataRowVersion.Original].ToString()));
                    if (TnActAreasManager.Count > 0)
                    {

                        requestInActives.FindByTableIdTableType(Convert.ToInt32(TnActAreasManager[0]["ActAreaId"]), (int)TSP.DataManager.TableCodes.TechniciansActivityAreas, TnReId);
                        if (requestInActives.Count == 0)
                            InsertInActive(requestInActivesManager, Convert.ToInt32(TnActAreasManager[0]["ActAreaId"]), TnReId, (int)TSP.DataManager.TableCodes.TechniciansActivityAreas, (int)TSP.DataManager.TableCodes.TechnicianRequest);
                    }
                }
                TnActAreasManager.DataTable.AcceptChanges();
                #endregion
                #region  InsertNewRow
                if (insRowsArea.Length > 0)
                {
                    for (int i = 0; i < insRowsArea.Length; i++)
                    {
                        DataRow drGrdArea = TnActAreasManager.NewRow();
                        drGrdArea["TnReId"] = TnReId;
                        drGrdArea["MGId"] = DBNull.Value;
                        drGrdArea["CitId"] = insRowsArea[i]["CitId"].ToString();
                        drGrdArea["CreateDate"] = Utility.GetDateOfToday();
                        drGrdArea["UserId"] = Utility.GetCurrentUser_UserId();
                        drGrdArea["ModifiedDate"] = DateTime.Now;
                        TnActAreasManager.AddRow(drGrdArea);
                    }
                    TnActAreasManager.Save();
                }
                #endregion

            }
            gradeManager.CurrentFilter = "";
            #endregion

            int TableId = TnReId;
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTechnicianRequestInfo;
            int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
            if (WfStart <= 0)
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }

            TransactionManager.EndSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخيره انجام شد.";
            this.TnReId.Value = Utility.EncryptQS(TnReId.ToString());
            PgMode.Value = Utility.EncryptQS("Edit");
            this.TnReId.Value = Utility.EncryptQS(TechnicianRequestManager[TechnicianRequestManager.Count - 1]["TnReId"].ToString());
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[0]["TaskName"]))
                {
                    lblWfState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[0]["TaskName"].ToString();
                }
            }

        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
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
        try
        {
            string Soource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["OtpImage"].ToString());
            string Target = Server.MapPath("~/Image/OtherPerson/Person/") + Path.GetFileName(Session["OtpImage"].ToString());
            System.IO.File.Copy(Soource, Target, true);
        }
        catch (Exception)
        {
        }
        try
        {
            string Soource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["AttachCommit"].ToString());
            string Target = Server.MapPath("~/Image/OtherPerson/License/") + Path.GetFileName(Session["AttachCommit"].ToString());
            System.IO.File.Copy(Soource, Target, true);
            HpCommit.ClientVisible = true;
            HpCommit.NavigateUrl = "~/Image/OtherPerson/License/" + Path.GetFileName(Session["AttachCommit"].ToString());
        }
        catch (Exception)
        {
        }
    }

    private void InsertWorkFlowStateView(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "View", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در مشاهده اطلاعات انجام گرفته است";
            }
        }
        catch (Exception err)
        {
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

    protected void InsertInActive(TSP.DataManager.RequestInActivesManager Manager, int TableId, int ReqId, int TableType, int ReTableType)
    {
        DataRow dr = Manager.NewRow();
        dr["TableId"] = TableId;
        dr["TableType"] = TableType;
        dr["ReqId"] = ReqId;
        dr["ReqType"] = ReTableType;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        Manager.Save();
        Manager.DataTable.AcceptChanges();
        GridViewResponseblity.DataBind();
    }

    private void InActive(int OtpId, int TnReId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(TechnicianRequestManager);

        try
        {
            TransactionManager.BeginSave();
            int CurrentNmcId = FindNmcId();
            if (CurrentNmcId <= 0)
            {
                DivReport.Visible = true;
                LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
                return;
            }
            #region Insert Request
            DataRow TechReqRow = TechnicianRequestManager.NewRow();
            TechReqRow["OtpId"] = OtpId;
            TechReqRow["Status"] = 1;
            TechReqRow["IsConfirmed"] = 0;
            TechReqRow["InActive"] = 1;
            if (txtOtpCode.Text != null)
                TechReqRow["OtpCode"] = txtOtpCode.Text;

            if (ComboMjId.Value != null)
                TechReqRow["MjId"] = ComboMjId.Value;
            TechReqRow["MjName"] = txtMjName.Text;

            TechReqRow["FirstName"] = txtFirstName.Text;
            TechReqRow["FatherName"] = txtFatherName.Text;
            TechReqRow["LastName"] = txtLastName.Text;
            TechReqRow["IdNo"] = txtIdNo.Text;
            TechReqRow["SSN"] = txtSSN.Text;
            TechReqRow["BirthPlace"] = txtBirthPlace.Text;
            TechReqRow["BirthDate"] = txtBirthDate.Text;
            TechReqRow["FileNo"] = txtFileNo.Text;
            TechReqRow["FileDate"] = txtFileNoDate.Text;
            // TechReqRow["Description"] = txtDesc.Text;
            if (txtTel.Text != "" && txtTel_pre.Text != "")
                TechReqRow["Tel"] = txtTel_pre.Text + "-" + txtTel.Text;
            else if (txtTel.Text != "")
                TechReqRow["Tel"] = txtTel.Text;
            TechReqRow["MobileNo"] = txtMobile.Text;
            TechReqRow["Address"] = txtAddress.Text;
            TechReqRow["AgentId"] = CmbAgent.Value;
            TechReqRow["CitId"] = CmbCity.Value;

            if (Session["AttachCommit"] != null)
            {
                TechReqRow["LicenceImgUrl"] = "~/Image/OtherPerson/License/" + Path.GetFileName(Session["AttachCommit"].ToString());
            }

            if (Session["OtpImage"] != null)
            {
                TechReqRow["ImageUrl"] = "~/Image/OtherPerson/Person/" + Path.GetFileName(Session["OtpImage"].ToString());
            }

            TechReqRow["UserId"] = Utility.GetCurrentUser_UserId();
            TechReqRow["ModifiedDate"] = DateTime.Now;
            TechnicianRequestManager.AddRow(TechReqRow);
            #endregion
            if (TechnicianRequestManager.Save() > 0)
            {
                int TableId = int.Parse(TechnicianRequestManager[0]["TnReId"].ToString());
                int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTechnicianRequestInfo;
                int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
                if (WfStart > 0)
                {
                    TransactionManager.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخيره انجام شد.";
                    PgMode.Value = Utility.EncryptQS("View");
                    btnSave.Enabled = false;
                    btnSave1.Enabled = false;
                    this.TnReId.Value = Utility.EncryptQS(TechnicianRequestManager[TechnicianRequestManager.Count - 1]["TnReId"].ToString());
                    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

                    WorkFlowTaskManager.FindByTaskCode(TaskCode);
                    if (WorkFlowTaskManager.Count > 0)
                    {
                        if (!Utility.IsDBNullOrNullValue(WorkFlowTaskManager[0]["TaskName"]))
                        {
                            lblWfState.Text = "وضعیت درخواست: " + WorkFlowTaskManager[0]["TaskName"].ToString();
                        }
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            TransactionManager.CancelSave();

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
    #endregion

    #region Edit
    protected void Edit(int TnReId)
    {
        if (IsPageRefresh)
        {
            return;
        }

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
        TSP.DataManager.AttachmentsManager AttachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TechniciansActivityAreasManager TnActAreasManager = new TSP.DataManager.TechniciansActivityAreasManager();
        TSP.DataManager.OtherPersonManager OthManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.RequestInActivesManager requestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.RequestInActivesManager requestInActives = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.DocOffMemberAcceptedGradeManager gradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        trans.Add(gradeManager);
        trans.Add(requestInActivesManager);
        trans.Add(TnActAreasManager);
        trans.Add(TechnicianRequestManager);
        trans.Add(requestInActives);

        trans.Add(AttachManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(OthManager);

        bool chAxImg = false;
        bool Licence = false;

        try
        {
            trans.BeginSave();
            TechnicianRequestManager.FindByCode(TnReId);
            if (TechnicianRequestManager.Count == 1)
            {
                int OtpId = int.Parse(TechnicianRequestManager[0]["OtpId"].ToString());
                OthManager.SelectOtherPersonKardanAndMemar(OtpId);
                if (Convert.ToInt32(OthManager[0]["OtpType"]) == 3)
                {
                    string S = txtFileNo.Text;

                    OthManager.FindMemarByFileNo(S, 3);
                    if (OthManager.Count > 0)
                    {
                        if (Convert.ToInt32(OthManager[0]["OtpId"]) != OtpId)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "شماره پروانه تکراری می باشد";
                            trans.CancelSave();
                            return;
                        }
                    }
                }
                else
                {

                    OthManager.FindKardanAndMemarByOtpCode(txtOtpCode.Text.Trim(), 1);
                    if (OthManager.Count > 0)
                    {
                        if (Convert.ToInt32(OthManager[0]["OtpId"]) != OtpId)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "کد کانون کاردان ها تکراری می باشد";
                            trans.CancelSave();
                            return;
                        }
                    }
                }

                #region Edit Oth Person when Status =0


                if (Convert.ToInt32(TechnicianRequestManager[0]["Status"]) == 0)
                {
                    OthManager[0].BeginEdit();

                    if (txtOtpCode.Text != null)
                        OthManager[0]["OtpCode"] = txtOtpCode.Text;

                    if (ComboMjId.Value != null)
                        OthManager[0]["MjId"] = ComboMjId.Value;
                    OthManager[0]["MjName"] = txtMjName.Text;

                    OthManager[0]["FirstName"] = txtFirstName.Text;
                    OthManager[0]["FatherName"] = txtFatherName.Text;
                    OthManager[0]["LastName"] = txtLastName.Text;
                    OthManager[0]["BankAccNo"] = txtBankAccNo.Text;
                    OthManager[0]["Bank"] = cmbBank.Value;
                    OthManager[0]["IdNo"] = txtIdNo.Text;
                    OthManager[0]["SSN"] = txtSSN.Text;
                    OthManager[0]["BirthPlace"] = txtBirthPlace.Text;
                    OthManager[0]["BirthDate"] = txtBirthDate.Text;
                    OthManager[0]["FileNo"] = txtFileNo.Text;
                    OthManager[0]["FileNoDate"] = txtFileNoDate.Text;

                    if (txtTel.Text != "" && txtTel_pre.Text != "")
                        OthManager[0]["Tel"] = txtTel_pre.Text + "-" + txtTel.Text;
                    else if (txtTel.Text != "")
                        OthManager[0]["Tel"] = txtTel.Text;
                    OthManager[0]["MobileNo"] = txtMobile.Text;
                    OthManager[0]["Address"] = txtAddress.Text;
                    OthManager[0]["AgentId"] = CmbAgent.Value;
                    OthManager[0]["CitId"] = CmbCity.Value;
                    OthManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    OthManager[0]["ModifiedDate"] = DateTime.Now;
                    if (Session["OtpImage"] != null)
                    {
                        OthManager[0]["ImageUrl"] = "~/Image/OtherPerson/Person/" + Path.GetFileName(Session["OtpImage"].ToString());
                        chAxImg = true;
                    }
                    if (Session["AttachCommit"] != null)
                    {
                        OthManager[0]["LicenceImgUrl"] = "~/Image/OtherPerson/License/" + Path.GetFileName(Session["AttachCommit"].ToString());
                        Licence = true;
                    }

                    OthManager[0].EndEdit();
                    if (OthManager.Save() <= 0)
                    {
                        trans.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                        return;
                    }

                }
                #endregion
                #region Edit TechnicianRequestManager            
                TechnicianRequestManager[0].BeginEdit();
                if (ComboMjId.Value != null)
                    TechnicianRequestManager[0]["MjId"] = ComboMjId.Value;
                TechnicianRequestManager[0]["MjName"] = txtMjName.Text;
                TechnicianRequestManager[0]["FirstName"] = txtFirstName.Text;
                if (txtOtpCode.Text != null)
                    TechnicianRequestManager[0]["OtpCode"] = txtOtpCode.Text;
                TechnicianRequestManager[0]["FatherName"] = txtFatherName.Text;
                TechnicianRequestManager[0]["LastName"] = txtLastName.Text;
                TechnicianRequestManager[0]["BankAccNo"] = txtBankAccNo.Text;
                TechnicianRequestManager[0]["Bank"] = cmbBank.Value;
                TechnicianRequestManager[0]["IdNo"] = txtIdNo.Text;
                TechnicianRequestManager[0]["SSN"] = txtSSN.Text;
                TechnicianRequestManager[0]["BirthPlace"] = txtBirthPlace.Text;
                TechnicianRequestManager[0]["BirthDate"] = txtBirthDate.Text;
                TechnicianRequestManager[0]["FileNo"] = txtFileNo.Text;
                TechnicianRequestManager[0]["FileDate"] = txtFileNoDate.Text;
                // TechnicianRequestManager[0]["Description"] = txtDesc.Text;
                if (txtTel.Text != "" && txtTel_pre.Text != "")
                    TechnicianRequestManager[0]["Tel"] = txtTel_pre.Text + "-" + txtTel.Text;
                else if (txtTel.Text != "")
                    TechnicianRequestManager[0]["Tel"] = txtTel.Text;
                TechnicianRequestManager[0]["MobileNo"] = txtMobile.Text;
                TechnicianRequestManager[0]["Address"] = txtAddress.Text;
                TechnicianRequestManager[0]["AgentId"] = CmbAgent.Value;
                TechnicianRequestManager[0]["CitId"] = CmbCity.Value;
                TechnicianRequestManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TechnicianRequestManager[0]["ModifiedDate"] = DateTime.Now;
                if (Session["OtpImage"] != null)
                {
                    TechnicianRequestManager[0]["ImageUrl"] = "~/Image/OtherPerson/Person/" + Path.GetFileName(Session["OtpImage"].ToString());
                    chAxImg = true;
                }
                if (Session["AttachCommit"] != null)
                {
                    TechnicianRequestManager[0]["ImageUrl"] = "~/Image/OtherPerson/License/" + Path.GetFileName(Session["AttachCommit"].ToString());
                    if ((!string.IsNullOrEmpty(TechnicianRequestManager[0]["ImageUrl"].ToString())) && (File.Exists(Server.MapPath(TechnicianRequestManager[0]["ImageUrl"].ToString()))))
                    {
                        File.Delete(Server.MapPath(TechnicianRequestManager[0]["ImageUrl"].ToString()));
                    }
                    HpCommit.NavigateUrl = Session["AttachCommit"].ToString();
                    TechnicianRequestManager[0]["ImageUrl"] = "~/Image/OtherPerson/License/" + Path.GetFileName(Session["AttachCommit"].ToString());
                }
                TechnicianRequestManager[0].EndEdit();

                if (TechnicianRequestManager.Save() <= 0)
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    return;
                }
                #endregion
                #region Grade
                dtObsGrade = (DataTable)Session["gradeManager"];
                if (dtObsGrade.GetChanges() != null)
                {
                    DataRow[] delRowsGrade = dtObsGrade.Select(null, null, DataViewRowState.Deleted);
                    DataRow[] insRowsGrade = dtObsGrade.Select(null, null, DataViewRowState.Added);
                    #region Delete/InActive
                    for (int i = 0; i < delRowsGrade.Length; i++)
                    {
                        gradeManager.FindByCode(int.Parse(delRowsGrade[i]["Id", DataRowVersion.Original].ToString()));
                        if (gradeManager.Count > 0)
                        {
                            if (Convert.ToInt32(gradeManager[0]["TnReId"]) == TnReId)
                            {
                                gradeManager[0].Delete();
                                gradeManager.Save();
                                gradeManager.DataTable.AcceptChanges();
                            }
                            else
                            {
                                requestInActives.FindByTableIdTableType(Convert.ToInt32(gradeManager[0]["MGId"]), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMemberAcceptedGrade), TnReId);
                                if (requestInActives.Count == 0)
                                    InsertInActive(requestInActivesManager, Convert.ToInt32(gradeManager[0]["MGId"]), TnReId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMemberAcceptedGrade) , TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TechnicianRequest));
                            }
                        }
                    }
                    gradeManager.DataTable.AcceptChanges();
                    #endregion
                    #region  InsertNewRow
                    if (insRowsGrade.Length > 0)
                    {
                        for (int i = 0; i < insRowsGrade.Length; i++)
                        {
                            DataRow drGrade = gradeManager.NewRow();
                            drGrade["TnReId"] = TnReId;
                            drGrade["GrdId"] = int.Parse(insRowsGrade[i]["GrdId"].ToString());
                            drGrade["ResId"] = insRowsGrade[i]["ResId"].ToString();
                            drGrade["InActive"] = 0;
                            drGrade["Status"] = insRowsGrade[i]["Status"].ToString();
                            drGrade["ModifiedDate"] = DateTime.Now;
                            drGrade["UserId"] = Utility.GetCurrentUser_UserId();
                            gradeManager.AddRow(drGrade);
                            gradeManager.Save();
                            gradeManager.DataTable.AcceptChanges();

                        }
                        gradeManager.Save();
                    }
                    #endregion

                }
                #endregion

                #region ObsArea
                dtObsArea = (DataTable)Session["ObsArea"];
                if (dtObsArea.GetChanges() != null)
                {
                    DataRow[] delRowsArea = dtObsArea.Select(null, null, DataViewRowState.Deleted);
                    DataRow[] insRowsArea = dtObsArea.Select(null, null, DataViewRowState.Added);

                    for (int i = 0; i < delRowsArea.Length; i++)
                    {
                        TnActAreasManager.FindByCode(int.Parse(delRowsArea[i]["Id", DataRowVersion.Original].ToString()));

                        if (TnActAreasManager.Count > 0)
                        {
                            if (Convert.ToInt32(TnActAreasManager[0]["TnReId"]) == TnReId)
                            {
                                TnActAreasManager[0].Delete();
                                TnActAreasManager.Save();
                                TnActAreasManager.DataTable.AcceptChanges();
                            }
                            else
                            {
                                requestInActives.FindByTableIdTableType(Convert.ToInt32(TnActAreasManager[0]["ActAreaId"]), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TechniciansActivityAreas), TnReId);
                                if (requestInActives.Count == 0)
                                    InsertInActive(requestInActivesManager, Convert.ToInt32(TnActAreasManager[0]["ActAreaId"]), TnReId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TechniciansActivityAreas), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TechnicianRequest));
                            }
                        }
                    }
                    TnActAreasManager.DataTable.AcceptChanges();

                    if (insRowsArea.Length > 0)
                    {
                        for (int i = 0; i < insRowsArea.Length; i++)
                        {
                            DataRow drGrdArea = TnActAreasManager.NewRow();
                            drGrdArea["TnReId"] = TnReId;
                            drGrdArea["MGId"] = DBNull.Value;
                            drGrdArea["CitId"] = insRowsArea[i]["CitId"].ToString();
                            drGrdArea["CreateDate"] = Utility.GetDateOfToday();
                            drGrdArea["UserId"] = Utility.GetCurrentUser_UserId();
                            drGrdArea["ModifiedDate"] = DateTime.Now;
                            TnActAreasManager.AddRow(drGrdArea);
                        }
                        TnActAreasManager.Save();
                    }
                }

                #endregion

                int UpdateState = -1;
                if (!(Convert.ToBoolean(Session["IsEdited_Technician"].ToString())))
                {
                    int TableType = (int)TSP.DataManager.TableCodes.TechnicianRequest;
                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TnReId, TableType, "Update", Utility.GetCurrentUser_UserId());

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
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";
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
        if (chAxImg == true)
        {
            try
            {
                string Soource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["OtpImage"].ToString());
                string Target = Server.MapPath("~/Image/OtherPerson/Person/") + Path.GetFileName(Session["OtpImage"].ToString());
                System.IO.File.Copy(Soource, Target, true);
            }
            catch (Exception)
            {
            }
        }

        if (Licence)
        {
            try
            {
                string Soource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["AttachCommit"].ToString());
                string Target = Server.MapPath("~/Image/OtherPerson/Lisense/") + Path.GetFileName(Session["AttachCommit"].ToString());
                System.IO.File.Copy(Soource, Target, true);
            }
            catch (Exception)
            {
            }
        }

    }
    #endregion

    #region Fill
    protected void FillForm(int OtpId)
    {
        TSP.DataManager.OtherPersonManager OthManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.AttachmentsManager AttachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();

        try
        {
            OthManager.FindByCode(OtpId);

            if (OthManager.Count == 1)
            {
                ObjectDataSourceOfficeMember.SelectParameters["PersonId"].DefaultValue = OtpId.ToString();
                switch (Convert.ToInt32(OthManager[0]["OtpType"]))
                {
                    case (int)TSP.DataManager.OtherPersonType.Kardan:
                        ObjectDataSourceOfficeMember.SelectParameters["OfmType"].DefaultValue = ((int)TSP.DataManager.OfficeMemberType.Kardan).ToString();
                        break;
                    case (int)TSP.DataManager.OtherPersonType.Memar:
                        ObjectDataSourceOfficeMember.SelectParameters["OfmType"].DefaultValue = ((int)TSP.DataManager.OfficeMemberType.Memar).ToString();
                        break;
                    case (int)TSP.DataManager.OtherPersonType.OtherPerson:
                        ObjectDataSourceOfficeMember.SelectParameters["OfmType"].DefaultValue = ((int)TSP.DataManager.OfficeMemberType.Otherperson).ToString();
                        break;
                    default:
                        ObjectDataSourceOfficeMember.SelectParameters["PersonId"].DefaultValue = "-2";
                        ObjectDataSourceOfficeMember.SelectParameters["OfmType"].DefaultValue = "-2";
                        break;
                }
                ComboType.DataBind();
                ComboType.SelectedIndex = ComboType.Items.IndexOfValue(OthManager[0]["OtpType"].ToString());
                txtOtpCode.Text = OthManager[0]["OtpCode"].ToString();
                txtFirstName.Text = OthManager[0]["FirstName"].ToString();
                txtLastName.Text = OthManager[0]["LastName"].ToString();
                txtBankAccNo.Text = OthManager[0]["BankAccNo"].ToString();
                cmbBank.SelectedIndex = cmbBank.Items.IndexOfValue(OthManager[0]["Bank"].ToString());

                txtFatherName.Text = OthManager[0]["FatherName"].ToString();
                txtIdNo.Text = OthManager[0]["IdNo"].ToString();
                txtSSN.Text = OthManager[0]["SSN"].ToString();
                txtBirthPlace.Text = OthManager[0]["BirthPlace"].ToString();
                txtBirthDate.Text = OthManager[0]["BirthDate"].ToString();
                txtFileNo.Text = OthManager[0]["FileNo"].ToString();
                txtFileNoDate.Text = OthManager[0]["FileNoDate"].ToString();
                txtDesc.Text = OthManager[0]["Description"].ToString();
                if (OthManager[0]["Tel"].ToString() != "")
                {
                    if (OthManager[0]["Tel"].ToString().IndexOf("-") > 0)
                    {
                        txtTel_pre.Text = OthManager[0]["Tel"].ToString().Substring(0, OthManager[0]["Tel"].ToString().IndexOf("-"));
                        txtTel.Text = OthManager[0]["Tel"].ToString().Substring(OthManager[0]["Tel"].ToString().IndexOf("-") + 1, OthManager[0]["Tel"].ToString().Length - OthManager[0]["Tel"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtTel.Text = OthManager[0]["Tel"].ToString();
                    }
                }
                txtMobile.Text = OthManager[0]["MobileNo"].ToString();
                txtAddress.Text = OthManager[0]["Address"].ToString();
                if (!Utility.IsDBNullOrNullValue(OthManager[0]["ImgUrl"]))
                {
                    imgMember.ImageUrl = OthManager[0]["ImgUrl"].ToString();
                    Session["OtpImage"] = OthManager[0]["ImgUrl"].ToString();
                    HDFlpCommit["PesonImg"] = 1;
                }

                if (!Utility.IsDBNullOrNullValue(OthManager[0]["MjId"]))
                {
                    ComboMjId.DataBind();
                    ComboMjId.SelectedIndex = ComboMjId.Items.IndexOfValue(OthManager[0]["MjId"].ToString());

                }
                if (!Utility.IsDBNullOrNullValue(OthManager[0]["AgentId"]))
                {
                    CmbAgent.DataBind();
                    CmbAgent.SelectedIndex = CmbAgent.Items.IndexOfValue(OthManager[0]["AgentId"].ToString());
                    ObjectDataSourceCity.SelectParameters["AgentId"].DefaultValue = OthManager[0]["AgentId"].ToString();
                }
                if (!Utility.IsDBNullOrNullValue(OthManager[0]["CitId"]))
                {
                    DataTable dtRegionOfCity = new DataTable();
                    CmbCity.DataBind();
                    CmbCity.SelectedIndex = CmbCity.Items.IndexOfValue(OthManager[0]["CitId"].ToString());
                    int CitId = int.Parse(OthManager[0]["CitId"].ToString());
                    TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
                    txtRegionOfCity.Text = "";

                    CityManager.FindByCode(CitId);
                    if (CityManager.Count == 1 && !Utility.IsDBNullOrNullValue(CityManager[0]["ReCitId"]))
                    {
                        txtRegionOfCity.Text = CityManager[0]["ReCitName"].ToString();
                        dtRegionOfCity = CityManager.SelectByReCitId(Convert.ToInt32(CityManager[0]["ReCitId"]));
                    }
                    cmbRegionOfCity.DataSource = dtRegionOfCity;
                    cmbRegionOfCity.DataBind();
                }

                txtMjName.Text = OthManager[0]["MjName"].ToString();

                HpCommit.ClientVisible = true;

                if (!Utility.IsDBNullOrNullValue(OthManager[0]["LicenceImgUrl"]))
                {
                    HpCommit.NavigateUrl = OthManager[0]["LicenceImgUrl"].ToString();
                    HDFlpCommit["name"] = 1;
                    Session["AttachCommit"] = OthManager[0]["LicenceImgUrl"].ToString();
                }
                #region Accepted Grade
             
                TechnicianRequestManager.FindByOtpId(OtpId);
                int LastTnReId = Convert.ToInt32(TechnicianRequestManager[TechnicianRequestManager.Count - 1]["TnReId"]);
                TSP.DataManager.DocOffMemberAcceptedGradeManager gradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
                DataTable dtgradeManager = gradeManager.FindByTnReIdForTechnicianRequest(LastTnReId, OtpId, 1);
                dtObsGrade = (DataTable)Session["gradeManager"];
                for (int i = 0; i < dtgradeManager.Rows.Count; i++)
                {
                    DataRow dr = dtObsGrade.NewRow();
                    dr["Id"] = dtgradeManager.Rows[i]["MGId"];
                    dr["GrdId"] = dtgradeManager.Rows[i]["GrdId"];
                    dr["ResId"] = dtgradeManager.Rows[i]["ResId"];
                    dr["TnReId"] = dtgradeManager.Rows[i]["TnReId"];
                    dr["InActive"] = dtgradeManager.Rows[i]["InActive"];
                    dr["Status"] = dtgradeManager.Rows[i]["Status"];
                    dr["ResName"] = dtgradeManager.Rows[i]["ResName"];
                    dr["GrdName"] = dtgradeManager.Rows[i]["GrdName"];
                    dr["MjName"] = dtgradeManager.Rows[i]["MjName"];
                    dtObsGrade.Rows.Add(dr);
                }

                dtObsGrade.AcceptChanges();
                GridViewResponseblity.DataSource = dtObsGrade;
                GridViewResponseblity.DataBind();
                #endregion

                #region Area
                TSP.DataManager.TechniciansActivityAreasManager AreasManager = new TSP.DataManager.TechniciansActivityAreasManager();
                DataTable dtArea = AreasManager.FindByOtpIdAndTnReId(OtpId, LastTnReId);
                dtObsArea = (DataTable)Session["ObsArea"];
                for (int i = 0; i < dtArea.Rows.Count; i++)
                {
                    DataRow drArea = dtObsArea.NewRow();
                    drArea["Id"] = dtArea.Rows[i]["ActAreaId"];
                    drArea["CitName"] = dtArea.Rows[i]["CitName"].ToString();
                    drArea["CitId"] = dtArea.Rows[i]["CitId"].ToString();
                    drArea["CitCode"] = dtArea.Rows[i]["CitCode"].ToString();
                    drArea["InActiveName"] = dtArea.Rows[i]["InActiveName"].ToString();
                    drArea["TnReId"] = TnReId;
                    dtObsArea.Rows.Add(drArea);
                }
                dtObsArea.AcceptChanges();
                GridViewCity.DataSource = dtObsArea;
                GridViewCity.DataBind();
                #endregion
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";

        }

    }

    protected void FillFormByReguest(int TnReId)
    {
        TSP.DataManager.OtherPersonManager OthManager = new TSP.DataManager.OtherPersonManager();

        TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();

        try
        {

            TechnicianRequestManager.FindByCode(TnReId);
            if (TechnicianRequestManager.Count != 1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                return;
            }
            #region Fill OtherPerson
            int OtpId = int.Parse(TechnicianRequestManager[0]["OtpId"].ToString());
            ObjectDataSourceOfficeMember.SelectParameters["PersonId"].DefaultValue = OtpId.ToString();
            switch (Convert.ToInt32(TechnicianRequestManager[0]["OtpType"]))
            {
                case (int)TSP.DataManager.OtherPersonType.Kardan:
                    ObjectDataSourceOfficeMember.SelectParameters["OfmType"].DefaultValue = ((int)TSP.DataManager.OfficeMemberType.Kardan).ToString();
                    break;
                case (int)TSP.DataManager.OtherPersonType.Memar:
                    ObjectDataSourceOfficeMember.SelectParameters["OfmType"].DefaultValue = ((int)TSP.DataManager.OfficeMemberType.Memar).ToString();
                    break;
                case (int)TSP.DataManager.OtherPersonType.OtherPerson:
                    ObjectDataSourceOfficeMember.SelectParameters["OfmType"].DefaultValue = ((int)TSP.DataManager.OfficeMemberType.Otherperson).ToString();
                    break;
                default:
                    ObjectDataSourceOfficeMember.SelectParameters["PersonId"].DefaultValue = "-2";
                    ObjectDataSourceOfficeMember.SelectParameters["OfmType"].DefaultValue = "-2";
                    break;
            }
            OthManager.FindByCode(OtpId);
            ComboType.DataBind();
            ComboType.SelectedIndex = ComboType.Items.IndexOfValue(TechnicianRequestManager[0]["OtpType"].ToString());

            txtOtpCode.Text = TechnicianRequestManager[0]["OtpCode"].ToString();
            txtFirstName.Text = TechnicianRequestManager[0]["FirstName"].ToString();
            txtLastName.Text = TechnicianRequestManager[0]["LastName"].ToString();
            txtBankAccNo.Text = TechnicianRequestManager[0]["BankAccNo"].ToString();
            cmbBank.SelectedIndex = cmbBank.Items.IndexOfValue(TechnicianRequestManager[0]["Bank"].ToString());
            txtFatherName.Text = TechnicianRequestManager[0]["FatherName"].ToString();
            txtIdNo.Text = TechnicianRequestManager[0]["IdNo"].ToString();
            txtSSN.Text = TechnicianRequestManager[0]["SSN"].ToString();
            txtBirthPlace.Text = TechnicianRequestManager[0]["BirthPlace"].ToString();
            txtBirthDate.Text = TechnicianRequestManager[0]["BirthDate"].ToString();
            txtFileNo.Text = TechnicianRequestManager[0]["FileNo"].ToString();
            txtFileNoDate.Text = TechnicianRequestManager[0]["FileDate"].ToString();
            txtDesc.Text = OthManager[0]["Description"].ToString();
            if (TechnicianRequestManager[0]["Tel"].ToString() != "")
            {
                if (TechnicianRequestManager[0]["Tel"].ToString().IndexOf("-") > 0)
                {
                    txtTel_pre.Text = TechnicianRequestManager[0]["Tel"].ToString().Substring(0, TechnicianRequestManager[0]["Tel"].ToString().IndexOf("-"));
                    txtTel.Text = TechnicianRequestManager[0]["Tel"].ToString().Substring(TechnicianRequestManager[0]["Tel"].ToString().IndexOf("-") + 1, TechnicianRequestManager[0]["Tel"].ToString().Length - TechnicianRequestManager[0]["Tel"].ToString().IndexOf("-") - 1);
                }
                else
                {
                    txtTel.Text = TechnicianRequestManager[0]["Tel"].ToString();
                }
            }
            txtMobile.Text = TechnicianRequestManager[0]["MobileNo"].ToString();
            txtAddress.Text = TechnicianRequestManager[0]["Address"].ToString();
            if (!Utility.IsDBNullOrNullValue(TechnicianRequestManager[0]["ImageUrl"]))
            {
                imgMember.ImageUrl = TechnicianRequestManager[0]["ImageUrl"].ToString();
                Session["OtpImage"] = TechnicianRequestManager[0]["ImageUrl"].ToString();
                HDFlpCommit["PesonImg"] = 1;
            }

            if (!Utility.IsDBNullOrNullValue(TechnicianRequestManager[0]["MjId"]))
            {
                ComboMjId.DataBind();
                ComboMjId.SelectedIndex = ComboMjId.Items.IndexOfValue(TechnicianRequestManager[0]["MjId"].ToString());

            }
            if (!Utility.IsDBNullOrNullValue(TechnicianRequestManager[0]["AgentId"]))
            {
                CmbAgent.DataBind();
                CmbAgent.SelectedIndex = CmbAgent.Items.IndexOfValue(TechnicianRequestManager[0]["AgentId"].ToString());
                ObjectDataSourceCity.SelectParameters["AgentId"].DefaultValue = OthManager[0]["AgentId"].ToString();

            }
            if (!Utility.IsDBNullOrNullValue(TechnicianRequestManager[0]["CitId"]))
            {
                DataTable dtRegionOfCity = new DataTable();
                CmbCity.DataBind();
                CmbCity.SelectedIndex = CmbCity.Items.IndexOfValue(TechnicianRequestManager[0]["CitId"].ToString());
                int CitId = int.Parse(TechnicianRequestManager[0]["CitId"].ToString());
                TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
                txtRegionOfCity.Text = "";

                CityManager.FindByCode(CitId);
                if (CityManager.Count == 1 && !Utility.IsDBNullOrNullValue(CityManager[0]["ReCitId"]))
                {
                    txtRegionOfCity.Text = CityManager[0]["ReCitName"].ToString();
                    dtRegionOfCity = CityManager.SelectByReCitId(Convert.ToInt32(CityManager[0]["ReCitId"]));
                }
                cmbRegionOfCity.DataSource = dtRegionOfCity;
                cmbRegionOfCity.DataBind();
            }


            txtMjName.Text = TechnicianRequestManager[0]["MjName"].ToString();

            HpCommit.ClientVisible = true;
            if (!Utility.IsDBNullOrNullValue(TechnicianRequestManager[0]["LicenceImgUrl"]))
            {
                HpCommit.NavigateUrl = TechnicianRequestManager[0]["LicenceImgUrl"].ToString();
                HDFlpCommit["name"] = 1;
                Session["AttachCommit"] = TechnicianRequestManager[0]["LicenceImgUrl"].ToString();
            }
            #endregion
            #region Accepted Grade

            TSP.DataManager.DocOffMemberAcceptedGradeManager gradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
            DataTable dtgradeManager = gradeManager.FindByTnReIdForTechnicianRequest(TnReId, OtpId, 1);
            dtObsGrade = (DataTable)Session["gradeManager"];
            for (int i = 0; i < dtgradeManager.Rows.Count; i++)
            {
                DataRow dr = dtObsGrade.NewRow();
                dr["Id"] = dtgradeManager.Rows[i]["MGId"];
                dr["GrdId"] = dtgradeManager.Rows[i]["GrdId"];
                dr["ResId"] = dtgradeManager.Rows[i]["ResId"];
                dr["TnReId"] = dtgradeManager.Rows[i]["TnReId"];
                dr["InActive"] = dtgradeManager.Rows[i]["InActive"];
                dr["Status"] = dtgradeManager.Rows[i]["Status"];
                dr["ResName"] = dtgradeManager.Rows[i]["ResName"];
                dr["GrdName"] = dtgradeManager.Rows[i]["GrdName"];
                dr["MjName"] = dtgradeManager.Rows[i]["MjName"];
                dtObsGrade.Rows.Add(dr);
            }
            dtObsGrade.AcceptChanges();
            GridViewResponseblity.DataSource = dtObsGrade;
            GridViewResponseblity.DataBind();

            #endregion

            #region Area
            TSP.DataManager.TechniciansActivityAreasManager AreasManager = new TSP.DataManager.TechniciansActivityAreasManager();
            DataTable dtArea = AreasManager.FindByOtpIdAndTnReId(OtpId, TnReId);
            dtObsArea = (DataTable)Session["ObsArea"];
            for (int i = 0; i < dtArea.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtArea.Rows[i]["InActive"]) == false)
                {
                    DataRow drArea = dtObsArea.NewRow();
                    drArea["Id"] = dtArea.Rows[i]["ActAreaId"];
                    drArea["CitName"] = dtArea.Rows[i]["CitName"].ToString();
                    drArea["CitId"] = dtArea.Rows[i]["CitId"].ToString();
                    drArea["CitCode"] = dtArea.Rows[i]["CitCode"].ToString();
                    drArea["InActiveName"] = dtArea.Rows[i]["InActiveName"].ToString();
                    drArea["TnReId"] = TnReId;
                    dtObsArea.Rows.Add(drArea);
                }
            }
            dtObsArea.AcceptChanges();
            GridViewCity.DataSource = dtObsArea;
            GridViewCity.DataBind();
            #endregion


        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }

    }

    private void RegionOfCity()
    {
        DataTable dtRegionOfCity = new DataTable();
        TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
        if (!Utility.IsDBNullOrNullValue(CmbCity.SelectedItem.Value))
        {
            int CitId = int.Parse(CmbCity.SelectedItem.Value.ToString());
            txtRegionOfCity.Text = "";

            CityManager.FindByCode(CitId);
            if (CityManager.Count == 1 && !Utility.IsDBNullOrNullValue(CityManager[0]["ReCitId"]))
            {


                txtRegionOfCity.Text = CityManager[0]["ReCitName"].ToString();
                dtRegionOfCity = CityManager.SelectByReCitId(Convert.ToInt32(CityManager[0]["ReCitId"]));

            }
        }

        cmbRegionOfCity.DataSource = dtRegionOfCity;
        cmbRegionOfCity.DataBind();

    }
    #endregion

    #region Save Image
    protected string SaveImageLicense(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);


                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/TechnicalServices/Implementer/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["AttachCommit"] = "~/Image/Temp/" + ret;

        }
        return ret;
    }

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            // Session["ExPlaceUpload"] = tempFileName;
            Session["OtpImage"] = "~/Image/Temp/" + ret;

        }
        return ret;
    }
    #endregion

    private int FindNmcId()
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, LoginManager);
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

    #region WF Permissions
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTechnicianRequestInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.TechnicianRequest;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());

                    if (CurrentTaskCode == TaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            int FirstUserId = int.Parse(dtWorkFlowState.Rows[0]["UserId"].ToString());
                            if (FirstTaskCode == TaskCode)
                            {
                                if (FirstUserId == Utility.GetCurrentUser_UserId())
                                {
                                    int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                                    if (Permission > 0)
                                        return true;
                                    else
                                        return false;
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

    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTechnicianRequestInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            string PageMode = Utility.DecryptQS(PgMode.Value);
            CheckWorkFlowPermissionForSave(PageMode);
            if (PageMode != "New")
                CheckWorkFlowPermissionForEdit(PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveTechnicianRequestInfo;
        int Permission = -1;
        int TableType = (int)TSP.DataManager.TableCodes.TechnicianRequest;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
        {
            BtnNew.Enabled = true;
            BtnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSave.Enabled = true;
                    btnSave1.Enabled = true;

                    break;
                case "View":
                    btnSave.Enabled = false;
                    btnSave1.Enabled = false;
                    break;
            }
        }
        else
        {
            BtnNew.Enabled = false;
            BtnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave1.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جریان کار جهت ثبت اطلاعات کاردان را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = btnEdit.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string TenReId = Utility.DecryptQS(TnReId.Value);
        int TableType = (int)TSP.DataManager.TableCodes.TechnicianRequest;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTechnicianRequestInfo;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, int.Parse(TenReId), TaskCode, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0)
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSave.Enabled = true;
                    btnSave1.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;
                    btnSave.Enabled = true;
                    btnSave1.Enabled = true;
                    break;
            }
        }
        else
        {
            switch (PageMode)
            {
                case "View":
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave1.Enabled = false;
                    break;

                case "Change":
                    btnSave.Enabled = true;
                    btnSave1.Enabled = true;
                    break;
            }
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }
    #endregion

    private void ResetSession()
    {

        Session["IsEdited_Technician"] = false;
        Session["gradeManager"] = null;
        Session["ObsArea"] = null;
        Session["OtpImage"] = null;
        Session["AttachCommit"] = null;
    }

    //private void ShowCityPanel(TSP.DataManager.DocOffMemberAcceptedGradeManager gradeManager)
    //{

    //    RoundPanelCity.ClientVisible = false;
    //    if (int.Parse(ComboType.Value.ToString()) == 1)
    //    {
    //        for (int i = 0; i < gradeManager.Count; i++)
    //        {
    //            DataRow drv = gradeManager[i];
    //            if (Convert.ToInt32(drv["ResId"]) == (int)TSP.DataManager.DocumentResponsibilityType.Observation && Convert.ToInt32(gradeManager[i]["InActive"]) == 0)
    //            {
    //                RoundPanelCity.ClientVisible = true;
    //            }
    //        }
    //    }
    //}

    protected void Disable()
    {
        txtOtpCode.Enabled = false;
        txtAddress.Enabled = false;
        txtBirthDate.Enabled = false;
        txtBirthPlace.Enabled = false;
        txtDesc.Enabled = false;
        txtFatherName.Enabled = false;
        txtFileNo.Enabled = false;
        txtFileNoDate.Enabled = false;
        txtFirstName.Enabled = false;
        txtIdNo.Enabled = false;
        txtLastName.Enabled = false;
        txtBankAccNo.Enabled = false;
        cmbBank.Enabled = false;
        txtMjName.Enabled = false;
        txtMobile.Enabled = false;
        txtSSN.Enabled = false;
        txtTel.Enabled = false;
        txtTel_pre.Enabled = false;
        ComboGrade.Enabled = false;
        ComboMjId.Enabled = false;
        ComboResp.Enabled = false;
        ComboType.Enabled = false;
        tblgr.Visible = false;
        tblCity.Visible = false;
        GridViewResponseblity.Columns["ColDeleteGrade"].Visible = false;
        GridViewCity.Columns["ClmDelete"].Visible = false;
        flp_Image.ClientVisible = false;
        flpCommit.ClientVisible = false;
        CmbAgent.Enabled = false;
        CmbCity.Enabled = false;
        txtRegionOfCity.Enabled = false;

        cmbRegionOfCity.Enabled = false;
    }

    protected void Enable()
    {
        txtOtpCode.Enabled = true;
        txtAddress.Enabled = true;
        txtBirthDate.Enabled = true;
        txtBirthPlace.Enabled = true;
        txtDesc.Enabled = true;
        txtFatherName.Enabled = true;
        txtFileNo.Enabled = true;
        txtFileNoDate.Enabled = true;
        txtFirstName.Enabled = true;
        txtIdNo.Enabled = true;
        txtLastName.Enabled = true;
        txtBankAccNo.Enabled = true;
        cmbBank.Enabled = true;
        txtMjName.Enabled = true;
        txtMobile.Enabled = true;
        txtSSN.Enabled = true;
        txtTel.Enabled = true;
        txtTel_pre.Enabled = true;
        ComboGrade.Enabled = true;
        ComboMjId.Enabled = true;
        ComboResp.Enabled = true;
        ComboType.Enabled = true;
        tblgr.Visible = true;
        tblCity.Visible = true;
        GridViewResponseblity.Columns["ColDeleteGrade"].Visible = true;
        GridViewCity.Columns["ClmDelete"].Visible = true;
        flp_Image.ClientVisible = true;
        flpCommit.ClientVisible = true;
        CmbAgent.Enabled = true;
        CmbCity.Enabled = true;
        txtRegionOfCity.Enabled = true;

        cmbRegionOfCity.Enabled = true;
    }

    protected void ClearForm()
    {
        txtOtpCode.Text = "";
        txtAddress.Text = "";
        txtBirthDate.Text = "";
        txtBirthPlace.Text = "";
        txtDesc.Text = "";
        txtFatherName.Text = "";
        txtFileNo.Text = "";
        txtFileNoDate.Text = "";
        txtFirstName.Text = "";
        txtIdNo.Text = "";
        txtLastName.Text = "";
        txtBankAccNo.Text = "";
        cmbBank.SelectedIndex = 0;
        txtMjName.Text = "";
        txtMobile.Text = "";
        txtSSN.Text = "";
        txtTel.Text = "";
        txtTel_pre.Text = "";
        txtRegionOfCity.Text = "";
        ComboGrade.DataBind();
        ComboMjId.DataBind();
        ComboResp.DataBind();
        ComboType.DataBind();
        ComboGrade.SelectedIndex = -1;
        ComboMjId.SelectedIndex = -1;
        ComboResp.SelectedIndex = -1;
        ComboType.SelectedIndex = 0;


        HDFlpCommit["name"] = 0;
        HDFlpCommit["PesonImg"] = 0;
        HpCommit.ClientVisible = false;
        CmbAgent.SelectedIndex = -1;
        CmbCity.SelectedIndex = -1;
        imgMember.ImageUrl = "";

        Session["AttachCommit"] = null;


        dtObsGrade = (DataTable)Session["gradeManager"];
        dtObsGrade.Rows.Clear();
        GridViewResponseblity.DataSource = dtObsGrade;
        GridViewResponseblity.DataBind();

        dtObsArea = (DataTable)Session["ObsArea"];
        dtObsArea.Rows.Clear();
        GridViewCity.DataSource = dtObsArea;
        GridViewCity.DataBind();
    }


    #endregion
}
