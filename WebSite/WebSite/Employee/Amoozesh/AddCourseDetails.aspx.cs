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

public partial class Employee_Amoozesh_AddCourseDetails : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["CrsId"]))
            {
                Response.Redirect("Course.aspx");
                return;
            }

            //TSP.DataManager.Permission per = TSP.DataManager.TrainingAcceptedGradeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            //BtnNew.Enabled = per.CanNew;
            //BtnNew2.Enabled = per.CanNew;
            //btnSave.Enabled = per.CanNew;
            //btnSave2.Enabled = per.CanNew;
            //btnInActive.Enabled = per.CanDelete;
            //btnInActive2.Enabled = per.CanDelete;

            try
            {
                CourseId.Value = Server.HtmlDecode(Request.QueryString["CrsId"]).ToString();
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string CrsId = Utility.DecryptQS(CourseId.Value);

            ObjectDataSourceGrid.SelectParameters[0].DefaultValue = CrsId;

            TSP.DataManager.CourseManager detail = new TSP.DataManager.CourseManager();
            detail.FindByCode(int.Parse(CrsId));
            if (detail.Count > 0)
            {
               // txtCourse.Text = detail[0]["CrsTitle"].ToString();
                RoundPanelCourseDetail.HeaderText = detail[0]["CrsTitle"].ToString();
            }

            // chbInActive.Checked = false;
           
           
            //this.ViewState["BtnSave"] = btnSave.Enabled;
            //this.ViewState["BtnInActive"] = btnInActive.Enabled;
            //this.ViewState["BtnNew"] = BtnNew.Enabled;


        }
        //if (this.ViewState["BtnSave"] != null)
        //    this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        //if (this.ViewState["BtnInActive"] != null)
        //    this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        //if (this.ViewState["BtnNew"] != null)
        //    this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];


        btnBack.PostBackUrl = "AddCourses.aspx?" + "&CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value;
        ASPxButton6.PostBackUrl = "AddCourses.aspx?" + "&CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value;

    }

    //protected void BtnNew_Click(object sender, EventArgs e)
    //{
    //    DetailId.Value = Utility.EncryptQS("");
    //    //PgMode.Value = Utility.EncryptQS("New");
    //    ASPxRoundPanel2.HeaderText = "جدید";
    //    ClearForm();
    //    Enable();

    //    TSP.DataManager.Permission per = TSP.DataManager.CourseDetailManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

    //    //btnSave.Enabled = per.CanNew;
    //    //btnSave2.Enabled = per.CanNew;
    //    //this.ViewState["BtnSave"] = btnSave.Enabled;
       
    //    //btnInActive.Enabled = false;
    //    //btnInActive2.Enabled = false;
    //    //this.ViewState["BtnInActive"] = btnInActive.Enabled;

    //}

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    if (string.IsNullOrEmpty(txtMajorId.Text))
    //    {
    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = "پایه مورد نظر را انتخاب نمایید";
    //        return;
    //    }
    //    else
    //        Insert();

    //    //string PageMode = Utility.DecryptQS(PgMode.Value);

    //    //string CdId = Utility.DecryptQS(DetailId.Value);

    //    //if (string.IsNullOrEmpty(PageMode))
    //    //{
    //    //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

    //    //    return;
    //    //}
    //    //else
    //    //{
    //    //    if (PageMode == "New")
    //    //    {
    //           //Insert();

    //    //    }
    //    //    else if (PageMode == "Edit")
    //    //    {

    //    //        if (string.IsNullOrEmpty(CdId))
    //    //        {
    //    //            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

    //    //            return;
    //    //        }
    //    //        else
    //    //        {
    //    //            Edit(int.Parse(CdId));
    //    //        }

    //    //    }

    //    //}
    //}
  
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddCourses.aspx?" + "&CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);

    }
     
    //protected void btnInActive_Click(object sender, EventArgs e)
    //{
    //    int TrGrId = -1;
    //    if (CustomAspxDevGridViewGrade.FocusedRowIndex > -1)
    //    {
    //        DataRow row = CustomAspxDevGridViewGrade.GetDataRow(CustomAspxDevGridViewGrade.FocusedRowIndex);
    //        TrGrId = (int)row["TrGrId"];
    //    }
    //    if (TrGrId == -1)
    //    {
    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

    //    }
    //    else
    //        InActive(TrGrId);
    //}

    protected void MenuCourseDetails_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Prerequisite":
                Response.Redirect("~/Employee/Amoozesh/CoursePrerequisite.aspx?CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "CourseRefrence":
                Response.Redirect("~/Employee/Amoozesh/CourseRefrences.aspx?CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Group":
                Response.Redirect("CourseGroups.aspx?CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Course":
                Response.Redirect("AddCourses.aspx?CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Attachment":
                Response.Redirect("CourseAttachments.aspx?CrsId=" + CourseId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
        }
    }
    #endregion

    #region Methods
    //protected void ClearForm()
    //{
    //    txtMjDesc.Text = "";
    //    txtMajorId.Text = "";
    //    txtGrdName.Text = "";
    //    txtMjDesc.Text = "";
    //    txtMjName.Text = "";
    //    txtResName.Text = "";
    //}

    //protected void Disable()
    //{
    //    txtMjDesc.Enabled = false;
    //    btnGrade.Enabled = false;

    //}

    //protected void Enable()
    //{
    //    txtMjDesc.Enabled = true;
    //    btnGrade.Enabled = true;
    //}
    
    //protected void Insert()
    //{
    //    TSP.DataManager.TrainingAcceptedGradeManager GradeManager = new TSP.DataManager.TrainingAcceptedGradeManager();
    //    try
    //    {
    //        DataRow drGrade = GradeManager.NewRow();

    //        if (CustomAspxDevGridViewGrade.VisibleRowCount > 0)
    //        {

    //            for (int i = 0; i < CustomAspxDevGridViewGrade.VisibleRowCount; i++)
    //            {
    //                DataRow row = CustomAspxDevGridViewGrade.GetDataRow(i);
    //                if (txtMajorId.Text == row["GMRId"].ToString())
    //                {
    //                    this.DivReport.Visible = true;
    //                    this.LabelWarning.Text = "پایه مورد نظر قبلاً ثبت شده است";
    //                    return;
    //                }
    //            }
    //        }

    //        drGrade["GMRId"] = txtMajorId.Text;
    //        drGrade["PkId"] = Utility.DecryptQS(CourseId.Value);
    //        drGrade["Type"] = 0;//course
    //        drGrade["Description"] = txtMjDesc.Text;
    //        drGrade["CreateDate"] = Utility.GetDateOfToday();
    //        drGrade["UserId"] = Utility.GetCurrentUser_UserId();
    //        drGrade["InActive"] = 0;
    //        drGrade["ModifiedDate"] = DateTime.Now;

    //        GradeManager.AddRow(drGrade);

    //        int cn = GradeManager.Save();
    //        if (cn == 1)
    //        {
    //            ClearForm();
    //            CustomAspxDevGridViewGrade.DataBind();
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "ذخیره انجام شد";

    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //        }
    //    }
                 
        
    //    catch (Exception err)
    //    {
    //        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
    //        {
    //            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
    //            if (se.Number == 2601)
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
    //            }
             
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //            }
    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //        }
    //    }


    //}

    //protected void InActive(int TrGrId)
    //{
    //    TSP.DataManager.TrainingAcceptedGradeManager managerEdit = new TSP.DataManager.TrainingAcceptedGradeManager();

    //    managerEdit.FindByCode(TrGrId);
    //    if (managerEdit.Count == 1)
    //    {
    //        try
    //        {
    //            if (Convert.ToBoolean(managerEdit[0]["InActive"]) == true)
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
    //                return;
    //            }

    //            managerEdit[0].BeginEdit();
    //            managerEdit[0]["InActive"] = 1;
    //            managerEdit[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //            managerEdit[0]["ModifiedDate"] = DateTime.Now;
    //            managerEdit[0].EndEdit();


    //            int cn = managerEdit.Save();
    //            if (cn == 1)
    //            {
    //                ////DetailId.Value = managerEdit[0]["CdId"].ToString();
    //                //DetailId.Value = Utility.EncryptQS("");
    //                //PgMode.Value = Utility.EncryptQS("New");
    //                //ASPxRoundPanel2.HeaderText = "جدید";
    //                //ClearForm();
    //                CustomAspxDevGridViewGrade.DataBind();
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "عملیات با موفقیت انجام شد";

    //            }
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //            }
    //        }
    //        catch (Exception err)
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

    //        }

    //    }
    //    else
    //    {
    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.مجدداً اقدام نمایید";
    //    }

    //}
    #endregion
}
