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

public partial class Members_Amoozesh_PreRegister : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}
        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }
        if (!IsPostBack)
        {
            //TSP.DataManager.Permission per = TSP.DataManager.PreRegisterManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)int.Parse(Session["LoginType"].ToString()));
            //btnNew.Enabled = per.CanNew;
            //btnNew2.Enabled = per.CanNew;
            //btnEdit.Enabled = per.CanEdit;
            //btnEdit2.Enabled = per.CanEdit;
            //btnDelete.Enabled = per.CanDelete;
            //btnDelete2.Enabled = per.CanDelete;
            //btnView.Enabled = per.CanView;
            //btnView2.Enabled = per.CanView;
            //GridViewPreRegister.Visible = per.CanView;

            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            LoginManager.FindByCode(Utility.GetCurrentUser_UserId());

            string MeId =LoginManager[0]["MeId"].ToString();
            ObjdsPreRegister.SelectParameters[0].DefaultValue = MeId;

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (IsPageRefresh)
            return;    

        if (GridViewPreRegister.FocusedRowIndex > -1)
        {
            DataRow PreRegisterRow = GridViewPreRegister.GetDataRow(GridViewPreRegister.FocusedRowIndex);

            DeletePreRegister(int.Parse(PreRegisterRow["PRegisterId"].ToString()));
            GridViewPreRegister.DataBind();
        }

    }

    protected void GridViewPreRegister_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "RegisteringDate" )
            e.Cell.Style["direction"] = "ltr";

    }

    protected void GridViewPreRegister_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "RegisteringDate" )
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int PRegisterId = -1;
        int FocucedIndex = GridViewPreRegister.FocusedRowIndex;

        if (FocucedIndex > -1)
        {
            //string TeacherId = Utility.DecryptQS(HiddenFieldTeacher["TeacherId"].ToString());

            DataRow row = GridViewPreRegister.GetDataRow(FocucedIndex);
            PRegisterId = (int)(row["PRegisterId"]);

        }
        if (PRegisterId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                PRegisterId = -1;
                Response.Redirect("AddPreRegister.aspx?PRegisterId=" + Utility.EncryptQS(PRegisterId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("AddPreRegister.aspx?PRegisterId=" + Utility.EncryptQS(PRegisterId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));
            }
        }
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else if (se.Number == 2627)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }  

    private void SetDeleteError(Exception err)
    {

        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
        }
    }

    private void DeletePreRegister(int PRegisterId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PreRegisterManager PreRegisterManager = new TSP.DataManager.PreRegisterManager();
        TSP.DataManager.CourseHoursManager CourseHoursManager = new TSP.DataManager.CourseHoursManager();
        TransactionManager.Add(PreRegisterManager);
        TransactionManager.Add(CourseHoursManager);
        try
        {          
            TransactionManager.BeginSave();

            CourseHoursManager.FindByPRegisterId(PRegisterId);

            if (CourseHoursManager.Count > 0)
            {
                int RowCn =CourseHoursManager.Count;
                for (int i = 0; i <RowCn ; i++)
                {
                    CourseHoursManager[0].Delete();
                }

                if (CourseHoursManager.Save() > 0)
                {
                    CourseHoursManager.DataTable.AcceptChanges();
                    PreRegisterManager.FindByCode(PRegisterId);
                    if (PreRegisterManager.Count > 0)
                    {
                        PreRegisterManager[0].Delete();
                        
                        if (PreRegisterManager.Save() > 0)
                        {
                            TransactionManager.EndSave();
                            GridViewPreRegister.DataBind();
                            DivReport.Visible = true;
                            LabelWarning.Text = "ذخیره انجام شد.";
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            DivReport.Visible = true;
                            LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        DivReport.Visible = true;
                        LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    DivReport.Visible = true;
                    LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                }
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetDeleteError(err);
        }
    }
    #endregion
   
}
