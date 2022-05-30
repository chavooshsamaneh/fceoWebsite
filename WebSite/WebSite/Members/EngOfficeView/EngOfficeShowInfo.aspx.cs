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

public partial class Members_EngOfficeView_EngOfficeShowInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            OdbEOffType.FilterParameters[0].DefaultValue = "1";
            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["EngOfId"]))
            {
                Response.Redirect("EngOffice.aspx");
                return;
            }

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                EngOfficeId.Value = Server.HtmlDecode(Request.QueryString["EngOfId"]).ToString();
                EngFileId.Value = Server.HtmlDecode(Request.QueryString["EOfId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string EngOfId = Utility.DecryptQS(EngOfficeId.Value);
            string EOfId = Utility.DecryptQS(EngFileId.Value);


            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            switch (PageMode)
            {
                case "View":
                    FillForm(int.Parse(EOfId));
                    txtdExpDate.Visible = true;
                    txtdLastRegDate.Visible = true;
                    txtdSerialNo.Visible = true;
                    cmbdIsTemporary.Visible = true;
                    ASPxLabeld1.Visible = true;
                    ASPxLabeld2.Visible = true;
                    ASPxLabeld3.Visible = true;
                    ASPxLabeld4.Visible = true;
                    txtFileNo.Enabled = false;
                    ASPxRoundPanel2.Enabled = false;
                    if (string.IsNullOrEmpty(EOfId) || string.IsNullOrEmpty(EngOfId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    break;
            }

        }
    }
    private void FillForm(int EOfId)
    {
        TSP.DataManager.OfficeMemberManager OffMemberManager = (TSP.DataManager.OfficeMemberManager)Session["OfficeMemberManager"];
        TSP.DataManager.TelManager telManager = new TSP.DataManager.TelManager();
        TSP.DataManager.AddressManager AddManager = new TSP.DataManager.AddressManager();
        TSP.DataManager.EngOffFileManager fileManager = new TSP.DataManager.EngOffFileManager();

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

            if (Convert.ToBoolean(fileManager[0]["Requester"]))//Emp
            {
                ASPxLabelddate.Visible = true;
                ASPxLabeldno.Visible = true;
                txtFileLetterDate.Visible = true;
                txtFileLetterNo.Visible = true;
                txtFileLetterDate.Text = fileManager[0]["FileLetterDate"].ToString();
                txtFileLetterNo.Text = fileManager[0]["FileLetterNo"].ToString();
            }

            txtdExpDate.Text = fileManager[0]["ExpireDate"].ToString();
            txtdLastRegDate.Text = fileManager[0]["RegDate"].ToString();
            txtdSerialNo.Text = fileManager[0]["SerialNo"].ToString();
            if (Convert.ToBoolean(fileManager[0]["IsTemp"]))
                cmbdIsTemporary.SelectedIndex = 1;
            else
                cmbdIsTemporary.SelectedIndex = 0;



            telManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.EngOffFile, EOfId);
            if (telManager.Count > 0)
            {
                for (int t = 0; t < telManager.Count; t++)
                {
                    if (telManager[t]["Kind"].ToString() == "0")

                        txtTel.Text = telManager[t]["Number"].ToString();
                    else
                        txtFax.Text = telManager[t]["Number"].ToString();
                }
            }


            AddManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.EngOffFile, EOfId);
            if (AddManager.Count > 0)
            {
                txtAddress.Text = AddManager[0]["Address"].ToString();
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EngOffice.aspx");
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Member":
                Response.Redirect("EngOfficeMember.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;

            case "Attach":
                Response.Redirect("EngOfficeAttachment.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;

            case "Job":
                Response.Redirect("EngOfficeJob.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);
                break;
        }

    }
}