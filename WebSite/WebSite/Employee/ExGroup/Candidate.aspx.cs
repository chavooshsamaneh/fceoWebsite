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

public partial class Employee_ExGroup_Candidate : System.Web.UI.Page
{

    Boolean IsPageRefresh = false;
    # region Events
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

        if (!Page.IsPostBack)
        {
            try
            {
                this.DivReport.Visible = false;
                this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
                this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

                if (Request.QueryString.Count != 0)
                {
                    ExGroupPeriodId = int.Parse(Utility.DecryptQS(Request.QueryString["ExGroupPeriodId"].ToString()));
                    ObjectDataSourceCandidate.SelectParameters["ExGroupPeriodId"].DefaultValue = ExGroupPeriodId.ToString();
                    GridViewCandidate.DataBind();
                }
                else
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo="+((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());


                TSP.DataManager.Permission Per = TSP.DataManager.CandidateManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
                (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnnew.Enabled = Per.CanDelete;
                btnnew2.Enabled = Per.CanDelete;
                btnedit.Enabled = Per.CanEdit;
                btnedit2.Enabled = Per.CanEdit;
                btndel.Enabled = Per.CanNew;
                btndel2.Enabled = Per.CanNew;
                btnview.Enabled = Per.CanView;
                btnview2.Enabled = Per.CanView;
                btnExportExcel.Enabled = Per.CanView;
                btnExportExcel2.Enabled = Per.CanView;
                GridViewCandidate.Visible = Per.CanView;

                this.ViewState["btnnew"] = btnnew.Enabled;
                this.ViewState["btnedit"] = btnedit.Enabled;
                this.ViewState["btndel"] = btndel.Enabled;
                this.ViewState["btnview"] = btnview.Enabled;
                this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }
        }

        if (this.ViewState["btnnew"] != null)
            this.btnnew.Enabled = this.btnnew2.Enabled = (bool)this.ViewState["btnnew"];
        if (this.ViewState["btnedit"] != null)
            this.btnedit.Enabled = this.btnedit2.Enabled = (bool)this.ViewState["btnedit"];
        if (this.ViewState["btndel"] != null)
            this.btndel.Enabled = this.btndel2.Enabled = (bool)this.ViewState["btndel"];
        if (this.ViewState["btnview"] != null)
            this.btnview.Enabled = this.btnview2.Enabled = (bool)this.ViewState["btnview"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        NextPage("new");
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        NextPage("edit");
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        NextPage("view");
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        if (!Utility.IsDBNullOrNullValue(ExGroupPeriodId))
            Response.Redirect("ExGroupPeriodInsert.aspx?id=" + Utility.EncryptQS(ExGroupPeriodId.ToString()) + "&mode=" + Utility.EncryptQS("edit"));
        else ShowMessage("دوره مشخص نمی باشد");
    }
    protected void btndel_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        try
        {
            DataRow row = GridViewCandidate.GetDataRow(GridViewCandidate.FocusedRowIndex);
            int id = (int)row["CandidateId"];

            TSP.DataManager.CandidateManager CandidateManager = new TSP.DataManager.CandidateManager();
            CandidateManager.FindByCode(id);
            if (CandidateManager.Count == 1)
            {
                if (Convert.ToBoolean(CandidateManager[0]["InActive"]))
                {
                    ShowMessage("امکان حذف رکورد غیرفعال وجود ندارد");
                    return;
                }
                CandidateManager[0].Delete();
                int result = CandidateManager.Save();
                if (result == 1)
                {
                    GridViewCandidate.DataBind();
                    ShowMessage("حذف انجام شد");
                }
                else
                {
                    ShowMessage("خطایی در حذف انجام گرفته است");
                }
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.CandidateManager CandidateManager = new TSP.DataManager.CandidateManager();
        DataRow Row = GridViewCandidate.GetDataRow(GridViewCandidate.FocusedRowIndex);
        try
        {
            CandidateManager.FindByCode((int)Row["CandidateId"]);
            if (CandidateManager.Count == 1)
            {
                if (Convert.ToBoolean(CandidateManager[0]["InActive"]))
                {
                    ShowMessage("رکورد مورد نظر غیر فعال می باشد");
                    return;
                }
                CandidateManager[0].BeginEdit();
                CandidateManager[0]["InActive"] = 1;
                CandidateManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                CandidateManager[0]["ModifiedDate"] = DateTime.Now;
                CandidateManager[0].EndEdit();
                if (CandidateManager.Save() == 1)
                {
                    GridViewCandidate.DataBind();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                }
                else
                {
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                ShowMessage("اطلاعات توسط کاربر دیگری تغییر یافته است");
            }

        }
        catch (Exception err)
        {
            SetError(err);
        }
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Period":
                Response.Redirect("~/Employee/ExGroup/ExGroupPeriodInsert.aspx?id=" + Utility.EncryptQS(ExGroupPeriodId.ToString()) + "&mode=" + Utility.EncryptQS("edit"));
                break;
            case "Candid":
                Response.Redirect("~/Employee/ExGroup/Candidate.aspx?ExGroupPeriodId=" + Utility.EncryptQS(ExGroupPeriodId.ToString()));
                break;
        }
    }
    protected void btntemp_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "ExGroups";
        GridViewExporter.WriteXlsToResponse(true);
    }
    protected void GridViewCandidate_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (e.Parameters == "print")
        {
            Session["DataTable"] = GridViewCandidate.Columns;
            Session["DataSource"] = ObjectDataSourceCandidate;
            Session["Title"] = "لیست کاندیدهای دوره";
            GridViewCandidate.DetailRows.CollapseAllRows();
            GridViewCandidate.JSProperties["cpDoPrint"] = 1;
        }
    }
    #endregion

