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

public partial class Employee_Amoozesh_CourseGroups : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["CrsId"])))
            {
                Response.Redirect("Course.aspx");
                return;
            }

            TSP.DataManager.Permission per = TSP.DataManager.CourseGroupsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnDelete.Enabled = per.CanDelete;
            btnDelete1.Enabled = per.CanDelete;

            try
            {
                CourseId.Value = Server.HtmlDecode(Request.QueryString["CrsId"]).ToString();
            }
            catch (Exception err)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }


            string CrsId = Utility.DecryptQS(CourseId.Value);
            if (string.IsNullOrEmpty(CrsId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            OdbGrid.FilterParameters[0].DefaultValue = CrsId;

        }

        btnBack.PostBackUrl = "AddCourses.aspx?CrsId=" + CourseId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"];
        ASPxButton6.PostBackUrl = "AddCourses.aspx?CrsId=" + CourseId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"];
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Id = -1;
        if (DevGridViewGroups.FocusedRowIndex < 0)
        {
            SetMessage("ابتدا یک رکورد را انتخاب نمایید");
            return;
        } DataRow row = DevGridViewGroups.GetDataRow(DevGridViewGroups.FocusedRowIndex);
        Id = (int)row["CrsGrId"];
        if (Id == -1)
        {
            SetMessage("لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        Delete(Id);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddCourses.aspx?CrsId=" + CourseId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"]);

    }

    protected void MenuCourseDetails_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Prerequisite":
                Response.Redirect("~/Employee/Amoozesh/CoursePrerequisite.aspx?CrsId=" + CourseId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "CourseRefrence":
                Response.Redirect("~/Employee/Amoozesh/CourseRefrences.aspx?CrsId=" + CourseId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "CourseDetail":
                Response.Redirect("AddCourseDetails.aspx?CrsId=" + CourseId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Course":
                Response.Redirect("AddCourses.aspx?CrsId=" + CourseId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Attachment":
                Response.Redirect("CourseAttachments.aspx?CrsId=" + CourseId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
        }
    }

    protected void btnSaveGroup_Click(object sender, EventArgs e)
    {
        TSP.DataManager.CourseGroupsManager GroupManager = new TSP.DataManager.CourseGroupsManager();
        try
        {
            if (TreeListTrainingGroups.GetSelectedNodes().Count == 0)
            {
                SetMessage("گروهی برای ذخیره انتخاب نشده است");
                return;
            }
            foreach (DevExpress.Web.ASPxTreeList.TreeListNode TreeNode in TreeListTrainingGroups.GetSelectedNodes())
            {
                for (int i = 0; i < DevGridViewGroups.VisibleRowCount; i++)
                {
                    if (TreeNode["TgrId"].ToString() == DevGridViewGroups.GetDataRow(i)["TgrId"].ToString())
                    {
                        TreeListTrainingGroups.UnselectAll();

                        SetMessage("گروه مورد نظر قبلاً انتخاب شده است");
                        return;

                    }
                }

                DataRow dr = GroupManager.NewRow();
                dr["CrsId"] = int.Parse(Utility.DecryptQS(CourseId.Value));
                dr["UserId"] = Utility.GetCurrentUser_UserId();
                dr["ModifiedDate"] = DateTime.Now;
                dr["TgrId"] = TreeNode["TgrId"].ToString();
                GroupManager.AddRow(dr);
                GroupManager.Save();
                GroupManager.DataTable.AcceptChanges();
            }
            TreeListTrainingGroups.UnselectAll();

            SetMessage("ذخیره انجام شد");

            DevGridViewGroups.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetExceptionError(err));
        }
    }
    #endregion

    #region Methods
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    protected void Delete(int Id)
    {

        try
        {
            TSP.DataManager.CourseGroupsManager Manager = new TSP.DataManager.CourseGroupsManager();
            Manager.FindByCode(Id);
            if (Manager.Count != 1)
            {
                SetMessage("خطایی در حذف انجام گرفته است");
                return;
            }
            Manager[0].Delete();
            if (Manager.Save() > 0)
            {
                DevGridViewGroups.DataBind();
                SetMessage("ذخیره انجام شد");
            }
            else
            {
                SetMessage("خطایی در حذف انجام گرفته است");
            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetExceptionError(err));
        }
    }
    #endregion
}
