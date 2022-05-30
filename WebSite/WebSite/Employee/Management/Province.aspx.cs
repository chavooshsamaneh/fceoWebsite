using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_Management_Province : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDelete();

        if (!IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.ProvinceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnNew.Enabled = btnNew2.Enabled = Per.CanNew;
            btnEdit2.Enabled = btnEdit.Enabled = Per.CanEdit;
            btnView.Enabled = btnView2.Enabled = GridViewProvince.Visible = Per.CanView;
            btnDelete2.Enabled = btnDelete.Enabled = Per.CanDelete;
            this.ViewState["btnNew"] = btnNew.Enabled;
            this.ViewState["btnEdit"] = btnEdit.Enabled;
            this.ViewState["btnView"] = btnView.Enabled;
            this.ViewState["btnDelete"] = btnDelete.Enabled;
        }
        if (this.ViewState["btnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["btnNew"];
        if (this.ViewState["btnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnEdit"];
        if (this.ViewState["btnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = GridViewProvince.Visible = (bool)this.ViewState["btnView"];
        if (this.ViewState["btnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["btnDelete"];

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int PrId = -1;
        int focucedIndex = GridViewProvince.FocusedRowIndex;
        if (focucedIndex < 0)
        {
            SetMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        if (focucedIndex > -1)
        {
            DataRow row = GridViewProvince.GetDataRow(focucedIndex);
            PrId = (int)row["PrId"];
            Delete(PrId);
        }

    }
    #endregion
    #region Metods
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    private void NextPage(string Mode)
    {
        int PrId = -1;
        int focucedIndex = GridViewProvince.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewProvince.GetDataRow(focucedIndex);
            PrId = (int)row["PrId"];
        }
        if (PrId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                PrId = -1;
            }

            Response.Redirect("ProvinceInsert.aspx?PId=" + Utility.EncryptQS(PrId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
        }
    }

    private void Delete(int PrId)
    {
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        try
        {
            ProvinceManager.FindByCode(PrId);
                  if (ProvinceManager.Count != 1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
                  ProvinceManager[0].Delete();
                  ProvinceManager.Save();
                  SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                  GridViewProvince.DataBind();
        }

              catch (Exception err)
        {
            
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 547)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
            }
        }
       }

    private void SetWarningLableDelete()
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
    #endregion
}