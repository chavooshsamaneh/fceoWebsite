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

public partial class UserControl_ProjectInfoUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private int _CitId;
    public int CitId
    {
        get { return _CitId; }
    }
    private string _CitName;
    public string CitName
    {
        get { return _CitName; }
    }
    private int _DecrementPercent;
    public int DecrementPercent
    {
        get { return _DecrementPercent; }
    }
    private int _WagePercent;
    public int WagePercent
    {
        get { return _WagePercent; }
    }
    private int _Foundation;
    public int Foundation
    {
        get { return _Foundation; }
    }
    private int _FoundationMixSkeletonSaze;
    public int FoundationMixSkeletonSaze
    {
        get { return _FoundationMixSkeletonSaze; }
    }
    private int _GroupId;
    public int GroupId
    {
        get { return _GroupId; }
    }
    private int _StructureSkeletonId;
    public int StructureSkeletonId
    {
        get { return _StructureSkeletonId; }
    }
    private Int16 _IsCharity;
    public Int16 IsCharity
    {
        get { return _IsCharity; }
    }
    private Int16 _IsBonyadMaskan;
    public Int16 IsBonyadMaskan
    {
        get { return _IsBonyadMaskan; }
    }
    private Boolean _IsShahrakSanaati;
    public Boolean IsShahrakSanaati
    {
        get { return _IsShahrakSanaati; }
    }
    private Boolean _IsEghdamMeliMaskan;
    public Boolean IsEghdamMeliMaskan
    {
        get { return _IsEghdamMeliMaskan; }
    }

    private int _AgentId;
    public int AgentId
    {
        get { return _AgentId; }
    }
    private string _AgentCode;
    public string AgentCode
    {
        get { return _AgentCode; }
    }
    private string _AgentCodeForPaymentIdProvince;
    public string AgentCodeForPaymentIdProvince
    {
        get { return _AgentCodeForPaymentIdProvince; }
    }
    private int _OwnerMeId;
    public int OwnerMeId
    {
        get { return _OwnerMeId; }
    }
    private int _OwnerId;
    public int OwnerId
    {
        get { return _OwnerId; }
    }
    private string _OwnerName;
    public string OwnerName
    {
        get { return _OwnerName; }
    }
    private string _OwnerMobileNo;
    public string OwnerMobileNo
    {
        get { return _OwnerMobileNo; }
    }
    private int _DiscountPercentId;
    public int DiscountPercentId
    {
        get { return _DiscountPercentId; }
    }
    private int _FundationDifference;
    public int FundationDifference
    {
        get { return _FundationDifference; }
    }
    private int _PreviousGroupId;
    public int PreviousGroupId
    {
        get { return _PreviousGroupId; }
    }

    private int _MunId;
    public int MunId
    {
        get { return _MunId; }
    }

    private string _MunName;
    public string MunName
    {
        get { return _MunName; }
    }
    private int _CurrentTaskCode;
    public int CurrentTaskCode
    {
        get { return _CurrentTaskCode; }
    }
    private Boolean _IsPopulationUnder25000;
    public Boolean IsPopulationUnder25000
    {
        get { return _IsPopulationUnder25000; }
    }
    private Boolean _CanEditProjectInfoInThisRequest;
    public Boolean CanEditProjectInfoInThisRequest
    {
        get { return _CanEditProjectInfoInThisRequest; }
    }
    private int _ProjectStatusId;
    public int ProjectStatusId
    {
        get { return _ProjectStatusId; }
    }
    private int _PrjReTypeId;
    public int PrjReTypeId
    {
        get { return _PrjReTypeId; }
    }

    public void Fill(int ProjectReId)
    {
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        DataTable ProjectInfoDT = ProjectRequestManager.GetProjectInfo(ProjectReId);
        if (ProjectInfoDT.Rows.Count > 0)
        {
            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["TraceCode"]))
                txtTraceCode.Text = ProjectInfoDT.Rows[0]["TraceCode"].ToString();
            _AgentId = Convert.ToInt32(ProjectInfoDT.Rows[0]["AgentId"].ToString());

            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["AgentCode"]))
                _AgentCode = ProjectInfoDT.Rows[0]["AgentCode"].ToString();

            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["AgentCodeForPaymentIdProvince"]))
                _AgentCodeForPaymentIdProvince = ProjectInfoDT.Rows[0]["AgentCodeForPaymentIdProvince"].ToString();

            _CitId = Convert.ToInt32(ProjectInfoDT.Rows[0]["CitId"].ToString());

            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["IsPopulationUnder25000"]))
                _IsPopulationUnder25000 = Convert.ToBoolean(ProjectInfoDT.Rows[0]["IsPopulationUnder25000"]);
            if (_IsPopulationUnder25000)
                lblCityPopulation.Text = "کمتر از 25000 نفر می باشد";
            else
                lblCityPopulation.Text = "بیشتر از 25000 نفر می باشد";


            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["ProjectId"]))
                txtPrProjectId.Text = ProjectInfoDT.Rows[0]["ProjectId"].ToString();

            txtPrProjectName.Text = ProjectInfoDT.Rows[0]["ProjectName"].ToString();
            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["CitName"]))
                _CitName = txtPrCitName.Text = ProjectInfoDT.Rows[0]["CitName"].ToString();
            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["FileNo"]))
                txtPrFileNo.Text = ProjectInfoDT.Rows[0]["FileNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["MunName"]))
                _MunName = txtPrMunName.Text = ProjectInfoDT.Rows[0]["MunName"].ToString();
            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["RegisteredNo"]))
                txtPrRegisteredNo.Text = ProjectInfoDT.Rows[0]["RegisteredNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["LicenseNo"]))
                txtPrLicenceNo.Text = ProjectInfoDT.Rows[0]["LicenseNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["GroupName"]))
                txtPrGroup.Text = ProjectInfoDT.Rows[0]["GroupName"].ToString();
            txtPrStructure.Text = ProjectInfoDT.Rows[0]["StructureSkeleton"].ToString();

            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["OwnerName"]))
                _OwnerName = txtPrOwnerName.Text = ProjectInfoDT.Rows[0]["OwnerName"].ToString();
            else
                _OwnerName = "";
            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["OwnerMeId"]))
                _OwnerMeId = Convert.ToInt32(ProjectInfoDT.Rows[0]["OwnerMeId"]);

            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["OwnerId"]))
                _OwnerId = Convert.ToInt32(ProjectInfoDT.Rows[0]["OwnerId"]);

            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["OwnerMobileNo"]))
                _OwnerMobileNo = ProjectInfoDT.Rows[0]["OwnerMobileNo"].ToString();
            else _OwnerMobileNo = "";


            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["DiscountPercentTitle"]))
                txtDiscountPercentTitle.Text = ProjectInfoDT.Rows[0]["DiscountPercentTitle"].ToString();
            _DecrementPercent = Convert.ToInt32(ProjectInfoDT.Rows[0]["DecrementPercent"].ToString());
            _WagePercent = Convert.ToInt32(ProjectInfoDT.Rows[0]["WagePercent"].ToString());
            txtFoundation.Text = ProjectInfoDT.Rows[0]["Foundation"].ToString();
            _Foundation = Convert.ToInt32(ProjectInfoDT.Rows[0]["Foundation"].ToString());

            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["FoundationMixSkeleton"]))
                _FoundationMixSkeletonSaze = Convert.ToInt32(ProjectInfoDT.Rows[0]["FoundationMixSkeleton"].ToString());

            _GroupId = Convert.ToInt32(ProjectInfoDT.Rows[0]["GroupId"]);
            _MunId = Convert.ToInt32(ProjectInfoDT.Rows[0]["MunId"]);
            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["MaxStageNum"]))
                txtStage.Text = ProjectInfoDT.Rows[0]["MaxStageNum"].ToString();
            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["StructureSkeletonId"]))
                _StructureSkeletonId = Convert.ToInt32(ProjectInfoDT.Rows[0]["StructureSkeletonId"]);


            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["DiscountPercentCode"]) && Convert.ToInt32(ProjectInfoDT.Rows[0]["DiscountPercentCode"]) == 3)
                _IsCharity = 1;
            else
                _IsCharity = 0;



            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["DiscountPercentCode"]) && Convert.ToInt32(ProjectInfoDT.Rows[0]["DiscountPercentCode"]) == (int)TSP.DataManager.TSDiscountPercent.BonyadMaskan)
                _IsBonyadMaskan = 1;
            else
                _IsBonyadMaskan = 0;


            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["DiscountPercentCode"]) && Convert.ToInt32(ProjectInfoDT.Rows[0]["DiscountPercentCode"]) == (int)TSP.DataManager.TSDiscountPercent.Industrial)
                _IsShahrakSanaati = true;
            else
                _IsShahrakSanaati = false;

            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["DiscountPercentCode"]) && Convert.ToInt32(ProjectInfoDT.Rows[0]["DiscountPercentCode"]) == (int)TSP.DataManager.TSDiscountPercent.EghdamMeliMaskan)
                _IsEghdamMeliMaskan = true;
            else
                _IsEghdamMeliMaskan = false;

            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["ArchiveNo"]))
                txtarchiveNo.Text = ProjectInfoDT.Rows[0]["ArchiveNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["TaskName"]))
                lblWorkFlowState.Text = "وضعیت درخواست ثبت پروژه: " + ProjectInfoDT.Rows[0]["TaskName"].ToString();
            else
                lblWorkFlowState.Text = "وضعیت درخواست ثبت پروژه: " + "نامشخص";


            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["TaskCode"]))
                _CurrentTaskCode = Convert.ToInt32(ProjectInfoDT.Rows[0]["TaskCode"]);

            if (!Utility.IsDBNullOrNullValue(ProjectInfoDT.Rows[0]["DiscountPercentId"]))
                _DiscountPercentId = Convert.ToInt32(ProjectInfoDT.Rows[0]["DiscountPercentId"]);
            _CanEditProjectInfoInThisRequest = true;
            _ProjectStatusId = Convert.ToInt32(ProjectInfoDT.Rows[0]["PrjReqProjectStatusId"]);
            lblProjectStatusName.Text = ProjectInfoDT.Rows[0]["ProjectStatus"].ToString();
            _PrjReTypeId = Convert.ToInt32(ProjectInfoDT.Rows[0]["PrjReTypeId"]);
            lblPrjReTypeName.Text = ProjectInfoDT.Rows[0]["PrjReTypeTittle"].ToString();
            switch (Convert.ToInt32(ProjectInfoDT.Rows[0]["PrjReTypeId"]))
            {
                case (int)TSP.DataManager.TSProjectRequestType.EndProjectCertificateRequest:
                    _CanEditProjectInfoInThisRequest = false;
                    break;
                case (int)TSP.DataManager.TSProjectRequestType.BuildingNotStarted:
                    _CanEditProjectInfoInThisRequest = false;
                    break;
                case (int)TSP.DataManager.TSProjectRequestType.BuildingsLicenseConfirming:
                    _CanEditProjectInfoInThisRequest = false;
                    break;
                case (int)TSP.DataManager.TSProjectRequestType.ChangeProject:
                    //*در درخواست تغییرات تنها در صورتی که وضعیت پروژه عدم شروع به ساخت و ساز باشد امکان ثبت ناظر و ثبت فیش وجود دارد و از قوانین 
                    _CanEditProjectInfoInThisRequest = false;
                    if(_ProjectStatusId==(int)(int)TSP.DataManager. TSProjectStatus.BuildingNotStarted)
                        _CanEditProjectInfoInThisRequest = true;
                    break;
                default:
                    break;
            }

            _FundationDifference = 0; _PreviousGroupId = _GroupId;
            DataTable dtPreRrjRest = ProjectRequestManager.SelectPreviousProjectRequestStageAndFoundation(Convert.ToInt32(ProjectInfoDT.Rows[0]["ProjectId"]), ProjectReId);//??, (int)TSP.DataManager.TSProjectStatus.End);
            if (dtPreRrjRest.Rows.Count == 1)
            {
                int PreFoundation = Convert.ToInt32(dtPreRrjRest.Rows[0]["Foundation"]);
                int PreMaxStageNum = Convert.ToInt32(dtPreRrjRest.Rows[0]["MaxStageNum"]);
                _FundationDifference = _Foundation - PreFoundation;
                _PreviousGroupId = Convert.ToInt32(dtPreRrjRest.Rows[0]["GroupId"]);
            }
            txtFundationDifference.Text = _FundationDifference.ToString();
            TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
            txtCountProjectObserver.Text = ProjectObserversManager.SelectTSProjectObserverCountForProjectUserControl(Convert.ToInt32(ProjectInfoDT.Rows[0]["ProjectId"])).ToString();

        }
    }

}
