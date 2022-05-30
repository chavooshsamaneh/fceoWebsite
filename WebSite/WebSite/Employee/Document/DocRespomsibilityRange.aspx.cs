using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_Document_DocRespomsibilityRange : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.DocResponsibilityRangeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            GridViewResRange.Visible = per.CanView;

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;


        }
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
        int ResRangeId = -1;
        if (GridViewResRange.FocusedRowIndex > -1)
        {
            DataRow row = GridViewResRange.GetDataRow(GridViewResRange.FocusedRowIndex);
            ResRangeId = (int)row["ResRangeId"];
        }
        if (ResRangeId == -1)
        {
            ShowMessage("لطفاً جهت حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        Delete(ResRangeId);
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int ResRangeId = -1;
        if (GridViewResRange.FocusedRowIndex > -1)
        {
            DataRow row = GridViewResRange.GetDataRow(GridViewResRange.FocusedRowIndex);
            ResRangeId = (int)row["ResRangeId"];
        }
        if (ResRangeId == -1)
        {
            ShowMessage("لطفاً جهت حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        InActive(ResRangeId);
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int ResRangeId = -1;
        int focucedIndex = GridViewResRange.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewResRange.GetDataRow(focucedIndex);
            ResRangeId = (int)row["ResRangeId"];
        }
        if (ResRangeId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                ResRangeId = -1;
            }
            
            Response.Redirect("AddDocRespomsibilityRange.aspx?RgId=" + Utility.EncryptQS(ResRangeId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void Delete(int ResRangeId)
    {
        try
        {
            TSP.DataManager.DocResponsibilityRangeManager DocResponsibilityRangeManager = new TSP.DataManager.DocResponsibilityRangeManager();
            DocResponsibilityRangeManager.FindByCode(ResRangeId);
            if (DocResponsibilityRangeManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            DocResponsibilityRangeManager[0].Delete();
            DocResponsibilityRangeManager.Save();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            GridViewResRange.DataBind();
        }
        catch (Exception err)
        {
            ShowMessage(Utility.Messages.GetExceptionError(err));
            Utility.SaveWebsiteError(err);
        }
    }

    private void InActive(int ResRangeId)
    {
        try
        {
            TSP.DataManager.DocResponsibilityRangeManager DocResponsibilityRangeManager = new TSP.DataManager.DocResponsibilityRangeManager();
            DocResponsibilityRangeManager.FindByCode(ResRangeId);
            if (DocResponsibilityRangeManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            DocResponsibilityRangeManager[0].BeginEdit();
            DocResponsibilityRangeManager[0]["InActive"] = 1;
            DocResponsibilityRangeManager[0]["InActiveDate"] = Utility.GetDateOfToday();
            DocResponsibilityRangeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            DocResponsibilityRangeManager[0].EndEdit();
            DocResponsibilityRangeManager.Save();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            GridViewResRange.DataBind();
        }
        catch (Exception err)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            Utility.SaveWebsiteError(err);
        }
    }
    #endregion
}