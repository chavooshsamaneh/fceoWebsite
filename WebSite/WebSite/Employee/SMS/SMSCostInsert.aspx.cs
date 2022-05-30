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

public partial class Employee_SMS_SMSCostInsert : System.Web.UI.Page
{
    #region Private Members
    string CostId;
    string PageMode;
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            HiddenFieldSMSCost["PageMode"] = "";
            HiddenFieldSMSCost["CostId"] = "";
            HiddenFieldSMSCost["NewMode"] = Utility.EncryptQS("New");
            //Check UserPermission
            TSP.DataManager.Permission per = TSP.DataManager.SmsCostManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;

            SetKeys();

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["CostId"])))
            {
                Response.Redirect("~/Employee/SMS/SMSCost.aspx");
                return;
            }

            this.ViewState["BtnSave"] = btnSave.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Employee/SMS/SMSCost.aspx");
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        CostId = Utility.DecryptQS(HiddenFieldSMSCost["CostId"].ToString());
        PageMode = Utility.DecryptQS(HiddenFieldSMSCost["PageMode"].ToString());
        if (string.IsNullOrEmpty(CostId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        else
        {
            if ((PageMode == "Edit" || PageMode == "View") && !string.IsNullOrEmpty(CostId))
            {
                DeleteSMSCost(int.Parse(CostId));
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(HiddenFieldSMSCost["PageMode"].ToString());
        switch (PageMode)
        {
            case "New":
                InsertSMSCost();
                break;
        }
    }
   
    #endregion

    #region Methods
    private void SetKeys()
    {
        HiddenFieldSMSCost["PageMode"] = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
        HiddenFieldSMSCost["CostId"] = Server.HtmlDecode(Request.QueryString["CostId"]).ToString();
        PageMode = Utility.DecryptQS(HiddenFieldSMSCost["PageMode"].ToString());
        CostId = Utility.DecryptQS(HiddenFieldSMSCost["CostId"].ToString());
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode();
    }

    private void SetMode()
    {
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (PageMode)
        {
            case "New":
                SetNewModeKeys();
                break;
        }
    }

    private void SetNewModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.SmsCostManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;


        //   ClearForm();

        RoundPanelSMSCost.HeaderText = "جدید";
    }

    private void InsertSMSCost()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();        
        TSP.DataManager.SmsCostManager SmsCostManager = new TSP.DataManager.SmsCostManager();
        trans.Add(SmsCostManager);        

        try
        {
            trans.BeginSave();
            SmsCostManager.ClearBeforeFill = true;
          
            //int IsMagfa;
          
            //if (cmbWebService.SelectedIndex == 0)
            //    IsMagfa = 0;
            //else IsMagfa = 1;
            DataTable dtSmsCost = SmsCostManager.FindByWebServiceType(Convert.ToInt32(cmbWebService.Value), 1);
            if (dtSmsCost.Rows.Count > 0)
            {
                for (int i = 0; i < dtSmsCost.Rows.Count; i++)
                {
                    SmsCostManager.FindByCode(int.Parse(dtSmsCost.Rows[i]["CostId"].ToString()));
                    if (SmsCostManager.Count == 1)
                    {
                        SmsCostManager[0]["IsActive"] = false;
                        int cnt = SmsCostManager.Save();
                        if (cnt < 0)
                        {
                            trans.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره صورت گرفته است";
                            return;
                        }

                        SmsCostManager.DataTable.AcceptChanges();
                    }
                }
            }
            DataRow RowCost = SmsCostManager.NewRow();
            RowCost["CostFr"] = txtbCostFr.Text;
            RowCost["MailNo"] = "";
            RowCost["MailDate"] = "";
            RowCost["StartDate"] = txtbDate.Text;            
            RowCost["CostEn"] = txtCostEn.Text;
            RowCost["IsActive"] = true;
            RowCost["TypeCom"] = Convert.ToInt32(cmbWebService.Value);
            
            RowCost["UserId"] = Utility.GetCurrentUser_UserId();
            RowCost["ModifiedDate"] = DateTime.Now;
            SmsCostManager.AddRow(RowCost);
            int cn = SmsCostManager.Save();
            if (cn > 0)
            {                
                CostId = SmsCostManager[0]["CostId"].ToString();
                trans.EndSave();
                HiddenFieldSMSCost["CostId"] = Utility.EncryptQS(CostId);
                //HiddenFieldSMSCost["PageMode"] = Utility.EncryptQS("Edit");
                HiddenFieldSMSCost["PageMode"] = Utility.EncryptQS("New");
                RoundPanelSMSCost.HeaderText = "مشاهده";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                DisableForm();
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره صورت گرفته است";
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره صورت گرفته است";
        }
    }
  
    private void DeleteSMSCost(int SMSCostId)
    {
        TSP.DataManager.SmsCostManager SmsCostManager = new TSP.DataManager.SmsCostManager();
        SmsCostManager.FindByCode(SMSCostId);
        if (SmsCostManager.Count == 1)
        {
            SmsCostManager[0].Delete();
            int cn = SmsCostManager.Save();
            if (cn > 0)
            {
                TSP.DataManager.Permission per = TSP.DataManager.SmsCostManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());


                btnSave.Enabled = per.CanNew || per.CanEdit;
                btnSave2.Enabled = per.CanNew || per.CanEdit;

                this.ViewState["BtnSave"] = btnSave.Enabled;


                HiddenFieldSMSCost["CostId"] = Utility.EncryptQS("");
                HiddenFieldSMSCost["PageMode"] = Utility.EncryptQS("New");
                ClearForm();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره صورت گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            LabelWarning.Text = "اطلاعات انتخابی توسط کاربر دیگری تغییر یافته است";
        }
    }

    private void ClearForm()
    {
        txtbCostFr.Text = "";
        txtbDate.Text = "";    
        txtCostEn.Text = "";
    }

    private void DisableForm()
    {
        txtbCostFr.Enabled = 
        txtbDate.Enabled =      
        txtCostEn.Enabled = 
        btnSave.Enabled = 
        btnSave2.Enabled =
        cmbWebService.Enabled = false;
    }
    #endregion
}
