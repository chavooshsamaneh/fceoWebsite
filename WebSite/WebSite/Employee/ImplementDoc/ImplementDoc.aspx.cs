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

public partial class Employee_ImplementDoc_ImplementDoc : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetHelpAddress();
            Session["SendBackDataTable_EmpImpDoc"] = "";

            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionImp(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnReDuplicate.Enabled = btnReDuplicate2.Enabled
                = btnRevival.Enabled = btnRevival2.Enabled
                = btnChange.Enabled = btnChange2.Enabled = CheckWorkFlowPermissionForChangeReq();

            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming).ToString();
        }

        SetPageFilter();
        SetGridRowIndex();

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["btnDelete"];
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        //************کد های خالی کردن صفحه را نوشتم تا سر وقت کار های منیجر را هم بکنم و صفحه خالی بشه-ایلناز******************************
        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtEndDateFrom = document.getElementById('" + txtEndDateFrom.ClientID + "').value;";
        script += "var txtEndDateTo = document.getElementById('" + txtEndDateTo.ClientID + "').value;";
        script += "var txtCreateDateLastReqFrom = document.getElementById('" + txtCreateDateLastReqFrom.ClientID + "').value;";
        script += "var txtCreateDateLastReqTo = document.getElementById('" + txtCreateDateLastReqTo.ClientID + "').value;";
        script += "if ( txtEndDateTo=='' && txtEndDateFrom=='' && txtMeId.GetText() == '' && txtFollowCode.GetText() == '' && txtCreateDateLastReqTo=='' && txtCreateDateLastReqFrom=='') return 0; else return 1;  }</SCRIPT>";
        //&& CmbTask.GetSelectedIndex() == -1 

        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
            txtMeId.SetText(''); txtFollowCode.SetText('');
            document.getElementById('" + txtCreateDateLastReqFrom.ClientID + "').value = '';  document.getElementById('" + txtCreateDateLastReqTo.ClientID + "').value = '';document.getElementById('" + txtEndDateFrom.ClientID + "').value = ''; document.getElementById('" + txtEndDateTo.ClientID + "').value = '';}</SCRIPT>";
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        int MfId = -1;
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            MfId = (int)row["MfId"];
        }
        switch (e.Item.Name)
        {
            case "View":
                if (MfId == -1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

                }
                else
                {
                    hfMfId.Value = Utility.EncryptQS(MfId.ToString());
                    //Session["MfId"] = MfId;
                    hfPageMode.Value = Utility.EncryptQS("View");
                    //Session["PageMode"] = "View";
                    Response.Redirect("AddMemberFile.aspx?MeId=" + hfMeId.Value + "&MfId=" + hfMfId.Value + "&PageMode=" + hfPageMode.Value);
                }
                break;



            case "New":
                hfMfId.Value = null;
                //Session["MfId"] = null;
                hfPageMode.Value = Utility.EncryptQS("New");
                //Session["PageMode"] = "New";
                Response.Redirect("AddMemberFile.aspx?MeId=" + hfMeId.Value + "&MfId=" + hfMfId.Value + "&PageMode=" + hfPageMode.Value);
                break;



            case "Edit":
                if (MfId == -1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

                }
                else
                {
                    Utility.EncryptQS(MfId.ToString());
                    hfPageMode.Value = Utility.EncryptQS("Edit");
                    //Session["PageMode"] = "Edit";
                    Response.Redirect("AddMemberFile.aspx?MeId=" + hfMeId.Value + "&MfId=" + hfMfId.Value + "&PageMode=" + hfPageMode.Value);
                }
                break;



            case "ReNew":
                if (MfId == -1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

                }
                else
                {
                    Utility.EncryptQS(MfId.ToString());
                    hfPageMode.Value = Utility.EncryptQS("ReNew");
                    //Session["PageMode"] = "ReNew";
                    Response.Redirect("AddMemberFile.aspx?MeId=" + hfMeId.Value + "&MfId=" + hfMfId.Value + "&PageMode=" + hfPageMode.Value);
                }
                break;



            case "Improve":
                if (MfId == -1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

                }
                else
                {
                    Utility.EncryptQS(MfId.ToString());
                    hfPageMode.Value = Utility.EncryptQS("Improve");
                    //Session["PageMode"] = "Improve";
                    Response.Redirect("AddMemberFile.aspx?MeId=" + hfMeId.Value + "&MfId=" + hfMfId.Value + "&PageMode=" + hfPageMode.Value);
                }
                break;


            case "Delete":
                break;

            //case "Save":
            //    break;

            case "Search":
                break;

            //case "Enabled":
            //    if (MfId == -1)
            //    {
            //        this.DivReport.Visible = true;
            //        this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

            //    }
            //    else
            //    {
            //        Session["MfId"] = MfId;
            //        Session["PageMede"] = "Enable";
            //    }

            //    break;

            //case "Disable":


            //    break;

            case "Back":
                break;

            //case "Clear":
            //    break;


            default:
                break;

        }

    }
    //***********************************************************************************************************************
    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            if (MeFileRow != null)
            {
                int MfId = int.Parse(MeFileRow["MfId"].ToString());
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DocMemberFileManager.ClearBeforeFill = true;
                DataTable dtImpDoc = DocMemberFileManager.SelectImplementDoc(-1, MfId);
                if (dtImpDoc.Rows.Count == 1)
                {
                    int MeFileId = int.Parse(dtImpDoc.Rows[0]["MeFileId"].ToString());
                    int MeId = int.Parse(dtImpDoc.Rows[0]["MemberId"].ToString());
                    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                    MemberManager.FindByCode(MeId);
                    if (MemberManager.Count == 1)
                    {
                        int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
                        if (MRsId == 1)
                        {
                            DataTable dtDocMeFile = DocMemberFileManager.SelectImpDocLastVersionByMeFileId(MeFileId);
                            if (dtDocMeFile.Rows.Count > 0)
                            {
                                int LastMfId = (int)dtDocMeFile.Rows[0]["MfId"];
                                if (CheckPermitionForEdit(LastMfId))
                                {
                                    NextPage("Edit");
                                }
                                else
                                {
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "شما در این مرحله از گردش کار قادر به ویرایش اطلاعات نیستید.";
                                }
                            }
                            else
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "بدلیل نامشخص بودن وضعیت پروانه امکان ویرایش اطلاعات وجود ندارد.";
                            }

                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعت مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }
            }
        }
    }

    protected void btnReNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.DocMemberFileManager MeFileManager = new TSP.DataManager.DocMemberFileManager();
        int MeId = -1;
        int FileNo = -1;
        int MfId = -1;
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow row = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            MfId = (int)row["MfId"];
            MeId = (int)row["MfId"];
            FileNo = (int)row["FileNo"];
        }
        if (MfId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای تمدید ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            MeFileManager.SelectLastMemberFile(MeId, FileNo);
            if (MeFileManager.Count == 1)
            {
                if (int.Parse(MeFileManager[0]["MfId"].ToString()) == MfId)
                {
                    hfMfId.Value = Utility.EncryptQS(MfId.ToString());
                    hfPageMode.Value = Utility.EncryptQS("ReNew");
                    //Session["PageMode"] = "ReNew";
                    Response.Redirect("AddMemberFile.aspx?MeId=" + hfMeId.Value + "&MfId=" + hfMfId.Value + "&PageMode=" + hfPageMode.Value);
                    //Response.Redirect("AddEngOffice.aspx?EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان تمدید اطلاعات وجود ندارد. لطفاً آخرین رکورد مربوط به شخص مورد نظر را انتخاب نمائید.";
                }

            }

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    protected void GridViewMemberFile_FocusedRowChanged(object sender, EventArgs e)
    {

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        int PostId = int.Parse(GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex)["MfId"].ToString());
        string GridFilterString = GridViewMemberFile.FilterExpression;
        string SearchFilterString = GenerateFilterString();
        int focucedIndex = -1;
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewMeFileHistory = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
            if (GridViewMeFileHistory != null)
            {
                focucedIndex = GridViewMeFileHistory.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = GridViewMeFileHistory.GetDataRow(focucedIndex);
                    //***ImpDocId
                    int TableId = (int)row["MfId"];
                    //***MfId
                    // MeId = (int)row["MeId"];
                    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileImp;
                    int WorkFlowCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;

                    String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString) +
                 "&PostId=" + Utility.EncryptQS(PostId.ToString());
                    Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                return;
            }
        }
        //if (GridViewMemberFile.FocusedRowIndex > -1)
        //{
        //    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileImp;
        //    DataRow DocMeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        //    int TableId = int.Parse(DocMeFileRow["MfId"].ToString());
        //    int WorkFlowCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        //    Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()));
        //}
        //else
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        //}
    }

    protected void btnReDuplicate_Click(object sender, EventArgs e)
    {
        if (CheckRequest("ReDuplicate"))
            NextPage("ReDuplicate");
        //    if (GridViewMemberFile.FocusedRowIndex > -1)
        //    {
        //        DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        //        if (MeFileRow != null)
        //        {
        //            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //            int MemberFileId = int.Parse(MeFileRow["MeId"].ToString());
        //            DocMemberFileManager.FindByCode(MemberFileId, 0);
        //            int MeId = -1;
        //            if (DocMemberFileManager.Count == 1)
        //            {
        //                MeId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
        //            }
        //            else
        //            {
        //                this.DivReport.Visible = true;
        //                this.LabelWarning.Text = "بدلیل نامشخص بودن وضعیت پروانه اشتغال به کار امکان صدور المثنی وجود ندارد.";
        //                return;
        //            }
        //            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        //            MemberManager.FindByCode(MeId);
        //            if (MemberManager.Count == 1)
        //            {
        //                int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
        //                if (MRsId == 1)
        //                {

        //                    DataTable dtDocMeFile = DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
        //                    if (dtDocMeFile.Rows.Count > 0)
        //                    {
        //                        int MfId = (int)dtDocMeFile.Rows[0]["MfId"];
        //                        ReDuplicate(MfId, MemberFileId);
        //                    }
        //                    else
        //                    {
        //                        ShowMessage("بدلیل نامشخص بودن وضعیت/غیر فعال و یا عدم تایید مجوز امکان صدور المثنی وجود ندارد.");
        //                    }

        //                }
        //                else
        //                {
        //                    this.DivReport.Visible = true;
        //                    this.LabelWarning.Text = "امکان صدور المثنی وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.";
        //                }
        //            }
        //            else
        //            {
        //                this.DivReport.Visible = true;
        //                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
        //            }
        //        }
        //    }
    }

    protected void btnRevival_Click(object sender, EventArgs e)
    {
        if (CheckRequest("Revival"))
            NextPage("Revival");
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        if (CheckRequest("Change"))
            NextPage("Change");
    }

    protected void btnInValid_Click(object sender, EventArgs e)
    {
        if (CheckRequest("InValid"))
            NextPage("InValid");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            if (MeFileRow != null)
            {
                int MfId = int.Parse(MeFileRow["MfId"].ToString());
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DocMemberFileManager.ClearBeforeFill = true;
                DataTable dtImpDoc = DocMemberFileManager.SelectImplementDoc(-1, MfId);
                if (dtImpDoc.Rows.Count == 1)
                {
                    int MeFileId = int.Parse(dtImpDoc.Rows[0]["MeFileId"].ToString());
                    int MeId = int.Parse(dtImpDoc.Rows[0]["MemberId"].ToString());
                    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                    MemberManager.FindByCode(MeId);
                    if (MemberManager.Count == 1)
                    {
                        int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
                        if (MRsId == 1)
                        {
                            DataTable dtDocMeFile = DocMemberFileManager.SelectImpDocLastVersionByMeFileId(MeFileId);
                            if (dtDocMeFile.Rows.Count > 0)
                            {
                                int LastMfId = (int)dtDocMeFile.Rows[0]["MfId"];
                                if (CheckPermitionForDelete(LastMfId))
                                {
                                    DeleteRequest(LastMfId);
                                }
                                else
                                {
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "شما در این مرحله از گردش کار قادر به ویرایش اطلاعات نیستید.";
                                }
                            }
                            else
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "بدلیل نامشخص بودن وضعیت پروانه امکان ویرایش اطلاعات وجود ندارد.";
                            }

                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ویرایش وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعت مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }
            }
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "ImplementDocument";
        GridViewExporter.WriteXlsToResponse(true);
    }

    //protected void btnActivate_Click(object sender, EventArgs e)
    //{
    //    if (CheckRequest("Active"))
    //        NextPage("Active");      
    //}
    //*********************************************************************************************************************

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "DoNextTaskOfClosePopUP")
        {
            WFUserControl.PerformCallback(-2, -2, -2, e);
            return;
        }
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewMeFileHistory = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
            if (GridViewMeFileHistory != null)
            {
                if (GridViewMeFileHistory.FocusedRowIndex > -1)
                {
                    DataRow row = GridViewMeFileHistory.GetDataRow(GridViewMeFileHistory.FocusedRowIndex);
                    int MfId = (int)row["MfId"];
                    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileImp;
                    int WFCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
                    WFUserControl.PerformCallback(MfId, TableType, WFCode, e);

                }
            }
            else
            {
                WFUserControl.SetMsgText("برای ارسال پرونده به مرحله بعد ابتدا یک درخواست را انتخاب نمائید");
                WFUserControl.PerformCallback(-2, -2, -2, e);
            }
        }
        else
        {
            WFUserControl.SetMsgText("ردیفی انتخاب نشده است.");
            WFUserControl.PerformCallback(-2, -2, -2, e);
        }

    }

    protected void CallbackPanelSearch_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "Clear")
            Clear();
    }

    #region Grid
    protected void GridViewMeFileHistory_BeforePerformDataSelect(object sender, EventArgs e)
    {
        if (!Utility.IsDBNullOrNullValue((sender as ASPxGridView).GetMasterRowFieldValues("MemberId")))
            Session["MeId"] = (sender as ASPxGridView).GetMasterRowFieldValues("MemberId");

        //int ImpDocId = (int)(sender as ASPxGridView).GetMasterRowKeyValue();
        //TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //DocMemberFileManager.ClearBeforeFill = true;
        //DocMemberFileManager.FindByCode(ImpDocId, 1);
        //if (DocMemberFileManager.Count == 1)
        //{
        //    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MeId"]))
        //    {
        //        int MfId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
        //        DocMemberFileManager.FindByCode(MfId, 0);
        //        if (DocMemberFileManager.Count == 1)
        //        {
        //            Session["MeId"] = DocMemberFileManager[0]["MeId"].ToString();
        //        }
        //    }
        //}
    }

    protected void GridViewMemberFile_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        GridViewMemberFile.FocusedRowIndex = e.VisibleIndex;
    }

    protected void GridViewMemberFile_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewMemberFile.DataBind();

        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Search":
                    Search();
                    GridViewMemberFile.DataBind();
                    break;
                case "Clear":
                    Clear();
                    break;
                case "Print":
                    ArrayList DeletedColumnsName = new ArrayList();
                    DeletedColumnsName.Add("WFState");
                    DeletedColumnsName.Add("ExpireState");

                    Session["DeletedColumnsName"] = DeletedColumnsName;
                    Session["DataTable"] = GridViewMemberFile.Columns;
                    Session["DataSource"] = ObjdsMemberFileMainRequest;
                    Session["Title"] = "مدیریت مجوز فعالیت مجری حقیقی";

                    GridViewMemberFile.DetailRows.CollapseAllRows();
                    GridViewMemberFile.JSProperties["cpDoPrint"] = 1;
                    break;
            }
        }
    }

    protected void GridViewMemberFile_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (e.GetValue("LastConfirm") != null)
        {
            if (e.GetValue("LastConfirm").ToString() == "0")
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        }
        SetGridRowColor(e);
    }

    protected void GridViewMemberFile_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "RegDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "LastExpireDate":
                // e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";

                e.Editor.Style["direction"] = "ltr";
                break;
            case "MFNo":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewMemberFile_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "RegDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "LastExpireDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "MFNo":
                e.Cell.Style["direction"] = "ltr";
                break;
        }

        //if (e.DataColumn.FieldName == "TaskId")
        //{
        //    DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMemberFile.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMemberFile.Columns["WFState"], "btnWFState");
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
        //        else
        //        {
        //        }
        //    }
        //}

        if (e.DataColumn.Name == "ExpireState")
        {
            DevExpress.Web.ASPxImage ImgExpireState = (DevExpress.Web.ASPxImage)GridViewMemberFile.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMemberFile.Columns["ExpireState"], "ImgExpireState");
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

    protected void GridViewMeFileHistory_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "RegDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "ExpireDate":
                // e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";

                e.Editor.Style["direction"] = "ltr";
                break;
            case "MFNo":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "MailDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewMeFileHistory_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
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
            case "MailDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }

        //if (e.DataColumn.FieldName == "TaskId")
        //{
        //    DevExpress.Web.ASPxGridView GridViewMeFileHistory = (DevExpress.Web.ASPxGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
        //    if (GridViewMeFileHistory == null)
        //        return;
        //    DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMeFileHistory.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMeFileHistory.Columns["WFState"], "btnWFState");
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
        //        else
        //        {
        //        }
        //    }
        //}
    }
    #endregion
    #endregion

    #region Methods   
    private void NextPage(string Mode)
    {
        int MfId = -1;
        int MeId = -1;
        int focucedIndex = -1;
        int PostId = -1;
        string SearchFilterString = GenerateFilterString();
        string GridFilterString = GridViewMemberFile.FilterExpression;
        if (Mode == "View" || Mode == "Edit")
        {
            if (GridViewMemberFile.FocusedRowIndex > -1)
            {
                TSP.WebControls.CustomAspxDevGridView GridViewMeFileHistory = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
                if (GridViewMeFileHistory != null)
                {
                    focucedIndex = GridViewMeFileHistory.FocusedRowIndex;
                    if (focucedIndex > -1)
                    {
                        DataRow row = GridViewMeFileHistory.GetDataRow(focucedIndex);
                        //***ImpDocId
                        PostId = MfId = (int)row["MfId"];
                        //***MfId
                        MeId = (int)row["MeId"];
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                    return;
                }
            }
        }
        else if (Mode != "New")
        {
            focucedIndex = GridViewMemberFile.FocusedRowIndex;
            if (focucedIndex > -1)
            {
                DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
                //***MfId
                MeId = (int)row["MeId"];
                //if (Mode != "New" || Mode != "View")
                //{
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DataTable dtDocMeFile = DocMemberFileManager.SelectImpDocLastVersion(-1, MeId, 1, -1);
                if (dtDocMeFile.Rows.Count > 0)
                {
                    //***ImpDocId
                    MfId = (int)dtDocMeFile.Rows[0]["MfId"];
                }
                //}
            }
        }

        if (MfId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New") MfId = -1;
            Response.Redirect("AddImplementDoc.aspx?MfId=" + Utility.EncryptQS(MfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&PostId=" + Utility.EncryptQS(PostId.ToString()) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
        }
    }

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

    //private void ReDuplicate(int MfId, int MeId)
    //{
    //    if (!CheckpermisionForNewRequest())
    //        return;
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
    //    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    WorkFlowTaskManager.ClearBeforeFill = true;
    //    DocMemberFileManager.ClearBeforeFill = true;
    //    try
    //    {
    //        //int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
    //        int WfCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
    //        DataTable dtWfState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, MfId);
    //        if (dtWfState.Rows.Count <= 0)
    //        {
    //            ShowMessage("برای پرونده انتخاب شده گردش کاری تعریف نشده است.");
    //            return;
    //        }
    //        int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
    //        int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectImplementDocAndEndProcess;
    //        int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmImplementDocAndEndProccess;
    //        int RejectTaskId = -1;
    //        int ConfirmTaskId = -1;

    //        WorkFlowTaskManager.FindByTaskCode(RejectTaskCode);
    //        if (WorkFlowTaskManager.Count > 0)
    //        {
    //            RejectTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
    //        }

    //        WorkFlowTaskManager.FindByTaskCode(ConfirmTaskCode);
    //        if (WorkFlowTaskManager.Count > 0)
    //        {
    //            ConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
    //        }

    //        if (CurrentTaskId == RejectTaskId || CurrentTaskId == ConfirmTaskId)
    //        {
    //            DocMemberFileManager.SelectImplementDoc(-1, MfId);
    //            if (DocMemberFileManager.Count != 1)
    //            {
    //                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
    //                return;
    //            }
    //            if (DocMemberFileManager[0]["IsConfirm"].ToString() == "1")
    //                NextPage("ReDuplicate");
    //            else
    //                ShowMessage("امکان صدور المثنی برای مجوز تایید نشده وجود ندارد.");
    //        }
    //        else
    //        {
    //            ShowMessage("به دلیل به پایان نرسیدن گردش کار مجوز انتخاب شده امکان صدور المثنی وجود ندارد.");
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        Utility.SaveWebsiteError(err);
    //        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInProcess));
    //    }

    //}

    //private void Revival(int MfId, int MeId)
    //{
    //    if (!CheckpermisionForNewRequest())
    //        return;
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
    //    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    WorkFlowTaskManager.ClearBeforeFill = true;
    //    DocMemberFileManager.ClearBeforeFill = true;

    //    try
    //    {
    //        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
    //        int WfCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
    //        DataTable dtWfState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, MfId);
    //        if (dtWfState.Rows.Count > 0)
    //        {
    //            int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
    //            int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectImplementDocAndEndProcess;
    //            int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmImplementDocAndEndProccess;

    //            int RejectTaskId = -1;
    //            int ConfirmTaskId = -1;
    //            int DocumentUnitConfirmTaskId = -1;

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
    //                DocMemberFileManager.SelectImplementDoc(-1, MfId);
    //                if (DocMemberFileManager.Count == 1)
    //                {
    //                    if (DocMemberFileManager[0]["IsConfirm"].ToString() == "1")
    //                    {
    //                        string CrtEndDate = DocMemberFileManager[0]["ExpireDate"].ToString();
    //                        Utility.Date objDate = new Utility.Date(CrtEndDate);
    //                        string LastMonth = objDate.AddMonths(-1);
    //                        string Today = Utility.GetDateOfToday();
    //                        int IsDocExp = string.Compare(Today, LastMonth);
    //                        if (IsDocExp > 0)
    //                        {
    //                            NextPage("Revival");
    //                        }
    //                        else
    //                        {
    //                            this.DivReport.Visible = true;
    //                            this.LabelWarning.Text = "تاریخ اعتبار مجوز انتخاب شده به پایان نرسیده است.";

    //                        }
    //                    }
    //                    else
    //                    {
    //                        this.DivReport.Visible = true;
    //                        this.LabelWarning.Text = "امکان تمدید برای مجوز تایید نشده وجود ندارد.";
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
    //                this.LabelWarning.Text = "به دلیل به پایان نرسیدن گردش کار مجوز انتخاب شده امکان درخواست تمدید وجود ندارد.";
    //            }

    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "برای پرونده انتخاب شده گردش کاری تعریف نشده است.";
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        Utility.SaveWebsiteError(err);
    //        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInProcess));
    //    }
    //}

    //private void Change(int MfId, int MeId)
    //{
    //    if (!CheckpermisionForNewRequest())
    //        return;
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
    //    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    WorkFlowTaskManager.ClearBeforeFill = true;
    //    DocMemberFileManager.ClearBeforeFill = true;

    //    try
    //    {
    //        int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
    //        int WfCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
    //        DataTable dtWfState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, MfId);
    //        if (dtWfState.Rows.Count > 0)
    //        {
    //            int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
    //            int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectImplementDocAndEndProcess;
    //            int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmImplementDocAndEndProccess;
    //            int RejectTaskId = -1;
    //            int ConfirmTaskId = -1;

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
    //                DocMemberFileManager.SelectImplementDoc(-1, MfId);
    //                if (DocMemberFileManager.Count == 1)
    //                {
    //                    if (DocMemberFileManager[0]["IsConfirm"].ToString() == "1")
    //                    {
    //                        NextPage("Change");
    //                    }
    //                    else
    //                    {
    //                        this.DivReport.Visible = true;
    //                        this.LabelWarning.Text = "امکان درخواست تغییرات برای مجوز تایید نشده وجود ندارد.";
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
    //                this.LabelWarning.Text = "به دلیل به پایان نرسیدن گردش کار مجوز انتخاب شده امکان درخواست تغییرات وجود ندارد.";
    //            }

    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "برای پرونده انتخاب شده گردش کاری تعریف نشده است.";
    //        }
    //    }
    //    catch (Exception err)
    //    {

    //        Utility.SaveWebsiteError(err);
    //        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInProcess));
    //    }
    //}

    //private void InValid(int MfId)
    //{
    //    if (!CheckpermisionForNewRequest())
    //        return;
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
    //    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
    //    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
    //    WorkFlowTaskManager.ClearBeforeFill = true;
    //    DocMemberFileManager.ClearBeforeFill = true;

    //    try
    //    {
    //        int WfCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
    //        DataTable dtWfState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, MfId);
    //        if (dtWfState.Rows.Count > 0)
    //        {
    //            int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
    //            int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectImplementDocAndEndProcess;
    //            int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmImplementDocAndEndProccess;
    //            int RejectTaskId = -1;
    //            int ConfirmTaskId = -1;

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

    //            if (CurrentTaskId != RejectTaskId && CurrentTaskId != ConfirmTaskId)
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "به دلیل به پایان نرسیدن گردش کار مجوز انتخاب شده امکان درخواست ابطال وجود ندارد.";
    //                return;
    //            }

    //        }
    //        DocMemberFileManager.SelectImplementDoc(-1, MfId);
    //        if (DocMemberFileManager.Count == 1)
    //        {
    //            if (DocMemberFileManager[0]["IsConfirm"].ToString() == "1")
    //            {
    //                NextPage("InValid");
    //            }
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "امکان درخواست ابطال برای مجوز تایید نشده وجود ندارد.";
    //            }
    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
    //        }

    //    }
    //    catch (Exception err)
    //    {
    //        Utility.SaveWebsiteError(err);
    //        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInProcess));
    //    }
    //}

    private Boolean CheckRequest(string ReqName)
    {
        try
        {
            if (!CheckpermisionForNewRequest())
                return false;
            int MfId = -2;
            int MemberFileId = -2;
            int MeId = -2;
            if (GridViewMemberFile.FocusedRowIndex <= -1)
            {
                ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
                return false;
            }
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            if (MeFileRow == null)
            {
                ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
                return false;
            }
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            MeId = int.Parse(MeFileRow["MemberId"].ToString());
            DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
            //int MeId = -1;
            if (DocMemberFileManager.Count != 1)
            {
                ShowMessage("بدلیل نامشخص بودن وضعیت پروانه اشتغال به کار امکان ثبت درخواست جدید وجود ندارد.");
                return false;
            }
            MemberFileId = Convert.ToInt32(DocMemberFileManager[0]["MfId"]);
            string Msg = "";
            if (!TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg))
            {
                ShowMessage(Msg);
                return false;
            }
            //TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            //MemberManager.FindByCode(MeId);
            //if (MemberManager.Count != 1)
            //{
            //    ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است.");
            //    return false;
            //}
            //int MRsId = Convert.ToInt32(MemberManager[0]["MrsId"]);          
            //if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed)
            //{
            //    if (MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.Pending)
            //    {
            //        ShowMessage("امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی در جریان می باشد.");
            //        return false;
            //    }
            //    else if (MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.Cancel)
            //    {
            //        ShowMessage("امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی لغو شده می باشد.");
            //        return false;
            //    }
            //    else if (MRsId == (int)TSP.DataManager.MembershipRegistrationStatus.CancelDebtorMember)
            //    {
            //        ShowMessage("امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی قطع شده می باشد.");
            //        return false;
            //    }
            //    else
            //    {
            //        ShowMessage("امکان ثبت درخواست جدید وجود ندارد.عضویت عضو انتخابی تایید شده نمی باشد.");
            //        return false;
            //    }
            //}


            DataTable dtDocMeFile = DocMemberFileManager.SelectImpDocLastVersion(MeId, -1, -1, -1);
            if (dtDocMeFile.Rows.Count <= 0)
            {
                ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است.");
                return false;
                //ShowMessage("بدلیل نامشخص بودن وضعیت/غیر فعال و یا عدم تایید مجوز امکان صدور المثنی وجود ندارد.");
            }
            //MfId = (int)dtDocMeFile.Rows[0]["MfId"];
            if (Convert.ToInt32(DocMemberFileManager[0]["IsConfirm"]) == 1)
            {
                if (ReqName == "Revival")
                {
                    string CrtEndDate = DocMemberFileManager[0]["ExpireDate"].ToString();
                    Utility.Date objDate = new Utility.Date(CrtEndDate);
                    string LastMonth = objDate.AddMonths(-1);
                    string Today = Utility.GetDateOfToday();
                    int IsDocExp = string.Compare(Today, LastMonth);
                    if (IsDocExp <= 0)
                    {
                        ShowMessage("امکان درخواست تمدید وجود ندارد.تاریخ اعتبار مجوز انتخاب شده به پایان نرسیده است.");
                        return false;

                    }
                }
            }
            else if (Convert.ToInt32(DocMemberFileManager[0]["IsConfirm"]) == 0)
            {
                ShowMessage("امکان ثبت درخواست جدید وجود ندارد.مجوز انتخاب شده دارای درخواست درجریان می باشد.");
                return false;
            }
            return true;
            //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

            //int WfCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
            //DataTable dtWfState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, MfId);
            //if (dtWfState.Rows.Count <= 0)
            //{
            //    ShowMessage("برای پرونده انتخاب شده گردش کاری تعریف نشده است.");
            //    return false;
            //}
            //int CurrentTaskCode = int.Parse(dtWfState.Rows[0]["TaskCode"].ToString());

            //if (CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.RejectImplementDocAndEndProcess && CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.ConfirmImplementDocAndEndProccess)
            //{
            //    ShowMessage("به دلیل به پایان نرسیدن گردش کار مجوز انتخاب شده امکانثبت درخواست جدید وجود ندارد.");
            //    return false;
            //}
            //DocMemberFileManager.SelectImplementDoc(-1, MfId);
            //if (DocMemberFileManager.Count != 1)
            //{
            //    ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
            //    return false;
            //}

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInProcess));
            return false;
        }
    }
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp);
        int WorkFlowCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WorkFlowCode, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());

            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
            int TaskCodeDocUnit = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingImplementDoc;

            if (CurrentTaskCode == DocMeFileSaveInfoTaskCode || CurrentTaskCode == TaskCodeDocUnit)
            {
                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WorkFlowCode, TableId, DocMeFileSaveInfoTaskCode, Utility.GetCurrentUser_UserId());
                int PermissionTaskCodeDocUnit = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WorkFlowCode, TableId, TaskCodeDocUnit, Utility.GetCurrentUser_UserId());
                if (Permission > 0 || PermissionTaskCodeDocUnit > 0)
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

    private Boolean CheckPermitionForDelete(int TableId)
    {
        return TSP.DataManager.WorkFlowPermission.CheckWFPermissionForDeleteRequest(TableId, (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming
         , (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId);
        //TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //WorkFlowStateManager.ClearBeforeFill = true;
        //int TaskOrder = -1;
        //int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        //WorkFlowTaskManager.FindByTaskCode(TaskCode);
        //if (WorkFlowTaskManager.Count > 0)
        //{
        //    TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        //    if (TaskOrder != 0)
        //    {
        //        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp);
        //        int WfCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
        //        DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
        //        if (dtState.Rows.Count == 1)
        //        {
        //            int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
        //            int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
        //            int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
        //            int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
        //            int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());
        //            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;

        //            if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
        //            {
        //                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
        //                if (dtWorkFlowState.Rows.Count > 0)
        //                {
        //                    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
        //                    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
        //                    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
        //                    if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
        //                    {
        //                        if ((Utility.GetCurrentUser_MeId() == Convert.ToInt32(dtState.Rows[0]["EmpId"]) || FirstNmcId == FindNmcId()) && FirstNmcIdType == 0)
        //                        {
        //                            return true;
        //                        }
        //                        else
        //                        {
        //                            return false;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        return false;
        //                    }
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //else
        //{
        //    return false;
        //}
    }

    /// <summary>
    /// جهت تنظیم سطح دسترسی دکمه های درخواست ها
    /// </summary>
    /// <returns></returns>
    private Boolean CheckWorkFlowPermissionForChangeReq()
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp);
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId());
        return (WFPer.BtnNewRequest);
    }

    /// <summary>
    /// جهت چک کردن سطح دسترسی هنگام کلیک بر روی دکمه های درخواست
    /// </summary>
    /// <returns></returns>
    private Boolean CheckpermisionForNewRequest()
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp);
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        TSP.DataManager.WFPermission Per = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(TaskCode, TableType, Utility.GetCurrentUser_UserId());
        if (!Per.BtnNewRequest)
        {
            ShowMessage("شما دارای سطح دسترسی گردش کار جهت ثبت درخواست جدید نمی باشید");
            return false;
        }
        return true;
    }

    private void DeleteRequest(int MfId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.DocOffJobHistoryQualityManager DocOffJobHistoryQualityManager = new TSP.DataManager.DocOffJobHistoryQualityManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager FinManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();
        TransactionManager.Add(FinManager);
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(attachManager);
        TransactionManager.Add(ProjectJobHistoryManager);
        TransactionManager.Add(DocOffJobHistoryQualityManager);
        try
        {
            TransactionManager.BeginSave();

            #region Delete JobHistory
            ProjectJobHistoryManager.FindForDelete(0, MfId, (int)TSP.DataManager.TableCodes.DocMemberFile);
            if (ProjectJobHistoryManager.Count > 0)
            {
                int C = ProjectJobHistoryManager.Count;
                for (int i = 0; i < C; i++)
                {
                    int JhId = (int)ProjectJobHistoryManager[i]["JhId"];
                    DataTable dtJobQuality = DocOffJobHistoryQualityManager.FindByJobCode(JhId);
                    if (dtJobQuality.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtJobQuality.Rows.Count; i++)
                        {
                            int JhqId = (int)dtJobQuality.Rows[j]["JhqId"];
                            DocOffJobHistoryQualityManager.FindByCode(JhqId);
                            if (DocOffJobHistoryQualityManager.Count == 1)
                            {
                                DocOffJobHistoryQualityManager[0].Delete();
                            }
                            if (DocOffJobHistoryQualityManager.Save() <= 0)
                            {
                                TransactionManager.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                                return;
                            }
                        }
                    }
                    ProjectJobHistoryManager[i].Delete();
                }
                if (ProjectJobHistoryManager.Save() < 0)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    return;
                }
            }
            #endregion

            #region Delete Financial Status
            FinManager.SelectForImplementDoc(MfId);
            if (FinManager.Count > 0)
            {
                int c = FinManager.Count;
                for (int i = 0; i < c; i++)
                    FinManager[0].Delete();

                FinManager.Save();
            }
            #endregion

            #region Delete Attachment
            attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.DocMemberFile, MfId);
            if (attachManager.Count > 0)
            {
                int c = attachManager.Count;
                for (int i = 0; i < c; i++)
                    attachManager[0].Delete();

                attachManager.Save();
            }
            #endregion

            #region Delete WFState
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete((int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming, MfId);
            if (WorkFlowStateManager.Count > 0)
            {
                int count = WorkFlowStateManager.Count;
                for (int i = 0; i < count; i++)
                {
                    WorkFlowStateManager[i].Delete();
                }
                if (WorkFlowStateManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    return;
                }
            }

            #endregion

            #region Delete Letter
            LetterRelatedTablesManager.FindByTableIdTableType(MfId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp));
            if (LetterRelatedTablesManager.Count > 0)
            {
                int count = LetterRelatedTablesManager.Count;
                for (int i = 0; i < count; i++)
                {
                    LetterRelatedTablesManager[0].Delete();
                }
                if (LetterRelatedTablesManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    return;
                }
            }
            #endregion

            #region Delete MemeberFile
            DocMemberFileManager.SelectImplementDoc(-1, MfId);
            if (DocMemberFileManager.Count == 1)
            {
                DocMemberFileManager[0].Delete();
                if (DocMemberFileManager.Save() > 0)
                {
                    TransactionManager.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لغو در خواست انجام گرفت.";
                }
                else
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
            #endregion
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
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.MemberFileImp).ToString());
    }

    void ShowMessage(string str)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = str;
    }

    void Clear()
    {
        txtEndDateFrom.Text = "";
        txtEndDateTo.Text = "";
        txtFollowCode.Text = "";
        txtMeId.Text = "";
        ObjdsMemberFileMainRequest.SelectParameters["MeId"].DefaultValue = "-1";
        ObjdsMemberFileMainRequest.SelectParameters["EndDateFrom"].DefaultValue = "1";
        ObjdsMemberFileMainRequest.SelectParameters["EndDateTo"].DefaultValue = "2";
        ObjdsMemberFileMainRequest.SelectParameters["FollowCode"].DefaultValue = "%";
        GridViewMemberFile.DataBind();
    }

    private void SetGridRowColor(ASPxGridViewTableRowEventArgs e)
    {
        if (e.GetValue("LastRequsetType") != null)
        {
            int LastRequsetType = Convert.ToInt32(e.GetValue("LastRequsetType"));
            switch (LastRequsetType)
            {
                case (int)TSP.DataManager.DocumentOfMemberRequestType.Change:
                    e.Row.ForeColor = System.Drawing.Color.Brown;
                    break;
                case (int)TSP.DataManager.DocumentOfMemberRequestType.New:
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    break;
                case (int)TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate:
                    e.Row.ForeColor = System.Drawing.Color.Magenta;
                    break;
                case (int)TSP.DataManager.DocumentOfMemberRequestType.Revival:
                    e.Row.ForeColor = System.Drawing.Color.Green;
                    break;
            }
        }

        if (e.GetValue("LastConfirmReqType") != null)
        {
            if (Convert.ToInt32(e.GetValue("LastConfirmReqType")) == (int)TSP.DataManager.DocumentOfMemberRequestType.InActive)
                e.Row.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void Search()
    {
        if (!string.IsNullOrEmpty(txtMeId.Text))
            ObjdsMemberFileMainRequest.SelectParameters["MeId"].DefaultValue = txtMeId.Text;
        else ObjdsMemberFileMainRequest.SelectParameters["MeId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtEndDateFrom.Text))
            ObjdsMemberFileMainRequest.SelectParameters["EndDateFrom"].DefaultValue = txtEndDateFrom.Text;
        else ObjdsMemberFileMainRequest.SelectParameters["EndDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtEndDateTo.Text))
            ObjdsMemberFileMainRequest.SelectParameters["EndDateTo"].DefaultValue = txtEndDateTo.Text;
        else ObjdsMemberFileMainRequest.SelectParameters["EndDateTo"].DefaultValue = "2";

        if (!string.IsNullOrEmpty(txtFollowCode.Text))
            ObjdsMemberFileMainRequest.SelectParameters["FollowCode"].DefaultValue = txtFollowCode.Text;
        else ObjdsMemberFileMainRequest.SelectParameters["FollowCode"].DefaultValue = "%";


        if (!string.IsNullOrEmpty(txtCreateDateLastReqFrom.Text))
            ObjdsMemberFileMainRequest.SelectParameters["ReqCreateDateFrom"].DefaultValue = txtCreateDateLastReqFrom.Text;
        else ObjdsMemberFileMainRequest.SelectParameters["ReqCreateDateFrom"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtCreateDateLastReqTo.Text))
            ObjdsMemberFileMainRequest.SelectParameters["ReqCreateDateTo"].DefaultValue = txtCreateDateLastReqTo.Text;
        else ObjdsMemberFileMainRequest.SelectParameters["ReqCreateDateTo"].DefaultValue = "2";

    }

    #region Set Grid Index
    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
                string SrchFlt = Utility.DecryptQS(Request.QueryString["SrchFlt"].ToString());

                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewMemberFile.FilterExpression = GrdFlt;
                if (!string.IsNullOrEmpty(SrchFlt))
                    FilterObjdsByValue(SrchFlt);
            }
        }

    }

    private void FilterObjdsByValue(string FilterString)
    {
        string[] SearchFilter = FilterString.Split('&');
        for (int i = 0; i < SearchFilter.Length; i = i + 2)
        {
            string ParameterName = SearchFilter[i].ToString();
            string Value = SearchFilter[i + 1].ToString();
            ObjdsMemberFileMainRequest.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    case "MeId":
                        if (Value != "-1")
                            txtMeId.Text = Value;
                        break;
                    case "FollowCode":
                        txtFollowCode.Text = Value;
                        break;
                    case "EndDateFrom":
                        if (Value != "1")
                            txtEndDateFrom.Text = Value;
                        break;
                    case "EndDateTo":
                        if (Value != "2")
                            txtEndDateTo.Text = Value;
                        break;

                }
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

                    GridViewMemberFile.DataBind();
                    Index = GridViewMemberFile.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewMemberFile.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewMemberFile.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewMemberFile.JSProperties["cpSelectedIndex"] = Index;
                        GridViewMemberFile.DetailRows.ExpandRow(Index);
                        GridViewMemberFile.FocusedRowIndex = Index;
                        GridViewMemberFile.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
        return Index;
    }

    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < ObjdsMemberFileMainRequest.SelectParameters.Count; i++)
        {
            if (ObjdsMemberFileMainRequest.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += ObjdsMemberFileMainRequest.SelectParameters[i].Name + "&";
                FilterString += ObjdsMemberFileMainRequest.SelectParameters[i].DefaultValue + "&";
            }
        }
        FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }
    #endregion

    #endregion
}
