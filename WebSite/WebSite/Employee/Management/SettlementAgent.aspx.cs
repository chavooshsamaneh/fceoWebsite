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

public partial class Employee_Employee_SettlementAgent : System.Web.UI.Page
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
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.SettlementAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDisActive.Enabled = per.CanEdit;
            btnDisActive2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            GridViewSettlementAgent.ClientVisible = per.CanView;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDisActive"] != null)
            this.btnDisActive.Enabled = this.btnDisActive2.Enabled = (bool)this.ViewState["BtnDisActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = GridViewSettlementAgent.ClientVisible = (bool)this.ViewState["BtnView"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnDisActive_Click(object sender, EventArgs e)
    {
        if (GridViewSettlementAgent.FocusedRowIndex > -1)
        {
            DataRow SettlRow = GridViewSettlementAgent.GetDataRow(GridViewSettlementAgent.FocusedRowIndex);
            int StlAgentId = int.Parse(SettlRow["StlAgentId"].ToString());
            DisAcStive(StlAgentId);
        }
    }


    protected void btnActive_Click(object sender, EventArgs e)
    {
        if (GridViewSettlementAgent.FocusedRowIndex > -1)
        {
            DataRow SettlRow = GridViewSettlementAgent.GetDataRow(GridViewSettlementAgent.FocusedRowIndex);
            int StlAgentId = int.Parse(SettlRow["StlAgentId"].ToString());            
            try
            {
                TSP.DataManager.SettlementAgentManager SettlementAgentManager = new TSP.DataManager.SettlementAgentManager();
                SettlementAgentManager.FindByCode(StlAgentId);
                if (SettlementAgentManager.Count > 0)
                {
                    SettlementAgentManager[0].BeginEdit();

                    SettlementAgentManager[0]["InActive"] = 0;
                    SettlementAgentManager[0].EndEdit();

                    int cn = SettlementAgentManager.Save();
                    if (cn > 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد.";
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                    }
                    GridViewSettlementAgent.DataBind();
                }
            }
            catch (Exception err)
            {
                SetError(err);
            }
        }
    }
    
    protected void btnResetSave_Click(object sender, EventArgs e)
    {
        int StlId = -1;
        string RsType = "";

        if (GridViewSettlementAgent.FocusedRowIndex > -1)
        {
            DataRow row = GridViewSettlementAgent.GetDataRow(GridViewSettlementAgent.FocusedRowIndex);
            if (row != null)
                StlId = (int)row["StlAgentId"];
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                return;
            }
            RsType = ((int)TSP.DataManager.ResetPasswordType.Settlement).ToString();

        }
        if (StlId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

        }
        else
            Response.Redirect("~/Users/ResetPassword.aspx?ID=" + Utility.EncryptQS(StlId.ToString()) + "&Type=" + Utility.EncryptQS(RsType));
        #region ResetPass
        #endregion

    }

    protected void btnTempPass_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        try
        {
            int StlId = -1;

            DevExpress.Web.ASPxButton f = (DevExpress.Web.ASPxButton)sender;
            if (GridViewSettlementAgent.FocusedRowIndex > -1)
            {
                DataRow row = GridViewSettlementAgent.GetDataRow(GridViewSettlementAgent.FocusedRowIndex);
                StlId = (int)row["StlAgentId"];
            }
            if (StlId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
            }
            else
            {
                LoginManager.FindByMeIdUltId(StlId, (int)TSP.DataManager.UserType.Settlement);
                if (LoginManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(LoginManager[0]["NeedTempPass"]) && Convert.ToBoolean(LoginManager[0]["NeedTempPass"]) && (f.ID == "btnActiveTempPass" || f.ID == "btnActiveTempPass2"))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "قبلا برای این شخص رمز یکبار عبور فعال شده است";
                        return;
                    }
                    if (!Utility.IsDBNullOrNullValue(LoginManager[0]["NeedTempPass"]) && !Convert.ToBoolean(LoginManager[0]["NeedTempPass"]) && (f.ID == "btnInActiveTempPass" || f.ID == "btnInActiveTempPass2"))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "برای این شخص رمز یکبار عبور غیر فعال است";
                        return;
                    }
                    if (Utility.IsDBNullOrNullValue(LoginManager[0]["MobileNo"]) && (f.ID == "btnActiveTempPass" || f.ID == "btnActiveTempPass2"))
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = " برای این شخص شماره تلفن همراه معتبر ثبت نشده است";
                        return;
                    }
                    else
                    {
                        LoginManager[0].BeginEdit();
                        if (f.ID == "btnActiveTempPass" || f.ID == "btnActiveTempPass2")
                            LoginManager[0]["NeedTempPass"] = true;
                        if (f.ID == "btnInActiveTempPass" || f.ID == "btnInActiveTempPass2")
                            LoginManager[0]["NeedTempPass"] = false;
                        LoginManager[0]["UserId2"] = Utility.GetCurrentUser_UserId();
                        LoginManager[0]["ModifiedDate"] = DateTime.Now;
                        LoginManager[0].EndEdit();
                        if (LoginManager.Save() > 0)
                        {
                            GridViewSettlementAgent.DataBind();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "ذخیره انجام شد. برای شماره تلفن همراه" + LoginManager[0]["MobileNo"].ToString();
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                        }
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                }
            }

        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int StlAgentId = -1;
        int FocucedIndex = GridViewSettlementAgent.FocusedRowIndex;

        if (FocucedIndex > -1)
        {
            DataRow row = GridViewSettlementAgent.GetDataRow(FocucedIndex);
            StlAgentId = (int)(row["StlAgentId"]);
        }
        if (StlAgentId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                StlAgentId = -1;
                Response.Redirect("AddSettlementAgent.aspx?StlAgntId=" + Utility.EncryptQS(StlAgentId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("AddSettlementAgent.aspx?StlAgntId=" + Utility.EncryptQS(StlAgentId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }
        }
    }

    private void DisAcStive(int StlAgentId)
    {
        try
        {
            TSP.DataManager.SettlementAgentManager SettlementAgentManager = new TSP.DataManager.SettlementAgentManager();
            SettlementAgentManager.FindByCode(StlAgentId);
            if (SettlementAgentManager.Count > 0)
            {
                SettlementAgentManager[0].BeginEdit();

                SettlementAgentManager[0]["InActive"] = 1;
                SettlementAgentManager[0].EndEdit();

                int cn = SettlementAgentManager.Save();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                }
                GridViewSettlementAgent.DataBind();
            }
        }
        catch (Exception err)
        {
            SetError(err);
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
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
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
            else if (se.Number == 2627)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
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
    #endregion

}
