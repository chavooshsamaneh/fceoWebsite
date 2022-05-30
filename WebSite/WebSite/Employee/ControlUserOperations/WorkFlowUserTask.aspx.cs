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

public partial class Employee_WorkFlow_WorkFlowUserTask : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.WorkFlowStateManager.GetUserPermissionForWFUserTaskTrace(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnPrint.Enabled = btnPrint2.Enabled = per.CanView;
            btnView.Enabled = btnView2.Enabled = per.CanView;
            btnExportExcel.Enabled = btnExportExcel2.Enabled = per.CanView;
            GridViewWFState.Visible = per.CanView;
          
            HiddenFieldWF["ShowAllTask"] = Utility.EncryptQS("0");
        }
        BidnGrid();
        SetPageFilter();
        SetGridRowIndex();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "DocumentLog";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void GridViewWFState_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType != GridViewRowType.Data)
        //    return;
        //if (e.GetValue("LastStateType") != null)
        //{
        //    if (e.GetValue("LastStateType").ToString() != "2" && e.GetValue("LastStateType").ToString() != "3")
        //        e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        //}
        if (e.GetValue("LastStateId") != null)
        {
            if (e.GetValue("LastStateId").ToString() == e.GetValue("StateId").ToString())
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        }
    }

    protected void GridViewWFState_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewWFState.JSProperties["cpError"] = 0;
        GridViewWFState.JSProperties["cpErrorMsg"] = 0;
        GridViewWFState.JSProperties["cpReType"] = e.Parameters;
        switch (e.Parameters)
        {
            case "View":
                string URL = NextPage("View");
                TSP.DataManager.WFParameters WFParameters = new TSP.DataManager.WFParameters();
                if (!string.IsNullOrEmpty(URL) && URL != "?&"+WFParameters.UserTaskPageQueryStringName+"="+Utility.EncryptQS(WFParameters.UserTaskPageQueryStringValue))
                {
                    string PostId = GridViewWFState.GetDataRow(GridViewWFState.FocusedRowIndex)["StateId"].ToString();
                    URL += "&PostId=" +Utility.EncryptQS(PostId);
                    GridViewWFState.JSProperties["cpURL"] = URL;
                }
                else
                {
                    GridViewWFState.JSProperties["cpError"] = 1;
                    GridViewWFState.JSProperties["cpErrorMsg"] = "آدرس صفحه پرونده انتخاب شده نامشخص می باشد.";
                }
                break;
            case"Print":
                GridViewWFState.JSProperties["cpPrint"] = 1;

                ArrayList DeletedColumnsName = new ArrayList();
                DeletedColumnsName.Add("Priority");

                Session["DeletedColumnsName"] = DeletedColumnsName;
                Session["DataTable"] = GridViewWFState.Columns;
                Session["DataSource"] = GridViewWFState.DataSource;
                Session["Header"] = "";
                Session["Title"] = "کنترل گردش پرونده ها";
                break;
            default:
                GridViewWFState.DataBind();
                break;
        }
    }

    //protected void btnShowAllTask_Click(object sender, EventArgs e)
    //{
    //    if (HiddenFieldWF["ShowAllTask"] != null)
    //    {
    //        string ShowAllTask = Utility.DecryptQS(HiddenFieldWF["ShowAllTask"].ToString());
    //        if (ShowAllTask == "0")
    //        {
    //            btnShowAllTask.ToolTip = "کارهای باقیمانده";
    //            btnShowAllTask.ImageUrl = "~/Images/NotDonWFTask.png";
    //            btnShowAllTask2.ToolTip = "کارهای باقیمانده";
    //            btnShowAllTask2.ImageUrl = "~/Images/NotDonWFTask.png";
    //            ObjdsWfState.SelectParameters["ShowAllTask"].DefaultValue = "1";
    //            HiddenFieldWF["ShowAllTask"] = Utility.EncryptQS("1");
    //        }
    //        else if (ShowAllTask == "1")
    //        {
    //            btnShowAllTask.ToolTip = "کلیه کارها";
    //            btnShowAllTask.ImageUrl = "~/Images/AllWFTask.png";
    //            btnShowAllTask2.ToolTip = "کلیه کارها";
    //            btnShowAllTask2.ImageUrl = "~/Images/AllWFTask.png";
    //            ObjdsWfState.SelectParameters["ShowAllTask"].DefaultValue = "0";
    //            HiddenFieldWF["ShowAllTask"] = Utility.EncryptQS("0");
    //        }
    //        GridViewWFState.DataBind();
    //    }
    //}

    protected void GridViewWFState_FocusedRowChanged(object sender, EventArgs e)
    {
        SetGridRowIndex();
    }

    protected void GridViewWFState_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "PriorityId":
                SetPriorityImage(e);
                break;
        }
    }

    #endregion

    #region Methods

    private void ShowMessage(string Message)
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.LabelWarning.Text = Message;
    }

    private string NextPage(string Mode)
    {
        string NextPageURL = "";
        int StateId = -1;
        int WfCode = -1;
        int FocucedIndex = GridViewWFState.FocusedRowIndex;

        if (FocucedIndex > -1)
        {
            DataRow row = GridViewWFState.GetDataRow(FocucedIndex);
            StateId = (int)(row["StateId"]);
            WfCode = (int)(row["WorkFlowCode"]);
        }
        if (StateId == -1)
        {
            ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            NextPageURL = FindNextPageURL(WfCode, StateId, Mode);
            if (Mode != "View")
                Response.Redirect(NextPageURL);
        }
        return NextPageURL;
    }

    private string FindNextPageURL(int WfCode, int StateId, string PageMode)
    {
        int TableId = -1;
        string Url = "";        
        string QueryString = "?";

        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.FindByCode(StateId);
        if (WorkFlowStateManager.Count == 1)
        {
            TableId = int.Parse(WorkFlowStateManager[0]["TableId"].ToString());
        }
        else
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }

        switch (WfCode)
        {
            case (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming:
                #region DocMemberFile
                Url = "../Employee/Document/AddMemberFile.aspx";
                //*****PgMd ; MFId ; MeId

                DocMemberFileManager.FindByCode(TableId, 0);
                if (DocMemberFileManager.Count > 0)
                {
                    QueryString += "PgMd=" + Utility.EncryptQS(PageMode);
                    string MFId = DocMemberFileManager[0]["MfId"].ToString();
                    string MeId = DocMemberFileManager[0]["MeId"].ToString();
                    QueryString += "&MFId=" + Utility.EncryptQS(MFId);
                    QueryString += "&MeId=" + Utility.EncryptQS(MeId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");

                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }

                #endregion
                break;
            case (int)TSP.DataManager.WorkFlows.EngOfficeConfirming:
                #region EngOffice
                Url = "../Document/EngOffice/EngOfficeRegister.aspx";
                //******PageMode ; EngOfId ; EOfId

                TSP.DataManager.EngOffFileManager EngOffFileManager = new TSP.DataManager.EngOffFileManager();
                EngOffFileManager.FindByCode(TableId);
                if (EngOffFileManager.Count == 1)
                {
                    QueryString += "PageMode=" + Utility.EncryptQS(PageMode);
                    string EOfId = EngOffFileManager[0]["EOfId"].ToString();
                    string EngOfId = EngOffFileManager[0]["EngOfId"].ToString();
                    QueryString += "&EOfId=" + Utility.EncryptQS(EOfId);
                    QueryString += "&EngOfId=" + Utility.EncryptQS(EngOfId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");

                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }

                #endregion
                break;
            case (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming:
                #region ImplementDocumentConfirming
                Url = "../ImplementDoc/AddImplementDoc.aspx";
                //*****PgMd ; MFId

                DocMemberFileManager.FindByCode(TableId, 1);
                if (DocMemberFileManager.Count > 0)
                {
                    QueryString += "PgMd=" + Utility.EncryptQS(PageMode);
                    string MFId = DocMemberFileManager[0]["MfId"].ToString();
                    QueryString += "&MFId=" + Utility.EncryptQS(MFId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");

                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }

                #endregion
                break;
            case (int)TSP.DataManager.WorkFlows.InstitueConfirming:
                #region Institue
                TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
                Url = "../Amoozesh/AddInstitues.aspx";
                //**********PageMode ; InsId

                InstitueManager.FindByCode(TableId);
                if (InstitueManager.Count == 1)
                {
                    QueryString += "PageMode=" + Utility.EncryptQS(PageMode);
                    string InsId = InstitueManager[0]["InsId"].ToString();
                    QueryString += "&InsId=" + Utility.EncryptQS(InsId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");

                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.MemberConfirming:
                #region Memmbers
                TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                Url = "../MembersRegister/MemberRequestShow.aspx";
                //********MeId ; TP ; PageMode ; MReId;
                //+ "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString)
                MemberRequestManager.FindByCode(TableId);
                if (MemberRequestManager.Count == 1)
                {
                    QueryString += "PageMode=" + Utility.EncryptQS(PageMode);
                    string MeId = MemberRequestManager[0]["MeId"].ToString();
                    QueryString += "&MeId=" + Utility.EncryptQS(MeId);
                    string TP = "1";
                    QueryString += "&TP=" + Utility.EncryptQS(TP);
                    // DataTable dtMeRe = MemberRequestManager.FindByMemberId(TableId, 0, -1);
                    //if (dtMeRe.Rows.Count > 0)
                    //{
                    string MReId = MemberRequestManager[0]["MReId"].ToString();
                    QueryString += "&MReId=" + Utility.EncryptQS(MReId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");
                    //}
                    //else
                    //{
                    //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    //}
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming:
                #region ObservationDocumentConfirming
                Url = "../Document/AddObservationDoc.aspx";

                //*****PgMd ; MFId

                DocMemberFileManager.FindByCode(TableId, 2);
                if (DocMemberFileManager.Count > 0)
                {
                    QueryString += "PgMd=" + Utility.EncryptQS(PageMode);
                    string MFId = DocMemberFileManager[0]["MfId"].ToString();
                    QueryString += "&MFId=" + Utility.EncryptQS(MFId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");

                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming:
                #region Office
                TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
                TSP.DataManager.OfficeRequestManager OfficeRequestManager = new TSP.DataManager.OfficeRequestManager();
                Url = "../OfficeRegister/OfficeRegister.aspx";
                //*******PageMode ; OfId ; OfReId;Dprt

                OfficeRequestManager.FindByCode(TableId);
                if (OfficeRequestManager.Count == 1)
                {
                    QueryString += "PageMode=" + Utility.EncryptQS(PageMode);
                    string OfId = OfficeRequestManager[0]["OfId"].ToString();
                    QueryString += "&OfId=" + Utility.EncryptQS(OfId);
                    string OfReId = OfficeRequestManager[0]["OfReId"].ToString();
                    QueryString += "&OfReId=" + Utility.EncryptQS(OfReId);
                    QueryString += "&Dprt=" + Utility.EncryptQS("MemberShip");
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.PeriodConfirming:
                #region Periods
                TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
                Url = "../Amoozesh/PeriodsView.aspx";
                //*******PageMode ; PPId

                PeriodPresentManager.FindByCode(TableId);
                if (PeriodPresentManager.Count == 1)
                {
                    QueryString += "PageMode=" + Utility.EncryptQS(PageMode);
                    string PPId = PeriodPresentManager[0]["PPId"].ToString();
                    QueryString += "&PPId=" + Utility.EncryptQS(PPId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");


                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.SeminarConfirming:
                #region Seminar
                TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
                Url = "../Amoozesh/SeminarView.aspx";
                //**** SeId ; PageMode

                SeminarManager.FindByCode(TableId);
                if (SeminarManager.Count == 1)
                {
                    QueryString += "PageMode=" + Utility.EncryptQS(PageMode);
                    string SeId = SeminarManager[0]["SeId"].ToString();
                    QueryString += "&SeId=" + Utility.EncryptQS(SeId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");

                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.SMSConfirming:
                #region SMS
                TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
                Url = "../SMS/NewSms.aspx";
                //**** PgMd ; SMSID ; PgName

                SmsManager.FindByCode(TableId);
                if (SmsManager.Count == 1)
                {
                    QueryString += "PgMd=" + Utility.EncryptQS(PageMode);
                    string SMSID = SmsManager[0]["SMSId"].ToString();
                    QueryString += "&SMSID=" + Utility.EncryptQS(SMSID);
                    QueryString += "&PgName=" + Utility.EncryptQS("~/Employee/WorkFlow/WorkFlowUserTask.aspx");
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");

                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.TeachersConfirming:
                #region Teachers
                TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
                Url = "../Amoozesh/AddTeachers.aspx";

                //**** PageMode ; TeId

                TeacherManager.FindByCode(TableId);
                if (TeacherManager.Count == 1)
                {
                    QueryString += "PageMode=" + Utility.EncryptQS(PageMode);
                    string TeId = TeacherManager[0]["TeId"].ToString();
                    QueryString += "&TeId=" + Utility.EncryptQS(TeId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");

                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.TSChangePlansAndDesigner:
                #region TSChangePlansAndDesigner
                Url = "../TechnicalServices/Project/AddPlans.aspx";
                //**** PrjId ; PrjReId ; PrePgMd ; PlnId ; PgMd

                PlansManager.FindByPlansId(TableId);
                if (PlansManager.Count == 1)
                {
                    QueryString += "PgMd=" + Utility.EncryptQS(PageMode);
                    QueryString += "&PrePgMd=" + Utility.EncryptQS("View");
                    string PrjId = PlansManager[0]["ProjectId"].ToString();
                    QueryString += "&PrjId=" + Utility.EncryptQS(PrjId);
                    string PrjReId = PlansManager[0]["PrjReId"].ToString();
                    QueryString += "&PrjReId=" + Utility.EncryptQS(PrjReId);
                    string PlnId = PlansManager[0]["PlansId"].ToString();
                    QueryString += "&PlnId=" + Utility.EncryptQS(PlnId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.TSEndStructuralProjectLicenceConfirming:
                #region TSEndStructuralProjectLicenceConfirming
                Url = "../";
                #endregion
                break;

            //case (int)TSP.DataManager.WorkFlows.TSImplementerChangesConfirming:
            //    Url = "~/";
            //    break;

            case (int)TSP.DataManager.WorkFlows.TSObserverChangesConfirming:
                #region TSObserverChangesConfirming
                Url = "../";
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.TSPlanMethodsChangesConfirming:
                #region TSPlanMehodsChangesConfirming
                Url = "../";
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.TSPlanRevisionConfirming:
                #region TSPlanRevisionConfirming
                Url = "../TechnicalServices/Project/AddPlans.aspx";
                //**** PrjId ; PrjReId ; PrePgMd ; PlnId ; PgMd

                PlansManager.FindByPlansId(TableId);
                if (PlansManager.Count == 1)
                {
                    QueryString += "PgMd=" + Utility.EncryptQS(PageMode);
                    QueryString += "&PrePgMd=" + Utility.EncryptQS("View");
                    string PrjId = PlansManager[0]["ProjectId"].ToString();
                    QueryString += "&PrjId=" + Utility.EncryptQS(PrjId);
                    string PrjReId = PlansManager[0]["PrjReId"].ToString();
                    QueryString += "&PrjReId=" + Utility.EncryptQS(PrjReId);
                    string PlnId = PlansManager[0]["PlansId"].ToString();
                    QueryString += "&PlnId=" + Utility.EncryptQS(PlnId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.TSPlansConfirming:
                #region TSPlansConfirming
                Url = "../TechnicalServices/Project/AddPlans.aspx";
                //**** PrjId ; PrjReId ; PrePgMd ; PlnId ; PgMd

                PlansManager.FindByPlansId(TableId);
                if (PlansManager.Count == 1)
                {
                    QueryString += "PgMd=" + Utility.EncryptQS(PageMode);
                    QueryString += "&PrePgMd=" + Utility.EncryptQS("View");
                    string PrjId = PlansManager[0]["ProjectId"].ToString();
                    QueryString += "&PrjId=" + Utility.EncryptQS(PrjId);
                    string PrjReId = PlansManager[0]["PrjReId"].ToString();
                    QueryString += "&PrjReId=" + Utility.EncryptQS(PrjReId);
                    string PlnId = PlansManager[0]["PlansId"].ToString();
                    QueryString += "&PlnId=" + Utility.EncryptQS(PlnId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.TSProjectConfirming:
                #region TSProjectConfirming
                Url = "../TechnicalServices/Project/ProjectInsert.aspx";
                //**** PageMode ; ProjectId ; PrjReId

                ProjectRequestManager.FindByCode(TableId);
                if (ProjectRequestManager.Count == 1)
                {
                    QueryString += "PageMode=" + Utility.EncryptQS(PageMode);
                    string ProjectId = ProjectRequestManager[0]["ProjectId"].ToString();
                    QueryString += "&ProjectId=" + Utility.EncryptQS(ProjectId);
                    string PrjReId = ProjectRequestManager[0]["PrjReId"].ToString();
                    QueryString += "&PrjReId=" + Utility.EncryptQS(PrjReId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;
            case (int)TSP.DataManager.WorkFlows.EmployeeRequestConfirming:
                #region  EmployeeRequestConfirming
                TSP.DataManager.EmployeeRequestManager EmployeeRequestManager = new TSP.DataManager.EmployeeRequestManager();
                Url = "../Employee/EmployeeInsert.aspx";
                //**** EmpId ;EmpReId;PageMode 
                EmployeeRequestManager.FindByCode(TableId);
                if (EmployeeRequestManager.Count == 1)
                {
                    QueryString += "PageMode=" + Utility.EncryptQS(PageMode);
                    string EmpId = EmployeeRequestManager[0]["EmpId"].ToString();
                    QueryString += "&EmpId=" + Utility.EncryptQS(EmpId);
                    string EmpReId = EmployeeRequestManager[0]["EmpReId"].ToString();
                    QueryString += "&EmpReId=" + Utility.EncryptQS(EmpReId);
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;

            case (int)TSP.DataManager.WorkFlows.MemberCardRequestConfirming:
                #region  MemberCardRequestConfirming
                Url = "../MembersRegister/AddMemberCards.aspx";
                //********MeCrdId ; PgMd                
                //MemberRequestManager.FindByCode(TableId);
                //if (MemberRequestManager.Count == 1)
                //{
                QueryString += "PgMd=" + Utility.EncryptQS(PageMode);
                QueryString += "&MeCrdId=" + Utility.EncryptQS(TableId.ToString());
                QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");
                //    string MReId = MemberRequestManager[0]["MReId"].ToString();
                //  }
                //  else
                //  {
                //      this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                //  }
                #endregion
                break;
            case (int)TSP.DataManager.WorkFlows.TechnicianRequestConfirming:
                #region TechnicianRequestConfirming
                TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
                TechnicianRequestManager.FindByCode(TableId);
                if (TechnicianRequestManager.Count == 1)
                {
                    Url = "../TechniciansManagement/TechnicianInsert.aspx";
                    //****** TnReId ;OtpId ;PageMode
                    QueryString += "PageMode=" + Utility.EncryptQS(PageMode);
                    QueryString += "&TnReId=" + Utility.EncryptQS(TableId.ToString());
                    QueryString += "&OtpId=" + Utility.EncryptQS(TechnicianRequestManager[0]["OtpId"].ToString());
                    QueryString += "&GrdFlt=" + Utility.EncryptQS("") + "&SrchFlt=" + Utility.EncryptQS("");
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;
            case (int)TSP.DataManager.WorkFlows.PeriodRegisterLicenceOutOfTime:
                #region PeriodRegisterLicenceOutOfTime
                TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
                PeriodRegisterManager.FindByCode(TableId);
                if (PeriodRegisterManager.Count == 1)
                {
                    if(Utility.IsDBNullOrNullValue(PeriodRegisterManager[0]["MeId"]))
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    if (Utility.IsDBNullOrNullValue(PeriodRegisterManager[0]["PPId"]))
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    int MeId = Convert.ToInt32(PeriodRegisterManager[0]["MeId"]);
                    int PPId = Convert.ToInt32(PeriodRegisterManager[0]["PPId"]);
                    Url = "../Amoozesh/PeriodsView.aspx";
                    //****** PRId ;PageMode ; MeId
                    QueryString += "PageMode=" + Utility.EncryptQS(PageMode);
                    QueryString += "&PRId=" + Utility.EncryptQS(TableId.ToString());
                    QueryString += "&MeId=" + Utility.EncryptQS(MeId.ToString());
                    QueryString += "&PPId=" + Utility.EncryptQS(PPId.ToString());


                  
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                }
                #endregion
                break;
        }
        TSP.DataManager.WFParameters WFParameters = new TSP.DataManager.WFParameters();
        QueryString += "&"+WFParameters.UserTaskPageQueryStringName+"=" + Utility.EncryptQS(WFParameters.UserTaskPageQueryStringValue);

        return (Url + QueryString);
    }

    private void Delete()
    {
        try
        {
            int StateId = -1;
            int WfCode = -1;
            Boolean CanDeleteSMS = true;
            if (GridViewWFState.FocusedRowIndex > -1)
            {
                DataRow row = GridViewWFState.GetDataRow(GridViewWFState.FocusedRowIndex);
                StateId = (int)(row["StateId"]);
                WfCode = (int)(row["WorkFlowCode"]);
                int LastStateType = int.Parse(row["LastStateType"].ToString());
                if (WfCode == (int)TSP.DataManager.WorkFlows.SMSConfirming && LastStateType == 2 && LastStateType == 3)
                {
                    int TableId = (int)(row["TableId"]);
                    TSP.DataManager.SmsManager SmsManager = new TSP.DataManager.SmsManager();
                    SmsManager.FindByCode(TableId);
                    if (SmsManager.Count == 1)
                    {
                        string SMSExpireDate = SmsManager[0]["ExpireDate"].ToString();
                        string DateNow = Utility.GetDateOfToday();
                        int IsExpired = string.Compare(SMSExpireDate, DateNow);
                        if (IsExpired > 0)
                        {
                            CanDeleteSMS = false;
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "به دلیل به پایان نرسیدن جریان کار پرونده انتخاب شده قادر به حذف درخواست از کارتابل نمی باشید.";
                            return;
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات درخواست توسط کاربر دیگری تغییر یافته است.";
                        return;
                    }

                }
                else if (WfCode != (int)TSP.DataManager.WorkFlows.SMSConfirming && LastStateType != 2 && LastStateType != 3)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به دلیل به پایان نرسیدن جریان کار پرونده انتخاب شده قادر به حذف درخواست از کارتابل نمی باشید.";
                    return;
                }
            }

            if (StateId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            }
            else
            {
                TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
                WorkFlowStateManager.FindByCode(StateId);
                if (WorkFlowStateManager.Count == 1)
                {
                    WorkFlowStateManager[0].BeginEdit();

                    WorkFlowStateManager[0]["InActive"] = 1;

                    WorkFlowStateManager[0].EndEdit();
                    if (WorkFlowStateManager.Save() > 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد.";
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
            GridViewWFState.DataBind();
        }
        catch (Exception err)
        {
            SetError(err);
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

    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());

                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewWFState.FilterExpression = GrdFlt;
            }
        }

    }

    private int SetGridRowIndex()
    {
        int Index = -1;
        if (!IsPostBack)
        {
            Utility.SetGridRowIndex(GridViewWFState, Request.QueryString["PostId"], ref Index);
        }
        return Index;
    }

    private void SetPriorityImage(ASPxGridViewTableDataCellEventArgs e)
    {
        DevExpress.Web.ASPxImage btnPriority = (DevExpress.Web.ASPxImage)GridViewWFState.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewWFState.Columns["Priority"], "btnPriority");
        if (btnPriority != null)
        {
            if (Utility.IsDBNullOrNullValue(e.GetValue("PriorityId")))
            {
                btnPriority.ToolTip = "تعریف نشده";
                btnPriority.ImageUrl = "~/Images/WFUnNounState.png";
                return;
            }

            if (int.Parse(e.GetValue("PriorityId").ToString()) == (int)TSP.DataManager.AutomationPriority.Normal)
            {
                btnPriority.ToolTip = e.GetValue("PriorityName").ToString();
                btnPriority.ImageUrl = "~/Images/Priority1.png";
            }
            else if (int.Parse(e.GetValue("PriorityId").ToString()) == (int)TSP.DataManager.AutomationPriority.AboveNormal)
            {
                btnPriority.ToolTip = e.GetValue("PriorityName").ToString();
                btnPriority.ImageUrl = "~/Images/Priority2.png";
            }
            else if (int.Parse(e.GetValue("PriorityId").ToString()) == (int)TSP.DataManager.AutomationPriority.Immediate)
            {
                btnPriority.ToolTip = e.GetValue("PriorityName").ToString();
                btnPriority.ImageUrl = "~/Images/Priority3.png";
            }           
        }
    }

    private DataTable BidnGrid()
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt = WorkFlowStateManager.SelectDocumentTrace();
        GridViewWFState.DataSource = dt;
        GridViewWFState.KeyFieldName = "Id";
        GridViewWFState.DataBind();
        return dt;
    }

    #endregion
}
