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

public partial class Office_OfficeInfo_OfficeLetters : System.Web.UI.Page
{
    private bool IsPageRefresh = false;
    string _Department
    {
        set
        {

            HiddenFieldOffice["Department"] = value;
        }

        get
        {
            return HiddenFieldOffice["Department"].ToString();
        }
    }
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

        this.DivReport.Visible = true;
        DivReport.Style["visibility"] = "hidden";
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["OfId"]))
            {
                Response.Redirect("~/Office/OfficeHome.aspx");
            }

            LetterMode["Mode"] = "";
            LetterId["Id"] = "";
            ViewState["PMode"] = "";

            try
            {
                OfficeId.Value = Request.QueryString["OfId"].ToString();
                OfficeRequest.Value = Request.QueryString["OfReId"].ToString();
                _Department = Utility.DecryptQS(Request.QueryString["Dprt"].ToString());
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            }
            catch (Exception)
            { }


            string OfId = Utility.DecryptQS(OfficeId.Value);
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);
            if ((string.IsNullOrEmpty(OfId)) || (string.IsNullOrEmpty(OfReId)))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            OdbOfLetters.SelectParameters["OfId"].DefaultValue = OfId;
            OdbOfLetters.SelectParameters["OfReId"].DefaultValue = OfReId;


            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            OfManager.FindByCode(int.Parse(OfId));
            if (OfManager.Count > 0)
                lblOfName.Text = OfManager[0]["OfName"].ToString();

            switch (_Department)
            {
                case "Home":
                    SetEnabled(false);
                    break;
                case "Document":
                    SetMenuItem();
                    if (!CheckPermitionForEditForDoc(int.Parse(OfReId)))
                        SetEnabled(false);
                    else SetEnabled(true);

                    TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
                    ReqManager.FindByCode(int.Parse(OfReId));
                    if (ReqManager.Count > 0)
                    {
                        if (Convert.ToBoolean(ReqManager[0]["Requester"]))//FromEmployee
                            SetEnabled(false);
                        else SetEnabled(true);
                        if (ReqManager[0]["IsConfirm"].ToString() != "0") //answered
                            SetEnabled(false);
                        else SetEnabled(true);
                    }
                    break;
                case "MemberShip":
                    SetMenuItem();
                    if (!CheckPermitionForEditForOffice(int.Parse(OfReId)))
                        SetEnabled(false);
                    else SetEnabled(true);
                    break;
            }

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
        }
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Office/OfficeRequestInsert.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value
         + "&Dprt=" + Utility.EncryptQS(_Department) + "&OfReId=" + OfficeRequest.Value);
    }
    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        Session["TblOfReImg"] = null;
        Session["MeReqUpload"] = null;
        Session["FileOfArm2"] = null;
        Session["FileOfSign2"] = null;
        string PageName = "~/Office/OfficeMembershipRequest.aspx";
        switch (_Department)
        {
            case "MemberShip":
                PageName = "~/Office/OfficeMembershipRequest.aspx";
                break;
            case "Document":
                PageName = "~/Office/OfficeRequest.aspx";
                break;
        }
        Response.Redirect(PageName + "?PostId=" + OfficeId.Value);
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        int OlId = -1;
        int OfReId = -1;
        int OfId = -1;
        string InActiveName = "";

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OlId = (int)row["OlId"];
            OfReId = (int)row["OfReId"];
            InActiveName = row["InActiveName"].ToString();
            OfId = (int)row["OfId"];
        }
        if (OlId == -1)
        {
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            OfManager.FindByCode(OfId);
            if ((bool)OfManager[0]["IsLock"] == true)
            {
                TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                string OfficeLockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), 1, 1);

                string lockers = Utility.GetFormattedObject(OfficeLockers);
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
                return;
            }

            if ((_Department == "Document" && !CheckPermitionForEditForDoc(OfReId)) || (_Department == "MemberShip" && !CheckPermitionForEditForOffice(OfReId)))
                return;

            TSP.DataManager.OfficialLetterManager LetterManager = new TSP.DataManager.OfficialLetterManager();

            LetterManager.FindByCode(OlId);
            if (LetterManager.Count == 1)
            {
                try
                {
                    int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

                    if (OfReId == CurrentOfReId)
                    {
                        LetterManager[0].Delete();
                        LetterManager.Save();
                        CustomAspxDevGridView1.DataBind();

                        DivReport.Style["visibility"] = "block";
                        this.LabelWarning.Text = "ذخیره انجام شد";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(InActiveName) && InActiveName != "فعال")
                        {
                            DivReport.Style["visibility"] = "block";
                            this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                            return;
                        }
                        InsertInActive(OlId, CurrentOfReId);
                    }
                    CheckMenuImageCurrentPage(CurrentOfReId);


                }
                catch (Exception err)
                {

                    DivReport.Style["visibility"] = "block";
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    Utility.SaveWebsiteError(err);
                }

            }
        }
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Agent":
                Response.Redirect("~/Office/OfficeInfo/OfficeAgent.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Utility.EncryptQS(_Department));
                break;
            case "Member":
                Response.Redirect("~/Office/OfficeInfo/OfficeMembers.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Utility.EncryptQS(_Department));
                break;
            case "Letters":
                Response.Redirect("~/Office/OfficeInfo/OfficeLetters.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Utility.EncryptQS(_Department));
                break;
            case "Financial":
                Response.Redirect("~/Office/OfficeInfo/OfficeFinancialStatus.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Utility.EncryptQS(_Department));
                break;
            case "Attach":
                Response.Redirect("OfficeAttachment.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Utility.EncryptQS(_Department));
                break;
            case "Group":
                Response.Redirect("OfficeGroups.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Utility.EncryptQS(_Department));
                break;
            case "Job":
                Response.Redirect("~/Office/OfficeInfo/OfficeJob.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Utility.EncryptQS(_Department));
                break;
            case "Office":
                Response.Redirect("~/Office/OfficeRequestInsert.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Utility.EncryptQS(_Department));
                break;
        }
    }
    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;
        if (OfficeRequest.Value != null)
        {
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);
            if (e.GetValue("OfReId") == null)
                return;
            string CurretnOfReId = e.GetValue("OfReId").ToString();
            if (OfReId == CurretnOfReId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }
    protected void CustomAspxDevGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        CustomAspxDevGridView1.JSProperties["cpShow"] = 1;
        int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));

        string[] Parameters = e.Parameters.Split(new char[] { ';' });
        string PgMd = Parameters[1];
        string VisibleIndex = Parameters[0];


        if (PgMd == "Edit")
        {

            DataRow row = CustomAspxDevGridView1.GetDataRow(int.Parse(VisibleIndex));
            int OlId = (int)row["OlId"];
            int OfReId = (int)row["OfReId"];


            int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
            if (OfReId == CurrentOfReId)
            {
                if ((_Department == "Document" && !CheckPermitionForEditForDoc(OfReId)) || (_Department == "MemberShip" && !CheckPermitionForEditForOffice(OfReId)))
                {
                    e.Result = "امکان ویرایش اطلاعات در این مرحله از گردش کار برای شما وجود ندارد.";
                    CustomAspxDevGridView1.JSProperties["cpError"] = 1;
                    CustomAspxDevGridView1.JSProperties["cpShow"] = 0;
                    return;
                }
            }
            else
            {
                e.Result = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                CustomAspxDevGridView1.JSProperties["cpError"] = 1;
                CustomAspxDevGridView1.JSProperties["cpShow"] = 0;
            }


        }
    }
    protected void CustomAspxDevGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        TSP.DataManager.OfficialLetterManager OffLeManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

        trans.Add(OffLeManager);
        trans.Add(WorkFlowStateManager);

        try
        {
            int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
            int OfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

            OffLeManager.FindByOfCode(OfId);

            for (int i = 0; i < OffLeManager.Count; i++)
            {
                if (OffLeManager[i]["LetterNo"].ToString() == e.NewValues["LetterNo"].ToString() && OffLeManager[i]["PageNo"].ToString() == e.NewValues["PageNo"].ToString() && OffLeManager[i]["InActiveName"].ToString() == "فعال")
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات وارد شده تکراری می باشد";
                    CustomAspxDevGridView1.CancelEdit();
                    return;
                }
            }

            DataRow dr = OffLeManager.NewRow();

            dr["OlId"] = 0;
            dr["OfReId"] = OfReId;

            dr["OfId"] = OfId;
            dr["LetterNo"] = e.NewValues["LetterNo"].ToString();
            dr["PageNo"] = int.Parse(e.NewValues["PageNo"].ToString());
            dr["Date"] = e.NewValues["Date"].ToString();
            if (e.NewValues["Description"] != null)
                dr["Description"] = e.NewValues["Description"].ToString();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;

            OffLeManager.AddRow(dr);
            trans.BeginSave();
            if (OffLeManager.Save() > 0)
            {
                int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming; ;
                if (_Department == "Document")
                    WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                else if (_Department == "MemberShip")
                    WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                WorkFlowStateManager.InsertWorkFlowStateLog(WfCode, OfReId, (int)TSP.DataManager.TableType.OfficialLetter, "ثبت اطلاعات جدید روزنامه رسمی شرکت", Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.UpdateInfo);
                trans.EndSave();
                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";
                Session["IsEdited_OffLetter"] = true;
                CustomAspxDevGridView1.DataBind();
                CustomAspxDevGridView1.JSProperties["cpMenu"] = 1;
            }
            else
            {
                trans.CancelSave();

                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            CustomAspxDevGridView1.CancelEdit();
        }
        catch (Exception err)
        {
            trans.CancelSave();
            CustomAspxDevGridView1.CancelEdit();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات تکراری می باشد";
                }
                else
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }
    protected void CustomAspxDevGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;

        int OlId = int.Parse(e.Keys["OlId"].ToString());
        int OfId = int.Parse(Utility.DecryptQS(OfficeId.Value));
        TSP.DataManager.OfficialLetterManager OffLeManager2 = new TSP.DataManager.OfficialLetterManager();

        OffLeManager2.FindByCode(OlId);
        int OfReId = int.Parse(OffLeManager2[0]["OfReId"].ToString());


        int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
        if (OfReId == CurrentOfReId)
        {
            if ((_Department == "Document" && !CheckPermitionForEditForDoc(OfReId)) || (_Department == "MemberShip" && !CheckPermitionForEditForOffice(OfReId)))
            {
                CustomAspxDevGridView1.CancelEdit();
                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "امکان ویرایش اطلاعات در این مرحله از گردش کار برای شما وجود ندارد.";
                return;
            }

            TSP.DataManager.OfficialLetterManager OffLeManager = new TSP.DataManager.OfficialLetterManager();
            TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

            trans.Add(OffLeManager);
            trans.Add(WorkFlowStateManager);

            try
            {

                OffLeManager.FindByCode(int.Parse(e.Keys["OlId"].ToString()));
                if (OffLeManager.Count == 1)
                {

                    OffLeManager[0].BeginEdit();
                    OffLeManager[0]["LetterNo"] = e.NewValues["LetterNo"].ToString();
                    OffLeManager[0]["PageNo"] = int.Parse(e.NewValues["PageNo"].ToString());
                    OffLeManager[0]["Date"] = e.NewValues["Date"].ToString();

                    OffLeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    if (e.NewValues["Description"] != null)
                        OffLeManager[0]["Description"] = e.NewValues["Description"].ToString();
                    OffLeManager[0]["ModifiedDate"] = DateTime.Now;
                    OffLeManager[0].EndEdit();
                    trans.BeginSave();
                    if (OffLeManager.Save() > 0)
                    {
                        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming; ;
                        if (_Department == "Document")
                            WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
                        else if (_Department == "MemberShip")
                            WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
                        WorkFlowStateManager.InsertWorkFlowStateLog(WfCode, OfReId, (int)TSP.DataManager.TableType.OfficialLetter, "بروزرسانی روزنامه رسمی شرکت", Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.UpdateInfo);
                        trans.EndSave();

                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";
                        Session["IsEdited_OffLetter"] = true;

                        CustomAspxDevGridView1.DataBind();
                        CustomAspxDevGridView1.CancelEdit();

                    }
                    else
                    {
                        trans.CancelSave();
                        CustomAspxDevGridView1.CancelEdit();

                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                        return;
                    }
                }
                else
                {
                    CustomAspxDevGridView1.CancelEdit();

                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                }

            }
            catch (Exception err)
            {
                trans.CancelSave();
                CustomAspxDevGridView1.CancelEdit();

                if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                {
                    System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                    if (se.Number == 2601)
                    {
                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات تکراری می باشد";
                    }
                    else
                    {
                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                    }
                }
                else
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }

        }
        else
        {
            CustomAspxDevGridView1.CancelEdit();
            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
        }

    }
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
            e.Cell.Style["direction"] = "ltr";
    }
    protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (Session["OffMenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
            Session["OffMenuArrayList"] = arr;
        }
        else
            CheckMenuImageCurrentPage(int.Parse(Utility.DecryptQS(OfficeRequest.Value)));
    }
    #endregion
    #region Methods
    void SetEnabled(bool Enabled)
    {
        btnEdit.Enabled = Enabled;
        btnEdit2.Enabled = Enabled;
        btnInActive.Enabled = Enabled;
        btnInActive2.Enabled = Enabled;
        BtnNew.Enabled = Enabled;
        BtnNew2.Enabled = Enabled;
    }  
    protected void InsertInActive(int OlId, int OfReId)
    {
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        DataRow dr = Manager.NewRow();
        dr["TableId"] = OlId;
        dr["TableType"] = (int)TSP.DataManager.TableCodes.OfficeFinancialStatus;
        dr["ReqId"] = OfReId;
        dr["ReqType"] = (int)TSP.DataManager.TableCodes.OfficeRequest;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        Manager.Save();

        CustomAspxDevGridView1.DataBind();

        this.DivReport.Visible = true;
        this.LabelWarning.Text = "ذخیره انجام شد";
    }

    protected void CheckMenuImage(int OfReId)
    {
        TSP.DataManager.OfficeAgentManager OffAgentManager = new TSP.DataManager.OfficeAgentManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficialLetterManager OffLetterManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager OffFinancialManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        TSP.DataManager.GroupDetailManager GrdManager = new TSP.DataManager.GroupDetailManager();
        TSP.DataManager.OfficeManager officeManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OfficeRequestManager officeRequestManager = new TSP.DataManager.OfficeRequestManager();



        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Agent
        arr.Add(0);//arr[1]-->Member
        arr.Add(0);//arr[2]-->Letters
        arr.Add(0);//arr[3]-->Job
        arr.Add(0);//arr[4]-->Attach
        arr.Add(0);//arr[5]-->Financial
        arr.Add(0);//arr[6]-->Office
        arr.Add(0);//arr[7]-->Group


        officeRequestManager.FindByCode(OfReId);
        if (officeRequestManager.Count > 0)
        {
            int OfId = Convert.ToInt32(officeRequestManager[0]["OfId"]);
            officeManager.FindByCode(OfId);
            if (officeManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
                arr[6] = 1;
            }
        }


        OffAgentManager.FindForDelete(OfReId);
        if (OffAgentManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        OffMemberManager.FindForDelete(OfReId, 0);
        if (OffMemberManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
            arr[1] = 1;
        }

        OffLetterManager.FindForDelete(OfReId);
        if (OffLetterManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(1, OfReId, (int)TSP.DataManager.TableCodes.OfficeRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.OfficeRequest, OfReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            arr[4] = 1;
        }
        OffFinancialManager.FindForDelete(OfReId);
        if (OffFinancialManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Height = Utility.MenuImgSize;
            arr[5] = 1;
        }

        Session["OffMenuArrayList"] = arr;
    }
    protected void CheckMenuImageCurrentPage(int OfReId)
    {
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        OffMemberManager.FindForDelete(OfReId, 0);

        if (Session["OffMenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
            if (OffMemberManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
                arr[1] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "";
                arr[1] = 0;

            }
            Session["OffMenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(OfReId);
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
            if (OffMemberManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
                arr[1] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "";
                arr[1] = 0;

            }
            Session["OffMenuArrayList"] = arr;

        }

    }
    protected void SetMenuItem()
    {
        if (Session["OffMenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];

            if ((int)arr[0] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[1] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[2] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[3] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[4] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[5] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[6] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
            }
        }
        else
        {
            CheckMenuImage(int.Parse(Utility.DecryptQS(OfficeRequest.Value)));

        }
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
            Message = "امکان ویرایش درخواست در این مرحله از گردش کار برای شما وجود ندارد";
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
                                if (FirstNmcIdType == (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId && FirstNmcId == Utility.GetCurrentUser_MeId())
                                    return true;
                                else
                                    Message = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان ویرایش درخواست برای شما وجود ندارد";
                            }
                        }
                    }
                }
            }
        }
        if (string.IsNullOrEmpty(Message))
            Message = "امکان ویرایش درخواست در این مرحله از گردش کار برای شما وجود ندارد";
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
        return false;
    }
    #endregion
}


