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

public partial class Employee_OfficeRegister_OfficeDocument : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            SetHelpAddress();
            GridViewOffice.JSProperties["cpSelectedIndex"] = 0;
            GridViewOffice.JSProperties["cpIsPostBack"] = 0;

            Session["OfficeId"] = null;

            Session["SendBackDataTable_OfConf"] = "";

          

            ObjdsWorkFlowTask.SelectParameters["WorkFlowCodeList"].DefaultValue = ((int)TSP.DataManager.WorkFlows.OfficeConfirming).ToString() + "," + ((int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming).ToString();
            // ((int)TSP.DataManager.WorkFlows.OfficeConfirming).ToString();    

            CmbTask.DataBind();
            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("-----------------", null));
            CmbTask.SelectedIndex = 0;

            CheckTablePermission();

            btnChange.Enabled = btnChange1.Enabled =
             btnReduplicate.Enabled = btnReduplicate2.Enabled =
             btnRevival.Enabled = btnRevival1.Enabled == CheckpermisionForNewRequest();
            //btnChangeBaseInfo.Enabled = btnChangeBaseInfo2.Enabled 

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnTracing"] = btnTracing.Enabled;
            this.ViewState["BtnRequset"] = btnChange.Enabled;
        }
        //  Search();
        //  GridViewOffice.DataBind();
        SetPageFilter();
        SetGridRowIndex();

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = this.btnSearch.Enabled = this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = btnPrint.Enabled = btnPrint2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnTracing"] != null)
            this.btnTracing.Enabled = this.btnTracing2.Enabled = (bool)this.ViewState["BtnTracing"];
        if (this.ViewState["BtnRequset"] != null)
            btnChange.Enabled = btnChange1.Enabled =
            btnReduplicate.Enabled = btnReduplicate2.Enabled =
            btnRevival.Enabled = btnRevival1.Enabled = (bool)this.ViewState["BtnRequset"];
        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        String Script = @"
                        function Clear()
                        {
        txtOfId.SetText('');
        txtOfName.SetText('');
        txtFileNo.SetText('');
        txtFollowCode.SetText('');
        txtMeId.SetText('');
        cmbMFType.SetSelectedIndex(0);
        cmbDocStatus.SetSelectedIndex(0);
        CmbTask.SetSelectedIndex(0);
        document.getElementById('" + txtEndDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtEndDateTo.ClientID + @"').value='';
        document.getElementById('" + txtCreateDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtCreateDateTo.ClientID + @"').value='';
        document.getElementById('" + txtCreateDateLastReqFrom.ClientID + @"').value='';
        document.getElementById('" + txtCreateDateLastReqTo.ClientID + @"').value='';
                        }";
        Script += " function CheckSearch() {";
        Script += "var txtEndDateFrom = document.getElementById('" + txtEndDateFrom.ClientID + "').value; ";
        Script += "var txtEndDateTo = document.getElementById('" + txtEndDateTo.ClientID + "').value;";
        Script += "var txtCreateDateFrom = document.getElementById('" + txtCreateDateFrom.ClientID + "').value;";
        Script += "var txtCreateDateTo = document.getElementById('" + txtCreateDateTo.ClientID + "').value;";
        Script += "var txtCreateDateLastReqFrom = document.getElementById('" + txtCreateDateLastReqFrom.ClientID + "').value;";
        Script += "var txtCreateDateLastReqTo = document.getElementById('" + txtCreateDateLastReqTo.ClientID + "').value;";
        Script += "if ( txtOfId.GetText('')=='' && txtOfName.GetText('')=='' && txtFileNo.GetText('')=='' && txtFollowCode.GetText('')=='' && txtMeId.GetText() == '' &&  cmbMFType.GetSelectedIndex() == 0 && cmbDocStatus.GetSelectedIndex() == 0 && CmbTask.GetSelectedIndex() == 0 && txtEndDateFrom=='' && txtEndDateTo=='' && txtCreateDateFrom=='' && txtCreateDateTo=='' && txtCreateDateLastReqFrom=='' && txtCreateDateLastReqTo=='') {return 0;} else{ return 1;}  }";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", Script, true);
    }

    #region btn Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int OfId = -1;
        int OfReId = -1;
        Int16 ReType = -1;

        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            OfId = (int)row["OfId"];
        }
        if (OfId == -1)
        {
            ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            try
            {
                TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
                if (GridRequest != null)
                {
                    if (GridRequest.VisibleRowCount > 0)
                    {
                        int index0 = GridRequest.FocusedRowIndex;
                        if (index0 != -1)
                        {
                            OfReId = int.Parse(GridRequest.GetDataRow(index0)["OfReId"].ToString());
                            ReType = short.Parse(GridRequest.GetDataRow(index0)["Type"].ToString());
                            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
                            ReqManager.FindByCode(OfReId);
                            if (ReqManager[0]["IsConfirm"].ToString() != "0")
                            {
                                ShowMessage("امکان ویرایش اطلاعات برای درخواست پاسخ داده شده وجود ندارد");
                                return;

                            }
                            if (!Convert.ToBoolean(ReqManager[0]["Requester"]))
                            {
                                ShowMessage("امکان ویرایش اطلاعات برای شما وجود ندارد");
                                return;
                            }
                            if (CheckPermitionForEdit(OfReId))
                            {
                                string GrdFlt = GridViewOffice.FilterExpression;
                                string SearchFilterString = GenerateFilterString();
                                Response.Redirect("OfficeDocumentInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GrdFlt) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                            }
                            else
                            {
                                ShowMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار برای شما وجود ندارد");
                            }
                        }
                        else
                        {
                            ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                        }
                    }
                    else
                    {
                        ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                    }
                }
                else
                {
                    ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                }
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                ShowMessage("خطایی در بازخوانی اطلاعات رخ داده است");
            }
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int OfId = -1;
        int OfReId = -1;
        Int16 ReType = -1;


        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            OfId = (int)row["OfId"];

        }
        if (OfId == -1)
        {
            ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");

        }
        else
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        OfReId = int.Parse(GridRequest.GetDataRow(index0)["OfReId"].ToString());
                        ReType = short.Parse(GridRequest.GetDataRow(index0)["Type"].ToString());
                        string GrdFlt = GridViewOffice.FilterExpression;
                        string SearchFilterString = GenerateFilterString();
                        Response.Redirect("OfficeDocumentInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&Mode=" + Utility.EncryptQS(ReType.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GrdFlt) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                    }
                    else
                    {
                        ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                    }
                }
                else
                {
                    ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                }


            }
            else
            {
                ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");

            }
        }
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        int focucedIndex = -1;
        int PostId = int.Parse(GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex)["OfId"].ToString());
        string GridFilterString = GridViewOffice.FilterExpression;
        string SearchFilterString = GenerateFilterString();
        if (GridViewOffice.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (grid != null)
            {

                int WorkFlowCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString) +
                   "&PostId=" + Utility.EncryptQS(PostId.ToString());

                focucedIndex = grid.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = grid.GetDataRow(focucedIndex);
                    int TableId = (int)row["OfReId"];
                    int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
                    //  string SearchFilterString = GenerateFilterString();
                    Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString())
                        + "&UrlReferrer=" + Utility.EncryptQS(Url));//+ "&SrchFlt=" + Utility.EncryptQS(SearchFilterString)

                }
                else
                {
                    ShowMessage(" لطفاً ابتدا یک درخواست را انتخاب نمائید");
                }
            }
            else
            {
                ShowMessage(" لطفاً ابتدا یک درخواست را انتخاب نمائید");
                return;
            }

        }
        else
        {
            ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید ");
            return;

        }

    }

    protected void btnDocNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeDocumentInsert.aspx?PageMode=" + Utility.EncryptQS("New") + "&OfId=" + Utility.EncryptQS("-1") + "&OfReId=" + Utility.EncryptQS("-1"));
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        int OfId = -1;

        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            OfId = (int)row["OfId"];

        }
        if (OfId == -1)
        {
            ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        OffManager.FindByCode(OfId);
        if (OffManager.Count <= 0)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        int MRsId = int.Parse(OffManager[0]["MrsId"].ToString());
        if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed
            && MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.DocumentCancel
            && MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval)
        {
            ShowMessage("امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.");
            return;
        }
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        ReqManager.FindByOfficeId(OfId, 0, -1);
        if (ReqManager.Count > 0)
        {
            ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
            return;
        }
        ReqManager.FindByOfficeId(OfId, 1, -1, 1);
        if (ReqManager.Count <= 0)
        {
            ShowMessage("امکان درخواست جدید وجود ندارد.برای عضو مورد نظر پروانه تایید شده صادر نشده است");
            return;
        }

        if (CheckpermisionForNewRequest())
            Response.Redirect("OfficeDocumentInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString())
                + "&PageMode=" + Utility.EncryptQS("Change")
                + "&OfReId=" + Utility.EncryptQS("-1"));
    }



    protected void btnRevival_Click(object sender, EventArgs e)
    {
        int OfId = -1;

        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            OfId = (int)row["OfId"];

        }
        if (OfId == -1)
        {
            ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        ReqManager.FindByOfficeId(OfId, 0, -1);
        if (ReqManager.Count > 0)
        {
            ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
            return;
        }
        ReqManager.FindByOfficeId(OfId, 1, -1, 1);
        if (ReqManager.Count <= 0)
        {
            ShowMessage("امکان درخواست تغییرات وجود ندارد.برای عضو مورد نظر پروانه تایید شده صادر نشده است");
            return;
        }

        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        OffManager.FindByCode(OfId);
        if (OffManager.Count <= 0)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        } int DocumentStatus = int.Parse(OffManager[0]["DocumentStatus"].ToString());
        if (DocumentStatus != (int)TSP.DataManager.OfficeDocumentStatus.Confirmed)
        {
            ShowMessage("امکان ثبت درخواست جدید برای پروانه شرکت وجود ندارد.پروانه عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.");
            return;
        }
        int MRsId = int.Parse(OffManager[0]["MrsId"].ToString());
        if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed
            && MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.DocumentCancel
            && MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval)
        {
            ShowMessage("امکان ویرایش وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.");
            return;
        }
        if (Utility.IsDBNullOrNullValue(OffManager[0]["FileDate"]))
        {
            ShowMessage("تاریخ اعتبار پروانه مشخص نمی باشد.امکان تمدید وجود ندارد");

            return;
        }
        string FileDate = OffManager[0]["FileDate"].ToString();
        Utility.Date objDate = new Utility.Date(FileDate);
        string LastMonth = objDate.AddMonths(-2);
        string Today = Utility.GetDateOfToday();
        int IsDocExp = string.Compare(Today, LastMonth);
        if (IsDocExp <= 0)
        {
            ShowMessage("تاریخ اعتبار پروانه انتخاب شده به پایان نرسیده است.امکان تمدید وجود ندارد");
            return;
        }



        if (CheckpermisionForNewRequest())
            Response.Redirect("OfficeDocumentInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString())
                + "&PageMode=" + Utility.EncryptQS("Revival")
                + "&OfReId=" + Utility.EncryptQS("-1"));
    }

    protected void btnReduplicate_Click(object sender, EventArgs e)
    {
        int OfId = -1;
        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            OfId = (int)row["OfId"];

        }
        if (OfId == -1)
        {
            ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        OffManager.FindByCode(OfId);
        if (OffManager.Count <= 0)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        int DocumentStatus = int.Parse(OffManager[0]["DocumentStatus"].ToString());
        if (DocumentStatus != (int)TSP.DataManager.OfficeDocumentStatus.Confirmed)
        {
            ShowMessage("امکان ثبت درخواست جدید برای پروانه شرکت وجود ندارد.پروانه عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.");
            return;
        }
        int MRsId = int.Parse(OffManager[0]["MrsId"].ToString());
        if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed
            && MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.DocumentCancel
            && MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval)
        {
            ShowMessage("امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.");
            return;
        }
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        ReqManager.FindByOfficeId(OfId, 0, -1);
        if (ReqManager.Count > 0)
        {
            ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
            return;
        }
        ReqManager.FindByOfficeId(OfId, 1, -1, 1);
        if (ReqManager.Count <= 0)
        {
            ShowMessage("امکان درخواست جدید وجود ندارد.برای عضو مورد نظر پروانه تایید شده صادر نشده است");
            return;
        }

        if (CheckpermisionForNewRequest())
            Response.Redirect("OfficeDocumentInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString())
                            + "&PageMode=" + Utility.EncryptQS("Reduplicate")
                            + "&OfReId=" + Utility.EncryptQS("-1"));
    }

    protected void btnDocumentInvalid_Click(object sender, EventArgs e)
    {
        int OfId = -1;
        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            OfId = (int)row["OfId"];

        }
        if (OfId == -1)
        {
            ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        OffManager.FindByCode(OfId);
        if (OffManager.Count <= 0)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        int DocumentStatus = int.Parse(OffManager[0]["DocumentStatus"].ToString());
        if (DocumentStatus != (int)TSP.DataManager.OfficeDocumentStatus.Confirmed)
        {
            ShowMessage("امکان ثبت درخواست جدید برای پروانه شرکت وجود ندارد.پروانه عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.");
            return;
        }
        int MRsId = int.Parse(OffManager[0]["MrsId"].ToString());
        //if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed
        //    && MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.DocumentCancel
        //    && MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval)
        //{
        //    ShowMessage("امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.");
        //    return;
        //}
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        ReqManager.FindByOfficeId(OfId, 0, -1);
        if (ReqManager.Count > 0)
        {
            ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
            return;
        }
        ReqManager.FindByOfficeId(OfId, 1, -1, 1);
        if (ReqManager.Count <= 0)
        {
            ShowMessage("امکان درخواست جدید وجود ندارد.برای عضو مورد نظر پروانه تایید شده صادر نشده است");
            return;
        }
        if (CheckpermisionForNewRequest())
            Response.Redirect("OfficeDocumentInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString())
                + "&PageMode=" + Utility.EncryptQS("DocumentInvalid") + "&OfReId="
                + Utility.EncryptQS("-1"));

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int OfId = -1;
        int OfReId = -1;

        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            OfId = (int)row["OfId"];
        }
        if (OfId == -1)
        {
            ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");

        }
        else
        {

            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        OfReId = int.Parse(GridRequest.GetDataRow(index0)["OfReId"].ToString());
                        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
                        ReqManager.FindByCode(OfReId);
                        if (ReqManager.Count > 0)
                        {
                            if (!Convert.ToBoolean(ReqManager[0]["Requester"]))
                            {
                                ShowMessage("امکان حذف برای درخواست صادر شده توسط عضو حقوقی وجود ندارد");
                                return;
                            }
                            if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo)//درخواست اولیه
                            {
                                ShowMessage("امکان حذف درخواست اولیه ثبت نام وجود ندارد");
                                return;
                            }
                            if (ReqManager[0]["IsConfirm"].ToString() != "0")
                            {
                                ShowMessage("امکان حذف برای درخواست پاسخ داده شده وجود ندارد");
                                return;
                            }
                            if (CheckPermitionForDelete(OfReId))
                                Delete(OfId, OfReId);
                            else
                            {
                                ShowMessage("امکان حذف درخواست در این مرحله از گردش کار برای شما وجود ندارد");
                            }

                        }
                        else
                        {

                            ShowMessage("خطایی در بازخوانی اطلاعات رخ داده است");
                        }
                    }
                    else
                    {
                        ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                    }
                }
                else
                {
                    ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                }


            }
            else
            {
                ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");


            }
        }
    }

    //protected void btnChangeBaseInfo_Click(object sender, EventArgs e)
    //{
    //    int OfId = -1;

    //    if (GridViewOffice.FocusedRowIndex > -1)
    //    {
    //        DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
    //        OfId = (int)row["OfId"];

    //    }
    //    if (OfId == -1)
    //    {
    //        ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
    //        return;
    //    }
    //    TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
    //    TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
    //    OffManager.FindByCode(OfId);
    //    if (OffManager.Count <= 0)
    //    {
    //        ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
    //        return;
    //    }
    //    if (OffManager[0]["MrsId"].ToString() != "1")
    //    {
    //        ShowMessage("امکان ویرایش وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.");
    //        return;
    //    }
    //    ReqManager.FindByOfficeId(OfId, 0, -1);
    //    if (ReqManager.Count > 0)
    //    {
    //        ShowMessage("به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد");
    //        return;
    //    }

    //    TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
    //    if (GridRequest != null)
    //    {
    //        if (GridRequest.VisibleRowCount > 0)
    //        {
    //            int index0 = GridRequest.FocusedRowIndex;
    //            int OfReId = -1;
    //            if (index0 != -1)
    //            {
    //                OfReId = int.Parse(GridRequest.GetDataRow(index0)["OfReId"].ToString());
    //                if (OfReId == -1)
    //                {
    //                    ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
    //                }
    //                ReqManager.FindByCode(OfReId);
    //                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
    //                {
    //                    int MFType = Convert.ToInt32(ReqManager[0]["MFType"]);
    //                    if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//طراح و ناظر
    //                    {
    //                        ReqManager.FindByOfficeId(OfId, -1, (int)TSP.DataManager.OfficeRequestType.SaveFileDocument);
    //                        if (ReqManager.Count == 0)
    //                        {
    //                            ShowMessage("امکان درخواست تغییرات پایه وجود ندارد.برای عضو مورد نظر پروانه صادر نشده است");
    //                            return;
    //                        }
    //                        else if (ReqManager[0]["IsConfirm"].ToString() != "1")
    //                        {

    //                            ShowMessage("امکان درخواست تغییرات پایه وجود ندارد.پروانه عضو مورد نظر تایید نشده است");
    //                            return;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    ReqManager.FindByOfficeId(OfId, -1, -1);//return last OfReId
    //    if (ReqManager.Count > 0)
    //    {
    //        if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.Invalid)//ابطال
    //        {
    //            ShowMessage("امکان درخواست تغییرات پایه وجود ندارد.پروانه عضو مورد نظر باطل شده است");
    //            return;
    //        }
    //        ChangeBaseInfo(int.Parse(ReqManager[0]["OfReId"].ToString()), OfId);
    //    }
    //}

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        //GridViewMemberFile.Columns["Status"].Visible = false;
        GridViewExporter.FileName = "OfficeDocument";

        GridViewExporter.WriteXlsxToResponse(true);
        //GridViewMemberFile.Columns["Status"].Visible = true;
    }

    #endregion

    #region GridViewOffice
    protected void GridViewOffice_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        //if (!Utility.IsDBNullOrNullValue(e.GetValue("IsConfirm")) && !Utility.IsDBNullOrNullValue(e.GetValue("Type")))//&& e.GetValue("Type") != null)
        //{
        //    if (e.GetValue("IsConfirm").ToString() == "0"
        //        && Convert.ToInt32(e.GetValue("Type")) != (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo
        //        && Convert.ToInt32(e.GetValue("Type")) != (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset
        //        && Convert.ToInt32(e.GetValue("Type")) != (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo)
        //        e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        //}
        if (!Utility.IsDBNullOrNullValue(e.GetValue("OffStatus")))
        {
            if (Convert.ToInt32(e.GetValue("MrsId")) == (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval)
            {
                e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
            }
        }
        if (!Utility.IsDBNullOrNullValue(e.GetValue("LastReqType")))
        {
            switch ((Convert.ToInt32(e.GetValue("LastReqType"))))
            {
                //case (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo:
                //    e.Row.ForeColor = System.Drawing.Color.Orange;
                //    break;
                case (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset:
                    e.Row.ForeColor = System.Drawing.Color.Olive;
                    break;
                case (int)TSP.DataManager.OfficeRequestType.Change:
                    e.Row.ForeColor = System.Drawing.Color.DarkBlue;
                    break;
                case (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo:
                    e.Row.ForeColor = System.Drawing.Color.DeepPink;
                    break;
                case (int)TSP.DataManager.OfficeRequestType.DocumentInvalid:
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    break;
                case (int)TSP.DataManager.OfficeRequestType.Reduplicate:
                    e.Row.ForeColor = System.Drawing.Color.DarkMagenta;
                    break;
                case (int)TSP.DataManager.OfficeRequestType.Revival:
                    e.Row.ForeColor = System.Drawing.Color.Green;
                    break;
                case (int)TSP.DataManager.OfficeRequestType.SaveFileDocument:
                    break;
            }

        }

        if (!Utility.IsDBNullOrNullValue(e.GetValue("DocumentStatus")))
        {
            switch ((Convert.ToInt32(e.GetValue("DocumentStatus"))))
            {
                case (int)TSP.DataManager.OfficeDocumentStatus.DocumentCancel:
                    e.Row.ForeColor = System.Drawing.Color.Brown;
                    break;
            }
        }
    }

    protected void GridViewOffice_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        //switch (e.DataColumn.FieldName)
        //{
        //    case "CreateDate":
        //        e.Cell.Style["direction"] = "ltr";
        //        break;
        //    case "RegDate":
        //        e.Cell.Style["direction"] = "ltr";
        //        break;
        //    case "LastExpireDate":
        //        e.Cell.Style["direction"] = "ltr";
        //        break;
        //    case "FileNo":
        //        e.Cell.Style["direction"] = "ltr";
        //        break;

        //}

        if (e.DataColumn.Name == "ExpireState")
        {
            DevExpress.Web.ASPxImage ImgExpireState = (DevExpress.Web.ASPxImage)GridViewOffice.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewOffice.Columns["ExpireState"], "ImgExpireState");
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

    protected void GridViewOffice_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
        else if (e.Column.FieldName == "RegDate")
            e.Editor.Style["direction"] = "ltr";
        else if (e.Column.FieldName == "LastExpireDate")
            e.Editor.Style["direction"] = "ltr";
        else if (e.Column.FieldName == "FileNo")
            e.Editor.Style["direction"] = "ltr";

    }

    protected void GridViewOffice_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Print":
                    ArrayList DeletedColumnsName = new ArrayList();
                    DeletedColumnsName.Add("WFState");
                    DeletedColumnsName.Add("ExpireState");
                    DeletedColumnsName.Add("MrsId");

                    Session["DeletedColumnsName"] = DeletedColumnsName;
                    Session["DataTable"] = GridViewOffice.Columns;
                    Session["DataSource"] = ObjdsOfficeDocument;
                    Session["Title"] = "مديريت پروانه اعضای حقوقی";

                    GridViewOffice.DetailRows.CollapseAllRows();
                    GridViewOffice.JSProperties["cpDoPrint"] = 1;
                    break;
            }
        }
        GridViewOffice.DataBind();
    }

    protected void GridViewOffice_PageIndexChanged(object sender, EventArgs e)
    {
        GridViewOffice.JSProperties["cpIsPostBack"] = 1;

    }
    #endregion

    #region //************DetailGrid****************************************************************************************

    protected void CustomAspxDevGridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["OfficeId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        int Index = GridViewOffice.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewOffice.FocusedRowIndex = Index;
    }

    //protected void CustomAspxDevGridViewRequest_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    //{
    //    if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "AnswerDate" || e.DataColumn.FieldName == "MFNo")
    //        e.Cell.Style["direction"] = "ltr";

    //if (e.DataColumn.FieldName == "TaskId")
    //{
    //    DevExpress.Web.ASPxGridView GridViewRequest = (DevExpress.Web.ASPxGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
    //    if (GridViewRequest != null)
    //    {
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
    //}

    protected void CustomAspxDevGridViewRequest_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate" || e.Column.FieldName == "AnswerDate" || e.Column.FieldName == "MFNo")
            e.Editor.Style["direction"] = "ltr";

    }
    #endregion

    #region Callback
    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "DoNextTaskOfClosePopUP")
        {
            WFUserControl.PerformCallback(-2, -2, -2, e);
            return;
        }
        int focucedIndex = -1;
        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow rowOff = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            int OfId = (int)rowOff["OfId"];

            TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (grid != null)
            {
                focucedIndex = grid.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = grid.GetDataRow(focucedIndex);
                    int OfReId = (int)row["OfReId"];
                    int WfCode = (int)row["WorkFlowCode"];

                    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
                    if (WfCode == (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming)
                    {
                        WFUserControl.SetMsgText("پرونده عضو حقوقی انتخاب شده توسط واحد عضویت در حال بررسی می باشد.جهت انجام عملیات از طریق واحد عضویت اقدام نمایید");
                        WFUserControl.PerformCallback(-2, -2, -2, e);
                        return;
                    }
                    WFUserControl.PerformCallback(OfReId, TableType, WfCode, e);
                    grid.DataBind();
                    GridViewOffice.DataBind();
                }
                else
                {
                    WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                    WFUserControl.PerformCallback(-2, -2, -2, e);
                    return;
                }
            }
            else
            {
                WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                WFUserControl.PerformCallback(-2, -2, -2, e);
                return;
            }
        }
        else
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            WFUserControl.PerformCallback(-2, -2, -2, e);
            return;
        }
    }

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    #endregion

    #region Methods

    private void ChangeBaseInfo(int OfReId, int OfId)
    {
        if (CheckpermisionForNewRequest())
            Response.Redirect("OfficeDocumentInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&PageMode=" + Utility.EncryptQS("ChangeBaseInfo") + "&OfReId=" + Utility.EncryptQS(OfReId.ToString()));
    }

    private void InValid(int OfReId, int OfId)
    {
        if (CheckpermisionForNewRequest())
            Response.Redirect("OfficeDocumentInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&PageMode=" + Utility.EncryptQS("InValid") + "&OfReId=" + Utility.EncryptQS(OfReId.ToString()));
    }

    protected void Delete(int OfId, int OfReId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        trans.Add(ReqManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(RequestInActivesManager);
        try
        {
            trans.BeginSave();
            ReqManager.DeleteRequest(OfReId, OfId);

            int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete(WfCode, OfReId);
            if (WorkFlowStateManager.Count > 0)
            {
                int c = WorkFlowStateManager.Count;
                for (int i = 0; i < c; i++)
                    WorkFlowStateManager[0].Delete();

                WorkFlowStateManager.Save();
            }

            RequestInActivesManager.FindByTableIdTableType(-1, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember), OfReId);
            if (RequestInActivesManager.Count > 0)
            {
                int len = RequestInActivesManager.Count;
                for (int i = 0; i < len; i++)
                {
                    RequestInActivesManager[0].Delete();
                    RequestInActivesManager.Save();
                    RequestInActivesManager.DataTable.AcceptChanges();
                }
            }

            trans.EndSave();
            GridViewOffice.DataBind();
            ShowMessage("حذف درخواست با موفقیت انجام شد");
        }
        catch (Exception err)
        {
            trans.CancelSave();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 547)
                {
                    ShowMessage("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
                }
                else
                {
                    ShowMessage("خطایی در حذف انجام گرفته است");
                }
            }
            else
            {
                ShowMessage("خطایی در حذف انجام گرفته است");
            }
        }
    }

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
            //DivReport.Visible = true;
            //LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            return (-1);
        }
    }

    #region WF
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        int TaskCode2 = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff;
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
            if (CurrentTaskCode == TaskCode || CurrentTaskCode == TaskCode2)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                if (dtWorkFlowState.Rows.Count > 0)
                {
                    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                    if (FirstTaskCode == TaskCode)
                    {
                        if (FirstNmcIdType == 0)
                        {
                            int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                            int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode2, Utility.GetCurrentUser_UserId());
                            if (Permission > 0 || Permission2 > 0)
                                return true;
                        }
                    }
                }
            }
        }
        return false;

    }

    private Boolean CheckPermitionForDelete(int TableId)
    {
        return TSP.DataManager.WorkFlowPermission.CheckWFPermissionForDeleteRequest(TableId, (int)TSP.DataManager.WorkFlows.OfficeConfirming
            , (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId);
        //TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        //int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        //int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        //int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
        //DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
        //dtState.DefaultView.RowFilter = "StateType=0";
        //if (dtState.DefaultView.Count == 1)
        //{
        //    int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
        //    int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
        //    int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
        //    int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
        //    int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());
        //    // int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;

        //    if (CurrentNmcId == FindNmcId(CurrentTaskId) && CurrentNmcIdType == 0)
        //    {
        //        if (CurrentTaskCode == TaskCode)
        //            return true;

        //    }
        //}
        //return false;

    }

    /// <summary>
    /// جهت چک کردن سطح دسترسی هنگام کلیک بر روی دکمه های درخواست
    /// </summary>
    /// <returns></returns>
    private Boolean CheckpermisionForNewRequest()
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);// (int)TSP.DataManager.TableCodes.OfficeRequest;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        TSP.DataManager.WFPermission Per = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(TaskCode, TableType, Utility.GetCurrentUser_UserId());
        if (!Per.BtnNewRequest)
        {
            ShowMessage("شما دارای سطح دسترسی گردش کار جهت ثبت درخواست جدید نمی باشید");
            return false;
        }
        return true;
    }
    #endregion

    #region SetGrid Index

    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < ObjdsOfficeDocument.SelectParameters.Count; i++)
        {
            if (ObjdsOfficeDocument.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += ObjdsOfficeDocument.SelectParameters[i].Name + "&";
                FilterString += ObjdsOfficeDocument.SelectParameters[i].DefaultValue + "&";
            }
        }
        if (FilterString.Length > 0)
            FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }

    //private void SetPageFilter()
    //{
    //    if (!IsPostBack)
    //    {
    //        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))
    //        {
    //            string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
    //            if (!string.IsNullOrEmpty(GrdFlt))
    //                GridViewOffice.FilterExpression = GrdFlt;
    //            //if (!string.IsNullOrEmpty(GrdFlt))
    //            //    GridViewOffice.FilterExpression = GrdFlt;
    //        }
    //    }

    //}

    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
                string SrchFlt = Utility.DecryptQS(Request.QueryString["SrchFlt"].ToString());

                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewOffice.FilterExpression = GrdFlt;
                if (!string.IsNullOrEmpty(SrchFlt))
                    FilterObjdsByValue(SrchFlt);
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
                if (!string.IsNullOrEmpty(Utility.DecryptQS(Request.QueryString["PostId"].ToString())))
                {
                    int PostKeyValue = int.Parse(Utility.DecryptQS(Request.QueryString["PostId"].ToString()));
                    string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());

                    GridViewOffice.DataBind();
                    Index = GridViewOffice.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewOffice.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewOffice.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewOffice.JSProperties["cpSelectedIndex"] = Index;
                        GridViewOffice.DetailRows.ExpandRow(Index);
                        GridViewOffice.FocusedRowIndex = Index;
                        GridViewOffice.JSProperties["cpIsReturn"] = 1;
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
            ObjdsOfficeDocument.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    case "MeId":
                        if (Value != "-1")
                            txtMeId.Text = Value;
                        break;
                    case "EndDateFrom":
                        txtEndDateFrom.Text = Value;
                        break;
                    case "EndDateTo":
                        txtEndDateTo.Text = Value;
                        break;
                    case "CreateDateFrom":
                        txtCreateDateFrom.Text = Value;
                        break;
                    case "CreateDateTo":
                        txtCreateDateTo.Text = Value;
                        break;
                    case "FollowCode":
                        txtFollowCode.Text = Value;
                        break;
                    case "OfName":
                        txtOfName.Text = Value;
                        break;
                    case "OfId":
                        if (Value != "-1")
                            txtOfId.Text = Value;
                        break;
                    case "FileNo":
                        txtFileNo.Text = Value;
                        break;
                    case "TaskId":
                        if (Value != "-1")
                        {
                            CmbTask.DataBind();
                            CmbTask.SelectedIndex = CmbTask.Items.FindByValue(Value).Index;
                        }
                        break;
                    case "MFType":
                        if (Value != "-1")
                        {
                            cmbMFType.DataBind();
                            cmbMFType.SelectedIndex = CmbTask.Items.FindByValue(Value).Index;
                        }
                        break;
                }
            }
        }
    }

    #endregion

    private void ShowMessage(string Message)
    {
        //   this.DivReport.Visible = true;
        this.DivReport.Attributes.Add("Style", "display:visible");
        this.LabelWarning.Text = Message;
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.OfficeDocument).ToString());
    }

    #region Search
    private void Search()
    {
        if (!string.IsNullOrEmpty(txtEndDateFrom.Text))
            ObjdsOfficeDocument.SelectParameters["EndDateFrom"].DefaultValue = txtEndDateFrom.Text;
        else
            ObjdsOfficeDocument.SelectParameters["EndDateFrom"].DefaultValue = "1";
        if (!string.IsNullOrEmpty(txtEndDateTo.Text))
            ObjdsOfficeDocument.SelectParameters["EndDateTo"].DefaultValue = txtEndDateTo.Text;
        else
            ObjdsOfficeDocument.SelectParameters["EndDateTo"].DefaultValue = "2";
        if (!string.IsNullOrEmpty(txtFollowCode.Text))
            ObjdsOfficeDocument.SelectParameters["FollowCode"].DefaultValue = txtFollowCode.Text;
        else
            ObjdsOfficeDocument.SelectParameters["FollowCode"].DefaultValue = "%";
        if (!string.IsNullOrEmpty(txtOfId.Text))
            ObjdsOfficeDocument.SelectParameters["OfId"].DefaultValue = txtOfId.Text;
        else
            ObjdsOfficeDocument.SelectParameters["OfId"].DefaultValue = "-1";
        if (!string.IsNullOrEmpty(txtOfName.Text))
            ObjdsOfficeDocument.SelectParameters["OfName"].DefaultValue = txtOfName.Text;
        else
            ObjdsOfficeDocument.SelectParameters["OfName"].DefaultValue = "%";
        if (!string.IsNullOrEmpty(txtFileNo.Text))
            ObjdsOfficeDocument.SelectParameters["FileNo"].DefaultValue = txtFileNo.Text;
        else
            ObjdsOfficeDocument.SelectParameters["FileNo"].DefaultValue = "%";
        if (!string.IsNullOrEmpty(txtMeId.Text))
            ObjdsOfficeDocument.SelectParameters["MeId"].DefaultValue = txtMeId.Text;
        else
            ObjdsOfficeDocument.SelectParameters["MeId"].DefaultValue = "-1";
        if (cmbMFType.SelectedItem != null && cmbMFType.SelectedItem.Value != null)
            ObjdsOfficeDocument.SelectParameters["MFType"].DefaultValue = cmbMFType.SelectedItem.Value.ToString();
        else
            ObjdsOfficeDocument.SelectParameters["MFType"].DefaultValue = "-1";
        if (!string.IsNullOrEmpty(txtCreateDateFrom.Text))
            ObjdsOfficeDocument.SelectParameters["CreateDateFrom"].DefaultValue = txtCreateDateFrom.Text;
        else
            ObjdsOfficeDocument.SelectParameters["CreateDateFrom"].DefaultValue = "1";
        if (!string.IsNullOrEmpty(txtCreateDateTo.Text))
            ObjdsOfficeDocument.SelectParameters["CreateDateTo"].DefaultValue = txtCreateDateTo.Text;
        else
            ObjdsOfficeDocument.SelectParameters["CreateDateTo"].DefaultValue = "2";
        if (cmbDocStatus.SelectedItem != null && cmbDocStatus.SelectedItem.Value != null)
            ObjdsOfficeDocument.SelectParameters["DocStatus"].DefaultValue = cmbDocStatus.SelectedItem.Value.ToString();
        else
            ObjdsOfficeDocument.SelectParameters["DocStatus"].DefaultValue = "-1";

        if (CmbTask.SelectedItem != null && CmbTask.SelectedItem.Value != null)
            ObjdsOfficeDocument.SelectParameters["TaskId"].DefaultValue = CmbTask.SelectedItem.Value.ToString();
        else
            ObjdsOfficeDocument.SelectParameters["TaskId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtCreateDateLastReqFrom.Text))
            ObjdsOfficeDocument.SelectParameters["CreateDateLastReqFrom"].DefaultValue = txtCreateDateLastReqFrom.Text;
        else ObjdsOfficeDocument.SelectParameters["CreateDateLastReqFrom"].DefaultValue = "1";
        if (!string.IsNullOrEmpty(txtCreateDateLastReqTo.Text))
            ObjdsOfficeDocument.SelectParameters["CreateDateLastReqTo"].DefaultValue = txtCreateDateLastReqTo.Text;
        else ObjdsOfficeDocument.SelectParameters["CreateDateLastReqTo"].DefaultValue = "2";
    }
    #endregion

    private void CheckTablePermission()
    {
        TSP.DataManager.Permission per = TSP.DataManager.OfficeManager.GetUserPermissionForOffDoc(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        btnView.Enabled = per.CanView;
        btnView2.Enabled = per.CanView;
        GridViewOffice.Visible = per.CanView;
        btnTracing.Enabled = per.CanView;
        btnTracing2.Enabled = per.CanView;
        btnDelete.Enabled = per.CanDelete;
        btnDelete2.Enabled = per.CanDelete;
    }
    #endregion
}
