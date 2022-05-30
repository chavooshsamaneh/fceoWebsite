using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Employee_Amoozesh_Lecturer : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            CheckUserPermission();

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
          
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnDisActive"] != null)
            this.btnDisActive.Enabled = this.btnDisActive2.Enabled = (bool)this.ViewState["BtnDisActive"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddLecturer.aspx?TeId=" + Utility.EncryptQS("-1") + "&PageMode=" + Utility.EncryptQS("New"));

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int TeId = -1;
        bool InActive = false;
        if (GridViewLecturer.FocusedRowIndex > -1)
        {
            DataRow row = GridViewLecturer.GetDataRow(GridViewLecturer.FocusedRowIndex);
            TeId = (int)row["TeId"];
            InActive = (bool)row["InActive"];

        }
        if (TeId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            if (InActive)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ویرایش اطلاعات برای رکورد غیر فعال وجود ندارد";

            }
            else
                Response.Redirect("AddLecturer.aspx?TeId=" + Utility.EncryptQS(TeId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));
        }

    }
    protected void btnDisActive_Click(object sender, EventArgs e)
    {
        int TeId = -1;
        if (GridViewLecturer.FocusedRowIndex > -1)
        {
            DataRow row = GridViewLecturer.GetDataRow(GridViewLecturer.FocusedRowIndex);
            TeId = (int)row["TeId"];
            bool InActive = (bool)row["InActive"];

            try
            {
                if (InActive)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                    return;
                }
                TSP.DataManager.TeacherManager Manager = new TSP.DataManager.TeacherManager();
                Manager.SelectLecturer(TeId);
                if (Manager.Count > 0)
                {


                    Manager[0].BeginEdit();

                    Manager[0]["InActive"] = 1;
                    Manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    Manager[0].EndEdit();

                    int cn = Manager.Save();
                    if (cn > 0)
                    {
                        GridViewLecturer.DataBind();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد.";
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                    }
                }
            }
            catch (Exception err)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            }
        }
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        int TeId = -1;

        if (GridViewLecturer.FocusedRowIndex > -1)
        {
            DataRow row = GridViewLecturer.GetDataRow(GridViewLecturer.FocusedRowIndex);
            TeId = (int)row["TeId"];
            bool InActive = (bool)row["InActive"];
            try
            {
                if (!InActive)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "رکورد مورد نظر فعال می باشد";
                    return;
                }
                TSP.DataManager.TeacherManager Manager = new TSP.DataManager.TeacherManager();
                Manager.SelectLecturer(TeId);
                if (Manager.Count > 0)
                {

                    Manager[0].BeginEdit();

                    Manager[0]["InActive"] = 0;
                    Manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    Manager[0].EndEdit();

                    int cn = Manager.Save();
                    if (cn > 0)
                    {
                        GridViewLecturer.DataBind();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد.";
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                    }
                }
            }
            catch (Exception err)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
            }
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int TeId = -1;
        if (GridViewLecturer.FocusedRowIndex > -1)
        {
            DataRow row = GridViewLecturer.GetDataRow(GridViewLecturer.FocusedRowIndex);
            TeId = (int)row["TeId"];
        }
        if (TeId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
            Response.Redirect("AddLecturer.aspx?TeId=" + Utility.EncryptQS(TeId.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));

    }

    private void CheckUserPermission()
    {
        //****Check table permission
        TSP.DataManager.Permission per = TSP.DataManager.TeacherManager.GetUserPermissionForLecturer(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnNew.Enabled = btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = btnEdit2.Enabled =btnActive.Enabled=btnActive2.Enabled=btnDisActive.Enabled=btnDisActive2.Enabled= per.CanEdit;
        btnView.Enabled =btnView2.Enabled=GridViewLecturer.Visible= per.CanView;
    }
}