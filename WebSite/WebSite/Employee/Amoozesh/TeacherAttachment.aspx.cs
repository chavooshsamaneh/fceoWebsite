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

public partial class Employee_Amoozesh_TeacherAttachment : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Request.QueryString["PgMd"] == null || Request.QueryString["TeId"] == null)
            {
                Response.Redirect("Teachers.aspx");
            }

            MenuTeacherInfo.Enabled = true;

            TSP.DataManager.Permission per = TSP.DataManager.AttachmentsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnSave.Enabled = per.CanNew;

            HiddenFieldTeacherAttach["TeacherId"] = Request.QueryString["TeId"].ToString();
            HiddenFieldTeacherAttach["PrePageMode"] = Request.QueryString["PgMd"].ToString();
            try
            {
                string TeId = Utility.DecryptQS(HiddenFieldTeacherAttach["TeacherId"].ToString());

                TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
                GridViewAttachment.DataSource = attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Teachers, int.Parse(TeId));
                GridViewAttachment.DataBind();

                string TeacherId = Utility.DecryptQS(HiddenFieldTeacherAttach["TeacherId"].ToString());
                TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
                TeacherManager.FindByCode(int.Parse(TeacherId));
                if (TeacherManager.Count > 0)
                {
                    RoundPanelAttachment.HeaderText = TeacherManager[0]["Name"].ToString() + " " + TeacherManager[0]["Family"].ToString();
                }
                else
                {
                    Response.Redirect("Teachers.aspx");
                }
            }
            catch
            {
                Response.Redirect("Teachers.aspx");
            }
            CheckWorkFlowPermission();

            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;

        }

        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void MenuTeacherInfo_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Madrak":
                Response.Redirect("TeachersLicence.aspx?TeacherId=" + HiddenFieldTeacherAttach["TeacherId"].ToString() + "&PageMode=" + HiddenFieldTeacherAttach["PrePageMode"].ToString());
                break;
            case "Research":
                Response.Redirect("TeacherResearchAct.aspx?TeacherId=" + HiddenFieldTeacherAttach["TeacherId"].ToString() + "&Pagemode=" + HiddenFieldTeacherAttach["PrePageMode"].ToString());
                break;
            case "Job":
                Response.Redirect("TeacherJobHistory.aspx?TeacherId=" + HiddenFieldTeacherAttach["TeacherId"].ToString() + "&Pagemode=" + HiddenFieldTeacherAttach["PrePageMode"].ToString());
                break;
            case "BasicInfo":
                Response.Redirect("AddTeachers.aspx?TeId=" + HiddenFieldTeacherAttach["TeacherId"] + "&PageMode=" + HiddenFieldTeacherAttach["PrePageMode"]);
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        InsertTeacherAttachment();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteTeacherAttachment();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {        
        Response.Redirect("AddTeachers.aspx?TeId=" + HiddenFieldTeacherAttach["TeacherId"] + "&PageMode=" + HiddenFieldTeacherAttach["PrePageMode"]);

    }

    protected void ASPxHyperLink1_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxHyperLink hp = (DevExpress.Web.ASPxHyperLink)sender;
        hp.Text = Path.GetFileName(hp.Value.ToString());
    }

    #endregion

    #region Methods
    private void InsertTeacherAttachment()
    {
        if (!flp.HasFile)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = " فایل مورد نظر را انتخاب نمایید";
            return;
        }

        string fileNameImg = "", pathAx = "", extension = "";
        // byte[] img = null;
        //  bool AxImg = false;
        try
        {
            TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();

            DataRow dr = attManager.NewRow();
            dr["TtId"] = (int)TSP.DataManager.TableCodes.Teachers;
            dr["RefTable"] = Utility.DecryptQS(HiddenFieldTeacherAttach["TeacherId"].ToString());
            dr["AttId"] = 1;
            dr["IsValid"] = 1;
            dr["Description"] = txtDescription.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModfiedDate"] = DateTime.Now;

            try
            {
                extension = Path.GetExtension(flp.FileName);
                extension = extension.ToLower();
                if (flp.HasFile)
                {
                    fileNameImg = Utility.GenerateName(Path.GetExtension(flp.FileName));
                    pathAx = Server.MapPath("~/image/Temp/");
                    flp.SaveAs(pathAx + fileNameImg);
                    dr["AtContent"] = DBNull.Value;
                    dr["FilePath"] = "~/Image/Employee/Amoozesh/Attachments/" + fileNameImg;
                    // AxImg = true;
                }
            }
            catch
            {
            }

            attManager.AddRow(dr);
            int cnt = attManager.Save();
            if (cnt == 1)
            {
                //if (AxImg == true)
                //{
                try
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + fileNameImg;
                    string ImgTarget = Server.MapPath("~/image/Employee/Amoozesh/Attachments/") + fileNameImg;
                    File.Move(ImgSoource, ImgTarget);
                }
                catch
                {
                }
                //}
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";
                txtDescription.Text = "";
                GridViewAttachment.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Teachers, int.Parse(Utility.DecryptQS(HiddenFieldTeacherAttach["TeacherId"].ToString())));
                GridViewAttachment.DataBind();

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    txtDescription.Text = "";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                txtDescription.Text = "";
            }
        }

    }

    private void DeleteTeacherAttachment()
    {
        int AttachId = -1;
        TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();

        GridViewAttachment.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Teachers, int.Parse(Utility.DecryptQS(HiddenFieldTeacherAttach["TeacherId"].ToString())));
        GridViewAttachment.DataBind();

        if (GridViewAttachment.FocusedRowIndex > -1)
        {
            DataRow row = GridViewAttachment.GetDataRow(GridViewAttachment.FocusedRowIndex);
            AttachId = (int)row["AttachId"];
        }
        if (AttachId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {

            attManager.FindByCode(AttachId);
            if (attManager.Count == 1)
            {
                try
                {
                    attManager[0].Delete();

                    int cn = attManager.Save();
                    if (cn == 1)
                    {
                        GridViewAttachment.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Teachers, int.Parse(Utility.DecryptQS(HiddenFieldTeacherAttach["TeacherId"].ToString())));
                        GridViewAttachment.DataBind();

                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام شد";

                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام نشد";
                    }

                }
                catch (Exception err)
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

            }
        }
    }

    #region WF Methods
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        string TeId = Utility.DecryptQS(HiddenFieldTeacherAttach["TeacherId"].ToString());
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.Teachers;

        int WFCode = (int)TSP.DataManager.WorkFlows.TeachersConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveTeacherInfo;
        int CommitteeGradingCode = (int)TSP.DataManager.WorkFlowTask.CommitteeGradingTeacher;
        int ComissionGradingCode = (int)TSP.DataManager.WorkFlowTask.ComissionGradingTeacher;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, int.Parse(TeId), Utility.GetCurrentUser_UserId());

        this.ViewState["BtnNew"] = BtnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew;
        this.ViewState["BtnDelete"] = btnDelete.Enabled = btnDelete2.Enabled = WFPer.BtnNew;

        btnDelete.Enabled = true;
        btnDelete2.Enabled = true;
    }

    private void InsertWorkFlowStateView(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "View", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
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
    }
    #endregion
    #endregion

}
