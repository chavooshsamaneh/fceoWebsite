using System;
using System.Data;
using System.Text;

public partial class Employee_TechnicalServices_Project_ObserverSelected : System.Web.UI.Page
{
    #region Properties
    Boolean IsPageRefresh = false;
    private string _PageMode
    {
        get
        {
            return HDpage["PgMd"].ToString();
        }
        set
        {
            HDpage["PgMd"] = value.ToString();
        }
    }
    private string _SelectObserverType
    {
        get
        {
            return HDpage["SelObT"].ToString();
        }
        set
        {
            HDpage["SelObT"] = value.ToString();
        }
    }
    private int _PrjReqId
    {

        get
        {
            return Convert.ToInt32(HDpage["PrjReqId"]);
        }
        set
        {
            HDpage["PrjReqId"] = value.ToString();
        }
    }

    private int _PrjId
    {

        get
        {
            return Convert.ToInt32(HDpage["PrjId"]);
        }
        set
        {
            HDpage["PrjId"] = value.ToString();
        }
    }

    private int _Foundation
    {
        get
        {
            return Convert.ToInt32(HDpage["Foundation"]);
        }
        set
        {
            HDpage["Foundation"] = value.ToString();
        }
    }
    private int _GroupId
    {
        get
        {
            return Convert.ToInt32(HDpage["GroupId"]);
        }
        set
        {
            HDpage["GroupId"] = value.ToString();
        }
    }
    private int _StructureSkeletonId
    {
        get
        {
            return Convert.ToInt32(HDpage["StructureSkeletonId"]);
        }
        set
        {
            HDpage["StructureSkeletonId"] = value.ToString();
        }
    }
    private int _PreviousGroupId
    {
        get
        {
            return Convert.ToInt32(HDpage["PreviousGroupId"]);
        }
        set
        {
            HDpage["PreviousGroupId"] = value.ToString();
        }
    }

    private int _CitId
    {
        get
        {
            return Convert.ToInt32(HDpage["CitId"]);
        }
        set
        {
            HDpage["CitId"] = value.ToString();
        }
    }

    private int _IsCharity
    {
        get
        {
            return Convert.ToInt32(HDpage["IsCharity"]);
        }
        set
        {
            HDpage["IsCharity"] = value.ToString();
        }
    }
    private int _AgentId
    {
        get
        {
            return Convert.ToInt32(HDpage["AgentId"]);
        }
        set
        {
            HDpage["AgentId"] = value.ToString();
        }
    }
    private int _OwnerMeId
    {
        get
        {
            return Convert.ToInt32(HDpage["OwnerMeId"]);
        }
        set
        {
            HDpage["OwnerMeId"] = value.ToString();
        }
    }
    private double _N
    {
        get
        {
            return Convert.ToDouble(HDpage["N"]);
        }
        set
        {
            HDpage["N"] = value.ToString();
        }
    }
    private int _NearestGradId
    {
        get
        {
            return Convert.ToInt32(HDpage["NearestGradId"]);
        }
        set
        {
            HDpage["NearestGradId"] = value.ToString();
        }
    }
    private string _listDesignerMeId
    {
        get
        {
            return HDpage["listDesignerMeId"].ToString();
        }
        set
        {
            HDpage["listDesignerMeId"] = value.ToString();
        }
    }

