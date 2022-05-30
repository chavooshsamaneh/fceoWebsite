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

public partial class Employee_Document_ObservationDoc : System.Web.UI.Page
{

    #region Events
    Boolean IsPageRefresh = false;
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
            SetHelpAddress();
            Session["SendBackDataTable_EmpObsDoc"] = "";
            GridViewMemberFile.JSProperties["cpIsPostBack"] = 1;
            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionObs(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;

            Session["DeletedDetailColumnsName"] = null;
            Session["DataTableDetail"] = null;
            Session["DataSourceDetail"] = null;
            Session["GridDetailName"] = null;
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming).ToString();
        }

        SetPageFilter();
        SetGridRowIndex();
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        NextPage("View");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            if (MeFileRow != null)
            {
                int MfId = int.Parse(MeFileRow["MfId"].ToString());
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DocMemberFileManager.ClearBeforeFill = true;
                DataTable dtObsDoc = DocMemberFileManager.SelectObservationDoc(-1, MfId);
                if (dtObsDoc.Rows.Count == 1)
                {
                    int MeFileId = int.Parse(dtObsDoc.Rows[0]["MeFileId"].ToString());
                    int MeId = int.Parse(dtObsDoc.Rows[0]["MemberId"].ToString());
                    string Msg = "";
                    if (!TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg))
                    {
                        SetMessage(Msg);
                        return;
                    }

                    DataTable dtDocMeFile = DocMemberFileManager.SelectObsDocLastVersionByMeFileId(MeFileId, 0);
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
                            this.LabelWarning.Text = "برای شما امکان ویرایش اطلاعات وجود ندارد.";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "تنها امکان ویرایش درخواست های در جریان وجود دارد.";
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
        if (IsPageRefresh)
            return;
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

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        NextPage("New");
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        NextPage("View");
    }

    protected void btnReDuplicate_Click(object sender, EventArgs e)
    {
        //if (GridViewMemberFile.FocusedRowIndex > -1)
        //{
        //    DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        //    if (MeFileRow != null)
        //    {
        //        int MeId = int.Parse(MeFileRow["MeId"].ToString());

        //        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        //        MemberManager.FindByCode(MeId);
        //        if (MemberManager.Count == 1)
        //        {
        //            int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
        //            if (MRsId == 1)
        //            {
        //                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //                DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
        //                if (dtDocMeFile.Rows.Count > 0)
        //                {
        //                    int MfId = (int)dtDocMeFile.Rows[0]["MfId"];
        //                    ReDuplicate(MfId, MeId);
        //                }
        //                else
        //                {
        //                    this.DivReport.Visible = true;
        //                    this.LabelWarning.Text = "بدلیل نامشخص بودن وضعیت پروانه امکان ویرایش اطلاعات وجود ندارد.";
        //                }

        //            }
        //            else
        //            {
        //                this.DivReport.Visible = true;
        //                this.LabelWarning.Text = "امکان ویرایش وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.";
        //            }
        //        }
        //        else
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
        //        }
        //    }
        //}
    }

    protected void btnRevival_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            if (MeFileRow != null)
            {
                int MfId = int.Parse(MeFileRow["MfId"].ToString());
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DocMemberFileManager.ClearBeforeFill = true;
                DataTable dtObsDoc = DocMemberFileManager.SelectObservationDoc(-1, MfId);
                if (dtObsDoc.Rows.Count == 1)
                {
                    int MeFileId = int.Parse(dtObsDoc.Rows[0]["MeFileId"].ToString());
                    int MeId = int.Parse(dtObsDoc.Rows[0]["MemberId"].ToString());
                    string Msg = "";
                    if (!TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg))
                    {
                        SetMessage(Msg);
                        return;
                    }

                    DataTable dtDocMeFile = DocMemberFileManager.SelectObsDocLastVersionByMeFileId(MeFileId);
                    if (dtDocMeFile.Rows.Count > 0)
                    {
                        int LastMfId = (int)dtDocMeFile.Rows[0]["MfId"];
                        Revival(LastMfId, MeId);
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
                    this.LabelWarning.Text = "اطلاعت مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }
            }
        }
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            if (MeFileRow != null)
            {
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                int MemberFileId = int.Parse(MeFileRow["MeId"].ToString());
                DocMemberFileManager.FindByCode(MemberFileId, 0);
                int MeId = -1;
                if (DocMemberFileManager.Count == 1)
                {
                    MeId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "بدلیل نامشخص بودن وضعیت پروانه اشتغال به کار امکان درخواست تغییرات وجود ندارد.";
                    return;
                }
                string Msg = "";
                if (!TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg))
                {
                    SetMessage(Msg);
                    return;
                }
                DataTable dtDocMeFile = DocMemberFileManager.SelectObsDocLastVersionByMeId(MeId);
                if (dtDocMeFile.Rows.Count > 0)
                {
                    int MfId = (int)dtDocMeFile.Rows[0]["MfId"];
                    Change(MfId, MemberFileId);
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = ("بدلیل نامشخص بودن وضعیت/غیر فعال و یا عدم تایید مجوز امکان درخواست تغییرات اطلاعات وجود ندارد.");
                }


            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            if (MeFileRow != null)
            {
                TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
                if (GridRequest != null)
                {
                    if (GridRequest.VisibleRowCount > 0)
                    {
                        int index0 = GridRequest.FocusedRowIndex;
                        if (index0 != -1)
                        {
                            int MfId = int.Parse(GridRequest.GetDataRow(index0)["MfId"].ToString());
                            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                            DocMemberFileManager.ClearBeforeFill = true;
                            DataTable dtObsDoc = DocMemberFileManager.SelectObservationDoc(-1, MfId);
                            if (dtObsDoc.Rows.Count == 1)
                            {
                                if (CheckPermitionForDelete(MfId))
                                {
                                    DeleteRequest(MfId);
                                }
                                else
                                {
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "بر اساس قوانین گردش کار امکان لغو این درخواست وجود ندارد.";
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
        if (IsPageRefresh)
            return;
        //GridViewMemberFile.Columns["Status"].Visible = false;
        GridViewExporter.FileName = "ObservationDocument";

        GridViewExporter.WriteXlsToResponse(true);
        //GridViewMemberFile.Columns["Status"].Visible = true;
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (GridViewMemberFile.FocusedRowIndex > -1)
        {
            int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileObs;
            DataRow DocMeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
            int TableId = int.Parse(DocMeFileRow["MfId"].ToString());

            Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void btnInvalid_Click(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex <= -1)
        {
            SetMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        if (MeFileRow == null)
        {
            SetMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        int MemberFileId = int.Parse(MeFileRow["MeId"].ToString());
        DocMemberFileManager.FindByCode(MemberFileId, 0);
        int MeId = -1;
        if (DocMemberFileManager.Count == 1)
        {
            MeId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "بدلیل نامشخص بودن وضعیت پروانه اشتغال به کار امکان درخواست تغییرات وجود ندارد.";
            return;
        }
        string Msg = "";
        if (!TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg))
        {
            SetMessage(Msg);
            return;
        }
        DataTable dtDocMeFile = DocMemberFileManager.SelectObsDocLastVersion(MeId, -1, 1);
        if (dtDocMeFile.Rows.Count <= 0)
        {
            SetMessage("بدلیل نامشخص بودن وضعیت/غیر فعال و یا عدم تایید مجوز امکان ثبت درخواست اطلاعات وجود ندارد.");
            return;
        }
        int MfId = (int)dtDocMeFile.Rows[0]["MfId"];
        if (!CheckpermisionForNewRequest())
            return;
        string GridFilterString = GridViewMemberFile.FilterExpression;
        Response.Redirect("AddObservationDoc.aspx?MfId=" + Utility.EncryptQS(MfId.ToString())
            + "&PgMd=" + Utility.EncryptQS("Invalid")
            + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));

    }
    //*************************************************************************************************************

    protected void GridViewMemberFile_FocusedRowChanged(object sender, EventArgs e)
    {

    }

    protected void GridViewMeFileHistory_BeforePerformDataSelect(object sender, EventArgs e)
    {
        if (!Utility.IsDBNullOrNullValue((sender as ASPxGridView).GetMasterRowFieldValues("MemberId")))
            Session["MeId"] = (sender as ASPxGridView).GetMasterRowFieldValues("MemberId");
        //int ObsDocId = (int)(sender as ASPxGridView).GetMasterRowKeyValue();
        //TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //DocMemberFileManager.ClearBeforeFill = true;
        //DocMemberFileManager.FindByCode(ObsDocId, 2);
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
        //int Index = GridViewMemberFile.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        //GridViewMemberFile.FocusedRowIndex = Index;
    }

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (IsPageRefresh)
            return;
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
                    int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileObs;
                    int WFCode = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;
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

    //*****************************Grid***********************************************************

    protected void GridViewMemberFile_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        GridViewMemberFile.FocusedRowIndex = e.VisibleIndex;
    }

    protected void GridViewMemberFile_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (IsPageRefresh)
            return;

        GridViewMemberFile.DataBind();
        GridViewMemberFile.ExpandRow(GridViewMemberFile.FocusedRowIndex);

        if (!string.IsNullOrEmpty(e.Parameters))
        {
            switch (e.Parameters)
            {
                case "Search":
                    if (!string.IsNullOrEmpty(txtEndDateFrom.Text))
                        ObjdsMemberFileMainRequest.SelectParameters["EndDateFrom"].DefaultValue = txtEndDateFrom.Text;

                    if (!string.IsNullOrEmpty(txtEndDateTo.Text))
                        ObjdsMemberFileMainRequest.SelectParameters["EndDateTo"].DefaultValue = txtEndDateTo.Text;

                    if (!string.IsNullOrEmpty(txtFollowCode.Text))
                        ObjdsMemberFileMainRequest.SelectParameters["FollowCode"].DefaultValue = txtFollowCode.Text;

                    GridViewMemberFile.DataBind();

                    break;
                case "Clear":
                    txtEndDateFrom.Text = "";
                    txtEndDateTo.Text = "";
                    txtFollowCode.Text = "";
                    ObjdsMemberFileMainRequest.SelectParameters["EndDateFrom"].DefaultValue = "1";
                    ObjdsMemberFileMainRequest.SelectParameters["EndDateTo"].DefaultValue = "2";
                    ObjdsMemberFileMainRequest.SelectParameters["FollowCode"].DefaultValue = "%";
                    GridViewMemberFile.DataBind();

                    break;
                case "Print":
                    ArrayList DeletedColumnsName = new ArrayList();
                    DeletedColumnsName.Add("WFState");
                    DeletedColumnsName.Add("ExpireState");

                    Session["DeletedColumnsName"] = DeletedColumnsName;
                    Session["DataTable"] = GridViewMemberFile.Columns;
                    Session["DataSource"] = ObjdsMemberFileMainRequest;
                    Session["Title"] = "مدیریت مجوز فعالیت ناظر حقیقی";

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
    }

    protected void GridViewMemberFile_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "MFNo":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "RegDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "LastExpireDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewMemberFile_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "MFNo":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "RegDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "LastExpireDate":
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

                if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFStart.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFInProcess.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
                }
                else
                {
                }
            }
        }

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

    protected void GridViewMeFileHistory_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "MFNo":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "MailNo":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "MailDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "RegDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "ExpireDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }

        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxGridView GridViewMeFileHistory = (DevExpress.Web.ASPxGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
            if (GridViewMeFileHistory == null)
                return;
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewMeFileHistory.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewMeFileHistory.Columns["WFState"], "btnWFState");
            if (btnWFState != null)
            {
                if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
                {
                    btnWFState.ToolTip = "تعریف نشده";
                    btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
                    return;
                }

                if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFStart.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFInProcess.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
                }
                else
                {
                }
            }
        }
    }

    protected void GridViewMeFileHistory_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "MFNo":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "MailNo":
                e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";

                // e.Editor.Style["direction"] = "ltr";
                break;
            case "MailDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "RegDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "ExpireDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewMemberFile_DetailsChanged(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxGridView GridViewMeFileHistory = (DevExpress.Web.ASPxGridView)GridViewMemberFile.FindDetailRowTemplateControl(GridViewMemberFile.FocusedRowIndex, "GridViewMeFileHistory");
        if (GridViewMeFileHistory != null)
        {
            ArrayList DeletedColumnsName = new ArrayList();
            DeletedColumnsName.Add("WFState");
            Session["DeletedDetailColumnsName"] = DeletedColumnsName;
            Session["DataTableDetail"] = GridViewMeFileHistory.Columns;
            Session["DataSourceDetail"] = ObjdsMeFileSubRequest;
            Session["GridDetailName"] = "GridViewMeFileHistory";
        }
    }

    protected void GridViewMemberFile_PageIndexChanged(object sender, EventArgs e)
    {
        GridViewMemberFile.JSProperties["cpIsPostBack"] = 1;
    }

    protected void CallbackPanelSearch_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (IsPageRefresh)
            return;
        CallbackPanelSearch.JSProperties["cpPrintCom"] = 0;
        CallbackPanelSearch.JSProperties["cpPrintComPath"] = "";
        CallbackPanelSearch.JSProperties["cpPrintAnn"] = 0;
        CallbackPanelSearch.JSProperties["cpPrintAnnPath"] = "";
        int MeId = -1;
        int focucedIndex = -1;
        if (GridViewMemberFile.FocusedRowIndex > -1)
            focucedIndex = GridViewMemberFile.FocusedRowIndex;

        switch (e.Parameter)
        {
            case "Clear":
                txtEndDateFrom.Text = "";
                txtEndDateTo.Text = "";
                txtFollowCode.Text = "";
                ObjdsMemberFileMainRequest.SelectParameters["EndDateFrom"].DefaultValue = "1";
                ObjdsMemberFileMainRequest.SelectParameters["EndDateTo"].DefaultValue = "2";
                ObjdsMemberFileMainRequest.SelectParameters["FollowCode"].DefaultValue = "%";
                GridViewMemberFile.DataBind();
                break;

            case "PrintCom":
                int ObsId = -1;
                if (focucedIndex > -1)
                {
                    DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
                    MeId = (int)row["MemberId"];
                    ObsId = (int)row["ObsId"];
                    if (ObsId != (int)TSP.DataManager.DocumentGrads.Grade3)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "تنها برای اعضایی که دارای پایه سه نظارت هستند امکان چاپ تعهدنامه وجود دارد";
                        return;
                    }
                    CallbackPanelSearch.JSProperties["cpPrintCom"] = 1;
                    CallbackPanelSearch.JSProperties["cpPrintComPath"] = "../../ReportForms/ObserverCommitment.aspx?MeId="
                        + Utility.EncryptQS(MeId.ToString());
                }
                break;

            case "PrintAnn":
                if (focucedIndex > -1)
                {
                    DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
                    MeId = (int)row["MemberId"];
                    CallbackPanelSearch.JSProperties["cpPrintAnn"] = 1;
                    CallbackPanelSearch.JSProperties["cpPrintAnnPath"] = "../../ReportForms/ObserverAnnounce.aspx?MeId="
                        + Utility.EncryptQS(MeId.ToString());
                }
                break;
        }
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int ObsMfId = -1;
        int MeId = -1;
        int MfId = -1;
        int focucedIndex = -1;
        if (Mode == "View")
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
                        //***ObsDocId
                        ObsMfId = (int)row["MfId"];
                        //***MfId
                        MfId = (int)row["MeId"];
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
        else
        {
            focucedIndex = GridViewMemberFile.FocusedRowIndex;
            if (focucedIndex > -1)
            {
                DataRow row = GridViewMemberFile.GetDataRow(focucedIndex);
                //***MfId
                MeId = (int)row["MeId"];
                if (Mode != "New" || Mode != "View")
                {
                    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();

                    DataTable dtDocMeFile = new DataTable();
                    dtDocMeFile = DocMemberFileManager.SelectObsDocLastVersionByMeFileId(MeId);

                    if (dtDocMeFile.Rows.Count > 0)
                    {
                        //***ObsDocId
                        ObsMfId = (int)dtDocMeFile.Rows[0]["MfId"];
                    }
                }
            }
        }

        if (ObsMfId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            string GridFilterString = GridViewMemberFile.FilterExpression;
            if (Mode == "New")
            {
                ObsMfId = -1;
                Response.Redirect("AddObservationDoc.aspx?MfId=" + Utility.EncryptQS(ObsMfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("AddObservationDoc.aspx?MfId=" + Utility.EncryptQS(ObsMfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
            }
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

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo; //(int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingObservationDoc
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileObs);
                int WorkFlowCode = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WorkFlowCode, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    //**** int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingObservationDoc;
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByWorkFlowCode(WorkFlowCode, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            // int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            //   int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            //   int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                            {
                                //   if (FirstNmcIdType == 0)
                                //  {
                                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WorkFlowCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                                if (Permission > 0)
                                    return true;
                                else
                                    return false;
                                //}
                                //  else
                                // {
                                return false;
                                //  }
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

    private void Revival(int MfId, int MeId)
    {
        if (!CheckpermisionForNewRequest())
            return;
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //WorkFlowTaskManager.ClearBeforeFill = true;
        DocMemberFileManager.ClearBeforeFill = true;


        try
        {

            DocMemberFileManager.SelectObservationDoc(-1, MfId);
            if (DocMemberFileManager.Count == 1)
            {
                if (Convert.ToBoolean(DocMemberFileManager[0]["IsConfirm"]) == true)
                {
                    string CrtEndDate = DocMemberFileManager[0]["ExpireDate"].ToString();
                    Utility.Date objDate = new Utility.Date(CrtEndDate);
                    string LastMonth = objDate.AddMonths(-1);
                    string Today = Utility.GetDateOfToday();
                    int IsDocExp = string.Compare(Today, LastMonth);
                    if (IsDocExp > 0)
                    {
                        NextPage("Revival");
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "تاریخ اعتبار پروانه انتخاب شده به پایان نرسیده است.";

                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان تمدید برای مجوز تایید نشده وجود ندارد.";
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
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
        }
    }

    private void Change(int MfId, int MeId)
    {
        if (!CheckpermisionForNewRequest())
            return;

        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.ClearBeforeFill = true;

        try
        {

            DocMemberFileManager.SelectObservationDoc(-1, MfId);
            if (DocMemberFileManager.Count == 1)
            {
                if (DocMemberFileManager[0]["IsConfirm"].ToString() == "1")
                {
                    NextPage("Change");
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان درخواست تغییرات برای مجوز تایید نشده وجود ندارد.";
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
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = (Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInProcess));
        }
    }

    private Boolean CheckPermitionForDelete(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileObs);
                int WfCode = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;
                DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
                if (dtState.Rows.Count == 1)
                {
                    int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;

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
                                if (FirstNmcId == FindNmcId() && FirstNmcIdType == 0)
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

    private Boolean CheckpermisionForNewRequest()
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileObs);
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObservationDocInfo;
        TSP.DataManager.WFPermission Per = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(TaskCode, TableType, Utility.GetCurrentUser_UserId());
        if (!Per.BtnNewRequest)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("شما دارای سطح دسترسی گردش کار جهت ثبت درخواست جدید نمی باشید");
            return false;
        }
        return true;
    }

    private void DeleteRequest(int MfId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.DocImpDocCityManager DocImpDocCityManager = new TSP.DataManager.DocImpDocCityManager();
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();
        TransactionManager.Add(DocImpDocCityManager);
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        try
        {
            TransactionManager.BeginSave();
            #region Delete WFState
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete((int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming, MfId);
            if (WorkFlowStateManager.Count > 0)
            {
                int count = WorkFlowStateManager.Count;
                for (int i = 0; i < count; i++)
                {
                    WorkFlowStateManager[0].Delete();
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

            #region City
            DataTable dtImpCity = DocImpDocCityManager.FindMfId(MfId);
            if (dtImpCity.Rows.Count > 0)
            {
                for (int i = 0; i < dtImpCity.Rows.Count; i++)
                    DocImpDocCityManager[0].Delete();
                if (DocImpDocCityManager.Save() <= 0)
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    return;
                }
            }
            #endregion

            #region Delete Letter
            LetterRelatedTablesManager.FindByTableIdTableType(MfId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileObs));
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
            DocMemberFileManager.SelectObservationDoc(-1, MfId);
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
    #endregion

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.MemberFileObs).ToString());
    }

    private void SetMessage(string Msg)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;
    }
    #endregion
}
