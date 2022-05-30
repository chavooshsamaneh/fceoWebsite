using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Document_ResponsibilityTypes : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.ResponcibilityTypeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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
            TSP.DataManager.ResponcibilityTypeManager ResponcibilityTypeManager = new TSP.DataManager.ResponcibilityTypeManager();

            try
            {
                DataRow d = ResponcibilityTypeManager.NewRow();
                d["ResName"] = e.NewValues["ResName"];
                d["InActive"] = false;
                d["UserId"] = Utility.GetCurrentUser_UserId();
                d["ModifiedDate"] = DateTime.Now;
                ResponcibilityTypeManager.AddRow(d);
                int cnt = ResponcibilityTypeManager.Save();

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
            if (CheckIsDefaulTypes(Convert.ToInt32(e.Keys["ResId"])))
            {
                grdTypes.JSProperties["cpMessage"] = "نوع های پیش فرض قابل ویرایش نیستند";
                grdTypes.CancelEdit();
                return;
            }


            TSP.DataManager.ResponcibilityTypeManager ResponcibilityTypeManager = new TSP.DataManager.ResponcibilityTypeManager();
            ResponcibilityTypeManager.FindByCode(Convert.ToInt32(e.Keys["ResId"]));
            if (ResponcibilityTypeManager.Count > 0)
            {
                ResponcibilityTypeManager[0].BeginEdit();
                ResponcibilityTypeManager[0]["ResName"] = e.NewValues["ResName"];
                ResponcibilityTypeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ResponcibilityTypeManager[0]["ModifiedDate"] = DateTime.Now;
                ResponcibilityTypeManager[0].EndEdit();

                int cn = ResponcibilityTypeManager.Save();
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
        TSP.DataManager.ResponcibilityTypeManager ResponcibilityTypeManager = new TSP.DataManager.ResponcibilityTypeManager();
        try
        {
            if (CheckIsDefaulTypesForDelete(Convert.ToInt32(grdTypes.GetDataRow(grdTypes.FocusedRowIndex)["ResId"])))
            {
                ShowMessage("نوع های پیش فرض قابل حذف نیستند");
                grdTypes.CancelEdit();
                return;
            }

            ResponcibilityTypeManager.FindByCode(Convert.ToInt32(grdTypes.GetDataRow(grdTypes.FocusedRowIndex)["ResId"]));
            if (ResponcibilityTypeManager.Count == 1)
            {
                ResponcibilityTypeManager[0].Delete();
                int cn = ResponcibilityTypeManager.Save();
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
        TSP.DataManager.ResponcibilityTypeManager ResponcibilityTypeManager = new TSP.DataManager.ResponcibilityTypeManager();
        try
        {
            ResponcibilityTypeManager.FindByCode(Convert.ToInt32(grdTypes.GetDataRow(grdTypes.FocusedRowIndex)["ResId"]));
            if (ResponcibilityTypeManager.Count == 1)
            {
                ResponcibilityTypeManager[0].BeginEdit();
                ResponcibilityTypeManager[0]["InActive"] = true;
                ResponcibilityTypeManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                ResponcibilityTypeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ResponcibilityTypeManager[0]["ModifiedDate"] = DateTime.Now;
                ResponcibilityTypeManager[0].EndEdit();
                int cn = ResponcibilityTypeManager.Save();
                if (cn == 1)
                {
                    grdTypes.DataBind();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
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

    protected void btnActive_Click(object sender, EventArgs e)
    {
        TSP.DataManager.ResponcibilityTypeManager ResponcibilityTypeManager = new TSP.DataManager.ResponcibilityTypeManager();
        try
        {
            ResponcibilityTypeManager.FindByCode(Convert.ToInt32(grdTypes.GetDataRow(grdTypes.FocusedRowIndex)["ResId"]));
            if (ResponcibilityTypeManager.Count == 1)
            {
                ResponcibilityTypeManager[0].BeginEdit();
                ResponcibilityTypeManager[0]["InActive"] = false;
                ResponcibilityTypeManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                ResponcibilityTypeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ResponcibilityTypeManager[0]["ModifiedDate"] = DateTime.Now;
                ResponcibilityTypeManager[0].EndEdit();
                int cn = ResponcibilityTypeManager.Save();
                if (cn == 1)
                {
                    grdTypes.DataBind();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
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

    #endregion

    #region Methods
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

    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("Style", "display:visible");
        this.LabelWarning.Text = Message;
    }

    Boolean CheckIsDefaulTypes(int Id)
    {
        if (Id == (int)TSP.DataManager.DocumentResponsibilityType.Design ||
       Id == (int)TSP.DataManager.DocumentResponsibilityType.Implement ||
       Id == (int)TSP.DataManager.DocumentResponsibilityType.Observation)
            return true;
        return false;
    }

    Boolean CheckIsDefaulTypesForDelete(int Id)
    {
        if (Id == (int)TSP.DataManager.DocumentResponsibilityType.Design ||
        Id == (int)TSP.DataManager.DocumentResponsibilityType.Implement ||
        Id == (int)TSP.DataManager.DocumentResponsibilityType.Observation ||
        Id == (int)TSP.DataManager.DocumentResponsibilityType.Traffic ||
        Id == (int)TSP.DataManager.DocumentResponsibilityType.Urbanism ||
        Id == (int)TSP.DataManager.DocumentResponsibilityType.Mapping)
            return true;
        return false;
    }
    #endregion

}
