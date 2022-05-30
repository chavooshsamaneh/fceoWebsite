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

public partial class Members_Documents_MemberExam : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["MFId"] == null || Request.QueryString["PgMd"] == null)
        {
            Response.Redirect("MemberFiles.aspx");
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            HiddenFieldExam["PrePageMode"] = Request.QueryString["PgMd"];
            HiddenFieldExam["MFId"] = Request.QueryString["MFId"];
            string MFId = Utility.DecryptQS(HiddenFieldExam["MFId"].ToString());

            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DocMemberFileManager.FindByCode(int.Parse(MFId), 0);
            if (DocMemberFileManager.Count == 1)
            {
                string MeId = DocMemberFileManager[0]["MeId"].ToString();
                ObjdsExam.SelectParameters[0].DefaultValue = MFId;
                ObjdsExam.SelectParameters["MeId"].DefaultValue = MeId;
                HiddenFieldExam["MeId"] = Utility.EncryptQS(MeId);             
                CheckMenueChange(int.Parse(MFId), int.Parse(MeId));
            }
            else
            {
                Response.Redirect("MemberFiles.aspx");
            }

            CheckPermitionForEdit(int.Parse(MFId));

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["btnActive"] = btnInActive.Enabled;


        }

        MemberInfoUserControl1.MeId = Utility.GetCurrentUser_MeId();
        MemberInfoUserControl1.MemberInfoUserControRequester = (int)TSP.DataManager.MemberInfoUserControRequester.DocMemberFile;

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnActive"] != null)
            btnActive.Enabled = btnActive2.Enabled = this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["btnActive"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int MfId = -1;
        int focucedIndex = GridViewMemberExam.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewMemberExam.GetDataRow(focucedIndex);
            MfId = (int)row["MfId"];
            if (HiddenFieldExam["MFId"] != null)
            {
                int CurrentMFId = int.Parse(Utility.DecryptQS(HiddenFieldExam["MFId"].ToString()));
                if (CurrentMFId == MfId)
                {
                    if (CheckPermitionForEdit(CurrentMFId))
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
        NextPage("View");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
              && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            Response.Redirect("AddMemberFiles.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&MeId=" + HiddenFieldExam["MeId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString()
                  + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
        else
            Response.Redirect("AddMemberFiles.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&MeId=" + HiddenFieldExam["MeId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString());
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {

        try
        {
            if (GridViewMemberExam.FocusedRowIndex > -1)
            {
                DataRow dr = GridViewMemberExam.GetDataRow(GridViewMemberExam.FocusedRowIndex);
                if (dr != null)
                {
                    if (!Convert.ToBoolean(dr["InactiveValue"]))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "رکورد انتخاب شده فعال می باشد";
                        return;
                    }
                    int MFId = int.Parse(Utility.DecryptQS(HiddenFieldExam["MFId"].ToString()));
                    int SelectedMfId = int.Parse(dr["MfId"].ToString());
                    int MExmDId = int.Parse(dr["MExmDId"].ToString());
                    Active(MExmDId, MFId);
                }
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
            if (Utility.ShowExceptionError())
            {
                this.LabelWarning.Text += err.Message;
            }

        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (GridViewMemberExam.FocusedRowIndex > -1)
        {
            DataRow MemberExamRow = GridViewMemberExam.GetDataRow(GridViewMemberExam.FocusedRowIndex);
            if (MemberExamRow != null)
            {
                if (Convert.ToBoolean(MemberExamRow["InactiveValue"]))
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "رکورد انتخاب شده غیرفعال می باشد";
                    return;
                }
                int MFId = int.Parse(Utility.DecryptQS(HiddenFieldExam["MFId"].ToString()));
                int MExmDId = int.Parse(MemberExamRow["MExmDId"].ToString());
                int SelectedMfId = int.Parse(MemberExamRow["MfId"].ToString());
                if (MFId == SelectedMfId)
                    Delete(MExmDId);
                else
                    InsertInActive(MExmDId, MFId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.DocMemberExam), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile));
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
            case "JobConfirmition":
                Response.Redirect("MemberJobConfirm.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "JobHistory":
                Response.Redirect("MemberJobHistory.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&DocType=" + Utility.EncryptQS("0") + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attachment":
                Response.Redirect("DocumentAttachment.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Major":
                Response.Redirect("AddMemberFile.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&MeId=" + HiddenFieldExam["MeId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "MeDatail":
                Response.Redirect("DocResponsibility.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Periods":
                Response.Redirect("MemberPeriods.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&MeId=" + HiddenFieldExam["MeId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Capacity":
                Response.Redirect("MemberDocCapacity.aspx?MFId=" + HiddenFieldExam["MFId"].ToString() + "&MeId=" + HiddenFieldExam["MeId"].ToString() + "&PgMd=" + HiddenFieldExam["PrePageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;

        }
    }    

    protected void GridViewMemberExam_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (HiddenFieldExam["MFId"] != null)
        {
            string MFId = Utility.DecryptQS(HiddenFieldExam["MFId"].ToString());
            if (e.GetValue("MFId") == null)
                return;
            string CurretnMfId = e.GetValue("MFId").ToString();
            if (MFId == CurretnMfId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        BackToManagementPage();
    }

    #endregion

    #region Methods

    private void NextPage(string Mode)
    {
        int MExmDId = -1;
        int focucedIndex = GridViewMemberExam.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewMemberExam.GetDataRow(focucedIndex);
            MExmDId = (int)row["MExmDId"];
        }
        if (MExmDId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                MExmDId = -1;
                Response.Redirect("AddMemberExam.aspx?MExmDId=" + Utility.EncryptQS(MExmDId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldExam["MFId"].ToString() + "&PrePgMd=" + HiddenFieldExam["PrePageMode"].ToString()
                      + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&PostId=" + Request.QueryString["PostId"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
            }
            else
            {
                Response.Redirect("AddMemberExam.aspx?MExmDId=" + Utility.EncryptQS(MExmDId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&MFId=" + HiddenFieldExam["MFId"].ToString() + "&PrePgMd=" + HiddenFieldExam["PrePageMode"].ToString()
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
        GridViewMemberExam.DataBind();
    }

    private void Delete(int MExmDId)
    {        
        TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager = new TSP.DataManager.DocMemberExamDetailManager();        
        try
        {
            DocMemberExamDetailManager.FindByCode(MExmDId);
            if (DocMemberExamDetailManager.Count == 1)
            {
                DocMemberExamDetailManager[0].Delete();
                if (DocMemberExamDetailManager.Save() > 0)
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
            GridViewMemberExam.DataBind();
        }
        catch (Exception err)
        {
            SetDeleteError(err);
        }
    }

    protected void Active(int TableId, int MfId)
    {
        switch (DeleteInActive(TableId, MfId))
        {
            case 0:
                this.DivReport.Visible = true;
                this.LabelWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete);
                GridViewMemberExam.DataBind();
                break;
            case 1:
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "رکورد فعال می باشد و یا در درخواست های قبل غیر فعال شده است";
                break;
            case 2:
                this.DivReport.Visible = true;
                this.LabelWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave);
                break;
        }
    }

    protected int DeleteInActive(int TableId, int ReqId)
    {
        int result = 0;  // 0 successful 1 not exist 2 error
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        RequestInActivesManager.FindByTableIdTableType(TableId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.DocMemberExam), ReqId);
        if (RequestInActivesManager.Count == 1)
        {
            RequestInActivesManager[0].Delete();
            if (RequestInActivesManager.Save() > 0)
                result = 0;
            else result = 2;
        }
        else result = 1;

        return result;
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
      =  btnActive.Enabled   = btnInActive2.Enabled = btnInActive2.Enabled = btnInActive.Enabled = btnInActive2.Enabled = WFPermission.BtnNew;
        return WFPermission.BtnEdit;
    }
    #endregion
    #endregion

}
