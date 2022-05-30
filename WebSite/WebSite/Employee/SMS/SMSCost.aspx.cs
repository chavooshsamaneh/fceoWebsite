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

public partial class Employee_SMS_SMSCost : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
     
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.SmsCostManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;

            this.ViewState["BtnDelete"] = BtnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }
        if (this.ViewState["BtnDelete"] != null)
            this.BtnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        LoadCredit();
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        TSP.DataManager.SmsRecieverManager SmsRecieverManager = new TSP.DataManager.SmsRecieverManager();
        DataRow RowSMSCost = GridViewCost.GetDataRow(GridViewCost.FocusedRowIndex);
        string StartDate = RowSMSCost["StartDate"].ToString();
        SmsRecieverManager.SelectSmsByDate(StartDate, Utility.GetDateOfToday());
        if (SmsRecieverManager.Count > 0)
        {
            this.DivReport.Visible = true;
            LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
        }
        else
        {
            //             DataRow RowSMSCost = GridViewCost.GetDataRow(GridViewCost.FocusedRowIndex);
            int SMSCostId = int.Parse(RowSMSCost["CostId"].ToString());
            DeleteSMSCost(SMSCostId);
        }
        //TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
        //DataTable dataTableSMS = SmsManager.SelectSMSJoinSMSCost(-1);
        //if (dataTableSMS.Rows.Count > 0)
        //{
        //    this.DivReport.Visible = true;
        //    LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
        //}
        //else
        //{
        //    DataRow RowSMSCost = GridViewCost.GetDataRow(GridViewCost.FocusedRowIndex);
        //    int SMSCostId = int.Parse(RowSMSCost["CostId"].ToString());
        //    DeleteSMSCost(SMSCostId);
        //}
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int CostId = -1;
        if (GridViewCost.FocusedRowIndex > -1)
        {
            DataRow row = GridViewCost.GetDataRow(GridViewCost.FocusedRowIndex);
            CostId = (int)row["CostId"];
        }
        if (CostId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                CostId = -1;
                Response.Redirect("~/Employee/SMS/SMSCostInsert.aspx?CostId=" + Utility.EncryptQS(CostId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("~/Employee/SMS/SMSCostInsert.aspx?CostId=" + Utility.EncryptQS(CostId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));
            }
        }
    }

    private void DeleteSMSCost(int CostId)
    {
        try
        {
            TSP.DataManager.SmsCostManager SmsCostManager = new TSP.DataManager.SmsCostManager();
            SmsCostManager.FindByCode(CostId);
            if (SmsCostManager.Count > 0)
            {
                SmsCostManager[0].Delete();
                int cn = SmsCostManager.Save();
                if (cn > 0)
                {
                    GridViewCost.DataBind();
                    this.DivReport.Visible = true;
                    LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    LabelWarning.Text = "خطایی در ذخیره صورت گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                LabelWarning.Text = "اطلاعات انتخاب شده توسط کاربر دیگری تغییر یافته است";
            }
        }
        catch (Exception ex)
        {
            SetError(ex);
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

    void LoadCredit()
    {
        try
        {
            string[] SmsInfo = new string[4];
            SmsInfo = Utility.GetMagfaWebServiceInformation();
            string UserName = SmsInfo[0];
            string PassWord = SmsInfo[1];
            string DomainName = SmsInfo[3];

            SMSMagfa.SoapSmsQueuableImplementationService ssq = new SMSMagfa.SoapSmsQueuableImplementationService();
            ssq.Credentials = new System.Net.NetworkCredential(UserName, PassWord);
            ssq.PreAuthenticate = true;
            double MagfaRemainingCredit = ssq.getCredit(DomainName);
            if (MagfaRemainingCredit > 0)
                lblMagfaCreditInfo.Text = MagfaRemainingCredit.ToString("N") + " ریال";
            else
                lblMagfaCreditInfo.Text = "اتمام اعتبار";
        }
        catch
        {
            lblMagfaCreditInfo.Text = "خطا در ارتباط با وب سرویس";
        }

        try
        {
            ir.afe.www.BoxService BoxService = new ir.afe.www.BoxService();
            string[] SmsInfo = new string[2];
            SmsInfo = Utility.GetSMSWebServiceInformation();
            string UserName = SmsInfo[0];
            string Password = SmsInfo[1];
            string arc = BoxService.GetRemainingCredit(UserName, Password);
            double AFERemainingCredit = 0;
            Double.TryParse(arc,out AFERemainingCredit);
            if (AFERemainingCredit > 0)
                lblAFECreditInfo.Text = AFERemainingCredit.ToString("N") + " ریال";
            else
                lblAFECreditInfo.Text = "اتمام اعتبار";
        }
        catch
        {
            lblAFECreditInfo.Text = "خطا در ارتباط با وب سرویس";
        }

        try
        {
            string[] SmsInfoPrdco = new string[2];
            SmsInfoPrdco = Utility.GetPrdcoWebServiceInformation();
            string UserNamePrdco = SmsInfoPrdco[0];
            string PasswordPrdco = SmsInfoPrdco[1];
            SMSPrdcoAsync.SendSoapClient sendSoapClient = new SMSPrdcoAsync.SendSoapClient();

            double PrdcoRemainingCredit = sendSoapClient.Credit(UserNamePrdco, PasswordPrdco);

            if (PrdcoRemainingCredit > 0)
                lblPrdcoCreditInfo.Text = PrdcoRemainingCredit.ToString("N") + " ریال";
            else
                lblPrdcoCreditInfo.Text = "اتمام اعتبار";
        }
        catch
        {
            lblPrdcoCreditInfo.Text = "خطا در ارتباط با وب سرویس";
        }

    }
    #endregion

}
