using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
public partial class Employee_HomePage_FormsType : System.Web.UI.Page
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
            GridViewFormsType.Visible = per.CanView;

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
        int FormTypeId = -1;
        if (GridViewFormsType.FocusedRowIndex > -1)
        {
            DataRow row = GridViewFormsType.GetDataRow(GridViewFormsType.FocusedRowIndex);
            FormTypeId = (int)row["FormTypeId"];
        }
        if (FormTypeId == -1)
        {
            ShowMessage("لطفاً جهت حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        Delete(FormTypeId);
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int FormTypeId = -1;
        if (GridViewFormsType.FocusedRowIndex > -1)
        {
            DataRow row = GridViewFormsType.GetDataRow(GridViewFormsType.FocusedRowIndex);
            FormTypeId = (int)row["FormTypeId"];
        }
        if (FormTypeId == -1)
        {
            ShowMessage("لطفاً جهت حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        InActive(FormTypeId);
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int FormTypeId = -1;
        int focucedIndex = GridViewFormsType.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewFormsType.GetDataRow(focucedIndex);
            FormTypeId = (int)row["FormTypeId"];
        }
        if (FormTypeId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                FormTypeId = -1;
            }
            Response.Redirect("AddFormsType.aspx?FtId=" + Utility.EncryptQS(FormTypeId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void Delete(int FormTypeId)
    {
        try
        {
            TSP.DataManager.FormsTypeManager FormsTypeManager = new TSP.DataManager.FormsTypeManager();
            FormsTypeManager.FindByCode(FormTypeId);
            if (FormsTypeManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            FormsTypeManager[0].Delete();
            FormsTypeManager.Save();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            GridViewFormsType.DataBind();
        }
        catch (Exception err)
        {
            ShowMessage(Utility.Messages.GetExceptionError(err));
            Utility.SaveWebsiteError(err);
        }
    }

    private void InActive(int FormTypeId)
    {
        try
        {
            TSP.DataManager.FormsTypeManager FormsTypeManager = new TSP.DataManager.FormsTypeManager();
            FormsTypeManager.FindByCode(FormTypeId);
            if (FormsTypeManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            FormsTypeManager[0].BeginEdit();
            FormsTypeManager[0]["InActive"] = 1;
            FormsTypeManager[0]["InActiveDate"] = Utility.GetDateOfToday();
            FormsTypeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            FormsTypeManager[0].EndEdit();
            FormsTypeManager.Save();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            GridViewFormsType.DataBind();
        }
        catch (Exception err)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            Utility.SaveWebsiteError(err);
        }
    }
    #endregion
}