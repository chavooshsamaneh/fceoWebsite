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

public partial class AgentView : System.Web.UI.Page
{
    string PageMode;
    string AgentId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["AgentId"])))
            {
                if (string.IsNullOrEmpty(Request.QueryString["Cd"]))
                {
                    Response.Redirect("Agents.aspx");
                    return;
                }
            }

            SetKeys();
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(PkAgentId.Value))
        {
            Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (e.Item.Name)
        {
            case "Agent":
                Response.Redirect("AgentView.aspx?AgentId=" + PkAgentId.Value.ToString() + "&PageMode=" + PgMode.Value);
                break;
            case "News":
                Response.Redirect("News.aspx?AgentId=" + PkAgentId.Value.ToString() + "&PageMode=" + Utility.EncryptQS("AgentView"));
                break;
            case "AgentList":
                Response.Redirect("Agents.aspx");
                break;
        }
    }

    /*************************************************************************************************************/
    private void SetError(Exception err, char Flag)
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
                if (Flag == 'D')
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات وابسته معتبر نمی باشد";
                }
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

    private void SetKeys()
    {
        try
        {
            //PgMode.Value = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());
            //PkAgentId.Value = Utility.DecryptQS(Request.QueryString["AgentId"]).ToString();
            if (string.IsNullOrEmpty(Request.QueryString["Cd"]))
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                PkAgentId.Value = Server.HtmlDecode(Request.QueryString["AgentId"]).ToString();
            }
            else
            {
                PgMode.Value = Utility.EncryptQS("View");
                TSP.DataManager.AccountingAgentManager AgentManager = new TSP.DataManager.AccountingAgentManager();
                AgentManager.FindByAgentCode(Convert.ToInt32(Server.HtmlDecode(Request.QueryString["Cd"]).ToString()));
                if (AgentManager.Count == 0)
                {
                    return;
                }
                PkAgentId.Value = Utility.EncryptQS(AgentManager[0]["AgentId"].ToString());
            }
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        PageMode = Utility.DecryptQS(PgMode.Value);
        AgentId = Utility.DecryptQS(PkAgentId.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        SetMode();
    }

    private void SetMode()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        AgentId = Utility.DecryptQS(PkAgentId.Value);

        if ((string.IsNullOrEmpty(AgentId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        txtName.Enabled = false;
        //txtAgentCode.Enabled = false;
        //txtDescription.KeyboardEnabled = false;
        //txtDescription.DefaultContextMenu = false;
        txtAddress.Enabled = false;
        txtEmail.Enabled = false;
        txtFax.Enabled = false;
        txtMobileNo.Enabled = false;
        txtTel.Enabled = false;
        txtWebsite.Enabled = false;

        TSP.DataManager.AccountingAgentManager Manager = new TSP.DataManager.AccountingAgentManager();
        TSP.DataManager.TelManager TelManager = new TSP.DataManager.TelManager();

        Manager.FindByCode(int.Parse(AgentId));
        if (Manager.Count != 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازیابی اطلاعات ایجاد شده است";
        }
            lblTel.Visible = txtTel.Visible = lblFax.Visible = txtFax.Visible =   txtDescription.Visible = lblDescription.Visible = txtAddress.Visible = lblAddress.Visible=
               txtEmail.Visible = lblEmail.Visible =    txtMobileNo.Visible = lblMobile.Visible =  txtWebsite.Visible = lblWebsite.Visible =
                false;
            txtName.Text = Manager[0]["Name"].ToString();
            //txtAgentCode.Text = Manager[0]["AgentCode"].ToString();
            //  if (!Utility.IsDBNullOrNullValue(Manager[0]["Description"]))
            if (!Utility.IsDBNullOrNullValue(Manager[0]["Description"]))
            {
                txtDescription.Html = Manager[0]["Description"].ToString();
                txtDescription.Visible = lblDescription.Visible = true;
            }

            if (!Utility.IsDBNullOrNullValue(Manager[0]["Address"]))
            {
                txtAddress.Text = Manager[0]["Address"].ToString();
                txtAddress.Visible = lblAddress.Visible = true;
            }

            if (!Utility.IsDBNullOrNullValue(Manager[0]["Email"]))
            {
                txtEmail.Text = Manager[0]["Email"].ToString();
                txtEmail.Visible = lblEmail.Visible = true;
            }
            if (!Utility.IsDBNullOrNullValue(Manager[0]["MobileNo"]))
            {
                txtMobileNo.Text = Manager[0]["MobileNo"].ToString();
                txtMobileNo.Visible = lblMobile.Visible = true;
            }
            if (!Utility.IsDBNullOrNullValue(Manager[0]["Website"]))
            {
                txtWebsite.Text = Manager[0]["Website"].ToString();
                txtWebsite.Visible = lblWebsite.Visible = true;
            }

            TelManager.FindByTablePrimaryKey(int.Parse(AgentId), (int)TSP.DataManager.TableCodes.Agent);
                
                for (int i = 0; i < TelManager.Count; i++)
                {
                    if (TelManager[i]["Kind"].ToString() == "0")
                    {
                        txtTel.Text = TelManager[i]["Number"].ToString();
                        lblTel.Visible = txtTel.Visible = true;
                    }
                    else if (TelManager[i]["Kind"].ToString() == "1")
                    {
                        txtFax.Text = TelManager[i]["Number"].ToString();
                        lblFax.Visible = txtFax.Visible = true;
                    }
                }
       

        // ASPxRoundPanel2.HeaderText = "مشاهده";
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("style", "display:visible");
        this.LabelWarning.Text = Message;
    }
}
