using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using TSP.DataManager;

/// <summary>
/// توابع مربوط به ظرفیتها
/// </summary>

//namespace TSP.DataManager.TechnicalServices
//{
/// <summary>
/// توابع مربوط به ظرفیتها
/// </summary>
public class Capacity
{
    #region Private Members
    PartnertsMajorCombination _PartnertsMajorCombination;
    OfficeCapacity _OfficeCapacity;
    MemberCapacity _MemberCapacity;
    #endregion

    #region Properties
    #region CapacityInformation
    //private int _TotalCapacity = 0;
    //public int TotalCapacity
    //{
    //    get
    //    {
    //        return _TotalCapacity;
    //    }
    //    set
    //    {
    //        _TotalCapacity = value;
    //    }
    //}   

    private int _OfficeEngConditionalCapacity = 0;
    public int OfficeEngConditionalCapacity
    {
        get
        {
            return _OfficeEngConditionalCapacity;
        }
        set
        {
            _OfficeEngConditionalCapacity = value;
        }
    }

    #region Unkwon ?????
    //************
    private int _DesignInc = 0;
    public int DesignInc
    {
        get
        {
            return _DesignInc;
        }
        set
        {
            _DesignInc = value;
        }
    }

    private int _SameGradeInc = 0;
    public int SameGradeInc
    {
        get
        {
            return _SameGradeInc;
        }
        set
        {
            _SameGradeInc = value;
        }
    }

    private int _MajorInc = 0;
    public int MajorInc
    {
        get
        {
            return _MajorInc;
        }
        set
        {
            _MajorInc = value;
        }
    }
    //************
    #endregion
    #endregion

    #region IngridientInformation
    private int _IngridientTypeId = 0;
    public int IngridientTypeId
    {
        get
        {
            return _IngridientTypeId;
        }
        set
        {
            _IngridientTypeId = value;
        }
    }

    private int _IngredientMeEngOfId = 0;
    public int IngredientMeEngOfId
    {
        get
        {
            return _IngredientMeEngOfId;
        }
        set
        {
            _IngredientMeEngOfId = value;
        }
    }

    //حداکثر تعداد کار مجاز
    private int _IngridientMaxJobCount = 0;
    public int IngridientMaxJobCount
    {
        get
        {
            return _IngridientMaxJobCount;
        }
        set
        {
            _IngridientMaxJobCount = value;
        }
    }

    // حداکثر ظرفیت مجاز
    private int _IngridientMaxJobCapacity = 0;
    public int IngridientMaxJobCapacity
    {
        get
        {
            return _IngridientMaxJobCapacity;
        }
        set
        {
            _IngridientMaxJobCapacity = value;
        }
    }
    //ظرفیت کل طراحی
    private int _IngridientDesignCapacity = 0;
    public int IngridientDesignCapacity
    {
        get
        {
            return _IngridientDesignCapacity;
        }
        set
        {
            _IngridientDesignCapacity = value;
        }
    }
    //ظرفیت کل نظارت
    private int _IngridientObservationCapacity = 0;
    public int IngridientObservationCapacity
    {
        get
        {
            return _IngridientObservationCapacity;
        }
        set
        {
            _IngridientObservationCapacity = value;
        }
    }
    //ظرفیت کل اجرا
    private int _IngridientImplementCapacity = 0;
    public int IngridientImplementCapacity
    {
        get
        {
            return _IngridientImplementCapacity;
        }
        set
        {
            _IngridientImplementCapacity = value;
        }
    }
    //ظرفیت کاهش/افزایش طراحی
    private int _IngridientConditionalCapacityDesign = 0;
    public int IngridientConditionalCapacityDesign
    {
        get
        {
            return _IngridientConditionalCapacityDesign;
        }
        set
        {
            _IngridientConditionalCapacityDesign = value;
        }
    }
    //ظرفیت کاهش/افزایش نظارت
    private int _IngridientConditionalCapacityObserve = 0;
    public int IngridientConditionalCapacityObserve
    {
        get
        {
            return _IngridientConditionalCapacityObserve;
        }
        set
        {
            _IngridientConditionalCapacityObserve = value;
        }
    }
    //ظرفیت کاهش/افزایش اجرا
    private int _IngridientConditionalCapacityImplement = 0;
    public int IngridientConditionalCapacityImplement
    {
        get
        {
            return _IngridientConditionalCapacityImplement;
        }
        set
        {
            _IngridientConditionalCapacityImplement = value;
        }
    }
    //کل ظرفیت مصرف شده
    private int _IngridientUsedCapacityValue = 0;
    public int IngridientUsedCapacityValue
    {
        get
        {
            return _IngridientUsedCapacityValue;
        }
        set
        {
            _IngridientUsedCapacityValue = value;
        }
    }
    //کل ظرفیت مصرف شده طراحی
    private int _IngridientUsedDesignCapacityValue = 0;
    public int IngridientUsedDesignCapacityValue
    {
        get
        {
            return _IngridientUsedDesignCapacityValue;
        }
        set
        {
            _IngridientUsedDesignCapacityValue = value;
        }
    }
    //کل ظرفیت مصرف شده نظارت
    private int _IngridientUsedObserveCapacityValue = 0;
    public int IngridientUsedObserveCapacityValue
    {
        get
        {
            return _IngridientUsedObserveCapacityValue;
        }
        set
        {
            _IngridientUsedObserveCapacityValue = value;
        }
    }
    //کل ظرفیت مصرف شده اجرا
    private int _IngridientUsedImplementCapacityValue = 0;
    public int IngridientUsedImplementCapacityValue
    {
        get
        {
            return _IngridientUsedImplementCapacityValue;
        }
        set
        {
            _IngridientUsedImplementCapacityValue = value;
        }
    }
    //ظرفیت رزرو شده
    private int _IngridientReservedCapacityValue = 0;
    public int IngridientReservedCapacityValue
    {
        get
        {
            return _IngridientReservedCapacityValue;
        }
        set
        {
            _IngridientReservedCapacityValue = value;
        }
    }
    //ظرفیت باقیمانده
    private int _IngridientRemainCapacity = 0;
    public int IngridientRemainCapacity
    {
        get
        {
            return _IngridientRemainCapacity;
        }
        set
        {
            _IngridientRemainCapacity = value;
        }
    }
    //ظرفیت باقیمانده طراحی
    private int _IngridientRemainCapacityDesign = 0;
    public int IngridientRemainCapacityDesign
    {
        get
        {
            return _IngridientRemainCapacityDesign;
        }
        set
        {
            _IngridientRemainCapacityDesign = value;
        }
    }
    //ظرفیت باقیمانده نظارت
    private int _IngridientRemainCapacityObserve = 0;
    public int IngridientRemainCapacityObserve
    {
        get
        {
            return _IngridientRemainCapacityObserve;
        }
        set
        {
            _IngridientRemainCapacityObserve = value;
        }
    }
    //ظرفیت باقیمانده اجرا
    private int _IngridientRemainCapacityImplement = 0;
    public int IngridientRemainCapacityImplement
    {
        get
        {
            return _IngridientRemainCapacityImplement;
        }
        set
        {
            _IngridientRemainCapacityImplement = value;
        }
    }

    private int _IngridientIncreamentCapacityOfOfficeMembership = 0;
    public int IngridientIncreamentCapacityOfOfficeMembership
    {
        get
        {
            return _IngridientIncreamentCapacityOfOfficeMembership;
        }
        set
        {
            _IngridientIncreamentCapacityOfOfficeMembership = value;
        }
    }

    private int _IngridientProjectNum = 0;
    public int IngridientProjectNum
    {
        get
        {
            return _IngridientProjectNum;
        }
        set
        {
            _IngridientProjectNum = value;
        }
    }

    /// <summary>
    /// برای مجری مفهوم دارد
    /// </summary>
    private int _IngridientMaxFloor = 0;
    public int IngridientMaxFloor
    {
        get
        {
            return _IngridientMaxFloor;
        }
        set
        {
            _IngridientMaxFloor = value;
        }
    }


    private int _ObservationPercent = 1;
    public int ObservationPercent
    {
        get
        {
            return _ObservationPercent;
        }
        set
        {
            _ObservationPercent = value;
        }
    }

    private int _IngridientGrade = -1;
    public int IngridientGrade
    {
        get
        {
            return _IngridientGrade;
        }
        set
        {
            _IngridientGrade = value;
        }
    }

    //private int _IngridienTotalDsgCapacity = 0;
    //public int IngridienTotalDsgCapacity
    //{
    //    get
    //    {
    //        return _IngridienTotalDsgCapacity;
    //    }
    //    set
    //    {
    //        _IngridienTotalDsgCapacity = value;
    //    }
    //}

    //private int _IngridienTotalObsCapacity = 0;
    //public int TotalObsCapacity
    //{
    //    get
    //    {
    //        return _IngridienTotalObsCapacity;
    //    }
    //    set
    //    {
    //        _IngridienTotalObsCapacity = value;
    //    }
    //}
    #endregion

    #region CapacityAssignmentInformation
    private double _CapacityAssignmentCapacityPrcntSum = 0;
    public double CapacityAssignmentCapacityPrcntSum
    {
        get
        {
            return _CapacityAssignmentCapacityPrcntSum;
        }
        set
        {
            _CapacityAssignmentCapacityPrcntSum = value;
        }
    }

    private double _CapacityAssignmentJobCountPrcntSum = 0;
    public double CapacityAssignmentJobCountPrcntSum
    {
        get
        {
            return _CapacityAssignmentJobCountPrcntSum;
        }
        set
        {
            _CapacityAssignmentJobCountPrcntSum = value;
        }
    }
    #endregion
    #endregion

    #region Classes
    /// <summary>
    /// مشخصات ترکیب رشته شرکای دفتر/شرکت
    /// </summary>
    private class PartnertsMajorCombination
    {
        public int MainMajorNum = 0;
        public int SecondaryMajorNum = 0;
        public int TotalMajorNum = 0;
    }

    public class MemberCapacity
    {
        public int MemberConditionalCapacityDesign = 0;
        public int MemberConditionalCapacityObserve = 0;
        public int MemberConditionalCapacityImplement = 0;
        public int MemberGradeId = 0;
        public int MemberMajorId = 0;
        public string MemberMajorName = "";
        public string MemberFullName = "";
        public double MemberMaxJobCount = 0;
        public double MemberMaxJobCapacity = 0;//حداکثر ظرفیت مجاز نسبت به طراحی و یا اجرا که مروط به جداول کتاب مبحث دوم باشد
        public int MemberDesignCapacity = 0;
        public int MemberObservationCapacity = 0;
        public int MemberObservationPercent = 0;
        public int MemberCurrentProjectNum = 0;
        public int MemberImplementCapacity = 0;
        public int MemberMaxFloorCount = 0;

    }

    public class OfficeCapacity
    {
        public int OfficeMaxJobCount = 0;
        public int OfficeMaxJobCapacityDesign = 0;
        public int OfficeObservationCapacity = 0;
        public int OfficeDesignCapacity = 0;

        public int OfficeConditionalCapacityDesign = 0;
        public int OfficeConditionalCapacityObserve = 0;
        public int OfficeConditionalCapacityImplement = 0;
        public int OfficeMaxFloorCount = 0;
        public int OfficeGradeId = 0;
        public int OfficeActivityType = 0;

        public int DocOffOfficeMembersQualificationType = 0;

        //****Office Member**********
        public int OfficeObservationPercent = 0;
        public int OfficeMemberMaxJobCount = 0;
        public int OfficeMemberMaxJobCapacity = 0;
        public int OfficeMemberCurrentProjectNum = 0;

        public int OfficeMemberUsedCapacity = 0;
        public int OfficeMemberUsedCapacityDesign = 0;
        public int OfficeMemberUsedCapacityObservation = 0;
        public int OfficeMemberUsedCapacityImplement = 0;

        public int OfficeMemberRemainCapacity = 0;
        public int OfficeMemberRemainCapacityDesign = 0;
        public int OfficeMemberRemainCapacityObservation = 0;
        public int OfficeMemberRemainCapacityImplement = 0;

        public int OfficeMemberReservedCapacity = 0;
        public int OfficeMemberTotalDsgCapacity = 0;
        public int OfficeMemberTotalObsCapacity = 0;
        public int OfficeMemberIncreamentCapacityOfOfficeMembership = 0;
        //***************************
    }
    #endregion

    #region Enumerations
    private enum CapacityErr
    {
        CanNotFindInfo = -1,
        NotEnoughRmainCapacity = 1,
        MaxJobIsTaken = 2,
        NotEnoughCapacityAndMaxJobIsTaken = 3,
        NotEnoughStep = 4
    }

    public enum ConditionalCapacityType
    {
        Increased = 0,
        Decreased = 1
    }
    #endregion

    #region Constructors
    public Capacity()
    {
        _PartnertsMajorCombination = new PartnertsMajorCombination();
        _OfficeCapacity = new OfficeCapacity();
        _MemberCapacity = new MemberCapacity();
    }
    #endregion

    private string FindErrorMessage(int Err)
    {
        string Message = "";
        switch (Err)
        {
            case (int)CapacityErr.NotEnoughCapacityAndMaxJobIsTaken:
                Message = "ظرفیت باقیمانده عضو مورد نظر کافی نمی باشد و حداکثر تعداد کار مجاز نیز گرفته شده است";
                break;

            case (int)CapacityErr.NotEnoughRmainCapacity:
                Message = "ظرفیت باقیمانده عضو مورد نظر کافی نمی باشد.";
                break;

            case (int)CapacityErr.MaxJobIsTaken:
                Message = "حداکثر تعداد کار مجاز برای عضو مورد نظر گرفته شده است.";
                break;

            case (int)CapacityErr.CanNotFindInfo:
                Message = "اطلاعات عضو مورد نظر یافت نشد.";
                break;

            default:
                Message = "";
                break;
        }
        return Message;
    }

