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

public partial class Employee_Amoozesh_CoursePrerequisite : System.Web.UI.Page
{
    #region Private Members
    int _CourseId
    {
        get
        {

            return Convert.ToInt32(HiddenFieldPre["CrsId"]);
        }
        set
        {
            HiddenFieldPre["CrsId"] = value.ToString();
        }
    }

    string _PageMode
    {
        get
        {
            return HiddenFieldPre["PageMode"].ToString();
        }
        set
        {
            HiddenFieldPre["PageMode"] = value;
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

            TSP.DataManager.Permission per = TSP.DataManager.CoursePrerequisiteManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            GridViewPrerequisite.Visible = per.CanView;

            if (string.IsNullOrEmpty(Request.QueryString["CrsId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Course.aspx");
                return;
            }
            _CourseId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["CrsId"]));
            _PageMode = Utility.DecryptQS(Request.QueryString["PageMode"]);
            ObjdsPrerequisite.SelectParameters[0].DefaultValue = _CourseId.ToString();
            TSP.DataManager.CourseManager CourseManager = new TSP.DataManager.CourseManager();
            CourseManager.FindByCode(_CourseId);
            if (CourseManager.Count > 0)
            {
                RoundPanelPrerequisite.HeaderText = CourseManager[0]["CrsName"].ToString();
            }
            else
            {
                Response.Redirect("~/Emplooye/Amoozesh/Course.aspx");
            }
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        btnBack.PostBackUrl = "AddCourses.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"];
        btnBack2.PostBackUrl = "AddCourses.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"];
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int PreId = -1;
        if (GridViewPrerequisite.FocusedRowIndex < 0)
        {
            SetMessage("ابتدا یک رکورد را انتخاب نمایید");
            return;
        }
        DataRow PreRow = GridViewPrerequisite.GetDataRow(GridViewPrerequisite.FocusedRowIndex);
        PreId = (int)(PreRow["PrerequisiteId"]);
        TSP.DataManager.CoursePrerequisiteManager CoursePrerequisiteManager = new TSP.DataManager.CoursePrerequisiteManager();
        CoursePrerequisiteManager.FindByCode(PreId);
        if (CoursePrerequisiteManager.Count <= 0)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        CoursePrerequisiteManager[0].Delete();
        if (CoursePrerequisiteManager.Save() > 0)
        {
            GridViewPrerequisite.DataBind();
            SetMessage("ذخیره انجام شد.");
        }
        else
        {
            SetMessage("خطایی در ذخیره انجام گرفته است.");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddCourses.aspx?CrsId=" + HiddenFieldPre["CrsId"].ToString() + "&PageMode=" + HiddenFieldPre["PageMode"].ToString() + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
    }

    protected void MenuCourseDetail_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Refrences":
                Response.Redirect("~/Employee/Amoozesh/CourseRefrences.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "CourseDetail":
                Response.Redirect("AddCourseDetails.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Course":
                Response.Redirect("AddCourses.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Group":
                Response.Redirect("CourseGroups.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Attachment":
                Response.Redirect("CourseAttachments.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
        }
    }

    protected void GridViewPrerequisite_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        if (Page.IsValid)
        {
            InsertCoursePrerequisite(e);
        }
    }

    protected void GridViewPrerequisite_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditCoursePrerequisite(e);
    }
    #endregion

    #region Methods
    private void InsertCoursePrerequisite(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {
            TSP.DataManager.CoursePrerequisiteManager CoursePrerequisiteManager = new TSP.DataManager.CoursePrerequisiteManager();
            DataRow PreRow = CoursePrerequisiteManager.NewRow();
            PreRow["CrsId"] = _CourseId;
            PreRow["ParentCrsId"] = e.NewValues["ParentCrsId"];
            PreRow["Description"] = e.NewValues["Description"];
            PreRow["UserId"] = Utility.GetCurrentUser_UserId();
            PreRow["ModifiedDate"] = DateTime.Now;
            CoursePrerequisiteManager.AddRow(PreRow);
            if (CoursePrerequisiteManager.Save() <= 0)
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
            }
            else
                SetMessage("ذخیره انجام شد.");
        }
        catch (Exception err)
        {
            Utility.Messages.GetExceptionError(err);
        }
        GridViewPrerequisite.CancelEdit();
    }

    private void EditCoursePrerequisite(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            TSP.DataManager.CoursePrerequisiteManager CoursePrerequisiteManager = new TSP.DataManager.CoursePrerequisiteManager();
            CoursePrerequisiteManager.FindByCode((int)(e.Keys[0]));
            if (CoursePrerequisiteManager.Count != 1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                GridViewPrerequisite.CancelEdit();
                return;
            }
            CoursePrerequisiteManager[0].BeginEdit();
            CoursePrerequisiteManager[0]["ParentCrsId"] = e.NewValues["ParentCrsId"];
            CoursePrerequisiteManager[0]["Description"] = e.NewValues["Description"];
            CoursePrerequisiteManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            CoursePrerequisiteManager[0]["ModifiedDate"] = DateTime.Now;
            CoursePrerequisiteManager[0].EndEdit();

            if (CoursePrerequisiteManager.Save() > 0)
            {
                SetMessage("ذخیره انجام شد.");
            }
            else
            {
                SetMessage("خطایی در ذخیره انجام گرفته است.");
            }

        }
        catch (Exception err)
        {
            SetMessage(Utility.Messages.GetExceptionError(err));
        }
        GridViewPrerequisite.CancelEdit();
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}
