using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Document_MajorTestTypes : System.Web.UI.Page
{
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        if (Context != null && Context.Request.UserAgent != null)
        {
            // If it is FIREFOX Browser
            if (Context.Request.UserAgent.ToLower().Contains("mozilla"))
            {
                Response.Cache.SetNoStore();
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.DocMajorTestTypeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            grdTypes.Visible = per.CanView;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

            grdTypes.JSProperties["cpMessage"] = "";
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void grdTypes_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        grdTypes.JSProperties["cpMessage"] = "";
        e.Cancel = true;

        if (Page.IsValid)
        {
            TSP.DataManager.DocMajorTestTypeManager DocMajorTestTypeManager = new TSP.DataManager.DocMajorTestTypeManager();

            try
            {
                DataRow d = DocMajorTestTypeManager.NewRow();
                d["TTypeId"] = e.NewValues["TTypeId"];
                d["MjId"] = e.NewValues["MjId"];
                d["InActive"] = false;
                d["UserId"] = Utility.GetCurrentUser_UserId();
                d["ModifiedDate"] = DateTime.Now;
                DocMajorTestTypeManager.AddRow(d);
                int cnt = DocMajorTestTypeManager.Save();

                if (cnt > 0)
                {
                    grdTypes.DataBind();
                    grdTypes.JSProperties["cpMessage"] = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete);
                }
                else
                {
                    grdTypes.JSProperties["cpMessage"] = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave);
                }
                grdTypes.CancelEdit();


            }
            catch (Exception err)
            {
                grdTypes.CancelEdit();
                Utility.SaveWebsiteError(err);
                String Error = Utility.Messages.GetExceptionError(err);
                if (String.IsNullOrWhiteSpace(Error) == false)
                {
                    grdTypes.JSProperties["cpMessage"] = Error;
                }
                else
                {
                    grdTypes.JSProperties["cpMessage"] = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave);
                }
            }
        }

    }

    protected void grdTypes_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        grdTypes.JSProperties["cpMessage"] = "";
        e.Cancel = true;

        try
        {
            TSP.DataManager.DocMajorTestTypeManager DocMajorTestTypeManager = new TSP.DataManager.DocMajorTestTypeManager();
            DocMajorTestTypeManager.FindByCode(Convert.ToInt32(e.Keys["MjTTypeId"]));
            if (DocMajorTestTypeManager.Count > 0)
            {
                DocMajorTestTypeManager[0].BeginEdit();
                DocMajorTestTypeManager[0]["TTypeId"] = e.NewValues["TTypeId"];
                DocMajorTestTypeManager[0]["MjId"] = e.NewValues["MjId"];
                DocMajorTestTypeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocMajorTestTypeManager[0]["ModifiedDate"] = DateTime.Now;
                DocMajorTestTypeManager[0].EndEdit();

                int cn = DocMajorTestTypeManager.Save();
                if (cn > 0)
                {
                    grdTypes.DataBind();
                    grdTypes.JSProperties["cpMessage"] = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete);
                }
                else
                {
                    grdTypes.JSProperties["cpMessage"] = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave);
                }
                grdTypes.CancelEdit();

            }
        }
        catch (Exception err)
        {
            grdTypes.CancelEdit();
            Utility.SaveWebsiteError(err);
            String Error = Utility.Messages.GetExceptionError(err);
            if (String.IsNullOrWhiteSpace(Error) == false)
            {
                grdTypes.JSProperties["cpMessage"] = Error;
            }
            else
            {
                grdTypes.JSProperties["cpMessage"] = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave);
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        TSP.DataManager.DocMajorTestTypeManager DocMajorTestTypeManager = new TSP.DataManager.DocMajorTestTypeManager();
        try
        {
            DocMajorTestTypeManager.FindByCode(Convert.ToInt32(grdTypes.GetDataRow(grdTypes.FocusedRowIndex)["MjTTypeId"]));
            if (DocMajorTestTypeManager.Count == 1)
            {
                DocMajorTestTypeManager[0].Delete();
                int cn = DocMajorTestTypeManager.Save();
                if (cn == 1)
                {
                    grdTypes.DataBind();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.DeleteComplete));
                }
                else
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInDelete));
                }
            }
            else
            {
                ShowMessage("اطلاعات توسط کاربر دیگری تغییر یافته است");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            String Error = Utility.Messages.GetExceptionError(err);
            if (String.IsNullOrWhiteSpace(Error) == false)
            {
                ShowMessage(Error);
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        TSP.DataManager.DocMajorTestTypeManager DocMajorTestTypeManager = new TSP.DataManager.DocMajorTestTypeManager();
        try
        {
            DocMajorTestTypeManager.FindByCode(Convert.ToInt32(grdTypes.GetDataRow(grdTypes.FocusedRowIndex)["MjTTypeId"]));
            if (DocMajorTestTypeManager.Count == 1)
            {
                DocMajorTestTypeManager[0].BeginEdit();
                DocMajorTestTypeManager[0]["InActive"] = true;
                DocMajorTestTypeManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                DocMajorTestTypeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocMajorTestTypeManager[0]["ModifiedDate"] = DateTime.Now;
                DocMajorTestTypeManager[0].EndEdit();
                int cn = DocMajorTestTypeManager.Save();
                if (cn == 1)
                {
                    grdTypes.DataBind();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.InActiveComplete));
                }
                else
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInInActive));
                }
            }
            else
            {
                ShowMessage("اطلاعات توسط کاربر دیگری تغییر یافته است");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            String Error = Utility.Messages.GetExceptionError(err);
            if (String.IsNullOrWhiteSpace(Error) == false)
            {
                ShowMessage(Error);
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInInActive));
            }
        }
    }

    protected void grdTypes_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

        switch (e.Column.FieldName)
        {
            case "InActiveDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void grdTypes_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "InActiveDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("Style", "display:visible");
        this.LabelWarning.Text = Message;
    }
}