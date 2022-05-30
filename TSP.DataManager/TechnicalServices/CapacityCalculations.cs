using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

using TSP.DataManager;
public class CapacityCalculations
{
    // public static int _CountWorkUnder400 = 50;
    #region Classes
    PartnertsMajorCombination _PartnertsMajorCombination;
    /// <summary>
    /// مشخصات ترکیب رشته شرکای دفتر/شرکت
    /// </summary>
    private class PartnertsMajorCombination
    {
        public int MainMajorNum = 0;
        public int SecondaryMajorNum = 0;
        public int TotalMajorNum = 0;
    }

    MemberCapacity _MemberCapacity;
    public class MemberCapacity
    {

        public Boolean IsConditionalCapacityChecked = false;
        public Boolean IsOfficeCapacityCalculate = false;

        //public int MemberGradeId = 0;
        //public int MemberMajorId = 0;
        //public string MemberMajorName = "";
        //public string MemberFullName = "";
        //public double MemberMaxJobCapacity = 0;//حداکثر ظرفیت مجاز نسبت به طراحی و یا اجرا که مربوط به جداول کتاب مبحث دوم باشد
        public int MemberObservationPercent = 0;
        //public int MemberCurrentProjectNum = 0;
        //public int MemberImplementCapacity = 0;
        //public int MemberMaxFloorCount = 0;
        //*********************************
        /// <summary>
        ///Conditional Capacity
        /// </summary>
        public int MemberConditionalCapacityDesign = 0;
        public int MemberConditionalCapacityObserve = 0;
        public int MemberConditionalCapacityImplement = 0;
        public int MemberDesignCapacity = 0;
        public int MemberObservationCapacity = 0;
        public int MemberImplementCapacity = 0;
        public int MemberUrbenismTarhShahriCapacity = 0;
        public int MemberUrbenismEntebaghShahriSakhtemanCapacity = 0;


        public int MemberMaxJobCount = 0;
        /// <summary>
        /// MemberDesignCapacity+OfficeCapacityیاEngOfficeCapacity
        /// </summary>
        public int MemberTotalDesignAndOfficeCapacity = 0;
        /// <summary>
        /// Max(MemberTotalDesignAndOfficeCapacity ,MemberObservationCapacity)
        /// </summary>
        public int MemberMaxCapacity = 0;
        public int OfficeCapacity = 0;
        public int EngOfficeCapacity = 0;
        public int IncreamentPercentOfOfficeMembership = 0;
        public int IncreamentPercentOfEngOfficeMembership = 0;
        //****************************************
        public int CurrentObsId = -2;
        public string ObsGradeName = "";
        public string ObsGradeDate = "";
        public int CurrentMappingId = -2;
        public string MappingGradeName = "";
        public string MappingGradeDate = "";
        public int CurrentUrbenismId = -2;
        public string UrbenismGradeName = "";
        public string UrbenismGradeDate = "";

        public int CurrentDesIdInOfficeEngOff = -2;
        public int CurrentDesId = -2;
        public string DesignGradeName = "";
        public string DesignGradeDate = "";
        public int CurrentImpId = -2;
        public string ImpGradeName = "";
        public string ImpGradeDate = "";

        public Boolean HasOffice = false;
        public Boolean IsOfficeIsExpired = false;
        public Boolean HasEngOffice = false;
        public Boolean IsEngOfficeIsExpired = false;
        public Boolean HasGasCert = false;

        public int MjParentId = 0;
        public Boolean HasError = false;
        public string ErrorMsg = "";

    }

    public class MemberCapacityInWorkRequestTable
    {
        public Boolean WantCharityWork;
        public Boolean WantShahrakSanatiMeter;
        public int BonyadMaskan;
        public int BonyadMaskanDesignMeter;
        public int ShirazMunicipalityMeter;
        public int ShirazMunicipalityDesignMeter;
        public int ShirazMunicipulityUrbenismTarh;
        public int ShirazMunicipulityUrbenismEntebaghShahri;
        public int UsedCapacity;
        public int UsedCapacityDesShirazMun;
        public int UsedCapacityDesOtherCities;
        public int UsedCapacityObsShiraz;
        public int UsedCapacityObsSadra;
        public int UsedCapacityObsOtherCities;
        public int UsedCapacityUrbenismTarhShirazMun;
        public int UsedCapacityEntebaghShahriShirazMun;
        public int UsedCapacityUrbenismTarhOtherCities;
        public int UsedCapacityEntebaghShahriOtherCities;
        public int UsedCapacityCharity;
        public int RemainCapacity;
        public int RemainCapacityObs;
        public int RemainCapacityObsReal;
        public int RemainCapacityDesign;
        public int PercentOfCapacityUsage;
        public int CountRemainWorkCount;
        public int TotalCapacity;
        public int CapacityObs;
        public int CapacityDesign;
        public int CountUnder400MeterWork;
        public int CountUnder400MeterWorkDesign;
        public int CountWorks;
        public int CountInproccesWorks;
        public int CountRandomSelected;
        public int CountRejectByObs;
    }
    #endregion

    #region Enumerations
    public enum CapacityErr
    {
        CanNotFindInfo = -1,
        NotEnoughRmainCapacity = 1,
        MaxJobIsTaken = 2,
        NotEnoughCapacityAndMaxJobIsTaken = 3,
        NotEnoughStep = 4,
        MoreThanZarghanEnteredInWorkReq = 5,
        MoreThanDareyonEnteredInWorkReq = 6,
        MoreThanKhanZenyanEnteredInWorkReq = 7,
        MoreThanLapooyObserveMeterEnteredInWorkReq = 8,
        DoesNotHaveEnoughCapacityForZarghan = 9,
        DoesNotHaveEnoughCapacityForDareyon = 10,
        DoesNotHaveEnoughCapacityForKhanZenyan = 11,
        DoesNotHaveEnoughCapacityForLapooyObserverMeter = 12,
        MoreThanShirazMunObsObserverMeterEnteredInWorkReq = 13,
        DoesNotHaveEnoughCapacityForShirazMunObsObserver = 14,
        DoesNotHaveEnoughCapacityForSadraObserver = 15,
        DoesNotHaveEnoughCapacityForObserver = 16,
        MoreThanShirazMuDesignMeterEnteredInWorkReq = 17,
        DoesNotHaveEnoughCapacityForShirazMunDesign = 18,
        DoesNotHaveEnoughCapacityForDesign = 19,
        ConflictInUsedCapacity = 20,
        ConflictInSumUsedAndRemainAndTotalCapacity = 21,
        ConflictInSumRemainAndRemainDesAndRemainobs = 22,
        ConflictInWorkCounts = 23,
        ConflictInRemainCapacityObs = 24,
        NotEnoughObserverCapacity = 25,
        NotEnoughDesingCapacity = 26,
        DoesNotHaveEnoughCapacityForDesignInShiraz = 27,
        DoesNotHaveEnoughDesign_SumUsedAndMunAndBonyadAndUsedMoreThanTotalCapacity = 28,
        DoNotHaveEnougCapcacityForObsOutOfShiraz = 29,
        DoNotHaveObserverCapacity = 30,
        DoNotHaveEnougCapcacityForShiraz = 31,
        HaveObsBonyadMaskanMoreThanLimitation = 32,
        HaveDesBonyadMaskanMoreThanLimitation = 33,
        DoNotHaveCapacityForObsBonyadMaskan = 34,
        DoNotHaveCapacityForDesBonyadMaskan = 35

    }

    public enum ConditionalCapacityType
    {
        Increased = 0,
        Decreased = 1
    }
    #endregion

    #region Constructors
    public CapacityCalculations()
    {
        _PartnertsMajorCombination = new PartnertsMajorCombination();
        _MemberCapacity = new MemberCapacity();
    }
    #endregion

