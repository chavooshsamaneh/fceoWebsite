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

public partial class Employee_Document_GovManagerName : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.GovManagerNameManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            CustomAspxDevGridView1.Visible = per.CanView;

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
        Response.Redirect("AddGovManagerName.aspx?GmnId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int GmnId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            GmnId = (int)row["GmnId"];
        }
        if (GmnId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("AddGovManagerName.aspx?GmnId=" + Utility.EncryptQS(GmnId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        // Response.Redirect("AmoozeshHome.aspx");
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int GmnId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            GmnId = (int)row["GmnId"];
        }
        if (GmnId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {

            Response.Redirect("AddGovManagerName.aspx?GmnId=" + Utility.EncryptQS(GmnId.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int GmnId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            GmnId = (int)row["GmnId"];
        }
        if (GmnId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.GovManagerNameManager managerEdit = new TSP.DataManager.GovManagerNameManager();
            managerEdit.FindByCode(GmnId);
            if (managerEdit.Count == 1)
            {
                try
                {
                    managerEdit[0].Delete();

                    int cn = managerEdit.Save();
                    if (cn == 1)
                    {
                        CustomAspxDevGridView1.DataBind();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام شد";

                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام نشد";
                    }
                }
                catch (Exception err)
                {

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
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int GmnId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            GmnId = (int)row["GmnId"];
        }
        if (GmnId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        InActive(GmnId);
        CustomAspxDevGridView1.DataBind();
    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";
    }
    #endregion

    #region Methods
    private void InActive(int GmnId)
    {
        try
        {
            TSP.DataManager.GovManagerNameManager GovManagerNameManager = new TSP.DataManager.GovManagerNameManager();
            GovManagerNameManager.FindByCode(GmnId);
            if (GovManagerNameManager.Count != 1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations);
                return;
            }
            GovManagerNameManager[0].BeginEdit();
            GovManagerNameManager[0]["InActive"] = 1;
            GovManagerNameManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            GovManagerNameManager[0]["ModifiedDate"] = DateTime.Now;
            GovManagerNameManager[0].EndEdit();
            if (GovManagerNameManager.Save() > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete);
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave);
            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }
    #endregion
}

