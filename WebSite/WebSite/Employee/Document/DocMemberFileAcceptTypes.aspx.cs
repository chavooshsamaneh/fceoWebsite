using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Document_DocMemberFileAcceptTypes : System.Web.UI.Page
{
    #region Events
   protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileAcceptTypeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            grdTypes.Visible = per.CanView;
            btnActive2.Enabled = btnActive2.Enabled = btnInActive.Enabled = btnInActive2.Enabled = per.CanEdit;
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
            TSP.DataManager.DocMemberFileAcceptTypeManager DocMemberFileAcceptTypeManager = new TSP.DataManager.DocMemberFileAcceptTypeManager();

            try
            {
                DataRow d = DocMemberFileAcceptTypeManager.NewRow();
                d["ActTypeName"] = e.NewValues["ActTypeName"];
                d["Description"] = e.NewValues["Description"];
                d["InActive"] = false;
                d["UserId"] = Utility.GetCurrentUser_UserId();
                d["ModifiedDate"] = DateTime.Now;
                DocMemberFileAcceptTypeManager.AddRow(d);
                int cnt = DocMemberFileAcceptTypeManager.Save();

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
            TSP.DataManager.DocMemberFileAcceptTypeManager DocMemberFileAcceptTypeManager = new TSP.DataManager.DocMemberFileAcceptTypeManager();
            DocMemberFileAcceptTypeManager.FindByCode(Convert.ToInt32(e.Keys["ActTypeId"]));
            if (DocMemberFileAcceptTypeManager.Count > 0)
            {
                DocMemberFileAcceptTypeManager[0].BeginEdit();
                DocMemberFileAcceptTypeManager[0]["ActTypeName"] = e.NewValues["ActTypeName"];
                DocMemberFileAcceptTypeManager[0]["Description"] = e.NewValues["Description"];
                DocMemberFileAcceptTypeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocMemberFileAcceptTypeManager[0]["ModifiedDate"] = DateTime.Now;
                DocMemberFileAcceptTypeManager[0].EndEdit();

                int cn = DocMemberFileAcceptTypeManager.Save();
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
        TSP.DataManager.DocMemberFileAcceptTypeManager DocMemberFileAcceptTypeManager = new TSP.DataManager.DocMemberFileAcceptTypeManager();
        try
        {
            DocMemberFileAcceptTypeManager.FindByCode(Convert.ToInt32(grdTypes.GetDataRow(grdTypes.FocusedRowIndex)["ActTypeId"]));
            if (DocMemberFileAcceptTypeManager.Count == 1)
            {
                if (Convert.ToInt32(DocMemberFileAcceptTypeManager[0]["ActTypeCode"]) == (int)TSP.DataManager.DocumentMemberFileAcceptType.GradeJumping)
                {
                    ShowMessage("امکان حذف این شیوه اخذ پروانه وجود ندارد.این شیوه اخذ ، جهت دریافت پایه بدون در نظر گرفتن توالی پایه ها تعریف شده است");
                    return;
                }
                DocMemberFileAcceptTypeManager[0].Delete();
                int cn = DocMemberFileAcceptTypeManager.Save();
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
        TSP.DataManager.DocMemberFileAcceptTypeManager DocMemberFileAcceptTypeManager = new TSP.DataManager.DocMemberFileAcceptTypeManager();
        try
        {
            DocMemberFileAcceptTypeManager.FindByCode(Convert.ToInt32(grdTypes.GetDataRow(grdTypes.FocusedRowIndex)["ActTypeId"]));
            if (DocMemberFileAcceptTypeManager.Count == 1)
            {
                DocMemberFileAcceptTypeManager[0].BeginEdit();
                DocMemberFileAcceptTypeManager[0]["InActive"] = true;
                DocMemberFileAcceptTypeManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                DocMemberFileAcceptTypeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocMemberFileAcceptTypeManager[0]["ModifiedDate"] = DateTime.Now;
                DocMemberFileAcceptTypeManager[0].EndEdit();
                int cn = DocMemberFileAcceptTypeManager.Save();
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

    protected void btnActive_Click(object sender, EventArgs e)
    {
        TSP.DataManager.DocMemberFileAcceptTypeManager DocMemberFileAcceptTypeManager = new TSP.DataManager.DocMemberFileAcceptTypeManager();
        try
        {
            DocMemberFileAcceptTypeManager.FindByCode(Convert.ToInt32(grdTypes.GetDataRow(grdTypes.FocusedRowIndex)["ActTypeId"]));
            if (DocMemberFileAcceptTypeManager.Count == 1)
            {
                DocMemberFileAcceptTypeManager[0].BeginEdit();
                DocMemberFileAcceptTypeManager[0]["InActive"] = false;
                DocMemberFileAcceptTypeManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                DocMemberFileAcceptTypeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                DocMemberFileAcceptTypeManager[0]["ModifiedDate"] = DateTime.Now;
                DocMemberFileAcceptTypeManager[0].EndEdit();
                int cn = DocMemberFileAcceptTypeManager.Save();
                if (cn == 1)
                {
                    grdTypes.DataBind();
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                }
                else
                {
                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
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
    #endregion
}
