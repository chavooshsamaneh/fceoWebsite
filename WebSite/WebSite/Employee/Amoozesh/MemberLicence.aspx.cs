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
public partial class Employee_Amoozesh_MemberLicence : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.display='none'; </script>");
        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {


            ClearMessageCP();
            Session["SendBackDataTable_PeriodReg"] = "";
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.PeriodRegisterLicenceOutOfTime).ToString();
            OdbMadrak.SelectParameters["MeId"].DefaultValue = "-1";
            #region Permission
            TSP.DataManager.Permission per = TSP.DataManager.MadrakManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnNewPeriodReg.Enabled = btnNewPeriodReg2.Enabled = CheckWorkFlowPermissionForNewReq();
            GridViewPeriodRegister.Visible = per.CanView;
            #endregion
            cmbCourse.DataBind();
            cmbCourse.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            ComboType.DataBind();
            ComboType.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));

            cmbObjectionType.DataBind();
            cmbObjectionType.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnNewPeriodReg"] = btnNewPeriodReg.Enabled;
        }
        
        ArrayList DeletedColumnsName = new ArrayList();
        DeletedColumnsName.Add("WFState");

        Session["DeletedColumnsName"] = DeletedColumnsName;
        Session["DataTable"] = GridViewPeriodRegister.Columns;
        Session["DataSource"] = OdbMadrak;
        Session["Title"] = "مدارک و دوره های مورد قبول نظام مهندسی";
        Session["Header"] = "";

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnNewPeriodReg"] != null)
            this.btnNewPeriodReg.Enabled = this.btnNewPeriodReg2.Enabled = (bool)this.ViewState["BtnNewPeriodReg"];

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddMemberLicence.aspx?MdId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));
    }

    private void btnEdit_Click()
    {
        int ID = -1;
        int Type = -1;
        int MdId = -1;
        int PRId = -1;
        int PPId = -1;
        if (GridViewPeriodRegister.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
             ID = MdId = PRId =  (int)row["ID"];
            Type = (int)row["Type"];
            PPId = (int)row["PPId"];
        }
        if (ID == -1)
        {
            ShowErrorMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");

        }
        else
        {
            if ((Type == 0) || (Type == 1))
            {
                if (IsCallback)
                    ASPxWebControl.RedirectOnCallback("AddMemberLicence.aspx?MdId=" + Utility.EncryptQS(ID.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));
                else
                    Response.Redirect("AddMemberLicence.aspx?MdId=" + Utility.EncryptQS(ID.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));
            }
            //else if (Type == 2)//ثبت نام دوره     
            //{
            //    if (!CheckIfCanEdit(PRId))
            //        return;
            //    ASPxWebControl.RedirectOnCallback("AddPeriodRegister.aspx?PgMd=" + Utility.EncryptQS("Edit") + "&PRId=" + Utility.EncryptQS(PRId.ToString()) + "&PrPg=" + Utility.EncryptQS("MeLicence") + "&PPId=" + Utility.EncryptQS(PPId.ToString()));
            //}
            else
            {
                ShowErrorMessage("امکان ویرایش اطلاعات برای رکورد مورد نظر وجود ندارد.تنها مدرک دوره و مدرک سمینار از طریق این صفحه قابل ویرایش می باشد.");

            }
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int ID = -1;
        int Type = -1;
        int PPId = -1;
        int MeId = -1;
        int MdId = -1;
        int PRId = -1;
        if (GridViewPeriodRegister.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
            ID = MdId = PRId = (int)row["ID"];
            Type = (int)row["Type"];
            PPId = (int)row["PPId"];
            MeId = Convert.ToInt32(row["MeId"]);
        }
        if (ID == -1 || PPId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if ((Type == 0) || (Type == 1))//مدرک دوره و یا سمینار
                Response.Redirect("AddMemberLicence.aspx?MdId=" + Utility.EncryptQS(ID.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));
            else if (Type == 2 || Type == 4)//ثبت نام دوره----گواهی دوره خارج از نوبت
                //Response.Redirect("PeriodsView.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&PageMode=" + Utility.EncryptQS("PRView") + "&PRId=" + Utility.EncryptQS(ID.ToString()) + "&MeId=" + Utility.EncryptQS(MeId.ToString()));
                Response.Redirect("AddPeriodRegister.aspx?PgMd=" + Utility.EncryptQS("View") + "&PRId=" + Utility.EncryptQS(PRId.ToString()) + "&PrPg=" + Utility.EncryptQS("MeLicence") + "&PPId=" + Utility.EncryptQS(PPId.ToString()));
            else if (Type == 3)//ثبت نام سمینار
                Response.Redirect("SeminarView.aspx?SeId=" + Utility.EncryptQS(PPId.ToString()) + "&PageMode=" + Utility.EncryptQS("PRView") + "&PRId=" + Utility.EncryptQS(ID.ToString()));

        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "MembersLicences";

        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btnNewPeriodReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddPeriodRegister.aspx?PgMd=" + Utility.EncryptQS("New") + "&PRId=" + Utility.EncryptQS("-1") + "&PrPg=" + Utility.EncryptQS("MeLicence"));
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewPeriodRegister.FocusedRowIndex > -1)
        {
            int TableType = (int)TSP.DataManager.TableCodes.PeriodRegister;
            DataRow PeriodRegRow = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
            int TableId = int.Parse(PeriodRegRow["ID"].ToString());

            string GridFilterString = GridViewPeriodRegister.FilterExpression;

            String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                "&PostId=" + Utility.EncryptQS(TableId.ToString());

            Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString())
                + "&TblId=" + Utility.EncryptQS(TableId.ToString())
                   + "&UrlReferrer=" + Utility.EncryptQS(Url));
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ردیفی انتخاب نشده است.";
        }
    }

    protected void btnSearch_OnClick(object sender, EventArgs e) { 
        Search(); 
    }
    

    protected void GridViewPeriodRegister_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate" || e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "TestDate")
            e.Cell.Style["direction"] = "ltr";
        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewPeriodRegister.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewPeriodRegister.Columns["WFState"], "btnWFState");
            if (btnWFState != null)
            {
                if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
                {
                    btnWFState.ToolTip = "تعریف نشده";
                    btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
                    return;
                }

                if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFStart.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFInProcess.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
                }
            }
        }
    }

    protected void GridViewPeriodRegister_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate" || e.Column.FieldName == "CreateDate" || e.Column.FieldName == "TestDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewPeriodRegister.FocusedRowIndex > -1)
        {
            DataRow PeriodRegRow = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
            int TableId = int.Parse(PeriodRegRow["ID"].ToString());
            int TableType = (int)TSP.DataManager.TableCodes.PeriodRegister;
            int WfCode = (int)TSP.DataManager.WorkFlows.PeriodRegisterLicenceOutOfTime;
            WFUserControl.PerformCallback(TableId, TableType, WfCode, e);
            GridViewPeriodRegister.DataBind();
        }
        else
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
    }

    protected void GridViewPeriodRegister_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Parameters))
        {
            GridViewPeriodRegister.JSProperties["cpReqType"] = e.Parameters;
            switch (e.Parameters)
            {
                case "Tracing":

                    if (GridViewPeriodRegister.FocusedRowIndex > -1)
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.PeriodRegister;
                        DataRow PeriodRegRow = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
                        int TableId = int.Parse(PeriodRegRow["ID"].ToString());
                        if (Convert.ToInt32(PeriodRegRow["Type"]) != 4)
                        {
                            ShowErrorMessage("تنها گواهینامه های خارج از نوبت قابل پیگیری می باشند");
                            return;
                        }
                        string GridFilterString = GridViewPeriodRegister.FilterExpression;

                        String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                            "&PostId=" + Utility.EncryptQS(TableId.ToString());
                        if (IsCallback)
                            ASPxWebControl.RedirectOnCallback("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString())
                                + "&TblId=" + Utility.EncryptQS(TableId.ToString())
                                   + "&UrlReferrer=" + Utility.EncryptQS(Url));
                        else
                            Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString())
                                + "&TblId=" + Utility.EncryptQS(TableId.ToString())
                                   + "&UrlReferrer=" + Utility.EncryptQS(Url));
                    }
                    else
                    {
                        ShowErrorMessage("ردیفی انتخاب نشده است.");
                    }
                    break;
                case "WF":
                    if (GridViewPeriodRegister.FocusedRowIndex > -1)
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.PeriodRegister;
                        DataRow PeriodRegRow = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
                        if (Convert.ToInt32(PeriodRegRow["Type"]) != 4)
                        {
                            ShowErrorMessage("تنها گواهینامه های خارج از نوبت دارای گردش کار می باشند");
                            return;
                        }
                    }
                    else
                    {
                        ShowErrorMessage("ردیفی انتخاب نشده است");
                    }
                    break;
                case "btnEdit":
                    btnEdit_Click();
                    break;
                case "btnDelete":
                    if (GridViewPeriodRegister.FocusedRowIndex > -1)
                    {
                        DataRow PeriodRegRow = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
                        int ID = int.Parse(PeriodRegRow["ID"].ToString());
                        int TypeLicence = int.Parse(PeriodRegRow["TypeLicence"].ToString());
                        DeleteRequest(ID, TypeLicence);
                    }
                    break;
            }
        }
        else
            GridViewPeriodRegister.DataBind();
    }
    #endregion

    #region Methods
    private void DeleteRequest(int ID, int TypeLicence)
    {
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MadrakManager MadrakManager = new TSP.DataManager.MadrakManager();
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        transact.Add(WorkFlowStateManager);
        transact.Add(MadrakManager);
        transact.Add(PeriodRegisterManager);

        try
        {
            transact.BeginSave();
            if (TypeLicence == 0)
            {
                MadrakManager.FindByCode(ID);
                if (MadrakManager.Count == 1)
                {
                    MadrakManager[0].Delete();
                    MadrakManager.Save();
                }
            }
            else
            {
                PeriodRegisterManager.FindByCode(ID);
                if (PeriodRegisterManager.Count == 1)
                {
                    PeriodRegisterManager[0].Delete();
                    PeriodRegisterManager.Save();
                }
            }

            DataTable dtWFState = WorkFlowStateManager.SelectByTableType(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister), ID);
            if (dtWFState.Rows.Count > 0)
            {
                for (int i = 0; i < dtWFState.Rows.Count; i++)
                {
                    WorkFlowStateManager.FindByCode(Convert.ToInt32(dtWFState.Rows[i]["StateId"]));
                    WorkFlowStateManager[0].Delete();
                    WorkFlowStateManager.Save();
                }
            }

            transact.EndSave();
            ShowErrorMessage("حذف درخواست با موفقیت انجام شد");
            GridViewPeriodRegister.DataBind();
        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            ShowErrorMessage("خطایی در حذف درخواست انجام گرفته است");
        }
    }

    private Boolean CheckWorkFlowPermissionForNewReq()
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodRegLicenceReqInfo;
        int TableType = (int)TSP.DataManager.TableCodes.PeriodRegister;
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId());
        return (WFPer.BtnNewRequest);
    }

    private void ClearMessageCP()
    {
        GridViewPeriodRegister.JSProperties["cpError"] = 0;
        GridViewPeriodRegister.JSProperties["cpMsg"] = "";
    }

    private void ShowErrorMessage(string Message)
    {
        GridViewPeriodRegister.JSProperties["cpError"] = 1;
        GridViewPeriodRegister.JSProperties["cpMsg"] = Message;
    }

    private void Search()
    {
        string CrsName = null;
        if (cmbCourse.SelectedIndex == 0)
            CrsName = "-1";
        else
            CrsName = cmbCourse.Value.ToString();

        string Type = null;
        if (ComboType.SelectedIndex == 0 )
            Type = "-1";
        else
            Type = ComboType.Value.ToString();

        string ObjectionType = null;
        if (cmbObjectionType.SelectedIndex == 0)
            ObjectionType = "-1";
        else
            ObjectionType = cmbObjectionType.Value.ToString();

            string MeId = "-1";
            if (!string.IsNullOrEmpty(txtMeId.Text))
                MeId = txtMeId.Text;

        if (!string.IsNullOrEmpty(txtMeId.Text))
                OdbMadrak.SelectParameters["MeId"].DefaultValue = txtMeId.Text;
        else
                OdbMadrak.SelectParameters["MeId"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtFirstName.Text))
            OdbMadrak.SelectParameters["FirstName"].DefaultValue = txtFirstName.Text;
        else
            OdbMadrak.SelectParameters["FirstName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtLastName.Text))
            OdbMadrak.SelectParameters["LastName"].DefaultValue = txtLastName.Text;
        else
            OdbMadrak.SelectParameters["LastName"].DefaultValue = "%"; 

        OdbMadrak.SelectParameters["CrsId"].DefaultValue = CrsName;
        OdbMadrak.SelectParameters["Type"].DefaultValue = Type;
        OdbMadrak.SelectParameters["PeriodHasObjection"].DefaultValue = ObjectionType;
        GridViewPeriodRegister.DataBind();
    }

    private Boolean CheckIfCanEdit(int PRId)
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        PeriodRegisterManager.FindByCode(PRId);
        if (PeriodRegisterManager.Count != 1)
        {
            ShowErrorMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }
        if (Convert.ToInt32(PeriodRegisterManager[0]["PaymentType"]) == (int)TSP.DataManager.PeriodRegisterPaymentType.EPayment)
        {
            ShowErrorMessage("امکان ویرایش ثبت نام هایی که از طریق پرداخت الکترونیکی انجام شده است وجود ندارد.");
            return false;
        }
        return true;
    }
    #endregion
}
