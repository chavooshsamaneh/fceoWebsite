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
using DevExpress.Web;

public partial class Employee_Document_TestCondition : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.DocTestConditionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDelete.Enabled = per.CanEdit;
            btnDelete2.Enabled = per.CanEdit;
            btnActive.Enabled = per.CanEdit;
            btnActive2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            GridViewTestCondition.Visible = per.CanView;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["btnActive"] = btnActive.Enabled;

        }
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnActive"] != null)
            this.btnActive.Enabled = this.btnActive2.Enabled = (bool)this.ViewState["btnActive"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];

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
        if (GridViewTestCondition.FocusedRowIndex > -1)
        {
            DataRow TestConditionRow = GridViewTestCondition.GetDataRow(GridViewTestCondition.FocusedRowIndex);
            if (TestConditionRow != null)
            {
                int TCondId = int.Parse(TestConditionRow["TCondId"].ToString());
                Inactive(TCondId);
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        if (GridViewTestCondition.FocusedRowIndex > -1)
        {
            DataRow TestConditionRow = GridViewTestCondition.GetDataRow(GridViewTestCondition.FocusedRowIndex);
            if (TestConditionRow != null)
            {
                int TCondId = int.Parse(TestConditionRow["TCondId"].ToString());
                Active(TCondId);
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void GridViewTCondDetail_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["TCondId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void GridViewTestCondition_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["TCondId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void GridViewTestCondition_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "ExpireDate":
                e.Editor.Style["direction"] = "ltr";
                break;
            case "TestValidDate":
                e.Editor.Style["direction"] = "ltr";
                break;

        }
    }

    protected void GridViewTestCondition_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "ExpireDate":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "TestValidDate":
                e.Cell.Style["direction"] = "ltr";
                break;

        }
    }

    protected void GridViewTCondDetail_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "TestDate":
                e.Editor.Style["direction"] = "ltr";
                break;

        }
    }

    protected void GridViewTCondDetail_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "TestDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int TCondId = -1;
        int focucedIndex = GridViewTestCondition.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewTestCondition.GetDataRow(focucedIndex);
            TCondId = (int)row["TCondId"];
        }
        if (TCondId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                TCondId = -1;
                Response.Redirect("AddTestCondition.aspx?TCondId=" + Utility.EncryptQS(TCondId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("AddTestCondition.aspx?TCondId=" + Utility.EncryptQS(TCondId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }
        }
    }

    private void Inactive(int TCondId)
    {
        TSP.DataManager.DocTestConditionManager DocTestConditionManager = new TSP.DataManager.DocTestConditionManager();
        try
        {
            DocTestConditionManager.FindByCode(TCondId);
            if (DocTestConditionManager.Count == 1)
            {
                DocTestConditionManager[0].BeginEdit();

                DocTestConditionManager[0]["Inactive"] = 1;

                DocTestConditionManager[0].EndEdit();
                int cn = DocTestConditionManager.Save();
                if (cn > 0)
                {
                    GridViewTestCondition.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام گرفت.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private void Active(int TCondId)
    {
        TSP.DataManager.DocTestConditionManager DocTestConditionManager = new TSP.DataManager.DocTestConditionManager();
        try
        {
            DocTestConditionManager.FindByCode(TCondId);
            if (DocTestConditionManager.Count == 1)
            {
                DocTestConditionManager[0].BeginEdit();

                DocTestConditionManager[0]["Inactive"] = 0;

                DocTestConditionManager[0].EndEdit();
                int cn = DocTestConditionManager.Save();
                if (cn > 0)
                {
                    GridViewTestCondition.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام گرفت.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
        }
        catch (Exception err)
        {
            SetError(err);
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
    #endregion

}
