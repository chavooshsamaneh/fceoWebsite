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

public partial class Employee_Amoozesh_CourseRefrences : System.Web.UI.Page
{
    #region Private Members
    int _CourseId
    {
        get
        {

            return Convert.ToInt32(HiddenFieldCourseRef["CrsId"]);
        }
        set
        {
            HiddenFieldCourseRef["CrsId"] = value.ToString();
        }
    }

    string _PageMode
    {
        get
        {
            return HiddenFieldCourseRef["PageMode"].ToString();
        }
        set
        {
            HiddenFieldCourseRef["PageMode"] = value;
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.CourseRefrenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            GridViewCourseRefrences.Visible = per.CanView;

            if (string.IsNullOrEmpty(Request.QueryString["CrsId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Course.aspx");
                return;
            }
            _CourseId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["CrsId"]));
            _PageMode = Utility.DecryptQS(Request.QueryString["PageMode"]);
            ObjdsCourseRefrences.SelectParameters[0].DefaultValue = _CourseId.ToString();
            TSP.DataManager.CourseManager CourseManager = new TSP.DataManager.CourseManager();
            CourseManager.FindByCode(_CourseId);
            if (CourseManager.Count > 0)
            {
                RoundPanelCourseRefrence.HeaderText = CourseManager[0]["CrsName"].ToString();
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

        btnBack.PostBackUrl = "AddCourses.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"];
        btnBack.PostBackUrl = "AddCourses.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"];

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int RefId = -1;
            if (GridViewCourseRefrences.FocusedRowIndex <0)
            {
                SetMessage("ابتدا یک ردیف را انتخاب نمایید.");
                return;
            }
            TSP.DataManager.CourseRefrenceManager CourseRefrenceManager = new TSP.DataManager.CourseRefrenceManager();

            DataRow CourseRefRow = GridViewCourseRefrences.GetDataRow(GridViewCourseRefrences.FocusedRowIndex);
            RefId = (int)(CourseRefRow["RefrenceId"]);

            CourseRefrenceManager.FindByCode(RefId);
            if (CourseRefrenceManager.Count != 0)
            {
                CourseRefrenceManager[0].Delete();
                int cn = CourseRefrenceManager.Save();
                if (cn > 0)
                {
                    GridViewCourseRefrences.DataBind();
                    SetMessage("ذخیره انجام شد.");
                }
                else
                {
                    SetMessage("خطایی در ذخیره انجام گرفته است.");
                }
            }
            else
            {
                SetMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInDelete));
        }
    }

    protected void GridViewCourseRefrences_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        if (Page.IsValid)
            InserCourseRefrence(e);
    }

    protected void GridViewCourseRefrences_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditCourseRefrence(e);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddCourses.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
    }

    protected void MenuCourseDetail_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Prerequisite":
                Response.Redirect("~/Employee/Amoozesh/CoursePrerequisite.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "CourseDetail":
                Response.Redirect("AddCourseDetails.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Course":
                Response.Redirect("AddCourses.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Group":
                Response.Redirect("CourseGroups.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Attachment":
                Response.Redirect("CourseAttachments.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
        }
    }
    #endregion

    #region Methods
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void InserCourseRefrence(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {
            TSP.DataManager.CourseRefrenceManager CourseRefrenceManager = new TSP.DataManager.CourseRefrenceManager();

            DataRow CourseRefRow = CourseRefrenceManager.NewRow();
            CourseRefRow.BeginEdit();
            CourseRefRow["CrsId"] = _CourseId;
            CourseRefRow["RefName"] = e.NewValues["RefName"];
            CourseRefRow["RefAuthor"] = e.NewValues["RefAuthor"];
            if (e.NewValues["RefTopics"] == null)
                CourseRefRow["RefTopics"] = "";
            else
                CourseRefRow["RefTopics"] = e.NewValues["RefTopics"];
            if (e.NewValues["Description"] == null)
                CourseRefRow["Description"] = "";
            else
                CourseRefRow["Description"] = e.NewValues["Description"];
            CourseRefRow["UserId"] = Utility.GetCurrentUser_UserId();
            CourseRefRow["ModifiedDate"] = DateTime.Now;

            CourseRefrenceManager.AddRow(CourseRefRow);

            if (CourseRefrenceManager.Save() <= 0)
            {
                SetMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            SetMessage("ذخیره انجام شد.");
        }
        catch (Exception err)
        {
            SetMessage(Utility.Messages.GetExceptionError(err));
        }
        GridViewCourseRefrences.CancelEdit();
    }

    private void EditCourseRefrence(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        TSP.DataManager.CourseRefrenceManager CourseRefrenceManager = new TSP.DataManager.CourseRefrenceManager();
        CourseRefrenceManager.Fill();
        DataRow RefRow = CourseRefrenceManager.DataTable.Rows.Find((int)(e.Keys[0]));
        try
        {
            RefRow.BeginEdit();
            if (e.NewValues["RefTopics"] == null)
                RefRow["RefTopics"] = "";
            else
                RefRow["RefTopics"] = e.NewValues["RefTopics"];
            RefRow["RefName"] = e.NewValues["RefName"];
            RefRow["RefAuthor"] = e.NewValues["RefAuthor"];
            if (e.NewValues["Description"] == null)
                RefRow["Description"] = "";
            else
                RefRow["Description"] = e.NewValues["Description"];
            RefRow["UserId"] = Utility.GetCurrentUser_UserId();
            RefRow["ModifiedDate"] = DateTime.Now;
            RefRow.EndEdit();
            if (CourseRefrenceManager.Save() <= 0)
            {
                SetMessage("خطایی در ذخیره انجام گرفته است.");
            }
            SetMessage("ذخیره انجام شد.");

        }
        catch (Exception err)
        {
            SetMessage(Utility.Messages.GetExceptionError(err));
        }
        GridViewCourseRefrences.CancelEdit();
    }
    #endregion
}
