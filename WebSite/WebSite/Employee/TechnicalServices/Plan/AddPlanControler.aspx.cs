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

public partial class Employee_TechnicalServices_Plan_AddPlanControler : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        txtMfNo.Attributes["onkeyup"] = "return ltr_override(event)";

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ControlerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanView;
            btnSave2.Enabled = per.CanView;

            if (Request.QueryString["CntId"] == null || Request.QueryString["PgMd"] == null || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PgMd"].ToString())) != "New"))
            {
                Response.Redirect("PlanControler.aspx");
            }

            SetKeys();

            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["btnSave"] = btnSave.Enabled;
        }

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["btnSave"];
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PlanControler.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldControler["PageMode"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        switch (PageMode)
        {
            case "New":
                Insert();
                break;
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        HiddenFieldControler["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldControler["ControlerId"] = "-1";
        SetNewModeKeys();
    }

    protected void txtMeNo_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(int.Parse(txtMeNo.Text.Trim()));
        if (MemberManager.Count == 1)
        {
          //  Capacity Cpty = new Capacity();
            //string DesignGrade = GetGradeName(Convert.ToInt32(txtMeNo.Text), TSP.DataManager.DocumentResponsibilityType.Design);
            //string ObservationGrade = GetGradeName(Convert.ToInt32(txtMeNo.Text), TSP.DataManager.DocumentResponsibilityType.Observation);
            //string ImplementationGrade = GetGradeName(Convert.ToInt32(txtMeNo.Text), TSP.DataManager.DocumentResponsibilityType.Implement);

          
            txtFamily.Text = MemberManager[0]["LastName"].ToString();
            txtFather.Text = MemberManager[0]["FatherName"].ToString();
            txtMfNo.Text = MemberManager[0]["FileNo"].ToString();
            txtName.Text = MemberManager[0]["FirstName"].ToString();
            txtDesign.Text = MemberManager[0]["DesGrdName"].ToString();
            txtImplement.Text = MemberManager[0]["ImpGrdName"].ToString();
            txtObservation.Text = MemberManager[0]["ObsGrdName"].ToString();
            txtUrbenism.Text = MemberManager[0]["UrbanismGrdName"].ToString();
            txtMapping.Text = MemberManager[0]["MappingGrdName"].ToString();
            txtTraffice.Text = MemberManager[0]["TrafficGrdName"].ToString();
        }
        else
        {
            SetLabelWarning("کد عضویت وارد شده معتبر نمی باشد.");
        }
    }
    #endregion

    #region Methods
    /*************************************************************************************************************/
    private void SetKeys()
    {
        try
        {
            HiddenFieldControler["ControlerId"] = Request.QueryString["CntId"].ToString();
            HiddenFieldControler["PageMode"] = Request.QueryString["PgMd"];
            
            string ControlerId = Utility.DecryptQS(HiddenFieldControler["ControlerId"].ToString());
            string PageMode = Utility.DecryptQS(HiddenFieldControler["PageMode"].ToString());

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(ControlerId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            SetMode(PageMode);
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    private void SetMode(string PageMode)
    {
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
                SetNewModeKeys();
                break;

            case "Edit":
                SetEditModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        BtnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;

        CheckAccess();

        if (HiddenFieldControler["ControlerId"] == null || (string.IsNullOrEmpty(HiddenFieldControler["ControlerId"].ToString())))
        {
            Response.Redirect("PlanControler.aspx");
            return;
        }

        int ControlerId = int.Parse(Utility.DecryptQS(HiddenFieldControler["ControlerId"].ToString()));
        FillForm(ControlerId);
        DisableControls();

        RoundPanelControler.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        BtnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        CheckAccess();

        ClearForm();
        EnableControls();
        RoundPanelControler.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        BtnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        CheckAccess();

        if (HiddenFieldControler["ControlerId"] == null || string.IsNullOrEmpty(HiddenFieldControler["ControlerId"].ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        int ControlerId = int.Parse(Utility.DecryptQS(HiddenFieldControler["ControlerId"].ToString()));

        EnableControls();
        FillForm(ControlerId);
        RoundPanelControler.HeaderText = "ویرایش";
    }

    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ControlerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (BtnNew.Enabled == true)
        {
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
        }

        if (Utility.DecryptQS(HiddenFieldControler["PageMode"].ToString()) == "New" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;
        }
        if (Utility.DecryptQS(HiddenFieldControler["PageMode"].ToString()) == "Edit" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
        }

        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["btnSave"] = btnSave.Enabled;
    }

    /*************************************************************************************************************/
    private void FillForm(int ControlerId)
    {
        TSP.DataManager.TechnicalServices.ControlerManager ControlerManager = new TSP.DataManager.TechnicalServices.ControlerManager();
        ControlerManager.FindByControlerId(ControlerId);
        if (ControlerManager.Count == 1)
        {
            txtMeNo.Text = ControlerManager[0]["MeId"].ToString();
            txtMeNo_TextChanged(this, new EventArgs());
        }
    }

    private void ClearForm()
    {
        txtDesign.Text = "";
        txtFamily.Text = "";
        txtFather.Text = "";
        txtImplement.Text = "";
        txtMeNo.Text = "";
        txtMfNo.Text = "";
        txtName.Text = "";
        txtObservation.Text = "";
    }

    private void EnableControls()
    {
        txtMeNo.Enabled = true;
    }

    private void DisableControls()
    {
        txtMeNo.Enabled = false;
    }

    //private string GetGradeName(int GradeId)
    //{
    //    TSP.DataManager.GradeManager GradeManager = new TSP.DataManager.GradeManager();
    //    GradeManager.FindByCode(GradeId);
    //    if (GradeManager.Count > 0)
    //        return GradeManager[0]["GrdName"].ToString();
    //    else
    //        return "---";
    //}

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    /*************************************************************************************************************/
    private void Insert()
    {
        if (!CheckDesignGrade(Convert.ToInt32(txtMeNo.Text)))
        {
            SetLabelWarning("عضو مورد نظر صلاحیت طراحی ندارد.");
            return;
        }

        if (CheckIfExist(Convert.ToInt32(txtMeNo.Text)))
        {
            SetLabelWarning("عضو مورد نظر قبلا به عنوان کنترل کننده نقشه معرفی شده است.");
            return;
        }

        TSP.DataManager.TechnicalServices.ControlerManager ControlerManager = new TSP.DataManager.TechnicalServices.ControlerManager();
        try
        {
            DataRow CntRow = ControlerManager.NewRow();
            CntRow["MeId"] = txtMeNo.Text.Trim();
            CntRow["Date"] = Utility.GetDateOfToday();
            CntRow["Type"] = (int)TSP.DataManager.TSControlerType.Nezam;
            CntRow["InActive"] = 0;
            CntRow["UserId"] = Utility.GetCurrentUser_UserId();
            CntRow["ModifiedDate"] = DateTime.Now;

            ControlerManager.AddRow(CntRow);
            if (ControlerManager.Save() > 0)
            {
                SetLabelWarning("ذخیره انجام شد.");

                HiddenFieldControler["PageMode"] = Utility.EncryptQS("View");
                SetViewModeKeys();
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }

        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    private bool CheckDesignGrade(int MeId)
    {
        Capacity Cpty = new Capacity();
        int Grade = Cpty.GetMemGrade(MeId, (int)TSP.DataManager.TSProjectIngridientType.Designer);
        if (Grade == 0)
            return false;
        else
            return true;
    }

    private bool CheckIfExist(int MeId)
    {
        TSP.DataManager.TechnicalServices.ControlerManager ControlerManager = new TSP.DataManager.TechnicalServices.ControlerManager();
        ControlerManager.FindByMeId(MeId);

        for (int i = 0; i < ControlerManager.Count; i++)
            if (Utility.IsDBNullOrNullValue(ControlerManager[i]["MunId"]))
                return true;

        return false;
    }

    /*************************************************************************************************************/

    private string GetGradeName(int MeId, TSP.DataManager.DocumentResponsibilityType DocumentResponsibilityType)
    {
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

        ArrayList GradeArr = DocMemberFileDetailManager.FindActiveResByResponsibility(MeId, (int)DocumentResponsibilityType);
        if (GradeArr.Count != 0)
            return GradeArr[1].ToString();
        else
            return "---";
    }
    #endregion
}
