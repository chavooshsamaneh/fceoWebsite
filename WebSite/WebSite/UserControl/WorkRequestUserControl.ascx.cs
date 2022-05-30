using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_WorkRequestUserControl : System.Web.UI.UserControl
{
    #region Properties
    private string _FileNo = "";
    public string FileNo
    {
        get
        {
            return (_FileNo);
        }
        set
        {
            _FileNo = value;
        }
    }
    private string _MeArchitectorCode = "";
    public string MeArchitectorCode
    {
        get
        {
            return (_MeArchitectorCode);
        }
        set
        {
            _MeArchitectorCode = value;
        }
    }
    private string _ErrorMessage = "";
    public string ErrorMessage
    {
        get
        {
            return (_ErrorMessage);
        }
        set
        {
            _ErrorMessage = value;
        }
    }


    private string _AlertMessage = "";
    public string AlertMessage
    {
        get
        {
            return (_AlertMessage);
        }
        set
        {
            _AlertMessage = value;
        }
    }

    private int _MeId = -2;
    public int MeId
    {
        get
        {
            return (_MeId);
        }
        set
        {
            _MeId = value;
        }
    }

    private int _MajorParentIdInWorkReq = -2;
    public int MajorParentIdInWorkReq
    {
        get
        {
            return (_MajorParentIdInWorkReq);
        }
        set
        {
            _MajorParentIdInWorkReq = value;
        }
    }
    private Boolean _UserControlvisible = false;
    public Boolean UserControlvisible
    {
        get
        {
            return (_UserControlvisible);
        }
        set
        {
            _UserControlvisible = value;
        }
    }

    private int _MfId = -2;
    public int MfId
    {
        get
        {
            return (_MfId);
        }
        set
        {
            _MfId = value;
        }
    }
    private int _AgentId = -2;
    public int AgentId
    {
        get
        {
            return (_AgentId);
        }
        set
        {
            _AgentId = value;
        }
    }
    private int _CurrentObsId = -2;
    public int CurrentObsId
    {
        get
        {
            return (_CurrentObsId);
        }
        set
        {
            _CurrentObsId = value;
        }
    }
    private int _CurrentDesId = -2;
    public int CurrentDesId
    {
        get
        {
            return (_CurrentDesId);
        }
        set
        {
            _CurrentDesId = value;
        }
    }
    private int _CurrentMappingId = -2;
    public int CurrentMappingId
    {
        get
        {
            return (_CurrentMappingId);
        }
        set
        {
            _CurrentMappingId = value;
        }
    }

    private int _CurrentUrbanismId = -2;
    public int UrbanismId
    {
        get
        {
            return (_CurrentUrbanismId);
        }
        set
        {
            _CurrentUrbanismId = value;
        }
    }

    private int _WantedWorkType = -2;
    public int WantedWorkType
    {
        get
        {
            return (_WantedWorkType);
        }
        set
        {
            _WantedWorkType = value;
        }
    }

    private Boolean _IsCivilObserver = false;
    public Boolean IsCiviObserver
    {
        get
        {
            return (_IsCivilObserver);
        }
        set
        {
            _IsCivilObserver = value;
        }
    }

    private Boolean _NeedCheckConditions = true;
    public Boolean NeedCheckConditions
    {
        get
        {
            return (_NeedCheckConditions);
        }
        set
        {
            _NeedCheckConditions = value;
        }
    }

    private int _MeOfOthId = -2;
    public int MeOfOthId
    {
        get
        {
            return _MeOfOthId;
        }
        set
        {
            _MeOfOthId = value;
        }
    }
    #endregion
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

    private Boolean _ShowAlert = false;
    public Boolean ShowAlert
    {
        get
        {
            return (_ShowAlert);
        }
        set
        {
            _ShowAlert = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _IsCivilObserver = false;
            _ErrorMessage = "";
            PanelMainWorkReqUserControl.Visible = _UserControlvisible;
            CheckBoxIsFullTimeWorker.ReadOnly = CheckListStructureGroups.ReadOnly = CheckBoxWantCharity.ReadOnly = CheckBoxWantShahrakSanati.ReadOnly = true;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="MeId">For Other Persons MeId=OtpCode
    /// OtherPersons کاردان ها می باشند</param>
    /// <param name="Type"></param>
    public Boolean FillForm(string MeId, TSP.DataManager.TSMemberType TSMemberType)
    {
        ClearForm();
        Boolean Result = true;
        switch (TSMemberType)
        {
            case TSP.DataManager.TSMemberType.Member:
                Result = FillMember(Convert.ToInt32(MeId));
                break;
            case TSP.DataManager.TSMemberType.ExpArchitect:
                Result = FillExpArchitect(MeId);
                RoundPanelCity.ClientVisible = RoundPanelPrjTypes.ClientVisible = RoundPanelBasicCapacityInfo.ClientVisible =
                    RoundPanelObserveCapacity.ClientVisible = RoundPanelDesignCapacity.ClientVisible = RoundPanelUrbenismCapacity.ClientVisible = false;
                break;
            case TSP.DataManager.TSMemberType.OtherPerson:
                Result = FillOtherPerson(MeId);
                RoundPanelCity.ClientVisible = RoundPanelPrjTypes.ClientVisible = RoundPanelBasicCapacityInfo.ClientVisible =
                    RoundPanelObserveCapacity.ClientVisible = RoundPanelDesignCapacity.ClientVisible = RoundPanelUrbenismCapacity.ClientVisible = false;
                break;
        }
        return Result;
    }
    private Boolean FillMember(int MeId)
    {
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        MeManager.FindByCode(MeId);
        #region CheckMemberConitions
        if (MeManager.Count != 1)
        {
            ShowMessage("چنین کد عضویتی وجود ندارد.مجدداً وارد نمایید");
            return false;
        }
        if (_ProjectIngridientTypeId == 0)
        {
            if (Utility.GetCurrentUser_AgentId() != Utility.GetCurrentAgentCode() && Convert.ToInt32(MeManager[0]["AgentId"]) != Utility.GetCurrentUser_AgentId())
            {
                ShowMessage("شما تنها قادر به جستجوی اعضای نمایندگی خود می باشید. " + (Utility.IsDBNullOrNullValue(MeManager[0]["AgentName"]) ? "نمایندگی عضو انتخاب شده" + MeManager[0]["AgentName"].ToString() + "می باشد" : ""));
                return false;
            }
        }
        if (_ProjectIngridientTypeId == TSP.DataManager.TSProjectIngridientType.Observer)
        {
            if (Utility.GetCurrentUser_AgentId() != Utility.GetCurrentAgentCode() && Convert.ToInt32(MeManager[0]["AgentId"]) != Utility.GetCurrentUser_AgentId())
            {
                ShowMessage("شما تنها قادر به ثبت ناظرین نمایندگی خود می باشید. " + (Utility.IsDBNullOrNullValue(MeManager[0]["AgentName"]) ? "نمایندگی ناظر انتخاب شده" + MeManager[0]["AgentName"].ToString() + "می باشد" : ""));
                return false;
            }
        }
        string Msg = "";
        if (_ProjectIngridientTypeId != 0 && !TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg) && _NeedCheckConditions)
        {
            ShowMessage(Msg);
            return false;
        }
        #endregion
        #region Set MemberBasicInfo
        if (!Utility.IsDBNullOrNullValue(MeManager[0]["AgentId"]))
        {
            _AgentId = Convert.ToInt32(MeManager[0]["AgentId"]);
            SetLabaleVisibilityBasedonAgent();
        }
        if (!Utility.IsDBNullOrNullValue(MeManager[0]["AgentName"]))
        {
            txtAgent.Text = MeManager[0]["AgentName"].ToString();
        }

        if (!Utility.IsDBNullOrNullValue(MeManager[0]["FatherName"]))
            txtFatherName.Text = MeManager[0]["FatherName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MeManager[0]["FirstName"]))
            txtFirstName.Text = MeManager[0]["FirstName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MeManager[0]["LastName"]))
            txtLastName.Text = MeManager[0]["LastName"].ToString();
        if (!Utility.IsDBNullOrNullValue(MeManager[0]["SSN"]))
            txtSSN.Text = MeManager[0]["SSN"].ToString();
        if (!Utility.IsDBNullOrNullValue(MeManager[0]["ArchitectorCode"]))
            _MeArchitectorCode = MeManager[0]["ArchitectorCode"].ToString();
        if (!Utility.IsDBNullOrNullValue(MeManager[0]["FileNo"]))
            txtFileNo.Text = _FileNo = MeManager[0]["FileNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(MeManager[0]["ObsId"]) && Convert.ToInt32(MeManager[0]["ObsId"]) != -1)
        {
            txtObsId.Text = MeManager[0]["ObsGrdName"].ToString();
            _CurrentObsId = Convert.ToInt32(MeManager[0]["ObsId"]);
        }
        else
            txtObsId.Text = "---";

        if (!Utility.IsDBNullOrNullValue(MeManager[0]["MappingId"]) && Convert.ToInt32(MeManager[0]["MappingId"]) != -1)
        {
            txtMapping.Text = MeManager[0]["MappingGrdName"].ToString();
            _CurrentMappingId = Convert.ToInt32(MeManager[0]["MappingId"]);
        }
        else
            txtMapping.Text = "---";

        if (!Utility.IsDBNullOrNullValue(MeManager[0]["DesId"]) && Convert.ToInt32(MeManager[0]["DesId"]) != -1)
        {
            txtDesign.Text = MeManager[0]["DesGrdName"].ToString();
            _CurrentDesId = Convert.ToInt32(MeManager[0]["DesId"]);
        }
        else
            txtDesign.Text = "---";

        if (!Utility.IsDBNullOrNullValue(MeManager[0]["UrbanismId"]) && Convert.ToInt32(MeManager[0]["UrbanismId"]) != -1)
        {
            txtUrbenism.Text = MeManager[0]["UrbanismGrdName"].ToString();
            _CurrentUrbanismId = Convert.ToInt32(MeManager[0]["UrbanismId"]);
        }
        else
            txtUrbenism.Text = "---";

        #region عضو دفتر گاز
        TSP.DataManager.DocGasOfficeMembersManager DocGasOfficeMembersManager = new TSP.DataManager.DocGasOfficeMembersManager();
        DocGasOfficeMembersManager.FindByMeId(MeId, (int)TSP.DataManager.GasOfficeMemberStatus.Confirmed);
        if (DocGasOfficeMembersManager.Count > 0)
        {
            lblHasGasCert.Text = "می باشد";
            lblHasGasCert.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lblHasGasCert.Text = "نمی باشد";
        }
        #endregion
        #endregion
        if (_NeedCheckConditions)
        {
            #region WorkRequestInfo
            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager TSObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
            System.Data.DataTable dtMeWorkRequest = TSObserverWorkRequestManager.SelectTSObserverWorkRequestFullInfoByMember(MeId, TSP.DataManager.TSObserverWorkRequestStatus.Confirm);
            if (dtMeWorkRequest.Rows.Count != 1)
            {
                ShowMessage("برای عضویت انتخاب شده آماده به کاری تایید شده یافت نشد");
                return false;
            }

            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["DocMeFileExpireDate"]))
                txtFileDate.Text = dtMeWorkRequest.Rows[0]["DocMeFileExpireDate"].ToString();
            if (Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["MasterMfMjParentId"]))
            {
                ShowMessage("اطلاعات مربوط به رشته انتخابی شخص در آماده بکاری ناقص می باشد.");
                return false;
            }
            _MajorParentIdInWorkReq = (int)dtMeWorkRequest.Rows[0]["MasterMfMjParentId"];
            if (_MajorParentIdInWorkReq != (int)(TSP.DataManager.MainMajors.Civil) && _MajorParentIdInWorkReq != (int)(TSP.DataManager.MainMajors.Architecture))
            {
                _IsCivilObserver = false;
            }
            else { _IsCivilObserver = true; }

            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["MjParentName"]))
                txtMemberFileMajor.Text = dtMeWorkRequest.Rows[0]["MjParentName"].ToString();
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["CitName1"]))
            {
                txtCity1.Text = dtMeWorkRequest.Rows[0]["CitName1"].ToString();
                SetLableVisibityBasedOnShirazCities(Convert.ToInt32(dtMeWorkRequest.Rows[0]["city1"]));
            }
            else
                txtCity1.Text = "---";
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["CitName2"]))
                txtCity2.Text = dtMeWorkRequest.Rows[0]["CitName2"].ToString();
            else
                txtCity2.Text = "---";
            CheckBoxWantCharity.Checked = Convert.ToBoolean(dtMeWorkRequest.Rows[0]["WantCharityWork"]);
            CheckListStructureGroups.DataBind();
            if (Convert.ToBoolean(dtMeWorkRequest.Rows[0]["Group1"]))
            {
                CheckListStructureGroups.Items.FindByValue("1").Selected = Convert.ToBoolean(dtMeWorkRequest.Rows[0]["Group1"]);
            }
            if (Convert.ToBoolean(dtMeWorkRequest.Rows[0]["Group2"]))
            {
                CheckListStructureGroups.Items.FindByValue("2").Selected = Convert.ToBoolean(dtMeWorkRequest.Rows[0]["Group2"]);
            }
            if (Convert.ToBoolean(dtMeWorkRequest.Rows[0]["Group3"]))
            {
                CheckListStructureGroups.Items.FindByValue("3").Selected = Convert.ToBoolean(dtMeWorkRequest.Rows[0]["Group3"]);
            }
            if (Convert.ToBoolean(dtMeWorkRequest.Rows[0]["Group4"]))
            {
                CheckListStructureGroups.Items.FindByValue("4").Selected = Convert.ToBoolean(dtMeWorkRequest.Rows[0]["Group4"]);
            }
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["BonyadMaskan"]))
                txtBonyadMaskan.Text = dtMeWorkRequest.Rows[0]["BonyadMaskan"].ToString();
            else
                txtBonyadMaskan.Text = "0";
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["ShirazMunicipalityMeter"]))
                txtObsShirazMunicipality.Text = dtMeWorkRequest.Rows[0]["ShirazMunicipalityMeter"].ToString();
            else
                txtObsShirazMunicipality.Text = "0";
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["ZarghanObserveMeter"]))
                txtZarghanObserveMeter.Text = dtMeWorkRequest.Rows[0]["ZarghanObserveMeter"].ToString();
            else
                txtZarghanObserveMeter.Text = "0";
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["LapooyObserveMeter"]))
                txtLapooyObserveMeter.Text = dtMeWorkRequest.Rows[0]["LapooyObserveMeter"].ToString();
            else
                txtLapooyObserveMeter.Text = "0";
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["KhanZenyanObserveMeter"]))
                txtKhanZenyanObserveMeter.Text = dtMeWorkRequest.Rows[0]["KhanZenyanObserveMeter"].ToString();
            else
                txtKhanZenyanObserveMeter.Text = "0";
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["DareyonObserveMeter"]))
                txtDareyonObserveMeter.Text = dtMeWorkRequest.Rows[0]["DareyonObserveMeter"].ToString();
            else
                txtDareyonObserveMeter.Text = "0";
            //***طراحی و شهرسازی هر دو در این قسمت وارد می شوند چون هر دو با هم وجود ندارند
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["ShirazMunicipalityDesignMeter"]))
                txtDesignShirazMunicipality.Text = dtMeWorkRequest.Rows[0]["ShirazMunicipalityDesignMeter"].ToString();
            else
                txtDesignShirazMunicipality.Text = "0";

            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["BonyadMaskanDesignMeter"]))
                txtDesignBonyadMaskan.Text = dtMeWorkRequest.Rows[0]["BonyadMaskanDesignMeter"].ToString();
            else
                txtDesignBonyadMaskan.Text = "0";

            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["ShirazMunicipulityUrbenismTarh"]))
                txtShirazMunicipulityUrbenismTarh.Text = dtMeWorkRequest.Rows[0]["ShirazMunicipulityUrbenismTarh"].ToString();
            else
                txtShirazMunicipulityUrbenismTarh.Text = "0";

            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["ShirazMunicipulityUrbenismEntebaghShahri"]))
                txtShirazMunicipulityUrbenismEntebaghShahri.Text = dtMeWorkRequest.Rows[0]["ShirazMunicipulityUrbenismEntebaghShahri"].ToString();
            else
                txtShirazMunicipulityUrbenismEntebaghShahri.Text = "0";

            CheckBoxIsFullTimeWorker.Checked = Convert.ToBoolean(dtMeWorkRequest.Rows[0]["IsFullTimeWorker"]);
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["WantedWorkType"]))
                _WantedWorkType = Convert.ToInt32(dtMeWorkRequest.Rows[0]["WantedWorkType"]);
            CheckBoxWantShahrakSanati.Checked = Convert.ToBoolean(dtMeWorkRequest.Rows[0]["WantShahrakSanatiMeter"]);
            _MfId = Convert.ToInt32(dtMeWorkRequest.Rows[0]["MfId"]);
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["CountWorks"]))
                lblMaxJobCount.Text = dtMeWorkRequest.Rows[0]["CountWorks"].ToString();
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["CapacityObs"]))
                lblMaxJobObsCapacity.Text = dtMeWorkRequest.Rows[0]["CapacityObs"].ToString();
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["CapacityDesign"]))
                lblMaxDesignCapacity.Text = dtMeWorkRequest.Rows[0]["CapacityDesign"].ToString();
            if (!Utility.IsDBNullOrNullValue(dtMeWorkRequest.Rows[0]["TotalCapacity"]))
                lblMaxTotalCapacity.Text = dtMeWorkRequest.Rows[0]["TotalCapacity"].ToString();
            #region Set Panel Based on Major
            RoundPanelCity.ClientVisible = RoundPanelObserveCapacity.ClientVisible = CheckListStructureGroups.ClientVisible =
            RoundPanelDesignCapacity.ClientVisible = RoundPanelUrbenismCapacity.ClientVisible = RoundPanelPrjTypes.ClientVisible = false;
            if (_MajorParentIdInWorkReq == (int)TSP.DataManager.MainMajors.Mapping)
            {
                lblDareyonObserveMeter.ClientVisible =
                lblZarghanObserveMeter.ClientVisible =
                lblLapooyObserveMeter.ClientVisible =
                lblKhanZenyanObserveMeter.ClientVisible =
                txtDareyonObserveMeter.ClientVisible =
                txtZarghanObserveMeter.ClientVisible =
                txtLapooyObserveMeter.ClientVisible =
                txtKhanZenyanObserveMeter.ClientVisible = lblBonyadMaskan.ClientVisible = txtBonyadMaskan.ClientVisible = false;
            }
            #region نظارت            
            if (_MajorParentIdInWorkReq != (int)TSP.DataManager.MainMajors.Urbanism && CurrentObsId != -2)
            {
                RoundPanelCity.ClientVisible = RoundPanelObserveCapacity.ClientVisible = RoundPanelPrjTypes.ClientVisible = CheckListStructureGroups.ClientVisible = true;
            }
            #endregion
            #region نقشه برداری
            if (_CurrentMappingId != -2)
            {
                RoundPanelCity.ClientVisible = RoundPanelObserveCapacity.ClientVisible = RoundPanelPrjTypes.ClientVisible = CheckListStructureGroups.ClientVisible = true;
            }
            #endregion
            #region طراحی
            if (_CurrentDesId != -2)
            {
                RoundPanelDesignCapacity.ClientVisible = RoundPanelPrjTypes.ClientVisible = true;
            }
            #endregion

            #region شهرسازی
            if (_MajorParentIdInWorkReq == (int)TSP.DataManager.MainMajors.Urbanism && _CurrentUrbanismId != -2)
            {
                TSP.DataManager.TechnicalServices.UrbanistQualificationManager UrbanistQualificationManager = new TSP.DataManager.TechnicalServices.UrbanistQualificationManager();

                UrbanistQualificationManager.FindByGrade(_CurrentUrbanismId, (int)TSP.DataManager.TSUrbanismQualificationType.SumAmadeSaziAraziAndTafkikAraziAndEntebaghKarbariArazi, 0);
                if (UrbanistQualificationManager.Count > 0)
                {
                    lblMaxJobUrbenismCapacityUrbenismTarh.Text = UrbanistQualificationManager[0]["QualificationMeter"].ToString();

                }
                UrbanistQualificationManager.FindByGrade(_CurrentUrbanismId, (int)TSP.DataManager.TSUrbanismQualificationType.EntebaghShahriSakhteman, 0);
                if (UrbanistQualificationManager.Count > 0)
                {
                    lblMaxJobUrbenismCapacityEntebaghShahri.Text = UrbanistQualificationManager[0]["QualificationMeter"].ToString();
                }
                RoundPanelUrbenismCapacity.ClientVisible = true;
                RoundPanelBasicCapacityInfo.ClientVisible = false;
                RoundPanelCity.ClientVisible = RoundPanelObserveCapacity.ClientVisible = RoundPanelPrjTypes.ClientVisible = CheckListStructureGroups.ClientVisible = false;
            }
            #endregion

            #endregion
            if (_ProjectIngridientTypeId == TSP.DataManager.TSProjectIngridientType.Observer)
            {
                // RoundPanelCity.ClientVisible = CheckListStructureGroups.ClientVisible = RoundPanelObserveCapacity.ClientVisible = true;
                RoundPanelDesignCapacity.ClientVisible = RoundPanelUrbenismCapacity.ClientVisible = false;
            }
            else if (_ProjectIngridientTypeId == TSP.DataManager.TSProjectIngridientType.Designer)
            {
                RoundPanelCity.ClientVisible = CheckListStructureGroups.ClientVisible = RoundPanelObserveCapacity.ClientVisible = false;
                //  RoundPanelDesignCapacity.ClientVisible = RoundPanelUrbenismCapacity.ClientVisible = true;
            }
            #endregion
        }
        else
        {
            #region tblMemberInfo
            if (!Utility.IsDBNullOrNullValue(MeManager[0]["FileDate"]))
                txtFileDate.Text = MeManager[0]["FileDate"].ToString();
            if (Utility.IsDBNullOrNullValue(MeManager[0]["MasterFileMajorParentId"]))
            {
                ShowMessage("اطلاعات مربوط به رشته پروانه شخص در سیستم ناقص می باشد، لطفا به واحد مربوطه جهت تصحیح اعلام گردد.");
                return false;
            }
            _MajorParentIdInWorkReq = (int)MeManager[0]["MasterFileMajorParentId"];
            if (_MajorParentIdInWorkReq != (int)(TSP.DataManager.MainMajors.Civil) && _MajorParentIdInWorkReq != (int)(TSP.DataManager.MainMajors.Architecture))
            {
                _IsCivilObserver = false;
            }
            else { _IsCivilObserver = true; }

            #endregion
        }
        _MeOfOthId = _MeId = MeId;
        return true;
    }
    private Boolean FillOtherPerson(string OtpCode)
    {
        #region
        TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.DocOffMemberAcceptedGradeManager MemberAcceptedGradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        OthpManager.FindKardanAndMemarByOtpCode(OtpCode, (int)TSP.DataManager.OtherPersonType.Kardan);
        if (OthpManager.Count == 1)
        {
            if (Convert.ToBoolean(OthpManager[0]["InActive"]))
            {
                ShowMessage("عضو مورد نظر غیر فعال می باشد");
                return false;
            }

            if (!Utility.IsDBNullOrNullValue(OthpManager[0]["FatherName"]))
                txtFatherName.Text = OthpManager[0]["FatherName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthpManager[0]["FirstName"]))
                txtFirstName.Text = OthpManager[0]["FirstName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthpManager[0]["LastName"]))
                txtLastName.Text = OthpManager[0]["LastName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthpManager[0]["SSN"]))
                txtSSN.Text = OthpManager[0]["SSN"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthpManager[0]["FileNoDate"]))
                txtFileDate.Text = OthpManager[0]["FileNoDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthpManager[0]["FileNo"]))
                txtFileNo.Text = _FileNo = OthpManager[0]["FileNo"].ToString();

            if (!Utility.IsDBNullOrNullValue(OthpManager[0]["AgentId"]))
                _AgentId = Convert.ToInt32(OthpManager[0]["AgentId"]);
            if (!Utility.IsDBNullOrNullValue(OthpManager[0]["AgentName"]))
            {
                txtAgent.Text = OthpManager[0]["AgentName"].ToString();
            }

            if (Utility.GetCurrentAgentCode() != Utility.GetCurrentUser_AgentId())
            {
                if (Utility.IsDBNullOrNullValue(OthpManager[0]["AgentId"]))
                {
                    ShowMessage("نمایندگی کاردان انتخاب شده مشخص نمی باشد");
                    return false;
                }
                if (Utility.GetCurrentUser_AgentId() != Convert.ToInt32(OthpManager[0]["AgentId"]))
                {
                    ShowMessage("تنها قادر به ثبت طراحی کاردان های نمایندگی خود می باشید.نمایندگی کاردان انتخاب شده با نمایندگی شما متفاوت می باشد");
                    return false;

                }
            }
            DataTable dtAcceptedGrade = MemberAcceptedGradeManager.FindByOtpIdAndResId(Convert.ToInt32(OthpManager[0]["OtpId"]), (int)TSP.DataManager.DocumentResponsibilityType.Observation, 0);
            if (dtAcceptedGrade.Rows.Count > 0)
                txtObsId.Text = dtAcceptedGrade.Rows[0]["GrdName"].ToString();
            _MeOfOthId = Convert.ToInt32(OthpManager[0]["OtpId"]);
        }
        else
        {
            ShowMessage("چنین کد عضویتی وجود ندارد.مجدداً وارد نمایید");
            return false;
        }
        #endregion
        return true;
    }
    private Boolean FillExpArchitect(string MeOfOthId)
    {
        #region
        TSP.DataManager.OtherPersonManager OthpManagerMemar = new TSP.DataManager.OtherPersonManager();
        OthpManagerMemar.FindKardanAndMemarByOtpCode(MeOfOthId, (int)TSP.DataManager.OtherPersonType.Memar);
        if (OthpManagerMemar.Count == 1)
        {
            if (Convert.ToBoolean(OthpManagerMemar[0]["InActive"]))
            {
                ShowMessage("عضو مورد نظر غیر فعال می باشد");
                return false;
            }

            if (!Utility.IsDBNullOrNullValue(OthpManagerMemar[0]["FatherName"]))
                txtFatherName.Text = OthpManagerMemar[0]["FatherName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthpManagerMemar[0]["FirstName"]))
                txtFirstName.Text = OthpManagerMemar[0]["FirstName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthpManagerMemar[0]["LastName"]))
                txtLastName.Text = OthpManagerMemar[0]["LastName"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthpManagerMemar[0]["SSN"]))
                txtSSN.Text = OthpManagerMemar[0]["SSN"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthpManagerMemar[0]["FileNoDate"]))
                txtFileDate.Text = OthpManagerMemar[0]["FileNoDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(OthpManagerMemar[0]["FileNo"]))
                txtFileNo.Text = _FileNo = OthpManagerMemar[0]["FileNo"].ToString();


            TSP.DataManager.DocOffMemberAcceptedGradeManager MemberAcceptedGradeManagerMemar = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
            DataTable dtMemberAcceptedGrade = MemberAcceptedGradeManagerMemar.FindByOtpIdAndResId(Convert.ToInt32(OthpManagerMemar[0]["OtpId"]), (int)TSP.DataManager.DocumentResponsibilityType.Observation, 0);
            if (dtMemberAcceptedGrade.Rows.Count > 0)
                txtObsId.Text = dtMemberAcceptedGrade.Rows[0]["GrdName"].ToString();

        }
        else
        {
            ShowMessage("چنین کد عضویتی وجود ندارد.مجدداً وارد نمایید");
            return false;
        }

        #endregion
        return true;
    }
    #region Set Visibility

    private void SetLabaleVisibilityBasedonAgent()
    {//*** رشته نقشه برداری در صورتی که شیراز باشد "زیربنا نظارت شهرداری شیراز" به آن نمایش داده می شود و اگر نه هیچ فیلد اطلاعاتی را پر نمی کند
        if (_AgentId == Utility.GetCurrentAgentCode())//نمایندگی شیراز
        {
            // SetComperBonyadVisible(true);
            txtDareyonObserveMeter.ClientVisible =
           lblDareyonObserveMeter.ClientVisible =
           txtZarghanObserveMeter.ClientVisible =
           lblZarghanObserveMeter.ClientVisible =
           txtLapooyObserveMeter.ClientVisible =
           lblLapooyObserveMeter.ClientVisible =
           txtKhanZenyanObserveMeter.ClientVisible =
           lblKhanZenyanObserveMeter.ClientVisible =
           lblDesignShirazMunicipality.ClientVisible = txtDesignShirazMunicipality.ClientVisible = lblDesignShirazMunicipalityLimitation.ClientVisible = true;
            lblObsShirazMunicipality.ClientVisible = txtObsShirazMunicipality.ClientVisible = lblObsShirazMunicipalityLimitation.ClientVisible = true;

        }
        else/////سایر استان
        {
            //SetComperBonyadVisible(true);
            txtDareyonObserveMeter.ClientVisible =
                lblDareyonObserveMeter.ClientVisible =
                txtZarghanObserveMeter.ClientVisible =
                lblZarghanObserveMeter.ClientVisible =
                txtLapooyObserveMeter.ClientVisible =
                lblLapooyObserveMeter.ClientVisible =
                txtKhanZenyanObserveMeter.ClientVisible =
                lblKhanZenyanObserveMeter.ClientVisible =
            lblObsShirazMunicipality.ClientVisible = txtObsShirazMunicipality.ClientVisible = lblObsShirazMunicipalityLimitation.ClientVisible = false;
            lblDesignShirazMunicipality.ClientVisible = txtDesignShirazMunicipality.ClientVisible = lblDesignShirazMunicipalityLimitation.ClientVisible = true;

        }
    }
    private void SetLableVisibityBasedOnShirazCities(int CitId1)
    {
        //***خرامه -سروستان-سپیدان
        if (CitId1 == (int)TSP.DataManager.CityCode.Kharameh || CitId1 == (int)TSP.DataManager.CityCode.Sarvestan || CitId1 == (int)TSP.DataManager.CityCode.Sepidan)
        {
            lblCity2.ClientVisible =
            txtCity2.ClientVisible = false;

            //lblDesignShirazMunicipality.ClientVisible = txtDesignShirazMunicipality.ClientVisible = lblDesignShirazMunicipalityLimitation.ClientVisible =
            lblObsShirazMunicipality.ClientVisible = txtObsShirazMunicipality.ClientVisible = lblObsShirazMunicipalityLimitation.ClientVisible =
            lblDareyonObserveMeter.ClientVisible =
            lblZarghanObserveMeter.ClientVisible =
            lblLapooyObserveMeter.ClientVisible =
            lblKhanZenyanObserveMeter.ClientVisible =
            txtDareyonObserveMeter.ClientVisible =
            txtZarghanObserveMeter.ClientVisible =
            txtLapooyObserveMeter.ClientVisible =
            txtKhanZenyanObserveMeter.ClientVisible = false;

            if (_MajorParentIdInWorkReq == (int)TSP.DataManager.MainMajors.Mapping)
            {
                RoundPanelObserveCapacity.ClientVisible = false;
            }
        }
        else if (CitId1 == (int)TSP.DataManager.CityCode.Shiraz)//**شیراز
        {
            lblCity2.ClientVisible = true;
            txtCity2.ClientVisible = true;
            lblDesignShirazMunicipality.ClientVisible = txtDesignShirazMunicipality.ClientVisible = lblDesignShirazMunicipalityLimitation.ClientVisible =
            lblObsShirazMunicipality.ClientVisible = txtObsShirazMunicipality.ClientVisible = lblObsShirazMunicipalityLimitation.ClientVisible = true;
            if (_MajorParentIdInWorkReq != (int)TSP.DataManager.MainMajors.Mapping)
            {
                lblDareyonObserveMeter.ClientVisible = true;
                lblZarghanObserveMeter.ClientVisible = true;
                lblLapooyObserveMeter.ClientVisible = true;
                lblKhanZenyanObserveMeter.ClientVisible = true;
                txtDareyonObserveMeter.ClientVisible = true;
                txtZarghanObserveMeter.ClientVisible = true;
                txtLapooyObserveMeter.ClientVisible = true;
                txtKhanZenyanObserveMeter.ClientVisible = true;
            }
            RoundPanelObserveCapacity.ClientVisible = true;
        }


    }
    #endregion

    public Boolean CheckConditions(int MeId, TSP.DataManager.TSProjectIngridientType ProjectIngridientTypeId, int ProjectCitId)
    {
        #region شرایط مشترک بین ناظر و طراح
        string Msg = "";
        if (!TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg))
        {
            ShowMessage(Msg);
            return false;
        }
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();

        System.Data.DataTable dtObWorkRequest = ObserverWorkRequestManager.SelectTSObserverWorkRequestByMember(MeId, TSP.DataManager.TSObserverWorkRequestStatus.Confirm);
        if (dtObWorkRequest.Rows.Count == 0)
        {
            ShowMessage("عضو انتخاب شده دارای آماده به کاری  تایید شده نمی باشد.");
            return false;
        }

        string DocMeFileExpireDate = dtObWorkRequest.Rows[0]["DocMeFileExpireDate"].ToString();
        if (string.IsNullOrEmpty(DocMeFileExpireDate))
        {
            ShowMessage("امکان ثبت عضو مورد نظر وجود ندارد.مدت زمان اعتبار پروانه اشتغال عضو نامشحص می باشد.");
            return false;
        }
        string DateOfToday = Utility.GetDateOfToday();
        if (DocMeFileExpireDate.CompareTo(DateOfToday) <= 0)
        {
            if (Utility.IsObserverDocExpireDateChecked())
            {
                ShowMessage("امکان ثبت عضو مورد نظر وجود ندارد.مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.");
                return false;
            }
            else
            {
                if (_ShowAlert == false)
                {
                    ShowMessage("هشدار: مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.");
                    _ShowAlert = true;
                    _AlertMessage = "هشدار: مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.";
                    // return false;
                }
                //else
                //    return true;
            }
        }

        #endregion
        switch (ProjectIngridientTypeId)

        {
            case TSP.DataManager.TSProjectIngridientType.Observer:
                #region Obs Conditions
                if (Convert.ToInt32(dtObWorkRequest.Rows[0]["ObsId"]) == -2)
                {
                    ShowMessage("امکان ثبت عضو مورد نظر به عنوان ناظر وجود ندارد.براساس آخرین درخواست تایید شده آماده بکاری ایشان دارای صلاحیت نظارت/ صلاحیت نقشه برداری نمی باشد");
                    return false;
                }
                #region CheckCity for Observer
                bool IsInCity = false;

                if ((!Utility.IsDBNullOrNullValue(dtObWorkRequest.Rows[0]["City1"]) && (Convert.ToInt32(dtObWorkRequest.Rows[0]["City1"]) == ProjectCitId))
                || (!Utility.IsDBNullOrNullValue(dtObWorkRequest.Rows[0]["City2"]) && Convert.ToInt32(dtObWorkRequest.Rows[0]["City2"]) == ProjectCitId)
                || (ProjectCitId == (int)TSP.DataManager.CityCode.KhanZenyan && Convert.ToInt32(dtObWorkRequest.Rows[0]["KhanZenyanObserveMeter"]) != 0)
                || (ProjectCitId == (int)TSP.DataManager.CityCode.Dareyon && Convert.ToInt32(dtObWorkRequest.Rows[0]["DareyonObserveMeter"]) != 0)
                || (ProjectCitId == (int)TSP.DataManager.CityCode.Zarghan && Convert.ToInt32(dtObWorkRequest.Rows[0]["ZarghanObserveMeter"]) != 0)
                || (ProjectCitId == (int)TSP.DataManager.CityCode.Lapooy && Convert.ToInt32(dtObWorkRequest.Rows[0]["LapooyObserveMeter"]) != 0)
                )
                {
                    IsInCity = true;
                }
                if (!IsInCity)
                {
                    ShowMessage("شهرهای انتخاب شده توسط عضو در فرم آماده به کاری با شهر پروژه همخوانی ندارد.");
                    return false;
                }
                #endregion
                #endregion
                break;
            case TSP.DataManager.TSProjectIngridientType.Designer:
                #region Designer Conditions
                //if (Convert.ToInt16(dtObWorkRequest.Rows[0]["WantedWorkType"]) == (int)TSP.DataManager.TSWorkRequestWantedWorkType.Obsever)
                //{
                //****رشته عمران و معماری می تواند کار طراحی زیر 600 متر انجام دهد و از ظرفیت نظارت وی کسر می شود اضافه شود
                _MajorParentIdInWorkReq = (int)dtObWorkRequest.Rows[0]["MasterMfMjParentId"];
                if (_MajorParentIdInWorkReq != (int)(TSP.DataManager.MainMajors.Civil) && _MajorParentIdInWorkReq != (int)(TSP.DataManager.MainMajors.Architecture))
                {
                    _IsCivilObserver = false;
                    //ShowMessage("عضویت انتخاب شده در آماده به کاری خود، زمینه کاری را فقط نظارت انتخاب کرده اند و کار طراحی برای ایشان قابل ثبت نمی باشد");
                    //return false;
                }
                else { _IsCivilObserver = true; }
                //}
                #endregion
                break;
        }

        return true;
    }
    public void SetUserControlVisible(Boolean Visible)
    {
        PanelMainWorkReqUserControl.Visible = UserControlvisible = Visible;
    }

    private void ShowMessage(string Msg)
    {
        lblWarning.Visible = true;
        lblWarning.InnerText = _ErrorMessage = Msg;

    }

    public void ClearForm()
    {
        _MeOfOthId = _AgentId =
         MeOfOthId = AgentId = -2;
         txtFatherName.Text =
         txtFirstName.Text =
         txtLastName.Text =
         txtSSN.Text =
         txtFileDate.Text =
         txtFileNo.Text = _FileNo = txtObsId.Text = txtAgent.Text = txtBonyadMaskan.Text =
         txtCity1.Text = txtCity2.Text = txtDareyonObserveMeter.Text = txtDesign.Text = txtDesignBonyadMaskan.Text =
         txtDesignShirazMunicipality.Text = txtKhanZenyanObserveMeter.Text = txtLapooyObserveMeter.Text =
         txtMapping.Text = txtMemberFileMajor.Text = txtObsShirazMunicipality.Text =
         txtShirazMunicipulityUrbenismEntebaghShahri.Text = txtShirazMunicipulityUrbenismTarh.Text =
         txtSSN.Text = txtUrbenism.Text = txtZarghanObserveMeter.Text = lblHasGasCert.Text = "----";
        CheckListStructureGroups.DataBind();
        CheckListStructureGroups.Items.FindByValue("1").Selected = false;
        CheckListStructureGroups.Items.FindByValue("2").Selected = false;
        CheckListStructureGroups.Items.FindByValue("3").Selected = false;
        CheckListStructureGroups.Items.FindByValue("4").Selected = false;
    }

}