using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

public partial class UserControl_CapacityUserControl : System.Web.UI.UserControl
{
    #region Properties
    private int _ProjectId = -2;
    public int ProjectId
    {
        get
        {
            return (_ProjectId);
        }
        set
        {
            _ProjectId = value;
        }
    }

    private int _PrjReqId = -2;
    public int PrjReqId
    {
        get
        {
            return (_PrjReqId);
        }
        set
        {
            _PrjReqId = value;
        }
    }

    private string _ReportMemberWageByCityMeId = "-2";
    public string ReportMemberWageByCityMeId
    {
        get
        {
            return (_ReportMemberWageByCityMeId);
        }
        set
        {
            _ReportMemberWageByCityMeId = value;
        }
    }

    private string _ReportMemberWageByCityProjectIngridientTypeId = "-1";
    public string ReportMemberWageByCityProjectIngridientTypeId
    {
        get
        {
            return (_ReportMemberWageByCityProjectIngridientTypeId);
        }
        set
        {
            _ReportMemberWageByCityProjectIngridientTypeId = value;
        }
    }
    private TSP.DataManager.TSProjectIngridientType _ProjectIngridientTypeId;
    public TSP.DataManager.TSProjectIngridientType ProjectIngridientTypeId
    {
        get
        {
            return (_ProjectIngridientTypeId);
        }
        set
        {
            _ProjectIngridientTypeId = value;
        }
    }


    public string CapacityDecrement
    {
        get
        {
            return txtcCapacityDecrement.Text;
        }
        set
        {
            txtcCapacityDecrement.Text = value;
            //if (!string.IsNullOrEmpty(txtcDecrementPercent.Text) && !string.IsNullOrEmpty(value))
            //    txtcRealCapacityDecrement.Text = (Convert.ToInt32(value) * 100 / Convert.ToInt32(txtcDecrementPercent.Text)).ToString();
        }
    }

    public string CapacityWage
    {
        get
        {
            return txtcWage.Text;
        }
        set
        {
            txtcWage.Text = value;
            //if (!string.IsNullOrEmpty(txtcWagePercent.Text) && !string.IsNullOrEmpty(value))
            //    txtcRealWage.Text = (Convert.ToInt32(value) * 100 / Convert.ToInt32(txtcWagePercent.Text)).ToString();
        }

    }

    public Boolean CapacityDecrementEnable
    {
        get
        {
            return txtcRealCapacityDecrement.ClientEnabled;
        }
        set
        {
            txtcRealCapacityDecrement.ClientEnabled = value;
        }
    }

    public Boolean CapacityWageEnable
    {
        get
        {
            return txtcRealWage.ClientEnabled;
        }
        set
        {
            txtcRealWage.ClientEnabled = value;
        }

    }


    public Boolean txtcCapacityDecrementClientEnabled
    {
        get
        {
            return txtcCapacityDecrement.ClientEnabled;
        }
        set
        {
            txtcCapacityDecrement.ClientEnabled = value;
        }

    }


