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
using System.IO;
using DevExpress.Web;

public partial class Employee_Amoozesh_Course : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.CourseManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDisActive.Enabled = per.CanEdit;
            btnDisActive2.Enabled = per.CanEdit;
            GridViewCourse.Visible = per.CanView;

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnDisActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;


        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnDisActive.Enabled = this.btnDisActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        SetPageFilter();

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

    #region btnClick
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddCourses.aspx?CrsId=" + Utility.EncryptQS("-1") + "&PageMode=" + Utility.EncryptQS("New"));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int CrsId = -1;
        string GridFilterString = GridViewCourse.FilterExpression;

        if (GridViewCourse.FocusedRowIndex > -1)
        {
            DataRow row = GridViewCourse.GetDataRow(GridViewCourse.FocusedRowIndex);
            CrsId = (int)row["CrsId"];
            if (Convert.ToInt16(row["InActive"]) == 1)
            {
                SetMessage("امکان ویرایش اطلاعات برای رکورد غیر فعال وجود ندارد");
                return;
            }
        }
        if (CrsId == -1)
        {
            SetMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        Response.Redirect("AddCourses.aspx?CrsId=" + Utility.EncryptQS(CrsId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
    }

    protected void btnDisActive_Click(object sender, EventArgs e)
    {
        int CrsId = -1;
        if (GridViewCourse.FocusedRowIndex < 0)
        {
            SetMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        DataRow row = GridViewCourse.GetDataRow(GridViewCourse.FocusedRowIndex);
        CrsId = (int)row["CrsId"];
        if (CrsId == -1)
        {
            SetMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        } 
        try
        {
            TSP.DataManager.CourseManager managerEdit = new TSP.DataManager.CourseManager();
            managerEdit.FindByCode(CrsId);
            if (managerEdit.Count != 1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }

            if (Convert.ToBoolean(managerEdit[0]["InActive"]) == true)
            {
                SetMessage("رکورد مورد نظر غیر فعال می باشد");
                return;
            }
            managerEdit[0].BeginEdit();
            managerEdit[0]["InActive"] = 1;
            managerEdit[0]["UserId"] = Utility.GetCurrentUser_UserId();
            managerEdit[0].EndEdit();

            if (managerEdit.Save() != 1)
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            GridViewCourse.DataBind();
            SetMessage("ذخیره انجام شد");
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage("خطایی در ذخیره انجام گرفته است");
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        int CrsId = -1;
        if (GridViewCourse.FocusedRowIndex < 0)
        {
            SetMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        DataRow row = GridViewCourse.GetDataRow(GridViewCourse.FocusedRowIndex);
        CrsId = (int)row["CrsId"];
        if (CrsId == -1)
        {
            SetMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        } 
        try
        {
            TSP.DataManager.CourseManager managerEdit = new TSP.DataManager.CourseManager();
            managerEdit.FindByCode(CrsId);
            if (managerEdit.Count != 1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }

            if (Convert.ToBoolean(managerEdit[0]["InActive"]) == false)
            {
                SetMessage("رکورد مورد نظر غیر فعال می باشد");
                return;
            }
            managerEdit[0].BeginEdit();
            managerEdit[0]["InActive"] = 0;
            managerEdit[0]["UserId"] = Utility.GetCurrentUser_UserId();
            managerEdit[0].EndEdit();

            if (managerEdit.Save() != 1)
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            GridViewCourse.DataBind();
            SetMessage("ذخیره انجام شد");
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage("خطایی در ذخیره انجام گرفته است");
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int CrsId = -1;
        string GridFilterString = GridViewCourse.FilterExpression;

        if (GridViewCourse.FocusedRowIndex > -1)
        {
            DataRow row = GridViewCourse.GetDataRow(GridViewCourse.FocusedRowIndex);
            CrsId = (int)row["CrsId"];
        }
        if (CrsId == -1)
        {
            SetMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");

        }
        else
        {
            Response.Redirect("AddCourses.aspx?CrsId=" + Utility.EncryptQS(CrsId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
        }

    }
   
    #endregion

    #region Grid
    protected void GridViewCourse_FocusedRowChanged(object sender, EventArgs e)
    {
        SetGridRowIndex();

    }
    #endregion

    #endregion

    #region Methods

    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());

                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewCourse.FilterExpression = GrdFlt;
            }
        }

    }

    private int SetGridRowIndex()
    {
        int Index = -1;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["PostId"]))
            {

                int PostKeyValue = int.Parse(Utility.DecryptQS(Request.QueryString["PostId"].ToString()));

                GridViewCourse.DataBind();
                Index = GridViewCourse.FindVisibleIndexByKeyValue(PostKeyValue);
                int PageIndex = -1;
                if (Index >= 0)
                    PageIndex = Index / GridViewCourse.SettingsPager.PageSize;
                if (PageIndex >= 0)
                    GridViewCourse.PageIndex = PageIndex;
                if (Index >= 0)
                {
                    GridViewCourse.FocusedRowIndex = Index;

                }
            }
        }
        return Index;
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion

}