    public string FindErrorMessage(int Err)
    {
        string Message = "";
        switch (Err)
        {
            case (int)CapacityErr.NotEnoughCapacityAndMaxJobIsTaken:
                Message = "ظرفیت باقیمانده عضو مورد نظر کافی نمی باشد و حداکثر تعداد کار مجاز نیز گرفته شده است";
                break;
            case (int)CapacityErr.NotEnoughObserverCapacity:
                Message = "با توجه به پایه نظارت شخص امکان دریافت این متراژ کار وجود ندارد";
                break;
            case (int)CapacityErr.NotEnoughDesingCapacity:
                Message = "با توجه به پایه طراحی شخص امکان دریافت این متراژ کار وجود ندارد";
                break;

            case (int)CapacityErr.NotEnoughRmainCapacity:
                Message = "ظرفیت باقیمانده عضو مورد نظر کافی نمی باشد.";
                break;

            case (int)CapacityErr.MaxJobIsTaken:
                Message = "حداکثر تعداد کار مجاز برای عضو مورد نظر گرفته شده است.";
                break;

            case (int)CapacityErr.CanNotFindInfo:
                Message = "اطلاعات عضو مورد نظر  در آماده بکاری یافت نشد.";
                break;
            case (int)CapacityErr.MoreThanZarghanEnteredInWorkReq:
                Message = "متراژ وارد شده بیشتر از  حد نصاب زیربنای انتخابی عضو در آماده بکاری برای شهر زرقان می باشد.";
                break;
            case (int)CapacityErr.MoreThanDareyonEnteredInWorkReq:
                Message = "متراژ وارد شده بیشتر از  حد نصاب زیربنای انتخابی عضو در آماده بکاری برای شهر داریون می باشد.";
                break;
            case (int)CapacityErr.MoreThanKhanZenyanEnteredInWorkReq:
                Message = "متراژ وارد شده بیشتر از  حد نصاب زیربنای انتخابی عضو در آماده بکاری برای شهر خان زنیان می باشد.";
                break;
            case (int)CapacityErr.MoreThanLapooyObserveMeterEnteredInWorkReq:
                Message = "متراژ وارد شده بیشتر از  حد نصاب زیربنای انتخابی عضو در آماده بکاری برای شهر لپویی می باشد.";
                break;
            case (int)CapacityErr.DoesNotHaveEnoughCapacityForZarghan:
                Message = "متراژ وارد شده بیشتر از میزان ظرفیت باقیمانده برای شهر زرقان می باشد";
                break;
            case (int)CapacityErr.DoesNotHaveEnoughCapacityForDareyon:
                Message = "متراژ وارد شده بیشتر از میزان ظرفیت باقیمانده برای شهر داریون می باشد";
                break;
            case (int)CapacityErr.DoesNotHaveEnoughCapacityForKhanZenyan:
                Message = "متراژ وارد شده بیشتر از میزان ظرفیت باقیمانده برای شهر خان زنیان می باشد";
                break;
            case (int)CapacityErr.DoesNotHaveEnoughCapacityForLapooyObserverMeter:
                Message = "متراژ وارد شده بیشتر از میزان ظرفیت باقیمانده برای شهر لپویی می باشد";
                break;
            case (int)CapacityErr.MoreThanShirazMunObsObserverMeterEnteredInWorkReq:
                Message = "متراژ وارد شده بیشتر از  حد نصاب زیربنای انتخابی عضو در آماده بکاری برای نظارت شهرداری شیراز می باشد.";
                break;
            case (int)CapacityErr.DoesNotHaveEnoughCapacityForShirazMunObsObserver:
                Message = "متراژ وارد شده بیشتر از میزان ظرفیت باقیمانده برای نظارت شهرداری شیراز می باشد";
                break;
            case (int)CapacityErr.DoesNotHaveEnoughCapacityForSadraObserver:
                Message = "با توجه به مجموع زیربناهای وارد شده برای شهرهای مختلف و بنیاد مسکن در آماده بکاری ،دارای ظرفیت نظارت جهت شهر صدرا نمی باشد ";
                break;
            case (int)CapacityErr.DoesNotHaveEnoughCapacityForObserver:
                Message =
                Message = "با توجه به مجموع زیربناهای وارد شده برای شهرهای مختلف و بنیاد مسکن در آماده بکاری ،دارای ظرفیت نظارت برای شهر این پروژه نمی باشد ";
                break;
            case (int)CapacityErr.MoreThanShirazMuDesignMeterEnteredInWorkReq:
                Message = "متراژ وارد شده بیشتر از  حد نصاب زیربنای انتخابی عضو در آماده بکاری برای طراحی شهرداری شیراز می باشد.";
                break;
            case (int)CapacityErr.DoesNotHaveEnoughCapacityForShirazMunDesign:
                Message = "متراژ وارد شده بیشتر از میزان ظرفیت باقیمانده برای طراحی شهرداری شیراز می باشد";
                break;
            case (int)CapacityErr.DoesNotHaveEnoughCapacityForDesignInShiraz:
                Message = "متراژ وارد شده بیشتر از میزان ظرفیت باقیمانده برای طراحی  می باشد";
                break;
            case (int)CapacityErr.DoesNotHaveEnoughDesign_SumUsedAndMunAndBonyadAndUsedMoreThanTotalCapacity:
                Message = "با توجه به متراژهای وارد شده در آماده بکاری و مجموع متراژ کارهای ثبت شده در سیستم ظرفیت حداکثر در برش زمانی تکمیل شده است ";
                break;

            case (int)CapacityErr.DoesNotHaveEnoughCapacityForDesign:
                Message = "متراژ وارد شده بیشتر از میزان ظرفیت باقیمانده برای طراحی جهت کار در استان می باشد";
                break;
            case (int)CapacityErr.DoNotHaveEnougCapcacityForObsOutOfShiraz:
                Message = "با توجه به متراژهای وارد شده در آماده بکاری و مجموع کارهای نظارت ثبت شده در سیستم، دارای ظرفیت نمی باشد";
                break;

            case (int)CapacityErr.DoNotHaveEnougCapcacityForShiraz:
                Message = "با توجه به متراژهای وارد شده در آماده بکاری و مجموع کارهای ثبت شده در سیستم ، دارای ظرفیت نمی باشد";
                break;

            case (int)CapacityErr.DoNotHaveObserverCapacity:
                Message = "ظرفیت نظارت کافی نمی باشد";
                break;
            case (int)CapacityErr.ConflictInUsedCapacity:
                Message = "مجموع متراژ های مصرف شده با کل مصرفی مغایرت دارد";
                break;
            case (int)CapacityErr.ConflictInSumUsedAndRemainAndTotalCapacity:
                Message = "تفاضل ظرفیت مصرفی و  کل ظرفیت با ظرفیت باقیمانده مغایرت دارد";
                break;
            case (int)CapacityErr.ConflictInSumRemainAndRemainDesAndRemainobs:
                Message = "مجموع باقیمانده نظارت و طراحی با باقیمانده کل مغایرت دارد";
                break;
            case (int)CapacityErr.ConflictInWorkCounts:
                Message = "تعداد کار عضو دچار مغایرت شده است";
                break;
            case (int)CapacityErr.ConflictInRemainCapacityObs:
                Message = "مانده ظرفیت نظارت دچار مغایرت شده است";
                break;

            case (int)CapacityErr.HaveObsBonyadMaskanMoreThanLimitation:
                Message = "متراژ کارهای نظارت ثبت شده از نوع بنیاد مسکن ، بیشتر از میزان متراژ اعلام شده نظارت بنیاد مسکن در آماده به کاری می باشد";
                break;
            case (int)CapacityErr.HaveDesBonyadMaskanMoreThanLimitation:
                Message = "متراژ کارهای طراحی ثبت شده از نوع بنیاد مسکن ، بیشتر از میزان متراژ اعلام شده طراحی بنیاد مسکن در آماده به کاری می باشد";
                break;
            case (int)CapacityErr.DoNotHaveCapacityForObsBonyadMaskan:
                Message = "با توجه به مجموع متراژ کارهای نظارت ثبت شده از نوع بنیاد مسکن و میزان متراژ  نظارت بنیاد مسکن اعلام شده در آماده بکاری ، ظرفیت لازم برای ثبت این متراژ از کار نظارت بنیاد مسکن وجود ندارد";
                break;
            case (int)CapacityErr.DoNotHaveCapacityForDesBonyadMaskan:
                Message = "با توجه به مجموع متراژ کارهای طراحی ثبت شده از نوع بنیاد مسکن و میزان متراژ  طراحی بنیاد مسکن اعلام شده در آماده بکاری ، ظرفیت لازم برای ثبت این متراژ از کار طراحی بنیاد مسکن وجود ندارد";
                break;
        }
        return Message;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="MeId"></param>
    /// <param name="MjParentId"></param>
    /// <param name="LastMfId"></param>
    /// <param name=""></param>
    /// <returns></returns>
    public MemberCapacity CalculateMemberPotentialCapacityAndSetGradeInfo(int MeId, int MjParentId, int LastMfId, Boolean CalculateByAssignmentPercent, int OfficeEngOfId, TSP.DataManager.DocOffIncreaseJobCapacityType DocOffIncreaseJobCapacityType, Boolean MeHasGasCert, Int16 _CurrentDesIdInOfficeEngOff)
    {
        _MemberCapacity.CurrentDesIdInOfficeEngOff = _CurrentDesIdInOfficeEngOff;
        _MemberCapacity.HasGasCert = MeHasGasCert;
        if (LastMfId == -1)
        {
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
            if (dtMeFile.Rows.Count <= 0)
            {
                _MemberCapacity.HasError = true;
                _MemberCapacity.ErrorMsg = "برای این کد عضویت پروانه اشتغال به کار تایید شده وجود ندارد.";
                return _MemberCapacity;
            }
            LastMfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
        }

        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        _MemberCapacity.MjParentId = MjParentId;
        DataTable dtMeDetailObs = DocMemberFileDetailManager.FindByResponsibility(LastMfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation, -1, _MemberCapacity.MjParentId);
        DataTable dtMeDetailMapping = DocMemberFileDetailManager.FindByResponsibility(LastMfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Mapping, -1, _MemberCapacity.MjParentId);
        DataTable dtMeDetailDesign = DocMemberFileDetailManager.FindByResponsibility(LastMfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design, -1, _MemberCapacity.MjParentId);
        DataTable dtMeDetailUrbanism = DocMemberFileDetailManager.FindByResponsibility(LastMfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Urbanism, -1, _MemberCapacity.MjParentId);
        if (dtMeDetailObs.Rows.Count <= 0 && dtMeDetailMapping.Rows.Count <= 0 && dtMeDetailDesign.Rows.Count <= 0 && dtMeDetailUrbanism.Rows.Count <= 0)
        {
            _MemberCapacity.HasError = true;
            _MemberCapacity.ErrorMsg = "بدلیل نداشتن هیچ یک از صلاحیت های نظارت / نقشه برداری / طراحی / شهرسازی امکان ثبت آماده بکاری در جهت وجود ندارد.";
            return _MemberCapacity;
        }

        #region نظارت
        if (dtMeDetailObs.Rows.Count > 0 && !Utility.IsDBNullOrNullValue(dtMeDetailObs.Rows[0]["GrdName"]) && !Utility.IsDBNullOrNullValue(dtMeDetailObs.Rows[0]["GrdId"]))
        {
            _MemberCapacity.ObsGradeName = dtMeDetailObs.Rows[0]["GrdName"].ToString();
            _MemberCapacity.ObsGradeDate = dtMeDetailObs.Rows[0]["Date"].ToString();
            _MemberCapacity.CurrentObsId = Convert.ToInt32(dtMeDetailObs.Rows[0]["GrdId"]);
        }
        #endregion
        #region نقشه برداری
        if (dtMeDetailMapping.Rows.Count > 0 && !Utility.IsDBNullOrNullValue(dtMeDetailMapping.Rows[0]["GrdName"]) && !Utility.IsDBNullOrNullValue(dtMeDetailMapping.Rows[0]["GrdId"]))
        {
            _MemberCapacity.MappingGradeName = dtMeDetailMapping.Rows[0]["GrdName"].ToString();

            _MemberCapacity.MappingGradeDate = dtMeDetailMapping.Rows[0]["Date"].ToString();
            _MemberCapacity.CurrentMappingId = Convert.ToInt32(dtMeDetailMapping.Rows[0]["GrdId"]);
        }
        #endregion
        #region طراحی        
        if (dtMeDetailDesign.Rows.Count > 0 && !Utility.IsDBNullOrNullValue(dtMeDetailDesign.Rows[0]["GrdName"]) && !Utility.IsDBNullOrNullValue(dtMeDetailDesign.Rows[0]["GrdId"]))
        {
            _MemberCapacity.DesignGradeName = dtMeDetailDesign.Rows[0]["GrdName"].ToString();
            _MemberCapacity.DesignGradeDate = dtMeDetailDesign.Rows[0]["Date"].ToString();
            _MemberCapacity.CurrentDesId = Convert.ToInt32(dtMeDetailDesign.Rows[0]["GrdId"]);
        }
        #endregion
        #region شهرسازی
        if (dtMeDetailUrbanism.Rows.Count > 0 && !Utility.IsDBNullOrNullValue(dtMeDetailUrbanism.Rows[0]["GrdName"]) && !Utility.IsDBNullOrNullValue(dtMeDetailUrbanism.Rows[0]["GrdId"]))
        {
            _MemberCapacity.UrbenismGradeName = dtMeDetailUrbanism.Rows[0]["GrdName"].ToString();
            _MemberCapacity.UrbenismGradeDate = dtMeDetailUrbanism.Rows[0]["Date"].ToString();
            _MemberCapacity.CurrentUrbenismId = Convert.ToInt32(dtMeDetailUrbanism.Rows[0]["GrdId"]);
        }
        #endregion

        SetMemberDesignObservationCapacity(MeId, CalculateByAssignmentPercent);
        if (_MemberCapacity.CurrentDesId != -2)
        {
            if (_MemberCapacity.IsOfficeCapacityCalculate)
                SetOfficMemberCapacityIncreament(OfficeEngOfId, DocOffIncreaseJobCapacityType, CalculateByAssignmentPercent);
            if (DocOffIncreaseJobCapacityType == DocOffIncreaseJobCapacityType.Office)
                _MemberCapacity.MemberTotalDesignAndOfficeCapacity = _MemberCapacity.MemberDesignCapacity + _MemberCapacity.OfficeCapacity;
            else if (DocOffIncreaseJobCapacityType == DocOffIncreaseJobCapacityType.EngOffice)
            {
                _MemberCapacity.MemberTotalDesignAndOfficeCapacity = _MemberCapacity.MemberDesignCapacity + _MemberCapacity.EngOfficeCapacity;
            }
            if (_MemberCapacity.HasGasCert && _MemberCapacity.MemberDesignCapacity != 0)
                _MemberCapacity.MemberDesignCapacity = _MemberCapacity.MemberDesignCapacity - (_MemberCapacity.MemberDesignCapacity / 4);

            if (_MemberCapacity.HasGasCert && _MemberCapacity.MemberTotalDesignAndOfficeCapacity != 0)
                _MemberCapacity.MemberTotalDesignAndOfficeCapacity = _MemberCapacity.MemberTotalDesignAndOfficeCapacity - (_MemberCapacity.MemberTotalDesignAndOfficeCapacity / 4);


        }
        if (_MemberCapacity.HasGasCert && _MemberCapacity.MemberObservationCapacity != 0)
            _MemberCapacity.MemberObservationCapacity = _MemberCapacity.MemberObservationCapacity - (_MemberCapacity.MemberObservationCapacity / 4);
        //**ماکزیزم ظرفیت
        _MemberCapacity.MemberMaxCapacity = Math.Max(_MemberCapacity.MemberTotalDesignAndOfficeCapacity, _MemberCapacity.MemberObservationCapacity);
        return _MemberCapacity;
    }


    /// <summary>
    /// ظرفیت کل طراحی و نظارت و شهرسازی یک عضو حقیقی را بر اساس پایه وی و بر اساس اختصاص ظرفیت تنظیم    
    ///   MaxJobCount, MaxJobCapacity, ObservationPercent, ObservationCapacity,  Grade,  MjId, MeId,  MeName
    ///   , MemberConditionalCapacityDesign,MemberConditionalCapacityObserve,MemberConditionalCapacityImplement
    ///   ,MemberDesignCapacity,MemberObservationCapacity
    /// </summary>
    /// <param name="MeId"></param>
    /// <param name="CalculateByCapacityAssignment">مشخص می کند آیا ظرفیت و تعداد کار براساس درصد اختصاص ظرفیت محاسبه شود</param>
    /// <returns></returns>
    private void SetMemberDesignObservationCapacity(int MeId, Boolean CalculateByCapacityAssignment)
    {
        #region Define Managers

        TSP.DataManager.DocOffMemberCapacityManager MemberCapacityManager = new TSP.DataManager.DocOffMemberCapacityManager();
        #endregion
        if (_MemberCapacity.IsConditionalCapacityChecked)
        {
            #region ConditionalCapacity
            if (_MemberCapacity.CurrentDesId != -2)
                _MemberCapacity.MemberConditionalCapacityDesign = GetConditionalCapacity(MeId, (int)TSProjectIngridientType.Designer);
            else
                _MemberCapacity.MemberConditionalCapacityDesign = 0;

            if (_MemberCapacity.CurrentObsId != -2)
                _MemberCapacity.MemberConditionalCapacityObserve = GetConditionalCapacity(MeId, (int)TSProjectIngridientType.Observer);
            else
                _MemberCapacity.MemberConditionalCapacityObserve = 0;

            if (_MemberCapacity.CurrentImpId != -2)
                _MemberCapacity.MemberConditionalCapacityImplement = GetConditionalCapacity(MeId, (int)TSProjectIngridientType.Implementer);
            else
                _MemberCapacity.MemberConditionalCapacityImplement = 0;
            #endregion
        }
        #region ظرفیت پایه شخص - MaxJobCapacity
        //*****بدست آوردن اطلاعات ظرفیت و تعداد کار مجاز سالانه برای شخص حقیقی بر اساس پایه
        if (_MemberCapacity.CurrentObsId != -2)
        {
            MemberCapacityManager.FindByGrdId(_MemberCapacity.CurrentObsId);
            if (MemberCapacityManager.Count > 0)
            {
                _MemberCapacity.MemberObservationCapacity = Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]);
                _MemberCapacity.MemberMaxJobCount = Convert.ToInt32(MemberCapacityManager[0]["MaxJobCount"]);
            }
        }
        if (_MemberCapacity.CurrentMappingId != -2)
        {
            MemberCapacityManager.FindByGrdId(_MemberCapacity.CurrentMappingId);
            if (MemberCapacityManager.Count > 0)
            {
                _MemberCapacity.MemberObservationCapacity = Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]);
                _MemberCapacity.MemberMaxJobCount = Convert.ToInt32(MemberCapacityManager[0]["MaxJobCount"]);
            }
        }
        if (_MemberCapacity.CurrentDesId != -2)
        {
            if (_MemberCapacity.CurrentDesIdInOfficeEngOff != -2)
                MemberCapacityManager.FindByGrdId(_MemberCapacity.CurrentDesIdInOfficeEngOff);
            else
                MemberCapacityManager.FindByGrdId(_MemberCapacity.CurrentDesId);
            if (MemberCapacityManager.Count > 0)
            {
                _MemberCapacity.MemberDesignCapacity = Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]);
                _MemberCapacity.MemberMaxJobCount = Convert.ToInt32(MemberCapacityManager[0]["MaxJobCount"]);
            }
        }
        if (_MemberCapacity.CurrentImpId != -2)
        {
            MemberCapacityManager.FindByGrdId(_MemberCapacity.CurrentImpId);
            if (MemberCapacityManager.Count > 0)
            {
                _MemberCapacity.MemberImplementCapacity = Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]);
                _MemberCapacity.MemberMaxJobCount = Convert.ToInt32(MemberCapacityManager[0]["MaxJobCount"]);
            }
        }
        //************رشته شهرسازی اینجا نوشته شود**********
        if (_MemberCapacity.CurrentUrbenismId != -2)
        {
            TSP.DataManager.TechnicalServices.UrbanistQualificationManager UrbanistQualificationManager = new TSP.DataManager.TechnicalServices.UrbanistQualificationManager();

            UrbanistQualificationManager.FindByGrade(_MemberCapacity.CurrentUrbenismId, (int)TSUrbanismQualificationType.SumAmadeSaziAraziAndTafkikAraziAndEntebaghKarbariArazi, 0);
            if (UrbanistQualificationManager.Count > 0)
            {
                _MemberCapacity.MemberUrbenismTarhShahriCapacity = Convert.ToInt32(UrbanistQualificationManager[0]["QualificationMeter"]);
                _MemberCapacity.MemberMaxJobCount = Convert.ToInt32(UrbanistQualificationManager[0]["Count"]);

            }
            UrbanistQualificationManager.FindByGrade(_MemberCapacity.CurrentUrbenismId, (int)TSUrbanismQualificationType.EntebaghShahriSakhteman, 0);
            if (UrbanistQualificationManager.Count > 0)
            {
                _MemberCapacity.MemberUrbenismEntebaghShahriSakhtemanCapacity = Convert.ToInt32(UrbanistQualificationManager[0]["QualificationMeter"]);
            }
        }

        #endregion

        //***********************************************************************************************************************************//
        //****میزان کل ظرفیت افزایش/کاهش نظارت= (ظرفیت افزایش کاهش طراحی *ضریب تبدیل طراحی به نظارت) + ظرفیت افزایش کاهش نظارت**//
        //****میزان  کل ظرفیت افزایش/کاهش طراحی= (ظرفیت افزایش کاهش نظارت/ضریب تبدیل طراحی به نظارت) + ظرفیت افزایش کاهش طراحی**//
        //**********************************************************************************************************************************//
        if (MemberCapacityManager.Count > 0)
            //**ضریب تبدیل طراحی به نظارت//*** در سیستم جدید سال 1398 این مورد اعمال نمی شود
            _MemberCapacity.MemberObservationPercent = Convert.ToInt32(MemberCapacityManager[0]["ObservationPercent"]);
        if (_MemberCapacity.IsConditionalCapacityChecked)
        {
            //**کل ظرفیت طراحی
            _MemberCapacity.MemberDesignCapacity += _MemberCapacity.MemberConditionalCapacityDesign;
            //**کل ظرفیت نظارت
            _MemberCapacity.MemberObservationCapacity += _MemberCapacity.MemberConditionalCapacityObserve;
            //**کل ظرفیت اجرا
            _MemberCapacity.MemberImplementCapacity += _MemberCapacity.MemberConditionalCapacityImplement;
        }
    }

    /// <summary>
    /// ظرفیت اضافی یا کم شده یک شخص یا شرکت یا دفتر را بر می گرداند
    /// </summary>
    public static int GetConditionalCapacity(int MeOfficeEngOId, int ProjectIngridientTypeId)
    {
        int InsConditionalCapacity = 0;
        int DesConditionalCapacity = 0;

        TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();

        ConditionalCapacityManager.FindByMeIdAndIsDecreased(MeOfficeEngOId, Utility.GetDateOfToday(), ProjectIngridientTypeId, (int)ConditionalCapacityType.Increased);
        for (int i = 0; i < ConditionalCapacityManager.Count; i++)
            InsConditionalCapacity += Convert.ToInt32(ConditionalCapacityManager[i]["Capacity"]);

        ConditionalCapacityManager.FindByMeIdAndIsDecreased(MeOfficeEngOId, Utility.GetDateOfToday(), ProjectIngridientTypeId, (int)ConditionalCapacityType.Decreased);
        for (int i = 0; i < ConditionalCapacityManager.Count; i++)
            DesConditionalCapacity += Convert.ToInt32(ConditionalCapacityManager[i]["Capacity"]);

        return InsConditionalCapacity - DesConditionalCapacity;
    }

    /// <summary>
    ///درصد و میزان افزایش ظرفیت کل طراحی شرکای یک دفتر یا شرکت را نسبت به دفاتر تک نفره را محاسبه و اعمال می کند    
    ///_MemberCapacity.IncreamentPercentOfOfficeMembership ,_MemberCapacity.IncreamentPercentOfEngOfficeMembership ,_MemberCapacity.OfficeCapacity,_MemberCapacity.OfficeCapacity,_MemberCapacity.MemberMaxJobCount:if its office
    /// </summary>
    /// <param name="OfficeEngoId"></param>
    /// <param name="DocOffIncreaseJobCapacityType"></param>
    /// <param name="CalculateByCapacityAssignment">مشخص می کند آیا ظرفیت و تعداد کار براساس درصد اختصاص ظرفیت محاسبه شود</param>    
    /// <returns></returns>
    public void SetOfficMemberCapacityIncreament(int OfficeEngoId, TSP.DataManager.DocOffIncreaseJobCapacityType DocOffIncreaseJobCapacityType, Boolean CalculateByAssignmentPercent)
    {
        DataTable OfficeMemberManager = GetOfficeMembers(OfficeEngoId, DocOffIncreaseJobCapacityType);
        if (OfficeMemberManager.Rows.Count != 1)
        {
            //********* محاسبه ظرفیت دفاتر چند نفره شامل 

            TSP.DataManager.DocOffMajorNum DocOffMajorNum = new TSP.DataManager.DocOffMajorNum();
            TSP.DataManager.DocOffIncreaseJobCapacityManager IncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();
            #region محاسبه ظرفیت دفاتر/شرکت چند نفره

            #region بدست آوردن ترکیب رشته شرکا را بدست می آورد و درصد افزایش اشتغال هر یک از شرکادفتر/شرکت نسبت به ظرفیت اشتغال دفتر تک نفره  از جداول صفحه26-31
            //*********ترکیب رشته شرکا را بدست می آورد:ArrayList[0]:_PartnertsMajorCombination.MainMajorNum;ArrayList[1]:_PartnertsMajorCombination.SecondaryMajorNum ;ArrayList[2]:_PartnertsMajorCombination.TotalMajorNum
            GetMajorNum(OfficeMemberManager);
            DocOffMajorNum.FindByMajorsNum(_PartnertsMajorCombination.MainMajorNum, _PartnertsMajorCombination.SecondaryMajorNum, _PartnertsMajorCombination.TotalMajorNum);
            if (DocOffMajorNum.Count <= 0)
                return;
            //*********بدست آوردن درصد افزایش ظرفیت اشتغال هر یک از شرکادفتر/شرکت نسبت به ظرفیت اشتغال دفتر تک نفره از جداول صفحه26-31**************
            IncreaseJobCapacityManager.FindByMNumId(Convert.ToInt32(DocOffMajorNum[0]["MNumId"]), (int)DocOffIncreaseJobCapacityType);
            if (IncreaseJobCapacityManager.Count <= 0)
                return;
            int DesignIncPer = Convert.ToInt32(IncreaseJobCapacityManager[0]["DesignIncPer"]);
            int SameGradeIncPer = Convert.ToInt32(IncreaseJobCapacityManager[0]["SameGradeIncPer"]);
            int MajorIncPer = Convert.ToInt32(IncreaseJobCapacityManager[0]["MajorIncPer"]);
            #endregion

            #region      //********بدست آوردن درصد افزایش بر اساس هم پایه بودن شرکا و حضور بیش از یک نفر در هر رشته
            bool SameGradeInc = false;
            bool MajorInc = false;
            for (int i = 0; i < OfficeMemberManager.Rows.Count; i++)
            {
                for (int j = 0; j < OfficeMemberManager.Rows.Count; j++)
                {
                    if (i != j)
                    {
                        //بر اساس درخواست خانم حیدری و توانگر در 94.02.08 که باید رشته پدر ملاک مقایسه باشد
                        if (!OfficeMemberManager.Rows[i].IsNull("FMjParentId") && !OfficeMemberManager.Rows[j].IsNull("FMjParentId") && Convert.ToInt32(OfficeMemberManager.Rows[i]["FMjParentId"]) == Convert.ToInt32(OfficeMemberManager.Rows[j]["FMjParentId"]))
                        {
                            //***هم رشته بودن شرکا
                            MajorInc = true;

                            if (!OfficeMemberManager.Rows[i].IsNull("DesCode") && !OfficeMemberManager.Rows[j].IsNull("DesCode") && Convert.ToInt32(OfficeMemberManager.Rows[i]["DesCode"]) == Convert.ToInt32(OfficeMemberManager.Rows[j]["DesCode"]))
                            { //***هم پایه بودن شرکا 
                                SameGradeInc = true;
                                //***هم رشته بودن شرکا                          
                            }
                        }
                    }
                }
                if (SameGradeInc && MajorInc)
                    break;
            }

            int SumInc = DesignIncPer;
            if (SameGradeInc)
                SumInc = SumInc + SameGradeIncPer;
            if (MajorInc)
                SumInc = SumInc + MajorIncPer;
            if (DocOffIncreaseJobCapacityType == TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
            {
                _MemberCapacity.IncreamentPercentOfOfficeMembership = SumInc;
                _MemberCapacity.OfficeCapacity = _MemberCapacity.MemberDesignCapacity * SumInc / 100;
                _MemberCapacity.MemberMaxJobCount = _MemberCapacity.MemberMaxJobCount / 2;//** پنجاه درصد مجموع کار افراد حقیقی شاغل در شرکت حقوقی
            }
            else if (DocOffIncreaseJobCapacityType == TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice)
            {
                _MemberCapacity.IncreamentPercentOfEngOfficeMembership = SumInc;
                _MemberCapacity.EngOfficeCapacity = _MemberCapacity.MemberDesignCapacity * SumInc / 100;
            }
            #endregion

            #endregion
        }
    }

    #region  بدست آوردن ترکیب شرکای دفتر-شرکت
    /// <summary>
    /// ترکیب رشته شرکا را بر اساس جداول صفحه26/31 کتاب مبحث دوم  بدست می آورد
    /// ArrayList[0]: MainMajorNum, ArrayList[1]: SecondaryMajorNum, ArrayList[2]: TotalMajorNum
    /// </summary>
    /// <param name="MembersArr"></param>
    /// <returns>فیلد های مورد نیاز جهت بدست آوردن ردیف مورد نظر در جدول
    /// [DocOff.MajorNum]بدست می آورد</returns>
    private void GetMajorNum(DataTable dtEngOffMember)
    {
        TSP.DataManager.MajorParentsManager MajorManager = new TSP.DataManager.MajorParentsManager();
        MajorManager.FindMjParents();

        int MainMajorNum = 0;
        int SecondaryMajorNum = 0;
        int TotalMajorNum = 0;
        //int MajorIncrement = 0;

        bool Architecture = false;
        bool Urbanism = false;
        bool Civil = false;
        bool Mechanic = false;
        bool Electronic = false;
        bool Mapping = false;
        bool Traffic = false;

        #region //******چک کردن داشتن یک رشته از رشته های هفتگانه
        for (int j = 0; j < dtEngOffMember.Rows.Count; j++)
        {
            if (Utility.IsDBNullOrNullValue(dtEngOffMember.Rows[j]["FMjParentId"]))
                continue;
            switch (Convert.ToInt32(dtEngOffMember.Rows[j]["FMjParentId"]))
            {
                case (int)TSP.DataManager.MainMajors.Architecture:
                    Architecture = true;
                    break;

                case (int)TSP.DataManager.MainMajors.Civil:
                    Civil = true;
                    break;

                case (int)TSP.DataManager.MainMajors.Electronic:
                    Electronic = true;
                    break;

                case (int)TSP.DataManager.MainMajors.Mechanic:
                    Mechanic = true;
                    break;

                case (int)TSP.DataManager.MainMajors.Mapping:
                    Mapping = true;
                    break;

                case (int)TSP.DataManager.MainMajors.Urbanism:
                    Urbanism = true;
                    break;

                case (int)TSP.DataManager.MainMajors.Traffic:
                    Traffic = true;
                    break;
            }
        }

        #endregion

        #region بدست آوردن ترکیب شرکا  از جدول 1 صفحه 26
        if (Architecture)
        {
            MajorManager.CurrentFilter = "MjParentId =" + ((int)TSP.DataManager.MainMajors.Architecture).ToString();
            if (MajorManager.Count == 1)
            {
                if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
                    MainMajorNum += 1;
                else
                    SecondaryMajorNum += 1;
            }
        }

        if (Civil)
        {
            MajorManager.CurrentFilter = "MjParentId =" + ((int)TSP.DataManager.MainMajors.Civil).ToString();
            if (MajorManager.Count == 1)
            {
                if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
                    MainMajorNum += 1;
                else
                    SecondaryMajorNum += 1;
            }
        }


        if (Electronic)
        {
            MajorManager.CurrentFilter = "MjParentId =" + ((int)TSP.DataManager.MainMajors.Electronic).ToString();
            if (MajorManager.Count == 1)
            {
                if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
                    MainMajorNum += 1;
                else
                    SecondaryMajorNum += 1;
            }
        }

        if (Mechanic)
        {
            MajorManager.CurrentFilter = "MjParentId =" + ((int)TSP.DataManager.MainMajors.Mechanic).ToString();
            if (MajorManager.Count == 1)
            {
                if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
                    MainMajorNum += 1;
                else
                    SecondaryMajorNum += 1;
            }
        }

        if (Mapping)
        {
            MajorManager.CurrentFilter = "MjParentId =" + ((int)TSP.DataManager.MainMajors.Mapping).ToString();
            if (MajorManager.Count == 1)
            {
                if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
                    MainMajorNum += 1;
                else
                    SecondaryMajorNum += 1;
            }
        }

        if (Urbanism)
        {
            MajorManager.CurrentFilter = "MjParentId =" + ((int)TSP.DataManager.MainMajors.Urbanism).ToString();
            if (MajorManager.Count == 1)
            {
                if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
                    MainMajorNum += 1;
                else
                    SecondaryMajorNum += 1;
            }
        }

        if (Traffic)
        {
            MajorManager.CurrentFilter = "MjParentId =" + ((int)TSP.DataManager.MainMajors.Traffic).ToString();
            if (MajorManager.Count == 1)
            {
                if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
                    MainMajorNum += 1;
                else
                    SecondaryMajorNum += 1;
            }
        }
        #endregion

        TotalMajorNum = MainMajorNum + SecondaryMajorNum;
        if ((MainMajorNum <= 1 && SecondaryMajorNum != 0) || TotalMajorNum == 1)
        {
            MainMajorNum = 0;
            SecondaryMajorNum = 0;
            TotalMajorNum = 1;
        }
        else if (MainMajorNum == 2 || MainMajorNum == 3)
        {
            SecondaryMajorNum = 0;
            TotalMajorNum = 0;
        }
        else if (MainMajorNum == 4 && SecondaryMajorNum == 0)
        {
            SecondaryMajorNum = 0;
            TotalMajorNum = 0;
        }
        else if (MainMajorNum == 4 && SecondaryMajorNum >= 1)
        {
            SecondaryMajorNum = 1;
            TotalMajorNum = 0;
        }
        _PartnertsMajorCombination.MainMajorNum = MainMajorNum;
        _PartnertsMajorCombination.SecondaryMajorNum = SecondaryMajorNum;
        _PartnertsMajorCombination.TotalMajorNum = TotalMajorNum;
    }

    #endregion

    #region Get Office/EngOffice MemberList & Info
    /// <summary>
    /// اعضای فعال شرکت یا دفتر (اعضای حقیقی) را بر می گرداند
    /// </summary>
    private DataTable GetOfficeMembers(int OfficeEngoId, TSP.DataManager.DocOffIncreaseJobCapacityType DocOffIncreaseJobCapacityType)
    {
        TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
        DataTable dOfficeMe = new DataTable();
        if (DocOffIncreaseJobCapacityType == TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
            //***اعضای غیرفعال شده را نیز بر می گرداند
            //OfficeMemberManager.FindByOffRequest(OfficeEngoId, -1, -1, -1, 2, -1, (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed, -1);
            dOfficeMe = OfficeMemberManager.SelectReportOfficeMembers(OfficeEngoId, -1, -1, -1);
        else if (DocOffIncreaseJobCapacityType == TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice)
            dOfficeMe = OfficeMemberManager.spReportEngOfficeMembers(OfficeEngoId);
        //  OfficeMemberManager.FindEngOfficeActiveMembers(OfficeEngoId, 0, -1);//****اعضای دفتر تنها از اعضای حقیقی هستند

        return dOfficeMe;
    }
    #endregion

    #region محاسبه ظرفیت مصرف شده-باقیمانده
    /// <summary>
    /// محاسبه ظرفیت مصرف شده
    /// </summary>
    /// <param name="ProjectIngridientTypeId">برای بدست آوردن متراژ کارکرد</param>
    /// <param name="MeOfficeOthPEngOTypeId">برای بدست آوردن تعداد کار</param>
    /// <param name="MeOfficeEngOId"></param>
    /// <param name="MemberTypeId">MeOfficeOthPEngOTypeId</param>
    /// <returns></returns>
    public UsedCapacityProjectCapacityDecrement UsedCapacity(int ProjectIngridientTypeId, int ActivityIngridientTypeIdForCounntWork, int MeOfficeEngOId, TSP.DataManager.TSMemberType MemberTypeId, Boolean Under400Meter, int CitId, TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, int DiscountPercentCode, int DiscountPercentCodeException)
    {
        return ProjectCapacityDecrementManager.FindSumUsedCapacity(MeOfficeEngOId, ProjectIngridientTypeId, ActivityIngridientTypeIdForCounntWork, (int)MemberTypeId, -1, "", "", 0, 1, Under400Meter ? 1 : -1, CitId, DiscountPercentCode, DiscountPercentCodeException);
    }

    public MemberCapacityInWorkRequestTable GetCapacityInformationBasedOnWorkRequestTable(int MeId)
    {
        MemberCapacityInWorkRequestTable MemberCapacityInWorkRequestTable = new MemberCapacityInWorkRequestTable();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        DataTable dtObsReq = ObserverWorkRequestManager.SelectTSObserverWorkRequestFullInfoByMember(MeId, TSObserverWorkRequestStatus.Confirm);
        if (dtObsReq.Rows.Count == 0) return MemberCapacityInWorkRequestTable;
        //MemberCapacityInWorkRequestTable. = Convert.ToInt32(dtObsReq.Rows[0][""]);
        MemberCapacityInWorkRequestTable.BonyadMaskan = Convert.ToInt32(dtObsReq.Rows[0]["BonyadMaskan"]);
        MemberCapacityInWorkRequestTable.BonyadMaskanDesignMeter = Convert.ToInt32(dtObsReq.Rows[0]["BonyadMaskanDesignMeter"]);
        MemberCapacityInWorkRequestTable.CapacityDesign = Convert.ToInt32(dtObsReq.Rows[0]["CapacityDesign"]);

        MemberCapacityInWorkRequestTable.CapacityObs = Convert.ToInt32(dtObsReq.Rows[0]["CapacityObs"]);
        MemberCapacityInWorkRequestTable.CountInproccesWorks = Convert.ToInt32(dtObsReq.Rows[0]["CountInproccesWorks"]);
        if (!Utility.IsDBNullOrNullValue(dtObsReq.Rows[0]["CountRandomSelected"]))
            MemberCapacityInWorkRequestTable.CountRandomSelected = Convert.ToInt32(dtObsReq.Rows[0]["CountRandomSelected"]);
        else
            MemberCapacityInWorkRequestTable.CountRandomSelected = 0;
        if (!Utility.IsDBNullOrNullValue(dtObsReq.Rows[0]["CountRejectByObs"]))
            MemberCapacityInWorkRequestTable.CountRejectByObs = Convert.ToInt32(dtObsReq.Rows[0]["CountRejectByObs"]);
        else
            MemberCapacityInWorkRequestTable.CountRejectByObs = 0;
        MemberCapacityInWorkRequestTable.CountRemainWorkCount = Convert.ToInt32(dtObsReq.Rows[0]["CountRemainWorkCount"]);
        MemberCapacityInWorkRequestTable.CountUnder400MeterWork = Convert.ToInt32(dtObsReq.Rows[0]["CountUnder400MeterWork"]);
        MemberCapacityInWorkRequestTable.CountUnder400MeterWorkDesign = Convert.ToInt32(dtObsReq.Rows[0]["CountUnder400MeterWorkDesign"]);
        MemberCapacityInWorkRequestTable.CountWorks = Convert.ToInt32(dtObsReq.Rows[0]["CountWorks"]);
        MemberCapacityInWorkRequestTable.PercentOfCapacityUsage = Convert.ToInt32(dtObsReq.Rows[0]["PercentOfCapacityUsage"]);
        MemberCapacityInWorkRequestTable.RemainCapacity = Convert.ToInt32(dtObsReq.Rows[0]["RemainCapacity"]);
        MemberCapacityInWorkRequestTable.RemainCapacityDesign = Convert.ToInt32(dtObsReq.Rows[0]["RemainCapacityDesign"]);
        MemberCapacityInWorkRequestTable.RemainCapacityObs = Convert.ToInt32(dtObsReq.Rows[0]["RemainCapacityObs"]);
        MemberCapacityInWorkRequestTable.RemainCapacityObsReal = Convert.ToInt32(dtObsReq.Rows[0]["RemainCapacityObsReal"]);
        MemberCapacityInWorkRequestTable.ShirazMunicipalityDesignMeter = Convert.ToInt32(dtObsReq.Rows[0]["ShirazMunicipalityDesignMeter"]);
        MemberCapacityInWorkRequestTable.ShirazMunicipalityMeter = Convert.ToInt32(dtObsReq.Rows[0]["ShirazMunicipalityMeter"]);
        MemberCapacityInWorkRequestTable.ShirazMunicipulityUrbenismEntebaghShahri = Convert.ToInt32(dtObsReq.Rows[0]["ShirazMunicipulityUrbenismEntebaghShahri"]);
        MemberCapacityInWorkRequestTable.ShirazMunicipulityUrbenismTarh = Convert.ToInt32(dtObsReq.Rows[0]["ShirazMunicipulityUrbenismTarh"]);
        MemberCapacityInWorkRequestTable.TotalCapacity = Convert.ToInt32(dtObsReq.Rows[0]["TotalCapacity"]);
        MemberCapacityInWorkRequestTable.UsedCapacity = Convert.ToInt32(dtObsReq.Rows[0]["UsedCapacity"]);
        MemberCapacityInWorkRequestTable.UsedCapacityCharity = Convert.ToInt32(dtObsReq.Rows[0]["UsedCapacityCharity"]);
        MemberCapacityInWorkRequestTable.UsedCapacityDesOtherCities = Convert.ToInt32(dtObsReq.Rows[0]["UsedCapacityDesOtherCities"]);
        MemberCapacityInWorkRequestTable.UsedCapacityDesShirazMun = Convert.ToInt32(dtObsReq.Rows[0]["UsedCapacityDesShirazMun"]);
        MemberCapacityInWorkRequestTable.UsedCapacityEntebaghShahriOtherCities = Convert.ToInt32(dtObsReq.Rows[0]["UsedCapacityEntebaghShahriOtherCities"]);
        MemberCapacityInWorkRequestTable.UsedCapacityEntebaghShahriShirazMun = Convert.ToInt32(dtObsReq.Rows[0]["UsedCapacityEntebaghShahriShirazMun"]);
        MemberCapacityInWorkRequestTable.UsedCapacityObsOtherCities = Convert.ToInt32(dtObsReq.Rows[0]["UsedCapacityObsOtherCities"]);
        MemberCapacityInWorkRequestTable.UsedCapacityObsSadra = Convert.ToInt32(dtObsReq.Rows[0]["UsedCapacityObsSadra"]);
        MemberCapacityInWorkRequestTable.UsedCapacityObsShiraz = Convert.ToInt32(dtObsReq.Rows[0]["UsedCapacityObsShiraz"]);
        MemberCapacityInWorkRequestTable.UsedCapacityUrbenismTarhOtherCities = Convert.ToInt32(dtObsReq.Rows[0]["UsedCapacityUrbenismTarhOtherCities"]);
        MemberCapacityInWorkRequestTable.UsedCapacityUrbenismTarhShirazMun = Convert.ToInt32(dtObsReq.Rows[0]["UsedCapacityUrbenismTarhShirazMun"]);
        MemberCapacityInWorkRequestTable.WantCharityWork = Convert.ToBoolean(dtObsReq.Rows[0]["WantCharityWork"]);
        MemberCapacityInWorkRequestTable.WantShahrakSanatiMeter = Convert.ToBoolean(dtObsReq.Rows[0]["WantShahrakSanatiMeter"]);
        //............
        return MemberCapacityInWorkRequestTable;
    }

    /// <summary>
    /// تعداد کار و  ظرفیت عضو را چک می کند
    /// </summary>
    public string CheckCapacityAndJobCount(int MeId, int CapacityDecreament, int ProjectCitId, TSP.DataManager.TSProjectIngridientType TSProjectIngridientType, TSUrbanismQualificationType? TSUrbanismQualificationType, Boolean HasOffice, Boolean IsBonyadMaskan, int ProjectId)
    {
        TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();

        int Error = 0;
        ObserverWorkRequestManager.FindByMeId(MeId);
        if (ObserverWorkRequestManager.Count == 0)
        {
            return FindErrorMessage((int)CapacityErr.CanNotFindInfo);
        }
        Boolean IsMemberInProject = false;
        int _CountWorkUnder400 = Convert.ToInt32(ObserverWorkRequestManager[0]["WorkCountUnder400CapAssign"]);
        int ConditionalCapacityDesign = ConditionalCapacityManager.SelectTSConditionalCapacitySum(MeId, Utility.GetDateOfToday(), (int)TSProjectIngridientType.Designer);
        int ConditionalCapacityObs = ConditionalCapacityManager.SelectTSConditionalCapacitySum(MeId, Utility.GetDateOfToday(), (int)TSProjectIngridientType.Observer);//برای افزایش ظرفیت نظارت بر اساس
        int ConditionalCapacityTotal = ConditionalCapacityDesign + ConditionalCapacityObs;
        MemberCapacityInWorkRequestTable MemberCapacityInWorkRequestTbl = GetCapacityInformationBasedOnWorkRequestTable(MeId);

        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        DataTable dtMeProject = ProjectCapacityDecrementManager.SelectTSProjectCapacityDecrementByMember(MeId, ProjectId, (int)TSProjectIngridientType);
        if (dtMeProject.Rows.Count > 0)
            IsMemberInProject = true;
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementAll = UsedCapacity(-1, -1, MeId, TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementAllShiraz = UsedCapacity(-1, -1, MeId, TSMemberType.Member, false, (int)CityCode.Shiraz, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementObsunder400 = UsedCapacity(-1, -1, MeId, TSMemberType.Member, true, -1, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementdes = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSProjectIngridientType.Designer, MeId, TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementdesShiraz = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSProjectIngridientType.Designer, MeId, TSMemberType.Member, false, (int)CityCode.Shiraz, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);

        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementobs = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, MeId, TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementobsShiraz = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, MeId, TSMemberType.Member, false, (int)CityCode.Shiraz, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);

        #region شرایط بنیاد مسکن
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementBonyadMaskanObs = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, MeId, TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, (int)TSDiscountPercent.BonyadMaskan, -1);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementBonyadMaskanDes = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSProjectIngridientType.Designer, MeId, TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, (int)TSDiscountPercent.BonyadMaskan, -1);
        if (UsedCapacityProjectCapacityDecrementBonyadMaskanObs.UsedCapacitySumCapacityDecrement > MemberCapacityInWorkRequestTbl.BonyadMaskan)
        {
            return FindErrorMessage((int)CapacityErr.HaveObsBonyadMaskanMoreThanLimitation);
        }
        if (UsedCapacityProjectCapacityDecrementBonyadMaskanDes.UsedCapacitySumCapacityDecrement > MemberCapacityInWorkRequestTbl.BonyadMaskanDesignMeter)
        {
            return FindErrorMessage((int)CapacityErr.HaveDesBonyadMaskanMoreThanLimitation);
        }
        if (IsBonyadMaskan)
        {
            switch (TSProjectIngridientType)
            {
                case TSProjectIngridientType.Designer:
                    if (UsedCapacityProjectCapacityDecrementBonyadMaskanDes.UsedCapacitySumCapacityDecrement + CapacityDecreament > MemberCapacityInWorkRequestTbl.BonyadMaskanDesignMeter)
                    {
                        return FindErrorMessage((int)CapacityErr.DoNotHaveCapacityForDesBonyadMaskan);
                    }
                    break;
                case TSProjectIngridientType.Observer:
                    if (UsedCapacityProjectCapacityDecrementBonyadMaskanObs.UsedCapacitySumCapacityDecrement + CapacityDecreament > MemberCapacityInWorkRequestTbl.BonyadMaskan)
                    {
                        return FindErrorMessage((int)CapacityErr.DoNotHaveCapacityForObsBonyadMaskan);
                    }
                    break;
            }
        }
        #endregion
        if (UsedCapacityProjectCapacityDecrementAll.UsedCapacitySumCapacityDecrement + CapacityDecreament > MemberCapacityInWorkRequestTbl.TotalCapacity + ConditionalCapacityTotal)
        {
            return FindErrorMessage((int)CapacityErr.NotEnoughCapacityAndMaxJobIsTaken);
        }
        ////??????????????
        if (MemberCapacityInWorkRequestTbl.RemainCapacity + ConditionalCapacityTotal < CapacityDecreament && MemberCapacityInWorkRequestTbl.CountRemainWorkCount <= 0
            && UsedCapacityProjectCapacityDecrementObsunder400.UsedCapacityCountProject > _CountWorkUnder400)
            return FindErrorMessage((int)CapacityErr.NotEnoughCapacityAndMaxJobIsTaken);
        ////??????????????
        if (IsBonyadMaskan)
        {
            if (MemberCapacityInWorkRequestTbl.RemainCapacity + ConditionalCapacityTotal
                + ((MemberCapacityInWorkRequestTbl.BonyadMaskanDesignMeter + MemberCapacityInWorkRequestTbl.BonyadMaskan)
                - (UsedCapacityProjectCapacityDecrementBonyadMaskanDes.UsedCapacitySumCapacityDecrement + UsedCapacityProjectCapacityDecrementBonyadMaskanObs.UsedCapacitySumCapacityDecrement))

                < CapacityDecreament)
                return FindErrorMessage((int)CapacityErr.NotEnoughRmainCapacity);
        }
        else
        {

            if (MemberCapacityInWorkRequestTbl.RemainCapacity + ConditionalCapacityTotal < CapacityDecreament)
                return FindErrorMessage((int)CapacityErr.NotEnoughRmainCapacity);
        }
        if (MemberCapacityInWorkRequestTbl.CountRemainWorkCount <= 0 && UsedCapacityProjectCapacityDecrementObsunder400.UsedCapacityCountProject > _CountWorkUnder400 && !IsMemberInProject)
            return FindErrorMessage((int)CapacityErr.MaxJobIsTaken);
        ////////////////////////////////////////
        if (ProjectCitId == (int)CityCode.Shiraz)
        {

        }
        else
        {//حدود کل ظرفیت برای ثبت کار جدید در خارج از شیراز
            if (IsBonyadMaskan)
            {
                if (UsedCapacityProjectCapacityDecrementAll.UsedCapacitySumCapacityDecrement - UsedCapacityProjectCapacityDecrementAllShiraz.UsedCapacitySumCapacityDecrement//کل کارهای خارج از شیراز
                + MemberCapacityInWorkRequestTbl.ShirazMunicipalityDesignMeter + MemberCapacityInWorkRequestTbl.ShirazMunicipalityMeter//نظارت و طراحی شهرداری شیراز
                + MemberCapacityInWorkRequestTbl.BonyadMaskanDesignMeter + MemberCapacityInWorkRequestTbl.BonyadMaskan //نظارت و طراحی بنیاد مسکن
                > MemberCapacityInWorkRequestTbl.TotalCapacity + ConditionalCapacityTotal)
                    return FindErrorMessage((int)CapacityErr.NotEnoughRmainCapacity);
            }
            else
            {
                if (CapacityDecreament
                + UsedCapacityProjectCapacityDecrementAll.UsedCapacitySumCapacityDecrement - UsedCapacityProjectCapacityDecrementAllShiraz.UsedCapacitySumCapacityDecrement//کل کارهای خارج از شیراز
                + MemberCapacityInWorkRequestTbl.ShirazMunicipalityDesignMeter + MemberCapacityInWorkRequestTbl.ShirazMunicipalityMeter//نظارت و طراحی شهرداری شیراز
                + MemberCapacityInWorkRequestTbl.BonyadMaskanDesignMeter + MemberCapacityInWorkRequestTbl.BonyadMaskan //نظارت و طراحی بنیاد مسکن
                > MemberCapacityInWorkRequestTbl.TotalCapacity + ConditionalCapacityTotal)
                    return FindErrorMessage((int)CapacityErr.NotEnoughRmainCapacity);
            }
        }
        /////////////////////////////////////////
        if (TSProjectIngridientType == TSProjectIngridientType.Designer && HasOffice)
        {
            if (IsBonyadMaskan)
            {
                if (MemberCapacityInWorkRequestTbl.RemainCapacity + ConditionalCapacityTotal
                    + ((MemberCapacityInWorkRequestTbl.BonyadMaskanDesignMeter + MemberCapacityInWorkRequestTbl.BonyadMaskan)
                    - (UsedCapacityProjectCapacityDecrementBonyadMaskanDes.UsedCapacitySumCapacityDecrement + UsedCapacityProjectCapacityDecrementBonyadMaskanObs.UsedCapacitySumCapacityDecrement))

                    < CapacityDecreament)
                    return FindErrorMessage((int)CapacityErr.NotEnoughRmainCapacity);
            }
            else
            {
                if (MemberCapacityInWorkRequestTbl.RemainCapacity + ConditionalCapacityTotal < CapacityDecreament)
                {
                    return FindErrorMessage((int)CapacityErr.NotEnoughRmainCapacity);
                }
            }
            switch (ProjectCitId)
            {
                case (int)CityCode.Shiraz:
                    if (MemberCapacityInWorkRequestTbl.ShirazMunicipalityDesignMeter < CapacityDecreament)
                    {
                        return FindErrorMessage((int)CapacityErr.MoreThanShirazMuDesignMeterEnteredInWorkReq);
                    }
                    if (MemberCapacityInWorkRequestTbl.ShirazMunicipalityDesignMeter - UsedCapacityProjectCapacityDecrementdesShiraz.UsedCapacitySumCapacityDecrement < CapacityDecreament)//ظرفیت بافیمانده این شهر (شهرداری شیراز)را داشته باشد
                    {
                        return FindErrorMessage((int)CapacityErr.DoesNotHaveEnoughCapacityForShirazMunDesign);
                    }
                    if (IsBonyadMaskan)
                    {
                        if (CapacityDecreament + UsedCapacityProjectCapacityDecrementdes.UsedCapacitySumCapacityDecrement
                       > MemberCapacityInWorkRequestTbl.CapacityDesign + ConditionalCapacityDesign)
                        {// "متراژ وارد شده بیشتر از میزان ظرفیت باقیمانده برای طراحی  می باشد"
                            return FindErrorMessage((int)CapacityErr.DoesNotHaveEnoughCapacityForDesignInShiraz);
                        }
                        if (CapacityDecreament + UsedCapacityProjectCapacityDecrementdesShiraz.UsedCapacitySumCapacityDecrement//مصرفی طراحی شیراز
                            + MemberCapacityInWorkRequestTbl.ShirazMunicipalityMeter//نظارت شهرداری شیراز
                            + UsedCapacityProjectCapacityDecrementAll.UsedCapacitySumCapacityDecrement - UsedCapacityProjectCapacityDecrementAllShiraz.UsedCapacitySumCapacityDecrement//کل مصرفی خارج از شیراز                            
                            + UsedCapacityProjectCapacityDecrementBonyadMaskanDes.UsedCapacitySumCapacityDecrement + UsedCapacityProjectCapacityDecrementBonyadMaskanObs.UsedCapacitySumCapacityDecrement//مصرفی بیناد مسکن
                            > MemberCapacityInWorkRequestTbl.TotalCapacity + ConditionalCapacityTotal)
                        {//"با توجه به متراژهای وارد شده در آماده بکاری و مجموع متراژ کارهای ثبت شده در سیستم ظرفیت حداکثر در برش زمانی تکمیل شده است "
                            return FindErrorMessage((int)CapacityErr.DoesNotHaveEnoughDesign_SumUsedAndMunAndBonyadAndUsedMoreThanTotalCapacity);
                        }
                    }
                    else
                    {
                        if (CapacityDecreament + UsedCapacityProjectCapacityDecrementdes.UsedCapacitySumCapacityDecrement + MemberCapacityInWorkRequestTbl.BonyadMaskanDesignMeter
                                             > MemberCapacityInWorkRequestTbl.CapacityDesign + ConditionalCapacityDesign)
                        {// "متراژ وارد شده بیشتر از میزان ظرفیت باقیمانده برای طراحی  می باشد"
                            return FindErrorMessage((int)CapacityErr.DoesNotHaveEnoughCapacityForDesignInShiraz);
                        }
                        if (CapacityDecreament + UsedCapacityProjectCapacityDecrementdesShiraz.UsedCapacitySumCapacityDecrement//مصرفی طراحی شیراز
                            + MemberCapacityInWorkRequestTbl.ShirazMunicipalityMeter//نظارت شهرداری شیراز
                            + UsedCapacityProjectCapacityDecrementAll.UsedCapacitySumCapacityDecrement - UsedCapacityProjectCapacityDecrementAllShiraz.UsedCapacitySumCapacityDecrement//کل مصرفی خارج از شیراز
                            + MemberCapacityInWorkRequestTbl.BonyadMaskanDesignMeter + MemberCapacityInWorkRequestTbl.BonyadMaskan//مجموع بنیاد مسکن

                            > MemberCapacityInWorkRequestTbl.TotalCapacity + ConditionalCapacityTotal)
                        {//"با توجه به متراژهای وارد شده در آماده بکاری و مجموع متراژ کارهای ثبت شده در سیستم ظرفیت حداکثر در برش زمانی تکمیل شده است "
                            return FindErrorMessage((int)CapacityErr.DoesNotHaveEnoughDesign_SumUsedAndMunAndBonyadAndUsedMoreThanTotalCapacity);
                        }
                    }
                    break;
                default:
                    //****چک کردن داشتن ظرفیت طراحی برای خارج از شیراز
                    if (IsBonyadMaskan)
                    {
                        if (CapacityDecreament + MemberCapacityInWorkRequestTbl.ShirazMunicipalityDesignMeter
                         + (UsedCapacityProjectCapacityDecrementdes.UsedCapacitySumCapacityDecrement - UsedCapacityProjectCapacityDecrementdesShiraz.UsedCapacitySumCapacityDecrement)//کارکرد طراحی خارج از شیراز
                          + UsedCapacityProjectCapacityDecrementBonyadMaskanDes.UsedCapacitySumCapacityDecrement > MemberCapacityInWorkRequestTbl.CapacityDesign + ConditionalCapacityDesign
                         )
                        {
                            return FindErrorMessage((int)CapacityErr.DoesNotHaveEnoughCapacityForDesign);
                        }
                    }
                    else
                    {
                        if (CapacityDecreament + MemberCapacityInWorkRequestTbl.ShirazMunicipalityDesignMeter
                        + (UsedCapacityProjectCapacityDecrementdes.UsedCapacitySumCapacityDecrement - UsedCapacityProjectCapacityDecrementdesShiraz.UsedCapacitySumCapacityDecrement)//کارکرد طراحی خارج از شیراز
                        + MemberCapacityInWorkRequestTbl.BonyadMaskanDesignMeter > MemberCapacityInWorkRequestTbl.CapacityDesign + ConditionalCapacityDesign
                        )
                        {
                            return FindErrorMessage((int)CapacityErr.DoesNotHaveEnoughCapacityForDesign);
                        }
                    }
                    break;
            }
        }
        else if ((TSProjectIngridientType == TSProjectIngridientType.Designer && !HasOffice) || TSProjectIngridientType == TSProjectIngridientType.Observer)
        {
            switch (ProjectCitId)
            {
                case (int)CityCode.Shiraz:
                    if (MemberCapacityInWorkRequestTbl.ShirazMunicipalityMeter < CapacityDecreament)
                    {
                        return FindErrorMessage((int)CapacityErr.MoreThanShirazMunObsObserverMeterEnteredInWorkReq);
                    }
                    if (MemberCapacityInWorkRequestTbl.ShirazMunicipalityMeter - UsedCapacityProjectCapacityDecrementobsShiraz.UsedCapacitySumCapacityDecrement < CapacityDecreament)//ظرفیت بافیمانده این شهر را داشته باشد
                    {
                        return FindErrorMessage((int)CapacityErr.DoesNotHaveEnoughCapacityForShirazMunObsObserver);
                    }
                    if (IsBonyadMaskan)
                    {
                        //+UsedCapacityProjectCapacityDecrementBonyadMaskanDes.UsedCapacitySumCapacityDecrement + UsedCapacityProjectCapacityDecrementBonyadMaskanObs.UsedCapacitySumCapacityDecrement
                        if (CapacityDecreament
                          + UsedCapacityProjectCapacityDecrementobs.UsedCapacitySumCapacityDecrement//کل کارهای نظارت ثبت شده در سیستم
                         + UsedCapacityProjectCapacityDecrementBonyadMaskanObs.UsedCapacitySumCapacityDecrement
                          > MemberCapacityInWorkRequestTbl.CapacityObs + ConditionalCapacityObs)
                        {//"ظرفیت نظارت کافی نمی باشد"
                            return FindErrorMessage((int)CapacityErr.DoNotHaveObserverCapacity);
                        }
                        if (CapacityDecreament
                            + UsedCapacityProjectCapacityDecrementAll.UsedCapacitySumCapacityDecrement - UsedCapacityProjectCapacityDecrementAllShiraz.UsedCapacitySumCapacityDecrement//کل کار خارج از شیراز
                            + MemberCapacityInWorkRequestTbl.ShirazMunicipalityDesignMeter
                            + UsedCapacityProjectCapacityDecrementobsShiraz.UsedCapacitySumCapacityDecrement//کارهای نظارت ثبت شده برای شیراز
                             + UsedCapacityProjectCapacityDecrementBonyadMaskanDes.UsedCapacitySumCapacityDecrement + UsedCapacityProjectCapacityDecrementBonyadMaskanObs.UsedCapacitySumCapacityDecrement//مجموع مصرفی بنیاد مسکن نظارت و طراحی
                             > MemberCapacityInWorkRequestTbl.TotalCapacity + ConditionalCapacityTotal)
                        {//"با توجه به متراژهای وارد شده در آماده بکاری و مجموع کارهای ثبت شده در سیستم ، دارای ظرفیت نمی باشد"
                            return FindErrorMessage((int)CapacityErr.DoNotHaveEnougCapcacityForShiraz);
                        }
                    }
                    else
                    {
                        if (CapacityDecreament
                            + UsedCapacityProjectCapacityDecrementobs.UsedCapacitySumCapacityDecrement//کل کارهای نظارت ثبت شده در سیستم
                           + MemberCapacityInWorkRequestTbl.BonyadMaskan
                            > MemberCapacityInWorkRequestTbl.CapacityObs + ConditionalCapacityObs)
                        {//"ظرفیت نظارت کافی نمی باشد"
                            return FindErrorMessage((int)CapacityErr.DoNotHaveObserverCapacity);
                        }
                        if (CapacityDecreament
                            + UsedCapacityProjectCapacityDecrementAll.UsedCapacitySumCapacityDecrement - UsedCapacityProjectCapacityDecrementAllShiraz.UsedCapacitySumCapacityDecrement//کل کار خارج از شیراز
                            + MemberCapacityInWorkRequestTbl.ShirazMunicipalityDesignMeter
                            + UsedCapacityProjectCapacityDecrementobsShiraz.UsedCapacitySumCapacityDecrement//کارهای نظارت ثبت شده برای شیراز
                             + MemberCapacityInWorkRequestTbl.BonyadMaskan + MemberCapacityInWorkRequestTbl.BonyadMaskanDesignMeter//مجموع بنیاد مسکن نظارت و طراحی
                             > MemberCapacityInWorkRequestTbl.TotalCapacity + ConditionalCapacityTotal)
                        {//"با توجه به متراژهای وارد شده در آماده بکاری و مجموع کارهای ثبت شده در سیستم ، دارای ظرفیت نمی باشد"
                            return FindErrorMessage((int)CapacityErr.DoNotHaveEnougCapcacityForShiraz);
                        }
                    }
                    break;
                default:
                    if (IsBonyadMaskan)
                    {
                        if (CapacityDecreament + MemberCapacityInWorkRequestTbl.ShirazMunicipalityMeter
                        + UsedCapacityProjectCapacityDecrementobs.UsedCapacitySumCapacityDecrement - UsedCapacityProjectCapacityDecrementobsShiraz.UsedCapacitySumCapacityDecrement
                         // + MemberCapacityInWorkRequestTbl.BonyadMaskan
                         + UsedCapacityProjectCapacityDecrementBonyadMaskanObs.UsedCapacitySumCapacityDecrement

                        > MemberCapacityInWorkRequestTbl.CapacityObs + ConditionalCapacityObs)
                        {
                            return FindErrorMessage((int)CapacityErr.DoNotHaveEnougCapcacityForObsOutOfShiraz);
                        }
                    }
                    else
                    {
                        if (CapacityDecreament + MemberCapacityInWorkRequestTbl.ShirazMunicipalityMeter
                            + UsedCapacityProjectCapacityDecrementobs.UsedCapacitySumCapacityDecrement - UsedCapacityProjectCapacityDecrementobsShiraz.UsedCapacitySumCapacityDecrement
                            + MemberCapacityInWorkRequestTbl.BonyadMaskan

                            > MemberCapacityInWorkRequestTbl.CapacityObs + ConditionalCapacityObs)
                        {
                            return FindErrorMessage((int)CapacityErr.DoNotHaveEnougCapcacityForObsOutOfShiraz);
                        }
                    }
                    break;
            }
            if (ProjectCitId != (int)CityCode.Shiraz &&
               MemberCapacityInWorkRequestTbl.CapacityObs + ConditionalCapacityObs - (MemberCapacityInWorkRequestTbl.ShirazMunicipalityMeter + MemberCapacityInWorkRequestTbl.BonyadMaskan) <= CapacityDecreament)
            {
                return FindErrorMessage((int)CapacityErr.DoesNotHaveEnoughCapacityForObserver);
            }
        }

        return FindErrorMessage(Error);
    }
    #endregion

    #region  توابع کسر ظرفیت هنگام ذخیره اطلاعات

    /// ProjectCapacityDecrementManager: Is Added To Transaction
    /// هنگام ثبت ناظر/طراح و یا مجری صدا زده می شود
    /// </summary>
    /// <param name="ProjectCapacityDecrementManager"></param>
    /// <param name="CapacityDecrement">متراژ کسر ظرفیت</param>
    /// <param name="Wage">متراژ دستمزد</param>
    /// <param name="ProjectIngridientTypeId"></param>
    /// <param name="PrjImpObsDsgnId"></param>
    /// <param name="OfficeId"> If the member is a member of an office</param>
    /// <returns> ReturnValue: PK</returns>
    public int InsertProjectCapacityDecrement(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, string CapacityDecrement, string Wage
    , Int16 ProjectIngridientTypeId, int ActivityIngridientTypeId, int PrjImpObsDsgnId, Nullable<int> OfficeId, int CurrentUserId, int MeOfficeOthPEngOId, int MeOfficeOthPEngOTypeId, int ProjectId, int IsFree, string DecreasedDate, Boolean SaveWithOutCondition, int IsWorkFree)
    {
        int ProjectCapacityDecrementId;
        DataRow rowProjectCapacityDecrement = ProjectCapacityDecrementManager.NewRow();

        rowProjectCapacityDecrement.BeginEdit();

        rowProjectCapacityDecrement["ProjectId"] = ProjectId;
        rowProjectCapacityDecrement["MeOfficeOthPEngOId"] = MeOfficeOthPEngOId;
        rowProjectCapacityDecrement["MeOfficeOthPEngOTypeId"] = MeOfficeOthPEngOTypeId;
        rowProjectCapacityDecrement["CapacityDecrement"] = CapacityDecrement;
        rowProjectCapacityDecrement["Wage"] = Wage;
        rowProjectCapacityDecrement["ProjectIngridientTypeId"] = ProjectIngridientTypeId;
        rowProjectCapacityDecrement["ActivityIngridientTypeId"] = ActivityIngridientTypeId;
        rowProjectCapacityDecrement["PrjImpObsDsgnId"] = PrjImpObsDsgnId;
        rowProjectCapacityDecrement["IsFree"] = IsFree;
        if (Convert.ToBoolean(IsFree))
        {
            rowProjectCapacityDecrement["FreeDate"] = Utility.GetDateOfToday();
        }
        rowProjectCapacityDecrement["IsWorkFree"] = IsWorkFree;
        if (Convert.ToBoolean(IsWorkFree))
        {
            rowProjectCapacityDecrement["WorkFreeDate"] = Utility.GetDateOfToday();
        }
        rowProjectCapacityDecrement["IsDecreased"] = 1;
        rowProjectCapacityDecrement["DecreasedDate"] = DecreasedDate;
        if (OfficeId.HasValue)
            rowProjectCapacityDecrement["OfficeId"] = OfficeId.Value;
        rowProjectCapacityDecrement["UserId"] = CurrentUserId;
        rowProjectCapacityDecrement["SaveWithOutCondition"] = SaveWithOutCondition;
        rowProjectCapacityDecrement["ModifiedDate"] = DateTime.Now;
        rowProjectCapacityDecrement.EndEdit();

        ProjectCapacityDecrementManager.AddRow(rowProjectCapacityDecrement);
        ProjectCapacityDecrementManager.Save();

        ProjectCapacityDecrementManager.DataTable.AcceptChanges();
        ProjectCapacityDecrementId = Convert.ToInt32(ProjectCapacityDecrementManager[0]["PrjCapacityDecrementId"]);



        return ProjectCapacityDecrementId;
    }

    public int UpdateProjectCapacityDecrement(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager
        , string CapacityDecrement, string Wage, Nullable<int> OfficeId
        , Int16 ProjectIngridientTypeId, int ActivityIngridientTypeId, int PrjImpObsDsgnId, int IsFree, int IsDecreased, int CurrentUserId, int ProjectId, Boolean SaveWithOutCondition)
    {

        int DiffrenceCapacity = 0;
        ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(ProjectId, PrjImpObsDsgnId, ProjectIngridientTypeId, ActivityIngridientTypeId);
        if (ProjectCapacityDecrementManager.Count > 0)
        {
            DiffrenceCapacity = Convert.ToInt32(CapacityDecrement) - Convert.ToInt32(ProjectCapacityDecrementManager[0]["CapacityDecrement"]);
            if (DiffrenceCapacity < 0 && string.Compare(ProjectCapacityDecrementManager[0]["DecreasedDate"].ToString(), "1398/03/25") < 0)
                DiffrenceCapacity = 0;
            ProjectCapacityDecrementManager[0].BeginEdit();
            ProjectCapacityDecrementManager[0]["CapacityDecrement"] = CapacityDecrement;
            ProjectCapacityDecrementManager[0]["Wage"] = Wage;
            if (OfficeId.HasValue)
                ProjectCapacityDecrementManager[0]["OfficeId"] = OfficeId.Value;
            ProjectCapacityDecrementManager[0]["ProjectIngridientTypeId"] = ProjectIngridientTypeId;
            ProjectCapacityDecrementManager[0]["ActivityIngridientTypeId"] = ActivityIngridientTypeId;
            if (PrjImpObsDsgnId != -1)
                ProjectCapacityDecrementManager[0]["PrjImpObsDsgnId"] = PrjImpObsDsgnId;
            if (IsFree != -1)
            {
                ProjectCapacityDecrementManager[0]["IsFree"] = Convert.ToBoolean(IsFree);
                ProjectCapacityDecrementManager[0]["IsWorkFree"] = Convert.ToBoolean(IsFree);
            }
            ProjectCapacityDecrementManager[0]["IsDecreased"] = Convert.ToBoolean(IsDecreased);
            ProjectCapacityDecrementManager[0]["SaveWithOutCondition"] = SaveWithOutCondition;
            ProjectCapacityDecrementManager[0]["UserId"] = CurrentUserId;
            ProjectCapacityDecrementManager[0]["ProjectId"] = ProjectId;
            ProjectCapacityDecrementManager[0]["ModifiedDate"] = DateTime.Now;
            ProjectCapacityDecrementManager[0].EndEdit();

            ProjectCapacityDecrementManager.Save();
        }
        return DiffrenceCapacity;
    }
    public int UpdateWorkRequestCapacityData(TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager, TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager
        , int MeId, int CurrentUserId, int ProjectId, int ProjectCitId, Boolean IsProjectCharity, TSP.DataManager.TSProjectIngridientType TSProjectIngridientType, TSUrbanismQualificationType? TSUrbanismQualificationType, Boolean IsObserverRandomSelect, Boolean IsObserverRejectWork, Boolean IsRemainWorkCountChecked)
    {
        #region 
        int Error = 0;
        Boolean IsMemberInProject = false;
        DataTable dtMeProject = ProjectCapacityDecrementManager.SelectTSProjectCapacityDecrementByMember(MeId, ProjectId, (int)TSProjectIngridientType);
        if (dtMeProject.Rows.Count > 1)
            IsMemberInProject = true;


        ObserverWorkRequestManager.FindByMeId(MeId);
        if (ObserverWorkRequestManager.Count == 0)
        {
            return (int)CapacityErr.CanNotFindInfo;
        }
        int _CountWorkUnder400 = Convert.ToInt32(ObserverWorkRequestManager[0]["WorkCountUnder400CapAssign"]);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementAll = UsedCapacity(-1, -1, MeId, TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementobs = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, MeId, TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementdes = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSProjectIngridientType.Designer, MeId, TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementObsunder400 = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, MeId, TSMemberType.Member, true, -1, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementDesunder400 = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSProjectIngridientType.Designer, MeId, TSMemberType.Member, true, -1, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);
        UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementCharity = UsedCapacity(-1, -1, MeId, TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, (int)TSDiscountPercent.Khayerin, -1);
        double UsedCapacityCountProjectAll = UsedCapacityProjectCapacityDecrementobs.UsedCapacityCountProject + UsedCapacityProjectCapacityDecrementdes.UsedCapacityCountProject;
        ObserverWorkRequestManager[0].BeginEdit();
        #region Used Capacity تنظیم فیلد های مصرفی
        if (IsProjectCharity)
        {//جمع خیریه در مصرفی کل حساب نمی شود
            ObserverWorkRequestManager[0]["UsedCapacityCharity"] = UsedCapacityProjectCapacityDecrementCharity.UsedCapacitySumCapacityDecrement;
        }
        if (TSProjectIngridientType == TSProjectIngridientType.Designer)
        {
            UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementDesignByCity = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSProjectIngridientType.Designer, MeId, TSMemberType.Member, false, ProjectCitId, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);
            //برای شهرسازی اعمال شود*******
            //UsedCapacityUrbenismTarhShirazMun
            //UsedCapacityEntebaghShahriShirazMun
            //UsedCapacityUrbenismTarhOtherCities
            //UsedCapacityEntebaghShahriOtherCities
            switch (ProjectCitId)
            {
                case (int)CityCode.Shiraz:
                    ObserverWorkRequestManager[0]["UsedCapacityDesShirazMun"] = UsedCapacityProjectCapacityDecrementDesignByCity.UsedCapacitySumCapacityDecrement;
                    break;
                default:
                    ObserverWorkRequestManager[0]["UsedCapacityDesOtherCities"] = UsedCapacityProjectCapacityDecrementDesignByCity.UsedCapacitySumCapacityDecrement;
                    break;
            }
        }
        else if (TSProjectIngridientType == TSProjectIngridientType.Observer)
        {
            UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementObsByCity = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, MeId, TSMemberType.Member, false, ProjectCitId, ProjectCapacityDecrementManager, -1, (int)TSDiscountPercent.BonyadMaskan);
            switch (ProjectCitId)
            {
                case (int)CityCode.Shiraz:
                    ObserverWorkRequestManager[0]["UsedCapacityObsShiraz"] = UsedCapacityProjectCapacityDecrementObsByCity.UsedCapacitySumCapacityDecrement;
                    break;
                case (int)CityCode.Sadra:
                    ObserverWorkRequestManager[0]["UsedCapacityObsSadra"] = UsedCapacityProjectCapacityDecrementObsByCity.UsedCapacitySumCapacityDecrement;
                    break;
                case (int)CityCode.Lapooy:
                    ObserverWorkRequestManager[0]["UsedCapacityObsLapooy"] = UsedCapacityProjectCapacityDecrementObsByCity.UsedCapacitySumCapacityDecrement;
                    break;
                case (int)CityCode.KhanZenyan:
                    ObserverWorkRequestManager[0]["UsedCapacityObsKhanZenyan"] = UsedCapacityProjectCapacityDecrementObsByCity.UsedCapacitySumCapacityDecrement;
                    break;
                case (int)CityCode.Dareyon:
                    ObserverWorkRequestManager[0]["UsedCapacityObsDareyon"] = UsedCapacityProjectCapacityDecrementObsByCity.UsedCapacitySumCapacityDecrement;
                    break;
                case (int)CityCode.Zarghan:
                    ObserverWorkRequestManager[0]["UsedCapacityObsZarghan"] = UsedCapacityProjectCapacityDecrementObsByCity.UsedCapacitySumCapacityDecrement;
                    break;
                default:
                    ObserverWorkRequestManager[0]["UsedCapacityObsOtherCities"] = UsedCapacityProjectCapacityDecrementObsByCity.UsedCapacitySumCapacityDecrement;
                    break;
            }

        }
        int SumBonyad = Convert.ToInt32(ObserverWorkRequestManager[0]["BonyadMaskan"]) + Convert.ToInt32(ObserverWorkRequestManager[0]["BonyadMaskanDesignMeter"]);
        int Sumused = SumBonyad + Convert.ToInt32(UsedCapacityProjectCapacityDecrementAll.UsedCapacitySumCapacityDecrement);

        ObserverWorkRequestManager[0]["UsedCapacity"] = UsedCapacityProjectCapacityDecrementAll.UsedCapacitySumCapacityDecrement + SumBonyad;

        if (Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacity"]) != Sumused)
        {
            return (int)CapacityErr.ConflictInUsedCapacity;
        }
        #endregion
        int MaxTotalCapacity = Convert.ToInt32(ObserverWorkRequestManager[0]["TotalCapacity"]);
        int MaxObs = Convert.ToInt32(ObserverWorkRequestManager[0]["CapacityObs"]);
        int MaxDes = Convert.ToInt32(ObserverWorkRequestManager[0]["CapacityDesign"]);
        int TotalUsedCapacityObs = Convert.ToInt32(ObserverWorkRequestManager[0]["BonyadMaskan"]) + Convert.ToInt32(UsedCapacityProjectCapacityDecrementobs.UsedCapacitySumCapacityDecrement);
        int TotalUsedCapacityDes = Convert.ToInt32(ObserverWorkRequestManager[0]["BonyadMaskanDesignMeter"]) + Convert.ToInt32(UsedCapacityProjectCapacityDecrementdes.UsedCapacitySumCapacityDecrement);

        #region Remains

        ObserverWorkRequestManager[0]["RemainCapacity"] = MaxTotalCapacity - (TotalUsedCapacityDes + TotalUsedCapacityObs);
        if (Convert.ToInt32(ObserverWorkRequestManager[0]["RemainCapacity"]) + Sumused != MaxTotalCapacity)
        {
            return (int)CapacityErr.ConflictInSumUsedAndRemainAndTotalCapacity;
        }
        ObserverWorkRequestManager[0]["RemainCapacityObs"] = MaxObs - TotalUsedCapacityObs;

        ObserverWorkRequestManager[0]["RemainCapacityObsReal"] =
        Math.Min(
          (MaxObs - (TotalUsedCapacityObs + (Convert.ToInt32(ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"]) - Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityObsShiraz"]))))
          , (MaxTotalCapacity - (TotalUsedCapacityObs + (Convert.ToInt32(ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"]) - Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityObsShiraz"])))
          - (TotalUsedCapacityDes + (Convert.ToInt32(ObserverWorkRequestManager[0]["ShirazMunicipalityDesignMeter"]) - Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityDesShirazMun"])))));
        //*********
        if (Convert.ToDouble(ObserverWorkRequestManager[0]["CapacityObs"]) == 0)
            ObserverWorkRequestManager[0]["PercentOfCapacityUsage"] = 0;
        else
            ObserverWorkRequestManager[0]["PercentOfCapacityUsage"] = (Convert.ToDouble(TotalUsedCapacityObs) + (Convert.ToInt32(ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"]) - Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityObsShiraz"]))) / Convert.ToDouble(ObserverWorkRequestManager[0]["CapacityObs"]);
        ObserverWorkRequestManager[0]["CountInproccesWorks"] = UsedCapacityProjectCapacityDecrementobs.UsedCapacityCountProject;
        ObserverWorkRequestManager[0]["CountInproccesWorksObs"] = UsedCapacityProjectCapacityDecrementobs.UsedCapacityCountProject;
        if (IsObserverRandomSelect)
            ObserverWorkRequestManager[0]["CountRandomSelected"] = Convert.ToInt32(ObserverWorkRequestManager[0]["CountRandomSelected"]) + 1;
        ObserverWorkRequestManager[0]["RemainCapacityDesign"] = MaxDes - TotalUsedCapacityDes;
        ObserverWorkRequestManager[0]["CountInproccesWorksDesign"] = UsedCapacityProjectCapacityDecrementdes.UsedCapacityCountProject;
        #endregion
        #region countWork Under400MeterWork
        ObserverWorkRequestManager[0]["CountUnder400MeterWork"] = UsedCapacityProjectCapacityDecrementObsunder400.UsedCapacityCountProject;
        ObserverWorkRequestManager[0]["CountUnder400MeterWorkDesign"] = UsedCapacityProjectCapacityDecrementDesunder400.UsedCapacityCountProject;
        double CountUnder400 = UsedCapacityProjectCapacityDecrementObsunder400.UsedCapacityCountProject + UsedCapacityProjectCapacityDecrementDesunder400.UsedCapacityCountProject;
        int CountWorks = Convert.ToInt32(ObserverWorkRequestManager[0]["CountWorks"]);
        if (CountUnder400 == 0 || CountUnder400 == 1)
        {
            ObserverWorkRequestManager[0]["CountRemainWorkCount"] = CountWorks - UsedCapacityCountProjectAll;
        }
        else if (CountUnder400 <= _CountWorkUnder400 - 1)
        {

            ObserverWorkRequestManager[0]["CountRemainWorkCount"] = CountWorks - (UsedCapacityCountProjectAll - (CountUnder400 - 1));
        }
        else
        {
            ObserverWorkRequestManager[0]["CountRemainWorkCount"] = CountWorks - (UsedCapacityCountProjectAll - (_CountWorkUnder400 - 1));

        }

        ObserverWorkRequestManager[0]["CountRejectByObs"] = Convert.ToInt32(ObserverWorkRequestManager[0]["CountRejectedObsWork"]);
        ObserverWorkRequestManager[0].EndEdit();
        ObserverWorkRequestManager.Save();
        ObserverWorkRequestManager.DataTable.AcceptChanges();
        Boolean MaxJobIsTaken = false;
        if (Convert.ToInt32(ObserverWorkRequestManager[0]["CountUnder400MeterWork"]) < 0
           || Convert.ToInt32(ObserverWorkRequestManager[0]["CountRemainWorkCount"]) < 0
           || Convert.ToInt32(ObserverWorkRequestManager[0]["CountUnder400MeterWorkDesign"]) < 0)
        {
            MaxJobIsTaken = true;
        }
        if (MaxJobIsTaken && !IsMemberInProject && IsRemainWorkCountChecked)
        {
            return (int)CapacityErr.MaxJobIsTaken;
        }
        if (Convert.ToInt32(ObserverWorkRequestManager[0]["CountRemainWorkCount"]) > Convert.ToInt32(ObserverWorkRequestManager[0]["CountWorks"]))
        {
            return (int)CapacityErr.ConflictInWorkCounts;
        }
        #endregion
        ///***از انجایی که ممکن است با افزایش متراژ ااجازه کار به افراد داده شود لذا ممکن است این عدد منفی شود و این شرط بر داشته شد ***********
        //////Convert.ToInt32(ObserverWorkRequestManager[0]["RemainCapacityObs"]) < 0
        //////    ||
        ///**************
        if (Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityObsShiraz"]) < 0
            || Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityObsSadra"]) < 0
            || Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityObsLapooy"]) < 0
            || Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityObsKhanZenyan"]) < 0
            || Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityObsDareyon"]) < 0
            || Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityObsZarghan"]) < 0
            || Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityObsOtherCities"]) < 0
            || Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityDesShirazMun"]) < 0
            || Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityDesOtherCities"]) < 0
             || Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityUrbenismTarhShirazMun"]) < 0
             || Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityEntebaghShahriShirazMun"]) < 0
             || Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityUrbenismTarhOtherCities"]) < 0
             || Convert.ToInt32(ObserverWorkRequestManager[0]["UsedCapacityEntebaghShahriOtherCities"]) < 0)
        {
            return (int)CapacityErr.ConflictInRemainCapacityObs;
        }

        return Error;
        #endregion

    }

    public int UpdateWorkRequestCapacityData(TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager, TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager
    , int MeId, int CurrentUserId, int ProjectId, int ProjectCitId, Boolean IsProjectCharity, TSP.DataManager.TSProjectIngridientType TSProjectIngridientType, TSUrbanismQualificationType? TSUrbanismQualificationType, Boolean IsObserverRandomSelect, Boolean IsObserverRejectWork)
    {
        return UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager
    , MeId, CurrentUserId, ProjectId, ProjectCitId, IsProjectCharity, TSProjectIngridientType, TSUrbanismQualificationType, IsObserverRandomSelect, IsObserverRejectWork, true);
    }
    #endregion

}

