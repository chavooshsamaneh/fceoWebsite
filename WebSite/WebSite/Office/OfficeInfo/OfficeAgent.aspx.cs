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

public partial class Office_OfficeInfo_OfficeAgent : System.Web.UI.Page
{
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

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            SetKeys();

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
        }
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit1.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew1.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView1.Enabled = (bool)this.ViewState["BtnView"];

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int OagId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OagId = (int)row["OagId"];
        }
        if (OagId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }

        Response.Redirect("OfficeAgentShow.aspx?OagId=" + Utility.EncryptQS(OagId.ToString()) + "&OfId=" + OfficeId.Value
            + "&OfReId=" + OfficeRequest.Value + "&PageMode=" + PgMode.Value + "&Dprt=" + Mode.Value + "&APageMode=" + Utility.EncryptQS("View"));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Office/OfficeRequestInsert.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value
         + "&Dprt=" + Mode.Value + "&OfReId=" + OfficeRequest.Value);
    }
    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        Session["TblOfReImg"] = null;
        Session["MeReqUpload"] = null;
        Session["FileOfArm2"] = null;
        Session["FileOfSign2"] = null;
        string Dprt = Utility.DecryptQS(Mode.Value);
        string PageName = "~/Office/OfficeMembershipRequest.aspx";
        switch (Dprt)
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
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeAgentShow.aspx?OagId=" + Utility.EncryptQS("-1") + "&APageMode=" + Utility.EncryptQS("New")
            + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&Dprt=" + Mode.Value);

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int OagId = -1;
        int OfReId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OagId = (int)row["OagId"];
            OfReId = (int)row["OfReId"];

        }
        if (OagId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();
            AgentManager.FindByCode(OagId);
            if (AgentManager.Count == 1)
            {
                int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
                if (OfReId == CurrentOfReId)
                {
                    string Department = Utility.DecryptQS(Mode.Value);
                    if ((Department == "Document" && !CheckPermitionForEditForDoc(OfReId)) || (Department == "MemberShip" && !CheckPermitionForEditForOffice(OfReId)))
                        return;

                    Response.Redirect("OfficeAgentShow.aspx?OagId=" + Utility.EncryptQS(OagId.ToString()) + "&APageMode=" + Utility.EncryptQS("Edit") + "&OfId=" + OfficeId.Value
                        + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&Dprt=" + Mode.Value);

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                }
            }
        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        //TSP.DataManager.OfficeManager OfManager = Session["OfficeManager"] as TSP.DataManager.OfficeManager;
        //if (OfManager == null)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
        //    return;
        //}
        //if ((bool)OfManager[0]["IsLock"] == true)
        //{
        //    string lockers = Utility.GetFormattedObject(Session["OfficeLockers"]);
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
        //    return;
        //}


        try
        {
            int OagId = -1;
            int OfReId = -1;
            int OfId = -1;
            string InActiveName = "";

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                OagId = (int)row["OagId"];
                OfId = (int)row["OfId"];
                OfReId = (int)row["OfReId"];
                InActiveName = row["InActiveName"].ToString();

            }
            if (OagId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
                return;
            }
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

            string Department = Utility.DecryptQS(Mode.Value);
            if ((Department == "Document" && !CheckPermitionForEditForDoc(OfReId)) || (Department == "MemberShip" && !CheckPermitionForEditForOffice(OfReId)))
                return;

            TSP.DataManager.OfficeAgentManager AgentManager = new TSP.DataManager.OfficeAgentManager();
            AgentManager.FindByCode(OagId);
            if (AgentManager.Count == 1)
            {
                int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

                if (OfReId == CurrentOfReId)
                {
                    AgentManager[0].Delete();
                    AgentManager.Save();
                    CustomAspxDevGridView1.DataBind();


                }
                else
                {
                    if (!string.IsNullOrEmpty(InActiveName) && InActiveName != "فعال")
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                        return;
                    }

                    InsertInActive(OagId, CurrentOfReId);
                }

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                CheckMenuImageCurrentPage(CurrentOfReId);

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان تغییر اطلاعات وجود ندارد";
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";

        }
    }

    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        string PMode = Utility.DecryptQS(Mode.Value);
        if (!string.IsNullOrEmpty(PMode))
        {
            if (PMode == "Document")
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
        }
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Agent":
                Response.Redirect("~/Office/OfficeInfo/OfficeAgent.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Mode.Value);
                break;
            case "Member":
                Response.Redirect("~/Office/OfficeInfo/OfficeMembers.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Mode.Value);
                break;
            case "Letters":
                Response.Redirect("~/Office/OfficeInfo/OfficeLetters.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Mode.Value);
                break;
            case "Financial":
                Response.Redirect("~/Office/OfficeInfo/OfficeFinancialStatus.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Mode.Value);
                break;
            case "Attach":
                Response.Redirect("OfficeAttachment.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Mode.Value);
                break;
            case "Group":
                Response.Redirect("OfficeGroups.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Mode.Value);
                break;
            case "Job":
                Response.Redirect("~/Office/OfficeInfo/OfficeJob.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Mode.Value);
                break;
            case "Office":
                Response.Redirect("~/Office/OfficeRequestInsert.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value
                    + "&Dprt=" + Mode.Value);
                break;
        }
    }
    protected void InsertInActive(int OagId, int OfReId)
    {
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        DataRow dr = Manager.NewRow();
        dr["TableId"] = OagId;
        dr["TableType"] = (int)TSP.DataManager.TableCodes.OfficeAgent;
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
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Tel")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Tel")
            e.Cell.Style["direction"] = "ltr";
    }

    void SetKeys()
    {
        if (string.IsNullOrEmpty(Request.QueryString["OfId"]) || string.IsNullOrEmpty(Request.QueryString["Dprt"]))
        {
            Response.Redirect("~/Office/OfficeHome.aspx");
        }
        try
        {
            OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
            OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();
            Mode.Value = Server.HtmlDecode(Request.QueryString["Dprt"]).ToString();
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();
        }
        catch (Exception)
        { }

        string OfId = Utility.DecryptQS(OfficeId.Value);
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string Department = Utility.DecryptQS(Mode.Value);

        if (string.IsNullOrEmpty(OfId) || string.IsNullOrEmpty(OfReId) || string.IsNullOrEmpty(Department))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        ODbOfAgent.SelectParameters[0].DefaultValue = OfId;
        ODbOfAgent.SelectParameters[1].DefaultValue = OfReId;

        //TSP.DataManager.OfficeManager OfManager = Session["OfficeManager"] as TSP.DataManager.OfficeManager;
        //if (OfManager == null)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
        //    return;
        //}

        SetMode(Department, OfReId);
    }
    void SetMode(string Department, string OfReId)
    {
        switch (Department)
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
                    if (Convert.ToBoolean(ReqManager[0]["Requester"]) || ReqManager[0]["IsConfirm"].ToString() != "0")//FromEmployee
                        SetEnabled(false);
                    else SetEnabled(true);

                    if (ReqManager[0]["IsConfirm"].ToString() == "0") //Not Answered
                    {
                        ODbOfAgent.SelectParameters[3].DefaultValue = "2";
                    }
                }
                break;
            case "MemberShip":
                SetMenuItem();
                if (!CheckPermitionForEditForOffice(int.Parse(OfReId)))
                    SetEnabled(false);
                else SetEnabled(true);
                break;
        }
    }
    void SetEnabled(bool Enabled)
    {
        btnEdit.Enabled = Enabled;
        btnEdit1.Enabled = Enabled;
        btnInActive.Enabled = Enabled;
        btnInActive2.Enabled = Enabled;
        btnNew.Enabled = Enabled;
        btnNew1.Enabled = Enabled;
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
        {
            Message = "امکان ویرایش درخواست در این مرحله از گردش کار برای شما وجود ندارد";
            this.DivReport.Visible = true;
            this.LabelWarning.Text = Message;
        }
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
        {
            Message = "امکان ویرایش درخواست در این مرحله از گردش کار برای شما وجود ندارد";
            this.DivReport.Visible = true;
            this.LabelWarning.Text = Message;
            return false;
        }
        return false;
    }
}
