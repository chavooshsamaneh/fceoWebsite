using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Members_Documents_MemberJobConfirm : System.Web.UI.Page
{
    #region property
   
    string DocType
    {
        get { return HiddenFieldPage["DocType"].ToString();  }
        set { HiddenFieldPage["DocType"] = value; }
    }

   
    string MFId
    {
        get { return HiddenFieldPage["MFId"].ToString(); }
        set { HiddenFieldPage["MFId"] = value; }
    }
  
    string MemberName
    {
        get { return HiddenFieldPage["MemberName"].ToString(); }
        set { HiddenFieldPage["MemberName"] = value; }
    }

   
    string PrePgMd
    {
        get { return HiddenFieldPage["PrePgMd"].ToString(); }
        set { HiddenFieldPage["PrePgMd"] = value; }
    }


    string MeId
    {
        set { HiddenFieldPage["MeId"] = value; }
        get { return HiddenFieldPage["MeId"].ToString() ; }
    }
    #endregion

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
        if (Request.QueryString["MFId"] == null || Request.QueryString["PgMd"] == null || Request.QueryString["DocType"] == null)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

       

        PrePgMd = Utility.DecryptQS(Request.QueryString["PgMd"]);
        MFId = Utility.DecryptQS(Request.QueryString["MFId"]);
        MemberName = "";
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.FindByCode(int.Parse(MFId), 0);
       
        if (DocMemberFileManager.Count == 1)
        {
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MeId = DocMemberFileManager[0]["MeId"].ToString();

            MemberManager.FindByCode(int.Parse(MeId));
            if (MemberManager.Count == 1)
            {
                MemberName = "پروانه اشتغال به کار: " + MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
            }
            CheckMenueChange(int.Parse(MFId), int.Parse(MeId));
        }
        else
        {
            Response.Redirect("MemberFiles.aspx");
        }

        CheckPermitionForEdit(int.Parse(MFId));

        MemberInfoUserControl1.MeId = Utility.GetCurrentUser_MeId();
        MemberInfoUserControl1.MemberInfoUserControRequester = (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile;

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["btnPrint"];

        ObjectDataSourceJobConfirm.SelectParameters["MfId"].DefaultValue = MFId;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
         NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int MfId = -1;
        int focucedIndex = GridViewJobCon.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewJobCon.GetDataRow(focucedIndex);
            MfId = (int)row["MfId"];
            if (!Utility.IsDBNullOrNullValue(MFId))
            {

                if (int.Parse(MFId) == MfId)
                {
                    if (CheckPermitionForEdit(int.Parse(MFId)))
                    {
                          NextPage("Edit");
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "شما قادر به ویرایش اطلاعات نمی باشید.";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                }
            }
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
         NextPage("View");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
             && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            Response.Redirect("AddMemberFile.aspx?MFId=" + MFId + "&MeId=" + MeId + "&PgMd=" + PrePgMd
                  + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
        else
            Response.Redirect("AddMemberFile.aspx?MFId=" + MFId + "&MeId=" + MeId + "&PgMd=" + PrePgMd);
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (GridViewJobCon.FocusedRowIndex > -1)
        {
            DataRow MemberJobConRow = GridViewJobCon.GetDataRow(GridViewJobCon.FocusedRowIndex);
            if (MemberJobConRow != null)
            {

                if (Convert.ToInt32(MemberJobConRow["InActiveStatus"]) == 1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ردیف انتخاب شده غیرفعال می باشد.";
                    return;
                }
                int JobConfId = int.Parse(MemberJobConRow["JobConfId"].ToString());
                int SelectedMfId = int.Parse(MemberJobConRow["MfId"].ToString());
                if (int.Parse(MFId) == SelectedMfId)
                    Delete(JobConfId);
                else

                    InsertInActive(JobConfId, int.Parse(MFId), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.DocMemberFileJobConfirmatio), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile));
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Exam":
                Response.Redirect("MemberExam.aspx?MFId=" +Utility.EncryptQS( MFId) + "&PgMd=" + Utility.EncryptQS(PrePgMd) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "JobHistory":
                Response.Redirect("MemberJobHistory.aspx?MFId=" + Utility.EncryptQS(MFId) + "&PgMd=" + Utility.EncryptQS(PrePgMd) + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attachment":
                Response.Redirect("DocumentAttachment.aspx?MFId=" + Utility.EncryptQS(MFId) + "&PgMd=" + Utility.EncryptQS(PrePgMd) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Major":
                Response.Redirect("AddMemberFile.aspx?MFId=" + Utility.EncryptQS(MFId) + "&MeId=" + Utility.EncryptQS(MeId) + "&PgMd=" + Utility.EncryptQS(PrePgMd) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "MeDetail":
                Response.Redirect("DocResponsibility.aspx?MFId=" + Utility.EncryptQS(MFId) + "&PgMd=" + Utility.EncryptQS(PrePgMd) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Periods":
                Response.Redirect("MemberPeriods.aspx?MFId=" + Utility.EncryptQS(MFId) + "&MeId=" + Utility.EncryptQS(MeId) + "&PgMd=" + Utility.EncryptQS(PrePgMd) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Capacity":
                Response.Redirect("MemberDocCapacity.aspx?MFId=" + Utility.EncryptQS(MFId) + "&MeId=" + Utility.EncryptQS(MeId) + "&PgMd=" + Utility.EncryptQS(PrePgMd) + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;

        }
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        BackToManagementPage();
    }

    #endregion

    #region method
    private void Delete(int JobConfId)
    {
        TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();
        try
        {
            DocMemberFileJobConfirmationManager.FindByCode(JobConfId);
            if (DocMemberFileJobConfirmationManager.Count == 1)
            {
                DocMemberFileJobConfirmationManager[0].Delete();
                int cn = DocMemberFileJobConfirmationManager.Save();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
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
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
            GridViewJobCon.DataBind();
           
        }
        catch (Exception err)
        {
            SetDeleteError(err);
        }
    }

    private void NextPage(string Mode)
    {
        int JobConfId = -1;
        int focucedIndex = GridViewJobCon.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewJobCon.GetDataRow(focucedIndex);
            JobConfId = (int)row["JobConfId"];
        }
        if (JobConfId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                JobConfId = -1;
                Response.Redirect("AddMemberJobConfirm.aspx?JobConfId=" + Utility.EncryptQS(JobConfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + Utility.EncryptQS(MFId) + "&PrePgMd=" + Utility.EncryptQS(PrePgMd) + "&DocType=" + Request.QueryString["DocType"]
                      + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
            }
            else
            {
                Response.Redirect("AddMemberJobConfirm.aspx?JobConfId=" + Utility.EncryptQS(JobConfId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + Utility.EncryptQS(MFId) + "&PrePgMd=" + Utility.EncryptQS(PrePgMd) + "&DocType=" + Request.QueryString["DocType"]
                      + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
            }
        }
    }

    protected void InsertInActive(int TableId, int ReqId, int TableType, int ReTableType)
    {
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
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
        if (Manager.Save() > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد.";
        }
        GridViewJobCon.DataBind();
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

    private void SetError(Exception err)
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

    private void BackToManagementPage()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
             && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))//!string.IsNullOrEmpty(MfId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string PostId = Server.HtmlDecode(Request.QueryString["PostId"]);
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"]);
            Response.Redirect("MemberFiles.aspx?PostId=" + PostId + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("MemberFiles.aspx");
        }
    }

    private void CheckMenueChange(int MfId, int MeId)
    {
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.DocMemberExamManager DocMemberExamManager = new TSP.DataManager.DocMemberExamManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        ProjectJobHistoryManager.FindForDelete(0, MfId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Documents));
        if (ProjectJobHistoryManager.Count > 0)
        {
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("JobHistory")].Image.Url = "~/Images/icons/Check.png";
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("JobHistory")].Image.Width = Utility.MenuImgSize;
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("JobHistory")].Image.Height = Utility.MenuImgSize;
        }
        DocMemberExamManager.SelectById(MfId, MeId);
        if (DocMemberExamManager.Count > 0)
        {
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("Exam")].Image.Url = "~/Images/icons/Check.png";
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("Exam")].Image.Width = Utility.MenuImgSize;
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("Exam")].Image.Height = Utility.MenuImgSize;
        }
        DataTable dtRes = DocMemberFileDetailManager.SelectById(MfId, MeId, -1);
        if (dtRes.Rows.Count > 0)
        {
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("MeDetail")].Image.Url = "~/Images/icons/Check.png";
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("MeDetail")].Image.Width = Utility.MenuImgSize;
            MenuMemberFile.Items[MenuMemberFile.Items.IndexOfName("MeDetail")].Image.Height = Utility.MenuImgSize;
        }

    }

    #region CheckWFPermissions
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WFPermission WFPermission = DocMemberFileManager.CheckWFEditPermissionForMemberPortal(TableId, "");
        BtnNew.Enabled = btnNew2.Enabled
          = btnEdit.Enabled = btnEdit2.Enabled
          = btnInActive.Enabled = btnInActive2.Enabled = btnInActive.Enabled = btnInActive2.Enabled = WFPermission.BtnNew;
        return WFPermission.BtnEdit;
    }
    #endregion

    #endregion
}