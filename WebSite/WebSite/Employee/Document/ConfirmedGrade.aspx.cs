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

public partial class Employee_Document_ConfirmedGrade : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            ResetGridJSProperties();
            TSP.DataManager.Permission per = TSP.DataManager.DocAcceptedGradeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnInActive.Enabled = btnInActive2.Enabled = btnDelete2.Enabled = btnDelete.Enabled = per.CanDelete;
            GridViewConfirmedGrade.Visible = per.CanView;
            btnActive.Enabled = per.CanEdit;
            btnActive2.Enabled = per.CanEdit;
            btnExportExcel2.Enabled = btnExportExcel.Enabled = per.CanView;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["btnActive"] = btnActive.Enabled;
            this.ViewState["btnExportExcel"] = btnExportExcel.Enabled;
        }
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnActive"] != null)
            this.btnActive.Enabled = this.btnActive2.Enabled = (bool)this.ViewState["btnActive"];
        if (this.ViewState["btnExportExcel"] != null)
            this.btnExportExcel.Enabled = this.btnExportExcel2.Enabled = (bool)this.ViewState["btnExportExcel"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddConfirmedGrade.aspx?pgMd=" + Utility.EncryptQS("New") + "&GMRId=" + Utility.EncryptQS("-1"));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (GridViewConfirmedGrade.FocusedRowIndex <= -1)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        DataRow row = GridViewConfirmedGrade.GetDataRow(GridViewConfirmedGrade.FocusedRowIndex);
        if ((int)row["GMRId"] == -1)
        {
            ShowMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        Response.Redirect("AddConfirmedGrade.aspx?pgMd=" + Utility.EncryptQS("Edit") + "&GMRId=" + Utility.EncryptQS(row["GMRId"].ToString()));

    }

    protected void GridViewGradeMjResponsibility_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        ResetGridJSProperties();
        InsertGradeMajorResponsibility(e);
    }

    protected void GridViewGradeMjResponsibility_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        ResetGridJSProperties();
        EditGradeMajorResponsibility(e);
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "ConfirmedGrade";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void GridViewConfirmedGrade_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        int GMRId = -1;
        switch (e.Parameters)
        {
            case "InActive":

                if (GridViewConfirmedGrade.FocusedRowIndex > -1)
                {
                    DataRow ConfirmGradeRow = GridViewConfirmedGrade.GetDataRow(GridViewConfirmedGrade.FocusedRowIndex);

                    GMRId = int.Parse(ConfirmGradeRow["GMRId"].ToString());
                    InActive(GMRId);
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ردیفی انتخاب نشده است";
                }
                break;
            case "Active":
                if (GridViewConfirmedGrade.FocusedRowIndex > -1)
                {
                    DataRow ConfirmGradeRow = GridViewConfirmedGrade.GetDataRow(GridViewConfirmedGrade.FocusedRowIndex);

                    GMRId = int.Parse(ConfirmGradeRow["GMRId"].ToString());
                    Active(GMRId);
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ردیفی انتخاب نشده است";
                }
                break;

            case "Delete":
                if (GridViewConfirmedGrade.FocusedRowIndex > -1)
                {
                    DataRow ConfirmGradeRow = GridViewConfirmedGrade.GetDataRow(GridViewConfirmedGrade.FocusedRowIndex);

                    GMRId = int.Parse(ConfirmGradeRow["GMRId"].ToString());
                    Delete(GMRId);
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ردیفی انتخاب نشده است";
                }
                break;
        }
    }
    #endregion

    #region Methods
    private void InsertGradeMajorResponsibility(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        TSP.DataManager.DocAcceptedGradeManager DocAcceptedGradeManager1 = new TSP.DataManager.DocAcceptedGradeManager();
        TSP.DataManager.DocAcceptedGradeManager DocAcceptedGradeManager = new TSP.DataManager.DocAcceptedGradeManager();
        try
        {
            DataRow GradeMajorResRow = DocAcceptedGradeManager.NewRow();

            int GrdId = Convert.ToInt32(e.NewValues["GrdId"]);
            int MjId = Convert.ToInt32(e.NewValues["MjId"]);
            int ResId = Convert.ToInt32(e.NewValues["ResId"]);
            DataTable dtAccGrd = DocAcceptedGradeManager1.SelectConfirmedGrade(MjId, GrdId, ResId);
            if (dtAccGrd.Rows.Count >= 1)
            {
                SetErrorMessage("اطلاعات وارد شده تکراری می باشد.");
                GridViewConfirmedGrade.CancelEdit();
                return;
            }
            GradeMajorResRow["GrdId"] = Convert.ToInt32(e.NewValues["GrdId"]);
            GradeMajorResRow["MjId"] = Convert.ToInt32(e.NewValues["MjId"]);
            GradeMajorResRow["ResId"] = Convert.ToInt32(e.NewValues["ResId"]);
            GradeMajorResRow["InActive"] = 0;
            GradeMajorResRow["UserId"] = Utility.GetCurrentUser_UserId();
            GradeMajorResRow["ModifiedDate"] = DateTime.Now;

            DocAcceptedGradeManager.AddRow(GradeMajorResRow);

            int cn = DocAcceptedGradeManager.Save();
            if (cn > 0)
            {
                SetErrorMessage("ذخیره انجام شد.");
            }
            else
            {
                SetErrorMessage("خطایی در ذخیره انجام گرفته است");
            }
            GridViewConfirmedGrade.CancelEdit();
            GridViewConfirmedGrade.DataBind();
        }
        catch (Exception err)
        {
            GridViewConfirmedGrade.CancelEdit();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void EditGradeMajorResponsibility(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        TSP.DataManager.DocAcceptedGradeManager DocAcceptedGradeManager = new TSP.DataManager.DocAcceptedGradeManager();
        TSP.DataManager.DocAcceptedGradeManager DocAcceptedGradeManager1 = new TSP.DataManager.DocAcceptedGradeManager();
        try
        {
            if (Convert.ToInt32(e.NewValues["GrdId"]) != Convert.ToInt32(e.OldValues["GrdId"])
                || Convert.ToInt32(e.NewValues["MjId"]) != Convert.ToInt32(e.OldValues["MjId"])
                || Convert.ToInt32(e.NewValues["ResId"]) != Convert.ToInt32(e.OldValues["ResId"]))
            {
                int GrdId = Convert.ToInt32(e.NewValues["GrdId"]);
                int MjId = Convert.ToInt32(e.NewValues["MjId"]);
                int ResId = Convert.ToInt32(e.NewValues["ResId"]);
                DataTable dtAccGrd = DocAcceptedGradeManager1.SelectConfirmedGrade(MjId, GrdId, ResId);
                if (dtAccGrd.Rows.Count >= 1)
                {
                    SetErrorMessage("اطلاعات وارد شده تکراری می باشد.");
                    GridViewConfirmedGrade.CancelEdit();
                    return;
                }
            }
            DocAcceptedGradeManager.FindByCode(int.Parse(e.Keys["GMRId"].ToString()));
            if (DocAcceptedGradeManager.Count > 0)
            {
                DocAcceptedGradeManager[0].BeginEdit();
                DocAcceptedGradeManager[0]["GrdId"] = Convert.ToInt32(e.NewValues["GrdId"]);// int.Parse(cmbGrade.SelectedItem.Value.ToString());
                DocAcceptedGradeManager[0]["MjId"] = Convert.ToInt32(e.NewValues["MjId"]);// int.Parse(cmbMajor.SelectedItem.Value.ToString());
                DocAcceptedGradeManager[0]["ResId"] = Convert.ToInt32(e.NewValues["ResId"]);// int.Parse(cmbResponsibility.SelectedItem.Value.ToString());

                DocAcceptedGradeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocAcceptedGradeManager[0]["ModifiedDate"] = DateTime.Now;
                DocAcceptedGradeManager[0].EndEdit();

                int cn = DocAcceptedGradeManager.Save();

                if (cn > 0)
                {
                    SetErrorMessage("ذخیره انجام شد.");
                }
                else
                {
                    SetErrorMessage("خطایی در ذخیره انجام گرفته است");
                }
            }
            GridViewConfirmedGrade.CancelEdit();
            GridViewConfirmedGrade.DataBind();
        }
        catch (Exception err)
        {
            GridViewConfirmedGrade.CancelEdit();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 547)
            {
                SetErrorMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotDeleteBecauseOfRelatedData));
            }
            else if (se.Number == 2601)
            {
                SetErrorMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DuplicateDataError));
            }
            else
            {
                SetErrorMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
        }
        else
        {
            SetErrorMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInDelete));
        }
    }

    private void InActive(int GMRId)
    {
        TSP.DataManager.DocAcceptedGradeManager DocAcceptedGradeManager = new TSP.DataManager.DocAcceptedGradeManager();
        try
        {
            DocAcceptedGradeManager.FindByCode(GMRId);
            if (DocAcceptedGradeManager.Count == 1)
            {
                DocAcceptedGradeManager[0].BeginEdit();
                DocAcceptedGradeManager[0]["InActive"] = 1;
                DocAcceptedGradeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocAcceptedGradeManager[0]["ModifiedDate"] = DateTime.Now;
                DocAcceptedGradeManager[0].EndEdit();

                int cn = DocAcceptedGradeManager.Save();

                if (cn > 0)
                {
                    SetErrorMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                    GridViewConfirmedGrade.DataBind();
                }
                else
                {
                    SetErrorMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                }


            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }

    }

    private void Active(int GMRId)
    {
        TSP.DataManager.DocAcceptedGradeManager DocAcceptedGradeManager = new TSP.DataManager.DocAcceptedGradeManager();
        try
        {
            DocAcceptedGradeManager.FindByCode(GMRId);
            if (DocAcceptedGradeManager.Count == 1)
            {
                DocAcceptedGradeManager[0].BeginEdit();
                DocAcceptedGradeManager[0]["InActive"] = 0;
                DocAcceptedGradeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocAcceptedGradeManager[0]["ModifiedDate"] = DateTime.Now;
                DocAcceptedGradeManager[0].EndEdit();

                int cn = DocAcceptedGradeManager.Save();

                if (cn > 0)
                {
                    SetErrorMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                    GridViewConfirmedGrade.DataBind();
                }
                else
                {
                    SetErrorMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                }


            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }

    }

    private void Delete(int GMRId)
    {
        TSP.DataManager.DocAcceptedGradeManager DocAcceptedGradeManager = new TSP.DataManager.DocAcceptedGradeManager();
        try
        {
            DocAcceptedGradeManager.FindByCode(GMRId);
            if (DocAcceptedGradeManager.Count == 1)
            {
                DocAcceptedGradeManager[0].Delete();
                int cn = DocAcceptedGradeManager.Save();

                if (cn > 0)
                {
                    SetErrorMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                    GridViewConfirmedGrade.DataBind();
                }
                else
                {
                    SetErrorMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                }


            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }

    }

    private void SetErrorMessage(string Message)
    {
        GridViewConfirmedGrade.JSProperties["cpError"] = 1;
        GridViewConfirmedGrade.JSProperties["cpErrorMsg"] = Message;
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    private void ResetGridJSProperties()
    {
        GridViewConfirmedGrade.JSProperties["cpError"] = 0;
        GridViewConfirmedGrade.JSProperties["cpErrorMsg"] = "";
    }
    #endregion
}
