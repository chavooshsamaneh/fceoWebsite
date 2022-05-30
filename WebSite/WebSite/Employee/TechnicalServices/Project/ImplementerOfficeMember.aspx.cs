using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_TechnicalServices_Project_ImplementerOfficeMember : System.Web.UI.Page
{
    #region Properties
    private int _ImpOfficeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["ImpOfficeId"]);
        }
        set
        {
            HiddenFieldPage["ImpOfficeId"] = value.ToString();
        }
    }
    private int _ImOfficeReqId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["ImOfficeReqId"]);
        }
        set
        {
            HiddenFieldPage["ImOfficeReqId"] = value.ToString();
        }
    }
    private string _PageMode
    {
        get
        {
            return HiddenFieldPage["PageMode"].ToString();
        }
        set
        {
            HiddenFieldPage["PageMode"] = value.ToString();
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ImplementerOfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = btnNew2.Enabled = per.CanNew;
            btnView.Enabled = btnView2.Enabled = per.CanView;

            btnEdit.Enabled = btnEdit2.Enabled = per.CanEdit;

            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["btnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNewVis"] = btnNew.Visible;
            this.ViewState["BtnView"] = btnView.Enabled;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnEdit"];

        string script = @"<SCRIPT language='javascript'> function CheckSearch() {";

        script += "if ( txtImpOfficeId.GetText() == '' &&  txtMeNo.GetText() == '' && txtMFNo.GetText() == ''   && CmbTask.GetSelectedIndex() == 0) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                    txtMeId.SetText(''); txtMFNo.SetText(''); ";
        script += "}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
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
    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int TableId = _ImOfficeReqId;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSImplementerOffice);
        int WFCode = (int)TSP.DataManager.WorkFlows.TSImplementOfficeConfirming;
        WFUserControl.PerformCallback(TableId, TableType, WFCode, e);
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect("ImplementerOfficeInsert.aspx?PgMd=" + Utility.EncryptQS(_PageMode) + "&ReqId=" + Utility.EncryptQS(_ImpOfficeId.ToString()) + "&ImpOfficeId=" + Utility.EncryptQS(_ImpOfficeId.ToString() + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt), false);
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("ImplementerOffice.aspx?PostId=" + Utility.EncryptQS(_ImpOfficeId.ToString()) + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("ImplementerOffice.aspx");
        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {

    }
    #endregion
    #region Methods


    void ShowMessage(string str)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = str;
    }

    private void NextPage(string PageMode)
    {
        try
        {
           int ImpOfficeMeId = -1;
            string Filter = "";
            if (PageMode != "New")
            {
                string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
                string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
                if (GridViewObserverImplementerOffice.FocusedRowIndex <= -1)
                {
                    ShowMessage("ابتدا یک ردیف از جدول را انتخاب نمایید");
                    return;
                }
                System.Data.DataRow row = GridViewObserverImplementerOffice.GetDataRow(GridViewObserverImplementerOffice.FocusedRowIndex);
                ImpOfficeMeId = Convert.ToInt32(row["ImpOfficeMeId"]);
                Filter = "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
            }
            Response.Redirect("ImplementerOfficeMemberInsert.aspx?PgMd=" + Utility.EncryptQS(PageMode) + "&ReqId=" + Utility.EncryptQS(_ImOfficeReqId.ToString()) + "&ImpOfficeId=" + Utility.EncryptQS(_ImpOfficeId.ToString()) + "&ImpOfficeMeId=" + Utility.EncryptQS(ImpOfficeMeId.ToString()) + Filter, false);
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }
    #endregion   

}