    public Boolean txtcWageClientEnabled
    {
        get
        {
            return txtcWage.ClientEnabled;
        }
        set
        {
            txtcWage.ClientEnabled = value;
        }

    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObjectDataSourceReportMemberWageByCity.SelectParameters["MeId"].DefaultValue = _ReportMemberWageByCityMeId;// "-2";
            ObjectDataSourceReportMemberWageByCity.SelectParameters["ProjectIngridientTypeId"].DefaultValue = _ReportMemberWageByCityProjectIngridientTypeId;// "-1";
            FillProjectInfo(_PrjReqId);
            SetRoundPanelHeader(_ProjectIngridientTypeId);
            txtSadraCapacityOBS.ClientVisible = lblSadraCapacityOBS.ClientVisible = lblSadraCapacity.ClientVisible = txtSadraCapacity.ClientVisible = false;

        }
    }

    #region private Methods
    private void FillProjectInfo(int ProjectReId)
    {
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        DataTable ProjectInfoDT = ProjectRequestManager.GetProjectInfo(ProjectReId);
        if (ProjectInfoDT.Rows.Count > 0)
        {
            txtcDecrementPercent.Text = ProjectInfoDT.Rows[0]["DecrementPercent"].ToString(); //متراژ کسر ظرفیت
            txtcWagePercent.Text = ProjectInfoDT.Rows[0]["WagePercent"].ToString();//متراژ دستمزد
            txtcFoundation.Text = ProjectInfoDT.Rows[0]["Foundation"].ToString();//متراژ کل پروژ     
            if (!string.IsNullOrEmpty(txtcCapacityDecrement.Text) && Convert.ToInt32(txtcDecrementPercent.Text) != 0)
                txtcRealCapacityDecrement.Text = (Convert.ToInt32(txtcCapacityDecrement.Text) * 100 / Convert.ToInt32(txtcDecrementPercent.Text)).ToString();
            if (!string.IsNullOrEmpty(txtcWage.Text))
                txtcRealWage.Text = (Convert.ToInt32(txtcWage.Text) * 100 / Convert.ToInt32(txtcWagePercent.Text)).ToString();
        }
    }

    private void SetRoundPanelHeader(TSP.DataManager.TSProjectIngridientType ProjectIngridientTypeId)
    {
        switch (ProjectIngridientTypeId)
        {
            case TSP.DataManager.TSProjectIngridientType.Designer:
                RoundPanelCapacity.InnerHtml = "ظرفیت پروژه - طراح";
                lblcRealCapacityDecrement.Text = "متراژ کارکرد طراح:";// "متراژ کسر ظرفیت طراح*";
                lblRealcWage.Text = "متراژ واقعی دستمزد طراح:*";
                lblcCapacityDecrement.Text = "متراژ کسر ظرفیت طراح:";
                lblcWage.Text = "متراژ دستمزد طراح:";
                //Text="متراژ کارکرد طراح" ID="lblcRealCapacityDecrement">
                //Text="متراژ واقعی دستمزد طراح" ID="lblRealcWage">
                //Text="متراژ کسر ظرفیت طراح:" ID="lblcCapacityDecrement">
                // Text="متراژ دستمزد طراح:" ID="lblcWage">
                break;
            case TSP.DataManager.TSProjectIngridientType.Implementer:
                RoundPanelCapacity.InnerHtml = "ظرفیت پروژه - مجری";
                lblcRealCapacityDecrement.Text = "متراژ کارکرد ظرفیت مجری*";
                lblRealcWage.Text = "متراژ واقعی دستمزد مجری*";
                lblcCapacityDecrement.Text = "متراژ کسر ظرفیت مجری:";
                lblcWage.Text = "متراژ دستمزد مجری:";
                break;
            case TSP.DataManager.TSProjectIngridientType.Observer:
                RoundPanelCapacity.InnerHtml = "ظرفیت پروژه - ناظر";
                lblcRealCapacityDecrement.Text = "متراژ کارکرد ظرفیت ناظر*";
                lblRealcWage.Text = "متراژ واقعی دستمزد ناظر*";
                lblcCapacityDecrement.Text = "متراژ کسر ظرفیت ناظر:";
                lblcWage.Text = "متراژ دستمزد ناظر:";
                break;
        }
    }
    /// <summary>
    /// پر کردن اطلاعات ظرفیت طراح بر اساس نوع طراح
    /// </summary>
    /// <param name="MemberTypeId"></param>
    /// <param name="OfficeEngoId"></param>
    private void FillDesignerCapacity(TSP.DataManager.TSMemberType MemberTypeId, int OfficeEngoId)
    {
        // ArrayList[0]: TotalCapacity(double), ArrayList[1]:UsedCapacity(double) , ArrayList[2]: RemainCapacity(double), ArrayList[3]:ReservedCapacity(double) , ArrayList[4]: ProjectNum(int)
        Capacity capacity = new Capacity();
        capacity.GetCapacityInformation((int)TSP.DataManager.TSProjectIngridientType.Designer, (int)MemberTypeId, OfficeEngoId, true);
        txtcTotalCapacity.Text = capacity.IngridientMaxJobCapacity.ToString();//ظرفیت کلarr[0]
        txtcRemainCapacity.Text = capacity.IngridientRemainCapacity.ToString();//ظرفیت باقیماندهarr[2]
        txtcTotalFunction.Text = capacity.IngridientUsedCapacityValue.ToString();//کل کارکرد    arr[1]        
        txtcProjectCount.Text = capacity.IngridientProjectNum.ToString();//تعداد پروژه arr[4]
        txtcReserve.Text = capacity.IngridientReservedCapacityValue.ToString();//کل رزرو شده arr[3]
        //Capacity capacity = new Capacity();
        //ArrayList arr = capacity.GetCapacityInformationPerStage((int)TSP.DataManager.TSProjectIngridientType.Designer, (int)MemberTypeId, OfficeEngoId);

        //if (arr.Count > 0)
        //{
        //    txtcTotalCapacity.Text = arr[0].ToString();//ظرفیت کل
        //    txtcRemainCapacity.Text = arr[2].ToString();//ظرفیت باقیمانده
        //    txtcTotalFunction.Text = arr[1].ToString();//کل کارکرد            
        //    txtcProjectCount.Text = arr[4].ToString();//تعداد پروژه
        //    txtcReserve.Text = arr[3].ToString();//کل رزرو شده
        //}
    }
    /// <summary>
    /// محاسبه اطلاعات ظرفیت عامل پروژه
    /// </summary>
    /// <param name="ProjectIngridientType"></param>
    /// <param name="MemberTypeId"></param>
    /// <param name="OfficeEngoId">MeOfficeEngoId</param>
    private void FillCapacity(int ProjectIngridientType, TSP.DataManager.TSMemberType MemberTypeId, int OfficeEngoId)
    {
        if (MemberTypeId == TSP.DataManager.TSMemberType.Member)
        {
            CapacityCalculations CapacityCalculations = new CapacityCalculations();
            UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrement = CapacityCalculations.UsedCapacity(-1, -1, OfficeEngoId, MemberTypeId, false, -1, new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager(), -1, (int)TSP.DataManager.TSDiscountPercent.BonyadMaskan);
            CapacityCalculations.MemberCapacityInWorkRequestTable MemberCapacityInWorkRequestTable = CapacityCalculations.GetCapacityInformationBasedOnWorkRequestTable(OfficeEngoId);
            txtcTotalFunction.Text = UsedCapacityProjectCapacityDecrement.UsedCapacitySumCapacityDecrement.ToString();
            txtcProjectCount.Text = UsedCapacityProjectCapacityDecrement.UsedCapacityCountProject.ToString();
            txtcTotalCapacity.Text = MemberCapacityInWorkRequestTable.TotalCapacity.ToString();
            txtcRemainCapacity.Text = MemberCapacityInWorkRequestTable.RemainCapacity.ToString();
            //کل رزرو شده-نظارت و طراحی با ثبت در سیستم کسر ظرفیت انجام می شود
            txtcReserve.ClientVisible =
            LabeReserve.ClientVisible = false;
            ////////
        }
    }

    private void FillCapacityByOfficeOREngOfficeMemberId(int MeId, int EngOffId, TSP.DataManager.TSMemberType MemberTypeId)
    {
        // int OffCode = -1;
        int DocOffIncreaseJobCapacityType = -1;
        switch (MemberTypeId)
        {
            case TSP.DataManager.TSMemberType.Office:
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office;
                break;

            case TSP.DataManager.TSMemberType.EngOffice:
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice;
                break;
        }


        /////*******پر کردن اطلاعات ظرفیت اعضای دفتر-شرکت*******
        // int OfficeEngoId = OffCode;
        Capacity capacity = new Capacity();
        Capacity.OfficeCapacity OfficeCapacity = capacity.GetOfficeDsgCapacity(EngOffId, (int)TSP.DataManager.TSProjectIngridientType.Designer, DocOffIncreaseJobCapacityType, true, MeId);
        //  txtMeTotalCapacity.Text 
        txtcTotalCapacity.Text = OfficeCapacity.OfficeMemberMaxJobCapacity.ToString();
        // txtMeUsedCapacity.Text = OfficeCapacity.OfficeMemberUsedCapacity.ToString();
        //txtMeProjectCount.Text =
        txtcProjectCount.Text = OfficeCapacity.OfficeMemberCurrentProjectNum.ToString();
        //txtMeReserved.Text =
        txtcReserve.Text = OfficeCapacity.OfficeMemberReservedCapacity.ToString();
        //txtMeRemainCapacity.Text
        txtcRemainCapacity.Text = OfficeCapacity.OfficeMemberRemainCapacity.ToString();
        txtcTotalFunction.Text = OfficeCapacity.OfficeMemberUsedCapacityDesign.ToString();
    }
    #endregion

    #region FillProjectIngridienCapacityInfo 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="TSMemberTypeId"></param>
    /// <param name="ProjectIngridientId"></param>
    /// <param name="OfficeEngOfMemberId"></param>
    public void FillProjectIngridienCapacityInfo(TSP.DataManager.TSMemberType TSMemberTypeId, int ProjectIngridientId, int OfficeEngOfMemberId)
    {
        switch (_ProjectIngridientTypeId)
        {
            case TSP.DataManager.TSProjectIngridientType.Designer:
                //  int MeId = -2;
                ObjectDataSourceReportMemberWageByCity.SelectParameters["ProjectIngridientTypeId"].DefaultValue = "-1";
                ObjectDataSourceReportMemberWageByCity.SelectParameters["MeId"].DefaultValue = "-2";


                FillCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, TSMemberTypeId, OfficeEngOfMemberId);
                _ReportMemberWageByCityMeId = OfficeEngOfMemberId.ToString();
                //  MeId = ProjectIngridientId;
                if (OfficeEngOfMemberId != -2)
                {
                    ObjectDataSourceReportMemberWageByCity.SelectParameters["MeId"].DefaultValue = OfficeEngOfMemberId.ToString();
                }

                break;
            case TSP.DataManager.TSProjectIngridientType.Observer:
                ObjectDataSourceReportMemberWageByCity.SelectParameters["ProjectIngridientTypeId"].DefaultValue = "-1";
                ObjectDataSourceReportMemberWageByCity.SelectParameters["MeId"].DefaultValue = _ReportMemberWageByCityMeId = ProjectIngridientId.ToString();

                FillCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, TSMemberTypeId, ProjectIngridientId);
                break;
            case TSP.DataManager.TSProjectIngridientType.Implementer:
                ObjectDataSourceReportMemberWageByCity.SelectParameters["ProjectIngridientTypeId"].DefaultValue = "-1";
                FillCapacity((int)TSP.DataManager.TSProjectIngridientType.Implementer, TSMemberTypeId, ProjectIngridientId);
                break;
        }
        GridViewProject.DataBind();
    }
    /// <summary>
    /// موارد استفاده: صفحه ثبت ناظر
    /// </summary>
    /// <param name="TSMemberTypeId"></param>
    /// <param name="ProjectIngridientId"></param>
    public void FillProjectIngridienCapacityInfo(TSP.DataManager.TSMemberType TSMemberTypeId, int ProjectIngridientId)
    {
        FillProjectIngridienCapacityInfo(TSMemberTypeId, ProjectIngridientId, -1);
    }
    #endregion
    #region ClearForm
    public void ClearControlsProjectInfo()
    {
        txtcDecrementPercent.Text = "0";
        txtcWagePercent.Text = "0";
        txtcFoundation.Text = "0";
    }

    public void ClearControlsIngridienCapacityInfo()
    {
        ObjectDataSourceReportMemberWageByCity.SelectParameters["MeId"].DefaultValue = _ReportMemberWageByCityMeId = "-2";
        ObjectDataSourceReportMemberWageByCity.SelectParameters["ProjectIngridientTypeId"].DefaultValue = _ReportMemberWageByCityProjectIngridientTypeId = "-1";
        txtcCapacityDecrement.Text = "";
        txtcProjectCount.Text = "---";
        txtcRemainCapacity.Text = "---";
        txtcReserve.Text = "---";
        txtcTotalCapacity.Text = "---";
        txtcTotalFunction.Text = "---";
        txtcWage.Text = "";
        txtcRealWage.Text = "";
        txtcRealCapacityDecrement.Text = "";
    }
    #endregion
}