    #region توابع کلی محاسبه ظرفیت فرد، شرکت یا یک دفتر
    /// <summary>
    /// اطلاعات ظرفیت فرد، شرکت یا یک دفتر را بر می گرداند
    /// ArrayList[0]: TotalCapacity(int), ArrayList[1]:UsedCapacity(int) , ArrayList[2]: RemainCapacity(int), ArrayList[3]:ReservedCapacity(int) , ArrayList[4]: ProjectNum(int), ArrayList[5]: MaxJoubCount(string), ArrayList[6]: MaxFloor(string), ArrayList[7]: ConditionalCapacity(int)
    /// </summary>
    /// <param name="ProjectIngridientTypeId">نوع عامل پروژه: طراح، ناظر ،مجری،مالک</param>
    /// <param name="MemberTypeId"></param>
    /// <param name="MeOfficeEngOId"></param>
    /// <param name="CalculateByAssignmentPercent">CalculateByAssignmentPercent=true : calculate by PerStage</param>
    /// <returns></returns>    
    public void GetCapacityInformation(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, Boolean CalculateByAssignmentPercent)
    {
        //#region GetCapacityInfo
        //*********اطلاعات ظرفیت فرد، شرکت یا یک دفتر را بر اساس اختصاص ظرفیت بر می گرداند        
        switch (ProjectIngridientTypeId)
        {
            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                GetDsgObsTotalCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, CalculateByAssignmentPercent);
                break;
            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                GetDsgObsTotalCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, CalculateByAssignmentPercent);
                break;
            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                GetImplementTotalCapacity(MemberTypeId, MeOfficeEngOId, CalculateByAssignmentPercent);
                break;
        }

        //***TotalCapacity===>IngridiantMaxJobCapacity+ConditonalCapacity      
        IngridientUsedCapacityValue = GetTotalUsedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId, CalculateByAssignmentPercent, IngridientDesignCapacity, IngridientObservationCapacity);
        switch (ProjectIngridientTypeId)
        {
            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                IngridientRemainCapacity = IngridientRemainCapacityDesign = IngridientDesignCapacity - IngridientUsedCapacityValue;
                IngridientRemainCapacity = IngridientRemainCapacityObserve = IngridientObservationCapacity - IngridientUsedCapacityValue;
                break;
            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                IngridientRemainCapacity = IngridientRemainCapacityImplement = IngridientImplementCapacity - IngridientUsedCapacityValue;
                break;
        }

        IngridientReservedCapacityValue = GetTotalReservedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
        IngridientProjectNum = GetTotalProjectNum(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
        //*****IngridiantMaxJobCount
        //*****ConditionalCapacity===>IngrediantConditionalCapacity       
        //#endregion
        if (Convert.ToInt32(IngridientMaxFloor) == -1)
            IngridientMaxFloor = -2;// "بدون محدودیت";
        if (Convert.ToInt32(IngridientMaxJobCount) == -1)
            IngridientMaxJobCount = -2;// "بدون محدودیت";     
    }
    #endregion

    #region محاسبه ظرفیت طراحی و یا نظارت
    /// <summary>
    /// کل ظرفیت و تعداد کار مجاز فرد، شرکت یا یک دفتر طراحی و نظارت را بر می گرداند
    /// Set Value:  IngridientMaxJobCount,IngridientMaxJobCapacity,  IngridientObservationCapacity,IngridientDesignCapacity
    /// ,IngridientConditionalCapacityDesign,IngridientConditionalCapacityObserve,IngridientConditionalCapacityImplement
    /// ,IngridientGrade,ObservationPercent
    /// </summary>
    /// <param name="ProjectIngridientTypeId">نوع عامل پروژه : طراح/ناظر/مجری</param>
    /// <param name="MemberTypeId"></param>
    /// <param name="MeOfficeEngOId"></param>
    /// <param name="CapacityInfo"></param>
    public void GetDsgObsTotalCapacity(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, Boolean CalculateByAssignmentPercent)
    {
        switch (MemberTypeId)
        {
            case (int)TSP.DataManager.TSMemberType.Member:   //   MaxJobCount, MaxJobCapacity, ObservationPercent, ObservationCapacity,  Grade,  MjId, MeId,  MeName, ConditionalCapacity             
                MemberCapacity MeCapacity = GetMemberDesignObservationCapacity(MeOfficeEngOId, ProjectIngridientTypeId, -1, CalculateByAssignmentPercent);
                IngridientMaxJobCount = Convert.ToInt32(MeCapacity.MemberMaxJobCount);
                IngridientMaxJobCapacity = Convert.ToInt32(MeCapacity.MemberMaxJobCapacity);
                IngridientObservationCapacity = MeCapacity.MemberObservationCapacity;
                IngridientDesignCapacity = MeCapacity.MemberDesignCapacity;
                IngridientConditionalCapacityDesign = MeCapacity.MemberConditionalCapacityDesign;
                IngridientConditionalCapacityObserve = MeCapacity.MemberConditionalCapacityObserve;
                IngridientConditionalCapacityImplement = MeCapacity.MemberConditionalCapacityImplement;
                IngridientGrade = MeCapacity.MemberGradeId;
                ObservationPercent = MeCapacity.MemberObservationPercent;
                break;

            case (int)TSP.DataManager.TSMemberType.Office:   // MaxJobCount, MaxJobCapacity, ObservationCapacity,ConditionalCapacity             
                OfficeCapacity OffCapacity = GetOfficeDesignObservationCapacity(MeOfficeEngOId, ProjectIngridientTypeId, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office, CalculateByAssignmentPercent);
                IngridientMaxJobCount = Convert.ToInt32(OffCapacity.OfficeMaxJobCount);
                IngridientMaxJobCapacity = Convert.ToInt32(OffCapacity.OfficeMaxJobCapacityDesign);
                IngridientObservationCapacity = OffCapacity.OfficeObservationCapacity;
                IngridientDesignCapacity = OffCapacity.OfficeDesignCapacity;
                IngridientConditionalCapacityDesign = OffCapacity.OfficeConditionalCapacityDesign;
                IngridientConditionalCapacityObserve = OffCapacity.OfficeConditionalCapacityObserve;
                IngridientConditionalCapacityImplement = OffCapacity.OfficeConditionalCapacityImplement;
                break;

            case (int)TSP.DataManager.TSMemberType.EngOffice:   // MaxJobCount, MaxJobCapacity, ObservationCapacity,ConditionalCapacity             
                OfficeCapacity EngOffCapacity = GetOfficeDesignObservationCapacity(MeOfficeEngOId, ProjectIngridientTypeId, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice, CalculateByAssignmentPercent);
                IngridientMaxJobCount = Convert.ToInt32(EngOffCapacity.OfficeMaxJobCount);
                IngridientMaxJobCapacity = Convert.ToInt32(EngOffCapacity.OfficeMaxJobCapacityDesign);
                IngridientObservationCapacity = EngOffCapacity.OfficeObservationCapacity;
                IngridientDesignCapacity = EngOffCapacity.OfficeDesignCapacity;
                IngridientConditionalCapacityDesign = EngOffCapacity.OfficeConditionalCapacityDesign;
                IngridientConditionalCapacityObserve = EngOffCapacity.OfficeConditionalCapacityObserve;
                IngridientConditionalCapacityImplement = EngOffCapacity.OfficeConditionalCapacityImplement;

                break;
        }
    }

    #region بدست آوردن ظرفیت طراحی/نظارت شرکت یا دفتر

    #region GetOfficeDesignObservationCapacity =====>Private Methods
    /// <summary>
    /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت را بر اساس اختصاص ظرفیت بر می گرداند
    /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationCapacity, ArrayList[3]: ConditionalCapacity
    /// </summary>
    /// <param name="OfficeEngoId"></param>
    /// <param name="ProjectIngridientTypeId"></param>
    /// <param name="DocOffIncreaseJobCapacityType"></param>
    /// <param name="CalculateByCapacityAssignment">مشخص می کند آیا ظرفیت و تعداد کار براساس درصد اختصاص ظرفیت محاسبه شود</param>
    /// <param name="PersonId"> کد عضویت شخص خاص</param>
    /// <returns></returns>
    private OfficeCapacity GetOfficeDesignObservationCapacity(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, Boolean CalculateByAssignmentPercent, int PersonId)
    {
        // MaxJobCount,MaxJobCapacity,  ObservationPercent, ObservationCapacity,Grade, MjId,GradeInOfficeLicense, DesignInc,  SameGradeInc, MajorInc, TotalDsgCapacity,  TotalObsCapacity,  MeId, MeName, ConditionalCapacity
        #region Define Manager
        TSP.DataManager.DocOffMajorNum DocOffMajorNum = new TSP.DataManager.DocOffMajorNum();
        TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocOffIncreaseJobCapacityManager IncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();
        #endregion

        #region Define Parameters
        int SumMaxJobCount = 0;
        int SumDesignCapacity = 0;
        int SumObservationCapacity = 0;
        int GradeIdOfMeI = 0;
        int GradeIdOfMeJ = 0;
        int MjIdOfMeI = 0;
        int MjIdOfMeJ = 0;
        int MeIdI = 0;
        int MeIdJ = 0;
        int CapacityDesignInc = 0;
        int CapacitySameGradeInc = 0;
        int CapacityMajorInc = 0;
        int TotalOfMeDsgCap = 0;
        int TotalOfMeObsCap = 0;
        #endregion

        OfficeMemberManager = GetOfficeMembers(OfficeEngoId, DocOffIncreaseJobCapacityType);

        #region بدست آوردن ترکیب رشته شرکا را بدست می آورد و درصد افزایش اشتغال از جداول صفحه26-31
        //*********ترکیب رشته شرکا را بدست می آورد:ArrayList[0]:_PartnertsMajorCombination.MainMajorNum;ArrayList[1]:_PartnertsMajorCombination.SecondaryMajorNum ;ArrayList[2]:_PartnertsMajorCombination.TotalMajorNum
        GetMajorNum(OfficeMemberManager.DataTable);
        DocOffMajorNum.FindByMajorsNum(_PartnertsMajorCombination.MainMajorNum, _PartnertsMajorCombination.SecondaryMajorNum, _PartnertsMajorCombination.TotalMajorNum);
        if (DocOffMajorNum.Count <= 0)
            return _OfficeCapacity;
        //*********بدست آوردن درصد افزایش ظرفیت اشتغال از جداول صفحه26-31**************
        IncreaseJobCapacityManager.FindByMNumId(Convert.ToInt32(DocOffMajorNum[0]["MNumId"]), DocOffIncreaseJobCapacityType);
        if (IncreaseJobCapacityManager.Count <= 0)
            return _OfficeCapacity;
        int DesignIncPer = Convert.ToInt32(IncreaseJobCapacityManager[0]["DesignIncPer"]);
        int SameGradeIncPer = Convert.ToInt32(IncreaseJobCapacityManager[0]["SameGradeIncPer"]);
        int MajorIncPer = Convert.ToInt32(IncreaseJobCapacityManager[0]["MajorIncPer"]);
        #endregion

        if (OfficeMemberManager.Count == 1)
        {
            #region محاسبه ظرفیت دفاتر تک نفره شامل : MaxJobCapacity - MaxJobCount - UsedCapDes - UsedCapObs - UsedCapTotal -RemainCapDes - RemainCapObs - ReservedCap
            int MeId = Convert.ToInt32(OfficeMemberManager[0]["PersonId"]);

            GetMemberDesignObservationCapacity(MeId, ProjectIngridientTypeId, -1, CalculateByAssignmentPercent);

            //*****درصد افزایش دفتر مهندسی
            CapacityDesignInc = Convert.ToInt32(_MemberCapacity.MemberMaxJobCapacity) * DesignIncPer / 100;
            SumMaxJobCount = Convert.ToInt32(_MemberCapacity.MemberMaxJobCount);
            SumDesignCapacity = TotalOfMeDsgCap = Convert.ToInt32(_MemberCapacity.MemberDesignCapacity) + CapacityDesignInc;
            SumObservationCapacity = TotalOfMeObsCap = TotalOfMeDsgCap * _MemberCapacity.MemberObservationPercent;
            _OfficeCapacity.OfficeMemberMaxJobCount = Convert.ToInt32(_MemberCapacity.MemberMaxJobCount);
            _OfficeCapacity.OfficeObservationPercent = _MemberCapacity.MemberObservationPercent;
            _OfficeCapacity.OfficeMemberCurrentProjectNum = _MemberCapacity.MemberCurrentProjectNum;
            _OfficeCapacity.OfficeMemberIncreamentCapacityOfOfficeMembership = CapacityDesignInc;
            _OfficeCapacity.OfficeMemberMaxJobCapacity = Convert.ToInt32(_MemberCapacity.MemberMaxJobCapacity);
            _OfficeCapacity.OfficeMemberTotalDsgCapacity = TotalOfMeDsgCap;
            _OfficeCapacity.OfficeMemberTotalObsCapacity = TotalOfMeObsCap;
            _OfficeCapacity.OfficeMemberUsedCapacityDesign = OfficeMembersUsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Designer, MeId, (int)TSP.DataManager.TSMemberType.Member);
            _OfficeCapacity.OfficeMemberUsedCapacityObservation = OfficeMembersUsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Observer, MeId, (int)TSP.DataManager.TSMemberType.Member);
            //******کل ظرفیت مصرفی=ظرفیت مصرفی نظارت+ظرفیت مصرفی طراحی
            _OfficeCapacity.OfficeMemberUsedCapacity = _OfficeCapacity.OfficeMemberUsedCapacityDesign + _OfficeCapacity.OfficeMemberUsedCapacityObservation;
            //********ظرفیت طراحی باقمانده = کل ظرفیت طراحی - ظرفیت طراحی مصرف شده -(ظرفیت نظارت مصرف شده/ضریب تبدیل طراحی به نظارت) ه
            if (_MemberCapacity.MemberObservationPercent != 0)
                _OfficeCapacity.OfficeMemberRemainCapacityDesign = TotalOfMeDsgCap - _OfficeCapacity.OfficeMemberUsedCapacityDesign - (_OfficeCapacity.OfficeMemberUsedCapacityObservation / _MemberCapacity.MemberObservationPercent);
            //****ظرفیت باقیمانده نظارت = ظرفیت باقیمانده طراحی * ضریب تبدیل طراحی به نظارت
            _OfficeCapacity.OfficeMemberRemainCapacityObservation = _OfficeCapacity.OfficeMemberRemainCapacityDesign * _MemberCapacity.MemberObservationPercent;
            _OfficeCapacity.OfficeMemberRemainCapacity = _OfficeCapacity.OfficeMemberRemainCapacityDesign;
            _OfficeCapacity.OfficeMemberReservedCapacity = GetOfficeMembersTotalReservedCapacity(ProjectIngridientTypeId, MeId, (int)TSP.DataManager.TSMemberType.Member);

            //if (PersonId != -1)
            //{                                             
            //}
            #endregion
        }
        else
        {
            #region محاسبه ظرفیت دفاتر/شرکت چند نفره
            DataTable dtOfficeMeCopy = OfficeMemberManager.DataTable;
            if (PersonId != -1)
            {
                OfficeMemberManager.CurrentFilter = "PersonId=" + PersonId.ToString();
            }

            for (int i = 0; i < OfficeMemberManager.Count; i++)
            {
                bool SameGradeInc = false;
                bool MajorInc = false;
                MeIdI = Convert.ToInt32(OfficeMemberManager.DataTable.DefaultView[i]["PersonId"]);
                if (Utility.IsDBNullOrNullValue(OfficeMemberManager.DataTable.DefaultView[i]["MfId"]))
                    continue;
                GetMemberDocMasterMajor(MeIdI, Convert.ToInt32(OfficeMemberManager.DataTable.DefaultView[i]["MfId"]));
                MjIdOfMeI = _MemberCapacity.MemberMajorId;
                #region چک می شود فرد هم پایه و هم رشته دارد و یا نه
                for (int j = 0; j < dtOfficeMeCopy.Rows.Count; j++)
                {
                    //if (i != j)
                    if (Convert.ToInt32(dtOfficeMeCopy.Rows[j]["OfmId"]) != Convert.ToInt32(OfficeMemberManager.DataTable.DefaultView[i]["OfmId"]))
                    {
                        MeIdJ = Convert.ToInt32(dtOfficeMeCopy.Rows[j]["PersonId"]);
                        if (Utility.IsDBNullOrNullValue(dtOfficeMeCopy.Rows[j]["MfId"]))
                            continue;
                        GetMemberDocMasterMajor(MeIdJ, Convert.ToInt32(dtOfficeMeCopy.Rows[j]["MfId"]));
                        MjIdOfMeJ = _MemberCapacity.MemberMajorId;
                        if (MjIdOfMeI == MjIdOfMeJ) //***هم رشته بودن شرکا 
                        {
                            MajorInc = true;
                            GradeIdOfMeI = GetGradeByMFId(Convert.ToInt32(OfficeMemberManager.DataTable.DefaultView[i]["MfId"]), MeIdI, ProjectIngridientTypeId);
                            GradeIdOfMeJ = GetGradeByMFId(Convert.ToInt32(dtOfficeMeCopy.Rows[j]["MfId"]), MeIdJ, ProjectIngridientTypeId);
                            if (GradeIdOfMeI == GradeIdOfMeJ)  //***هم پایه بودن شرکا 
                                SameGradeInc = true;//if (!MajorInc) SameGradeInc = true;
                        }
                    }
                    MjIdOfMeJ = 0;
                    GradeIdOfMeJ = 0;
                }
                #endregion

                #region Calculate Capacity
                //********بدست آوردن اطلاعات ظرفیت طراحی/نظارت شخص حقیقی بر اساس پایه شخص
                GetMemberDesignObservationCapacity(MeIdI, ProjectIngridientTypeId, -1, CalculateByAssignmentPercent);
                #region بدست آوردن ظرفیت موثر عضویت در شرکت/دفتر
                //****DesignIncPer:درصد افزایش طراحی          
                CapacityDesignInc = Convert.ToInt32(_MemberCapacity.MemberMaxJobCapacity) * DesignIncPer / 100;
                if (SameGradeInc)
                    CapacitySameGradeInc = Convert.ToInt32(_MemberCapacity.MemberMaxJobCapacity) * SameGradeIncPer / 100;
                if (MajorInc)
                    CapacityMajorInc = Convert.ToInt32(_MemberCapacity.MemberMaxJobCapacity) * MajorIncPer / 100;
                #endregion
                //******ظرفیت شخص + ظرفیت موثر دریافتی از عضویت در شرکت/دفتر                
                TotalOfMeDsgCap = Convert.ToInt32(_MemberCapacity.MemberDesignCapacity) + CapacityDesignInc + CapacitySameGradeInc + CapacityMajorInc;
                //******برای بدست آوردن کل ظرفیت نظارت مجموع ظرفیت طراحی در ضریب تبدیل طراحی به نظارت ضرب می شود
                //******کل ظرفیت نظارت =مجموع ظرفیت طراحی *ضریب تبدیل طراحی به نظارت
                TotalOfMeObsCap = _MemberCapacity.MemberObservationPercent * TotalOfMeDsgCap;
                //******
                SumMaxJobCount += Convert.ToInt32(_MemberCapacity.MemberMaxJobCount);
                SumDesignCapacity += TotalOfMeDsgCap;
                SumObservationCapacity += TotalOfMeObsCap;

                if (PersonId != -1 && MeIdI == PersonId)
                {
                    _OfficeCapacity.OfficeObservationPercent = _MemberCapacity.MemberObservationPercent;
                    _OfficeCapacity.OfficeMemberTotalDsgCapacity = TotalOfMeDsgCap;
                    _OfficeCapacity.OfficeMemberTotalObsCapacity = TotalOfMeObsCap;

                    _OfficeCapacity.OfficeMemberMaxJobCapacity = Convert.ToInt32(_MemberCapacity.MemberMaxJobCapacity);
                    _OfficeCapacity.OfficeMemberMaxJobCount = Convert.ToInt32(_MemberCapacity.MemberMaxJobCount);
                    _OfficeCapacity.OfficeMemberIncreamentCapacityOfOfficeMembership = CapacityDesignInc + CapacitySameGradeInc + CapacityMajorInc;
                    _OfficeCapacity.OfficeMemberCurrentProjectNum = _MemberCapacity.MemberCurrentProjectNum;    // ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity//int TotalDsg = Convert.ToInt32(((ArrayList)MemberArray[i])[10]);//int TotalObs = Convert.ToInt32(((ArrayList)MemberArray[i])[11]);
                    int UsedObs = 0;
                    int UsedDsg = 0;
                    _OfficeCapacity.OfficeMemberUsedCapacityDesign = OfficeMembersUsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Designer, PersonId, (int)TSP.DataManager.TSMemberType.Member);
                    _OfficeCapacity.OfficeMemberUsedCapacityObservation = OfficeMembersUsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Observer, PersonId, (int)TSP.DataManager.TSMemberType.Member);
                    _OfficeCapacity.OfficeMemberUsedCapacity = _OfficeCapacity.OfficeMemberUsedCapacityDesign + _OfficeCapacity.OfficeMemberUsedCapacityObservation;
                    //********ظرفیت طراحی باقمانده = کل ظرفیت طراحی - ظرفیت طراحی مصرف شده -(ظرفیت نظارت مصرف شده/ضریب تبدیل طراحی به نظارت) ه
                    if (_MemberCapacity.MemberObservationPercent != 0)
                        _OfficeCapacity.OfficeMemberRemainCapacityDesign = TotalOfMeDsgCap - _OfficeCapacity.OfficeMemberUsedCapacityDesign - (_OfficeCapacity.OfficeMemberUsedCapacityObservation / _MemberCapacity.MemberObservationPercent);
                    //****ظرفیت باقیمانده نظارت = ظرفیت باقیمانده طراحی * ضریب تبدیل طراحی به نظارت
                    _OfficeCapacity.OfficeMemberRemainCapacityObservation = _OfficeCapacity.OfficeMemberRemainCapacityDesign * _MemberCapacity.MemberObservationPercent;
                    _OfficeCapacity.OfficeMemberRemainCapacity = _OfficeCapacity.OfficeMemberRemainCapacityDesign;
                    _OfficeCapacity.OfficeMemberReservedCapacity = GetOfficeMembersTotalReservedCapacity(ProjectIngridientTypeId, PersonId, (int)TSP.DataManager.TSMemberType.Member);
                }
                #endregion

                #region ResetValues
                GradeIdOfMeI = 0;
                GradeIdOfMeJ = 0;
                MjIdOfMeI = 0;
                MjIdOfMeJ = 0;
                _MemberCapacity.MemberMaxJobCapacity = 0;
                CapacityDesignInc = 0;
                CapacitySameGradeInc = 0;
                CapacityMajorInc = 0;
                //TotalDsgCapacity = 0;
                //TotalObsCapacity = 0;
                #endregion
            }
            #endregion
        }

        #region Calculate ConditionalCapacity & Totalize with SumMaxJobCapacity
        //***********ظرفیت اضافی یا کم شده یک شرکت یا دفتر را بر می گرداند
        // int ConditionalCapacityOffice = GetConditionalCapacity(OfficeEngoId, ProjectIngridientTypeId);
        int ConditionalCapacityOfficeDes = GetConditionalCapacity(OfficeEngoId, (int)TSProjectIngridientType.Designer);
        int ConditionalCapacityOfficeObs = GetConditionalCapacity(OfficeEngoId, (int)TSProjectIngridientType.Observer);
        ConditionalCapacityOfficeDes = ConditionalCapacityOfficeDes + (ConditionalCapacityOfficeObs * _MemberCapacity.MemberObservationPercent);
        int ConditionalCapacityOfficeImp = GetConditionalCapacity(OfficeEngoId, (int)TSProjectIngridientType.Implementer);
        switch (ProjectIngridientTypeId)
        {
            case (int)TSProjectIngridientType.Designer:
                SumDesignCapacity += Convert.ToInt32(ConditionalCapacityOfficeDes);
                break;
            case (int)TSProjectIngridientType.Observer:

                SumDesignCapacity += Convert.ToInt32(ConditionalCapacityOfficeObs);
                break;
            case (int)TSProjectIngridientType.Implementer:
                SumDesignCapacity += Convert.ToInt32(ConditionalCapacityOfficeImp);
                break;
        }
        //SumMaxJobCapacity += Convert.ToInt32(ConditionalCapacityOffice);
        SumObservationCapacity += Convert.ToInt32(ConditionalCapacityOfficeObs);
        #endregion

        if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
            SumMaxJobCount = SumMaxJobCount / 2;

        _OfficeCapacity.OfficeMaxJobCount = SumMaxJobCount;
        _OfficeCapacity.OfficeMaxJobCapacityDesign = SumDesignCapacity;
        _OfficeCapacity.OfficeDesignCapacity = SumDesignCapacity;
        _OfficeCapacity.OfficeObservationCapacity = SumObservationCapacity;
        if (PersonId != -1)
        {
            _OfficeCapacity.OfficeConditionalCapacityDesign = _MemberCapacity.MemberConditionalCapacityDesign;
            _OfficeCapacity.OfficeConditionalCapacityObserve = _MemberCapacity.MemberConditionalCapacityObserve;
            _OfficeCapacity.OfficeConditionalCapacityImplement = _MemberCapacity.MemberConditionalCapacityImplement;
        }
        else
        {
            _OfficeCapacity.OfficeConditionalCapacityDesign = ConditionalCapacityOfficeDes;
            _OfficeCapacity.OfficeConditionalCapacityObserve = ConditionalCapacityOfficeObs;
            _OfficeCapacity.OfficeConditionalCapacityImplement = ConditionalCapacityOfficeImp;
        }
        return _OfficeCapacity;
    }

    /// <summary>
    /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت را بر اساس اختصاص ظرفیت بر می گرداند
    /// MaxJobCount, MaxJobCapacity, ObservationCapacity,ConditionalCapacity
    /// </summary>
    /// <param name="OfficeEngoId"></param>
    /// <param name="ProjectIngridientTypeId"></param>
    /// <param name="DocOffIncreaseJobCapacityType"></param>
    /// <param name="CalculateByCapacityAssignment">مشخص می کند آیا ظرفیت و تعداد کار براساس درصد اختصاص ظرفیت محاسبه شود</param>
    /// <returns></returns>
    private OfficeCapacity GetOfficeDesignObservationCapacity(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, Boolean CalculateByAssignmentPercent)
    {
        return GetOfficeDesignObservationCapacity(OfficeEngoId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, CalculateByAssignmentPercent, -1);
    }
    #endregion

    #region GetOfficeDsgCapacity =====>Public Methods
    /// <summary>
    /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت را بر می گرداند
    /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationCapacity, ArrayList[3]: ConditionalCapacity
    /// </summary>
    public OfficeCapacity GetOfficeDsgCapacity(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType)
    {
        return GetOfficeDesignObservationCapacity(OfficeEngoId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, false);
    }

    /// <summary>
    /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت را بر می گرداند
    /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationCapacity, ArrayList[3]: ConditionalCapacity
    /// </summary>
    public OfficeCapacity GetOfficeDsgCapacity(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, int PersonId)
    {
        return GetOfficeDesignObservationCapacity(OfficeEngoId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, false, PersonId);
    }

    /// <summary>
    /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت را بر می گرداند
    /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationCapacity, ArrayList[3]: ConditionalCapacity
    /// </summary>
    /// <param name="OfficeEngoId"></param>
    /// <param name="ProjectIngridientTypeId"></param>
    /// <param name="DocOffIncreaseJobCapacityType"></param>
    /// <param name="CalculateByAssignmentPercent">CalculateByAssignmentPercent=True : PerStage</param>
    /// <param name="PersonId"></param>
    /// <returns></returns>
    public OfficeCapacity GetOfficeDsgCapacity(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, Boolean CalculateByAssignmentPercent, int PersonId)
    {
        return GetOfficeDesignObservationCapacity(OfficeEngoId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, CalculateByAssignmentPercent, PersonId);
    }
    #endregion


    /// <summary>
    /// ظرفیت رزرو شده عضوی از یک شرکت یا یک دفتر را بر می گرداند
    /// </summary>
    private int GetOfficeMembersTotalReservedCapacity(int ProjectIngridientTypeId, int MeOthpId, int MemberTypeId)
    {
        int CapacityDecrement = 0;
        TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
        // if (ProjectIngridientTypeId != (int)TSP.DataManager.TSProjectIngridientType.Designer)
        // {
        ProjectOfficeMembersManager.FindReservedCapacity(MeOthpId, ProjectIngridientTypeId, MemberTypeId);
        for (int i = 0; i < ProjectOfficeMembersManager.Count; i++)
            CapacityDecrement += Convert.ToInt32(ProjectOfficeMembersManager[i]["CapacityDecrement"]);
        //  }
        return CapacityDecrement;
        #region Per Codes==>Commented
        //int CapacityDecrement = 0;

        //switch (ProjectIngridientTypeId)
        //{
        //    case (int)TSP.DataManager.TSProjectIngridientType.Designer:
        //    case (int)TSP.DataManager.TSProjectIngridientType.Observer:
        //        CapacityDecrement = OfficeMembersReservedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOthpId, MemberTypeId);
        //        CapacityDecrement = OfficeMembersReservedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOthpId, MemberTypeId);
        //        break;

        //    case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
        //        CapacityDecrement = OfficeMembersReservedCapacity(ProjectIngridientTypeId, MeOthpId, MemberTypeId);
        //        break;
        //}

        //return CapacityDecrement;
        #endregion
    }

    #region بدست آوردن ترکیب شرکای دفتر-شرکت
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
            MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Architecture).ToString();
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
            MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Civil).ToString();
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
            MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Electronic).ToString();
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
            MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Mechanic).ToString();
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
            MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Mapping).ToString();
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
            MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Urbanism).ToString();
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
            MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Traffic).ToString();
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

    ///// <summary>
    ///// ترکیب رشته شرکا را بر اساس جداول صفحه26/31 کتاب مبحث دوم  بدست می آورد
    ///// ArrayList[0]: MainMajorNum, ArrayList[1]: SecondaryMajorNum, ArrayList[2]: TotalMajorNum
    ///// </summary>
    ///// <param name="MembersArr"></param>
    ///// <returns>فیلد های مورد نیاز جهت بدست آوردن ردیف مورد نظر در جدول
    ///// [DocOff.MajorNum]بدست می آورد</returns>
    //private void GetMajorNum(ArrayList MembersArr)
    //{
    //    TSP.DataManager.MajorParentsManager MajorManager = new TSP.DataManager.MajorParentsManager();
    //    MajorManager.FindMjParents();

    //    int MainMajorNum = 0;
    //    int SecondaryMajorNum = 0;
    //    int TotalMajorNum = 0;
    //    //int MajorIncrement = 0;

    //    bool Architecture = false;
    //    bool Urbanism = false;
    //    bool Civil = false;
    //    bool Mechanic = false;
    //    bool Electronic = false;
    //    bool Mapping = false;
    //    bool Traffic = false;
    //    #region //******چک کردن داشتن یک رشته از رشته های هفتگانه
    //    for (int j = 0; j < MembersArr.Count; j++)
    //    {
    //        switch (Convert.ToInt32(((ArrayList)MembersArr[j])[5]))
    //        {
    //            case (int)TSP.DataManager.MainMajors.Architecture:
    //                Architecture = true;
    //                break;

    //            case (int)TSP.DataManager.MainMajors.Civil:
    //                Civil = true;
    //                break;

    //            case (int)TSP.DataManager.MainMajors.Electronic:
    //                Electronic = true;
    //                break;

    //            case (int)TSP.DataManager.MainMajors.Mechanic:
    //                Mechanic = true;
    //                break;

    //            case (int)TSP.DataManager.MainMajors.Mapping:
    //                Mapping = true;
    //                break;

    //            case (int)TSP.DataManager.MainMajors.Urbanism:
    //                Urbanism = true;
    //                break;

    //            case (int)TSP.DataManager.MainMajors.Traffic:
    //                Traffic = true;
    //                break;
    //        }
    //    }
    //    #endregion

    //    #region بدست آوردن ترکیب شرکا  از جدول 1 صفحه 26
    //    if (Architecture)
    //    {
    //        MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Architecture).ToString();
    //        if (MajorManager.Count == 1)
    //        {
    //            if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
    //                MainMajorNum += 1;
    //            else
    //                SecondaryMajorNum += 1;
    //        }
    //    }

    //    if (Civil)
    //    {
    //        MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Civil).ToString();
    //        if (MajorManager.Count == 1)
    //        {
    //            if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
    //                MainMajorNum += 1;
    //            else
    //                SecondaryMajorNum += 1;
    //        }
    //    }


    //    if (Electronic)
    //    {
    //        MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Electronic).ToString();
    //        if (MajorManager.Count == 1)
    //        {
    //            if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
    //                MainMajorNum += 1;
    //            else
    //                SecondaryMajorNum += 1;
    //        }
    //    }

    //    if (Mechanic)
    //    {
    //        MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Mechanic).ToString();
    //        if (MajorManager.Count == 1)
    //        {
    //            if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
    //                MainMajorNum += 1;
    //            else
    //                SecondaryMajorNum += 1;
    //        }
    //    }

    //    if (Mapping)
    //    {
    //        MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Mapping).ToString();
    //        if (MajorManager.Count == 1)
    //        {
    //            if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
    //                MainMajorNum += 1;
    //            else
    //                SecondaryMajorNum += 1;
    //        }
    //    }

    //    if (Urbanism)
    //    {
    //        MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Urbanism).ToString();
    //        if (MajorManager.Count == 1)
    //        {
    //            if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
    //                MainMajorNum += 1;
    //            else
    //                SecondaryMajorNum += 1;
    //        }
    //    }

    //    if (Traffic)
    //    {
    //        MajorManager.CurrentFilter = "MjId =" + ((int)TSP.DataManager.MainMajors.Traffic).ToString();
    //        if (MajorManager.Count == 1)
    //        {
    //            if (Convert.ToBoolean(MajorManager[0]["IsMain"]))
    //                MainMajorNum += 1;
    //            else
    //                SecondaryMajorNum += 1;
    //        }
    //    }
    //    #endregion
    //    TotalMajorNum = MainMajorNum + SecondaryMajorNum;
    //    if ((MainMajorNum <= 1 && SecondaryMajorNum != 0) || TotalMajorNum == 1)
    //    {
    //        TotalMajorNum = 1;
    //        MainMajorNum = 0;
    //        SecondaryMajorNum = 0;

    //    }
    //    else if (MainMajorNum <= 4 && SecondaryMajorNum != 1)
    //    {
    //        SecondaryMajorNum = 0;
    //        TotalMajorNum = 0;
    //    }
    //    else if (MainMajorNum == 4 && SecondaryMajorNum == 1)
    //    {
    //        TotalMajorNum = 0;
    //    }

    //    _PartnertsMajorCombination.MainMajorNum = MainMajorNum;
    //    _PartnertsMajorCombination.SecondaryMajorNum = SecondaryMajorNum;
    //    _PartnertsMajorCombination.TotalMajorNum = TotalMajorNum;
    //}
    #endregion

    #endregion

    #region   ظرفیت طراحی-نظارت شخص حقیقی
    /// <summary>
    /// ظرفیت کل طراحی و نظارت یک عضو حقیقی را بر اساس پایه وی و بر اساس اختصاص ظرفیت بر می گرداند    
    ///   MaxJobCount, MaxJobCapacity, ObservationPercent, ObservationCapacity,  Grade,  MjId, MeId,  MeName
    ///   , MemberConditionalCapacityDesign,MemberConditionalCapacityObserve,MemberConditionalCapacityImplement
    ///   ,MemberDesignCapacity,MemberObservationCapacity
    /// </summary>
    /// <param name="MeId"></param>
    /// <param name="ProjectIngridientTypeId">نوع عامل پروژه</param>
    /// <param name="MfId">برای محاسبه ظرفیت شخص در شرکت و دفتر بر اساس پروانه ای که در شرکت دارد استفاده می شود</param>
    /// <param name="CalculateByCapacityAssignment">مشخص می کند آیا ظرفیت و تعداد کار براساس درصد اختصاص ظرفیت محاسبه شود</param>
    /// <returns></returns>
    private MemberCapacity GetMemberDesignObservationCapacity(int MeId, int ProjectIngridientTypeId, int MfId, Boolean CalculateByCapacityAssignment)
    {
        //*** MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity,ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc,,ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName,ArrayList[14]: ConditionalCapacity
        #region Define Managers
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.DocOffMemberCapacityManager MemberCapacityManager = new TSP.DataManager.DocOffMemberCapacityManager();
        #endregion

        #region Find Member Info
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 1)
            return _MemberCapacity;
        IngredientMeEngOfId = MeId;
        _MemberCapacity.MemberFullName = MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
        #endregion

        #region Find Grade
        int Grade = 0;
        if (MfId == -1)
        {
            Grade = GetMemGrade(MeId, ProjectIngridientTypeId);
            //****بدست آوردن اطلاعات رشته موضوع پروانه شخص حقیقی
            GetMemberDocMasterMajor(MeId);
        }
        else
        {
            Grade = GetGradeByMFId(MfId, MeId, ProjectIngridientTypeId);
            //****بدست آوردن اطلاعات رشته موضوع پروانه شخص حقیقی
            GetMemberDocMasterMajor(MeId, MfId);
        }
        if (Grade == 0)
            return _MemberCapacity;
        _MemberCapacity.MemberGradeId = Grade;
        #endregion

        #region ConditionalCapacity
        _MemberCapacity.MemberConditionalCapacityDesign = GetConditionalCapacity(MeId, (int)TSProjectIngridientType.Designer);
        _MemberCapacity.MemberConditionalCapacityObserve = GetConditionalCapacity(MeId, (int)TSProjectIngridientType.Observer);
        _MemberCapacity.MemberConditionalCapacityImplement = GetConditionalCapacity(MeId, (int)TSProjectIngridientType.Implementer);
        //_MemberCapacity.MemberConditionalCapacityDesign = GetConditionalCapacity(MeId, ProjectIngridientTypeId);
        #endregion

        #region ظرفیت پایه شخص - MaxJobCapacity
        //*****بدست آوردن اطلاعات ظرفیت و تعداد کار مجاز سالانه برای شخص حقیقی بر اساس پایه
        MemberCapacityManager.FindByGrdId(Grade);
        if (MemberCapacityManager.Count <= 0)
            return _MemberCapacity;
        if (CalculateByCapacityAssignment)//*****با درنظرگرفتن درصد اختصاص ظرفیت جاری
        {//****Find MaxJobCount And  MaxJobCapacity-بدست آوردن درصد اختصاص ظرفیت جاری
            CalculateMaxJobCountByCapacityAssignment(Convert.ToInt32(MemberCapacityManager[0]["MaxJobCount"]), Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]));
        }
        else//*****بدون درنظرگرفتن درصد اختصاص ظرفیت جاری
        {
            _MemberCapacity.MemberMaxJobCapacity = Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]);
            _MemberCapacity.MemberMaxJobCount = Convert.ToInt32(MemberCapacityManager[0]["MaxJobCount"]);
        }
        #endregion

        //***********************************************************************************************************************************//
        //****میزان کل ظرفیت افزایش/کاهش نظارت= (ظرفیت افزایش کاهش طراحی *ضریب تبدیل طراحی به نظارت) + ظرفیت افزایش کاهش نظارت**//
        //****میزان  کل ظرفیت افزایش/کاهش طراحی= (ظرفیت افزایش کاهش نظارت/ضریب تبدیل طراحی به نظارت) + ظرفیت افزایش کاهش طراحی**//
        //**********************************************************************************************************************************//
        //**ضریب تبدیل طراحی به نظارت
        _MemberCapacity.MemberObservationPercent = Convert.ToInt32(MemberCapacityManager[0]["ObservationPercent"]);
        //**کل ظرفیت طراحی
        _MemberCapacity.MemberDesignCapacity = Convert.ToInt32(_MemberCapacity.MemberMaxJobCapacity) + _MemberCapacity.MemberConditionalCapacityDesign
                                                                            + (_MemberCapacity.MemberConditionalCapacityObserve / _MemberCapacity.MemberObservationPercent);
        //**کل ظرفیت نظارت
        _MemberCapacity.MemberObservationCapacity = (Convert.ToInt32(_MemberCapacity.MemberMaxJobCapacity) * _MemberCapacity.MemberObservationPercent)
                                                                             + (_MemberCapacity.MemberConditionalCapacityDesign * _MemberCapacity.MemberObservationPercent)
                                                                             + _MemberCapacity.MemberConditionalCapacityObserve;

        #region //**تعداد پروژه های شخص------تعداد کل پروژه های در دست اجرا عضوی از یک شرکت یا یک دفتر را بر می گرداند
        int ProjectNum = 0;
        switch (ProjectIngridientTypeId)
        {
            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                ProjectNum += OfficeMembersCurrentProjectNum((int)TSP.DataManager.TSProjectIngridientType.Designer, MeId, (int)TSP.DataManager.TSMemberType.Member);
                ProjectNum += OfficeMembersCurrentProjectNum((int)TSP.DataManager.TSProjectIngridientType.Observer, MeId, (int)TSP.DataManager.TSMemberType.Member);
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                ProjectNum += OfficeMembersCurrentProjectNum(ProjectIngridientTypeId, MeId, (int)TSP.DataManager.TSMemberType.Member);
                break;
        }
        _MemberCapacity.MemberCurrentProjectNum = ProjectNum;
        #endregion

        return _MemberCapacity;
    }
    #endregion
    #endregion

    #region محاسبه ظرفیت اجرا

    /// <summary>
    /// کل ظرفیت و تعداد کار و تعداد طبقات مجاز فرد، شرکت یا یک دفتر اجرایی را بر اساس اختصاص ظرفیت بر می گرداند
    /// ArrayList[0]: MaxFloor(string), ArrayList[1]: MaxJobCapacity(string), ArrayList[2]: MaxUnitCount OR MaxJobCount(int), ArrayList[3]: ConditionalCapacity
    /// </summary>
    /// <param name="MemberTypeId"></param>
    /// <param name="MeOfficeEngOId"></param>
    /// <param name="CalculateByAssignmentPercent">محاسبه بر اساس ظرفیت جاری باشد یا نه</param>
    public void GetImplementTotalCapacity(int MemberTypeId, int MeOfficeEngOId, Boolean CalculateByAssignmentPercent)
    {
        ArrayList CapacityArr = new ArrayList();
        ArrayList CapArr = new ArrayList();

        switch (MemberTypeId)
        {
            case (int)TSP.DataManager.TSMemberType.Member:
                MemberCapacity MeCapacity = GetMemberImpCapacity(MeOfficeEngOId, CalculateByAssignmentPercent);
                IngridientMaxFloor = MeCapacity.MemberMaxFloorCount;
                IngridientMaxJobCapacity = Convert.ToInt32(MeCapacity.MemberMaxJobCapacity);
                IngridientImplementCapacity = Convert.ToInt32(MeCapacity.MemberImplementCapacity);
                IngridientMaxJobCount = Convert.ToInt32(MeCapacity.MemberMaxJobCount);
                IngridientConditionalCapacityImplement = MeCapacity.MemberConditionalCapacityImplement;
                break;

            case (int)TSP.DataManager.TSMemberType.Office:
                OfficeCapacity OffCapacity = GetOfficeImplementCapacity(MeOfficeEngOId, CalculateByAssignmentPercent);
                IngridientMaxFloor = OffCapacity.OfficeMaxFloorCount;
                IngridientMaxJobCapacity = Convert.ToInt32(OffCapacity.OfficeMemberMaxJobCapacity);
                IngridientMaxJobCount = Convert.ToInt32(OffCapacity.OfficeMaxJobCount);
                IngridientConditionalCapacityImplement = OffCapacity.OfficeConditionalCapacityImplement;
                break;
        }
        #region Comment
        //??????????????????????
        ////if (Convert.ToInt32(CapArr[0]) == -1)
        ////    CapArr[0] = "بدون محدودیت";

        ////if (Convert.ToInt32(CapArr[2]) == -1)
        ////    CapArr[2] = "بدون محدودیت";
        //??????????????????????
        //CapacityArr.Add(Convert.ToInt32(CapArr[0]));
        //CapacityArr.Add(Convert.ToInt32(CapArr[1]));
        //CapacityArr.Add(Convert.ToInt32(CapArr[2]));
        //if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
        //    CapacityArr.Add(Convert.ToInt32(CapArr[4]));
        //else
        //    CapacityArr.Add(Convert.ToInt32(CapArr[3]));
        //  return CapacityArr;
        #endregion
    }

    #region   ظرفیت اجرا شخص حقیقی
    /// <summary>
    /// ظرفیت کل اجرا یک عضو را بر اساس اختصاص ظرفیت بر می گرداند
    ///  MaxFloor,  MaxJobCapacity +ConditionalCapacity =TotalCapacity=ImpCapacity, MaxUnitCount ==>MaxJobCount,Grade,ConditionalCapacity
    /// </summary>
    private MemberCapacity GetMemberImpCapacity(int MeId, Boolean CalculateByAssignmentPercent)
    {
        _MemberCapacity.MemberGradeId = GetMemGrade(MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
        if (_MemberCapacity.MemberGradeId != 0)
        {
            //***ظرفیت کاهش/افزایش اجرا
            _MemberCapacity.MemberConditionalCapacityImplement = GetConditionalCapacity(MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
            TSP.DataManager.DocOffEngOfficeImpQualificationManager EngOfficeImpQualificationManager = new TSP.DataManager.DocOffEngOfficeImpQualificationManager();
            EngOfficeImpQualificationManager.FindByGrdId(_MemberCapacity.MemberGradeId);
            if (EngOfficeImpQualificationManager.Count > 0)//****ظرفیت پایه شخص
            {
                if (CalculateByAssignmentPercent)//*****با درنظرگرفتن درصد اختصاص ظرفیت جاری
                {
                    //****Find MaxJobCount And  MaxJobCapacity-بدست آوردن درصد اختصاص ظرفیت جاری                    
                    CalculateMaxJobCountByCapacityAssignment(Convert.ToInt32(EngOfficeImpQualificationManager[0]["MaxUnitCount"]), Convert.ToInt32(EngOfficeImpQualificationManager[0]["MaxJobCapacity"]));
                }
                else//*****بدون درنظرگرفتن درصد اختصاص ظرفیت جاری
                {
                    _MemberCapacity.MemberMaxJobCapacity = Convert.ToInt32(EngOfficeImpQualificationManager[0]["MaxJobCapacity"]);
                    _MemberCapacity.MemberMaxJobCount = Convert.ToInt32(EngOfficeImpQualificationManager[0]["MaxJobCount"]);
                }
                _MemberCapacity.MemberMaxFloorCount = Convert.ToInt32(EngOfficeImpQualificationManager[0]["MaxFloor"]);
                _MemberCapacity.MemberImplementCapacity = Convert.ToInt32(_MemberCapacity.MemberMaxJobCapacity) + _MemberCapacity.MemberConditionalCapacityImplement;
            }
        }
        return _MemberCapacity;
    }
    #endregion

    #region   ظرفیت اجرا دفتر/شرکت
    /// <summary>
    /// ظرفیت کل اجرا یک شرکت را بر اساس اختصاص ظرفیت بر می گرداند
    /// ArrayList[0]: MaxFloor, ArrayList[1]:MaxCapacity , ArrayList[2]: MaxJobCount, ArrayList[3]: ConditionalCapacity, ArrayList[4]: GradeId, ArrayList[5]: GrdType
    /// </summary>
    private OfficeCapacity GetOfficeImplementCapacity(int OfficeId, Boolean CalculateByAssignmentPercent)
    {
        #region Define Managers
        TSP.DataManager.DocOffOfficeMembersQualificationManager OfficeMembersQualificationManager = new TSP.DataManager.DocOffOfficeMembersQualificationManager();
        TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
        #endregion
        // GradeArr-----> ArrayList[0]: GradeId, ArrayList[1]: Type, ArrayList[2]: CivilGrdId, ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId
        ArrayList GradeArr = GetOfficeImpGrade(OfficeId);
        if (GradeArr.Count != 0)
        {
            _OfficeCapacity.OfficeGradeId = (int)GradeArr[0];
            _OfficeCapacity.DocOffOfficeMembersQualificationType = (int)GradeArr[1];
            int CivilGrdId = (int)GradeArr[2];
            OfficeManager.FindByCode(OfficeId);
            if (OfficeManager.Count != 1)
                return _OfficeCapacity;
            if (Utility.IsDBNullOrNullValue(OfficeManager[0]["ActivityType"]))
                return _OfficeCapacity;
            _OfficeCapacity.OfficeActivityType = Convert.ToInt32(OfficeManager[0]["ActivityType"]);

            _OfficeCapacity.OfficeConditionalCapacityImplement = GetConditionalCapacity(OfficeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);

            if (_OfficeCapacity.DocOffOfficeMembersQualificationType == (int)TSP.DataManager.DocOffOfficeMembersQualificationType.Kardan_Kardan)
                OfficeMembersQualificationManager.FindByGrdId(_OfficeCapacity.OfficeGradeId, _OfficeCapacity.DocOffOfficeMembersQualificationType, CivilGrdId, _OfficeCapacity.OfficeActivityType);
            else
                OfficeMembersQualificationManager.FindByGrdId(_OfficeCapacity.OfficeGradeId, _OfficeCapacity.DocOffOfficeMembersQualificationType, null, _OfficeCapacity.OfficeActivityType);

            if (OfficeMembersQualificationManager.Count > 0)
            {
                //CapacityAssArr ----> ArrayList[0] = MaxJobCount, ArrayList[1] = MaxJobCapacity
                CalculateMaxJobCountByCapacityAssignment(Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxJobCount"]), Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxCapacity"]) + GetCapacityOfPoints(OfficeId, Convert.ToInt32(GradeArr[3]), Convert.ToInt32(GradeArr[4]), Convert.ToInt32(OfficeMembersQualificationManager[0]["PointsLimitation"])));
                _OfficeCapacity.OfficeMaxFloorCount = Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxFloor"]);
                _OfficeCapacity.OfficeMemberMaxJobCapacity = Convert.ToInt32(_MemberCapacity.MemberMaxJobCapacity) + _OfficeCapacity.OfficeConditionalCapacityImplement;
                _OfficeCapacity.OfficeMemberMaxJobCount = Convert.ToInt32(_MemberCapacity.MemberMaxJobCount);
            }
        }
        return _OfficeCapacity;
    }

    /// <summary>
    /// مجموع امتیاز اعضا یک شرکت اجرا را بر می گرداند
    /// </summary>
    private int GetCapacityOfPoints(int OfficeId, int CivilMeId, int SecondMeId, int PointsLimitation)
    {
        TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocOffGradeValuesManager DocOffGradeValuesManager = new TSP.DataManager.DocOffGradeValuesManager();
        int Capacity = 0;

        OfficeMemberManager = GetOfficeAllMembers(OfficeId);
        OfficeMemberManager.CurrentFilter = "PersonId <>" + CivilMeId + "AND PersonId <>" + SecondMeId;

        for (int i = 0; i < OfficeMemberManager.Count; i++)
        {
            int GradeId = 0;
            int GrdType = 0;
            int OfmType = Convert.ToInt32(OfficeMemberManager[i]["OfmType"]);

            if (OfmType == (int)TSP.DataManager.OfficeMemberType.Member)
            {
                GradeId = GetGradeByMFId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                GrdType = (int)TSP.DataManager.DocOffGradeValuesGrdType.Engineer;
            }
            else if (OfmType == (int)TSP.DataManager.OfficeMemberType.Kardan)
            {
                GradeId = GetTechnicianGrade(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                GrdType = (int)TSP.DataManager.DocOffGradeValuesGrdType.Technician;
            }
            else if (OfmType == (int)TSP.DataManager.OfficeMemberType.Memar)
            {
                GradeId = GetTechnicianGrade(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                GrdType = (int)TSP.DataManager.DocOffGradeValuesGrdType.ExperimentalArchitect;
            }

            DocOffGradeValuesManager.FindByGrdId(GradeId, GrdType);
            if (DocOffGradeValuesManager.Count > 0)
                Capacity += Convert.ToInt32(DocOffGradeValuesManager[0]["Value"]) * Convert.ToInt32(DocOffGradeValuesManager[0]["CapacityPerValue"]);
        }

        if (Capacity > PointsLimitation)
            Capacity = PointsLimitation;

        return Capacity;
    }
    #endregion
    #endregion

    #region توابع بدست آوردن ظرفیت رزرو شده

    /// <summary>
    /// ظرفیت رزرو شده فرد، شرکت یا یک دفتر را بر می گرداند
    /// </summary>
    private int GetTotalReservedCapacity(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId)
    {
        int CapacityDecrement = 0;

        switch (ProjectIngridientTypeId)
        {
            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                CapacityDecrement += ReservedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOfficeEngOId, MemberTypeId);
                CapacityDecrement += ReservedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOfficeEngOId, MemberTypeId);
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                CapacityDecrement += ReservedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
                break;
        }

        return CapacityDecrement;
    }

    private int ReservedCapacity(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId)
    {
        int CapacityDecrement = 0;
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        ProjectCapacityDecrementManager.FindReservedCapacity(MeOfficeEngOId, ProjectIngridientTypeId, MemberTypeId);
        for (int i = 0; i < ProjectCapacityDecrementManager.Count; i++)
            CapacityDecrement += Convert.ToInt32(ProjectCapacityDecrementManager[i]["CapacityDecrement"]);
        return CapacityDecrement;
    }
    #endregion

    #region محاسبه ظرفیت مصرف شده
    /// <summary>
    /// ظرفیت مصرف شده فرد، شرکت یا یک دفتر را بر می گرداند
    /// </summary>
    private int GetTotalUsedCapacity(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId, Boolean CalculateByAssignmentPercent, int TotalDesignCapacity, int TotalObservationCapacity)
    {
        int CapacityDecrement = 0;
        //int UsedDsg = 0;
        //int UsedObs = 0;

        IngridientUsedDesignCapacityValue = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOfficeEngOId, MemberTypeId, CalculateByAssignmentPercent);
        IngridientUsedObserveCapacityValue = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOfficeEngOId, MemberTypeId, CalculateByAssignmentPercent);
        IngridientUsedImplementCapacityValue = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOfficeEngOId, MemberTypeId, CalculateByAssignmentPercent);

        switch (ProjectIngridientTypeId)
        {
            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                IngridientUsedDesignCapacityValue = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOfficeEngOId, MemberTypeId, CalculateByAssignmentPercent);
                if (TotalObservationCapacity != 0)
                    IngridientUsedObserveCapacityValue = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOfficeEngOId, MemberTypeId, CalculateByAssignmentPercent) * TotalDesignCapacity / TotalObservationCapacity;
                CapacityDecrement = IngridientUsedDesignCapacityValue + IngridientUsedObserveCapacityValue;
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                if (TotalDesignCapacity != 0)
                    IngridientUsedDesignCapacityValue = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOfficeEngOId, MemberTypeId, CalculateByAssignmentPercent) * TotalObservationCapacity / TotalDesignCapacity;
                IngridientUsedObserveCapacityValue = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOfficeEngOId, MemberTypeId, CalculateByAssignmentPercent);
                CapacityDecrement = IngridientUsedDesignCapacityValue + IngridientUsedObserveCapacityValue;
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                CapacityDecrement = IngridientUsedImplementCapacityValue = UsedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId, CalculateByAssignmentPercent);
                break;
        }
        return CapacityDecrement;
    }

    private int UsedCapacity(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId, Boolean CalculateByAssignmentPercent)
    {
        int CapacityDecrement = 0;
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        if (CalculateByAssignmentPercent)
            ProjectCapacityDecrementManager.FindUsedCapacityPerStage(MeOfficeEngOId, ProjectIngridientTypeId, MemberTypeId);
        else
            ProjectCapacityDecrementManager.FindUsedCapacity(MeOfficeEngOId, ProjectIngridientTypeId, MemberTypeId);
        for (int i = 0; i < ProjectCapacityDecrementManager.Count; i++)
            CapacityDecrement += Convert.ToInt32(ProjectCapacityDecrementManager[i]["CapacityDecrement"]);
        return CapacityDecrement;
    }

    /// <summary>
    /// ظرفیت مصرف شده یکی از اعضا یک شرکت یا یک دفتر را بر اساس اختصاص ظرفیت بر می گرداند
    /// </summary>
    private int OfficeMembersUsedCapacityPerStage(int ProjectIngridientTypeId, int MeOthPId, int MemberTypeId)
    {
        int CapacityDecrement = 0;
        TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
        ProjectOfficeMembersManager.FindUsedCapacityPerStage(MeOthPId, ProjectIngridientTypeId, MemberTypeId);
        for (int i = 0; i < ProjectOfficeMembersManager.Count; i++)
            CapacityDecrement += Convert.ToInt32(ProjectOfficeMembersManager[i]["CapacityDecrement"]);
        return CapacityDecrement;
    }

    #endregion

    #region تعداد پروژه های در دست اجرا فرد، شرکت یا یک دفتر را بر می گرداند

    /// <summary>
    /// تعداد پروژه های در دست اجرا فرد، شرکت یا یک دفتر را بر می گرداند
    /// </summary>
    private int GetTotalProjectNum(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId)
    {
        int ProjectNum = 0;

        switch (ProjectIngridientTypeId)
        {
            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                ProjectNum += CurrentProjectNum((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOfficeEngOId, MemberTypeId);
                ProjectNum += CurrentProjectNum((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOfficeEngOId, MemberTypeId);
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                ProjectNum += CurrentProjectNum(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
                break;
        }

        return ProjectNum;
    }

    private int CurrentProjectNum(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId)
    {
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        ProjectCapacityDecrementManager.FindUsedCapacity(MeOfficeEngOId, ProjectIngridientTypeId, MemberTypeId);
        return ProjectCapacityDecrementManager.Count;
    }
    #endregion
    //**********************************************************************************************************************************************************************************
    #region محاسبه ظرفیت کاردان

    /// <summary>
    /// ظرفیت یک کاردان را بر می گرداند
    /// </summary>
    private int GetTechnicianCapacity(int OtpId, int DocumentResponsibilityType)
    {
        switch (DocumentResponsibilityType)
        {
            case (int)TSP.DataManager.DocumentResponsibilityType.Design:
                return 0;
                break;

            case (int)TSP.DataManager.DocumentResponsibilityType.Observation:
                return 0;
                break;

            case (int)TSP.DataManager.DocumentResponsibilityType.Implement:
                return 0;
                break;

            default:
                return 0;
                break;
        }
    }

    #endregion

    #region بدست آوردن اطلاعات "اختصاص ظرفیت" در سال جاری
    /// <summary>
    /// مقدار ظرفیت را بر اساس درصد ""اختصاص ظرفیت"" جاری محاسبه می کند
    /// ArrayList[0] = MaxJobCount, ArrayList[1] = MaxJobCapacity
    /// Set Peroperties of "MemberCapacity" Class 
    /// </summary>
    private void CalculateMaxJobCountByCapacityAssignment(int CapacityMaxJobCount, int CapacityMaxJobCapacity)
    {
        GetCurrentPrcntsSum();

        int CapacityPrcntSum = Convert.ToInt32(CapacityAssignmentCapacityPrcntSum);
        int JobCountPrcntSum = Convert.ToInt32(CapacityAssignmentJobCountPrcntSum);

        _MemberCapacity.MemberMaxJobCount = Math.Ceiling(Convert.ToDouble(JobCountPrcntSum * CapacityMaxJobCount) / 100);
        _MemberCapacity.MemberMaxJobCapacity = Math.Ceiling(Convert.ToDouble(CapacityPrcntSum * CapacityMaxJobCapacity) / 100);
    }

    /// <summary>
    /// اختصاص ظرفیت مرحله جاری را بر می گرداند
    /// ArrayList[0] = Year, ArrayList[1] = StageText, ArrayList[0] = CapacityPrcnt, ArrayList[1] = JobCountPrcnt
    /// </summary>
    public ArrayList GetCurrentStage()
    {
        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        CapacityAssignmentManager.SelectCurrentYearAndStage(1);
        GetCurrentPrcntsSum();
        ArrayList CurrentStageArr = new ArrayList();
        if (CapacityAssignmentManager.Count > 0 && (CapacityAssignmentCapacityPrcntSum != 0 || CapacityAssignmentJobCountPrcntSum != 0))
        {
            CurrentStageArr.Add(CapacityAssignmentManager[0]["Year"]);
            CurrentStageArr.Add(CapacityAssignmentManager[0]["StageText"]);
            CurrentStageArr.Add(CapacityAssignmentCapacityPrcntSum);
            CurrentStageArr.Add(CapacityAssignmentJobCountPrcntSum);
        }

        return CurrentStageArr;
    }

    /// <summary>
    /// ظرفیت اضافی یا کم شده یک شخص یا شرکت یا دفتر را بر می گرداند
    /// </summary>
    private int GetConditionalCapacity(int MeOfficeEngOId, int ProjectIngridientTypeId)
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
    /// درصد اختصاص ظرفیت جاری را بر می گرداند
    /// ArrayList[0] = CapacityPrcntSum, ArrayList[1] = JobCountPrcntSum
    /// </summary>
    private void GetCurrentPrcntsSum()
    {
        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        ArrayList CapacityAssArr = CapacityAssignmentManager.GetCurrentPrcntsSum();
        CapacityAssignmentCapacityPrcntSum = Convert.ToInt32(CapacityAssArr[0]);
        CapacityAssignmentJobCountPrcntSum = Convert.ToInt32(CapacityAssArr[1]);
    }
    #endregion

    #region بدست آوردن پایه
    /// <summary>
    /// پایه یک عضو را بر می گرداند
    /// </summary>
    public int GetMemGrade(int MeId, int ProjectIngridientTypeId)
    {
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        int ResponsibilityType = 0;

        switch (ProjectIngridientTypeId)
        {
            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                ResponsibilityType = (int)TSP.DataManager.DocumentResponsibilityType.Design;
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                ResponsibilityType = (int)TSP.DataManager.DocumentResponsibilityType.Implement;
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                ResponsibilityType = (int)TSP.DataManager.DocumentResponsibilityType.Observation;
                break;
        }

        ArrayList GradeArr = DocMemberFileDetailManager.FindActiveResByResponsibility(MeId, ResponsibilityType);
        if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Observer && GradeArr.Count == 0)
        {
            GradeArr = DocMemberFileDetailManager.FindActiveResByResponsibility(MeId, (int)TSP.DataManager.DocumentResponsibilityType.Mapping);
        }
        ArrayList GradeArr2 = new ArrayList();
        if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Observer)
        {
            GradeArr2 = DocMemberFileDetailManager.FindActiveResByResponsibility(MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
        }
        else if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Designer)
        {
            GradeArr2 = DocMemberFileDetailManager.FindActiveResByResponsibility(MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
        }
        if (GradeArr2.Count != 0 && GradeArr.Count != 0)
        {
            if (Convert.ToInt32(GradeArr2[0]) < Convert.ToInt32(GradeArr[0]))
                return Convert.ToInt32(GradeArr2[0]);
        }
        if (GradeArr.Count != 0)
            return Convert.ToInt32(GradeArr[0]);
        else
            return 0;
    }

    /// <summary>
    /// پایه یک عضو را بر اساس یک پروانه خاص بر می گرداند
    /// </summary>
    private int GetGradeByMFId(int MFId, int MeId, int ProjectIngridientTypeId)
    {
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        int ResponsibilityType = 0;

        switch (ProjectIngridientTypeId)
        {
            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                ResponsibilityType = (int)TSP.DataManager.DocumentResponsibilityType.Design;
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                ResponsibilityType = (int)TSP.DataManager.DocumentResponsibilityType.Implement;
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                ResponsibilityType = (int)TSP.DataManager.DocumentResponsibilityType.Observation;
                break;
        }

        DataTable dt = DocMemberFileDetailManager.FindByResponsibilityAndMainMajor(MFId, MeId, ResponsibilityType);
        if (dt.Rows.Count > 0)
            return Convert.ToInt32(dt.Rows[0]["GrdId"]);
        else
            return 0;
    }

    /// <summary>
    /// پایه یک کاردان یا معمار تجربی را بر می گرداند
    /// </summary>
    private int GetTechnicianGrade(int OtpId, int ProjectIngridientTypeId)
    {
        TSP.DataManager.DocOffMemberAcceptedGradeManager MemberAcceptedGradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        int ResponsibilityType = 0;

        switch (ProjectIngridientTypeId)
        {
            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                ResponsibilityType = (int)TSP.DataManager.DocumentResponsibilityType.Design;
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                ResponsibilityType = (int)TSP.DataManager.DocumentResponsibilityType.Implement;
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                ResponsibilityType = (int)TSP.DataManager.DocumentResponsibilityType.Observation;
                break;
        }

        return MemberAcceptedGradeManager.GetGradeId(OtpId, ResponsibilityType);
    }

    /// <summary>
    /// پایه یک مجری حقوقی را بر می گرداند
    /// ArrayList[0]: GradeId, ArrayList[1]: Type:DocOffOfficeMembersQualificationType, ArrayList[2]: CivilGrdId, ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId
    /// </summary>
    private ArrayList GetOfficeImpGrade(int OfficeId)
    {
        TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
        ArrayList GradeArr = OfficeMemberManager.FindOfficeImpGrade(OfficeId);
        return GradeArr;
    }
    #endregion

    #region بدست آوردن اطلاعات عضویت/پروانه دفاتر-شرکت-عضو
    #region Members Info
    /// <summary>
    /// رشته موضوع پروانه یک عضو را بر می گرداند
    /// ArrayList[0]: MjId, ArrayList[1]: MjName
    /// </summary>
    private void GetMemberDocMasterMajor(int MeId, int MfId)
    {
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        if (MfId == -1)
        {
            DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
            if (DocMemberFileMajorManager.Count != 0)
            {
                if (!Utility.IsDBNullOrNullValue(DocMemberFileMajorManager[0]["MjId"]))
                    _MemberCapacity.MemberMajorId = Convert.ToInt32(DocMemberFileMajorManager[0]["MjId"]);
                if (!Utility.IsDBNullOrNullValue(DocMemberFileMajorManager[0]["MjName"]))
                    _MemberCapacity.MemberMajorName = DocMemberFileMajorManager[0]["MjName"].ToString();
            }
        }
        else
        {
            DataTable dtDocMajor = DocMemberFileMajorManager.SelectMemberFileById(MfId, MeId, 0, 1);
            if (dtDocMajor.Rows.Count != 0)
            {
                if (!Utility.IsDBNullOrNullValue(dtDocMajor.Rows[0]["MjId"]))
                    _MemberCapacity.MemberMajorId = Convert.ToInt32(dtDocMajor.Rows[0]["MjId"]);
                if (!Utility.IsDBNullOrNullValue(dtDocMajor.Rows[0]["MjName"]))
                    _MemberCapacity.MemberMajorName = dtDocMajor.Rows[0]["MjName"].ToString();
            }
        }
    }

    /// <summary>
    /// رشته موضوع پروانه یک عضو را بر می گرداند
    /// ArrayList[0]: MjId, ArrayList[1]: MjName
    /// </summary>
    private void GetMemberDocMasterMajor(int MeId)
    {
        GetMemberDocMasterMajor(MeId, -1);
    }

    private string GetMeName(int MeId)
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count > 0)
            return MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
        else
            return "";
    }

    /// <summary>
    /// رشته اصلی شخص حقیقی در عضویت را بدست می آورد
    /// </summary>
    /// <param name="MeId"></param>
    /// <returns></returns>
    private int GetMjId(int MeId)
    {
        int MjId = 0;
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count > 0)
            int.TryParse(MemberManager[0]["LastMjId"].ToString(), out MjId);
        return MjId;
    }
    #endregion

    #region Office/EngOffice Info
    /// <summary>
    /// اعضای فعال شرکت یا دفتر (اعضای حقیقی) را بر می گرداند
    /// </summary>
    private TSP.DataManager.OfficeMemberManager GetOfficeMembers(int OfficeEngoId, int DocOffIncreaseJobCapacityType)
    {
        TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
        if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
            //?????????OfficeMemberManager.FindOfficeActiveMembers(OfficeEngoId, (int)TSP.DataManager.OfficeMemberType.Member, 0, -1);
            //***اعضای غیرفعال شده را نیز بر می گرداند
            OfficeMemberManager.FindByOffRequest(OfficeEngoId, -1, -1, -1, 2, -1, (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed, -1);
        else if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice)
            OfficeMemberManager.FindEngOfficeActiveMembers(OfficeEngoId, 0, -1);//****اعضای دفتر تنها از اعضای حقیقی هستند

        return OfficeMemberManager;
    }

    /// <summary>
    /// اعضای فعال شرکت یا دفتر را بر می گرداند
    /// این تابع برای استفاده در 
    /// objectDataSource می باشد
    /// </summary>
    /// <param name="OfficeEngoId"></param>
    /// <param name="DocOffIncreaseJobCapacityType">نوع عامل پروژه:دفتر/شرکت/عضو حقیقی...</param>
    /// <returns></returns>
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetOffMembers(int OfficeEngoId, int DocOffIncreaseJobCapacityType)
    {
        TSP.DataManager.OfficeMemberManager OfficeMemberManager = GetOfficeMembers(OfficeEngoId, DocOffIncreaseJobCapacityType);
        return OfficeMemberManager.DataTable;
    }

    /// <summary>
    /// اعضا و کاردان و معمارهای فعال شرکت را بر می گرداند
    /// </summary>
    private TSP.DataManager.OfficeMemberManager GetOfficeAllMembers(int OfficeId)
    {
        TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
        OfficeMemberManager.FindOfficeActiveMembers(OfficeId, -1, 0, -1);

        return OfficeMemberManager;
    }
    #endregion

    #region OtherPerson Info
    /// <summary>
    /// ArrayList[0]= MeName, ArrayList[1]= MjId
    /// </summary>
    private ArrayList GetOthPersonName(int OtpId)
    {
        ArrayList Arr = new ArrayList();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();
        OtherPersonManager.FindByCode(OtpId);
        if (OtherPersonManager.Count > 0)
        {
            Arr.Add(OtherPersonManager[0]["FirstName"].ToString() + " " + OtherPersonManager[0]["LastName"].ToString());
            Arr.Add(OtherPersonManager[0]["MjId"]);
        }
        else
        {
            Arr.Add("");
            Arr.Add("");
        }
        return Arr;
    }
    #endregion
    #endregion

    #region  Public Methods Insert-Update  

    /// <summary>
    /// ProjectOfficeMembersManager: Is Added To Transaction
    /// ReturnValue: PK
    /// </summary>
    public int InsertPrjOffMembersForOfficeMembers(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, string CapacityDecrement, string Wage, int ProjectIngridientTypeId, int PrjImpObsDsgnId, int MeId, int CurrentUserId)
    {
        int ProjectOfficeMembersId;
        DataRow rowProjectOfficeMembers = ProjectOfficeMembersManager.NewRow();

        rowProjectOfficeMembers.BeginEdit();
        rowProjectOfficeMembers["ProjectIngridientTypeId"] = ProjectIngridientTypeId;
        rowProjectOfficeMembers["PrjImpObsDsgnId"] = PrjImpObsDsgnId;
        rowProjectOfficeMembers["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.Member;
        rowProjectOfficeMembers["MeOthPId"] = MeId;
        rowProjectOfficeMembers["CapacityDecrement"] = CapacityDecrement;
        rowProjectOfficeMembers["Wage"] = Wage;
        rowProjectOfficeMembers["IsFree"] = 0;
        rowProjectOfficeMembers["IsDecreased"] = 0;
        rowProjectOfficeMembers["OfficeMemberTypeId"] = (int)TSP.DataManager.TSMemberType.Office;
        rowProjectOfficeMembers["UserId"] = CurrentUserId;
        rowProjectOfficeMembers["ModifiedDate"] = DateTime.Now;
        rowProjectOfficeMembers.EndEdit();

        ProjectOfficeMembersManager.AddRow(rowProjectOfficeMembers);

        ProjectOfficeMembersManager.Save();

        ProjectOfficeMembersManager.DataTable.AcceptChanges();
        ProjectOfficeMembersId = Convert.ToInt32(ProjectOfficeMembersManager[0]["PrjCapacityDecrementId"]);
        return ProjectOfficeMembersId;
    }

    /// <summary>
    /// ProjectOfficeMembersManager: Is Added To Transaction
    /// ReturnValue: PK
    /// </summary>
    public int InsertPrjOffMembersForOfficeTechnicians(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, string CapacityDecrement, string Wage, int ProjectIngridientTypeId, int PrjImpObsDsgnId, int OthPId, int CurrentUserId)
    {
        int ProjectOfficeMembersId;
        DataRow rowProjectOfficeMembers = ProjectOfficeMembersManager.NewRow();

        rowProjectOfficeMembers.BeginEdit();
        rowProjectOfficeMembers["ProjectIngridientTypeId"] = ProjectIngridientTypeId;
        rowProjectOfficeMembers["PrjImpObsDsgnId"] = PrjImpObsDsgnId;
        rowProjectOfficeMembers["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.OtherPerson;
        rowProjectOfficeMembers["MeOthPId"] = OthPId;
        rowProjectOfficeMembers["CapacityDecrement"] = CapacityDecrement;
        rowProjectOfficeMembers["Wage"] = Wage;
        rowProjectOfficeMembers["IsFree"] = 0;
        rowProjectOfficeMembers["IsDecreased"] = 0;
        rowProjectOfficeMembers["OfficeMemberTypeId"] = (int)TSP.DataManager.TSMemberType.Office;
        rowProjectOfficeMembers["UserId"] = CurrentUserId;
        rowProjectOfficeMembers["ModifiedDate"] = DateTime.Now;
        rowProjectOfficeMembers.EndEdit();

        ProjectOfficeMembersManager.AddRow(rowProjectOfficeMembers);

        ProjectOfficeMembersManager.Save();

        ProjectOfficeMembersManager.DataTable.AcceptChanges();
        ProjectOfficeMembersId = Convert.ToInt32(ProjectOfficeMembersManager[0]["PrjCapacityDecrementId"]);
        return ProjectOfficeMembersId;
    }

    /// <summary>
    /// ProjectOfficeMembersManager: Is Added To Transaction
    /// ReturnValue: PK
    /// </summary>
    public int InsertPrjOffMembersForEngOfficeMembers(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, string CapacityDecrement, string Wage, int ProjectIngridientTypeId, int PrjImpObsDsgnId, int MeId, int CurrentUserId)
    {
        int ProjectOfficeMembersId;
        DataRow rowProjectOfficeMembers = ProjectOfficeMembersManager.NewRow();

        rowProjectOfficeMembers.BeginEdit();
        rowProjectOfficeMembers["ProjectIngridientTypeId"] = ProjectIngridientTypeId;
        rowProjectOfficeMembers["PrjImpObsDsgnId"] = PrjImpObsDsgnId;
        rowProjectOfficeMembers["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.Member;
        rowProjectOfficeMembers["MeOthPId"] = MeId;
        rowProjectOfficeMembers["CapacityDecrement"] = CapacityDecrement;
        rowProjectOfficeMembers["Wage"] = Wage;
        rowProjectOfficeMembers["IsFree"] = 0;
        rowProjectOfficeMembers["IsDecreased"] = 0;
        rowProjectOfficeMembers["OfficeMemberTypeId"] = (int)TSP.DataManager.TSMemberType.EngOffice;
        rowProjectOfficeMembers["UserId"] = CurrentUserId;
        rowProjectOfficeMembers["ModifiedDate"] = DateTime.Now;
        rowProjectOfficeMembers.EndEdit();

        ProjectOfficeMembersManager.AddRow(rowProjectOfficeMembers);

        ProjectOfficeMembersManager.Save();

        ProjectOfficeMembersManager.DataTable.AcceptChanges();
        ProjectOfficeMembersId = Convert.ToInt32(ProjectOfficeMembersManager[0]["PrjCapacityDecrementId"]);
        return ProjectOfficeMembersId;
    }

    /// <summary>
    /// ProjectOfficeMembersManager: Is Added To Transaction
    /// ReturnValue: PK
    /// </summary>
    public int InsertPrjOffMembersForEngOfficeTechnicians(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, string CapacityDecrement, string Wage, int ProjectIngridientTypeId, int PrjImpObsDsgnId, int OthPId, int CurrentUserId)
    {
        int ProjectOfficeMembersId;
        DataRow rowProjectOfficeMembers = ProjectOfficeMembersManager.NewRow();

        rowProjectOfficeMembers.BeginEdit();
        rowProjectOfficeMembers["ProjectIngridientTypeId"] = ProjectIngridientTypeId;
        rowProjectOfficeMembers["PrjImpObsDsgnId"] = PrjImpObsDsgnId;
        rowProjectOfficeMembers["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.OtherPerson;
        rowProjectOfficeMembers["MeOthPId"] = OthPId;
        rowProjectOfficeMembers["CapacityDecrement"] = CapacityDecrement;
        rowProjectOfficeMembers["Wage"] = Wage;
        rowProjectOfficeMembers["IsFree"] = 0;
        rowProjectOfficeMembers["IsDecreased"] = 0;
        rowProjectOfficeMembers["OfficeMemberTypeId"] = (int)TSP.DataManager.TSMemberType.EngOffice;
        rowProjectOfficeMembers["UserId"] = CurrentUserId;
        rowProjectOfficeMembers["ModifiedDate"] = DateTime.Now;
        rowProjectOfficeMembers.EndEdit();

        ProjectOfficeMembersManager.AddRow(rowProjectOfficeMembers);

        ProjectOfficeMembersManager.Save();

        ProjectOfficeMembersManager.DataTable.AcceptChanges();
        ProjectOfficeMembersId = Convert.ToInt32(ProjectOfficeMembersManager[0]["PrjCapacityDecrementId"]);
        return ProjectOfficeMembersId;
    }

    /// <summary>
    /// ProjectOfficeMembersManager: Is Added To Transaction
    /// </summary>    
    public void UpdateProjectOfficeMembers(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, string CapacityDecrement, string Wage, int ProjectOfficeMembersId, int CurrentUserId)
    {
        ProjectOfficeMembersManager.FindByProjectOfficeMembersId(ProjectOfficeMembersId);
        if (ProjectOfficeMembersManager.Count > 0)
        {
            ProjectOfficeMembersManager[0].BeginEdit();
            ProjectOfficeMembersManager[0]["CapacityDecrement"] = CapacityDecrement;
            ProjectOfficeMembersManager[0]["Wage"] = Wage;
            ProjectOfficeMembersManager[0]["UserId"] = CurrentUserId;
            ProjectOfficeMembersManager[0]["ModifiedDate"] = DateTime.Now;
            ProjectOfficeMembersManager[0].EndEdit();

            ProjectOfficeMembersManager.Save();
        }
    }

    /// <summary>
    /// ProjectOfficeMembersManager: Is Added To Transaction And Has The Record Taht Will Be Updated
    /// </summary>    
    public void UpdateProjectOfficeMembers(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, string CapacityDecrement, string Wage, int CurrentUserId)
    {
        if (ProjectOfficeMembersManager.Count > 0)
        {
            ProjectOfficeMembersManager[0].BeginEdit();
            ProjectOfficeMembersManager[0]["CapacityDecrement"] = CapacityDecrement;
            ProjectOfficeMembersManager[0]["Wage"] = Wage;
            ProjectOfficeMembersManager[0]["UserId"] = CurrentUserId;
            ProjectOfficeMembersManager[0]["ModifiedDate"] = DateTime.Now;
            ProjectOfficeMembersManager[0].EndEdit();

            ProjectOfficeMembersManager.Save();
        }
    }
    #endregion

    #region چک کردن تعداد کار مجاز و ظرفیت

    #region شرکت/دفتر/عضو حقیقی
    /// <summary>
    /// تعداد کار مجاز و ظرفیت یک فرد، شرکت یا یک دفتر را چک می کند
    /// </summary>
    /// <param name="ProjectIngridientTypeId"></param>
    /// <param name="MemberTypeId"></param>
    /// <param name="MeOfficeEngOId"></param>
    /// <param name="Capacity">میزان ظرفیت مورد نظر جهت کسر شدن</param>
    /// <returns></returns>
    public string CheckCapacityAndJobCount(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, int Capacity)
    {
        return CheckCapacityAndJobCount(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, Capacity, -1);
    }

    /// <summary>
    /// تعداد کار و طبقات مجاز و ظرفیت یک مجری را چک می کند
    /// </summary>
    public string CheckCapacityAndJobCount(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, int Capacity, int Step)
    {
        //****** int Err = CheckCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, Capacity, Step);
        int Error = 0;
        // ArrayList[0]: TotalCapacity(int), ArrayList[1]:UsedCapacity(int) , ArrayList[2]: RemainCapacity(int), ArrayList[3]:ReservedCapacity(int) , ArrayList[4]: ProjectNum(int), ArrayList[5]: MaxJoubCount(string), ArrayList[6]: MaxFloor(string), ArrayList[7]: ConditionalCapacity(int)
        //*****************اطلاعات ظرفیت فرد، شرکت یا یک دفتر را بر اساس اختصاص ظرفیت بر می گرداند
        GetCapacityInformation(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, true);

        // if (Convert.ToInt32(CapacityArr[2]) < Capacity && Convert.ToInt32(CapacityArr[5]) <= Convert.ToInt32(CapacityArr[4]))
        if (IngridientRemainCapacity < Capacity && IngridientMaxJobCount <= IngridientProjectNum)
            Error = (int)CapacityErr.NotEnoughCapacityAndMaxJobIsTaken;

        //if (Convert.ToInt32(CapacityArr[2]) < Capacity)
        if (IngridientRemainCapacity < Capacity)
            Error = (int)CapacityErr.NotEnoughRmainCapacity;

        //if (Convert.ToInt32(CapacityArr[5]) <= Convert.ToInt32(CapacityArr[4]))
        if (IngridientMaxJobCount <= IngridientProjectNum)
            Error = (int)CapacityErr.MaxJobIsTaken;

        // CapacityArr[6]) = 0 ----> ندارد  CapacityArr[6]) = -1 ----> بدون محدودیت
        //if (Step != -1 && Convert.ToInt32(CapacityArr[6]) != 0 && Convert.ToInt32(CapacityArr[6]) != -1 && Convert.ToInt32(CapacityArr[6]) < Step)
        if (Step != -1 && IngridientMaxFloor != 0 && IngridientMaxFloor != -1 && IngridientMaxFloor < Step)
            Error = (int)CapacityErr.NotEnoughStep;

        return FindErrorMessage(Error);
    }
    #endregion

    #region اعضای شرکت/دفتر
    /// <summary>
    /// تعداد کار مجاز و ظرفیت عضوی از یک شرکت یا یک دفتر را چک می کند
    /// </summary>
    public string CheckOfficecMembersCapacityAndJobCount(int ProjectIngridientTypeId, int MemberTypeId, int OfficeEngOId, int Capacity, int MeOthPId)
    {
        // int Err = CheckOfficeMembersCapacity(ProjectIngridientTypeId, MemberTypeId, OfficeEngOId, Capacity, MeOthPId, -1);
        int Err = 0;
        int DocOffIncreaseJobCapacityType = -1;
        switch (MemberTypeId)
        {
            case (int)TSP.DataManager.TSMemberType.EngOffice:
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice;
                break;
            case (int)TSP.DataManager.TSMemberType.Office:
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office;
                break;
        }

        OfficeCapacity offCap = GetOfficeDesignObservationCapacity(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, true, MeOthPId);
        if (offCap.OfficeMemberRemainCapacity < Capacity && offCap.OfficeMemberMaxJobCount < offCap.OfficeMemberCurrentProjectNum)
            Err = (int)CapacityErr.NotEnoughCapacityAndMaxJobIsTaken;
        else if (offCap.OfficeMemberRemainCapacity < Capacity)
            Err = (int)CapacityErr.NotEnoughRmainCapacity;
        else if (offCap.OfficeMemberMaxJobCount < offCap.OfficeMemberCurrentProjectNum)
            Err = (int)CapacityErr.MaxJobIsTaken;
        //  return 0;
        return FindErrorMessage(Err); ;
    }

    /// <summary>
    /// تعداد کار مجاز و ظرفیت عضوی از یک شرکت یا یک دفتر را چک می کند
    /// </summary>
    private int CheckOfficeMembersCapacity(int ProjectIngridientTypeId, int MemberTypeId, int OfficeEngOId, int Capacity, int MeOthPId, int Step)
    {
        int DocOffIncreaseJobCapacityType = -1;
        switch (MemberTypeId)
        {
            case (int)TSP.DataManager.TSMemberType.EngOffice:
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice;
                break;
            case (int)TSP.DataManager.TSMemberType.Office:
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office;
                break;
        }

        OfficeCapacity offCap = GetOfficeDesignObservationCapacity(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, true, MeOthPId);
        if (offCap.OfficeMemberRemainCapacity < Capacity && offCap.OfficeMemberMaxJobCount < offCap.OfficeMemberCurrentProjectNum)
            return (int)CapacityErr.NotEnoughCapacityAndMaxJobIsTaken;
        if (offCap.OfficeMemberRemainCapacity < Capacity)
            return (int)CapacityErr.NotEnoughRmainCapacity;
        if (offCap.OfficeMemberMaxJobCount < offCap.OfficeMemberCurrentProjectNum)
            return (int)CapacityErr.MaxJobIsTaken;
        return 0;
    }
    #endregion
    #endregion

    #region  بدست آوردن تعداد کار اعضای شرکت

    /// <summary>
    /// تعداد پروژه های در دست اجرا عضوی از یک شرکت یا یک دفتر را بر اساس نوع فعالیت(طراحی/نظارت/اجرا) بر می گرداند
    /// </summary>
    /// <param name="ProjectIngridientTypeId"></param>
    /// <param name="MeOthPId"></param>
    /// <param name="MemberTypeId"></param>
    /// <returns></returns>
    private int OfficeMembersCurrentProjectNum(int ProjectIngridientTypeId, int MeOthPId, int MemberTypeId)
    {
        TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
        ProjectOfficeMembersManager.FindUsedCapacity(MeOthPId, ProjectIngridientTypeId, MemberTypeId);
        return ProjectOfficeMembersManager.Count;
    }
    #endregion

    #region توابع کاهش ظرفیت

    /// <summary>
    /// ظرفیت فرد، شرکت یا یک دفتر مربوط به یک پروژه را کم می کند
    /// A Transaction Is Needed
    /// </summary>
    public void CapacityDecrement(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, int ProjectId, int PrjImpObsDsgnId, int ProjectIngridientTypeId,int ActivityIngridientTypeId, int CurrentUserId)
    {
        ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(ProjectId, PrjImpObsDsgnId, ProjectIngridientTypeId, ActivityIngridientTypeId);
        if (ProjectCapacityDecrementManager.Count > 0)
        {
            ProjectCapacityDecrementManager[0].BeginEdit();
            ProjectCapacityDecrementManager[0]["IsDecreased"] = 1;
            ProjectCapacityDecrementManager[0]["DecreasedDate"] = Utility.GetDateOfToday();
            ProjectCapacityDecrementManager[0]["UserId"] = CurrentUserId;
            ProjectCapacityDecrementManager[0]["ModifiedDate"] = DateTime.Now;
            ProjectCapacityDecrementManager[0].EndEdit();
        }
        ProjectCapacityDecrementManager.Save();
        //******ظرفیت اعضا شرکت یا یک دفتر مربوط به یک پروژه را کم می کند
        ProjectOfficeMembersManager.FindByProjectIdAndPrjImpObsDsgnId(ProjectId, PrjImpObsDsgnId);
        if (ProjectOfficeMembersManager.Count > 0)
        {
            ProjectOfficeMembersManager[0].BeginEdit();
            ProjectOfficeMembersManager[0]["IsDecreased"] = 1;
            ProjectOfficeMembersManager[0]["DecreasedDate"] = Utility.GetDateOfToday();
            ProjectOfficeMembersManager[0]["UserId"] = CurrentUserId;
            ProjectOfficeMembersManager[0]["ModifiedDate"] = DateTime.Now;
            ProjectOfficeMembersManager[0].EndEdit();
        }
        ProjectOfficeMembersManager.Save();
    }

    #region کاهش ظرفیت مجری
    /// <summary>
    /// ظرفیت مجری های مربوط به یک پروژه را کم می کند
    /// A Transaction Is Needed
    /// </summary>
    public void ImplementersCapacityDecrement(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImplementerManager, int ProjectId, int CurrentUserId)
    {
        ProjectImplementerManager.FindActivesByProjectId(ProjectId);
        for (int i = 0; i < ProjectImplementerManager.Count; i++)
        {
            CapacityDecrement(ProjectCapacityDecrementManager, ProjectOfficeMembersManager, ProjectId, Convert.ToInt32(ProjectImplementerManager[i]["PrjImpId"]), (int)TSP.DataManager.TSProjectIngridientType.Implementer, (int)TSP.DataManager.TSProjectIngridientType.Implementer, CurrentUserId);
        }
    }
    #endregion

    #region کاهش ظرفیت ناظر
    /// <summary>
    /// ظرفیت ناظران مربوط به یک پروژه را کم می کند
    /// A Transaction Is Needed
    /// </summary>
    public void ObserversCapacityDecrement(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObserversManager, int ProjectId, int CurrentUserId)
    {
        ProjectObserversManager.FindActivesByProjectId(ProjectId);
        for (int i = 0; i < ProjectObserversManager.Count; i++)
        {
            CapacityDecrement(ProjectCapacityDecrementManager, ProjectOfficeMembersManager, ProjectId, Convert.ToInt32(ProjectObserversManager[i]["ProjectObserversId"]), (int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, CurrentUserId);
        }
    }
    #endregion

    #region کاهش ظرفیت طراح
    /// <summary>
    /// ظرفیت طراحان مربوط به یک پروژه را کم می کند
    /// A Transaction Is Needed
    /// </summary>
    public void DesignersCapacityDecrement(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager, int ProjectId, int CurrentUserId)
    {
        ProjectDesignerManager.FindActivesByProjectId(ProjectId);
        for (int i = 0; i < ProjectDesignerManager.Count; i++)
        {
            CapacityDecrement(ProjectCapacityDecrementManager, ProjectOfficeMembersManager, ProjectId, Convert.ToInt32(ProjectDesignerManager[i]["PrjDesignerId"]), (int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSProjectIngridientType.Designer, CurrentUserId);
        }
    }
    #endregion
    #endregion

    #region توابع آزاد سازی ظرفیت
    /// <summary>
    /// ظرفیت ناظرین را آزاد می کند
    /// A Transaction Is Needed
    /// </summary>
    public void ToFreeObsCapacity(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, int CurrentUserId)
    {
        ProjectCapacityDecrementManager.FindNotFreeByProjectIngridientTypeId((int)TSP.DataManager.TSProjectIngridientType.Observer);
        for (int i = 0; i < ProjectCapacityDecrementManager.Count; i++)
        {
            ProjectCapacityDecrementManager[i].BeginEdit();
            ProjectCapacityDecrementManager[i]["IsFree"] = 1;
            ProjectCapacityDecrementManager[i]["FreeDate"] = Utility.GetDateOfToday();
            ProjectCapacityDecrementManager[i]["UserId"] = CurrentUserId;
            ProjectCapacityDecrementManager[i]["ModifiedDate"] = DateTime.Now;
            ProjectCapacityDecrementManager[i].EndEdit();
        }
        ProjectCapacityDecrementManager.Save();
        ProjectCapacityDecrementManager.DataTable.AcceptChanges();

        /// <summary>
        /// ظرفیت اعضا شرکتهای نظارت را آزاد می کند
        /// </summary>
        ProjectOfficeMembersManager.FindNotFreeByProjectIngridientTypeId((int)TSP.DataManager.TSProjectIngridientType.Observer);
        for (int i = 0; i < ProjectOfficeMembersManager.Count; i++)
        {
            ProjectOfficeMembersManager[i].BeginEdit();
            ProjectOfficeMembersManager[i]["IsFree"] = 1;
            ProjectOfficeMembersManager[i]["UserId"] = CurrentUserId;
            ProjectOfficeMembersManager[i]["ModifiedDate"] = DateTime.Now;
            ProjectOfficeMembersManager[i].EndEdit();
        }
        ProjectOfficeMembersManager.Save();
        ProjectOfficeMembersManager.DataTable.AcceptChanges();
        // ToFreeOfficeMembersObsCapacity(ProjectOfficeMembersManager, CurrentUserId);
    }

    /// <summary>
    /// ظرفیت طراحان را آزاد می کند
    /// A Transaction Is Needed
    /// </summary>
    public void ToFreeDsgnCapacity(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, int CurrentUserId)
    {
        ProjectCapacityDecrementManager.FindNotFreeByProjectIngridientTypeId((int)TSP.DataManager.TSProjectIngridientType.Designer);
        for (int i = 0; i < ProjectCapacityDecrementManager.Count; i++)
        {
            ProjectCapacityDecrementManager[i].BeginEdit();
            ProjectCapacityDecrementManager[i]["IsFree"] = 1;
            ProjectCapacityDecrementManager[i]["FreeDate"] = Utility.GetDateOfToday();
            ProjectCapacityDecrementManager[i]["UserId"] = CurrentUserId;
            ProjectCapacityDecrementManager[i]["ModifiedDate"] = DateTime.Now;
            ProjectCapacityDecrementManager[i].EndEdit();
        }
        ProjectCapacityDecrementManager.Save();
        ProjectCapacityDecrementManager.DataTable.AcceptChanges();

        /// <summary>
        /// ظرفیت اعضا شرکتهای طراحی را آزاد می کند
        /// </summary>
        ProjectOfficeMembersManager.FindNotFreeByProjectIngridientTypeId((int)TSP.DataManager.TSProjectIngridientType.Designer);
        for (int i = 0; i < ProjectOfficeMembersManager.Count; i++)
        {
            ProjectOfficeMembersManager[i].BeginEdit();
            ProjectOfficeMembersManager[i]["IsFree"] = 1;
            ProjectOfficeMembersManager[i]["UserId"] = CurrentUserId;
            ProjectOfficeMembersManager[i]["ModifiedDate"] = DateTime.Now;
            ProjectOfficeMembersManager[i].EndEdit();
        }
        ProjectOfficeMembersManager.Save();
        ProjectOfficeMembersManager.DataTable.AcceptChanges();
    }
    #endregion
}
