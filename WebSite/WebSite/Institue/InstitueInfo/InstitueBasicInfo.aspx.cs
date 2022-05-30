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

public partial class Institue_InstitueInfo_InstitueBasicInfo : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

      
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {                   
            int InsId = Utility.GetCurrentUser_MeId();
            HiddenFieldInstitue["InsId"] = Utility.EncryptQS(InsId.ToString());

            if (string.IsNullOrEmpty(Request.QueryString["InsCId"]))
            {
                Response.Redirect("InstitueCertificates.aspx");
                return;
            }
            SetKey();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InstitueCertificates.aspx");

    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Facility":
                Response.Redirect("InstituteFacilities.aspx?InsCId=" + HiddenFieldInstitue["InsCId"].ToString());
                break;
            case "Activity":
                Response.Redirect("InstitueActivity.aspx?InsCId=" + HiddenFieldInstitue["InsCId"].ToString());
                break;
            case "Manager":
                Response.Redirect("InstitueManager.aspx?InsCId=" + HiddenFieldInstitue["InsCId"].ToString());
                break;            
            case "InsTeacher":
                Response.Redirect("InstitueTeacher.aspx?InsCId=" + HiddenFieldInstitue["InsCId"].ToString());
                break;
        }
    }
    #endregion

    #region Methods

    private void SetKey()
    {
        try
        {
            HiddenFieldInstitue["InsCId"] = Server.HtmlDecode(Request.QueryString["InsCId"].ToString());

            SetMode("View");
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

    }

    private void SetMode(string Mode)
    {
        switch (Mode)
        {
            case "View":
                if (HiddenFieldInstitue["InsId"] == null && (string.IsNullOrEmpty(HiddenFieldInstitue["InsId"].ToString())))
                {
                    Response.Redirect("InstitueHome.aspx");
                    return;
                }
                else
                {
                    string InsId = Utility.DecryptQS(HiddenFieldInstitue["InsId"].ToString());
                    FillForm(int.Parse(InsId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                }                
                break;
        }
    }

    protected void FillForm(int InsId)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        TSP.DataManager.InstitueCertificateManager InstitueCertificateManager = new TSP.DataManager.InstitueCertificateManager();
        TSP.DataManager.InstitueManager manager = new TSP.DataManager.InstitueManager();
        TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();

        manager.FindByCode(InsId);
        if (manager.Count == 1)
        {
            int CitId = int.Parse(manager[0]["CitId"].ToString());
            CityManager.FindByCode(CitId);
            if (CityManager.Count == 1)
            {
                txtCity.Text = CityManager[0]["CitName"].ToString();
            }
            txtInsName.Text = manager[0]["InsName"].ToString();
            txtManager.Text = manager[0]["Manager"].ToString();
            txtRegNo.Text = manager[0]["RegNo"].ToString();
            txtRegDate.Text = manager[0]["RegDate"].ToString();
            txtRegPlace.Text = manager[0]["RegPlace"].ToString();
           
            txtTel1.Text = manager[0]["Tel1"].ToString();
            txtTel2.Text = manager[0]["Tel2"].ToString();
            txtMobileNo.Text = manager[0]["MobileNo"].ToString();
            txtAddress.Text = manager[0]["Address"].ToString();
            txtEmail.Text = manager[0]["Email"].ToString();
            txtWebSite.Text = manager[0]["WebSite"].ToString();
            txtDesc.Text = manager[0]["Description"].ToString();
        }
        DataTable dtInsCert = InstitueCertificateManager.SelectLastVersion(InsId,1);
        if (dtInsCert.Rows.Count > 0)
        {
            txtFileNo.Text = dtInsCert.Rows[0]["FileNo"].ToString();
        }
    }
    #endregion
}
