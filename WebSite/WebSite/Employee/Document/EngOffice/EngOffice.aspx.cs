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

public partial class Employee_Document_EngOffice_EngOffice : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;


    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        #region PageRefresh
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
        #endregion

        if (!IsPostBack)
        {
            SetHelpAddress();
            Session["EngOfficeId"] = null;

            Session["SendBackDataTable_OfConf"] = "";

            GridViewEngOffice.JSProperties["cpSelectedIndex"] = 0;
            GridViewEngOffice.JSProperties["cpIsReturn"] = 0;

            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.EngOfficeConfirming).ToString();
            CmbTask.DataBind();
            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
            CmbTask.SelectedIndex = 0;
            TSP.DataManager.Permission per = TSP.DataManager.EngOfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = btnDelete2.Enabled = per.CanDelete;
            GridViewEngOffice.Visible = per.CanView;
            btnTracing.Enabled = btnTracing2.Enabled = per.CanView;
            btnPrint.Enabled = btnprint2.Enabled = per.CanView;
            btnExportExcel.Enabled = btnExportExcel2.Enabled = per.CanView;

            btnChange.Enabled = btnChange2.Enabled =
                btnReduplicate.Enabled = btnReduplicate2.Enabled =
                btnRevival.Enabled = btnRevival2.Enabled =
                btnInvalid.Enabled = btnInvalid2.Enabled =
                btnChangeBaseInfo.Enabled = btnChangeBaseInfo2.Enabled =
                btnActivate.Enabled = btnActivate2.Enabled = CheckWorkFlowPermissionForChangeReq();

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnTracing"] = btnTracing.Enabled;
            this.ViewState["btnPrint"] = btnPrint.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
            this.ViewState["BtnRequset"] = btnChange.Enabled;

            Session["DataTable"] = GridViewEngOffice.Columns;
            Session["DataSource"] = OdbEngOffice;
            Session["Title"] = "مدیریت دفاتر";

        }

        if (this.ViewState["btnPrint"] != null)
            this.btnPrint.Enabled = this.btnprint2.Enabled = (bool)this.ViewState["btnPrint"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnTracing"] != null)
            this.btnTracing.Enabled = this.btnTracing2.Enabled = (bool)this.ViewState["BtnTracing"];
        if (this.ViewState["BtnRequset"] != null)
            btnChange.Enabled = btnChange2.Enabled =
                btnReduplicate.Enabled = btnReduplicate2.Enabled =
                btnRevival.Enabled = btnRevival2.Enabled =
                btnInvalid.Enabled = btnInvalid2.Enabled =
                btnChangeBaseInfo.Enabled = btnChangeBaseInfo2.Enabled =
                btnActivate.Enabled = btnActivate2.Enabled = (bool)this.ViewState["BtnRequset"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        //    Search();
        SetPageFilter();
        SetGridRowIndex();


        string script = @" function CheckSearch() { var txtEndDateFrom = document.getElementById('" + txtEndDateFrom.ClientID + "').value;";
        script += "var txtEndDateTo = document.getElementById('" + txtEndDateTo.ClientID + "').value;";
        script += "var txtCreateDateFrom = document.getElementById('" + txtCreateDateFrom.ClientID + "').value;";
        script += "var txtCreateDateTo = document.getElementById('" + txtCreateDateTo.ClientID + "').value;";
        script += "var txtWFFrom = document.getElementById('" + txtWFFrom.ClientID + "').value;";
        script += "var txtWFTo = document.getElementById('" + txtWFTo.ClientID + "').value;";

        script += "if (txtEndAuditor.GetText()=='' && txtWFFrom=='' && txtWFTo=='' &&  txtEndDateFrom=='' && txtEndDateTo=='' && txtCreateDateFrom=='' && txtCreateDateTo=='' && txtFollowCode.GetText() == '' && txtManagerfamily.GetText() == '' && txtManagerName.GetText() == '' && txtEngOffId.GetText() == '' && txtEngOffName.GetText() == '' && CmbReqType.GetSelectedIndex() == 0 &&  CmbTask.GetSelectedIndex() == 0 ) return 0; else return 1;  }";
        script += @"function ClearSearch() {
        txtEndAuditor.SetText('');
        txtFollowCode.SetText('');
        txtManagerfamily.SetText('');        
        txtManagerName.SetText('');
        txtEngOffId.SetText('');
        txtEngOffName.SetText('');
        CmbReqType.SetSelectedIndex(0);
        CmbTask.SetSelectedIndex(0);
        document.getElementById('" + txtWFFrom.ClientID + @"').value='';
        document.getElementById('" + txtWFTo.ClientID + @"').value='';
        document.getElementById('" + txtEndDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtEndDateTo.ClientID + @"').value='';
        document.getElementById('" + txtCreateDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtCreateDateTo.ClientID + @"').value='';}";
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script, true);

    }

    #region btn Click
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporterEngOffice.FileName = "EngOfficeDocument";
        GridViewExporterEngOffice.WriteXlsToResponse(true);
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        string GridFilterString = GridViewEngOffice.FilterExpression;
        string SearchFilterString = GenerateFilterString();

        if (IsCallback)
            ASPxWebControl.RedirectOnCallback("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS("-1") + "&PageMode=" + Utility.EncryptQS("New") + "&EOfId=" + Utility.EncryptQS("-1") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
        else
            Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS("-1") + "&PageMode=" + Utility.EncryptQS("New") + "&EOfId=" + Utility.EncryptQS("-1") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            string GridFilterString = GridViewEngOffice.FilterExpression;
            string SearchFilterString = GenerateFilterString();

            TSP.DataManager.EngOfficeManager EngManager = new TSP.DataManager.EngOfficeManager();

            int EOfId = -1;
            int EngOfId = -1;
            if (GridViewEngOffice.FocusedRowIndex > -1)
            {
                DataRow row = GridViewEngOffice.GetDataRow(GridViewEngOffice.FocusedRowIndex);
                EngOfId = (int)row["EngOfId"];
            }
            if (EngOfId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";
                return;
            }

            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewEngOffice.FindDetailRowTemplateControl(GridViewEngOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest == null)
            {
                ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            if (GridRequest.VisibleRowCount < 0)
            {
                ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            int index0 = GridRequest.FocusedRowIndex;
            if (index0 < 0)
            {
                ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            EOfId = int.Parse(GridRequest.GetDataRow(index0)["EOfId"].ToString());

            TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
            FileManager.FindByCode(EOfId);

            if (FileManager[0]["IsConfirm"].ToString() != "0")
            {
                ShowMessage("امکان ویرایش اطلاعات برای درخواست پاسخ داده شده وجود ندارد");
                return;

            }
            if (!Convert.ToBoolean(FileManager[0]["Requester"]))
            {
                ShowMessage("امکان ویرایش اطلاعات برای شما وجود ندارد");
                return;
            }
            if (CheckPermitionForEdit(EOfId))
            {
                if (IsCallback)
                    ASPxWebControl.RedirectOnCallback("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                else
                    Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
            }
            else
            {
                ShowMessage("امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازخوانی اطلاعات رخ داده است");
        }
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        int EngOfId = -1;

        if (GridViewEngOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEngOffice.GetDataRow(GridViewEngOffice.FocusedRowIndex);
            EngOfId = (int)row["EngOfId"];
        }
        if (EngOfId == -1)
        {
            ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        //TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();
        //EngOffManager.FindByCode(EngOfId);
        //if (EngOffManager.Count != 1)
        //{
        //    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
        //    return;
        //}
        //if (Convert.ToInt32(EngOffManager[0]["IsConfirm"]) != (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed 
        //    && Convert.ToInt32(EngOffManager[0]["IsConfirm"]) != (int)TSP.DataManager.EngOfficeConfirmationType.ConditionalApprove)
        //{
        //    ShowMessage("امکان ویرایش وجود ندارد.عضویت دفتر انتخابی تایید شده نمی باشد.");
        //    return;
        //}
        FileManager.FindByEngOffCode(EngOfId, 0, -1);
        if (FileManager.Count > 0)
        {
            ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
            return;
        }

        FileManager.FindByEngOffCode(EngOfId, -1, -1);//return last EOfId
        if (FileManager.Count > 0)
        {
            if (Convert.ToInt32(FileManager[0]["Isconfirm"]) == 1 && Convert.ToInt32(FileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.Invalid)//ابطال
            {
                ShowMessage("امکان درخواست تغییرات وجود ندارد.پروانه دفتر مورد نظر باطل شده است");
                return;
            }
            Change(EngOfId, Convert.ToInt32(FileManager[0]["EOfId"]));
        }
    }

    protected void btnChangeBaseInfo_Click(object sender, EventArgs e)
    {
        int EngOfId = -1;

        if (GridViewEngOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEngOffice.GetDataRow(GridViewEngOffice.FocusedRowIndex);
            EngOfId = (int)row["EngOfId"];
        }
        if (EngOfId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        //TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();
        //EngOffManager.FindByCode(EngOfId);
        //if (EngOffManager.Count != 1)
        //{
        //    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
        //    return;
        //}
        //if (EngOffManager[0]["IsConfirm"].ToString() != "1")
        //{
        //    ShowMessage("امکان ویرایش وجود ندارد.عضویت دفتر انتخابی تایید شده نمی باشد.");
        //    return;
        //}
        FileManager.FindByEngOffCode(EngOfId, 0, -1);
        if (FileManager.Count > 0)
        {
            ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
            return;
        }
        FileManager.FindByEngOffCode(EngOfId, -1, -1);//return last EOfId
        if (FileManager.Count > 0)
        {
            if (Convert.ToInt32(FileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.Invalid)//ابطال
            {
                ShowMessage("امکان درخواست تغییرات وجود ندارد.پروانه دفتر مورد نظر باطل شده است");
                return;
            }
            ChangeBaseInfo(EngOfId, Convert.ToInt32(FileManager[0]["EOfId"]));
        }
    }

    protected void btnRevival_Click(object sender, EventArgs e)
    {
        int EngOfId = -1;

        if (GridViewEngOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEngOffice.GetDataRow(GridViewEngOffice.FocusedRowIndex);
            EngOfId = (int)row["EngOfId"];
        }
        if (EngOfId == -1)
        {
            ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        //TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();
        //EngOffManager.FindByCode(EngOfId);
        //if (EngOffManager.Count <= 0)
        //{
        //    ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
        //    return;
        //}
        //if (Convert.ToInt32(EngOffManager[0]["IsConfirm"]) != 1)
        //{
        //    ShowMessage("امکان ویرایش وجود ندارد.عضویت دفتر انتخابی تایید شده نمی باشد.");
        //    return;
        //}
        FileManager.FindByEngOffCode(EngOfId, 0, -1);
        if (FileManager.Count > 0)
        {
            ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
            return;
        }

        FileManager.FindByEngOffCode(EngOfId, 1, -1);//return last EOfId
        if (FileManager.Count > 0)
        {
            if (Convert.ToInt32(FileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.Invalid)//ابطال
            {
                ShowMessage("امکان درخواست تمدید وجود ندارد.پروانه دفتر مورد باطل شده است");
                return;
            }
            Revival(EngOfId, int.Parse(FileManager[0]["EOfId"].ToString()));

        }



    }

    protected void btnInvalid_Click(object sender, EventArgs e)
    {
        int EngOfId = -1;

        if (GridViewEngOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEngOffice.GetDataRow(GridViewEngOffice.FocusedRowIndex);
            EngOfId = (int)row["EngOfId"];
        }
        if (EngOfId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک دفتر را انتخاب نمائید";
            return;
        }
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();
        EngOffManager.FindByCode(EngOfId);
        if (EngOffManager.Count < 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            return;
        }

        if (Convert.ToInt32(EngOffManager[0]["IsConfirm"]) == (int)TSP.DataManager.EngOfficeConfirmationType.Cancel)
        {
            ShowMessage("امکان ثبت درخواست ابطال وجود ندارد.عضویت دفتر باطل شده است.");
            return;

        }
        FileManager.FindByEngOffCode(EngOfId, 0, -1);
        if (FileManager.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
            return;
        }

        FileManager.FindByEngOffCode(EngOfId, -1, -1);//return last EOfId
        if (FileManager.Count > 0)
        {
            InValid(EngOfId, int.Parse(FileManager[0]["EOfId"].ToString()));
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int EOfId = -1;
        int EngOfId = -1;

        if (GridViewEngOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEngOffice.GetDataRow(GridViewEngOffice.FocusedRowIndex);
            EngOfId = (int)row["EngOfId"];
        }
        if (EngOfId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "رکوردی انتخاب نشده است";
            return;
        }
        try
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewEngOffice.FindDetailRowTemplateControl(GridViewEngOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        EOfId = int.Parse(GridRequest.GetDataRow(index0)["EOfId"].ToString());

                        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
                        FileManager.FindByCode(EOfId);
                        if (FileManager.Count > 0)
                        {                           
                            if (FileManager[0]["IsConfirm"].ToString() != "0")
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان حذف برای درخواست پاسخ داده شده وجود ندارد";
                                return;
                            }
                            if (CheckPermitionForDelete(EOfId))
                                Delete(EngOfId, EOfId);
                            else
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "امکان حذف درخواست در این مرحله از جریان کار برای شما وجود ندارد";
                            }
                        }
                        else
                        {

                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                    }

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                }


            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";

            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
            if (Utility.ShowExceptionError())
                this.LabelWarning.Text += err.Message;
        }
    }

    protected void btnReduplicate_Click(object sender, EventArgs e)
    {
        int EngOfId = -1;

        if (GridViewEngOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEngOffice.GetDataRow(GridViewEngOffice.FocusedRowIndex);
            EngOfId = (int)row["EngOfId"];
        }
        if (EngOfId == -1)
        {
            ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        FileManager.FindByEngOffCode(EngOfId, 0, -1);
        if (FileManager.Count > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
            return;
        }
        FileManager.FindByEngOffCode(EngOfId, -1, -1);//return last EOfId
        if (FileManager.Count > 0)
        {
            if (Convert.ToInt32(FileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.Invalid)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان درخواست تغییرات وجود ندارد.پروانه دفتر مورد باطل شده است";
                return;
            }
            Reduplicate(EngOfId, int.Parse(FileManager[0]["EOfId"].ToString()));

        }

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            string GridFilterString = GridViewEngOffice.FilterExpression;
            string SearchFilterString = GenerateFilterString();
            int EOfId = -1;
            int EngOfId = -1;

            if (GridViewEngOffice.FocusedRowIndex > -1)
            {
                DataRow row = GridViewEngOffice.GetDataRow(GridViewEngOffice.FocusedRowIndex);
                EngOfId = (int)row["EngOfId"];
            }
            if (EngOfId == -1)
            {
                ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
                return;
            }

            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewEngOffice.FindDetailRowTemplateControl(GridViewEngOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest == null)
            {
                ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            if (GridRequest.VisibleRowCount < 0)
            {
                ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            int index0 = GridRequest.FocusedRowIndex;
            if (index0 == -1)
            {
                ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            EOfId = int.Parse(GridRequest.GetDataRow(index0)["EOfId"].ToString());
            if (IsCallback)
                ASPxWebControl.RedirectOnCallback("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
            else
                Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
        }
        catch (Exception err)
        {
            ShowMessage("خطایی در بازخوانی اطلاعات رخ داده است");
            Utility.SaveWebsiteError(err);
        }
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        int focucedIndex = -1;

        if (GridViewEngOffice.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)GridViewEngOffice.FindDetailRowTemplateControl(GridViewEngOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (grid != null)
            {
                focucedIndex = grid.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = grid.GetDataRow(focucedIndex);
                    int TableId = (int)row["EOfId"];
                    int PostId = (int)row["EngOfId"];
                    int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
                    int WorkFlowCode = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;
                    string GridFilterString = GridViewEngOffice.FilterExpression;
                    String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                    "&PostId=" + Utility.EncryptQS(PostId.ToString());

                    if (IsCallback)
                    {
                        ASPxWebControl.RedirectOnCallback("../../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
                    }
                    else
                    {
                        Response.Redirect("../../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
                    }

                    //if (IsCallback)
                    //    ASPxWebControl.RedirectOnCallback("../../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
                    //else
                    //    Response.Redirect("../../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                    return;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                return;
            }

        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
            return;

        }



    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        Search();
    }
    #endregion

    #region //********************************************Grid's*****************************************************************************************************************

    protected void GridViewEngOffice_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (!Utility.IsDBNullOrNullValue(e.GetValue("FileConfirm")))
        {
            if (e.GetValue("FileConfirm").ToString() == "0")
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        }

        if (!Utility.IsDBNullOrNullValue(e.GetValue("IsConfirm")))
        {
            if (e.GetValue("IsConfirm").ToString() == "3")
                e.Row.ForeColor = System.Drawing.Color.Brown;
            //  }
            else
            {
                if (Utility.IsDBNullOrNullValue(e.GetValue("NotConfirmOfReId")))
                    return;
                if (!Utility.IsDBNullOrNullValue(e.GetValue("ReqType")))
                {
                    switch ((Convert.ToInt32(e.GetValue("ReqType"))))
                    {
                        case (int)TSP.DataManager.EngOffFileType.Change:
                            e.Row.ForeColor = System.Drawing.Color.DarkBlue;
                            break;
                        case (int)TSP.DataManager.EngOffFileType.Invalid:
                            e.Row.ForeColor = System.Drawing.Color.Red;
                            break;
                        case (int)TSP.DataManager.EngOffFileType.Reduplicate:
                            e.Row.ForeColor = System.Drawing.Color.DarkMagenta;
                            break;
                        case (int)TSP.DataManager.EngOffFileType.Revival:
                            e.Row.ForeColor = System.Drawing.Color.Green;
                            break;
                        case (int)TSP.DataManager.EngOffFileType.ChangeBaseInfo:
                            e.Row.ForeColor = System.Drawing.Color.Magenta;
                            break;
                        case (int)TSP.DataManager.EngOffFileType.SaveFileDocument:
                            break;
                    }
                }
            }
        }


    }

    protected void GridViewEngOffice_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "ParticipateLetterDate" || e.DataColumn.FieldName == "FileNo")
            e.Cell.Style["direction"] = "ltr";
        #region WF Cloumn comment
        //if (e.DataColumn.FieldName == "TaskId")
        //{
        //    DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewEngOffice.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewEngOffice.Columns["WFState"], "btnWFState");
        //    if (btnWFState != null)
        //    {
        //        if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
        //        {
        //            btnWFState.ToolTip = "تعریف نشده";
        //            btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
        //            return;
        //        }

        //        if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
        //        {
        //            btnWFState.ToolTip = e.GetValue("TaskName").ToString();
        //            btnWFState.ImageUrl = "~/Images/WFStart.png";
        //        }
        //        else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
        //        {
        //            btnWFState.ToolTip = e.GetValue("TaskName").ToString();
        //            btnWFState.ImageUrl = "~/Images/WFInProcess.png";
        //        }
        //        else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
        //        {
        //            btnWFState.ToolTip = e.GetValue("TaskName").ToString();
        //            btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
        //        }
        //        else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
        //        {
        //            btnWFState.ToolTip = e.GetValue("TaskName").ToString();
        //            btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
        //        }
        //    }
        //}
        #endregion

        if (e.DataColumn.Name == "ExpireState")
        {
            DevExpress.Web.ASPxImage ImgExpireState = (DevExpress.Web.ASPxImage)GridViewEngOffice.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewEngOffice.Columns["ExpireState"], "ImgExpireState");
            if (ImgExpireState != null)
            {
                if (Utility.IsDBNullOrNullValue(e.GetValue("LastExpireDate")))
                {
                    ImgExpireState.ToolTip = "نامشخص";
                    ImgExpireState.ImageUrl = "~/Images/WFUnNounState.png";
                    return;
                }
                else if (String.Compare(e.GetValue("LastExpireDate").ToString(), Utility.GetDateOfToday()) >= 0)
                {
                    ImgExpireState.ToolTip = "دارای اعتبار";
                    ImgExpireState.ImageUrl = "~/Images/CertificateValid.png";
                }
                else
                {
                    ImgExpireState.ToolTip = "پایان اعتبار";
                    ImgExpireState.ImageUrl = "~/Images/CertificateExpired.png";
                }
            }
        }
    }

    protected void GridViewEngOffice_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                //case "Search":
                //    Search();
                //    GridViewEngOffice.ExpandRow(0);
                //    break;
                //case "Clear":
                //    Clear();
                //    Search();
                //    //  ResetObjDts();
                //    break;
                default:
                    GridViewEngOffice.DataBind();
                    break;
            }
        }
    }

    protected void GridViewEngOffice_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "ParticipateLetterDate")
            e.Editor.Style["direction"] = "ltr";
        if (e.Column.FieldName == "LastExpireDate")
            e.Editor.Style["direction"] = "ltr";
        if (e.Column.FieldName == "FileNo")
            e.Editor.Style["direction"] = "ltr";
    }

    //protected void CustomAspxDevGridViewRequest_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    //{
    //    if (e.Column.FieldName == "FileNo" || e.Column.FieldName == "CreateDate" || e.Column.FieldName == "AnswerDate")
    //        e.Editor.Style["direction"] = "ltr";
    //}

    //protected void CustomAspxDevGridViewRequest_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    //{
    //    if (e.DataColumn.FieldName == "FileNo" || e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "AnswerDate" || e.DataColumn.FieldName == "LastExpireDate")
    //        e.Cell.Style["direction"] = "ltr";

    //    if (e.DataColumn.FieldName == "TaskId")
    //    {
    //        DevExpress.Web.ASPxGridView GridViewRequest = (DevExpress.Web.ASPxGridView)GridViewEngOffice.FindDetailRowTemplateControl(GridViewEngOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
    //        if (GridViewRequest == null)
    //            return;
    //        DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewRequest.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewRequest.Columns["WFState"], "btnWFState");
    //        if (btnWFState != null)
    //        {
    //            if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
    //            {
    //                btnWFState.ToolTip = "تعریف نشده";
    //                btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
    //                return;
    //            }

    //            if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
    //            {
    //                btnWFState.ToolTip = e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFStart.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
    //            {
    //                btnWFState.ToolTip = e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFInProcess.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
    //            {
    //                btnWFState.ToolTip = e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
    //            }
    //            else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
    //            {
    //                btnWFState.ToolTip = e.GetValue("TaskName").ToString();
    //                btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
    //            }
    //            else
    //            {
    //            }
    //        }
    //    }
    //}
    #endregion

    //*****************************************************CallBack's********************************************************************************************************

    protected void CallbackEngOffice_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (IsPageRefresh)
        {
            return;
        }
        switch (e.Parameter)
        {
            case "New":
                BtnNew_Click(this, new EventArgs());
                break;
            case "Edit":
                btnEdit_Click(this, new EventArgs());
                break;
            case "View":
                btnView_Click(this, new EventArgs());
                break;
            case "Delete":
                btnDelete_Click(this, new EventArgs());
                break;
            case "Change":
                btnChange_Click(this, new EventArgs());
                break;
            case "ChangeBaseInfo":
                btnChangeBaseInfo_Click(this, new EventArgs());
                break;
            case "Revival":
                btnRevival_Click(this, new EventArgs());
                break;
            case "Reduplicate":
                btnReduplicate_Click(this, new EventArgs());
                break;
            case "Invalid":
                btnInvalid_Click(this, new EventArgs());
                break;
            case "ConditionalAprrove":
                ConditionalAprrove();
                break;
            case "Tracing":
                btnTracing_Click(this, new EventArgs());
                break;
            case "Print":
                ArrayList DeletedColumnsName = new ArrayList();
                DeletedColumnsName.Add("WFState");
                DeletedColumnsName.Add("ExpireState");

                Session["DeletedColumnsName"] = DeletedColumnsName;
                Session["DataTable"] = GridViewEngOffice.Columns;
                Session["DataSource"] = OdbEngOffice;
                Session["Title"] = "مدیریت دفاتر";
                GridViewEngOffice.DetailRows.CollapseAllRows();
                CallbackEngOffice.JSProperties["cpDoPrint"] = 1;
                break;
            case "Activate":
                Activate();
                break;
        }
    }

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "DoNextTaskOfClosePopUP")
        {
            WFUserControl.PerformCallback(-2, -2, -2, e);
            GridViewEngOffice.DataBind();
            return;
        }
        int focucedIndex = -1;
        if (GridViewEngOffice.FocusedRowIndex < 0)
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        DataRow rowOff = GridViewEngOffice.GetDataRow(GridViewEngOffice.FocusedRowIndex);
        int EngOfId = (int)rowOff["EngOfId"];

        TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)GridViewEngOffice.FindDetailRowTemplateControl(GridViewEngOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
        if (grid == null)
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        focucedIndex = grid.FocusedRowIndex;
        if (focucedIndex < 0)
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        DataRow row = grid.GetDataRow(focucedIndex);
        int EOfId = (int)row["EOfId"];
        int WfCode = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;

        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);//(int)TSP.DataManager.TableTypeManager.TableCodes.EngOffice;
        WFUserControl.PerformCallback(EOfId, TableType, WfCode, e);
        grid.DataBind();
        GridViewEngOffice.DataBind();
    }

    protected void CustomAspxDevGridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["EngOfficeId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        int Index = GridViewEngOffice.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewEngOffice.FocusedRowIndex = Index;

    }

    #endregion

    #region Methods

    #region WorkFlow

    private int FindNmcId()
    {
        int UserId = Utility.GetCurrentUser_UserId();
        int NmcId = -1;
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();

        int EmpId = Utility.GetCurrentUser_MeId();
        int UltId = Utility.GetCurrentUser_LoginType();
        NezamChartManager.FindByEmpId(EmpId, UltId);
        if (NezamChartManager.Count > 0)
        {
            NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
        }
        else
        {
            DivReport.Visible = true;
            LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
        }
        return (NmcId);
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();       
        int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            if (CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo
                || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentEngOffice)
            {
                int PermissionSaveInfo = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo, Utility.GetCurrentUser_UserId());
                int PermissionSaveInfoEmployeeConfirming = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentEngOffice, Utility.GetCurrentUser_UserId());
                if (PermissionSaveInfo > 0 || PermissionSaveInfoEmployeeConfirming > 0)
                    return true;      
            }
        }
        return false;

    }

    private Boolean CheckPermitionForDelete(int TableId)
    {
        return TSP.DataManager.WorkFlowPermission.CheckWFPermissionForDeleteRequest(TableId, (int)TSP.DataManager.WorkFlows.EngOfficeConfirming
         , (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId);

        //TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        //int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        //int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
        //int WfCode = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;
        //DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
        //if (dtState.Rows.Count > 0)
        //{
        //    dtState.DefaultView.RowFilter = "StateType=" + ((int)TSP.DataManager.WorkFlowStateType.SendDocToNextStep).ToString();
        //    if (dtState.DefaultView.Count == 1)
        //    {
        //        int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
        //        int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
        //        int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
        //        int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
        //        int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());

        //        if ((Utility.GetCurrentUser_MeId() == Convert.ToInt32(dtState.Rows[0]["EmpId"]) || CurrentNmcId == FindNmcId()) && CurrentNmcIdType == (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId)
        //        {
        //            if (CurrentTaskCode == TaskCode)
        //                return true;
        //        }
        //    }
        //}
        //return false;

    }
    #endregion

    protected void Delete(int EngOfId, int EOfId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.EngOfficeManager EngOfficeManager = new TSP.DataManager.EngOfficeManager();
        TSP.DataManager.OfficeMemberManager MemManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TelManager telManager = new TSP.DataManager.TelManager();
        TSP.DataManager.AddressManager AddManager = new TSP.DataManager.AddressManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();

        trans.Add(FileManager);
        trans.Add(MemManager);
        trans.Add(attachManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(telManager);
        trans.Add(AddManager);
        trans.Add(EngOfficeManager);

        try
        {

            trans.BeginSave();

            MemManager.FindForDeleteEngOffice(EOfId, 1);
            if (MemManager.Count > 0)
            {
                int c = MemManager.Count;
                for (int i = 0; i < c; i++)
                    MemManager[0].Delete();

                MemManager.Save();

            }
            attachManager.FindByTablePrimaryKey(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile), EOfId);
            if (attachManager.Count > 0)
            {
                int c = attachManager.Count;
                for (int i = 0; i < c; i++)
                    attachManager[0].Delete();

                attachManager.Save();
            }
            int WfCode = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete(WfCode, EOfId);
            if (WorkFlowStateManager.Count > 0)
            {
                WorkFlowStateManager[0].Delete();
                WorkFlowStateManager.Save();
            }
            telManager.FindByTablePrimaryKey(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile), EOfId);
            if (telManager.Count > 0)
            {
                int c = telManager.Count;
                for (int i = 0; i < c; i++)
                    telManager[0].Delete();

                telManager.Save();
            }
            AddManager.FindByTablePrimaryKey(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile), EOfId);
            if (AddManager.Count > 0)
            {
                AddManager[0].Delete();
                AddManager.Save();
            }

            RequestInActivesManager.FindByTableIdTableType(-1, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember), EOfId);
            if (RequestInActivesManager.Count > 0)
            {
                RequestInActivesManager[0].Delete();
                RequestInActivesManager.Save();
            }

            FileManager.FindByCode(EOfId);
            if (FileManager.Count != 1)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                return;
            }
            int ReqType = Convert.ToInt32(FileManager[0]["Type"]);
            FileManager[0].Delete();
            int cn = FileManager.Save();
            FileManager.DataTable.AcceptChanges();
            if (cn != 1)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                return;
            }

            if (ReqType == (int)TSP.DataManager.EngOffFileType.SaveFileDocument)//درخواست اولیه  
            {
                EngOfficeManager.FindByCode(EngOfId);
                EngOfficeManager[0].Delete();
                EngOfficeManager.Save();
                EngOfficeManager.DataTable.AcceptChanges();
            }

            trans.EndSave();
            GridViewEngOffice.DataBind();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "حذف درخواست با موفقیت انجام شد";

        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
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
            if (Utility.ShowExceptionError())
                this.LabelWarning.Text += err.Message;
        }


    }

    private void Activate()
    {
        try
        {
            int EngOfId = -1;

            if (GridViewEngOffice.FocusedRowIndex <= -1)
            {
                ShowMessage("ابتدا یک دفتر را انتخاب نمایید");
                return;
            }
            DataRow row = GridViewEngOffice.GetDataRow(GridViewEngOffice.FocusedRowIndex);
            EngOfId = (int)row["EngOfId"];

            if (EngOfId == -1)
            {
                ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
                return;
            }

            TSP.DataManager.EngOffFileManager EngOffFileManager = new TSP.DataManager.EngOffFileManager();
            TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();
            EngOffManager.FindByCode(EngOfId);
            if (EngOffManager.Count <= 0)
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                return;
            }
            if (Convert.ToInt32(EngOffManager[0]["IsConfirm"]) == (int)TSP.DataManager.EngOfficeConfirmationType.Pending)
            {
                ShowMessage("امکان  ثبت درخواست جدید وجود ندارد.عضویت دفتر انتخابی در وضعیت در جریان می باشد.");
                return;
            }

            if (Convert.ToInt32(EngOffManager[0]["IsConfirm"]) != (int)TSP.DataManager.EngOfficeConfirmationType.Cancel
                && Convert.ToInt32(EngOffManager[0]["IsConfirm"]) != (int)TSP.DataManager.EngOfficeConfirmationType.NotConfirmed)
            {
                ShowMessage("امکان  ثبت درخواست احیا وجود ندارد.عضویت دفتر انتخابی در وضعیت در جریان و یا تایید شده می باشد.");
                return;
            }
            EngOffFileManager.FindByEngOffCode(EngOfId, 0, -1);
            if (EngOffFileManager.Count > 0)
            {
                ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
                return;
            }

            EngOffFileManager.FindByEngOffCode(EngOfId, 1, -1);//return last EOfId
            if (EngOffFileManager.Count > 0)
            {
                int EOfId = Convert.ToInt32(EngOffFileManager[0]["EOfId"]);
                string GridFilterString = GridViewEngOffice.FilterExpression;

                string SearchFilterString = GenerateFilterString();

                if (!CheckpermisionForNewRequest())
                {
                    return;
                }
                EngOffFileManager.FindByCode(EOfId);
                if (EngOffFileManager.Count != 1)
                {
                    ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                    return;
                }

                if (IsCallback)
                {
                    ASPxWebControl.RedirectOnCallback("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Activate") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                    return;
                }
                else
                    Response.Redirect("EngOfficeRegister.aspx?OfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Activate") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در درخواست انجام گرفته است.");
            if (Utility.ShowExceptionError())
                ShowMessage("خطایی در درخواست انجام گرفته است.", err);
        }



    }

    #region Request
    private void Change(int EngOfId, int EOfId)
    {
        string GridFilterString = GridViewEngOffice.FilterExpression;
        TSP.DataManager.EngOfficeManager EngOfficeManager = new TSP.DataManager.EngOfficeManager();
        string SearchFilterString = GenerateFilterString();
        try
        {
            if (!CheckpermisionForNewRequest())
            {
                return;
            }
            EngOfficeManager.FindByCode(EngOfId);
            if (EngOfficeManager.Count == 1)
            {
                if (EngOfficeManager[0]["IsConfirm"].ToString() != "0")
                {
                    if (IsCallback)
                        ASPxWebControl.RedirectOnCallback("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Change") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                    else
                        Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Change") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));

                }
                else
                {
                    ShowMessage("امکان درخواست تغییرات برای پروانه تایید نشده وجود ندارد.");
                }
            }
            else
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            if (Utility.ShowExceptionError())
                ShowMessage("خطایی در ذخیره انجام گرفته است.", err);
        }
    }

    private void ChangeBaseInfo(int EngOfId, int EOfId)
    {
        string GridFilterString = GridViewEngOffice.FilterExpression;
        TSP.DataManager.EngOfficeManager EngOfficeManager = new TSP.DataManager.EngOfficeManager();
        string SearchFilterString = GenerateFilterString();
        try
        {
            if (!CheckpermisionForNewRequest())
            {
                return;
            }
            EngOfficeManager.FindByCode(EngOfId);
            if (EngOfficeManager.Count == 1)
            {
                if (EngOfficeManager[0]["IsConfirm"].ToString() != "0")
                {
                    if (IsCallback)
                        ASPxWebControl.RedirectOnCallback("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("ChangeBaseInfo") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                    else
                        Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("ChangeBaseInfo") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));

                }
                else
                {
                    ShowMessage("امکان درخواست تغییرات برای پروانه تایید نشده وجود ندارد.");
                }
            }
            else
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            if (Utility.ShowExceptionError())
                ShowMessage("خطایی در ذخیره انجام گرفته است.", err);
        }
    }

    private void Reduplicate(int EngOfId, int EOfId)
    {
        string GridFilterString = GridViewEngOffice.FilterExpression;
        TSP.DataManager.EngOffFileManager EngOffFileManager = new TSP.DataManager.EngOffFileManager();
        string SearchFilterString = GenerateFilterString();
        try
        {
            if (!CheckpermisionForNewRequest())
            {
                return;
            }
            EngOffFileManager.FindByCode(EOfId);
            if (EngOffFileManager.Count == 1)
            {
                if (EngOffFileManager[0]["IsConfirm"].ToString() != "0")
                {
                    if (IsCallback)
                        ASPxWebControl.RedirectOnCallback("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Reduplicate") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                    else
                        Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Reduplicate") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                }
                else
                {
                    ShowMessage("امکان درخواست المثنی برای پروانه تایید نشده وجود ندارد.");
                }
            }
            else
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            if (Utility.ShowExceptionError())
                ShowMessage("خطایی در ذخیره انجام گرفته است.", err);
        }
    }

    private void Revival(int EngOfId, int EOfId)
    {
        string GridFilterString = GridViewEngOffice.FilterExpression;
        TSP.DataManager.EngOffFileManager EngOffFileManager = new TSP.DataManager.EngOffFileManager();
        string SearchFilterString = GenerateFilterString();
        try
        {
            if (!CheckpermisionForNewRequest())
            {
                return;
            }
            EngOffFileManager.FindByCode(EOfId);
            if (EngOffFileManager.Count == 1)
            {
                if (EngOffFileManager[0]["IsConfirm"].ToString() != "0")
                {
                    if (Utility.IsDBNullOrNullValue(EngOffFileManager[0]["ExpireDate"]))
                    {
                        ShowMessage("تاریخ اعتبار پروانه انتخاب شده نامشخص می باشد");
                        return;
                    }
                    string CrtEndDate = EngOffFileManager[0]["ExpireDate"].ToString();
                    Utility.Date objDate = new Utility.Date(CrtEndDate);
                    string LastMonth = objDate.AddMonths(-2);
                    string Today = Utility.GetDateOfToday();
                    int IsDocExp = string.Compare(Today, LastMonth);
                    if (IsDocExp > 0)
                    {
                        if (IsCallback)
                        {
                            ASPxWebControl.RedirectOnCallback("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Revival") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                            return;
                        }
                        else
                            Response.Redirect("EngOfficeRegister.aspx?OfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Revival") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));

                    }
                    else
                    {
                        ShowMessage("تاریخ اعتبار پروانه انتخاب شده به پایان نرسیده است.امکان تمدید وجود ندارد");

                    }
                }
                else
                {
                    ShowMessage("امکان تمدید برای پروانه تایید نشده وجود ندارد.");
                }
            }
            else
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در سیستم انجام گرفته است.");
            if (Utility.ShowExceptionError())
                ShowMessage("خطایی در سیستم انجام گرفته است.", err);
        }
    }

    private void InValid(int EngOfId, int EOfId)
    {
        string GridFilterString = GridViewEngOffice.FilterExpression;
        TSP.DataManager.EngOffFileManager EngOffFileManager = new TSP.DataManager.EngOffFileManager();
        string SearchFilterString = GenerateFilterString();
        try
        {
            if (!CheckpermisionForNewRequest())
            {
                return;
            }
            EngOffFileManager.FindByCode(EOfId);
            if (EngOffFileManager.Count == 1)
            {
                if (EngOffFileManager[0]["IsConfirm"].ToString() != "0")
                {
                    if (IsCallback)
                        ASPxWebControl.RedirectOnCallback("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("InValid") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                    else
                        Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("InValid") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                }
                else
                {
                    ShowMessage("امکان درخواست ابطال برای پروانه تایید نشده وجود ندارد.");
                }
            }
            else
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            if (Utility.ShowExceptionError())
                ShowMessage("خطایی در ذخیره انجام گرفته است.", err);
        }
    }

    private void ConditionalAprrove()
    {
        try
        {
            int EngOfId = -1;

            if (GridViewEngOffice.FocusedRowIndex > -1)
            {
                DataRow row = GridViewEngOffice.GetDataRow(GridViewEngOffice.FocusedRowIndex);
                EngOfId = (int)row["EngOfId"];
            }
            if (EngOfId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";
                return;
            }
            TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
            TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();
            EngOffManager.FindByCode(EngOfId);
            if (EngOffManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            if (EngOffManager[0]["IsConfirm"].ToString() != "1")
            {
                ShowMessage("امکان ویرایش وجود ندارد.عضویت دفتر انتخابی تایید شده نمی باشد.");
                return;
            }
            FileManager.FindByEngOffCode(EngOfId, 0, -1);
            if (FileManager.Count > 0)
            {
                ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
                return;
            }
            FileManager.FindByEngOffCode(EngOfId, -1, -1);//return last EOfId
            if (FileManager.Count > 0)
            {
                if (Convert.ToInt32(FileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.Invalid)//ابطال
                {
                    ShowMessage("امکان درخواست تغییرات وجود ندارد.پروانه دفتر مورد نظر باطل شده است");
                    return;
                }
                //ChangeBaseInfo(EngOfId, Convert.ToInt32(FileManager[0]["EOfId"]));
                int EOfId = Convert.ToInt32(FileManager[0]["EOfId"]);
                string GridFilterString = GridViewEngOffice.FilterExpression;
                TSP.DataManager.EngOfficeManager EngOfficeManager = new TSP.DataManager.EngOfficeManager();
                string SearchFilterString = GenerateFilterString();

                if (!CheckpermisionForNewRequest())
                {
                    return;
                }
                EngOfficeManager.FindByCode(EngOfId);
                if (EngOfficeManager.Count == 1)
                {
                    if (EngOfficeManager[0]["IsConfirm"].ToString() != "0")
                    {
                        if (IsCallback)
                            ASPxWebControl.RedirectOnCallback("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("ConditionalAprrove") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                        else
                            Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("ConditionalAprrove") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));

                    }
                    else
                    {
                        ShowMessage("امکان درخواست تغییرات برای پروانه تایید نشده وجود ندارد.");
                    }
                }
                else
                {
                    ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                }

            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            if (Utility.ShowExceptionError())
                ShowMessage("خطایی در ذخیره انجام گرفته است.", err);
        }
    }
    #endregion

    #region WF
    /// <summary>
    /// جهت تنظیم سطح دسترسی دکمه های درخواست ها
    /// </summary>
    /// <returns></returns>
    private Boolean CheckWorkFlowPermissionForChangeReq()
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId());
        return (WFPer.BtnNewRequest);
    }

    /// <summary>
    /// جهت چک کردن سطح دسترسی هنگام کلیک بر روی دکمه های درخواست
    /// </summary>
    /// <returns></returns>
    private Boolean CheckpermisionForNewRequest()
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        TSP.DataManager.WFPermission Per = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(TaskCode, TableType, Utility.GetCurrentUser_UserId());
        if (!Per.BtnNewRequest)
        {
            ShowMessage("شما دارای سطح دسترسی گردش کار جهت ثبت درخواست جدید نمی باشید");
            return false;
        }
        return true;
    }
    #endregion

    #region FilteringMethod
    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
                string SrchFlt = Utility.DecryptQS(Request.QueryString["SrchFlt"].ToString());

                if (!string.IsNullOrEmpty(SrchFlt))
                    FilterObjdsByValue(SrchFlt);
                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewEngOffice.FilterExpression = GrdFlt;
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
                string PostId = Utility.DecryptQS(Request.QueryString["PostId"].ToString());
                if (!string.IsNullOrEmpty(PostId))
                {
                    int PostKeyValue = int.Parse(PostId);

                    GridViewEngOffice.DataBind();
                    Index = GridViewEngOffice.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewEngOffice.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewEngOffice.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewEngOffice.JSProperties["cpSelectedIndex"] = Index;
                        GridViewEngOffice.DetailRows.ExpandRow(Index);
                        GridViewEngOffice.FocusedRowIndex = Index;
                        GridViewEngOffice.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
        return Index;
    }

    private void FilterObjdsByValue(string FilterString)
    {
        string[] SearchFilter = FilterString.Split('&');
        for (int i = 0; i < SearchFilter.Length; i = i + 2)
        {
            string ParameterName = SearchFilter[i].ToString();
            string Value = SearchFilter[i + 1].ToString();
            OdbEngOffice.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    case "MeId":                        
                    case "EndDateFrom":
                        txtEndDateFrom.Text = Value;
                        break;
                    case "EndDateTo":
                        txtEndDateTo.Text = Value;
                        break;
                    case "FollowCode":
                        txtFollowCode.Text = Value;
                        break;
                    case "ReqType":
                        CmbReqType.DataBind();
                        if (Value == "-1")
                        {
                            CmbReqType.DataBind();
                            CmbReqType.SelectedIndex = -1;
                        }
                        else
                        {
                            CmbReqType.SelectedIndex = CmbReqType.Items.FindByValue(Value).Index;
                        }
                        break;
                    case "ManagerName":
                        txtManagerName.Text = Value;
                        break;
                    case "ManagerFamily":
                        txtManagerfamily.Text = Value;
                        break;
                    case "EngOffName":
                        txtEngOffName.Text = Value;
                        break;
                    case "EngOfId":
                        if (Value != "-1")
                            txtEngOffId.Text = Value;
                        break;
                    case "CreateDateFrom":
                        txtCreateDateFrom.Text = Value;
                        break;
                    case "CreateDateTo":
                        txtCreateDateTo.Text = Value;
                        break;
                    case "TaskId":
                        if (Value != "-1")
                            CmbTask.SelectedIndex = CmbTask.Items.FindByValue(Value).Index;
                        break;
                }
            }
        }
    }

    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < OdbEngOffice.SelectParameters.Count; i++)
        {
            if (OdbEngOffice.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += OdbEngOffice.SelectParameters[i].Name + "&";
                FilterString += OdbEngOffice.SelectParameters[i].DefaultValue + "&";
            }
        }
        FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }
    #endregion

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void ShowMessage(string Message, Exception err)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message + err.Message;
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.EngOffice).ToString());
    }

    void Search()
    {
        if (!string.IsNullOrEmpty(txtEndDateFrom.Text))
            OdbEngOffice.SelectParameters["EndDateFrom"].DefaultValue = txtEndDateFrom.Text;
        else
            OdbEngOffice.SelectParameters["EndDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtEndDateTo.Text))
            OdbEngOffice.SelectParameters["EndDateTo"].DefaultValue = txtEndDateTo.Text;
        else
            OdbEngOffice.SelectParameters["EndDateTo"].DefaultValue = "2";

        if (!string.IsNullOrEmpty(txtFollowCode.Text))
            OdbEngOffice.SelectParameters["FollowCode"].DefaultValue = txtFollowCode.Text;
        else
            OdbEngOffice.SelectParameters["FollowCode"].DefaultValue = "%";       

        if (!string.IsNullOrEmpty(txtManagerName.Text))
            OdbEngOffice.SelectParameters["ManagerName"].DefaultValue = txtManagerName.Text;
        else
            OdbEngOffice.SelectParameters["ManagerName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtManagerfamily.Text))
            OdbEngOffice.SelectParameters["Managerfamily"].DefaultValue = txtManagerfamily.Text;
        else
            OdbEngOffice.SelectParameters["Managerfamily"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtEngOffName.Text))
            OdbEngOffice.SelectParameters["EngOffName"].DefaultValue = txtEngOffName.Text;
        else
            OdbEngOffice.SelectParameters["EngOffName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtEngOffId.Text))
            OdbEngOffice.SelectParameters["EngOfId"].DefaultValue = txtEngOffId.Text;
        else
            OdbEngOffice.SelectParameters["EngOfId"].DefaultValue = "-1";

        if (CmbReqType.SelectedIndex > 0)
            OdbEngOffice.SelectParameters["ReqType"].DefaultValue = CmbReqType.Value.ToString();
        else
            OdbEngOffice.SelectParameters["ReqType"].DefaultValue = "-1";

        if (CmbTask.SelectedIndex > 0)
            OdbEngOffice.SelectParameters["TaskId"].DefaultValue = CmbTask.Value.ToString();
        else
            OdbEngOffice.SelectParameters["TaskId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtCreateDateFrom.Text))
            OdbEngOffice.SelectParameters["CreateDateFrom"].DefaultValue = txtCreateDateFrom.Text;
        else
            OdbEngOffice.SelectParameters["CreateDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtCreateDateTo.Text))
            OdbEngOffice.SelectParameters["CreateDateTo"].DefaultValue = txtCreateDateTo.Text;
        else
            OdbEngOffice.SelectParameters["CreateDateTo"].DefaultValue = "2";
        if (!string.IsNullOrEmpty(txtWFTo.Text))
            OdbEngOffice.SelectParameters["WFDateTo"].DefaultValue = txtWFTo.Text;
        else
            OdbEngOffice.SelectParameters["WFDateTo"].DefaultValue = "2";

        if (!string.IsNullOrEmpty(txtWFFrom.Text))
            OdbEngOffice.SelectParameters["WFDateFrom"].DefaultValue = txtWFFrom.Text;
        else
            OdbEngOffice.SelectParameters["WFDateFrom"].DefaultValue = "1";
        if (!string.IsNullOrEmpty(txtEndAuditor.Text))
            OdbEngOffice.SelectParameters["WFDoerName"].DefaultValue = txtEndAuditor.Text;
        else
            OdbEngOffice.SelectParameters["WFDoerName"].DefaultValue = "%";
        GridViewEngOffice.DataBind();
    }

    private void Clear()
    {
        txtEndDateFrom.Text =
        txtEndDateTo.Text =
        txtFollowCode.Text =
        txtManagerfamily.Text =        
        txtManagerName.Text =
        txtEngOffId.Text =
        txtCreateDateFrom.Text =
        txtCreateDateTo.Text =
        txtEngOffName.Text = "";
        CmbReqType.SelectedIndex = CmbTask.SelectedIndex = 0;
    }

    //private void ResetObjDts()
    //{
    //    OdbEngOffice.SelectParameters["EndDateFrom"].DefaultValue = "1";
    //    OdbEngOffice.SelectParameters["EndDateTo"].DefaultValue = "2";
    //    OdbEngOffice.SelectParameters["FollowCode"].DefaultValue = "%";
    //    OdbEngOffice.SelectParameters["MeId"].DefaultValue = "-1";
    //    OdbEngOffice.SelectParameters["ManagerName"].DefaultValue = "%";
    //    OdbEngOffice.SelectParameters["Managerfamily"].DefaultValue = "%";
    //    OdbEngOffice.SelectParameters["EngOffName"].DefaultValue = "%";
    //    OdbEngOffice.SelectParameters["EngOfId"].DefaultValue = "-1";
    //    OdbEngOffice.SelectParameters["ReqType"].DefaultValue = "-1";
    //    OdbEngOffice.SelectParameters["CreateDateFrom"].DefaultValue = "1";
    //    OdbEngOffice.SelectParameters["CreateDateTo"].DefaultValue = "2";
    //    GridViewEngOffice.DataBind();
    //}
    #endregion
}