    private int _PriceArchiveId
    {
        get
        {
            return Convert.ToInt32(HDpage["PriceArchiveId"]);
        }
        set
        {
            HDpage["PriceArchiveId"] = value.ToString();
        }
    }
    private int _DecrementPercent
    {
        get
        {
            return Convert.ToInt32(HDpage["DecrementPercent"]);
        }
        set
        {
            HDpage["DecrementPercent"] = value.ToString();
        }
    }
    private int _WagePercent
    {
        get
        {
            return Convert.ToInt32(HDpage["WagePercent"]);
        }
        set
        {
            HDpage["WagePercent"] = value.ToString();
        }
    }
    private Boolean _IsGreaterThan5000
    {
        get
        {
            return Convert.ToBoolean(HDpage["IsGreaterThan3000"]);
        }
        set
        {
            HDpage["IsGreaterThan3000"] = value.ToString();
        }
    }
    public int _PrjReTypeId
    {

        get
        {
            return Convert.ToInt32(HDpage["PrjReTypeId"]);
        }
        set
        {
            HDpage["PrjReTypeId"] = value.ToString();
        }
    }
    public int _ProjectStatusId
    {

        get
        {
            return Convert.ToInt32(HDpage["ProjectStatusId"]);
        }
        set
        {
            HDpage["ProjectStatusId"] = value.ToString();
        }
    }
    public int _FundationDifference
    {
        get
        {
            return Convert.ToInt32(HDpage["FundationDifference"]);
        }
        set
        {
            HDpage["FundationDifference"] = value.ToString();
        }
    }
    private string _Year
    {
        get
        {
            return HDpage["Year"].ToString();
        }
        set
        {
            HDpage["Year"] = value.ToString();
        }
    }
    public int _CountWorkUnder400
    {
        get
        {
            return Convert.ToInt32(HDpage["CountWorkUnder400"]);
        }
        set
        {
            HDpage["CountWorkUnder400"] = value.ToString();
        }
    }
    public Boolean _IsShahrakSanaati
    {

        get
        {
            return Convert.ToBoolean(HDpage["IsShahrakSanaati"]);
        }
        set
        {
            HDpage["IsShahrakSanaati"] = value.ToString();
        }
    }
    public Boolean _IsEghdamMeliMaskan
    {
        get
        {
            return Convert.ToBoolean(HDpage["IsEghdamMeliMaskan"]);
        }
        set
        {
            HDpage["IsEghdamMeliMaskan"] = value.ToString();
        }
    }
    public Boolean _IsPopulationUnder25000
    {
        get
        {
            return Convert.ToBoolean(HDpage["IsPopulationUnder25000"]);
        }
        set
        {
            HDpage["IsPopulationUnder25000"] = value.ToString();
        }
    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
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


            if (string.IsNullOrEmpty(Request.QueryString["PrjId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PgMd"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            SetKeys();

        }
    }
    protected void btnObserverSelect_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager ProjectObserverSelectedManager = new TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager();
        ProjectObserverSelectedManager.SearchObserverSelected(-1, _PrjId, _PrjReqId);
        if (ProjectObserverSelectedManager.Count > 0 && _SelectObserverType != "Limit")
        {
            SetLabelWarning("پیشتر به این پروژه ناظر ارجاع داده شده است");
            return;
        }
        FunctionA2();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect("Observers.aspx?ProjectId=" + Utility.EncryptQS(_PrjId.ToString())
            + "&PageMode=" + Request.QueryString["PageMode"]
            + "&PrjReId=" + Utility.EncryptQS(_PrjReqId.ToString())
            + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
    }

    #region Methods

    private void SetKeys()
    {
        try
        {
            _PrjId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjId"]));
            _PrjReqId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjReId"]));
            _PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);
            _SelectObserverType = Utility.DecryptQS(Request.QueryString["SelObT"]);

            if (string.IsNullOrEmpty(_PageMode) || Utility.IsDBNullOrNullValue(_PrjId) || Utility.IsDBNullOrNullValue(_PrjReqId) || Utility.IsDBNullOrNullValue(_SelectObserverType))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            FillProjectInfo(_PrjReqId);
            if (!CheckAccountingConditions())
            {
                SetLabelWarning("قبل از ارجاع ناظر در این درخواست باید حق الزحمه پرداخت شده باشد");
                return;
            }

            ObjectDataSourceSelectObs.SelectParameters["ProjectId"].DefaultValue = _PrjId.ToString();
            GridViewProjectSelectedObserver.DataBind();
            #region   //اطلاعات گشایش ظرفیت سالانه
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            if (_AgentId == Utility.GetCurrentAgentCode())
                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
            else
                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
            if (CapacityAssignmentManager.Count != 0)
            {
                _Year = CapacityAssignmentManager[0]["Year"].ToString();
                _CountWorkUnder400 = (_AgentId == Utility.GetCurrentAgentCode()) ? Convert.ToInt32(CapacityAssignmentManager[0]["WorkCountUnder400MainAgent"]) : Convert.ToInt32(CapacityAssignmentManager[0]["WorkCountUnder400OtherAgents"]);
            }
            else
            {
                SetLabelWarning("اطلاعات مربوط به بازگشایی ظرفیت یافت نمی شود");
                return;

            }
            #endregion
            #region یافتن نزدیک ترین پایه به گروه ساختمانی پروژه
            switch (_GroupId)
            {
                case (int)TSP.DataManager.TSStructureGroups.A:
                    _NearestGradId = (int)TSP.DataManager.DocumentGrads.Grade3;
                    break;
                case (int)TSP.DataManager.TSStructureGroups.B:
                    _NearestGradId = (int)TSP.DataManager.DocumentGrads.Grade2;
                    break;
                case (int)TSP.DataManager.TSStructureGroups.C:
                    _NearestGradId = (int)TSP.DataManager.DocumentGrads.Grade1;
                    break;
                case (int)TSP.DataManager.TSStructureGroups.D:
                    _NearestGradId = (int)TSP.DataManager.DocumentGrads.Arshad;
                    break;
            }

            #endregion
            TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();
            _PriceArchiveId = PriceArchiveManager.FindCurrentPriceArchiveId();

            TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager ProjectObserverSelectedManager = new TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager();
            ProjectObserverSelectedManager.SearchObserverSelected(-1, _PrjId, _PrjReqId);
            if (ProjectObserverSelectedManager.Count > 0 && _SelectObserverType != "Limit")
            {
                SetLabelWarning("پیشتر به این پروژه ناظر ارجاع داده شده است");
                return;
            }
            SetMode(_PageMode);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
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
        }
    }

    private void SetNewModeKeys()
    {
        btnObserverSelect.Enabled = btnObserverSelect2.Enabled = true;
        CheckAccess();
        if (_SelectObserverType == "Limit")
        {
            RoundPanelCmbObsType.Visible = true;
            GridViewObsTypeByProjectInfo.Visible = false;
        }
        if (_SelectObserverType == "All")
        {
            RoundPanelCmbObsType.Visible = false;
            GridViewObsTypeByProjectInfo.Visible = true;
        }
        comboObsTypeByProjectInfo.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
        ClearForm();
        ASPxRoundPanelObsType.HeaderText = "جدید";
    }

    private void SetViewModeKeys()
    {
        //FillForm();
        //SetControlsViewMode();
        GridViewObsTypeByProjectInfo.Visible = true;
    }
    public string DataTableToJSONWithJSONNet(DataTable table)
    {
        var jsonString = new StringBuilder();
        if (table.Rows.Count > 0)
        {
            jsonString.Append("[");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                          + "\":" + "\""
                                          + table.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                          + "\":" + "\""
                                          + table.Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    jsonString.Append("}");
                }
                else
                {
                    jsonString.Append("},");
                }
            }
            jsonString.Append("]");
        }
        return jsonString.ToString();
    }

    private void FunctionA2()
    {
        //CountWorks :تعدادکل کار مجاز
        //CountRandomSelected : تعداد کار انتخاب تصادفی
        //CountInproccesWorks : تعداد کار های در دست اجرا بالای 400 متر.هر کار بالای 400 متر اختصاص داده شود یا آزاد شود بروزرسانی می شود
        //CountRejectByObs:تعداد کار رد شده از سوی ناظر. برای هر 2 کار رد شدن یک کار از تعداد مجاز کسر می شود و سر سال در صفحه اختصاص ظرفیت برای همه اعضا صفر می شود        
        //CountUnder400MeterWork: تعداد کارهای زیر 400 متر نظارت و طراحی.به ازای هر 5 تا کار 1  کار محسوب می شود.هر کار 400 متر که اختصاص داده شود یا آزاد شود مقدار آن بروز می شود
        //CountRemainWorkCount : CountWorks- CountInproccesWorks -((CountUnder400MeterWork+CountUnder400MeterWorkDesign)-4)-(CountRejectByObs/2) :هر بار که کار جدید اختصاص داده می شود یا آزاد می شود مقدار آن طبق فرمول روبرو بروز می شود
        //*******************
        //RemainCapacityObsReal ظرفیت باقیمانده واقعی  نظارت
        //Capacity : ظرفیت کل اختصاص داده شده.زمان ثبت آماده بکاری و تغییرات آماده بکاری بروزرسانی انجام می شود
        //UsedCapacity:ظرفیت مصرف شده.در زمان اختصاص کار و یا آزادسازی آن،  بروزرسانی می شود
        //RemainCapacity :Capacity-UsedCapacity ظرفیت باقیمانده.زمان اختصاص کار یا آزادسازی بروزرسانی می شود
        //PercentOfCapacityUsage: (UsedCapacity/Capacity) جهت بدست آوردن مقدار متغییر است و زمان بروزرسانی ظرفیت مصرفی بروزرسانی می شود
        try
        {
            DataView dvSelectedObs;
            //DataView dvTemp;
            string Sort = "";
            string Filter = "";
            string FilterMeIdList = "";
            // DataTable dtSelectedObs = new DataTable();
            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
            TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager ProjectObserverSelectedManager = new TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager();
            //Boolean IsFoundationIsGreaterThanRemainCapacityObsReal = false;
            //کل متراژی که در این ارجاع باید کار روی آن صورت گیرد
            double FoundationForObserverSelect = 0;
            //***********************************************************************************************
            //******
            double FoundationCalculation = 0;
            /*************************************************************************************************************************
            کل درصدی از متراژ پروژه که بایستی ارجاع نظارت به گروه خاص از ناظرین (سازه/ برق/مکانیک/معماری/..)ارجاع داده شود.در صورتی که متراژ بالای 5000 باشد تقسیم بندیمتراژ این پرامتر در پارامتر 
            FoundationCalculation
            قرار داده می شود*/
            double FoundationCalculationPart = 0;
            //**************************************************************************************************************************
            double FoundationAssigned = 0;
            double SmFoundationAssigned = 0;
            double FoundationRemained = 0;
            double BeforAssignedFundation = 0;
            double PatrCount = 0;
            double SumDecimalTotal = 0;
            double SumDecimalObsType = 0;
            bool PlusOneGroupMajor = false;
            Boolean ForBreak = false;
            int CountdtObserverMajorByProjectInfo = 0;

            #region Datatable جهت نگهداری ناظرانی که از لیست انتخاب می شودند

            DataTable dtTSFunctionALogs = new DataTable();
            dtTSFunctionALogs.Columns.Add("ProjectId", typeof(int));
            dtTSFunctionALogs.Columns.Add("StageId", typeof(int));
            dtTSFunctionALogs.Columns.Add("IngridiantMajor", typeof(string));
            dtTSFunctionALogs.Columns.Add("Date", typeof(string));
            dtTSFunctionALogs.Columns.Add("ModifiedDate", typeof(DateTime));
            dtTSFunctionALogs.Columns.Add("UserId", typeof(int));
            dtTSFunctionALogs.Columns.Add("LogJson", typeof(string));

            DataTable dtObsSelectedForInsert = new DataTable();
            dtObsSelectedForInsert.Columns.Add("MasterMfMjParentId", typeof(int));
            dtObsSelectedForInsert.Columns.Add("MeId", typeof(int));
            dtObsSelectedForInsert.Columns.Add("ObsId", typeof(int));
            dtObsSelectedForInsert.Columns.Add("ObsDate", typeof(string));
            dtObsSelectedForInsert.Columns.Add("MembershipDate", typeof(string));
            dtObsSelectedForInsert.Columns.Add("ObsWorkReqId", typeof(int));
            dtObsSelectedForInsert.Columns.Add("FoundationAssigned", typeof(double));
            dtObsSelectedForInsert.Columns.Add("ReasonType", typeof(TSP.DataManager.TSProjectObserverSelectedReasonType));
            dtObsSelectedForInsert.Columns.Add("PercentOfPrjFundation", typeof(double));
            dtObsSelectedForInsert.Columns.Add("PlusOneGroupMajor", typeof(bool));
            dtObsSelectedForInsert.Columns.Add("IsMother", typeof(Int16));

            DataView dvObserverMajorByProjectInfo = (DataView)ObjectDataSourceObserverMajorByProjectInfo.Select();

            DataTable dtConditionalCapacity = new DataTable();
            dtConditionalCapacity.Columns.Add("Capacity", typeof(int));
            dtConditionalCapacity.Columns.Add("MeOfficeEngOId", typeof(int));

            #endregion
            
           
            if (_SelectObserverType == "Limit")//**آیا ارجاع محدود می باشد؟
                CountdtObserverMajorByProjectInfo = 1;
            else if (_SelectObserverType == "All")
                CountdtObserverMajorByProjectInfo = dvObserverMajorByProjectInfo.Count;
            if (CountdtObserverMajorByProjectInfo == 0)
            {
                SetLabelWarning("سامانه در تعیین ترکیب ناظران برای این پروژه موفق نبوده است. انطباق گروه ساختمانی، نوع اسکلت و متراژ را بررسی کنید.");
                return;
            }
            
            string MeIdList = ProjectObserverSelectedManager.SelectTSProjectObserverSelectedMemberForFunctionA(_PrjId);


            #region محاسبه متراژی که می خواهیم ارجاع دهیم.1- کل پروژه می باشد. یا 2-مابه تفاوت نسبت به درخواست قبل می باشد

            FoundationForObserverSelect = _Foundation;
            //***تنها در حالتی که زمین فاقد ساختمان باشد ارجاع کل زیر بنای پروژه در نظر گرفته می شود در بقیه موارد توسعه بنا میزان توسعه برای ارجاع ملاک می باشد********************
            if (_ProjectStatusId != (int)TSP.DataManager.TSProjectStatus.BuildingNotStarted && _FundationDifference > 0)
            {
                FoundationForObserverSelect = _FundationDifference;
            }
            #endregion
            


            for (int i = 0; i < CountdtObserverMajorByProjectInfo; i++)
            {
                #region Reset Foundation Parameters
                SmFoundationAssigned = FoundationAssigned = 0;
                PlusOneGroupMajor = false;
                string[] FoundationPercentList = null;
                #region آیا ارجاع محدود است یا ارجاع کل
                if (_SelectObserverType == "Limit")
                {
                    if (i > 0)
                        return;
                    if (Utility.IsDBNullOrNullValue(comboObsTypeByProjectInfo.SelectedItem) || Utility.IsDBNullOrNullValue(comboObsTypeByProjectInfo.SelectedItem.Value))
                    {
                        SetLabelWarning("نوع ناظر باید انتخاب شود");
                        return;
                    }
                    string filter = "MajorIdList = '" + comboObsTypeByProjectInfo.SelectedItem.Value.ToString() + "'";
                    dvObserverMajorByProjectInfo.RowFilter = filter;
                    FoundationPercentList = dvObserverMajorByProjectInfo[i]["FoundationPercentList"].ToString().Split(',');

                }

                if (_SelectObserverType == "All")
                    FoundationPercentList = dvObserverMajorByProjectInfo[i]["FoundationPercentList"].ToString().Split(',');
                #endregion
                #region محاسبه متراژی که قبلا به این دسته از ناظران ارجاع داده و ذخیره شده است و باید از متراژی ارجاعی این دسته کسر شود
                //**در صورتی که ساخت و ساز شروع نشده باشد بایستی کل متراژ پروژه و کل ارجاعات تا این لحظه در نظر گرفته شود
                if (_ProjectStatusId == (int)TSP.DataManager.TSProjectStatus.BuildingNotStarted || _ProjectStatusId == (int)TSP.DataManager.TSProjectStatus.SaveProjectInfo)
                    BeforAssignedFundation = ProjectObserverSelectedManager.ObserverSelectedAssignedFundationForFunctionA(dvObserverMajorByProjectInfo[i]["MajorIdList"].ToString(), _PrjId);
                else
                    BeforAssignedFundation = ProjectObserverSelectedManager.ObserverSelectedAssignedFundationForFunctionA(dvObserverMajorByProjectInfo[i]["MajorIdList"].ToString(), _PrjId, _PrjReqId);
                #endregion
                double FoundationPercentItem = Convert.ToInt32(FoundationPercentList[0]);
                double PercentOfPrjFundation = FoundationPercentItem / 100;

                SumDecimalTotal = Math.Round(((FoundationForObserverSelect * PercentOfPrjFundation) - Math.Floor(FoundationForObserverSelect * PercentOfPrjFundation)) + SumDecimalTotal, 1);

                if (SumDecimalTotal == Math.Floor(SumDecimalTotal) && SumDecimalTotal!=0)
                {
                    FoundationCalculationPart = FoundationRemained = FoundationCalculation = Math.Floor((FoundationForObserverSelect * PercentOfPrjFundation)) + SumDecimalTotal - BeforAssignedFundation;
                    SumDecimalTotal = 0;
                    PlusOneGroupMajor = true;
                }
                else
                {
                    int countOnePlus = 0;
                    for (int r = 0; r < dvObserverMajorByProjectInfo[i]["MajorIdList"].ToString().Split(',').Length; r++)
                    {
                        countOnePlus += ProjectObserverSelectedManager.SelectObserverSelectedMajorOnePlusCount(_PrjId, Convert.ToInt32(dvObserverMajorByProjectInfo[i]["MajorIdList"].ToString().Split(',')[r]));
                    }
                    if (_SelectObserverType == "Limit" && SumDecimalTotal != 0 && countOnePlus > 0)
                    {
                        FoundationCalculationPart = FoundationRemained = FoundationCalculation = Math.Floor(FoundationForObserverSelect * PercentOfPrjFundation) + 1 - BeforAssignedFundation;
                        PlusOneGroupMajor = true;
                    }
                    else
                        FoundationCalculationPart = FoundationRemained = FoundationCalculation = Math.Floor(FoundationForObserverSelect * PercentOfPrjFundation) - BeforAssignedFundation;
                }

                if (Math.Floor(FoundationRemained) <= 0)
                {
                    SetLabelWarning("قبلا برای این دسته, ناظران ذی صلاح انتخاب شده است ");
                    return;
                }

                if (_IsGreaterThan5000 && dvObserverMajorByProjectInfo[i]["MajorIdList"].ToString() != TSP.DataManager.MainMajors.Mapping.ToString())
                {
                    PatrCount = Math.Floor(FoundationCalculation / 2500);

                    if (PatrCount < 2)
                    {
                        PatrCount = 1;
                    }
                    if (PatrCount >= 5)
                    {
                        PatrCount = 5;
                    }

                    FoundationCalculation = Math.Floor(FoundationCalculation / PatrCount);
                }
                #endregion

                
                #region پیدا کردن ناظر برای یک گروه مشخص از ناظران تازمانی که کل درصد متراژ مربوط به آن دسته اختصاص داده شود
                do
                {
                    #region Find Observer
                    Sort = Filter = "";

                    if (_SelectObserverType == "Limit" && Utility.IsDBNullOrNullValue(dvObserverMajorByProjectInfo[i]["MajorIdList"]))
                        continue;
                    if (_SelectObserverType == "All" && Utility.IsDBNullOrNullValue(dvObserverMajorByProjectInfo[i]["MajorIdList"]))
                        continue;
                    string ObsType = "";
                    if (_SelectObserverType == "Limit")
                        ObsType = dvObserverMajorByProjectInfo[i]["MajorIdList"].ToString();
                    if (_SelectObserverType == "All")
                        ObsType = dvObserverMajorByProjectInfo[i]["MajorIdList"].ToString();

                    #region Part1

                    dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, _CitId, _GroupId, ObsType, -1, _AgentId, true, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                    if (dvSelectedObs.Count != 0 && MeIdList != "")
                        dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                    AddLogToDT(ref dtTSFunctionALogs, 101, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                    #endregion

                    #region Part2
                    if (dvSelectedObs.Count == 0)
                    {
                        dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, _CitId, -1, ObsType, _NearestGradId, _AgentId, true, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                        if (dvSelectedObs.Count != 0 && MeIdList != "")
                            dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                        AddLogToDT(ref dtTSFunctionALogs, 102, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                        //***********************
                        //تیکه جدید

                        if (dvSelectedObs.Count == 0)
                        {

                            #region   //در صورتی که در یک شهر ظرفیت تمامی ناظران پر نباشد اما همه رد کار زده باشند ابتدا به اجبار به یکی از آنهایی بدهد که رد کار زده اند اما ظرفیت دارد
                            #region Part2-1-f
                            //به اجبار
                            dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, _CitId, _GroupId, ObsType, -1, _AgentId, true, 1, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                            if (dvSelectedObs.Count != 0 && MeIdList != "")
                                dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                            AddLogToDT(ref dtTSFunctionALogs, 1021, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                            #endregion
                            #region Part2-2-f
                            if (dvSelectedObs.Count == 0)
                            {
                                //به اجبار
                                dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, _CitId, -1, ObsType, _NearestGradId, _AgentId, true, 1, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                                if (dvSelectedObs.Count != 0 && MeIdList != "")
                                    dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                                AddLogToDT(ref dtTSFunctionALogs, 1022, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                                if (dvSelectedObs.Count == 0)
                                {
                                    //در صورتی که در یک شهر ظرفیت تمامی ناظران پر باشد پروژه به همان ناظران با اعمال ظرفیت منفی تخصیص داده شود 
                                    #region  حذف فیلتر منفی بودن باقیمانده نظارت واقعی  و اجرای مجدد تمام فیلترها
                                    #region Part2-1

                                    dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, _CitId, _GroupId, ObsType, -1, _AgentId, false, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                                    if (dvSelectedObs.Count != 0 && MeIdList != "")
                                        dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                                    AddLogToDT(ref dtTSFunctionALogs, 1023, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                                    #endregion
                                    #region Part2-2
                                    if (dvSelectedObs.Count == 0)
                                    {
                                        dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, _CitId, -1, ObsType, _NearestGradId, _AgentId, false, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                                        if (dvSelectedObs.Count != 0 && MeIdList != "")
                                            dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                                        AddLogToDT(ref dtTSFunctionALogs, 1024, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                                        //درصورتی که از  تمامی ناظرانی که ظرفیت آنها پر بود همه هم رد کار زده بودند به اجبار به یکی از همان ها که رد کرده اند ارجاع دهد
                                        if (dvSelectedObs.Count == 0)
                                        {
                                            #region  درصورتی که از  تمامی ناظرانی که ظرفیت آنها پر بود همه هم رد کار زده بودند به اجبار به یکی از همان ها که رد کرده اند ارجاع دهد 
                                            #region Part2-1

                                            //به اجبار
                                            dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, _CitId, _GroupId, ObsType, -1, _AgentId, false, 1, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                                            if (dvSelectedObs.Count != 0 && MeIdList != "")
                                                dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                                            AddLogToDT(ref dtTSFunctionALogs, 1025, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                                            #endregion
                                            #region Part2-2
                                            if (dvSelectedObs.Count == 0)
                                            {
                                                //به اجبار
                                                dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, _CitId, -1, ObsType, _NearestGradId, _AgentId, false, 1, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                                                if (dvSelectedObs.Count != 0 && MeIdList != "")
                                                    dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                                                AddLogToDT(ref dtTSFunctionALogs, 1026, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                                                //پایان تیکه جدید
                                                //تیکه جدید پایان***************


                                                if (dvSelectedObs.Count == 0)
                                                {
                                                    #region Part3 
                                                    //*****************فیلتر بر اساس نمایندگی شخص و پروژه(شهرهای اطراف شهر اصلی پروژه)*****************
                                                    //****SetLabelWarning("ناظر جهت" + comboObsTypeByProjectInfo.Items[i].Text + " یافت نشد");
                                                    dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, -1, -1, ObsType, _NearestGradId, _AgentId, true, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                                                    if (dvSelectedObs.Count != 0 && MeIdList != "")
                                                        dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                                                    AddLogToDT(ref dtTSFunctionALogs, 103, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                                                    if (dvSelectedObs.Count == 0)
                                                    {
                                                        dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, -1, -1, ObsType, -1, _AgentId, true, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                                                        if (dvSelectedObs.Count != 0 && MeIdList != "")
                                                            dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                                                        AddLogToDT(ref dtTSFunctionALogs, 1031, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                                                        if (dvSelectedObs.Count == 0)
                                                        {

                                                            #region  حذف فیلتر منفی بودن باقیمانده نظارت واقعی  و اجرای مجدد تمام فیلترها
                                                            #region Part2-1

                                                            dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, _CitId, _GroupId, ObsType, -1, _AgentId, false, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                                                            if (dvSelectedObs.Count != 0 && MeIdList != "")
                                                                dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                                                            AddLogToDT(ref dtTSFunctionALogs, 10311, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                                                            #endregion
                                                            #region Part2-2
                                                            if (dvSelectedObs.Count == 0)
                                                            {
                                                                dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, _CitId, -1, ObsType, _NearestGradId, _AgentId, false, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                                                                if (dvSelectedObs.Count != 0 && MeIdList != "")
                                                                    dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                                                                AddLogToDT(ref dtTSFunctionALogs, 10312, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                                                                if (dvSelectedObs.Count == 0)
                                                                {
                                                                    #region Part3 
                                                                    //*****************فیلتر بر اساس نمایندگی شخص و پروژه(نزدیک ترین شهر به پروژه)?????????????????????????????
                                                                    //****SetLabelWarning("ناظر جهت" + comboObsTypeByProjectInfo.Items[i].Text + " یافت نشد");
                                                                    dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, -1, -1, ObsType, _NearestGradId, _AgentId, false, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                                                                    if (dvSelectedObs.Count != 0 && MeIdList != "")
                                                                        dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                                                                    AddLogToDT(ref dtTSFunctionALogs, 10313, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                                                                    if (dvSelectedObs.Count == 0)
                                                                    {
                                                                        dvSelectedObs = new DataView(ObserverWorkRequestManager.SelectTSObserverWorkRequestBasedFunctionA(_OwnerMeId, Utility.GetDateOfToday(), _PrjId, _IsCharity == 1 ? 1 : -1, -1, -1, ObsType, -1, _AgentId, false, _IsShahrakSanaati, _IsEghdamMeliMaskan));
                                                                        if (dvSelectedObs.Count != 0 && MeIdList != "")
                                                                            dvSelectedObs.RowFilter = FilterMeIdList = " MeId not in (" + MeIdList + ")";
                                                                        AddLogToDT(ref dtTSFunctionALogs, 103131, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                                                                    }
                                                                    #endregion
                                                                }
                                                            }
                                                            #endregion
                                                            #endregion
                                                        }
                                                    }
                                                    #endregion
                                                }
                                            }

                                            //******************
                                            //مال تیکه جدید
                                        }
                                    }
                                }
                                #endregion
                                #endregion
                            }
                        }
                        #endregion
                        #endregion
                        #endregion
                        #endregion
                    }
                    //مال تیکه جدید
                    //*************************


                    Sort = "RemainCapacityObsReal desc";
                    dvSelectedObs.Sort = Sort;
                    AddLogToDT(ref dtTSFunctionALogs, 100, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                    if (dvSelectedObs.Count == 0)
                    {
                        if (_SelectObserverType == "Limit")
                            SetLabelWarning("عضوی جهت ارجاع کار یافت نشد-خروج 1032-لطفا به دفتر شیراز تماس بگیرید و کد پروژه را اطلاع دهید");
                        ForBreak = false;
                        break;
                    }

                    if (_IsGreaterThan5000 && FoundationRemained < 2500 && PatrCount >= 2 && PatrCount < 5)
                    {
                        FoundationCalculation = FoundationRemained;
                    }

                    FoundationAssigned = FoundationCalculation;

                    double Fr = FoundationRemained - FoundationAssigned;
                    if (Fr < 100)
                        FoundationAssigned += Fr;

                    if (dvSelectedObs.Count == 0)
                    {
                        //عضوی جهت ارجاع کار یافت نشد
                        ForBreak = false;
                        break;
                    }
                    #endregion

                    #region *******Part4

                    #region *****PartRemainCapacityObsRealAndCountWork
                    string FilterFoundation = "RemainCapacityObsReal>=" + FoundationCalculation.ToString();  //***فیلتر بر اساس ظرفیت باقی مانده نظازت واقعی با توجه به متراژ پروژه                    
                    string FilterCountWork = ""; string FilterCountWorkUnder400 = " OR (CountUnder400MeterWork+CountUnder400MeterWorkDesign) < " + _CountWorkUnder400.ToString() + ")";
                    if (_Foundation < 400)//**پروژه زیر 400 متر  5 تا پروژه اول 1 پروژه محسوب می شود K<=CountWorks:8 && m<=5
                                         //**حذف افرادی که تعداد کار باقیمانده آنها صفر و تعداد کار زیر400 بیشتر یا مساوی 5 است
                    {
                        FilterCountWork = "(CountRemainWorkCount >0" + FilterCountWorkUnder400;
                    }
                    else//**فیلتر بر اساس تعداد کار باقیمانده
                    {
                        FilterCountWork = "CountRemainWorkCount >0";//CountWorks:8
                    }
                    //*********Filter dvSelectedObs
                    Filter = FilterMeIdList != "" ? FilterMeIdList + " AND " + FilterFoundation + " AND " + FilterCountWork : FilterFoundation + " AND " + FilterCountWork;
                    dvSelectedObs.RowFilter = Filter;
                    AddLogToDT(ref dtTSFunctionALogs, 1041, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                    if (dvSelectedObs.Count == 0)//اگر کسی در لیست نماند، فیلتر تعداد کار را حذف کن
                    {
                        if (_Foundation < 400)
                        {
                            //اگر کسی در لیست نماند،اول فیلتر تعداد کار زیر 400 را حذف کن
                            FilterCountWork = "CountRemainWorkCount > 0";
                            Filter = FilterMeIdList != "" ? FilterMeIdList + " AND " + FilterFoundation + " AND " + FilterCountWork : FilterFoundation + " AND " + FilterCountWork;
                            dvSelectedObs.RowFilter = Filter;
                            AddLogToDT(ref dtTSFunctionALogs, 1047, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                            if (dvSelectedObs.Count == 0)
                            {
                                //اگر کسی در لیست نماند، فیلتر تعداد کار را کلا حذف کن
                                Filter = FilterMeIdList != "" ? FilterMeIdList + " AND " + FilterFoundation : FilterFoundation;
                                dvSelectedObs.RowFilter = Filter;
                                AddLogToDT(ref dtTSFunctionALogs, 1048, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                            }
                        }
                        else
                        {
                            //اگر کسی در لیست نماند و پروژه زیر 400 هم نباشد فیلتر تعداد کار را کلا حذف کن
                            Filter = FilterMeIdList != "" ? FilterMeIdList + " AND " + FilterFoundation : FilterFoundation;
                            dvSelectedObs.RowFilter = Filter;
                            AddLogToDT(ref dtTSFunctionALogs, 1042, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                        }
                    }
                    if (dvSelectedObs.Count == 0)//اگر کسی در لیست نماند، فیلتر ظرفیت باقی مانده نظارت واقعی را حذف کن
                    {
                        Filter = FilterMeIdList;
                        dvSelectedObs.RowFilter = Filter;
                        AddLogToDT(ref dtTSFunctionALogs, 1043, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                    }
                    #endregion

                    #region *********PartWorkCount
                    string FilterWorkCount = " CountObsWorkSelect=0";//CountInproccesWorksObs***  تعداد کار باقیمانده برابر تعداد کل کارهایی باشد که می تواندبگیر.یعنی تا الان هیج کاری نگرفته است
                    string FilterPlusWorkCount = Filter != "" ? Filter + " AND " + FilterWorkCount : FilterWorkCount;
                    dvSelectedObs.RowFilter = FilterPlusWorkCount;
                    AddLogToDT(ref dtTSFunctionALogs, 1045, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                    if (dvSelectedObs.Count == 0)
                    {
                        dvSelectedObs.RowFilter = Filter;
                        FilterWorkCount = "";
                        AddLogToDT(ref dtTSFunctionALogs, 1046, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                    }
                    else
                    {
                        Filter = FilterPlusWorkCount;
                    }
                    #endregion

                    #region *******PartNj
                    Sort = "PercentOfCapacityUsage asc";
                    dvSelectedObs.Sort = Sort;
                    //1049
                    AddLogToDT(ref dtTSFunctionALogs, (int)TSP.DataManager.TSObserverSelectLog.PercentOfCapacityUsageSort, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                    Double PercentOfCapacityUsage = Convert.ToDouble(dvSelectedObs[0]["PercentOfCapacityUsage"]);
                    int j = (int)Math.Ceiling((PercentOfCapacityUsage * 100) / _N);
                    if (j == 0) j = 1;
                    string FilterNj = " PercentOfCapacityUsage " + "<" + (j * _N / 100).ToString();
                    string FilterPlusNj = Filter != "" ? Filter + " AND " + FilterNj : FilterNj;
                    dvSelectedObs.RowFilter = FilterPlusNj;
                    AddLogToDT(ref dtTSFunctionALogs, 1044, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                    if (dvSelectedObs.Count == 0)
                    {
                        j++;
                        FilterNj = " PercentOfCapacityUsage " + "<" + (j * _N / 100).ToString();
                        FilterPlusNj = Filter != "" ? Filter + " AND " + FilterNj : FilterNj;
                        dvSelectedObs.RowFilter = FilterPlusNj;
                        //10491
                        AddLogToDT(ref dtTSFunctionALogs, (int)TSP.DataManager.TSObserverSelectLog.EqualPercentOfCapacityUsageSort, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                    }
                    Filter = FilterPlusNj;
                    #endregion


                    Sort = "RemainCapacityObsFuncA desc";
                    dvSelectedObs.Sort = Sort;
                    AddLogToDT(ref dtTSFunctionALogs, 104, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                    int MaxRemainCapacity = Convert.ToInt32(dvSelectedObs[0]["RemainCapacityObsFuncA"]);
                    if (dvSelectedObs.Count == 1 || MaxRemainCapacity != Convert.ToInt32(dvSelectedObs[1]["RemainCapacityObsFuncA"]))
                    {

                        AddProjectIngridientMember(ref dtObsSelectedForInsert, dvSelectedObs[0], TSP.DataManager.TSProjectObserverSelectedReasonType.MaxRemainCapacity, FoundationAssigned, PercentOfPrjFundation, ref MeIdList, ref dtConditionalCapacity, PlusOneGroupMajor);
                        //*********
                        FoundationRemained -= FoundationAssigned;
                        SmFoundationAssigned += FoundationAssigned;
                        //**********
                        continue;
                    }

                    #endregion

                    #region *********Part5
                    string fCap = " AND RemainCapacityObsFuncA= " + MaxRemainCapacity.ToString();
                    dvSelectedObs.RowFilter = Filter + fCap;
                    AddLogToDT(ref dtTSFunctionALogs, 1053, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                    if (dvSelectedObs.Count == 0)
                    {
                        dvSelectedObs.RowFilter = Filter;
                        fCap = "";
                        AddLogToDT(ref dtTSFunctionALogs, 1051, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                    }
                    string fGrd = " AND ObsId= " + _NearestGradId.ToString();
                    dvSelectedObs.RowFilter = Filter + fCap + fGrd;
                    AddLogToDT(ref dtTSFunctionALogs, 1054, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                    if (dvSelectedObs.Count == 0)
                    {
                        dvSelectedObs.RowFilter = Filter;
                        fGrd = "";
                        AddLogToDT(ref dtTSFunctionALogs, 1052, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));
                    }

                    Filter = Filter + fCap + fGrd;
                    dvSelectedObs.RowFilter = Filter;
                    AddLogToDT(ref dtTSFunctionALogs, 105, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                    MaxRemainCapacity = Convert.ToInt32(dvSelectedObs[0]["RemainCapacityObsFuncA"]);
                    if (dvSelectedObs.Count == 1 || MaxRemainCapacity != Convert.ToInt32(dvSelectedObs[1]["RemainCapacityObsFuncA"]))
                    {
                        AddProjectIngridientMember(ref dtObsSelectedForInsert, dvSelectedObs[0], TSP.DataManager.TSProjectObserverSelectedReasonType.SameObsIdAndProjectGroupId, FoundationAssigned, PercentOfPrjFundation, ref MeIdList, ref dtConditionalCapacity, PlusOneGroupMajor);
                        //*********
                        FoundationRemained -= FoundationAssigned;
                        SmFoundationAssigned += FoundationAssigned;
                        //**********
                        continue;
                    }
                    #endregion

                    #region *********Part6
                    //مرتب سازی براساس ناظرانی که کمترین کار را دارند ** بیشترین تعداد کار باقیمانده را دارند
                    Sort += ",CountInproccesWorksObs asc";
                    dvSelectedObs.Sort = Sort;
                    int MinCountRemainWorkCount = Convert.ToInt32(dvSelectedObs[0]["CountInproccesWorksObs"]);
                    AddLogToDT(ref dtTSFunctionALogs, 106, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                    if (dvSelectedObs.Count == 1 || MinCountRemainWorkCount != Convert.ToInt32(dvSelectedObs[1]["CountInproccesWorksObs"]))
                    {
                        AddProjectIngridientMember(ref dtObsSelectedForInsert, dvSelectedObs[0], TSP.DataManager.TSProjectObserverSelectedReasonType.LeastWorkCount, FoundationAssigned, PercentOfPrjFundation, ref MeIdList, ref dtConditionalCapacity, PlusOneGroupMajor);
                        //*********
                        FoundationRemained -= FoundationAssigned;
                        SmFoundationAssigned += FoundationAssigned;
                        //**********
                        continue;
                    }
                    #region  فیلتر براساس کسانی که کمترین کار را دارند
                    Filter += " AND CountInproccesWorksObs= " + MinCountRemainWorkCount.ToString();
                    dvSelectedObs.RowFilter = Filter;
                    AddLogToDT(ref dtTSFunctionALogs, 1062, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                    if (dvSelectedObs.Count == 1)
                    {
                        AddProjectIngridientMember(ref dtObsSelectedForInsert, dvSelectedObs[0], TSP.DataManager.TSProjectObserverSelectedReasonType.LeastWorkCount, FoundationAssigned, PercentOfPrjFundation, ref MeIdList, ref dtConditionalCapacity, PlusOneGroupMajor);
                        //*********
                        FoundationRemained -= FoundationAssigned;
                        SmFoundationAssigned += FoundationAssigned;
                        //**********
                        continue;
                    }
                    #endregion
                    #endregion

                    #region *********Part7
                    Sort += ",ObsDate asc";//مرتب سازی براساس کوچکترین تاریخ اخذ صلاحیت=با سابقه ترین فرد
                    dvSelectedObs.Sort = Sort;
                    AddLogToDT(ref dtTSFunctionALogs, 107, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                    string MinObsDate = dvSelectedObs[0]["ObsDate"].ToString();
                    if (dvSelectedObs.Count == 1 || MinObsDate != dvSelectedObs[1]["ObsDate"].ToString())
                    {
                        AddProjectIngridientMember(ref dtObsSelectedForInsert, dvSelectedObs[0], TSP.DataManager.TSProjectObserverSelectedReasonType.LeastObsDateAndMorExpert, FoundationAssigned, PercentOfPrjFundation, ref MeIdList, ref dtConditionalCapacity, PlusOneGroupMajor);
                        //*********
                        FoundationRemained -= FoundationAssigned;
                        SmFoundationAssigned += FoundationAssigned;
                        //**********
                        continue;
                    }
                    #region  فیلتر براساس با سابقه ترین فرد
                    Filter += " AND ObsDate= '" + MinObsDate + "'";
                    dvSelectedObs.RowFilter = Filter;
                    AddLogToDT(ref dtTSFunctionALogs, 1072, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                    if (dvSelectedObs.Count == 1)
                    {
                        AddProjectIngridientMember(ref dtObsSelectedForInsert, dvSelectedObs[0], TSP.DataManager.TSProjectObserverSelectedReasonType.LeastObsDateAndMorExpert, FoundationAssigned, PercentOfPrjFundation, ref MeIdList, ref dtConditionalCapacity, PlusOneGroupMajor);
                        //*********
                        FoundationRemained -= FoundationAssigned;
                        SmFoundationAssigned += FoundationAssigned;
                        //**********
                        continue;
                    }
                    #endregion
                    #endregion

                    #region *********Part8
                    Sort += ",MembershipDate asc";//مرتب سازی براساس کوچکترین تاریخ عضویت در سازمان=بیشترین سابقه عضویت
                    dvSelectedObs.Sort = Sort;
                    string MinMembershipDate = dvSelectedObs[0]["MembershipDate"].ToString();
                    AddLogToDT(ref dtTSFunctionALogs, 108, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                    if (dvSelectedObs.Count == 1 || MinMembershipDate != dvSelectedObs[1]["MembershipDate"].ToString())
                    {
                        AddProjectIngridientMember(ref dtObsSelectedForInsert, dvSelectedObs[0], TSP.DataManager.TSProjectObserverSelectedReasonType.LeastMembershipDateAndOldestMember, FoundationAssigned, PercentOfPrjFundation, ref MeIdList, ref dtConditionalCapacity, PlusOneGroupMajor);
                        //*********
                        FoundationRemained -= FoundationAssigned;
                        SmFoundationAssigned += FoundationAssigned;
                        //**********
                        continue;
                    }
                    #region  بیشترین سابقه عضویت
                    Filter += " AND MembershipDate= '" + MinMembershipDate + "'";
                    dvSelectedObs.RowFilter = Filter;
                    AddLogToDT(ref dtTSFunctionALogs, 1082, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                    if (dvSelectedObs.Count == 1)
                    {
                        AddProjectIngridientMember(ref dtObsSelectedForInsert, dvSelectedObs[0], TSP.DataManager.TSProjectObserverSelectedReasonType.LeastMembershipDateAndOldestMember, FoundationAssigned, PercentOfPrjFundation, ref MeIdList, ref dtConditionalCapacity, PlusOneGroupMajor);
                        //*********
                        FoundationRemained -= FoundationAssigned;
                        SmFoundationAssigned += FoundationAssigned;
                        //**********
                        continue;
                    }
                    #endregion
                    #endregion

                    #region *********Part9 طراح در لیست باشد
                    if (!Utility.IsDBNullOrNullValue(_listDesignerMeId))
                    {
                        string FilterDesigner = Filter + " AND MeId in (" + _listDesignerMeId + ")";
                        dvSelectedObs.RowFilter = FilterDesigner;
                        AddLogToDT(ref dtTSFunctionALogs, 109, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                        if (dvSelectedObs.Count == 1)
                        {
                            AddProjectIngridientMember(ref dtObsSelectedForInsert, dvSelectedObs[0], TSP.DataManager.TSProjectObserverSelectedReasonType.ProjectDesigner, FoundationAssigned, PercentOfPrjFundation, ref MeIdList, ref dtConditionalCapacity, PlusOneGroupMajor);

                            //*********
                            FoundationRemained -= FoundationAssigned;
                            SmFoundationAssigned += FoundationAssigned;
                            //**********
                            continue;
                        }
                    }
                    #endregion

                    #region *********Part10
                    dvSelectedObs.RowFilter = Filter;
                    Sort += ",CountRandomSelected asc";//مرتب سازی براساس کوچکترین دفعات انتخاب تصادفی
                    dvSelectedObs.Sort = Sort;
                    int MinCountRandomSelected = Convert.ToInt32(dvSelectedObs[0]["CountRandomSelected"]);
                    AddLogToDT(ref dtTSFunctionALogs, 1010, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                    if (dvSelectedObs.Count == 1 || MinCountRandomSelected != Convert.ToInt32(dvSelectedObs[1]["CountRandomSelected"]))
                    {
                        AddProjectIngridientMember(ref dtObsSelectedForInsert, dvSelectedObs[0], TSP.DataManager.TSProjectObserverSelectedReasonType.MinCountRandomSelected, FoundationAssigned, PercentOfPrjFundation, ref MeIdList, ref dtConditionalCapacity, PlusOneGroupMajor);

                        //*********
                        FoundationRemained -= FoundationAssigned;
                        SmFoundationAssigned += FoundationAssigned;
                        //**********
                        continue;
                    }
                    #region  فیلتر براساس کمترین دفعات انتخاب تصادفی
                    Filter += " AND CountRandomSelected= " + MinCountRandomSelected;
                    dvSelectedObs.RowFilter = Filter;
                    AddLogToDT(ref dtTSFunctionALogs, 10102, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                    if (dvSelectedObs.Count == 1)
                    {
                        AddProjectIngridientMember(ref dtObsSelectedForInsert, dvSelectedObs[0], TSP.DataManager.TSProjectObserverSelectedReasonType.MinCountRandomSelected, FoundationAssigned, PercentOfPrjFundation, ref MeIdList, ref dtConditionalCapacity, PlusOneGroupMajor);

                        //*********
                        FoundationRemained -= FoundationAssigned;
                        SmFoundationAssigned += FoundationAssigned;
                        //**********
                        continue;
                    }
                    #endregion
                    #endregion

                    #region *********Part11 انتخاب تصادفی عضو
                    int cntRemaindObs = dvSelectedObs.Count;
                    System.Random r = new Random();
                    int RandomIndex = r.Next(cntRemaindObs);
                    if (RandomIndex == cntRemaindObs)
                        RandomIndex = RandomIndex - 1;
                    AddProjectIngridientMember(ref dtObsSelectedForInsert, dvSelectedObs[RandomIndex], TSP.DataManager.TSProjectObserverSelectedReasonType.RandomSelect, FoundationAssigned, PercentOfPrjFundation, ref MeIdList, ref dtConditionalCapacity, PlusOneGroupMajor);

                    //*********
                    FoundationRemained -= FoundationAssigned;
                    SmFoundationAssigned += FoundationAssigned;
                    //**********

                    AddLogToDT(ref dtTSFunctionALogs, 1011, ObsType, DataTableToJSONWithJSONNet(dvSelectedObs.ToTable()));

                    continue;
                    #endregion
                    #endregion
                } while (SmFoundationAssigned != FoundationCalculationPart);
                #endregion

                if (ForBreak)
                    break;
            }
            InsertSelectedObserverFromdt(dtObsSelectedForInsert, dtConditionalCapacity);
            TSP.DataManager.TechnicalServices.TSFunctionALogsManager TSFunctionALogsManager = new TSP.DataManager.TechnicalServices.TSFunctionALogsManager();
            TSFunctionALogsManager.InsertDataTableIntoTSFunctionALogs(dtTSFunctionALogs);
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _PrjReqId, (int)TSP.DataManager.TableType.TSProject_Observers, "ارجاع کار نظارت  " + (_IsPopulationUnder25000 ? "جمعیت شهر پروژه زیر 25000 نفر است " : ""), Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.InsertNewRow);
        }
        catch (Exception ex)
        {
            SetLabelWarning("خطا در ارجاع کار ناظران ایجاد شده است");
            Utility.SaveWebsiteError(ex);
        }
    }

    private Boolean InsertSelectedObserverFromdt(DataTable dtObsSelectedForInsert, DataTable dtConditionalCapacity)
    {
        DataView dvObsSelectedForInsert;
        int MeIdMother = -2;
        #region Define Managers
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager ProjectObserverSelectedManager = new TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager();
        TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObsManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager CapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();
        if (dtConditionalCapacity.Rows.Count > 0)
        {
            trans.Add(ConditionalCapacityManager);
        }
        trans.Add(ObserverWorkRequestManager);
        trans.Add(ProjectObsManager);
        trans.Add(CapacityDecrementManager);
        trans.Add(ProjectObserverSelectedManager);
        #endregion

        try
        {
            if (dtObsSelectedForInsert.Rows.Count == 0)
            {
                SetLabelWarning("عضوی جهت ارجاع کار یافت نشد");
                return false;
            }
            //*** شهرک صنعتی نیاز به ناظر هماهنگ کننده ندارند
            if (!_IsShahrakSanaati)
            {
                ProjectObsManager.FindObsMother(_PrjId);
                if (ProjectObsManager.Count == 0)
                {
                    dvObsSelectedForInsert = new DataView(dtObsSelectedForInsert);
                    string Filter = "MasterMfMjParentId= '" + Convert.ToUInt32(TSP.DataManager.MainMajors.Civil).ToString() + "'" + " OR MasterMfMjParentId= '" + Convert.ToUInt32(TSP.DataManager.MainMajors.Architecture) + "'";
                    dvObsSelectedForInsert.RowFilter = Filter;
                    if (dvObsSelectedForInsert.Count > 0)
                    {
                        string Sort = "ObsId asc,ObsDate asc,MembershipDate asc";
                        dvObsSelectedForInsert.Sort = Sort;
                        MeIdMother = Convert.ToInt32(dvObsSelectedForInsert[0]["MeId"]);
                        dvObsSelectedForInsert[0].BeginEdit();
                        dvObsSelectedForInsert[0]["IsMother"] = 1;
                        dvObsSelectedForInsert[0].EndEdit();
                        dvObsSelectedForInsert.Table.AcceptChanges();
                    }
                }
            }
            trans.BeginSave();
            for (int i = 0; i < dtObsSelectedForInsert.Rows.Count; i++)
            {

                Nullable<int> OfId = null;//???????????\?عضویت فرد در شرکت حقوقی طراح و ناظر ثبت شود
                int ObserversTypeId = -1;
                Boolean IsMother = false;
                int MeOfOthId = Convert.ToInt32(dtObsSelectedForInsert.Rows[i]["MeId"]);
                if (Convert.ToInt16(dtObsSelectedForInsert.Rows[i]["IsMother"]) == 1)
                    IsMother = true;
                switch (Convert.ToInt32(dtObsSelectedForInsert.Rows[i]["MasterMfMjParentId"]))
                {
                    case (int)TSP.DataManager.MainMajors.Architecture:
                        ObserversTypeId = (int)TSP.DataManager.TSObserversType.Memari;
                        break;
                    case (int)TSP.DataManager.MainMajors.Civil:
                        ObserversTypeId = (int)TSP.DataManager.TSObserversType.Sazeh;
                        break;
                    case (int)TSP.DataManager.MainMajors.Electronic:
                        ObserversTypeId = (int)TSP.DataManager.TSObserversType.TasisatBargh;
                        break;
                    case (int)TSP.DataManager.MainMajors.Mapping:
                        ObserversTypeId = (int)TSP.DataManager.TSObserversType.Mapping;
                        break;
                    case (int)TSP.DataManager.MainMajors.Mechanic:
                        ObserversTypeId = (int)TSP.DataManager.TSObserversType.TasisatMechanic;
                        break;
                }

                #region Insert PrjObserver
                DataRow drPrjObs = ProjectObsManager.NewRow();
                drPrjObs["ProjectId"] = _PrjId;
                drPrjObs["PrjReId"] = _PrjReqId;
                drPrjObs["MeOfficeOthPEngOId"] = MeOfOthId;
                drPrjObs["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.Member;
                drPrjObs["IsMother"] = IsMother;
                if (Convert.ToBoolean(_IsCharity))
                    drPrjObs["PayFivePercent"] = true;
                else
                    drPrjObs["PayFivePercent"] = false;
                drPrjObs["ObserversTypeId"] = ObserversTypeId;
                drPrjObs["PriceArchiveId"] = _PriceArchiveId;
                drPrjObs["Year"] = _Year;
                drPrjObs["CreateDate"] = Utility.GetDateOfToday();
                drPrjObs["UserId"] = Utility.GetCurrentUser_UserId();
                drPrjObs["ModifiedDate"] = DateTime.Now;
                ProjectObsManager.AddRow(drPrjObs);
                if (ProjectObsManager.Save() <= 0)
                {
                    trans.CancelSave();
                    SetLabelWarning("خطایی در ذخیره اطلاعات انجام گرفته است");
                    return false;
                }
                // ProjectObsManager.DataSet.AcceptChanges();
                ProjectObsManager.DataTable.AcceptChanges();
                int PrjObsId = Convert.ToInt32(ProjectObsManager[ProjectObsManager.Count - 1]["ProjectObserversId"]);// Convert.ToInt32(ProjectObsManager[i]["ProjectObserversId"]);
                #endregion

                #region Inert ProjectObserverSelected
                DataRow drObsSelected = ProjectObserverSelectedManager.NewRow();
                drObsSelected["ObsWorkReqId"] = dtObsSelectedForInsert.Rows[i]["ObsWorkReqId"];
                drObsSelected["ReasonType"] = dtObsSelectedForInsert.Rows[i]["ReasonType"];
                drObsSelected["ProjectId"] = _PrjId;
                drObsSelected["ProjectRequestId"] = _PrjReqId;
                drObsSelected["MeId"] = dtObsSelectedForInsert.Rows[i]["MeId"];
                drObsSelected["MeType"] = 0;//Member
                drObsSelected["RandomMembersList"] = "";
                drObsSelected["IsObserverConfirmed"] = 1;
                drObsSelected["CapacityDecrement"] = dtObsSelectedForInsert.Rows[i]["FoundationAssigned"];
                drObsSelected["PercentOfPrjFundation"] = dtObsSelectedForInsert.Rows[i]["PercentOfPrjFundation"];
                drObsSelected["PlusOneGroupMajor"] = dtObsSelectedForInsert.Rows[i]["PlusOneGroupMajor"];
                drObsSelected["ProjectIngridientMajorsId"] = dtObsSelectedForInsert.Rows[i]["MasterMfMjParentId"];
                drObsSelected["ProjectObserversId"] = PrjObsId;
                drObsSelected["CreateDate"] = Utility.GetDateOfToday();
                drObsSelected["UserId"] = Utility.GetCurrentUser_UserId();
                drObsSelected["ModifiedDate"] = DateTime.Now;
                ProjectObserverSelectedManager.AddRow(drObsSelected);

                if (ProjectObserverSelectedManager.Save() <= 0)
                {
                    trans.CancelSave();
                    SetLabelWarning("خطایی در ذخیره اطلاعات انجام گرفته است");
                    return false;
                }
                ProjectObserverSelectedManager.DataTable.AcceptChanges();
                #endregion
                #region Insert Capacity
                string CapacityDecrement = _DecrementPercent == 0 ? "0" : Math.Floor(Convert.ToDouble(dtObsSelectedForInsert.Rows[i]["FoundationAssigned"]) * _DecrementPercent / 100).ToString();
                string CapacityWage = Math.Floor(Convert.ToDouble(dtObsSelectedForInsert.Rows[i]["FoundationAssigned"]) * _WagePercent / 100).ToString();
                if (_Foundation < 100)
                {
                    CapacityWage = Math.Floor(Convert.ToDouble(dtObsSelectedForInsert.Rows[i]["PercentOfPrjFundation"]) * 100 * _WagePercent / 100).ToString();
                }
                CapacityCalculations CapacityCalculations = new CapacityCalculations();
                int IsWorkFree = Convert.ToBoolean(_IsCharity) ? 1 : 0;
                int IsFree = 0;
                CapacityCalculations.InsertProjectCapacityDecrement(CapacityDecrementManager, CapacityDecrement, CapacityWage
                    , (Int16)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, PrjObsId, OfId
                    , Utility.GetCurrentUser_UserId()
                    , MeOfOthId, (int)TSP.DataManager.TSMemberType.Member, _PrjId, IsFree, Utility.GetDateOfToday(), false, IsWorkFree);
                //CapacityDecrementManager.DataTable.AcceptChanges();
                #endregion
                #region Update Observer Shars
                TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
                ProjectRequestManager.UpdateObserverPriceByObserverId(PrjObsId, trans, ProjectObsManager);
                //ProjectRequestManager.DataTable.AcceptChanges();
                //ProjectObsManager.DataTable.AcceptChanges();
                Boolean RandonDelect = false;
                if ((TSP.DataManager.TSProjectObserverSelectedReasonType)dtObsSelectedForInsert.Rows[i]["ReasonType"] == TSP.DataManager.TSProjectObserverSelectedReasonType.RandomSelect)
                    RandonDelect = true;
                CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, CapacityDecrementManager, MeOfOthId, Utility.GetCurrentUser_UserId(), _PrjId, _CitId, Convert.ToBoolean(_IsCharity), TSP.DataManager.TSProjectIngridientType.Observer, null, RandonDelect, false);
                #endregion

            }
            for (int i = 0; i < dtConditionalCapacity.Rows.Count; i++)
            {
                DataRow rowConditionalCapacity = ConditionalCapacityManager.NewRow();

                rowConditionalCapacity["ReasonId"] = (int)TSP.DataManager.TSReason.FunctionA;
                rowConditionalCapacity["Capacity"] = dtConditionalCapacity.Rows[i]["Capacity"];
                rowConditionalCapacity["FromDate"] = Utility.GetDateOfToday();
                rowConditionalCapacity["ToDate"] = Utility.GetDateOfToday();
                rowConditionalCapacity["Description"] = "افزایش اتوماتیک متراژ در تابع ارجاع کار نظارت";
                rowConditionalCapacity["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.Member;
                rowConditionalCapacity["MeOfficeEngOId"] = dtConditionalCapacity.Rows[i]["MeOfficeEngOId"];
                rowConditionalCapacity["ProjectIngridientTypeId"] = (int)TSP.DataManager.TSProjectIngridientType.Observer;
                rowConditionalCapacity["ProjectId"] = _PrjId;
                rowConditionalCapacity["InActive"] = 0;
                rowConditionalCapacity["IsConfirmed"] = 1;
                rowConditionalCapacity["UserId"] = Utility.GetCurrentUser_UserId();
                rowConditionalCapacity["ModifiedDate"] = DateTime.Now;

                ConditionalCapacityManager.AddRow(rowConditionalCapacity);
                ConditionalCapacityManager.Save();
                ConditionalCapacityManager.DataTable.AcceptChanges();
            }
            trans.EndSave();
            SendSms(dtObsSelectedForInsert);
            SetLabelWarning("ذخیره موارد یافت شده انجام شد");
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            return false;
        }
        return true;

    }

    private void AddLogToDT(ref DataTable dtTSFunctionALogs, int StageId, string ObsType, string JSON)
    {
        DataRow dr = dtTSFunctionALogs.NewRow();
        dr["ProjectId"] = _PrjId;
        dr["StageId"] = StageId;
        dr["IngridiantMajor"] = ObsType;
        dr["Date"] = Utility.GetDateOfToday();
        dr["ModifiedDate"] = DateTime.Now;
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["LogJson"] = JSON;
        dtTSFunctionALogs.Rows.Add(dr);
        dtTSFunctionALogs.AcceptChanges();
    }
    private void AddProjectIngridientMember(ref DataTable dtObsSelectedForInsert, DataRowView dtrowObsSelected, TSP.DataManager.TSProjectObserverSelectedReasonType ReasonType, double FoundationAssigned, double PercentOfPrjFundation, ref string MeIdList, ref DataTable dtConditionalCapacity, bool PlusOneGroupMajor)
    {
        DataRow dr = dtObsSelectedForInsert.NewRow();
        dr["IsMother"] = 0;
        dr["MasterMfMjParentId"] = dtrowObsSelected["MasterMfMjParentId"];
        dr["MeId"] = dtrowObsSelected["MeId"];
        dr["ObsId"] = dtrowObsSelected["ObsId"];
        dr["ObsDate"] = dtrowObsSelected["ObsDate"];
        dr["MembershipDate"] = dtrowObsSelected["MembershipDate"];
        dr["ObsWorkReqId"] = dtrowObsSelected["ObsWorkReqId"];
        dr["FoundationAssigned"] = FoundationAssigned;
        dr["ReasonType"] = ReasonType;
        dr["PercentOfPrjFundation"] = PercentOfPrjFundation;
        dr["PlusOneGroupMajor"] = PlusOneGroupMajor;
        dtObsSelectedForInsert.Rows.Add(dr);
        dtObsSelectedForInsert.AcceptChanges();
        if (MeIdList == "")
        {
            MeIdList = dtrowObsSelected["MeId"].ToString();
        }
        else
        {
            MeIdList = MeIdList + "," + dtrowObsSelected["MeId"].ToString();
        }
        if (FoundationAssigned > Convert.ToInt32(dtrowObsSelected["RemainCapacityObsReal"]))
        {
            double Capacity = 0;
            if (Convert.ToDouble(dtrowObsSelected["RemainCapacityObsReal"]) <= 0)
                Capacity = FoundationAssigned;
            else
                Capacity = FoundationAssigned - Convert.ToDouble(dtrowObsSelected["RemainCapacityObsReal"]);

            DataRow drConditionalCapacity = dtConditionalCapacity.NewRow();
            drConditionalCapacity["Capacity"] = Capacity;
            drConditionalCapacity["MeOfficeEngOId"] = dtrowObsSelected["MeId"];
            dtConditionalCapacity.Rows.Add(drConditionalCapacity);
            dtConditionalCapacity.AcceptChanges();
        }
    }
    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        _IsShahrakSanaati = prjInfo.IsShahrakSanaati;
        _IsEghdamMeliMaskan = prjInfo.IsEghdamMeliMaskan;
        _IsCharity = prjInfo.IsCharity;
        _CitId = prjInfo.CitId;
        _GroupId = prjInfo.GroupId;
        _Foundation = prjInfo.Foundation;
        _AgentId = prjInfo.AgentId;
        _PrjReTypeId = prjInfo.PrjReTypeId;
        _ProjectStatusId = prjInfo.ProjectStatusId;
        _FundationDifference = prjInfo.FundationDifference;
        _PreviousGroupId = prjInfo.PreviousGroupId;
        _IsPopulationUnder25000 = prjInfo.IsPopulationUnder25000;
        _StructureSkeletonId = prjInfo.StructureSkeletonId;
        if (_Foundation > 5000)//??????
            _IsGreaterThan5000 = true;
        else
            _IsGreaterThan5000 = false;
        if (_GroupId == (int)TSP.DataManager.TSStructureGroups.A && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Ajory && _IsPopulationUnder25000)//***گروه ساختمانی الف اسکلت بنایی و  جمعیت شهر زیر 25000 نفر
        {
            ObjectDataSourceObserverMajorByProjectInfo.SelectParameters["ExecptionMajorIdList"].DefaultValue = ((int)TSP.DataManager.MainMajors.Electronic).ToString() + "," + ((int)TSP.DataManager.MainMajors.Mechanic).ToString();
        }
        else
            ObjectDataSourceObserverMajorByProjectInfo.SelectParameters["ExecptionMajorIdList"].DefaultValue = "";
        //***تنها در حالتی که زمین فاقد ساختمان باشد ارجاع کل زیر بنای پروژه در نظر گرفته می شود در بقیه موارد توسعه بنا میزان توسعه برای ارجاع ملاک می باشد********************
        if (_ProjectStatusId != (int)TSP.DataManager.TSProjectStatus.BuildingNotStarted && _FundationDifference > 0)
        {
            ObjectDataSourceObserverMajorByProjectInfo.SelectParameters["GroupId"].DefaultValue = prjInfo.GroupId.ToString();
            ObjectDataSourceObserverMajorByProjectInfo.SelectParameters["ProjectFoundation"].DefaultValue = _FundationDifference.ToString();
            if (_StructureSkeletonId == 3)
                ObjectDataSourceObserverMajorByProjectInfo.SelectParameters["StructureSkeletonId"].DefaultValue = _StructureSkeletonId.ToString();
            else
                ObjectDataSourceObserverMajorByProjectInfo.SelectParameters["StructureSkeletonId"].DefaultValue = "-1";
            comboObsTypeByProjectInfo.DataBind();
            GridViewObsTypeByProjectInfo.DataBind();
        }
        else
        {
            ObjectDataSourceObserverMajorByProjectInfo.SelectParameters["GroupId"].DefaultValue = prjInfo.GroupId.ToString();
            ObjectDataSourceObserverMajorByProjectInfo.SelectParameters["ProjectFoundation"].DefaultValue = prjInfo.Foundation.ToString();
            if (_StructureSkeletonId == 3)
                ObjectDataSourceObserverMajorByProjectInfo.SelectParameters["StructureSkeletonId"].DefaultValue = _StructureSkeletonId.ToString();
            else
                ObjectDataSourceObserverMajorByProjectInfo.SelectParameters["StructureSkeletonId"].DefaultValue = "-1";

            comboObsTypeByProjectInfo.DataBind();
            GridViewObsTypeByProjectInfo.DataBind();
        }
        _DecrementPercent = prjInfo.DecrementPercent;
        _WagePercent = prjInfo.WagePercent;
        _OwnerMeId = prjInfo.OwnerMeId;

        TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        DataTable dtDesignerList = ProjectDesignerManager.GetTSProjectDesignerListNezamMembers(_PrjId, _PrjReqId);
        if (dtDesignerList.Rows.Count != 0)
        {
            _listDesignerMeId = dtDesignerList.Rows[0]["Melist"].ToString();
        }

        TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
        CityManager.FindByCode(_CitId);
        if (CityManager.Count != 0)
        {
            _N = Convert.ToDouble(CityManager[0]["NValueInFunctionA"]);
        }
        else
        {
            SetLabelWarning("خطا در بازخوانی اطلاعات شهر پروژه ایجاد شده است");
            return;

        }

    }

    private void ClearForm()
    {
        ObjectDataSourceSelectObs.SelectParameters["ProjectId"].DefaultValue = _PrjId.ToString();
        GridViewProjectSelectedObserver.DataBind();
    }

    private void CheckAccess()
    {

        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());


        if (_PageMode == "New")
        {
            btnObserverSelect.Enabled = btnObserverSelect2.Enabled = per.CanNew || per.CanEdit;
        }
        else if (_PageMode == "View")
        {
            btnObserverSelect.Enabled = btnObserverSelect2.Enabled = false;
        }
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;

    }

    private bool CheckAccountingConditions()
    {
        if (Convert.ToBoolean(_IsCharity)) return true;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers);
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        string AccTypeList = ((int)TSP.DataManager.TSAccountingAccType.ObserversFiche).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation).ToString();
        int ProjectId = -1;
        int TableTypeId = -1;
        if (_PrjReTypeId == (int)TSP.DataManager.TSProjectRequestType.AdditionalStageRequest || _PrjReTypeId == (int)TSP.DataManager.TSProjectRequestType.IncreaseBuildingAreaRequest)//اگر اضافه اشکوب یا افزایش متراژ بود فقط فیش پرداخت شده درخواست جاری مورد قبول است
        {
            ProjectId = -1;
            TableTypeId = _PrjReqId;
        }
        else
        {

            ProjectId = _PrjId;
            TableTypeId = -1;
        }
        DataTable dt = AccountingManager.SelectExistAccountingByAccTypeList(TableTypeId, TableType, ProjectId, AccTypeList);
        if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment)
        {
            return true;
        }
        return false;
    }

    private void SendSms(DataTable dt)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Boolean IsMother = false;
            if (Convert.ToInt32(dt.Rows[i]["IsMother"]) == 1)
            {
                IsMother = true;
            }

            int MeId = Convert.ToInt32(dt.Rows[i]["MeId"]);
            string SMSMeId = MeId.ToString();

            string DocDescription = "";

            TSP.DataManager.MemberManager member = new TSP.DataManager.MemberManager();
            member.FindByCode(MeId);
            string SMSMobileNo = member[0]["MobileNo"].ToString();
            string name = member[0]["FirstName"].ToString() + " " + member[0]["LastName"].ToString();
            DocDescription =
            "جناب آقای/ سرکار خانم مهندس "
            + name + "\n"
            + "به شماره عضویت " +
            member[0]["MeNo"].ToString()
            + "بر اساس اطلاعات اعلام آماده به کاری شما در سامانه ارجاع نظارت الکترونیک، یک پروژه در ساعت " +
            Utility.GetCurrentTime()
            + "و تاریخ " +
            Utility.GetDateOfToday()
         +
"به شما ارجاع شده است." +
(IsMother == true ? "شما به عنوان ناظر هماهنگ کننده این پروژه انتخاب شده اید." : "")
+
" خواهشمند است حداکثر ۲۴ ساعت از زمان ارجاع، با مراجعه به پرتال"
+
 "(https://fceo.ir/login.aspx)"
+
"خود نسبت به تایید پروژه (یا عدم تایید با پذیرش تبعات آن مطابق با نظام نامه ارجاع نظارت شورای مرکزی سازمان) اقدام نمایید. بدیهی است در صورت پایان زمان مقرر و عدم اقدام، مطابق با مقررات ناظر پروژه محسوب می گردید.";

            SendSMSNotification(Utility.Notifications.NotificationTypes.ObsSelected, DocDescription, SMSMobileNo, SMSMeId);
        }
    }

    private void SendSMSNotification(Utility.Notifications.NotificationTypes NotificationType, string Description, string SMSMobileNo, string SMSMeId)
    {

        if (String.IsNullOrWhiteSpace(SMSMobileNo) == false)
        {

            DataTable dtRecivers = new DataTable();
            dtRecivers.Columns.Add("SMSMobileNo");
            dtRecivers.Columns.Add("SMSMeId");
            dtRecivers.Columns.Add("SMSUltId");
            dtRecivers.Columns.Add("Description");

            DataRow dr = dtRecivers.NewRow();
            dr["SMSMobileNo"] = SMSMobileNo;
            dr["SMSMeId"] = SMSMeId;
            dr["SMSUltId"] = ((int)TSP.DataManager.UserType.Member).ToString();
            dr["Description"] = Description;

            dtRecivers.Rows.Add(dr);
            dtRecivers.AcceptChanges();

            //send sms contain TempPass
            SendSMSNotification SMSNotifications = new SendSMSNotification(NotificationType);
            SMSNotifications.SendSMSNote(dtRecivers, Description);
        }
    }
    #endregion


}