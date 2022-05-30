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

public partial class Employee_Management_University : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.UniversityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = Per.CanDelete;
            btnDelete2.Enabled = Per.CanDelete;
            btnEdit.Enabled = Per.CanEdit;
            btnEdit2.Enabled = Per.CanEdit;
            BtnNew.Enabled = Per.CanNew;
            btnNew2.Enabled = Per.CanNew;
            btnView.Enabled = Per.CanView;
            btnView2.Enabled = Per.CanView;
            GridViewUniversity.Visible = Per.CanView;
            btnInActive.Enabled = btnInActive2.Enabled = Per.CanEdit;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnView"] != null)
            this.btnView2.Enabled = this.btnView.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = btnInActive.Enabled = btnInActive2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnNew2_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteUniversity();
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int UnId = -1;
        if (GridViewUniversity.FocusedRowIndex > -1)
        {
            DataRow row = GridViewUniversity.GetDataRow(GridViewUniversity.FocusedRowIndex);
            UnId = (int)row["UnId"];
        }

        if (UnId == -1)
        {
            ShowMessage("لطفاًابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        InActive(UnId);
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        int UnId = -1;
        if (GridViewUniversity.FocusedRowIndex > -1)
        {
            DataRow row = GridViewUniversity.GetDataRow(GridViewUniversity.FocusedRowIndex);
            UnId = (int)row["UnId"];
        }

        if (UnId == -1)
        {
            ShowMessage("لطفاًابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        Active(UnId);
    }

    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int UnId = -1;
        if (GridViewUniversity.FocusedRowIndex > -1)
        {
            DataRow row = GridViewUniversity.GetDataRow(GridViewUniversity.FocusedRowIndex);
            UnId = (int)row["UnId"];

        }
        if (UnId == -1 && Mode != "New")
        {
            ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            if (Mode == "Edit")
            {
                if (!CheckIfInActive())
                    return;
            }
            if (Mode == "New")
            {
                UnId = -1;
                Response.Redirect("UniversityInsert.aspx?UnId=" + Utility.EncryptQS(UnId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("UniversityInsert.aspx?UnId=" + Utility.EncryptQS(UnId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));
            }
        }
    }

    private void DeleteUniversity()
    {
        int UnId = -1;
        if (GridViewUniversity.FocusedRowIndex > -1)
        {
            DataRow row = GridViewUniversity.GetDataRow(GridViewUniversity.FocusedRowIndex);
            UnId = (int)row["UnId"];
        }

        if (UnId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاًابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            TSP.DataManager.UniversityManager UnivertityManager = new TSP.DataManager.UniversityManager();
            UnivertityManager.FindByCode(UnId);

            if (UnivertityManager.Count == 1)
            {
                try
                {
                    UnivertityManager[0].Delete();
                    int cn = UnivertityManager.Save();
                    if (cn == 1)
                    {
                        GridViewUniversity.DataBind();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام شد";
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                    }
                }
                catch (Exception err)
                {
                    SetError(err);
                }
            }
        }
    }

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
                this.LabelWarning.Text = "کد تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
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

    private void InActive(int UnId)
    {
        try
        {
            TSP.DataManager.UniversityManager UniversityManager = new TSP.DataManager.UniversityManager();
            UniversityManager.FindByCode(UnId);
            if (UniversityManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            UniversityManager[0].BeginEdit();
            UniversityManager[0]["InActive"] = 1;
            UniversityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            UniversityManager[0]["ModifiedDate"] = DateTime.Now;
            UniversityManager[0].EndEdit();
            UniversityManager.Save();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            GridViewUniversity.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void Active(int UnId)
    {
        try
        {
            TSP.DataManager.UniversityManager UniversityManager = new TSP.DataManager.UniversityManager();
            UniversityManager.FindByCode(UnId);
            if (UniversityManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            UniversityManager[0].BeginEdit();
            UniversityManager[0]["InActive"] = 0;
            UniversityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            UniversityManager[0]["ModifiedDate"] = DateTime.Now;
            UniversityManager[0].EndEdit();
            UniversityManager.Save();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            GridViewUniversity.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private Boolean CheckIfInActive()
    {
        if (GridViewUniversity.FocusedRowIndex <= -1)
        {
            ShowMessage("لطفاًابتدا یک رکورد را انتخاب نمائید");
            return false;
        }
        DataRow row = GridViewUniversity.GetDataRow(GridViewUniversity.FocusedRowIndex);
        if (!Utility.IsDBNullOrNullValue(row["InActive"]) && Convert.ToBoolean(row["InActive"]))
        {
            ShowMessage("امکان ویرایش رکورد غیر فعال وجود ندارد");
            return false;
        }
        return true;
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}
