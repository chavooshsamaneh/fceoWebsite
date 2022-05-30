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

public partial class Accounting_BaseInfo_CostSettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        this.DivReport.Visible = false;

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.AccountingCostSettingsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;
            ASPxRoundPanel2.Visible = per.CanView;
            
            SetKeys();

           this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["View"] = ASPxRoundPanel2.Visible;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["View"] != null)
            ASPxRoundPanel2.Visible = (bool)this.ViewState["View"];
    }

    void SetNavBar(int i)
    {
        DevExpress.Web.ASPxNavBar ASPxNavBar1 = (DevExpress.Web.ASPxNavBar)Master.FindControl("ASPxNavBar1");
        ASPxNavBar1.DataBind();
        ASPxNavBar1.Groups[i].Expanded = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveCostSettings();
    }

    private void SaveCostSettings()
    {
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();
        TSP.DataManager.AccountingCostSettingsManager CostSettingsManager = new TSP.DataManager.AccountingCostSettingsManager();

        transact.Add(CostSettingsManager);

        try
        {
            transact.BeginSave();

            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.FirstMembershipCost.ToString(), Utility.GetCurrentUser_AgentId());
            if (CostSettingsManager.Count > 0)
                UpdateCostSettings(CostSettingsManager[0]);
            else
            {
                if (ASPxTextBoxFirstMembershipCost.Text != "")
                    InsertCostSettings(CostSettingsManager, TSP.DataManager.CostSettingsSData.FirstMembershipCost.ToString());
            }
            CostSettingsManager.Save();

            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.YearlyMembershipCost.ToString(), Utility.GetCurrentUser_AgentId());
            if (CostSettingsManager.Count > 0)
                UpdateCostSettings(CostSettingsManager[0]);
            else
            {
                if (ASPxTextBoxYearlyMembershipCost.Text != "")
                    InsertCostSettings(CostSettingsManager, TSP.DataManager.CostSettingsSData.YearlyMembershipCost.ToString());
            }
            CostSettingsManager.Save();

            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.FirstMembershipCostOffice.ToString(), Utility.GetCurrentUser_AgentId());
            if (CostSettingsManager.Count > 0)
                UpdateCostSettings(CostSettingsManager[0]);
            else
            {
                if (ASPxTextBoxFirstMembershipCost.Text != "")
                    InsertCostSettings(CostSettingsManager, TSP.DataManager.CostSettingsSData.FirstMembershipCostOffice.ToString());
            }
            CostSettingsManager.Save();

            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.YearlyMembershipCostOffice.ToString(), Utility.GetCurrentUser_AgentId());
            if (CostSettingsManager.Count > 0)
                UpdateCostSettings(CostSettingsManager[0]);
            else
            {
                if (ASPxTextBoxYearlyMembershipCost.Text != "")
                    InsertCostSettings(CostSettingsManager, TSP.DataManager.CostSettingsSData.YearlyMembershipCostOffice.ToString());
            }
            CostSettingsManager.Save();

            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.MemberFileModifiedCost.ToString(), Utility.GetCurrentUser_AgentId());
            if (CostSettingsManager.Count > 0)
                UpdateCostSettings(CostSettingsManager[0]);
            else
            {
                if (ASPxTextBoxMemberFileModifiedCost.Text != "")
                    InsertCostSettings(CostSettingsManager, TSP.DataManager.CostSettingsSData.MemberFileModifiedCost.ToString());
            }
            CostSettingsManager.Save();

            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.MemberFileRegistrationCost.ToString(), Utility.GetCurrentUser_AgentId());
            if (CostSettingsManager.Count > 0)
                UpdateCostSettings(CostSettingsManager[0]);
            else
            {
                if (ASPxTextBoxMemberFileRegistrationCost.Text != "")
                    InsertCostSettings(CostSettingsManager, TSP.DataManager.CostSettingsSData.MemberFileRegistrationCost.ToString());
            }
            CostSettingsManager.Save();

            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.ExpertPlace27sOnAccount.ToString(), Utility.GetCurrentUser_AgentId());
            if (CostSettingsManager.Count > 0)
                UpdateCostSettings(CostSettingsManager[0]);
            else
            {
                if (ASPxTextBoxExpertPlace27sOnAccount.Text != "")
                    InsertCostSettings(CostSettingsManager, TSP.DataManager.CostSettingsSData.ExpertPlace27sOnAccount.ToString());
            }
            CostSettingsManager.Save();


            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.ImplementDoc.ToString(), Utility.GetCurrentUser_AgentId());
            if (CostSettingsManager.Count > 0)
                UpdateCostSettings(CostSettingsManager[0]);
            else
            {
                if (TextBoxImplentDoc.Text != "")
                    InsertCostSettings(CostSettingsManager, TSP.DataManager.CostSettingsSData.ImplementDoc.ToString());
            }
            CostSettingsManager.Save();


            CostSettingsManager.FindBySData(TSP.DataManager.CostSettingsSData.YearlyMembershipCostOfficeImp.ToString(), Utility.GetCurrentUser_AgentId());
            if (CostSettingsManager.Count > 0)
                UpdateCostSettings(CostSettingsManager[0]);
            else
            {
                if (TextBoxYearlyMembershipCostOfficeImp.Text != "")
                    InsertCostSettings(CostSettingsManager, TSP.DataManager.CostSettingsSData.YearlyMembershipCostOfficeImp.ToString());
            }
            CostSettingsManager.Save();

            transact.EndSave();
            SetKeys();

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";


        }
        catch (Exception err)
        {
            transact.CancelSave();
            SetError(err);
        }
    }

    private void InsertCostSettings(TSP.DataManager.AccountingCostSettingsManager CostSettingsManager, string SData)
    {
        DataRow RowCostSettings = CostSettingsManager.NewRow();

        RowCostSettings.BeginEdit();

        RowCostSettings["SData"] = SData;
        RowCostSettings["SValue"] = GetSValue(SData);
        RowCostSettings["SName"] = GetSName(SData);
        RowCostSettings["AgentId"] = Utility.GetCurrentUser_AgentId();
        RowCostSettings["ModifiedDate"] = DateTime.Now;

        RowCostSettings.EndEdit();

        CostSettingsManager.AddRow(RowCostSettings);
    }

    private void UpdateCostSettings(DataRow RowCostSettings)
    {
        string SData = RowCostSettings["SData"].ToString();

        RowCostSettings.BeginEdit();

        RowCostSettings["SValue"] = GetSValue(SData);
        RowCostSettings["ModifiedDate"] = DateTime.Now;

        RowCostSettings.EndEdit();
    }

    /*************************************************************************************************************/    
    private string GetSValue(string SData)
    {
        if (SData == TSP.DataManager.CostSettingsSData.YearlyMembershipCost.ToString())
            return ASPxTextBoxYearlyMembershipCost.Text;
        else if (SData == TSP.DataManager.CostSettingsSData.FirstMembershipCost.ToString())
            return ASPxTextBoxFirstMembershipCost.Text;
        else if (SData == TSP.DataManager.CostSettingsSData.YearlyMembershipCostOffice.ToString())
            return ASPxTextBoxYearlyMembershipCostOffice.Text;
        else if (SData == TSP.DataManager.CostSettingsSData.FirstMembershipCostOffice.ToString())
            return ASPxTextBoxFirstMembershipCostOffice.Text;
        else if (SData == TSP.DataManager.CostSettingsSData.MemberFileModifiedCost.ToString())
            return ASPxTextBoxMemberFileModifiedCost.Text;
        else if (SData == TSP.DataManager.CostSettingsSData.MemberFileRegistrationCost.ToString())
            return ASPxTextBoxMemberFileRegistrationCost.Text;
        else if (SData == TSP.DataManager.CostSettingsSData.ExpertPlace27sOnAccount.ToString())
            return ASPxTextBoxExpertPlace27sOnAccount.Text;
        else if (SData == TSP.DataManager.CostSettingsSData.ImplementDoc.ToString())
            return TextBoxImplentDoc.Text;
        else if (SData == TSP.DataManager.CostSettingsSData.YearlyMembershipCostOfficeImp.ToString())
            return TextBoxYearlyMembershipCostOfficeImp.Text;

        return "";

    }

    private string GetSName(string SData)
    {
        if (SData == TSP.DataManager.CostSettingsSData.YearlyMembershipCost.ToString())
            return "حق عضویت سالیانه برای اعضای حقیقی";

        else if (SData == TSP.DataManager.CostSettingsSData.FirstMembershipCost.ToString())
            return "ورودیه عضویت برای اعضای حقیقی";

        else if (SData == TSP.DataManager.CostSettingsSData.YearlyMembershipCostOffice.ToString())
            return "حق عضویت سالیانه برای اعضای حقوقی";

        else if (SData == TSP.DataManager.CostSettingsSData.FirstMembershipCostOffice.ToString())
            return "ورودیه عضویت برای اعضای حقوقی";

        else if (SData == TSP.DataManager.CostSettingsSData.MemberFileModifiedCost.ToString())
            return "هزینه تمدید و تغییر پروانه";

        else if (SData == TSP.DataManager.CostSettingsSData.MemberFileRegistrationCost.ToString())
            return "هزینه صدور پروانه";

        else if (SData == TSP.DataManager.CostSettingsSData.ExpertPlace27sOnAccount.ToString())
            return "علی الحساب ماده 27";

        else if (SData == TSP.DataManager.CostSettingsSData.ImplementDoc.ToString())
            return "هزینه صدور/تمدید مجوز اجرا";

        else if (SData == TSP.DataManager.CostSettingsSData.YearlyMembershipCostOfficeImp.ToString())
            return "حق عضویت سالیانه برای اعضای حقوقی(مجریان) ";

        return "";

    }

    /*************************************************************************************************************/
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
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات وابسته معتبر نمی باشد";

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
        TSP.DataManager.AccountingCostSettingsManager Manager = new TSP.DataManager.AccountingCostSettingsManager();
        Manager.FindByAgentId(Utility.GetCurrentUser_AgentId());

        for (int i = 0; i < Manager.Count; i++)
        {
            if (Manager[i]["SData"].ToString() == TSP.DataManager.CostSettingsSData.FirstMembershipCost.ToString())
            {
                if (!string.IsNullOrEmpty(Manager[i]["SValue"].ToString()))
                    ASPxTextBoxFirstMembershipCost.Text = Convert.ToDecimal(Manager[i]["SValue"]).ToString("#,#");
            }

            if (Manager[i]["SData"].ToString() == TSP.DataManager.CostSettingsSData.YearlyMembershipCost.ToString())
            {
                if (!string.IsNullOrEmpty(Manager[i]["SValue"].ToString()))                
                    ASPxTextBoxYearlyMembershipCost.Text =Convert.ToDecimal(Manager[i]["SValue"]).ToString("#,#");                
            }

            if (Manager[i]["SData"].ToString() == TSP.DataManager.CostSettingsSData.YearlyMembershipCostOffice.ToString())
            {
                if (!string.IsNullOrEmpty(Manager[i]["SValue"].ToString()))
                    ASPxTextBoxYearlyMembershipCostOffice.Text = Convert.ToDecimal(Manager[i]["SValue"]).ToString("#,#");
            }

            if (Manager[i]["SData"].ToString() == TSP.DataManager.CostSettingsSData.FirstMembershipCostOffice.ToString())
            {
                if (!string.IsNullOrEmpty(Manager[i]["SValue"].ToString()))
                    ASPxTextBoxFirstMembershipCostOffice.Text = Convert.ToDecimal(Manager[i]["SValue"]).ToString("#,#");
            }

            if (Manager[i]["SData"].ToString() == TSP.DataManager.CostSettingsSData.MemberFileModifiedCost.ToString())
            {
                if (!string.IsNullOrEmpty(Manager[i]["SValue"].ToString()))
                    ASPxTextBoxMemberFileModifiedCost.Text = Convert.ToDecimal(Manager[i]["SValue"]).ToString("#,#");
            }

            if (Manager[i]["SData"].ToString() == TSP.DataManager.CostSettingsSData.MemberFileRegistrationCost.ToString())
            {
                if (!string.IsNullOrEmpty(Manager[i]["SValue"].ToString()))
                    ASPxTextBoxMemberFileRegistrationCost.Text = Convert.ToDecimal(Manager[i]["SValue"]).ToString("#,#");
            }

            if (Manager[i]["SData"].ToString() == TSP.DataManager.CostSettingsSData.ExpertPlace27sOnAccount.ToString())
            {
                if (!string.IsNullOrEmpty(Manager[i]["SValue"].ToString()))
                    ASPxTextBoxExpertPlace27sOnAccount.Text = Convert.ToDecimal(Manager[i]["SValue"]).ToString("#,#");
            }

            if (Manager[i]["SData"].ToString() == TSP.DataManager.CostSettingsSData.YearlyMembershipCostOfficeImp.ToString())
            {
                if (!string.IsNullOrEmpty(Manager[i]["SValue"].ToString()))
                    TextBoxYearlyMembershipCostOfficeImp.Text = Convert.ToDecimal(Manager[i]["SValue"]).ToString("#,#");
            }
            
            if (Manager[i]["SData"].ToString() == TSP.DataManager.CostSettingsSData.ImplementDoc.ToString())
            {
                if (!string.IsNullOrEmpty(Manager[i]["SValue"].ToString()))
                    TextBoxImplentDoc.Text = Convert.ToDecimal(Manager[i]["SValue"]).ToString("#,#");
            } 
        }

        ASPxRoundPanel2.HeaderText = "ویرایش";
    }
}