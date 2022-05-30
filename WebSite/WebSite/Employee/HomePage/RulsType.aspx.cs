using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Employee_HomePage_RulsType : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.FormsTypeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());            
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            GridViewRulesType.Visible = per.CanView;

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }
        SetPageFilter();

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int RuleTypeId = -1;
        if (GridViewRulesType.FocusedRowIndex > -1)
        {
            DataRow row = GridViewRulesType.GetDataRow(GridViewRulesType.FocusedRowIndex);
            RuleTypeId = (int)row["RuleTypeId"];
        }
        if (RuleTypeId == -1)
        {
            ShowMessage("لطفاً جهت حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        Delete(RuleTypeId);
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int RulesTypeId = -1;
        if (GridViewRulesType.FocusedRowIndex > -1)
        {
            DataRow row = GridViewRulesType.GetDataRow(GridViewRulesType.FocusedRowIndex);
            RulesTypeId = (int)row["RuleTypeId"];
        }
        if (RulesTypeId == -1)
        {
            ShowMessage("لطفاً جهت حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
      //  InActive(RulesTypeId);
    }
    #endregion

    #region Methods

    private void NextPage(string Mode)
    {
        int RulesTypeId = -1;
        int focucedIndex = GridViewRulesType.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewRulesType.GetDataRow(focucedIndex);
            RulesTypeId = (int)row["RuleTypeId"];
        }
        if (RulesTypeId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                RulesTypeId = -1;
            }
            string GridFilterString = GridViewRulesType.FilterExpression;
            Response.Redirect("AddRulesType.aspx?FtId=" + Utility.EncryptQS(RulesTypeId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
        }
    }
    private void Delete(int RulesTypeId)
    {
        try
        {

            TSP.DataManager.RulesTypeManager RulesTypeManager = new TSP.DataManager.RulesTypeManager();
            RulesTypeManager.FindByCode(RulesTypeId);
            if (RulesTypeManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            RulesTypeManager[0].Delete();
            RulesTypeManager.Save();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            GridViewRulesType.DataBind();
        }
        catch (Exception err)
        {
            ShowMessage(Utility.Messages.GetExceptionError(err));
            Utility.SaveWebsiteError(err);
        }
    }
    //private void InActive(int RulesTypeId)
    //{
    //    try
    //    {
    //        TSP.DataManager.RulesTypeManager RulesTypeManager = new TSP.DataManager.RulesTypeManager();
    //        RulesTypeManager.FindByCode(RulesTypeId);
    //        if (RulesTypeManager.Count != 1)
    //        {
    //            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
    //            return;
    //        }
    //        RulesTypeManager[0].BeginEdit();
    //        RulesTypeManager[0]["InActive"] = 1;
    //        RulesTypeManager[0]["InActiveDate"] = Utility.GetDateOfToday();
    //        RulesTypeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //        RulesTypeManager[0].EndEdit();
    //        RulesTypeManager.Save();
    //        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
    //        GridViewFormsType.DataBind();
    //    }
    //    catch (Exception err)
    //    {
    //        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
    //        Utility.SaveWebsiteError(err);
    //    }
    //}
    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());

                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewRulesType.FilterExpression = GrdFlt;
            }
        }

    }

    #endregion
}