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

public partial class Employee_TechnicalServices_Project_ImplementerAgent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Project.aspx");
            }

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ImplementerAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnInActive.Enabled = per.CanEdit;
            btnInActive2.Enabled = per.CanEdit;

            if (!per.CanView)
                CustomAspxDevGridView1.Visible = false;

            if ((string.IsNullOrEmpty(Request.QueryString["PrjImpId"])) || (string.IsNullOrEmpty(Request.QueryString["Mode"])))
            {
                Response.Redirect("Implementer.aspx?ProjectId=" + Server.HtmlDecode(Request.QueryString["ProjectId"]) + "&PageMode=" + Request.QueryString["PageMode"] + "&PrjReId=" + Server.HtmlDecode(Request.QueryString["PrjReId"]));
                return;
            }

            SetKeys();
            CheckWorkFlowPermission();

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
        }

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        TSP.DataManager.TechnicalServices.ImplementerAgentManager ImpAgentManager = new TSP.DataManager.TechnicalServices.ImplementerAgentManager();
        ImpAgentManager.FindByPrjImpId(int.Parse(Utility.DecryptQS(HDImpId.Value)));
        if (ImpAgentManager.Count > 0)
        {
            if (!Convert.ToBoolean(ImpAgentManager[0]["InActive"]))
            {
                SetLabelWarning("امکان ثبت نماینده جدید وجود ندارد.مجری مورد نظر نماینده فعال دارد");
                return;
            }
        }

        Response.Redirect("ImplementerAgentInsert.aspx?ProjectId=" + HDProjectId.Value + "&ImpAgentId=" + Utility.EncryptQS("") + "&PageMode=" + PgMode.Value 
            + "&PageMode2=" + Utility.EncryptQS("New") + "&PrjReId="
            + RequestId.Value + "&Mode=" + HDMode.Value + "&PrjImpId=" + HDImpId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int ImplementerAgentId = -1;
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            ImplementerAgentId = (int)row["ImplementerAgentId"];

        }
        if (ImplementerAgentId == -1)
        {
            SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            Response.Redirect("ImplementerAgentInsert.aspx?ProjectId=" + HDProjectId.Value + "&ImpAgentId=" + Utility.EncryptQS(ImplementerAgentId.ToString())
                + "&PageMode=" + PgMode.Value + "&PageMode2=" + Utility.EncryptQS("View") + "&PrjReId="
                + RequestId.Value + "&Mode=" + HDMode.Value + "&PrjImpId=" + HDImpId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);

        }

    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        try
        {
            int ImplementerAgentId = -1;
            int PrjReId = -1;

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                ImplementerAgentId = (int)row["ImplementerAgentId"];
                PrjReId = (int)row["PrjReId"];


            }
            if (ImplementerAgentId == -1)
            {
                SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");

            }
            else
            {
                TSP.DataManager.TechnicalServices.ImplementerAgentManager ImpAgentManager = new TSP.DataManager.TechnicalServices.ImplementerAgentManager();

                ImpAgentManager.FindByImplementerAgentId(ImplementerAgentId);
                if (ImpAgentManager.Count == 1)
                {
                    int CurrentPrjReId = int.Parse(Utility.DecryptQS(RequestId.Value));

                    if (PrjReId == CurrentPrjReId)
                    {
                        ImpAgentManager[0].Delete();
                    }
                    else
                    {
                        if (Convert.ToBoolean(ImpAgentManager[0]["InActive"]))
                        {
                            SetLabelWarning("رکورد مورد نظر غیر فعال می باشد");
                            return;
                        }
                        else
                        {
                            ImpAgentManager[0].BeginEdit();
                            ImpAgentManager[0]["InActive"] = 1;
                            ImpAgentManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            ImpAgentManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                            ImpAgentManager[0].EndEdit();
                        }

                    }
                    int cn = ImpAgentManager.Save();
                    if (cn == 1)
                    {
                        CustomAspxDevGridView1.DataBind();
                        SetLabelWarning("ذخیره انجام شد");

                    }
                    else
                    {
                        SetLabelWarning("خطایی در ذخیره انجام گرفته است");
                    }

                }
                else
                {
                    SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
                }

            }
        }
        catch (Exception Err)
        {
            SetError(Err);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        if (Mode == "Imp")
            Response.Redirect("Implementer.aspx?ProjectId=" + HDProjectId.Value
                + "&PageMode=" + PgMode.Value + "&PrjReId=" + RequestId.Value
                + "&PrjImpId=" + HDImpId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        else
            Response.Redirect("ProjectInsert.aspx?ProjectId=" + HDProjectId.Value
                + "&PageMode=" + PgMode.Value + "&PrjReId=" + RequestId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
    }

    /*****************************************************************************************************************************/
    private void SetKeys()
    {
        try
        {
            HDProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"].ToString());
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();
            RequestId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();
            HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"]).ToString();

            string ProjectId = Utility.DecryptQS(HDProjectId.Value);
            string Mode = Utility.DecryptQS(HDMode.Value);
            string PrjReId = Utility.DecryptQS(RequestId.Value);
            string PageMode = Utility.DecryptQS(PgMode.Value);
            string PrjImpId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PrjImpId"]).ToString());

            if (string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(Mode) || string.IsNullOrEmpty(PrjReId) || string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(PrjImpId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            SetMode();
            FillProjectInfo(int.Parse(PrjReId));
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    private void SetMode()
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        string Mode = Utility.DecryptQS(HDMode.Value);
        string PrjReId = Utility.DecryptQS(RequestId.Value);

        switch (Mode)
        {
            case "Imp":
                if (string.IsNullOrEmpty(Request.QueryString["PrjImpId"]))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                HDImpId.Value = Server.HtmlDecode(Request.QueryString["PrjImpId"]).ToString();
                string PrjImpId = Utility.DecryptQS(HDImpId.Value);
                ObjectDataSource1.SelectParameters[0].DefaultValue = ProjectId;
                ObjectDataSource1.SelectParameters[1].DefaultValue = PrjImpId;
                ObjectDataSource1.SelectParameters[2].DefaultValue = PrjReId;


                break;

            case "Project":
                HDImpId.Value = Utility.EncryptQS("-1");
                ObjectDataSource1.SelectParameters[0].DefaultValue = ProjectId;
                ObjectDataSource1.SelectParameters[2].DefaultValue = PrjReId;

                BtnNew.Enabled = false;
                btnNew2.Enabled = false;
                break;
        }
    }

    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
    }

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
                SetLabelWarning("کد تکراری می باشد");
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

    /*****************************************************************************************************************************/
    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (RequestId.Value != null)
        {
            string PrjReId = Utility.DecryptQS(RequestId.Value);
            if (e.GetValue("PrjReId") == null)
                return;
            string CurretnPrjReId = e.GetValue("PrjReId").ToString();
            if (PrjReId == CurretnPrjReId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "CreateDate":
                e.Cell.Style["direction"] = "ltr";
                break;

            case "InActiveDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "No":
                e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";
                break;

            case "CreateDate":
                e.Editor.Style["direction"] = "ltr";
                break;

            case "InActiveDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    /****************************************************** WorkFlow *************************************************************/
    #region WorkFlow Methods
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.TSProjectRequest;
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementerOfProject;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, Convert.ToInt32(PrjReId), Utility.GetCurrentUser_UserId());
        this.ViewState["BtnInActive"] = btnInActive.Enabled = btnInActive2.Enabled = WFPer.BtnInactive;
        this.ViewState["BtnNew"] = BtnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew;
    }
    #endregion
}
