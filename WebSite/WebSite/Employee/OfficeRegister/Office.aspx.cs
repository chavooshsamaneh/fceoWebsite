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

public partial class Employee_OfficeRegister_Office : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            SetHelpAddress();
            GridViewOffice.JSProperties["cpIsPostBack"] = 1;
            GridViewOffice.JSProperties["cpSelectedIndex"] = 0;
            CallbackPanelPage.JSProperties["cpReqType"] = "";
            CallbackPanelPage.JSProperties["cpReqValue"] = "";

            Session["OfficeId"] = null;

            Session["SendBackDataTable_Off"] = "";

         
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCodeList"].DefaultValue = ((int)TSP.DataManager.WorkFlows.OfficeConfirming).ToString() + "," + ((int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming).ToString();
                // ((int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming).ToString();


            CmbTask.DataBind();
            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("-----------------", null));
            CmbTask.SelectedIndex = 0;

            CheckTablePermission();
            btnNewReq.Enabled = btnNewReq1.Enabled = btnChangeBaseInfo.Enabled = btnChangeBaseInfo2.Enabled =
            btnInvalid.Enabled = btnInvalid2.Enabled = CheckWorkFlowPermissionForChangeReq();
        }
        //  Search();
        SetPageFilter();
        SetGridRowIndex();
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        ArrayList DeletedColumnsName = new ArrayList();
        DeletedColumnsName.Add("WFState");
        DeletedColumnsName.Add("MrsId");

        Session["DeletedColumnsName"] = DeletedColumnsName;
        Session["DataTable"] = GridViewOffice.Columns;
        Session["DataSource"] = ObjdsOffice;
        Session["Title"] = "اعضای حقوقی";

        String Script = @"
                        function Clear()
                        {
        txtFollowCode.SetText('');
        txtOfId.SetText('');
        txtMeId.SetText('');
        txtOfName.SetText('');
        txtFileNo.SetText('');
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
        Script += "if ( txtFollowCode.GetText('')=='' && CmbTask.GetSelectedIndex() == 0  && txtOfId.GetText('')=='' && txtMeId.GetText('')=='' && txtOfName.GetText('')=='' && txtMeId.GetText() == '' && txtFileNo.GetText() == '' && txtEndDateFrom=='' && txtEndDateTo=='' && txtCreateDateFrom=='' && txtCreateDateTo=='' && txtCreateDateLastReqFrom=='' && txtCreateDateLastReqTo=='') {return 0;} else{ return 1;}  }";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", Script, true);

    }

    #region //******************Button's Click**************************

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeInsert.aspx?OfId=" + Utility.EncryptQS("-1") + "&PageMode=" + Utility.EncryptQS("New") + "&OfReId=" + Utility.EncryptQS("-1"));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int OfId = -1;
        int OfReId = -1;
        Int16 ReType = -1;

        if (GridViewOffice.FocusedRowIndex <= -1)
        {
            ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
        if (row != null)
            OfId = (int)row["OfId"];
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }
        try
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest == null)
            {
                ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            if (GridRequest.VisibleRowCount <= 0)
            {
                ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            if (GridRequest.FocusedRowIndex == -1)
            {
                ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            OfReId = int.Parse(GridRequest.GetDataRow(GridRequest.FocusedRowIndex)["OfReId"].ToString());
            ReType = short.Parse(GridRequest.GetDataRow(GridRequest.FocusedRowIndex)["Type"].ToString());

            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
            ReqManager.FindByCode(OfReId);
            if (ReqManager[0]["IsConfirm"].ToString() != "0")
            {
                ShowMessage("امکان ویرایش اطلاعات برای درخواست پاسخ داده شده وجود ندارد");
                return;
            }
            //if (!Convert.ToBoolean(ReqManager[0]["Requester"]))
            //{
            //    ShowMessage("امکان ویرایش اطلاعات برای شما وجود ندارد");
            //    return;
            //}
            if (CheckPermitionForEdit(OfReId))
            {
                string GrdFlt = GridViewOffice.FilterExpression;
                string SearchFilterString = GenerateFilterString();
                // CallbackPanelPage.JSProperties["cpCall"] = 1;
                //CallbackPanelPage.JSProperties["cpRedirectUrl"] =
                if (IsCallback)
                    ASPxWebControl.RedirectOnCallback("OfficeInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GrdFlt)
                     + "&Dprt=" + Utility.EncryptQS("MemberShip") + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
                else
                    Response.Redirect("OfficeInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GrdFlt)
                       + "&Dprt=" + Utility.EncryptQS("MemberShip") + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
            }
            else
            {
                ShowMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار برای شما وجود ندارد");
            }

        }
        catch (Exception err)
        {
            ShowMessage("خطایی در بازخوانی اطلاعات رخ داده است");
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int OfId = -1;
        int OfReId = -1;
        Int16 ReType = -1;

        if (GridViewOffice.FocusedRowIndex <= -1)
        {
            ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
        if (row != null)
            OfId = (int)row["OfId"];
        else
        {
            this.DivReport.Visible = true;
            ShowMessage("خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.");
            return;
        }

        TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
        if (GridRequest == null)
        {
            ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        if (GridRequest.VisibleRowCount <= 0)
        {
            ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        if (GridRequest.FocusedRowIndex == -1)
        {
            ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        OfReId = int.Parse(GridRequest.GetDataRow(GridRequest.FocusedRowIndex)["OfReId"].ToString());
        ReType = short.Parse(GridRequest.GetDataRow(GridRequest.FocusedRowIndex)["Type"].ToString());
        string GrdFlt = GridViewOffice.FilterExpression;
        string SearchFilterString = GenerateFilterString();
        if (IsCallback)
            ASPxWebControl.RedirectOnCallback("OfficeInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&Mode=" + Utility.EncryptQS(ReType.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GrdFlt)
                + "&Dprt=" + Utility.EncryptQS("MemberShip") + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
        else
            Response.Redirect("OfficeInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&Mode=" + Utility.EncryptQS(ReType.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GrdFlt)
           + "&Dprt=" + Utility.EncryptQS("MemberShip") + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int OfId = -1;
        int OfReId = -1;

        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            if (row != null)
                OfId = (int)row["OfId"];
            else
            {
                ShowMessage("خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.");
                return;
            }
        }
        if (OfId == -1)
        {
            ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        }

        TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
        if (GridRequest == null)
        {
            ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        if (GridRequest.VisibleRowCount <= 0)
        {
            ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        int index0 = GridRequest.FocusedRowIndex;
        if (index0 == -1)
        {
            ShowMessage("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
        OfReId = int.Parse(GridRequest.GetDataRow(index0)["OfReId"].ToString());
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        ReqManager.FindByCode(OfReId);
        if (ReqManager.Count <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
            return;
        }
        if (!Convert.ToBoolean(ReqManager[0]["Requester"]))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان حذف برای درخواست صادر شده توسط عضو حقوقی وجود ندارد";
            return;
        }
        //////if (Convert.ToInt32(ReqManager[0]["Type"]) == (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo)//درخواست اولیه
        //////{
        //////    this.DivReport.Visible = true;
        //////    this.LabelWarning.Text = "امکان حذف درخواست اولیه ثبت نام وجود ندارد";
        //////    return;
        //////}
        if (ReqManager[0]["IsConfirm"].ToString() != "0")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان حذف برای درخواست پاسخ داده شده وجود ندارد";
            return;
        }
        if (CheckPermitionForDelete(OfReId))
            Delete(OfId, OfReId);
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان حذف درخواست در این مرحله از گردش کار برای شما وجود ندارد";
        }
    }
    #region Requests
    protected void btnInvalid_Click(object sender, EventArgs e)
    {
        int OfId = CheckRequestCondition("Invalid");
        if (OfId == -1)
            return;
        string SearchFilterString = GenerateFilterString();
        Response.Redirect("OfficeInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString())
            + "&PageMode=" + Utility.EncryptQS("InValid") + "&OfReId=" + Utility.EncryptQS("-1")
            + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
    }

    protected void btnNewReq_Click(object sender, EventArgs e)
    {
        #region Comment
        //int OfId = -1;
        //if (GridViewOffice.FocusedRowIndex > -1)
        //{
        //    DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
        //    OfId = (int)row["OfId"];
        //}
        //if (OfId == -1)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "ابتدا یک شرکت را انتخاب نمائید";
        //    return;
        //}
        //TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();

        //ReqManager.FindByOfficeId(OfId, 0, (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo);
        //if (ReqManager.Count > 0)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "به دلیل عدم پاسخ درخواست ثبت نام اولیه, امکان درخواست تغییرات وجود ندارد";
        //    return;
        //}

        //ReqManager.FindByOfficeId(OfId, 0, -1);
        //if (ReqManager.Count > 0)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده,امکان ثبت درخواست جدید وجود ندارد";
        //    return;
        //}
        #endregion

        int OfId = CheckRequestCondition("NewReq");
        if (OfId == -1)
            return;
        if (IsCallback)
            ASPxWebControl.RedirectOnCallback("OfficeInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&PageMode=" + Utility.EncryptQS("NewReqMembership") + "&OfReId=" + Utility.EncryptQS("-1")
                      + "&Dprt=" + Utility.EncryptQS("MemberShip"));
        else
            Response.Redirect("OfficeInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&PageMode=" + Utility.EncryptQS("NewReqMembership") + "&OfReId=" + Utility.EncryptQS("-1")
                        + "&Dprt=" + Utility.EncryptQS("MemberShip"));
    }

    protected void btnChangeBaseInfo_Click(object sender, EventArgs e)
    {

        int OfId = CheckRequestCondition("ChangeBaseInfo");
        if (OfId == -1)
            return;
        ChangeBaseInfo(OfId);

        #region Comment
        //TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
        //if (GridRequest == null)
        //{
        //    return;
        //}
        //if (GridRequest.VisibleRowCount <= 0)
        //{
        //    return;
        //}
        //int index0 = GridRequest.FocusedRowIndex;
        //int OfReId = -1;
        //if (index0 == -1)
        //{
        //    return;
        //}
        //OfReId = int.Parse(GridRequest.GetDataRow(index0)["OfReId"].ToString());
        //if (OfReId == -1)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
        //}
        //ReqManager.FindByCode(OfReId);
        //if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
        //{
        //    int MFType = Convert.ToInt32(ReqManager[0]["MFType"]);
        //    if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//طراح و ناظر
        //    {
        //        ReqManager.FindByOfficeId(OfId, -1, (int)TSP.DataManager.OfficeRequestType.SaveFileDocument);
        //        if (ReqManager.Count == 0)
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "امکان درخواست تغییرات پایه وجود ندارد.برای عضو مورد نظر پروانه صادر نشده است";
        //            return;
        //        }
        //        else if (ReqManager[0]["IsConfirm"].ToString() != "1")
        //        {

        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "امکان درخواست تغییرات پایه وجود ندارد.پروانه عضو مورد نظر تایید نشده است";
        //            return;
        //        }
        //    }
        //}
        #endregion

    }


    protected void btnChangeMambers_Click(object sender, EventArgs e)
    {
        int OfId = CheckRequestCondition("ChangeShareHolderAndBaseInfo");
        if (OfId == -1)
            return;
        if (CheckWorkFlowPermissionForChangeReq())
            Response.Redirect("OfficeInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&PageMode=" + Utility.EncryptQS("ChangeShareHolderAndBaseInfo") + "&OfReId=" + Utility.EncryptQS("-1"));
    }
    #endregion
    protected void btnResetSave_Click(object sender, EventArgs e)
    {
        int OfId = -1;
        string RsType = "";

        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            if (row != null)
                OfId = (int)row["OfId"];
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                return;
            }
            RsType = ((int)TSP.DataManager.ResetPasswordType.Office).ToString();

        }
        if (OfId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است";
            return;
        }
        else
            Response.Redirect("~/Users/ResetPassword.aspx?ID=" + Utility.EncryptQS(OfId.ToString()) + "&Type=" + Utility.EncryptQS(RsType));

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Office";
        GridViewExporter.WriteXlsxToResponse(true);
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        //CallbackPanelPage.JSProperties["cpCall"] = 0;
        //CallbackPanelPage.JSProperties["cpDoPrint"] = 0;
        int focucedIndex = -1;
        string GridFilterString = GridViewOffice.FilterExpression;
        string SearchFilterString = GenerateFilterString();

        if (GridViewOffice.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)GridViewOffice.FindDetailRowTemplateControl(GridViewOffice.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (grid != null)
            {
                focucedIndex = grid.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = grid.GetDataRow(focucedIndex);
                    int TableId = (int)row["OfReId"];
                    int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;

                    String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString) +
                    "&PostId=" + Utility.EncryptQS(TableId.ToString());


                    //CallbackPanelPage.JSProperties["cpCall"] = 1;
                    //CallbackPanelPage.JSProperties["cpRedirectUrl"] = "../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString());
                    DevExpress.Web.ASPxWebControl.RedirectOnCallback("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) +
                        "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " لطفاً ابتدا یک درخواست را انتخاب نمائید";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " لطفاً ابتدا یک درخواست را انتخاب نمائید";
                return;
            }

        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید ";
            return;

        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();

    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    #endregion

    #region  /************DetailGrid************************************************/
    protected void CustomAspxDevGridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["OfficeId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        int Index = GridViewOffice.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewOffice.FocusedRowIndex = Index;
    }

    protected void CustomAspxDevGridViewRequest_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "AnswerDate" || e.DataColumn.FieldName == "MFNo")
            e.Cell.Style["direction"] = "ltr";
    }

    protected void CustomAspxDevGridViewRequest_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate" || e.Column.FieldName == "AnswerDate" || e.Column.FieldName == "MFNo")
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

    #region //************Main Grid************************************************//

    protected void GridViewOffice_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
    {
        GridViewOffice.JSProperties["cpIsPostBack"] = 1;
    }

    protected void GridViewOffice_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void GridViewOffice_PageIndexChanged(object sender, EventArgs e)
    {
        GridViewOffice.JSProperties["cpIsPostBack"] = 1;
    }

    protected void GridViewOffice_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewOffice_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        //if (!Utility.IsDBNullOrNullValue(e.GetValue("IsConfirm")) && !Utility.IsDBNullOrNullValue(e.GetValue("Type")))//&& e.GetValue("Type") != null)
        //{
        //    if (e.GetValue("IsConfirm").ToString() == "0" && ((Convert.ToInt32(e.GetValue("Type")) == (int)TSP.DataManager.OfficeRequestType.SaveRequestInfo)
        //        || (Convert.ToInt32(e.GetValue("Type")) == (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset)
        //        || (Convert.ToInt32(e.GetValue("Type")) == (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo)))
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
                case (int)TSP.DataManager.OfficeRequestType.SaveMembershipRequset:
                    e.Row.ForeColor = System.Drawing.Color.Olive;
                    break;
                case (int)TSP.DataManager.OfficeRequestType.Change:
                    e.Row.ForeColor = System.Drawing.Color.DarkBlue;
                    break;
                case (int)TSP.DataManager.OfficeRequestType.Invalid:
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    break;
                case (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo:
                    e.Row.ForeColor = System.Drawing.Color.DeepPink;
                    break;
            }
        }

        if (!Utility.IsDBNullOrNullValue(e.GetValue("MrsId")))
        {
            switch ((Convert.ToInt32(e.GetValue("MrsId"))))
            {
                case (int)TSP.DataManager.MembershipRegistrationStatus.Cancel:
                    e.Row.ForeColor = System.Drawing.Color.Brown;
                    break;
            }
        }
    }

    //protected void GridViewOffice_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    //{
    //    if (!string.IsNullOrEmpty(e.Parameters))
    //    {
    //        switch (e.Parameters)
    //        {
    //            case "Search":
    //                break;
    //            case "Clear":
    //                Clear();
    //                break;
    //        }
    //    }
    //    GridViewOffice.DataBind();
    //}
    #endregion

    #region//****************************CallBacks*******************************************************
    protected void CallbackPanelPage_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "btnTracing")
        {
            btnTracing_Click(null, null);
        }
        else if (e.Parameter == "btnNewReq")
        {
            btnNewReq_Click(null, null);
        }
        else if (e.Parameter == "btnView")
        {
            btnView_Click(null, null);
        }
        else if (e.Parameter == "btnEdit")
        {
            btnEdit_Click(null, null);
        }
        else if (e.Parameter == "btnDelete")
        {
            btnDelete_Click(null, null);
        }
        else if (e.Parameter == "Print")
        {
            GridViewOffice.DetailRows.CollapseAllRows();
            CallbackPanelPage.JSProperties["cpDoPrint"] = 1;
        }

        CallbackPanelPage.JSProperties["cpReqType"] = "";
        CallbackPanelPage.JSProperties["cpReqValue"] = "";

        if (e.Parameter == "PrintCard")
        {

            int OfId = -1;

            if (GridViewOffice.FocusedRowIndex > -1)
            {
                DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
                if (row != null)
                    OfId = (int)row["OfId"];
                else
                {
                    CallbackPanelPage.JSProperties["cpReqType"] = "Message";
                    CallbackPanelPage.JSProperties["cpReqValue"] = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                    return;
                }

            }
            if (OfId == -1)
            {
                CallbackPanelPage.JSProperties["cpReqType"] = "Message";
                CallbackPanelPage.JSProperties["cpReqValue"] = "ردیفی انتخاب نشده است";
                return;
            }
            else
            {
                CallbackPanelPage.JSProperties["cpReqType"] = e.Parameter;
                CallbackPanelPage.JSProperties["cpReqValue"] = "../../ReportForms/OfficeTemporaryCard.aspx?OfId=" + Utility.EncryptQS(OfId.ToString());
            }
        }
    }

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
                    int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;

                    if (WfCode == (int)TSP.DataManager.WorkFlows.OfficeConfirming)
                    {
                        WFUserControl.SetMsgText("پرونده عضو حقوقی انتخاب شده توسط واحد پروانه در حال بررسی می باشد.جهت انجام عملیات از طریق واحد پروانه اقدام نمایید");
                        WFUserControl.PerformCallback(-2, -2, -2, e);
                        return;
                    }

                    WFUserControl.PerformCallback(OfReId, TableType, WfCode, e);
                    grid.DataBind();
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
    #endregion

    #region Methods
    private int CheckRequestCondition(string Type)
    {
        int OfId = -1;
        if (GridViewOffice.FocusedRowIndex > -1)
        {
            DataRow row = GridViewOffice.GetDataRow(GridViewOffice.FocusedRowIndex);
            if (row != null)
                OfId = (int)row["OfId"];
            else
            {
                ShowMessage("خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.");
                return -1;
            }
        }
        if (OfId == -1)
        {
            ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return -1;
        }

        if (Type == "ChangeBaseInfo" || Type == "ChangeShareHolderAndBaseInfo")
        {
            TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
            OffManager.FindByCode(OfId);
            if (OffManager.Count <= 0)
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                return -1;
            }

            if (Convert.ToInt32(OffManager[0]["MrsId"]) != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed
         && Convert.ToInt32(OffManager[0]["MrsId"]) != (int)TSP.DataManager.MembershipRegistrationStatus.ConditionalApproval)
            {
                ShowMessage("امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.");
                return -1;
            }
        }
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        ReqManager.FindByOfficeId(OfId, 0, -1);
        if (ReqManager.Count > 0)
        {
            ShowMessage("به دلیل وجود درخواست بررسی نشده در واحد عضویت و یا پروانه اشتغال برای شرکت انتخاب شده، امکان ثبت درخواست جدید وجود ندارد");
            return -1;
        }
        return OfId;
    }

    #region WF

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.WFPermission WFPermission = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo, (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming, TableId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPermissionMembershipUnitConfirmingOffice = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming, TableId, Utility.GetCurrentUser_UserId());
        if (WFPermissionMembershipUnitConfirmingOffice.BtnEdit || WFPermission.BtnEdit)
            return true;
        else
            return false;
        //return WFPermission.BtnEdit;
        #region Comment
        //TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //int TaskOrder = -1;
        //int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        //WorkFlowTaskManager.FindByTaskCode(TaskCode);
        //if (WorkFlowTaskManager.Count > 0)
        //{
        //    TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        //    if (TaskOrder != 0)
        //    {
        //        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
        //        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        //        if (dtWorkFlowLastState.Rows.Count > 0)
        //        {
        //            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
        //            if (CurrentTaskCode == TaskCode)
        //            {
        //DataTable dtWorkFlowState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
        //if (dtWorkFlowState.Rows.Count > 0)
        //{
        //    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());

        //    if (FirstTaskCode == TaskCode)
        //    {
        //int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
        //if (Permission > 0)
        //    return true;
        //        }
        //    }
        //            }
        //        }
        //    }
        //}
        //return false;
        #endregion
    }

    private Boolean CheckPermitionForDelete(int TableId)
    {
        return TSP.DataManager.WorkFlowPermission.CheckWFPermissionForDeleteRequest(TableId, (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming, (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId);
        #region Comment
        //  TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        //int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        //int WfCode = ;
        //DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
        //dtState.DefaultView.RowFilter = "StateType=0";
        //if (dtState.DefaultView.Count > 0)
        //{
        //    dtState.DefaultView.RowFilter = "StateType=" + ((int)TSP.DataManager.WorkFlowStateType.SendDocToNextStep).ToString();
        //    if (dtState.DefaultView.Count == 1)
        //    {
        //        int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
        //        int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
        //        int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
        //        int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
        //        int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());
        //        int CurrentUserId = int.Parse(dtState.Rows[0]["UserId"].ToString());

        //        if (CurrentUserId == Utility.GetCurrentUser_UserId())
        //        {
        //            if (CurrentTaskCode == TaskCode)
        //                return true;
        //        }
        //    }
        //}
        //return false;
        #endregion
    }

    /// <summary>
    /// جهت تنظیم سطح دسترسی دکمه های درخواست ها
    /// </summary>
    /// <returns></returns>
    private Boolean CheckWorkFlowPermissionForChangeReq()
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Office);
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId());
        return (WFPer.BtnNewRequest);
    }
    #endregion

    #region GridIndex
    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < ObjdsOffice.SelectParameters.Count; i++)
        {
            if (ObjdsOffice.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += ObjdsOffice.SelectParameters[i].Name + "&";
                FilterString += ObjdsOffice.SelectParameters[i].DefaultValue + "&";
            }
        }
        if (FilterString.Length > 0)
            FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }

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
                    GridViewOffice.FilterExpression = GrdFlt;
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
            ObjdsOffice.SelectParameters[ParameterName].DefaultValue = Value;
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
                }
            }
        }
    }
    #endregion

    #region Requests
    //private void Change(int OfReId, int OfId)
    //{
    //   // TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
    //    TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    WorkFlowTaskManager.ClearBeforeFill = true;
    //    ReqManager.ClearBeforeFill = true;

    //    //TransactionManager.Add(WorkFlowStateManager);
    //    //TransactionManager.Add(ReqManager);

    //    try
    //    {

    //        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
    //        DataTable dtWfState = WorkFlowStateManager.SelectLastState(TableType, OfReId);
    //        if (dtWfState.Rows.Count > 0)
    //        {
    //            int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
    //            int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectDocumentOfOfficeAndEndProcess;
    //            int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfOfficeAndEndProccess;
    //            int DocumentUnitConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff;

    //            int RejectTaskId = -1;
    //            int ConfirmTaskId = -1;
    //            int DocumentUnitConfirmTaskId = -1;

    //            WorkFlowTaskManager.FindByTaskCode(DocumentUnitConfirmTaskCode);
    //            if (WorkFlowTaskManager.Count > 0)
    //            {
    //                DocumentUnitConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
    //            }


    //            WorkFlowTaskManager.FindByTaskCode(RejectTaskCode);
    //            if (WorkFlowTaskManager.Count > 0)
    //            {
    //                RejectTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
    //            }

    //            WorkFlowTaskManager.FindByTaskCode(ConfirmTaskCode);
    //            if (WorkFlowTaskManager.Count > 0)
    //            {
    //                ConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
    //            }

    //            if (CurrentTaskId == RejectTaskId || CurrentTaskId == ConfirmTaskId)
    //            {
    //                string SearchFilterString = GenerateFilterString();
    //                ReqManager.FindByCode(OfReId);
    //                if (ReqManager.Count == 1)
    //                {
    //                    if (ReqManager[0]["IsConfirm"].ToString() != "0")
    //                    {
    //                        Response.Redirect("OfficeInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Change") + "&OfReId=" + Utility.EncryptQS(OfReId.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));//+ "&Mode=" + Utility.EncryptQS("")

    //                    }
    //                    else
    //                    {
    //                        this.DivReport.Visible = true;
    //                        this.LabelWarning.Text = "امکان درخواست تغییرات برای پروانه تایید نشده وجود ندارد.";
    //                    }
    //                }
    //                else
    //                {
    //                    this.DivReport.Visible = true;
    //                    this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
    //                }
    //            }
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "به دلیل به پایان نرسیدن گردش کار پروانه انتخاب شده امکان درخواست تغییرات وجود ندارد.";
    //            }

    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "برای پرونده انتخاب شده گردش کار تعریف نشده است.";
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
    //    }
    //}

    private void ChangeBaseInfo(int OfId)
    {
        if (CheckWorkFlowPermissionForChangeReq())
            Response.Redirect("OfficeInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&PageMode=" + Utility.EncryptQS("ChangeBaseInfo") + "&OfReId=" + Utility.EncryptQS("-1"));
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
            int ReqInCount = RequestInActivesManager.Count;
            if (ReqInCount > 0)
            {
                for (int i = 0; i < ReqInCount; i++)
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
            Utility.SaveWebsiteError(err);
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

    private void InValid(int OfReId, int OfId)
    {
        //TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //WorkFlowTaskManager.ClearBeforeFill = true;
        //ReqManager.ClearBeforeFill = true;

        //TransactionManager.Add(WorkFlowStateManager);
        //TransactionManager.Add(ReqManager);

        try
        {
            int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
            DataTable dtWfState = WorkFlowStateManager.SelectLastState(TableType, OfReId);
            if (dtWfState.Rows.Count <= 0)
            {
                ShowMessage("برای پرونده انتخاب شده گردش کار تعریف نشده است.");
                return;
            }
            int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
            int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectDocumentOfOfficeAndEndProcess;
            int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfOfficeAndEndProccess;
            int DocumentUnitConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff;

            int RejectTaskId = -1;
            int ConfirmTaskId = -1;
            //int DocumentUnitConfirmTaskId = -1;

            //WorkFlowTaskManager.FindByTaskCode(DocumentUnitConfirmTaskCode);
            //if (WorkFlowTaskManager.Count > 0)
            //{
            //    DocumentUnitConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            //}

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
                ReqManager.FindByCode(OfReId);
                if (ReqManager.Count == 1)
                {
                    string SearchFilterString = GenerateFilterString();
                    Response.Redirect("OfficeInsert.aspx?OfId=" + Utility.EncryptQS(OfId.ToString())
                        + "&PageMode=" + Utility.EncryptQS("InValid") + "&OfReId=" + Utility.EncryptQS(OfReId.ToString())
                        + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));//+ "&Mode=" + Utility.EncryptQS("")
                }
                else
                {
                    ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                }
            }
            else
            {
                ShowMessage("به دلیل به پایان نرسیدن گردش کار پروانه انتخاب شده امکان درخواست ابطال وجود ندارد.");
            }
        }
        catch (Exception err)
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            Utility.SaveWebsiteError(err);
        }
    }
    #endregion

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.Office).ToString());
    }

    private void Search()
    {
        if (!string.IsNullOrEmpty(txtEndDateFrom.Text))
            ObjdsOffice.SelectParameters["EndDateFrom"].DefaultValue = txtEndDateFrom.Text;
        else ObjdsOffice.SelectParameters["EndDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtEndDateTo.Text))
            ObjdsOffice.SelectParameters["EndDateTo"].DefaultValue = txtEndDateTo.Text;
        else ObjdsOffice.SelectParameters["EndDateTo"].DefaultValue = "2";

        if (!string.IsNullOrEmpty(txtFollowCode.Text))
            ObjdsOffice.SelectParameters["FollowCode"].DefaultValue = txtFollowCode.Text;
        else ObjdsOffice.SelectParameters["FollowCode"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtOfId.Text))
            ObjdsOffice.SelectParameters["OfId"].DefaultValue = txtOfId.Text;
        else ObjdsOffice.SelectParameters["OfId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtOfName.Text))
            ObjdsOffice.SelectParameters["OfName"].DefaultValue = txtOfName.Text;
        else ObjdsOffice.SelectParameters["OfName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtFileNo.Text))
            ObjdsOffice.SelectParameters["FileNo"].DefaultValue = txtFileNo.Text;
        else ObjdsOffice.SelectParameters["FileNo"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtMeId.Text))
            ObjdsOffice.SelectParameters["MeId"].DefaultValue = txtMeId.Text;
        else ObjdsOffice.SelectParameters["MeId"].DefaultValue = "-1";

        if (CmbTask.SelectedItem != null && CmbTask.SelectedItem.Value != null)
            ObjdsOffice.SelectParameters["TaskId"].DefaultValue = CmbTask.SelectedItem.Value.ToString();
        else
            ObjdsOffice.SelectParameters["TaskId"].DefaultValue = "-1";
        if (!string.IsNullOrEmpty(txtCreateDateFrom.Text))
            ObjdsOffice.SelectParameters["CreateDateFrom"].DefaultValue = txtCreateDateFrom.Text;
        else ObjdsOffice.SelectParameters["CreateDateFrom"].DefaultValue = "1";
        if (!string.IsNullOrEmpty(txtCreateDateTo.Text))
            ObjdsOffice.SelectParameters["CreateDateTo"].DefaultValue = txtCreateDateTo.Text;
        else ObjdsOffice.SelectParameters["CreateDateTo"].DefaultValue = "2";


        if (!string.IsNullOrEmpty(txtCreateDateLastReqFrom.Text))
            ObjdsOffice.SelectParameters["CreateDateLastReqFrom"].DefaultValue = txtCreateDateLastReqFrom.Text;
        else ObjdsOffice.SelectParameters["CreateDateLastReqFrom"].DefaultValue = "1";
        if (!string.IsNullOrEmpty(txtCreateDateLastReqTo.Text))
            ObjdsOffice.SelectParameters["CreateDateLastReqTo"].DefaultValue = txtCreateDateLastReqTo.Text;
        else ObjdsOffice.SelectParameters["CreateDateLastReqTo"].DefaultValue = "2";
        GridViewOffice.DataBind();
    }

    //private void Clear()
    //{
    //    ObjdsOffice.SelectParameters["CreateDateFrom"].DefaultValue = "1";
    //    ObjdsOffice.SelectParameters["CreateDateTo"].DefaultValue = "2";
    //    ObjdsOffice.SelectParameters["EndDateFrom"].DefaultValue = "1";
    //    ObjdsOffice.SelectParameters["EndDateTo"].DefaultValue = "2";
    //    ObjdsOffice.SelectParameters["FollowCode"].DefaultValue = "%";
    //    ObjdsOffice.SelectParameters["OfId"].DefaultValue = "-1";
    //    ObjdsOffice.SelectParameters["MeId"].DefaultValue = "-1";
    //    ObjdsOffice.SelectParameters["OfName"].DefaultValue = "%";
    //    ObjdsOffice.SelectParameters["FileNo"].DefaultValue = "%";
    //    GridViewOffice.DataBind();
    //}

    private void CheckTablePermission()
    {
        TSP.DataManager.Permission per = TSP.DataManager.OfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        BtnNew.Enabled = per.CanNew;
        BtnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        btnView.Enabled = per.CanView;
        btnView2.Enabled = per.CanView;
        GridViewOffice.ClientVisible = per.CanView;
        btnTracing.Enabled = per.CanView;
        btnTracing2.Enabled = per.CanView;
        btnReset.Enabled = per.CanNew;
        btnReset1.Enabled = per.CanNew;
        btnPrint.Enabled = per.CanView;
        btnPrint2.Enabled = per.CanView;
        btnExportExcel.Enabled = per.CanView;
        btnExportExcel2.Enabled = per.CanView;
        btnDelete.Enabled = per.CanDelete;
        btnDelete2.Enabled = per.CanDelete;
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion

}
