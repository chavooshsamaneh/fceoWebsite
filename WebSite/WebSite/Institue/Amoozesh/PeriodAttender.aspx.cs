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

public partial class Institue_Amoozesh_PeriodAttender : System.Web.UI.Page
{
    #region Properties
    int PPId
    {
        set
        {
            HiddenPPId.Value = value.ToString();
        }
        get
        {
            return Convert.ToInt32(HiddenPPId.Value);
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PPId"]))
            {
                Response.Redirect("Period.aspx");
            }
            string PPID = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"]).ToString());

            if (string.IsNullOrEmpty(PPID))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            else
            {
                PPId = Convert.ToInt32(PPID);
                ObjdsPeriodRegister.SelectParameters["PPId"].DefaultValue = PPId.ToString();
                FillPeriod( PPId);
            }
          
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int PRId = -2;
        if (GridViewPeriodRegister.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
            PRId = (int)row["PRId"];
        }
        if (PRId == -1)
        {
            ShowMessage("لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        InActivePeriodRegister(PRId);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Period.aspx");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (!CheckIfCanNew(PPId))
        {
            return;
        }
        Response.Redirect("AddPeriodAttenders.aspx?PgMd=" + Utility.EncryptQS("NewRegister") + "&PRId=" + Utility.EncryptQS("-1") + "&PrPg=" + Utility.EncryptQS("Periods") + "&PPId=" + Utility.EncryptQS(PPId.ToString()));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int PRId = -1;
        if (GridViewPeriodRegister.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
            PRId = (int)row["PRId"];
        }
        if (PRId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        if (!CheckIfCanEdit(PRId))
            return;
        Response.Redirect("AddPeriodAttenders.aspx?PgMd=" + Utility.EncryptQS("Edit") + "&PRId=" + Utility.EncryptQS(PRId.ToString()) + "&PrPg=" + Utility.EncryptQS("Periods") + "&PPId=" + Utility.EncryptQS(PPId.ToString()));
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int PRId = -1;
        if (GridViewPeriodRegister.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
            PRId = (int)row["PRId"];
        }
        if (PRId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        Response.Redirect("AddPeriodAttenders.aspx?PgMd=" + Utility.EncryptQS("View") + "&PRId=" + Utility.EncryptQS(PRId.ToString()) + "&PrPg=" + Utility.EncryptQS("Periods") + "&PPId=" + Utility.EncryptQS(PPId.ToString()));
    }

    protected void GridViewAccountingDetails_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["AccountingId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "PeriodAtender";

        GridViewExporter.WriteXlsToResponse(true);
    }
    #endregion

    #region Methods
    private void InActivePeriodRegister(int PRId)
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        PeriodRegisterManager.FindByCode(PRId);
        if (PeriodRegisterManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return;
        }
        PeriodRegisterManager[0].BeginEdit();
        PeriodRegisterManager[0]["InActive"] = 1;
        PeriodRegisterManager[0].EndEdit();
        PeriodRegisterManager.Save();
        GridViewPeriodRegister.DataBind();
        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private Boolean CheckIfCanEdit(int PRId)
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        PeriodRegisterManager.FindByCode(PRId);
        if (PeriodRegisterManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }
        if (Convert.ToInt32(PeriodRegisterManager[0]["PaymentType"]) == (int)TSP.DataManager.PeriodRegisterPaymentType.EPayment)
        {
            ShowMessage("امکان ویرایش ثبت نام هایی که از طریق پرداخت الکترونیکی انجام شده است وجود ندارد.");
            return false;
        }
        if (Convert.ToInt32(PeriodRegisterManager[0]["InActive"]) == 1)
        {
            ShowMessage("امکان ویرایش ثبت نام لغو شده وجود ندارد.");
            return false;
        }
        if (Convert.ToInt32(PeriodRegisterManager[0]["Status"]) != (int)TSP.DataManager.PeriodPresentStatus.PeriodRegister)
        {
            ShowMessage("امکان ویرایش این دوره وجود ندارد.وضعیت دوره بایستی ''ثبت نام'' باشد");
            return false;
        }
        return true;
    }

    private Boolean CheckIfCanNew(int PPId)
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }
        if (Convert.ToInt32(PeriodPresentManager[0]["Status"]) != (int)TSP.DataManager.PeriodPresentStatus.PeriodRegister)
        {
            ShowMessage("امکان ثبت نام در این دوره وجود ندارد.وضعیت دوره بایستی ''ثبت نام'' باشد");
            return false;
        }
        return true;
    }

    private void FillPeriod(int PPId)
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count == 1)
        {
            lblPeriodTitle.Text = PeriodPresentManager[0]["PeriodTitle"].ToString();
            lblPPCode.Text = PeriodPresentManager[0]["PPCode"].ToString();
            lblCapacity.Text = PeriodPresentManager[0]["Capacity"].ToString();
            lblStartDate.Text = PeriodPresentManager[0]["StartDate"].ToString();
            lblEndDate.Text = PeriodPresentManager[0]["EndDate"].ToString();
            lblStartRegisterDate.Text = PeriodPresentManager[0]["StartRegisterDate"].ToString();
            lblEndRegisterDate.Text = PeriodPresentManager[0]["EndRegisterDate"].ToString();
        }
    }
    #endregion
}