    #region Properties
    public int ExGroupPeriodId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HiddenFieldID["ExGroupPeriodId"].ToString()));
        }
        set
        {
            HiddenFieldID["ExGroupPeriodId"] = Utility.EncryptQS(value.ToString());
        }
    }
    #endregion

    #region Methods
    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    void SetError(Exception err)
    {
        Utility.SaveWebsiteError(err);
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
            }
            else if (se.Number == 2627)
            {
                ShowMessage("کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.RelatedDataIsNotValid));
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
        }
        else
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }
    void NextPage(string mode)
    {
        if (IsPageRefresh)
            return;
        //-----------find ExGroupCode-----
        int ExGroupCode = -1;
        TSP.DataManager.ExGroupPeriodManager ExGroupPeriodManager = new TSP.DataManager.ExGroupPeriodManager();
        ExGroupPeriodManager.FindByCode(ExGroupPeriodId);
        if (ExGroupPeriodManager.Count == 1)
            if (Utility.IsDBNullOrNullValue(ExGroupPeriodManager[0]["ExGroupCode"]))
                ExGroupCode = -1;
            else
                ExGroupCode = Convert.ToInt32(ExGroupPeriodManager[0]["ExGroupCode"]);
        if (mode == "new")//----------------------------------------
        {
            Response.Redirect("CandidateInsert.aspx?id=" + Utility.EncryptQS("-1") + "&ExGroupPeriodId=" + Utility.EncryptQS(ExGroupPeriodId.ToString())
                + "&mode=" + Utility.EncryptQS(mode) + "&ExGroupCode=" + Utility.EncryptQS(ExGroupCode.ToString()));
        }
        else
        {//---------------------------------------------------------
            TSP.DataManager.Permission Per = TSP.DataManager.CandidateManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
               (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            DataRow row = GridViewCandidate.GetDataRow(GridViewCandidate.FocusedRowIndex);
            int id = (int)row["CandidateId"];
            TSP.DataManager.CandidateManager CandidateManager = new TSP.DataManager.CandidateManager();
            if (mode == "edit")
            {
                if (!Per.CanEdit) return;
                CandidateManager.FindByCode(id);
                if (CandidateManager.Count == 1)
                {
                    if (Convert.ToBoolean(CandidateManager[0]["InActive"]))
                    {
                        ShowMessage("امکان ویرایش رکورد غیرفعال وجود ندارد");
                        return;
                    }
                }
            }
            else if (mode == "view")//---------------------------------
            {
                if (!Per.CanView) return;
            }
            if (!Utility.IsDBNullOrNullValue(mode))
                Response.Redirect("CandidateInsert.aspx?id=" + Utility.EncryptQS(id.ToString()) + "&ExGroupPeriodId=" + Utility.EncryptQS(ExGroupPeriodId.ToString())
                    + "&mode=" + Utility.EncryptQS(mode) + "&ExGroupCode=" + Utility.EncryptQS(ExGroupCode.ToString()));
        }
    }
    #endregion
}
