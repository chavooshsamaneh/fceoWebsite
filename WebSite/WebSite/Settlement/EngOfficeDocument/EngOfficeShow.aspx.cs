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

public partial class Settlement_EngOfficeDocument_EngOfficeShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Request.QueryString["EOfId"] == null || Request.QueryString["EngOfId"] == null)
            {
                Response.Redirect("EngOffice.aspx");
            }

            EngOfficeId.Value = Request.QueryString["EngOfId"].ToString();
            EngFileId.Value = Request.QueryString["EOfId"].ToString();
            PgMode.Value = Request.QueryString["PageMode"].ToString();
            string PageMode = Utility.DecryptQS(PgMode.Value);
            string EOfId = Utility.DecryptQS(EngFileId.Value);
            string EngOfId = Utility.DecryptQS(EngOfficeId.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(EOfId) || string.IsNullOrEmpty(EngOfId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            switch (PageMode)
            {
                case "View":
                    FillForm(int.Parse(EOfId),int.Parse(EngOfId));
                    txtLastRegDate.Enabled = false;
                    txtdSerialNo.Enabled = false;
                    txtExpDate.Enabled = false;
                    cmbdIsTemporary.Enabled = false;
                    RoundPanelMemberFile.HeaderText = "مشاهده";
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    RoundPanelMemberFile.Enabled = false;
                  //  ASPxRoundPanel4.Enabled = false;
                    break;
                case "Edit":
                    FillForm(int.Parse(EOfId), int.Parse(EngOfId));
                    txtdSerialNo.Enabled = true;
                    txtExpDate.Enabled = true;
                    txtLastRegDate.Enabled = true;
                    cmbdIsTemporary.Enabled = true;

                    RoundPanelMemberFile.HeaderText = "ویرایش";
                    RoundPanelMemberFile.Enabled = false;
                //    ASPxRoundPanel4.Enabled = true;

                    break;
            }


            this.ViewState["BtnSave"] = btnSave.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
    }
    private void FillForm(int EOfId,int EngOfId)
    {
        
        TSP.DataManager.OfficeMemberManager OffMemberManager = (TSP.DataManager.OfficeMemberManager)Session["OfficeMemberManager"];
        TSP.DataManager.TelManager telManager = new TSP.DataManager.TelManager();
        TSP.DataManager.AddressManager AddManager = new TSP.DataManager.AddressManager();
        TSP.DataManager.EngOffFileManager fileManager = new TSP.DataManager.EngOffFileManager();
        //TSP.DataManager.EngOffFileManager fileManager2 = new TSP.DataManager.EngOffFileManager();

        //TSP.DataManager.AccountingDocOperationManager DocManager = new TSP.DataManager.AccountingDocOperationManager();
        //ASPxTextBoxFicheCode.Text = DocManager.GetBankDocNum(EOfId, (int)TSP.DataManager.AccountingTT.MembershipConfirmation);

        fileManager.FindByCode(EOfId);
        if (fileManager.Count == 1)
        {
            //txtEOfCode.Text = fileManager[0]["EOfCode"].ToString();
            if (!Utility.IsDBNullOrNullValue(fileManager[0]["EOfTId"]))
            {
                ComboEOfTId.DataBind();
                ComboEOfTId.SelectedIndex = ComboEOfTId.Items.IndexOfValue(fileManager[0]["EOfTId"].ToString());
            }
            txtLetterNo.Text = fileManager[0]["ParticipateLetterNo"].ToString();
            txtLetterDate.Text = fileManager[0]["ParticipateLetterDate"].ToString();
            txtDaftarNo.Text = fileManager[0]["EngOffNo"].ToString();
            txtDaftarLoc.Text = fileManager[0]["EngOffLoc"].ToString();
            txtFileNo.Text = fileManager[0]["FileNo"].ToString();
            txtDesc.Text = fileManager[0]["Description"].ToString();
          

            txtExpDate.Text = fileManager[0]["ExpireDate"].ToString();
            txtLastRegDate.Text = fileManager[0]["RegDate"].ToString();
            txtdSerialNo.Text = fileManager[0]["SerialNo"].ToString();
            txtAddress.Text = fileManager[0]["Address"].ToString();
            txtEmail.Text = fileManager[0]["Email"].ToString();
            txtFax.Text = fileManager[0]["FaxNo"].ToString();
            txtTel.Text = fileManager[0]["TellNo"].ToString();
            txtMobileNo.Text = fileManager[0]["MobileNo"].ToString();
            txtEngOffName.Text = fileManager[0]["EngOffName"].ToString();
            if (!Utility.IsDBNullOrNullValue(fileManager[0]["IsTemp"]))
                cmbdIsTemporary.SelectedIndex = cmbdIsTemporary.Items.FindByValue(Convert.ToInt32(fileManager[0]["IsTemp"]).ToString()).Index;

            //fileManager2.FindByEngOffCode(EngOfId, -1, 0);//صدور پروانه
            //if (fileManager2.Count > 0)
            //{
            //    if (!Utility.IsDBNullOrNullValue(fileManager2[0]["RegDate"]))
            //        txtdRegDate.Text = fileManager2[0]["RegDate"].ToString();
            //}
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string EOfId = Utility.DecryptQS(EngFileId.Value);

        if (string.IsNullOrEmpty(EOfId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            Edit(int.Parse(EOfId));
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string EngOfId = Utility.DecryptQS(EngOfficeId.Value);
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(EngOfId))//&& !string.IsNullOrEmpty(Request.QueryString["SrchFlt"])
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            //string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("EngOffice.aspx?PostId=" + EngOfficeId.Value + "&GrdFlt=" + GrdFlt);// + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("EngOffice.aspx");
        }
        //Response.Redirect("EngOffice.aspx");
    }
    protected void Edit(int EOfId)
    {
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        try
        {
            FileManager.FindByCode(EOfId);
            if (FileManager.Count > 0)
            {
                FileManager[0].BeginEdit();

                FileManager[0]["RegDate"] = txtLastRegDate.Text;
                FileManager[0]["SerialNo"] = txtdSerialNo.Text;
                FileManager[0]["ExpireDate"] = txtExpDate.Text;
                if (cmbdIsTemporary.SelectedItem.Value.ToString() == "0")
                    FileManager[0]["IsTemp"] = 0;
                else
                    FileManager[0]["IsTemp"] = 1;

                FileManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                FileManager[0]["ModifiedDate"] = DateTime.Now;
                FileManager[0].EndEdit();

                if (FileManager.Save() > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";

                    //if (FileManager[0]["Type"].ToString() == "0")//صدور
                    //    txtRegDate.Text = txtdLastRegDate.Text;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                }


            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";

            }
        }
        catch (Exception err)
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
    }
    private int FindNmcId()
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        LoginManager.FindByCode(UserId);
        int NmcId = -1;
        if (LoginManager.Count > 0)
        {
            int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
            int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
            NezamChartManager.FindByEmpId(EmpId, UltId);
            if (NezamChartManager.Count > 0)
            {
                NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            }
        }
        else
        {
            Response.Redirect("~Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return (-1);
        }
        return (NmcId);
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Member":
                Response.Redirect("EngOfficeMember.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;

            case "Attach":
                Response.Redirect("EngOfficeAttachment.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;

            case "Job":
                Response.Redirect("EngOfficeJob.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    protected void CallbackPanelDoRegDate_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (string.IsNullOrEmpty(e.Parameter))
        {
            return;
        }
        SetMeDocDefualtExpireDate(Convert.ToInt32(e.Parameter));
    }

    private void SetMeDocDefualtExpireDate(int DocumentSetExpireDateType)
    {
        Utility.Date Date;
        if (string.IsNullOrEmpty(txtLastRegDate.Text))
        {
            txtLastRegDate.Text = Utility.GetDateOfToday();
            Date = new Utility.Date();
        }
        else
            Date = new Utility.Date(txtLastRegDate.Text);

        int MonthCount = 12;
        switch (DocumentSetExpireDateType)
        {

            case (int)TSP.DataManager.DocumentSetExpireDateType.Temporary:
                // Date = new Utility.Date();
                MonthCount = 12 * Utility.GetDefaultTemporaryMemberDocExpireDate();
                txtExpDate.Text = Date.AddMonths(MonthCount);
                break;
            case (int)TSP.DataManager.DocumentSetExpireDateType.Permanent:

                MonthCount = 12 * Utility.GetDefaultPermanentMemberDocExpireDate();
                txtExpDate.Text = Date.AddMonths(MonthCount);
                break;
        }
    }
}
