using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class UserControl_MemberCapacityUserControl : System.Web.UI.UserControl
{
    #region Properties
    private int _ProjectIngridientId = -2;
    private int ProjectIngridientId
    {
        get
        {
            return (_ProjectIngridientId);
        }
        set
        {
            _ProjectIngridientId = value;
        }
    }


    //private TSP.DataManager.TSMemberType _TSMemberTypeId;
    //private TSP.DataManager.TSMemberType TSMemberTypeId
    //{
    //    get
    //    {
    //        return (_TSMemberTypeId);
    //    }
    //    set
    //    {
    //        _TSMemberTypeId = value;
    //    }
    //}

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



    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObjectDataSourceReportMemberWageByCity.SelectParameters["MeId"].DefaultValue = "-2";
            ObjectDataSourceReportMemberWageByCity.SelectParameters["ProjectIngridientTypeId"].DefaultValue = "-1";
            SetControls(_ProjectIngridientTypeId);
        }
    }

    private void SetControls(TSP.DataManager.TSProjectIngridientType ProjectIngridientTypeId)
    {
        switch (ProjectIngridientTypeId)
        {
            case TSP.DataManager.TSProjectIngridientType.Designer:
                RoundPanelCapacityHeader.InnerText = "ظرفیت طراحی - نظارت";
                break;
            case TSP.DataManager.TSProjectIngridientType.Observer:
                RoundPanelCapacityHeader.InnerText = "ظرفیت طراحی - نظارت";

                break;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="MeId"></param>
    /// <param name="IsDesignAndObserve">1 : If Fiding Design or Observe Capacity,0: If Finding Implement Capacity</param>
    public void FillMemberCapacityInfo(int MeId, int IsDesignAndObserve)
    {
        ObjectDataSourceReportMemberWageByCity.SelectParameters["MeId"].DefaultValue = MeId.ToString();
        if (IsDesignAndObserve == 1)
        {
            ObjectDataSourceReportMemberWageByCity.SelectParameters["ProjectIngridientTypeId"].DefaultValue = "-1";
            FillMemberCapacityInfo(MeId);
        }
        //else
        //{
        //    ObjectDataSourceReportMemberWageByCity.SelectParameters["ProjectIngridientTypeId"].DefaultValue = ((int)TSP.DataManager.TSProjectIngridientType.Implementer).ToString();
        //    FillMemberImplementByOfficeEngOffMember(MeId);
        //}
    }

    private void FillMemberCapacityInfo(int MeId)
    {
        try
        {

            TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
            UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementObs = ProjectCapacityDecrementManager.FindSumUsedCapacity(MeId, (int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSMemberType.Member, -1, "", "", 0, 1, -1, -1,-1, (int)TSP.DataManager. TSDiscountPercent.BonyadMaskan);
            UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementDes = ProjectCapacityDecrementManager.FindSumUsedCapacity(MeId, (int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSMemberType.Member, -1, "", "", 0, 1, -1, -1,-1, (int)TSP.DataManager.TSDiscountPercent.BonyadMaskan);
            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager TSObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
            System.Data.DataTable dtMeWorkRequest = TSObserverWorkRequestManager.SelectTSObserverWorkRequestFullInfoByMember(MeId, TSP.DataManager.TSObserverWorkRequestStatus.All);
            if (dtMeWorkRequest.Rows.Count == 0)
                return;
            int MaxObs = Convert.ToInt32(dtMeWorkRequest.Rows[0]["CapacityObs"]);
            int MaxDes = Convert.ToInt32(dtMeWorkRequest.Rows[0]["CapacityDesign"]);
            txtRemainCapacity.Text = dtMeWorkRequest.Rows[0]["RemainCapacity"].ToString();         

          
            txtRemainCapacityObsReal.Text = dtMeWorkRequest.Rows[0]["RemainCapacityObsReal"].ToString();
            txtMaxJobCount.Text = dtMeWorkRequest.Rows[0]["CountWorks"].ToString();
            txtCountRemainWorkCount.Text = dtMeWorkRequest.Rows[0]["CountRemainWorkCount"].ToString();
            txtCountUnder400MeterWorkDesign.Text = dtMeWorkRequest.Rows[0]["CountUnder400MeterWorkDesign"].ToString();
            txtCountUnder400MeterWork.Text = dtMeWorkRequest.Rows[0]["CountUnder400MeterWork"].ToString();
            txtConditionalCapacityDesign.Text = CapacityCalculations.GetConditionalCapacity(MeId, (int)TSP.DataManager.TSProjectIngridientType.Designer).ToString();
            txtConditionalCapacityObservation.Text = CapacityCalculations.GetConditionalCapacity(MeId, (int)TSP.DataManager.TSProjectIngridientType.Observer).ToString();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }

}