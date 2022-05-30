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

public partial class TechnicalServices_PriceArchive : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (IsPostBack == false)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.PriceArchiveManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            if (per.CanView == false)
            {
                grdPriceArchive.Visible = false;
                btnView.Enabled = btnView2.Enabled = false;
                btnEdit.Enabled = btnEdit2.Enabled = false;
                btnNew.Enabled = btnNew2.Enabled = false;
            }
            if (per.CanEdit == false)
            {
                btnEdit.Enabled = btnEdit2.Enabled = false;
            }
            if (per.CanNew == false)
            {
                btnNew.Enabled = btnNew2.Enabled = false;
            }
            if (per.CanDelete == false)
            {
                btnDelete.Enabled = btnDelete2.Enabled = per.CanDelete;
            }
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("SavePriceArchive.aspx?PT=" + Utility.EncryptQS("1") + "&PrAId=" + Utility.EncryptQS("0"));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        // grdPriceArchive.DataBind();
        if (grdPriceArchive.FocusedRowIndex >= 0)
            Response.Redirect("SavePriceArchive.aspx?PT=" + Utility.EncryptQS("2") + "&PrAId=" + Utility.EncryptQS(grdPriceArchive.GetDataRow(grdPriceArchive.FocusedRowIndex)["PriceArchiveId"].ToString()));
        else
            ShowMessage("رکوردی انتخاب نشده است");

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        //  grdPriceArchive.DataBind();
        if (grdPriceArchive.FocusedRowIndex >= 0)
            Response.Redirect("SavePriceArchive.aspx?PT=" + Utility.EncryptQS("3") + "&PrAId=" + Utility.EncryptQS(grdPriceArchive.GetDataRow(grdPriceArchive.FocusedRowIndex)["PriceArchiveId"].ToString()));
        else
            ShowMessage("رکوردی انتخاب نشده است");

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        grdPriceArchive.DataBind();
        if (grdPriceArchive.FocusedRowIndex >= 0)
        {
            int PriceArchiveId = Convert.ToInt32(grdPriceArchive.GetDataRow(grdPriceArchive.FocusedRowIndex)["PriceArchiveId"]);
            Delete(PriceArchiveId);
            grdPriceArchive.DataBind();
        }
        else
        {
            ShowMessage("رکوردی انتخاب نشده است");
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (grdPriceArchive.FocusedRowIndex < 0)
        {
            ShowMessage("رکوردی انتخاب نشده است");
            return;
        }
        ChangeActiveStatus(Convert.ToInt32(grdPriceArchive.GetDataRow(grdPriceArchive.FocusedRowIndex)["PriceArchiveId"]), true);
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        if (grdPriceArchive.FocusedRowIndex < 0)
        {
            ShowMessage("رکوردی انتخاب نشده است");
            return;
        }
        ChangeActiveStatus(Convert.ToInt32(grdPriceArchive.GetDataRow(grdPriceArchive.FocusedRowIndex)["PriceArchiveId"]), false);
    }

    protected void grdPriceArchive_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "DocumentNo")
            e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";
        if (e.Column.FieldName == "DocumentNo")
            e.Editor.Style["direction"] = "ltr";
        if (e.Column.FieldName == "DocumentDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void grdPriceArchive_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "DocumentNo":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "DocumentDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }

    }
    #endregion

    #region Methods
    private void Delete(int PriceArchiveId)
    {
        try
        {
            TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();
            PriceArchiveManager.DeletePriceArchive(PriceArchiveId);
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
        }
        catch (Exception err)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInDelete));
            Utility.SaveWebsiteError(err);
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void ChangeActiveStatus(int PriceArchiveId, Boolean InActive)
    {
        try
        {
            TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();
            PriceArchiveManager.FindById(PriceArchiveId);
            if (PriceArchiveManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            PriceArchiveManager[0].BeginEdit();
            PriceArchiveManager[0]["InActive"] = InActive;
            PriceArchiveManager[0].EndEdit();
            PriceArchiveManager.Save();
            grdPriceArchive.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }
    #endregion
}
