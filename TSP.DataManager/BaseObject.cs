using System;

namespace TSP.DataManager
{
    #region Enumerations

    #region tblTableType Info
    /// <summary>
    ///TtId from tblTableType
    /// </summary>
    [Obsolete("Don't use enum TableType insted of use TableCodes -- In TableType case TtCode from tblTableType")]
    public enum TableCodes : int
    {
        Accounting = 93,
        ExpertAnswer = 65,
        ExpertRequest = 67,
        ExpertPlace = 198,
        ExpertFile = 66,
        EngOffice = 87,
        Office = 334,
        SMS = 88,
        SmsCost = 332,
        SMSType = 102,
        Message = 27,
        Member = 333,
        Teachers = 145,
        TeacherLicence = 158,
        TeacherJobHistory = 147,
        Agent = 92,
        TeacherResearchActivity = 148,
        MemberResearchActivity = 24,
        MemberLicence = 22,
        Institue = 149,
        PeriodPresent = 165,
        Seminar = 172,
        SettlementAgent = 209,
        TeacherCourseJudgment = 176,
        TrainingRule = 177,
        TeacherJudgment = 164,
        TrainingJudgment = 179,
        ExpertCosts = 201,
        PeriodCosts = 181,
        SeminarCosts = 182,
        Course = 150,
        MemberRequest = 206,
        DocMemberFile = 342,
        DocMemberFileImp = 343,
        DocMemberFileObs = 344,
        // SeminarTeachers= 216,
        AutomationCartables = 239,
        AutomationLetters = 237,
        AutomationLetterReferences = 238,

        ProjectJobHistory = 326,

        OfficeRequest = 207,
        OfficeFinancialStatus = 210,
        MemberActivity = 20,
        MemberLanguage = 21,
        OfficeMember = 38,
        OfficeLetter = 41,
        OfficeAgent = 37,
        DocMemberExam = 230,
        DocMemberFileMajor = 232,
        DocMemberFileDetail = 233,
        Attachment = 10,

        ProjectJobQuality = 345,
        EngOffFile = 211,
        SmsDoc = 143,

        TechnicalServices = 250,
        TSProject_Implementer = 285,
        TSProject_Designer = 267,
        TSProject_Observers = 293,
        TSImplementerAgent = 284,
        TSPlans = 297,
        TSContract = 265,
        TSProjectRequest = 312,
        TSPlansMethod = 314,
        TSTiming = 308,
        TSProject = 299,
        TSInsurance = 288,
        TSDevelopmentPercent = 276,
        TSConditionalCapacity = 320,

        OtherPerson = 335,
        Employee = 336,
        EmployeeRequest = 218,
        MemberCards = 23,
        TechnicianRequest = 217,
        Setting = 348,
        ExpertRequestSeparate = 356,
        LockHistory = 214,
        ResetPassword = 325,
        OfficememberAcceptedGrade = 367,
        TechniciansActivityAreas = 216,
        AutomationLetterTittleType = 366,
        AutomationLetterDivision = 369,
        AutomationRefrenceDivision = 370,
        AutomationLetterReferenceAims = 371,
        AutomationLetterType = 372,
        AutomationPriority = 373,
        AutomationLetterReferenceTypes = 374,
        AutomationLetterMainAssigner = 375,
        AutomationRefrenceSender = 376,
        QuestionSet = 248,
        PeriodRegister = 176
    }

    /// <summary>
    ///TtCode from tblTableType
    /// </summary>
    public enum TableType
    {
        None = 0,
        NemzamMembers = 100,
        MemberManagement = 101, Member, MemberLicence, MemberLanguage, MemberActivitySubject, MemberCards, MembershipRegistrationStatus, Title,
        TransferMember, MemberRequest, TempMember, TempMemberCardPrint, MemberCardRequestPrint, ReportMemberCardRequest, MemberLicenceInqueryPrint,
        ReportMemberLicenceInquery, ChangeAgentRequest, SaveCardAndAutoConfirm, MemberInfoReport, ChangeBankAccNoRequest, MemberLicenceReport, ExportExcelMembers, CancelMembership,

        OfficeManagement = 150, Office, OfficeAgent, OfficeMember, OfficialLetter, OfficeActivityType, OfficePosition, OfficeType,
        OfficeFinancialStatus, OfficeRequest, OfficeMemberAcceptedGrade,

        TechniciansManagement = 200, Technicians, TechniciansActivityAreas, TechnicianRequest, OtherPersonManagment,

        LockHistory = 250, ProjectJobHistory, ProjectJobQuality,

        EmployeeManagement = 300, Employee, UserRight, ResetPassword, EmployeeRequest,

        LicenceRegistrationUnit = 400, EngOfficeManagement, EngOffice, EngOffFile, EngOfficeType,


        OfficeDocument = 405, OfficeDocuments, OfficeAgentDocument, OfficeMemberDocument, OfficialLetterDocument,
        OfficeFinancialStatusDocument
            , EngOfficeMember, EngOfficeJob, EngOfficeAttachments, OfficeJobDocument, OfficeAttachments, OfficeDocumentAttachments, OfficeDocumentPrintReport, EngOfficeDocumentPrintReport,

        Documents = 450, GradeMajorResponsibility, DocTestType, DocTestCondition, DocUpGradePoint, DocTestConditionDetail, DocMemberExam, DocMemberExamDetail, DocMemberFileMajor, DocMemberFileDetail,
        DocImpDocCity, Grade, MemberFile, MemberFileImp, MemberFileObs, PrintingHistory, ResponsibilityType, DocMemberFileAcceptType, DocMajorTestType, DocResponsibilityRange,
        DocMemberFileJobConfirmatio, AccountingUnitConfirmatDocument, MeDocumteCardLiverty, PrintingHistoryImplementDoc, EditDocMemberFileJobConfirmationDate, DocGasOfficeMembers,DocDelivery,




        Management = 500, ActivitySubject, ActivityType, Attachments, AttachType, City, Commission, Country, Language, LanguageQuality, Licence, Major,
        MaritalStatus, NeededDocument, Province, Religion, ResearchActivity, Sex, Soldier, TableType, University, UserLoginType,
        Group, GroupDetail, Region, SettlementAgent, RegionOfCity, TSMunicipality, Organization, Setting, DefaultThemes, DefaultThemeTypes,
        PrintType, PrintSetting, PrintAssignerSetting, Condolence, MajorParents, ReportBuilder = 537,
        FormBuilderForms, FormBuilderElementTypes, FormBuilderElements, FormBuilderElementItems, FormBuilderGroups, FormBuilderInputForm,

        Automation = 600, MessageManagement, Message, MessageType, PublicMessageGroups, PublicMessages, PublicMessageTypes,
        SMSManagement = 620, SMS, SMSCost, SMSType, SMSReciever, SmsTypeModified, SmsConfirmPerson, SmsDoc, SMSDeliveryReportType, SmsSetting, SmsInbox, ConflictManagement,

        AutomationCartables = 650, AutomationLetters, AutomationLetterReferences, AutomationManageCartableGroups, AutomationLetterFolders,
        AutomationLetterInFolder, AutomationSecretariat, AutomationSecretariatNezamChart, LetterReferenceActions, Resignation,
        LetterLock, AutomationUserRight, AutomationArchive, AutomationLetterCreationTypes, AutomationFormCreatorElementTypes, AutomationFormCreatorElementItems,
        AutomationFormCreatorElements, AutomationLetterInputForm, AutomationLetterTittleType, AutomationLetterDivision, AutomationRefrenceDivision,
        AutomationLetterReferenceAims, AutomationLetterType, AutomationPriority, AutomationLetterReferenceTypes, AutomationLetterMainAssigner,
        AutomationRefrenceSender, AutomationReferenceSetting, AutomationLetterAttachments, AutomationLetterRelation,

        OrganizationalDefinitions = 700, NezamChart, Nezam, NezamMemberChart, NezamMemberPosition, NezamPeriod, Partition,
        AccountingAgent, GovManagerName, GovManagerTitle,


        EntezamiComplain = 800, Complain,
        EntezamiComplainDocument, EntezamiComplainOrder, EntezamiComplainstatus, EntezamiInvitation,
        EntezamiMoteshaki, EntezamiOrder, EntezamiRivision, EntezamiSession, EntezamiSessionMember, EntezamiShaki, EntezamiDocumentType, EntezamiComplainSubject, EntezamiReply,
        EntezamiClausesViolation, EntezamiComplainRequest,

        Expert = 900, ExpertPlaceManagement, ExpertPlace, ExpertAnswer, ExpertRequest, ExpertTitle, ExpertBenfitPercent, ExpertCostAuditList, ExpertCost, ExpertRequestSeparate,
        ExpertFileManagement = 950, ExpertFile/***/, ExpertFileRegion, ExpertFileRequest,

        Learning = 1000, Course, CourseRefrence, CoursePrerequisite, CourseGroups, Teacher, TeacherJobHistory, TeacherResearchActivity, TeacherLicence,
        TeacherCertificate, TeacherJudgment, TeachingGrade, TeachersCourse, TeacherCourseJudgment, TrainingRule, Madrak,
        PeriodPresent, PeriodTimeTable, Test, TestObserver, PeriodAttender, TestAttender, PeriodTestMarks, TrainingAcceptedGrade, PeriodCosts,
        PreRegister, PeriodRegister, Seminar, SeminarCosts, TrainingJudgment, TrainingStatusChange,
        Institue, InstitueFacility, InstitueActivity, InstitueManager, InstitueTeachers, InstitueObserve, InstitueObserver, InstitueJudgment, InstitueCertificate,
        Observer, TrainingQuestinos, Opinions, Questions, PeriodOpinion, QuestionSet, MemberResearchActivity, TeacherSalary, TrainingGroups, PeriodPresentRequest,
        SeminarRequest, Lecturer, ReportMemberPeriods,
        //ConfirmPerson, Confirm,

        AccountingAccount = 1100, AccountingAccGroup, AccountingAccHistory, AccountingAccType,
        AccountingDocBalance, AccountingDocBalanceDetail, AccountingDocOperation, AccountingDocStatus,
        AccountingGroupStatus, AccountingGroupType, AccountingPrintedBankFish, AccountingProject, AccountingService,
        AccountingSaleService, AccountingSaleServiceDetail, AccountingAcc, AccountingTT, AccountingGroupNature, AccountingCheque,
        AccountingChequeStatus, AccountingChequeStatusChange, AccountingChequeStatusChangeDetail, AccountingChequeHeader, AccountingPeriod,
        AccountingStorage, AccountingGoods, AccountingGoodsColor, AccountingGoodsGroup,
        AccountingGoodsStatusType, AccountingGoodsType, AccountingStorageDetail, AccountingUnit, AccountingUnitNames,
        AccountingCostCenter, AccountingCash, AccountingHistory, AccountingAction, AccountingHistoryDetail,
        AccountingPurchase, AccountingPurchaseDetail, AccountingBank, AccountingChequeBook, AccountingChequeBookPages,
        AccountingCoffer, AccountingStorageInventory, AccountingTempInventory, AccountingLoan, AccountingPeriodType, AccountingLoanConditions,
        AccountingForbiddenLoan, AccountingLoanPayment, AccountingGuarantyCheque, AccountingDebt, AccountingBankContradiction,
        AccountingBankContradictionDetail, AccountingContradictionType, AccSettings, CostSettings,
        AccountingAccountAgent = 1158,
        AccountingAccountHoverDetail, AccountingAccGroupHoverDetail, AccountingAccDefultDescriptions,
        AccountingAccountRelations, AccountingDocBalanceDetailRelation, AccountingDocumentBill, AccountingDocumentBillHistory,
        AccountingBillNature, AccountingBillNatureType, AccountingBillStatus, AccountingBillStautsType, AccountingBillType, ReportAccountingFish,


        EpaymentPeriodRegister = 1172, EpaymentPeriodMembership = 1173,

        FileManagement = 1200, Forms, Links, Tender, Rules, Introduction, GalleryAlbum, GalleryImages, HomePageAttachmentManager,
        FormsType = 1209,
        RulesType = 1210, SubSystems = 1211, FAQ, Videos, Podcast, AmoozeshFiles,
        News = 1300, NewsArchive, NewsIdea, NewsSubject, NewsImg,

        WorkFlowManagement = 1400, WorkFlow, WorkFlowState, WorkFlowTask, TaskDoer, WorkFlowTaskLoginType,

        //*****************خدمات مهنسی*********************
        TsAccountingMeRestriction = 1450,
        TechnicalServices_ControlAndEvaluation = 1500, TechnicalServices = 1501, TS_PriceArchive, TSStructureGroups, TS_PriceArchiveStructureItems, TS_PriceArchiveStructureItemDetail, TS_PriceArchiveStructureItemDetailType, TSAttachments, TSAttachType, TSBalcony,
        TSBalconyType = 1510, TSBuildingsLicense, TSContract, TSControler, TSProject_Designer, TSDesigner_Plans, TSDesignerType, TSDesignStatus, TSDevelopmentItemsStatus, TSDevelopmentPercent,
        TSDiscountPercent = 1520, TSFoundation, TSImplementerAgent, TSProject_Implementer, TSInsurance, TSJudgmentGroup, TSLicenseRevival, TSProject_Observers, TSOwner, TSPlans,
        TSPlans_Controler = 1530, TSProject, TSProjectCapacityDecrement, TSTiming, TSTimingItemsStatus, TSProjectRequest, TSRegisteredNo, TSPlansMethod, TSBlock, TSProjectOfficeMembers,
        TSConsultantCompany = 1540, TSTimingPredecessors, TSConditionalCapacity, TSAccounting, TSCapacityAssignment, TSEntrance, TSWalls, TsCapacity, TsProjectIngridientMajors, TSPlansControlerViewPoint,
        DocOffMemberCapacity = 1550, DocOffIncreaseJobCapacity, DocOffEngOfficeImpQualification, DocOffOfficeMembersQualification, TSSetting, TSReport_ObserverReportList, TSReport_ObserverWage, TSReport_ProjectRemainAmount, TSBaseInfoManagment, TSCapacityManagement,
        TSReportManagement = 1560, TSReport_MemberOpration, TSReport_PrintedAccountingFish, TSChangeAccountingStatus, TSEditPayedFhish, TSReport_RemoveObserverReportList, TSChangeAccDocumentStatus, TSReportProjectDesigner, TSProjectUserRight, TSObserverInsurance, TSStructureGroupsAndObserverGrade,
        MemberRestrictionForObserverWorkRequest, TSProjectObserverSelected, ReportObserverSelected, CapacityInMunicipality, UrbanistLimitQualification, UrbanistQualification, TSWorkRequest,
        ObserverPayment, PrintMunicipalityDesPermit, PrintMunicipalityObsPermit, EndProject, ObserverChangeRequest, DesignerChangeRequest, ImpChangeRequest, TraceObserverSelected,
        SaveTSProjectWithOutDesigner, TSProjectChooseWorkYearForObserverAndDesign, RejectObserverSelectByNezam, ChooseCoordinatorObserver, TSNewObserverKardan, TSKardanReport, TSProjectOwnerReport, TSProjectChoosePriceArchiveForObserverAndDesign, TSProjectChooseAgent, TSCapacityRelease,
        TSReportEpaymentObserverFish, TSReportEpaymentDesignerFish, TSReportPlans, TSQueueListMunicipality,
        //*********کنترل ارزیابی*********
        ControlAndEvaluation = 1600, TSBuildingItemsStatus, TSBuildingStatus, TSCommonVisitSchedule, TSDesignerPerformanceEvaluation,
        TSArchitectureCriteriaItems, TSArchitectureCriteriaStatus, TSConnector, TSExecutivePerformanceEvaluation, TSExecutivePerformanceItemsStatus,
        TSExpiredLicenseReport, TSFoundationInfractions, TSHighStructureReport, TSInfractionDescription, TSInspector, TSLowStructureReport, TSObserversAnswer,
        TSObserversPerformanceEvaluation, TSPeriodicVisitSchedule, TSProjectIngridientPerformance, TSRandomVisitSchedule,
        TSSuccessorSupremeObservers, TSSupremeObservers, TSSupremeObserversGroup, TSTasisatItemsStatus, TSTasisatReport, TSVisitGroup, TSVisitReply,
        TSDevelopmentObservers, TSMasonryStuffReport,
        //********************************
        TSSaveDesignerWithOutCondition, TSSaveObserverWithOutCondition, TSReportPlanControler, TSSetShahraksanati, TSSetShirazObsMunicipalityMeterForOtherAgents, TSProjectDeletRequestAllInfo, TSObserverselectDelete, TSImplementerOffice,
        TSChangeProjectRequestType, SaveTSProjectWithOutObserver,
        //********************************
        ExpertGroups = 1700, ExGroup = 1701, ExGroupPeriod, Candidate,

        ControlUsers = 1800, Trace, WFUserTaskTrace, TempPassSetting = 1803,
        Search = 1820, MemberSearch, OfficeSearch, EngOfficeSearch, PrintMeEnvelope, PrintMemberSearch, ExportExcelMemberSearch, PrintOfficeSearch,
        ExportExcelOfficeSearch, PrintEngOfficeSearch, ExportExcelEngOfficeSearch, ReportDocMemberSeparateByMajor,

        Sessions = 1900, Session_SessionLocations, Session_SessionStatus, Session_SessionTypes, Session_Members, Session_SpecificMembers, Session_Events,
        Session_Conditions, Session_EventHolding, Session_EventHoldingLocations, Session_EventTypes, Session_SessionRequests, Session_SessionTypePermissions,
        Session_Agenda, Session_AgendaTypes, Session_MeetingMinutes, Session_MeetingMinuteAttachments, Session_SessionRelations, Session_SessionAttachments,

        Poll = 2000, PollQuestion, PollQuestionShowType, PollChoise, PollAnswer, AgentPoll
    }
    #endregion

    #region General Enum
    public enum UserType
    {
        Member = 1, Office, Daftar, Employee, Teacher, Institute, Settlement, Municipality, TemporaryMembers, TSProjectOwner, TSImplementerOffice
    }

    public enum AttachType
    {
        None = 1,
        Attachments = 2,
        TeacherJob = 3,
        TechnicianLicense = 4,
        IdNo = 5,
        SSN = 6,
        MemberLicense = 7,
        ExpertAnswer = 8,
        SoldierCard = 9,
        BuildingLicense = 10,
        Contract = 11,
        GuaranteeSample = 12,
        ViewerReport = 13,
        Judge = 14,
        MapAlbum = 15,
        Others = 16,
        ResidentDoc = 17,
        SSNBack = 18,
        IdNoP2 = 19,
        IdNoPDes = 20,
        SoldierCardBack = 21,
        NezamKardani = 22
    }

    public enum NewsAttachmentType
    {
        Image = 0,
        Attachment = 1
    }

    public enum Licence
    {
        kardani = 1,
        MoadeleKarshenasi = 2,
        KarshenasiNaPeyvaste = 3,
        Karshenasi = 4,
        MoadeleKarshenasiArshad = 5,
        KarshenasiArshad = 6,
        PHD = 7,
        ArshadPeybaste = 8,
        PHDPeyvaste = 9
    }

    public enum MainMajors
    {
        Architecture = 1,
        Urbanism = 2,
        Civil = 3,
        Mechanic = 4,
        Electronic = 5,
        Mapping = 6,
        Traffic = 7
    }

    public enum EmployeeRequestType
    {
        SaveInfo = 0,
        ChangeRequest = 1,
    }

    public enum OtherPersonType
    {
        OtherPerson = 0,
        Kardan = 1,
        PeriodAttender = 2,
        Memar = 3,
        Owner = 4,
        OwnerLawyer = 5,
        Judge = 6
    }
    #endregion

    #region Accounting
    public enum ParsianPaymentGateway
    {
        Successful = 0,
        OrderIdDuplicated = -112,
        InvalidLoginAccount = -126,
        InvalidCallerIP = -127,
        BatchBillPaymentRequestWasValidForSomeOfItems = -1554
    }
    public enum PaymentGateWay
    {
        IranKish = 0, Parsian = 1
    }
    public enum AccSettingsSData
    {
        MainBank = 1,
        MembershipEarnings,
        MembersCurrentAccount,
        MembersTempCurrentAccount,
        ProfitEarnings,
        ProfitsWageEarnigs,
        ExpertPlace27sEarnings,
        ExpertDemandant,
        MembersCurrentAccountOffice,
        InstituteCurrentAccount,
        TrainingEarnings,
        PersonnelCurrentAccount,
        OtherPersonAccount,
        SMS
    }

    public enum AccountingBankName
    {
        //تجارت- ملت- مسکن- کشاورزی- صادرات- ملی- رفاه- سپه- پارسیان- اقتصاد نوین- توسعه صادرات- شهر
        Tejarat = 1,
        Mellat,
        Maskan,
        Keshvarzi,
        Saderat,
        Melli,
        Refah,
        Sepah,
        Parsian,
        EghtsadNovin,
        TosehSaderat,
        Shahr
    }

    public enum CostSettingsSData
    {
        YearlyMembershipCost = 1,
        MemberFileModifiedCost,
        MemberFileRegistrationCost,
        ExpertPlace27sOnAccount,
        YearlyMembershipCostOffice,
        FirstMembershipCost,
        FirstMembershipCostOffice,
        EngOfficeDocument,
        EngOfficeDocument2,
        OfficeDocument,
        YearlyMembershipCostOfficeImp,
        ImplementDoc
    }

    public enum DocumentStatusType
    {
        Movaghat = 1, Daem = 2, Merg = 4, MovMerg = 5, All = 7, Sabt = 8, PishNevis = 16
    }

    public enum AccountingTT
    {
        Foroosh = 1,
        Dasti = 2,
        Daryaft = 3,
        ChequePass = 4,
        PardakhtBeDigaran = 5,
        FormKhordan = 6,
        BargashtDadeShodan = 7,
        SookhtShodan = 8,
        Fish = 9,
        Eftetahiye = 10,
        Ekhtetamiye = 11,
        EnteghaleSoodOZiyan = 12,
        Purchase = 13,
        Pardakhat = 14,
        CloseBenefit = 15,
        LoanPayment = 16,
        DebtReception = 17,
        MembershipRegistration = 18,
        MembershipConfirmation = 19,
        PeriodRegistration = 20,
        SeminarRegistration = 21,
        Pony = 22,
        ExpertPlace27sRegistration = 23,
        ExpertPony = 24,
        SMSCost = 25
    }

    public enum AccountingChequeStatus
    {
        DarJaryaneVosool = 1,
        Pass = 2,
        PardakhtBeDigaran = 3,
        Form = 4,
        BargashtDade = 5,
        Sookht = 6
    }

    public enum AccountingAccType
    {
        Kol = 1,
        Moin = 2,
        Tafzili = 4,
        OtherTafzilies = 5,
        HoverDetails = 6
    }

    public enum AccountingGroupNature
    {
        Sarmayeh = 1,
        Bedehi = 2,
        Darayi = 3,
        Hazineh = 4,
        Darramad = 5,
        Entezami = 6,
        GheymateTamamShodeh = 7
    }

    public enum AccountingGroupType
    {
        Taraznameh = 1,
        SoodoZiyan = 2,
        Entezami = 3
    }

    public enum AccountingGoodsType
    {
        Kala = 1,
        Dasteh = 2,
        Mahsool = 3,
        Khadamat = 4,
        DarayiMashHood = 5,
        DarayiGheireMashHood = 6
    }

    public enum AccountingAction
    {
        New = 1,
        Edit = 2,
        View = 3,
        Merge = 4,
        SabteDaem = 5,
        Sabt = 6,
        BargashtBeMovaghat = 7,
        Sort = 8,
        Swap = 9,
        NewFromPishNevis = 10,
        Inactive = 11,
        ChangeDocStatus
    }

    public enum AccountingCHType
    {
        Daryaft = 1,
        Pardakht = 2
    }

    public enum AccountingChequeType
    {
        Others = 1,
        Ours = 2
    }

    public enum AccountingCSCType
    {
        Daryaft = 1,
        Pardakht = 2
    }

    public enum AccountingContradictionType
    {
        NotInSystem = 1,
        NotInBank = 2,
        DiffrentAmount = 3
    }

    public class AccountingSetting
    {

        public static bool ConvertDocShift = true;

    }

    public enum AccountingDocBalanceDetailTableType
    {
        None = 0,
        Members = 1
    }

    public enum AccountingAccGroupCategory
    {
        Account = 0,
        HoverDetail = 1
    }

    public enum AccountingJurnalReportCalculatingType
    {
        WithOutGroupBy = 0,
        GroupByDocNo = 1,
        GroupByDate = 2
    }
    #endregion

    #region Automation

    public enum ResignationAcceptanceStatus
    {
        UnConfirmed = 0,
        Pending = 1,
        Confiremd = 2,
        Expired = 3,
        Ended = 4
    }

    public enum AutomationLetterTypes
    {
        Draft = 1,
        Letter = 2,
        EndedLetter = 3
    }

    public enum AutomationLetterCreationType
    {
        In = 1,
        Out = 2,
        Internal = 4,
        MinFormCreatorIndex = 5
    }

    public enum AutomationLetterSenderTypes
    {
        Member = 1,
        Organization = 2,
        OtherPerson = 3,
        Office = 4,
        Employee = 5
    }

    public enum AutomationLetterRecieverTypes
    {
        OtherPerson = 1,
        Organization = 2,
        Member = 3,
        Employee = 4,
        Office = 5
    }

    public enum AutomationUserRightTypes : byte
    {
        CreationTypes = 1,
        Divisions = 2,
        ViewFolders = 3,
        AttachToFolders = 4,
        LetterRelations = 5,
        SetPassword = 6,
        ReferenceAims = 7,
        Secretariat = 8,
        Archive = 9
    }

    public enum AutomationLetterDivision
    {
        Public = 1,
        Private = 2,
        SuperPrivate = 3,
        Protected = 4
    }

    public enum AutomationCartableSettings : short
    {
        NoChanges = 1,
        Delete = 2,
        ChangeCartable = 3

    }

    public enum AutomationLetterReferenceAims
    {
        Action = 1,
        FollowUp = 2,
        Notice = 3,
        Signing = 4
    }


    public enum AutomationLetterStauts
    {
        InProcess = 0,
        NumberingRequest = 1,
        HasNumber = 2
    }

    public enum AutomationLetterTittleType
    {
        Bakhshname = 1,
        SooratJalase = 2,
        Form = 3,
        Oomoomi = 4
    }

    public enum AutomationPriority
    {
        Normal = 1,
        AboveNormal = 2,
        Immediate = 3
    }

    public enum AutomationLetterReferenceRecieverTypes
    {
        Employee = 0,
        Secretariat = 1
    }

    public enum AutomationLetterReferenceSenderType
    {
        Employee = 0,
        Secretariat = 1
    }

    public enum AutomationAutoReferenceExeTime
    {
        ByUser = 1,
        AfterSign = 2,
        AfterAction = 3,
        AfterSave = 4
    }

    public enum AutomationHowToSetDocNo
    {
        ByUser = 1,
        BySecreteriat = 2,
        Automatic = 3
    }


    #endregion

    #region TechnicalServices

    public enum TSReasonCapacityRelease
    {
        Finish = 1,
        WorkshopClosure = 2
    }
    public enum TSTypeAccountingMeRestriction
    {
        DepositToOtherAccount = 1,
        PreventDeposit = 2
    }


    public enum TSEsupWebserviceCallingLogType
    {
        SaveQtaInfo = 1,
        SaveEngInfo = 2,
        SaveJobAgreeMent = 3,
        SaveCompetence = 4,
        SaveQtaInfoQueList = 5,
        SaveEngInfoQueList = 6,
        SaveJobAgreeMentQueList = 7,
        SaveCompetenceQueList = 8
    }
    public enum TSEsupWebserviceCallingLogStatus
    {
        Succes = 1,
        Error = 2
    }
    public enum TSSafarayanehWebServiceCallingRefrenceType
    {
        QueueListForm = 1,
        WorkRequest = 2
    }
    public enum TSAccountingdocumentStatus
    {
        SaveReport = 0, SendReportToAccountingUnit = 1

    }

    public enum TSBalconyType
    {
        Close = 1,
        Open = 2
    }

    public enum TSAttachType
    {
        Croquis = 1,
        PlansMethod = 2,
        Commitment = 3,
        JobLicense = 4,
        ExperimentInformation = 5,
        Contract = 6,
        Timing = 7,
        DevelopmentPercent = 8,
        ObserversDevelopmentPercent = 9,
        Plan = 10,
        PlansEvaluation = 11,
        HighStructureReport = 12,
        LowStructureReport = 13,
        MasonryStuffStructureReport = 14,
        ExpiredLicenseReport = 15,
        TasisatReport1 = 16,
        TasisatReport2 = 17,
        TasisatReport3 = 18,
        ExecutivePerformanceEvaluation = 19,
        SazehObserversPerformanceEvaluation = 20,
        TasisatObserversPerformanceEvaluation = 21,
        ShahrakSanatiObserversPerformanceEvaluation = 22,
        DesignerObserversPerformanceEvaluation = 23,
        ProjectAttachments = 24,
        VisitReplies = 25,
        DesigningContract = 26,
        Insurance = 27,
        PlanBooklet = 28,
        FormNo5 = 29,
        ArchContract = 30,
        CalculationFile = 31,
        StructureContract = 32,
        ElectInstalContract = 33,
        FormNo6 = 34,
        MechanInstalContract = 35

    }

    public enum TSStructureGroups
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4
    }

    public enum TSDirectionType
    {
        Dimension = 1,
        Length = 2,
        Wideness = 3,
        RemainDimension = 4,
        PathWayWidth = 5,
        Limit = 6
    }
    /// <summary>
    ///   Mix=9,//<==اسکلت ترکیبی
    /// </summary>
    public enum TSStructureSkeleton
    {
        Felezy = 1,
        Betony = 2,
        Ajory = 3,
        NimeFelez = 4,
        NimeBeton = 5,
        SandwichPanel = 6,
        Other = 7,
        Mix = 9,//<==اسکلت ترکیبی
        Foladi = 10
        //,Banaee=8
    }

    public enum TSStructureBuiltPlace
    {
        North = 1,
        South = 2
    }

    public enum TSSubmissionType
    {
        New = 1,
        Revival = 2
    }

    public enum TSSObserversType
    {
        Sazeh = 1,
        Tasisat = 2,
        Memar = 3
    }

    public enum TSDesignerType
    {
        Sazeh = 1,
        Memari = 2,
        TasisatBargh = 3,
        TasisatMechanic = 4,
        Shahrsazi = 5
    }

    public enum TSPlansType
    {
        Sazeh = 1,
        TasisatBargh = 2,
        TasisatMechanic = 3,
        Memari = 4,
        Shahrsazi = 5
    }

    public enum TSObserversType
    {
        Sazeh = 1,
        TasisatBargh = 2,
        TasisatMechanic = 3,
        Memari = 4,
        Mapping = 5
    }

    public enum TSMemberType
    {
        Member = 1,
        Office = 2,
        EngOffice = 3,
        OtherPerson = 4,
        ConsultantCompany = 5,
        ExpArchitect = 6
    }

    public enum TSProjectIngridientType
    {
        Nothing = 0,
        Implementer = 1,
        Observer = 2,
        Designer = 3,
        ImplementerAgent = 4,
        Owner = 5
    }

    public enum TSDiscountPercent
    {
        Usual = 1,
        Mehr = 2,
        Khayerin = 3,
        Darmani = 4,
        Industrial = 5,
        FarhangiMazhabi = 6,
        School = 7,
        BonyadMaskan = 8,
        EghdamMeliMaskan = 9
    }

    public enum TSProjectStatus
    {
        SaveProjectInfo = 1,//ثبت اولیه
        LicenseRequest = 2,
        Start = 3,//شروع به کار
        ReStart = 4,//شروع مجدد
        Stoped = 5,
        End = 6,//پایان کار
        InsertedFromOldSystem = 7,
        BuildingNotStarted = 8//عدم شروع به ساخت
    }

    public enum TSProjectRequestType
    {
        InsertProject = 1,
        ChangeObservationRequest = 2,
        ChangeImplementerAgentRequest = 3,
        ChangePlansMethodRequest = 4,
        ChangeImplementerRequest = 5,
        EndProjectCertificateRequest = 6,
        BuildingsLicenseConfirming = 7,
        StatusAnnouncement = 8,
        LicenseRevivalConfirming = 9,
        InvalidationConfirming = 10,
        ValidationConfirming = 11,
        ChangeProject = 12,
        AdditionalStageRequest = 13,
        IncreaseBuildingAreaRequest = 14,
        BuildingNotStarted = 15
    }

    /// <summary>
    /// PlansStatus
    /// </summary>
    public enum TSPlanRequestType
    {
        SavePlanInfoInsideProjectConfirming = 0,
        PlanRevisingRequest = 1,
        ChangePlanAndDesignerBasically = 2
    }

    public enum TSPlansConfirming
    {
        Pending = 0,
        Confirmed = 1,
        NotConfirmed = 2
        // ControlerConfirm = 3
    }

    public enum TSDesignerAcceptance
    {
        Pending = 0,
        Accepted = 1,
        NotAccepted = 2,
        ConfirmedWithoutSaveController = 3
    }

    public enum TSReason
    {
        ShorayeEntezami = 1,
        Reward = 2,
        LackRecovery = 3,
        Repay = 4,
        Others = 5,
        NezamIncrease = 6,
        NezamDecrease = 7,
        FunctionA = 8
    }

    public enum TSEntranceType
    {
        Entrance = 1,
        ParkingEntrance = 2,
        StoreEntrance = 3
    }

    public enum TSAccountingAccType
    {
        ObserversFiche = 1,//***100%        
        _5In1000 = 2,
        _2In1000 = 3,
        Designing5Percent = 4,//***Memari
        Registeration = 5,
        Entrance = 6,
        Registeration_Entrance = 7,
        DocMemberFile = 8,
        EngOfficeDocument = 9,
        EngOfficeDocument2 = 10,
        OfficeDocument = 11,//پروانه شرکت
        PeriodRegister = 12,//ثبت نام دوره
        ObserversFicheFivePercent_Structure = 13,
        Designing5PercentStructure = 14,
        Designing5PercentInstalation = 15,
        ObserversFicheFivePercent_Instalation = 16,
        SeminarRegister = 17,//ثبت نام سمینار
        MemberDebpt = 18
    }

    public enum TSAccountingPaymentType
    {
        Fiche = 1,
        Cheque = 2,
        POS = 3,
        EPayment = 4
    }

    public enum TSAccountingStatus
    {
        SaveInDB = 1,
        Print = 2,
        Payment = 3
    }
    public enum DocOffIncreaseJobCapacityType
    {
        EngOffice = 1,
        Office = 2
    }
    public enum AcceptedUpGrade
    {
        TreetoTwo = 1,
        TwoToOne = 2,
        OneToArshad = 3

    }
    public enum DocOffOfficeMembersQualificationType
    {
        Engineer_Engineer = 1,
        Kardan_Engineer = 2,
        Kardan_Kardan = 3
    }

    public enum DocOffGradeValuesGrdType
    {
        Engineer = 1,
        Technician = 2,
        ExperimentalArchitect = 3
    }

    public enum TSControlerType
    {
        Nezam = 1,
        Municipality = 2
    }

    public enum TSProjectRequestConfirming
    {
        Pending = 0,
        Confirmed = 1,
        NotConfirmed = 2
    }

    public enum TSOwnershipType
    {
        Residential = 2
    }

    public enum TSStructureSystem
    {
        Others = 4
    }

    public enum TSRoofType
    {
        Others = 8
    }

    public enum TSDocumentDetailType
    {
        SaveReport = 0
    }

    public enum TSAccountingDocumentType
    {
        PayObserverShare = 0,
        PayInstallment = 1,
        CorrectList = 2
    }

    public enum TSProjectObserverSelectedReasonType
    {
        MaxRemainCapacity = 1,
        SameObsIdAndProjectGroupId = 2,
        LeastWorkCount = 3,
        LeastObsDateAndMorExpert = 4,
        LeastMembershipDateAndOldestMember = 5,
        ProjectDesigner = 6,
        MinCountRandomSelected = 7,
        RandomSelect = 8
    }
    public enum TSProjectObserverSelectedConfirmType
    {
        Select = 0, SaveInfo = 1, NotAnswered = 2, AcceptWorkByObserver = 3, RejectWorkByObserve = 4, RejectWorkByNezam = 5
    }
    public enum TSObserverWorkRequestStatus
    {
        All = -1, Pending = 0, Confirm = 1, Reject = 2, ShorayeEntezami = 3, AnbohSazan = 4, SherkatAzemayeshgahi = 5, PeymanKar = 6, InActive = 7
    }
    public enum MemberRestrictionTypeForObserverWorkRequest
    {
        AnbohSazan = 1, PeymanKar = 3, BazrasiGaz = 5, ShorayeEntezami = 6, SherkatAzemayeshgahi = 8

    }
    public enum TSObserverWorkRequestChangeType
    {
        All = -1,
        New = 0,
        Change = 1,
        Off = 2,
        ShorayeEntezami = 3,
        MemberDocumentChange = 4,
        AgentChange = 5,
        GasOfficeMembership = 6,
        AnbohSazan = 7,
        SherkatAzemayeshgahi = 8,
        PeymanKar = 9,
        InActive = 10,
        InActiveGasOfficeMembership = 11,
        InActiveObserverRestrictionMembership = 12

    }
    public enum TSUrbanismQualificationType
    {
        AmadeSaziArazi = 1,
        TafkikArazi = 2,
        EntebaghKarbariArazi = 3,
        EntebaghShahriSakhteman = 4,
        SumAmadeSaziAraziAndTafkikAraziAndEntebaghKarbariArazi = 5
    }
    public enum TSObserverSelectLog
    {
        PercentOfCapacityUsageSort = 1049,
        EqualPercentOfCapacityUsageSort = 10491

    }
    public enum TSProjectCapacityDecrementIsFreeType
    {
        NotFree = 0,
        Free = 1,
        FreeOnSpecificDate = 2
    }
    #endregion


    #region ControlAndEvaluation

    public enum TSReportType
    {
        LowStructureReport = 1,
        MasonryStuffReport = 2,
        HighStructureReport = 3,
        ExpiredLicenseReport = 4
    }

    public enum TSIngridientPerformanceType
    {
        ElamNavaghes_Mojri = 1,
        Ekhtariye_Mojri = 2,
        EkhtareShorayeEntezami_Mojri = 3,
        ErsalBeShorayeEntezami = 4,
        ElamVaziyat_Mojri = 5,
        Form1_Mojri = 6,
        Form2_Mojri = 7,
        Form3_Mojri = 8,
        Taghdir_Mojri = 9,
        Taahod_Mojri = 10,
        DavatName_Mojti = 11,
        EkhtareJedi_Mojri = 12,
        EkhtareShorayeEntezami_Nazer = 13,
        Stop = 14,
        Start = 15,
        End = 16
    }
    #endregion

    #region WorkFlow
    public enum WorkFlows
    {
        TeachersConfirming = 1, InstitueConfirming, PeriodConfirming, SeminarConfirming, CancelPeriod,
        DocumentOfMemberConfirming, SMSConfirming, MemberConfirming, MemberResearchActivity, OfficeConfirming,
        ImplementDocumentConfirming, ObservationDocumentConfirming, EngOfficeConfirming,
        TSPlansConfirming, TSProjectConfirming, TSPlanRevisionConfirming, TSChangePlansAndDesigner, TSObserverChangesConfirming = 18,
        TSPlanMethodsChangesConfirming = 19, TSEndStructuralProjectLicenceConfirming = 20,
        EmployeeRequestConfirming, MemberCardRequestConfirming, TechnicianRequestConfirming, OfficeMembershipConfirming,
        MemberTransferConfirming = 25,
        TSChangeImplementerConfirming = 26, TSChangeImplementerAgentConfirming = 27, TSBuildingsLicenseConfirming = 28,
        TSStatusAnnouncementConfirming = 29,
        PeriodRegisterLicenceOutOfTime = 30, DisciplinaryDossierConfirming = 31, DisciplinaryDossierCanceling = 32,
        DisciplinaryDossierRejectingOrder = 33,
        Session = 34, ExpertFileRequest = 35, PrindPeriodCertificates = 36, TSWorkRequestConfirming = 37, TSCapacityRelease = 38, AccountingMeRestriction = 39, TSImplementOfficeConfirming = 40,
        DocDeliveryConfirming=41
    }

    public enum WorkFlowTask
    {
        /// <summary>
        /// Task of Teacher's WorkFlow
        /// </summary>
        TeacherConfirming, SaveTeacherInfo, LearningManagerConfirmingTeacher, ComissionGradingTeacher,
        ComissionConfirmingTeacher, CommitteeGradingTeacher, CommitteeConfirmingTeacher, settlementAgentConfiringTeacher, ConfirmTeacherAndEndProccess, RejectTeacherAndEndProccess,

        /// <summary>
        /// Task of Institue's WorkFlow
        /// </summary>
        SaveInstitueInfo = 30, LearningManagerConfirmingAndSpecifyObserverForInstitue, ObserverConfirmingInstitue, CommitteeConfirmingInstitue, settlementAgentConfiringInstitue, ConfirmInstitueAndEndProccess, RejectInstitueAndEndProccess,

        /// <summary>
        /// Task of Period's WorkFlow
        /// درخواست برگزاری دوره آموزشی و صدور گواهینامه60
        ///بررسی و تایید دوره توسط کارشناس آموزش61
        ///بررسی و تایید دوره توسط  مسئول آموزش62
        ///ثبت نام در دوره آموزشی63
        ///ثبت غیبت ها توسط مجری آموزشی64
        ///ثبت اسامی شرکت کنندگان فقط آزمون65
        ///ثبت صورتجلسه آزمون دوره توسط کارشناس آموزش66
        ///ثبت نمرات و تایید توسط اساتید67
        ///بررسي و تاييد نمرات توسط کارشناس آموزش68
        ///بررسي و تاييد نمرات توسط مسئول آموزش69
        ///بررسي و تاييد نمرات توسط معاون آموزش70
        ///71	چاپ گواهینامه آموزشی
        ///72	چاپ شده و در انتظار تایید نهایی گواهینامه آموزشی
        ///تایید ریاست سازمان و درج امضاء الکترونیک73
        ///تاييد کارشناس اداره کل راه و شهرسازي74
        ///تاييد رئيس اداره توسعه مهندسي و نظارت بر مقررات ملي و کيفيت ساخت75
        ///تاييد معاون شهرسازي و معماري اداره کل راه و شهرسازي76
        ///تاييد مدير کل راه و شهرسازي و درج امضا الکترونيک77
        ///78تایید کلی توسط اداره راه و شهرسازی و درج مهر برجسته برروی گواهینامه ها
        ///عدم تایید و پایان بررسی درخواست برگزاری دوره آموزشی79
        /// </summary>
        SavePeriodInfo = 60, LearningExpertConfirmingPeriod = 61, LearningManagerConfirmingPeriod = 62, //LearningExpertInSettlementConfirming, NezamManagerInSettlementConfirmPeriodBeforRegistration,
        PeriodRegistration = 63, RecordAbsenteeism = 64, PeriodRegistrationJustExam = 65, PeriodSaveExamMinute = 66,
        PeriodSavePointsByTeachers = 67, PeriodConfirmPointsByInstituteAndPrintPoint = 68, PeriodConfirmPointsByLearningExpert = 69, PeriodConfirmPointsByLearningManager = 70, PeriodConfirmPointsByLearningAssistant = 71
            , PeriodConfirmingPrintCertificate = 72, PeriodConfirmingCertificateWaitForConfirming = 73
      , PeriodConfirmingByRiasatSazemanAndSign = 74, PeriodConfirmingBysettlementAgent = 75
        , PeriodConfirmingByNezamEmployeeInMaskan = 76, PeriodConfirmingMoavenShahrsaziVaMemariEdareKoleRahoShahrsazi = 77, PeriodRoadAndurbanismConfirmingAndSign = 78
        , ConfirmPeriodAndEndProccess = 79, RejectPeriodAndEndProccess = 80,

        /// <summary>
        /// Task of Seminar's WorkFlow
        /// </summary>
        SaveSeminarInfo = 80, SeminarLearningManagerConfirming, SeminarExpertCommitteeConfirming, SeminarLearningCommitteeGrading, SeminarSettlementAgentConfirming, ConfirmSeminarAndEndProccess, RejectSeminarAndEndProccess,

        /// <summary>
        /// Task of PeriodInActivate's WorkFlow
        /// </summary>
        PeriodInActivateRequest = 100, LearningEmployeeConfirmingPeriodInActivate, LearnigManagerConfirmingPeriodInActivate, ConfirmingPeriodInActivateAndEndProccess, RejectPeriodInActivateAndEndProcess,

        /// <summary>
        /// Task of Member Job Certificat(Doc Member File)'s WorkFlow
        /// </summary>
        DocumentOfMemberConfirmingSaveInfo = 120, CompleteMemebershipData, DocumentUnitEmployeeConfirmingDocument, ResponsibleOfDocumentUnitEmployeeConfirmingDocument, settlementAgentConfirmingDocument, NezamEmployeeInSettlementConfirmingDocument, ModireMaskanConfirmatingDocument, RoadAndurbanismConfirmingDocument, PrintDocumentByNezamEmployee, PrintAndWaitingForConfirm, DocumentOfMemberConfirmedAndWaitForSendingToNezam, ConfirmDocumentOfMemberAndEndProccess, RejectDocumentOfMemberAndEndProcess,

        /// <summary>
        /// Task of SMS's WorkFlow
        /// </summary>
        SaveSMSInfo = 140, ITManagerConfirmingSMS, ExecutiveManagerConfirmingSMS, ConfirmSMSAndEndProccess, RejectSMSAndEndProcess,

        /// <summary>
        /// Task of Membershipe's WorkFlow
        /// </summary>
        SaveMemberInfoForConfirming = 160, MemberLicenceInquiryAndConfirming, MembershipUnitConfirmingMember, DepartmentalManagerConfirmingMember, ExecutiveManagerConfirmingMemberInActiveTask, TSManagerConfirmingMember, ConfirmMemberAndEndProccess, RejectMemberAndEndProcess,

        /// <summary>
        /// Task of MemberResearchActivity's WorkFlow
        /// </summary>
        SaveMemberResearchAct = 180, CommitteeConfirmingMemberResearchAct, LearningManagerConfirmingMemberResearchAct, ConfirmMemberResearchActAndEndProccess, RejectMemberResearchActAndEndProcess,

        /// <summary>
        /// Task of Office Certificate's WorkFlow
        /// </summary>
        DocumentOfOfficeConfirmingSaveInfo = 200, DocumentUnitEmployeeConfirmingDocumentOff, settlementAgentConfiringDocumentOff, NezamEmployeeInSettlementConfirmingDocumentOff, RoadAndurbanismConfirmingDocumentOff, PrintDocumentByNezamEmployeeDocumentOff, PrintAndWaitingForConfirmDocumentOff, ConfirmDocumentOfOfficeAndEndProccess, RejectDocumentOfOfficeAndEndProcess,

        /// <summary>
        /// Task of Member Implement Certificate's WorkFlow
        /// </summary>
        SaveImplementDocInfo = 220, DocumentUnitEmployeeConfirmingImplementDoc, settlementAgentConfirmingImplementDoc, ImplementDocNezamEmployeeInSettlementConfirmingDocument, ImplementDocRoadAndurbanismConfirmingDocument, ImplementDocPrintDocumentByNezamEmployee, ImplementDocPrintAndWaitingForConfirm, ConfirmImplementDocAndEndProccess, RejectImplementDocAndEndProcess,

        /// <summary>
        /// Task of Member Observation Certificate's WorkFlow
        /// DocumentUnitEmployeeConfirmingObservationDoc :بررسی و تایید درخواست توسط کارشناس واحد خدمات مهندسی
        /// </summary>        
        SaveObservationDocInfo = 240, DocumentUnitEmployeeConfirmingObservationDoc, ExecutiveManagerConfirmingObservationDoc, ConfirmObservationDocAndEndProccess, RejectObservationDocAndEndProcess,

        /// <summary>
        /// Task of EngOffice Certificate's WorkFlow
        /// </summary>
        DocumentOfEngOfficeConfirmingSaveInfo = 260, DocumentUnitEmployeeConfirmingDocumentEngOffice, settlementAgentConfiringDocumentEngOffice, EngOfficeNezamEmployeeInSettlementConfirmingDocument, EngOfficeRoadAndurbanismConfirmingDocument, EngOfficePrintDocumentByNezamEmployee, EngOfficePrintAndWaitingForConfirm, ConfirmDocumentOfEngOfficeAndEndProccess, RejectDocumentOfEngOfficeAndEndProcess,

        /// <summary>
        /// Task of Plan's WorkFlow (Inside of Structural Project Confirming)
        /// </summary>
        //SavePlanInfo = 280, AssignControlerToPlan, ControlerConfirmTaskOfControlingPlan, ControlerConfirmingPlan, ConfirmingPlanAndEndProccess, RejectPlanAndEndProcess,
        SavePlanInfo = 280, TSEmployeeConfirmDesigners, FishPaymentByMember, AssignControlerToPlan, ControlerConfirmingPlan, ConfirmingPlanAndEndProccess, RejectPlanAndEndProcess,

        /// <summary>
        /// Task of Office's WorkFlow
        /// </summary>
        SaveOfficeInfo = 290, MembershipUnitConfirmingOffice, DepartmentalManagerConfirmingOffice, ExecutiveManagerConfirmingOffice, ConfirmOfficeAndEndProccess, RejectOfficeAndEndProcess,

        /// <summary>
        /// Task of structural Project's WorkFlow
        /// </summary>
        SaveProjectBaseInfo = 300, SaveArchitecturalPlanOfProject, SaveStructurePlanOfProject, SaveMechanicInstallationPlanOfProject, SaveElectricalInstalationPlanOfProject, SaveStructureAndInstallationPlanOfProject, SaveObserverOfProject, ProjectObserverAcceptingTask, SaveImplementerOfProject, ControlManagerConfirmingImplementerOfProject, TechnicalServiceManagerConfirmingProject, ConfirmingProjectAndEndProccess, RejectProjectAndEndProcess,

        /// <summary>
        /// Task of Plan Revising's WorkFlow
        /// </summary>
        SavePlanNewRevisionInfo = 320, TechnicalServiceManagerConfirmingPlanRevisionConfirming, ConfirmingPlanRevisionAndEndProccess, RejectPlanRevisionAndEndProcess,

        /// <summary>
        /// Task of Plan Changing basically's WorkFlow
        /// </summary>
        SavePlanAndDesignerInfo = 330, TechnicalServiceManagerFirstConfirmingPlanChanges, AssignControlerToNewPlan, ControlerConfirmTaskOfControlingNewPlan, ControlerConfirmingNewPlan, TechnicalServiceManagerFinalConfirmingPlanChanges, RejectPlanChangingAndEndProcess, ConfirmingPlanChangingAndEndProccess = 337,

        /// <summary>
        ///  Task of Employee's WorkFlow
        /// </summary>
        SaveEmployeeInfo = 345, DepartmentalManagerConfirmingEmployee, ExecutiveManagerConfirmingEmployee, ConfirmingEmployeeChangingAndEndProccess, RejectEmployeeChangingAndEndProcess,

        /// <summary>
        ///  Task of MemberCard's WorkFlow
        /// </summary>
        SaveMemberCardInfo = 360, MembershipUnitClerkConfirmingMemberCard, MembershipUniManagerConfirmingMemberCard, ExecutiveManagerConfirmingMemberCard, ConfirmingMemberCardAndEndProccess, RejectMemberCardAndEndProcess,

        /// <summary>
        /// Task of TechnicianRequest's WorkFlow
        /// </summary>
        SaveTechnicianRequestInfo = 380, MembershipUnitManagerConfirmingTechnicianRequest, ExecutiveManagerConfirmingTechnicianRequest, ConfirmingTechnicianRequestChangingAndEndProccess, RejectTechnicianRequestChangingAndEndProcess,

        /// <summary>
        ///  Task of MemberTransfer's WorkFlow
        /// </summary>
        SaveMemberTransferRequestInfo = 390, MembershipUnitConfirmingMemberTransfer = 391, ControlManagerConfirmingMemberTransfer = 392, TechnicalServiceManagerConfirmingMemberTransfer,
        AccountingManagerConfirmingMemberTransfer, DocumentUnitConfirmingMemberTransfer, EngineeringOfficeAndCompanyUnitConfirmingMemberTransfer,
        DisciplinaryCouncilConfirmingMemberTransfer, LibraryConfirmingMemberTransfer, DepartmentalManagerConfirmingMemberTransfer, ExecutiveManagerConfirmingMemberTransfer,
        ConfirmMemberTransferAndEndProccess, RejectMemberTransferAndEndProcess,

        /// <summary>
        /// Task of BuildingsLicense WorkFlow
        /// </summary>
        SaveBuildingsLicensesInfo = 420, TechnicalServiceConfirmingBuildingsLicense, TechnicalServiceManagerConfirmingBuildingsLicense, ConfirmingBuildingsLicenseAndEndProccess, RejectBuildingsLicenseAndEndProcess,

        /// <summary>
        /// Task of StatusAnnouncement WorkFlow
        /// </summary>
        SaveStatusAnnouncementInfo = 440, ControlAndEvaluationConfirmingStatusAnnouncement, ControlAndEvaluationManagerConfirmingStatusAnnouncement, ConfirmingStatusAnnouncementAndEndProccess, RejectStatusAnnouncementAndEndProcess,

        /// <summary>
        /// Task of SavePeriodRegisterLicenceOutOfTime
        /// </summary>
        SavePeriodRegLicenceReqInfo = 460, LearningUnitManagerConfirmingPeriodRegLicenceReq = 461, ConfirmingPeriodRegLicenceReqAndEndProccess, RejectPeriodRegLicenceReqAndEndProcess,

        /// <summary>
        ///Task Of DisciplinaryDossierConfirming
        /// </summary>
        saveDisciplinaryComplain = 470, DisciplinaryComplainDiscussionAndConfirmation = 471, DisciplinaryComplainAuthoritiesAndSaveReplyComplain = 472, DisciplinaryComplainDefineSessionTimeAndAssignToSession = 473,
        SavingDisciplinaryComplainVote = 474, DisciplinarycouncilManagerConfirmingDisciplinaryComplainVote = 475, OrganizationManagerConfirmingDisciplinaryComplainVote = 476,
        InformOrderAndWaitingRivisionRequest = 477, ConfirmingDisciplinaryComplainAndEndProcess = 478, RejectDisciplinaryComplainAndEndProcess = 479,

        /// <summary>
        ///Task Of DisciplinaryDossierCanceling
        /// </summary>
        SaveDisciplinaryComplainCancel = 490, DisciplinaryCouncilManagerConfirmingCancel = 491, OrganizationManagerConfirmingCancel = 492,
        ConfirmingDisciplinaryComplainCancel = 493, RejectingDisciplinaryComplainCancel = 494,

        /// <summary>
        ///Task Of DisciplinaryDossierRejectingOrder
        /// </summary>
        SaveDisciplinaryComplainRejectorder = 500, DisciplinaryCouncilManagerConfirmingRejectorder = 501, OrganizationManagerConfirmingRejectorder = 502,
        ConfirmingDisciplinaryComplainRejectorder = 503, RejectingDisciplinaryComplainRejectorder = 504,

        /// <summary>
        ///Task Of Session
        /// </summary>
        SaveSessionInfo = 520, ManagerConfirmingSession = 521, ConfirmingSession = 522, RejectingSession = 523,

        /// <summary>
        ///Task Of Change Project Obsarvation Info
        /// </summary>
        SaveChangeProjectObserverRequestInfo = 540, ProjectObserverAcceptingTaskRequest, TechnicalServiceManagerConfirmingProjectRequest, ConfirmingChangeProjectObserverAndEndProccess, RejectChangeProjectObserverAndEndProcess,

        /// <summary>
        ///Task Of Change Project Implementer Info
        /// </summary>
        SaveChangeProjectImplementerRequestInfo = 560, TechnicalServiceManagerCheckingChanges, ControlManagerConfirmingChangeImplementerOfProject, TechnicalServiceManagerConfirmingChangeImplementerOfProject, ConfirmingChangeProjectImplementerAndEndProccess, RejectChangeProjectImplementerAndEndProcess,

        /// <summary>
        ///Task Of ExpertFile(ماده 27) Info      
        /// </summary>
        SaveExpertFileRequest = 600, ConfirmingExpertFileRequestAndEndProccess, RejectExpertFileRequestAndEndProcess,
        /// <summary>
        /// Tasks of PrindPeriodCertificates
        /// </summary>
        PrindPeriodCertificatesSaveRequest = 630, PrindPeriodCertificatesPrintAndAsign = 631
        , PrindPeriodCertificatesRequestConfirmAndEndProccess = 632, PrindPeriodCertificatesRequestRejectAndEndProccess = 633,

        /// <summary>
        /// Task of TSWorkRequest's WorkFlow
        /// </summary>        
        SaveTSWorkRequestConfirminInfo = 700, TSUnitEmployeeConfirminConfirmingTSWorkRequest, TSManagerConfirmingTSWorkRequestConfirmin, BonyadMaskanConfirmingTSWorkRequestConfirmin, ShahrdaryConfirmingTSWorkRequestConfirmin, ConfirmTSWorkRequestConfirminAndEndProccess, RejectTSWorkRequestConfirminAndEndProcess,
        /// <summary>
        /// Task of CapacityRelease's WorkFlow آزاد سازی
        /// </summary>
        SaveCapacityReleaseRequestInfo = 720, ControlAgentEmployeeConfirminCapacityReleaseRequest, AgentManagementConfirminCapacityReleaseRequest, ControlEmployeeConfirmingCapacityReleaseRequest, ControlManagerConfirmingCapacityReleaseRequest, TSUnitEmployeeConfirminCapacityReleaseRequest, TSManagerConfirmingCapacityReleaseRequest, TSVicarConfirmingCapacityReleaseRequest, ConfirmCapacityReleaseConfirminAndEndProccess, RejectCapacityReleaseConfirminAndEndProcess,
        /// <summary>
        /// Task of AccountingMeRestriction's WorkFlow محدودیت های تراکنش های مالی اعضا
        /// </summary>
        SaveAccountingMeRestrictionRequestInfo = 740, TSUnitEmployeeConfirminAccountingMeRestrictionRequest, TSManagerConfirmingAccountingMeRestrictionRequest, ConfirmAccountingMeRestrictionConfirminAndEndProccess, RejectAccountingMeRestrictionConfirminAndEndProcess,
        /// <summary>
        /// Task of AccountingMeRestriction's WorkFlow محدودیت های تراکنش های مالی اعضا
        /// </summary>
        SaveImplementOffice = 770, TSUnitEmployeeConfirmingImplementOfficeRequest, ConfirmingImplementOfficeRequestAndEndProccess, RejectConfirmingImplementOfficeRequestAndEndProccess=773,
        /// <summary>
        /// Task of AccountingMeRestriction's WorkFlow تحویل پروانه اشتغال
        /// </summary>
         SaveDocDeliveryRequest=790,DocDeliverToAgent,DocDeliverToMember, ConfirmingDocDeliveryRequestAndEndProccess, RejectDocDeliveryRequestAndEndProccess,
    }

    public enum TSAccountingMeRestrictionTypeMeId
    {
        Member = 1,
    }
    public enum TSCapacityReleaseMeIdType
    {
        Member = 1,
    }
    public enum WorkFlowTaskType
    {
        StartingTask = 0,
        MeddleTask = 1,
        ConfirmingAndEndProccessTask = 2,
        RejectingingAndEndProccessTask = 3,
    }

    public enum WorkFlowStateNmcIdType
    {
        NmcId = 0,
        MeId = 1,
        OfficId = 2,
        Munipulicity = 3,
        System = 4,
        TempMember = 5,
        Teacher = 6,
        TSProjectOwner = 7,
        TSImplementerOffice = 8
    }

    public enum WorkFlowStateType
    {
        SendDocToNextStep = 0,
        ViewInfo = 1,
        UpdateInfo = 2,
        InActiveInfo = 3,
        InsertNewRow = 4
    }

    public enum WorkFlowRequestConfirmStatus
    {
        Pending = 0,
        Confirm = 1,
        Reject = 2,
        Cancel = 3
    }
    #endregion

    #region Documents
    public enum DocumentTypesOfMember
    {
        DocMemberFile = 0,
        DocMemberFileImp = 1,
        DocMemberFileObs = 2,
    }
    public enum DocumentGrads
    {
        Arshad = 1,
        Grade1 = 2,
        Grade2 = 3,
        Grade3 = 4
    }

    public enum DocumentResponsibilityType
    {
        Observation = 1,
        Design = 2,
        Implement = 3,
        Mapping = 6,
        Traffic = 7,
        Urbanism = 8,
        Gas = 9
    }

    public enum DocumentOfMemberRequestType
    {
        New = 0,
        Revival = 1,
        Change = 2,
        UpGrade = 3,
        InActive = 4,
        Transfer = 5,//==>انتقالی-صدور
        Qualification = 6,
        ReDuplicate = 7,
        OldDocument = 8,//==>پروانه های صادر شده در سیستم قدیم
        OldDocumentRenew = 9,//==>پروانه های تمدید/ارتقاء داده شده در سیستم قدیم
        TransferAndRevival = 10,//==>انتقالی-تمدید
        Reissues = 11,//==>صدور مجدد
        TransferedMemberRequest = 12//==>تغییرات جهت صدور شماره پروانه جدید افراد انتقالی
    }

    /// <summary>
    /// ترتیب قرار گرفتن کد رشته افراد در شماره پروانه دفتر.به ترتیب از چپ به راست
    /// </summary>
    public enum DocumentOfficeMeMajor
    {
        Traffic = 0,
        Mapping = 1,
        Urbanism = 2,
        Electronic = 3,
        Mechanic = 4,
        Civil = 5,
        Architecture = 6,
    }

    public enum DocumentOfficeResponsibilityType
    {
        ObservationAndDesign = 1,
        Implement = 2,
        ObservationAndDesignAndImplement = 3,
    }

    public enum DocOffOfficeFactorDocumentsType
    {
        FinancialStatus = 1,
        JobQualification = 2
    }

    public enum DocumentSetExpireDateType
    {
        Permanent = 0,
        Temporary = 1
    }

    public enum DocumentMemberFileAcceptType
    {
        GradeJumping = 9
    }

    public enum DocumentRequesterType
    {
        Employee = 0,
        Member = 1
    }

    public enum DocumentJobConfirmType
    {
        Office = 0,
        TwoMembers = 1,
        GovCom = 2,
        TwoMembersOtherPrv = 3
    }

    public enum DocTestType
    {
        Observation = 6,
        Implement = 9
    }


    public enum GasOfficeMemberStatus
    {
        Confirmed = 1,
        InActive = 2,
        Off = 3 //مرخصی
    }

    public enum TSWorkRequestWantedWorkType
    {
        Obsever = 0,
        Design = 1,
        ObseverAndDesign = 2
    }

    public enum CityCode
    {
        KhanZenyan = 162,
        Kharameh = 166,
        Dareyon = 187,
        Zarghan = 255,
        Sarvestan = 269,
        Shiraz = 317,
        Lapooy = 438,
        Sepidan = 518,
        Beyza = 524,
        SoltanShahr = 536,
        Sadra = 541

    }
    #endregion

    #region Members
    public enum MemberCardsType
    {
        NewCard = 0,
        WasDestroyed = 1,
        TechnicalProblem = 2,
        MemberInfoChange = 3,
        WasLost = 4

    }
    public enum MembershipRegistrationStatus
    {
        Confirmed = 1,
        Pending = 2,
        NotConfirmed = 3,
        Dead = 4,
        TransferToOtherProvince = 5,
        ReturnToCurrentProvince = 6,
        Fired = 7,
        Cancel = 8,
        Temp = 9,
        FakeLicense = 10,
        DocumentCancel = 11,
        ConditionalApproval = 12,
        TransferFromOtherProvince = 13,
        CancelDebtorMember = 14
    }
    /// <summary>
    /// IsCreated in tblMemberRequest
    /// </summary>
    public enum MemberRequestType
    {
        Request = 0,
        Create = 1,
        AgentChange = 2,
        ChangeBaseInfo = 3,
        ChangeLicence = 4,
        BankAccNoChange = 5,
        Dead = 6,
        TransferToOtherProvince = 7,
        ReturnToCurrentProvince = 8,
        Fired = 9,
        FakeLicense = 10,
        Cancel = 11,
        CancelDebtorMember = 12,
        ActivateDebtorMember = 13
    }
    public enum NoaForMarkazi
    {
        select = 1,
        insert = 2,
        update = 3,
        delete = 4
    }

    public enum MemberConfirmType
    {
        Pending = 0,
        Confirmed = 1,
        NotConfirmed = 2,
        Cancel = 3,
    }

    public enum LockMemberType
    {
        Member = 0,
        Office = 1,
        EngOffice = 2,
        Kardan = 3,
        Teacher = 4,
        Institue = 5,
        Memar = 6
    }

    public enum MembershipRequest
    {
        Member = 0,
        Employee = 1
    }

    public enum TemporaryMemberStatus
    {
        Pending = 0,
        HasMeId = 1,
        Canceled = 2
    }

    public enum TransferMemberType
    {
        AllTypes = -1,
        TransferFromOtherProvince = 1,
        GoToOtherProvince = 2,
        ReturnToCurrentProvince = 3
    }

    public enum MemberPrivateInfoSettingType
    {
        UnSelected = 0,
        MobileAndAddress = 1,
        OnlyMobile = 2,
        OnlyHomeAddress = 3,
        NonOfTheme = 4
    }

    public enum RecieveMagazineType
    {
        UnSelected = 0,
        Yes = 1,
        No = 2
    }
    #endregion

    public enum OfficeMemberKind
    {
        Office = 0, EngOffice = 1
    }

    #region  EngOffice
    public enum EngOfficeType
    {
        Design = 1,
        Implimentation = 2,
        StructureDesign = 3
    }

    public enum EngOffFileType
    {
        SaveFileDocument = 0,
        Revival = 1,
        Change = 2,
        Reduplicate = 3,
        Invalid = 4,
        OldDocument = 5,
        ChangeBaseInfo = 6,
        Activate = 7,
        ConditionalAprrove = 8

    }

    public enum EngOfficeConfirmationType
    {
        Pending = 0,
        Confirmed = 1,
        NotConfirmed = 2,
        Cancel = 3,
        ConditionalApprove = 4
    }
    #endregion

    #region Office
    public enum OfficeMemberType
    {
        Member = 1,
        Kardan = 2,
        Otherperson = 3,
        Memar = 4,
        NewKardan = 5

    }

    public enum OfficePosition
    {
        Manager = 1,
        Employed = 2,
        EngOfficeManager = 4,//مدیر مسئول
        Board = 5,//عضو هیئت مدیره
        ShareHolder = 6,//سهامدار
        EngOfficeEmployed = 7,//عضو دفتر
        ManagerAndBoard = 8,//مدیر عامل و عضو هیئت مدیره    
        ViceChairmanOfTheBoard = 9,//نائب رئیس هیئت مدیره   
        ChairmanOfTheBoard = 10//رئیس هیئت مدیره
    }

    public enum OfficeRequestType
    {
        SaveRequestInfo = 0,
        SaveFileDocument = 1,
        Revival = 2,
        Change = 3,
        Invalid = 4,
        Reduplicate = 5,
        SaveMembershipRequset = 6,
        DocumentInvalid = 7,
        ChangeBaseInfo = 8,
        ChangeShareHolderAndBaseInfo = 9
    }

    public enum OfficeDocumentStatus
    {
        DoNotHaveDocument = 0,
        Pending = 1,
        Confirmed = 2,
        NotConfirmed = 3,
        DocumentCancel = 4
    }

    public enum OfficeType
    {
        PublicJointStock = 1,
        PrivateJointStock = 2,
        LimitedJointStock = 3
    }
    #endregion

    #region Expert27
    public enum ExpertRequestStatus
    {
        Pending = 0,
        Confirmed = 1,
        NotConfirmed = 2
    }
    public enum ExpertStatus
    {
        Inserting = 1,
        SendToExpert = 2,
        ExpertConfirmed = 3,
        ExpertNotConfirmed = 5,
        ExpertAnswered = 6,
        ReturnFromExpert = 7,
        ExpertApprove = 9,
        ExpertPlaceApprove = 10,
        LastSend = 12

    }
    public enum ExpertFileType
    {
        New = 0,
        Revivial = 1,
        Change = 2
    }
    #endregion

    #region Print
    public enum AddressType
    {
        HomeAddress = 0,
        WorkAddress = 1,
        Address = 2
    }

    public enum PrintType
    {
        PeriodPrinting = 1,
        SeminarPrinting = 2,
        DocMemberFilePrinting = 3,
        OfficeDocumentPrinting = 4,
        ImplementerDocPrinting = 5,
        EngOfficeDocPrinting = 6,
        InqueryMemberLicence = 7,
        MemberCartRequestPrinting = 8,
        MemberTempreryCartPrinting = 9,
        OfficeTempreryCardPrinting = 10,
        TaxConfirmLetter = 11,
        TsDesignerconfirmationLetter = 12,
        TsObserverconfirmationLetter = 13,
        PrePrintPeriodPrinting = 14
    }
    #endregion

    #region Technician
    public enum TechnicianRequestType
    {
        SaveInfo = 0,
        ChangeRequest = 1,
    }
    #endregion

    #region Entezami
    public enum ShakiTypeMember
    {
        Member = 0,
        Municipality = 1,
        Dolati = 2,
        HeyatModire = 3,
        Others = 4,
        Motefareghe = 5
    }
    public enum MoteshakiTypeMember
    {
        Member = 0,
        Office = 1,
        Kardan = 2,
        Memar = 3

    }
    public enum MoteshakiType
    {
        ExecutiveGaz = 0,
        SupervisorGaz = 1,
        Person = 2,
    }
    public enum ComplainStatus
    {
        CreateComplain = 1, Pending, ReplyWaiting, InvitationWaiting, SessionWaiting, SessionHolding, ResultWaiting,
        OrderWaiting, InformStateOrder, RevisionWaiting, InformFinalOrder, Cancel, Rejected, Closed, RejectedOrder
    };
    public enum ComplainRequestType
    {
        SaveComplain = 0,
        Cancel = 1,
        RejectOrder = 2,
        Rejected = 3
    }
    #endregion


    #region SMS
    public enum SMSType
    {
        AnswerOfRecieved = 1,
    }

    public enum SMSWebServiceType
    {
        AsreFaraErtebat = 0,
        Magfa = 1,
        Prdco = 2,
        PrdcoAsync = 3

    }
    #endregion

    #region Poll
    public enum PollChoisePrivateInfoSetting
    {
        AddressAndTell = 1,
        Tell = 2,
        Address = 3,
        None = 4
    }

    public enum PollChoiseRecieveMagazine
    {
        Yes = 5,
        No = 6
    }
    #endregion

    #region ProjectJob History
    public enum ProjectReasonOfExpress
    {
        Membership = 1,
        DocMemberFile = 2,
        Biography = 6,
        TechnicalService = 7

    }

    public enum ProjectJobHistoryType
    {
        Structure = 1,
        RoadConstruction = 2,
        Petrocehmical = 3,
        Powerhouse = 4
    }

    public enum ProjectJobPosition
    {
        Implementer = 8,
        ImplementerAgent = 9,
        Observer = 10,
        Designer = 12
    }

    public enum ProjectCorporationType
    {
        Contract = 1,
        CorporateInOffice = 2,
        MembershipOfOfficeOrEngoffice = 4

    }
    #endregion

    public enum ResetPasswordType
    {
        TsProjectOwner = 0,
        Member = 1,
        Office = 2,
        Teacher = 3,
        Institue = 4,
        Employee = 5,
        MunEmployee = 6,
        Settlement = 7,
        TempMember = 8
    }

    #region Amoozesh
    public enum PeriodPresentStatus
    {
        Inserting = 0,//**
        PeriodRegister = 1,//**
        CompleteCapacity = 2,
        InvalidPeriod = 3,
        LockPeriod = 4,
        StartPeriod = 5,
        EndPreiod = 6,
        StartTest = 7,//**
        AnnounceResultAndObjection = 8,//**
        EndObjection = 9,
        ConfirmedPeriod = 10//**

    }

    public enum PeriodRegisterType
    {
        PeriodAndExam = 0,
        OnlyExam = 1,
        OutOfTime = 2,
        OnlyPeriod = 3
    }

    public enum SeminarStatus
    {
        Inserting = 0,
        Registeration = 1,
        CompleteCapacity = 2,
        Canceling = 3,
        LockSeminar = 4,
        StartSeminar = 5,
        EndSeminar = 6
    }

    public enum PeriodPresentRequestType
    {
        SaveInfo = 0,
        Change = 1,
        PrintRequest = 2
    }

    public enum SeminarRequestType
    {
        SaveInfo = 0,
        Change = 1
    }
    public enum PeriodRegisterPaymentType
    {
        Monye = 0,
        Fish = 1,
        Card = 2,
        EPayment = 3

    }
    #endregion

    #region Commision
    public enum CandidateStatus
    {
        Original = 1,
        Alternate = 2,
        Cancel = 3,
        FirstCancel = 4,
        Other = 5
    }

    public enum CandidatePosition
    {
        Manager = 1,
        Interface = 2,
        other = 3
    }

    public enum ExGroupType
    {
        Entezami = 1,
        Candid = -1
    }
    #endregion

    #region Payment
    public enum AccountingPaymentType
    {
        Fiche = 1,
        Cheque = 2,
        POS = 3,
        EPayment = 4
    }

    public enum AccountingPaymentResultCodeType
    {
        BankResponseResultCode = 0,
        BankConfimTransactionResultCode = 1
    }

    public enum EpaymentType
    {
        WizardMemberRegistration = 1,
        WizardNewMemberDoc = 2,
        MemberPayment = 3,
        MemberMultiplePayment = 4,
        EpaymentForAllSite = 5,
        ParsianGetWay = 6
    }
    #endregion

    public enum MemberInfoUserControRequester
    {
        Member = 0,
        DocMemberFile = 1
    }
    #endregion

    public class Permission
    {
        public bool CanNew;
        public bool CanEdit;
        public bool CanView;
        public bool CanDelete;
    }

    public abstract class BaseObject : System.ComponentModel.Component
    {

        #region ProtectedMember
        protected System.Data.DataTable _dataTable;
        protected static TableType _tableId = TableType.None;
        #endregion

        #region PrivatesMember
        private System.Data.SqlClient.SqlDataAdapter _adapter;

        private System.Data.SqlClient.SqlConnection _connection;
        private System.Data.SqlClient.SqlTransaction _transaction;

        private System.Data.DataSet _copyDataSet;
        private System.Data.DataSet _dataset;
        private bool _clearBeforeFill;
        #endregion

        #region Constructors

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public BaseObject()
        {
            _clearBeforeFill = true;
            Adapter.AcceptChangesDuringUpdate = false;
            InitConnection();
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public BaseObject(System.Data.DataSet ds)
            : this()
        {
            this._dataset = ds;
        }
        #endregion

        #region Properties
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected System.Data.SqlClient.SqlDataAdapter Adapter
        {
            get
            {
                if ((this._adapter == null))
                {
                    this._adapter = new System.Data.SqlClient.SqlDataAdapter();

                }
                return this._adapter;
            }
        }
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Data.DataSet DataSet
        {
            get
            {
                if (this._dataset == null)
                {
                    this._dataset = new System.Data.DataSet();

                }
                return this._dataset;
            }
        }
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal System.Data.SqlClient.SqlConnection Connection
        {
            get
            {
                if ((this._connection == null))
                {
                    this.InitConnection();
                }
                return this._connection;
            }
            set
            {
                this._connection = value;
                if ((this.Adapter.InsertCommand != null))
                {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if ((this.Adapter.DeleteCommand != null))
                {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if ((this.Adapter.UpdateCommand != null))
                {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                if ((this.Adapter.SelectCommand != null))
                {
                    this.Adapter.SelectCommand.Connection = value;
                }
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal System.Data.SqlClient.SqlTransaction Transaction
        {

            get
            {
                return this._transaction;
            }
            set
            {
                this._transaction = value;
                if ((this.Adapter.InsertCommand != null))
                {
                    this.Adapter.InsertCommand.Transaction = value;
                }
                if ((this.Adapter.DeleteCommand != null))
                {
                    this.Adapter.DeleteCommand.Transaction = value;
                }
                if ((this.Adapter.UpdateCommand != null))
                {
                    this.Adapter.UpdateCommand.Transaction = value;
                }
                if ((this.Adapter.SelectCommand != null))
                {
                    this.Adapter.SelectCommand.Transaction = value;
                }
            }
        }



        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public bool ClearBeforeFill
        {
            get
            {
                return this._clearBeforeFill;
            }
            set
            {
                this._clearBeforeFill = value;
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual bool HasChanged
        {
            get
            {
                return DataSet.HasChanges();
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public bool EnforceConstraints
        {
            get { return DataSet.EnforceConstraints; }
            set { DataSet.EnforceConstraints = value; }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual string CurrentFilter
        {
            get { return DataSet.Tables[0].DefaultView.RowFilter; }
            set { this.DataSet.Tables[0].DefaultView.RowFilter = value; }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual System.Data.DataRow this[int i]
        {
            get { return this.DataSet.Tables[0].DefaultView[i].Row; }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual System.Data.DataRow this[string tableName, int i]
        {
            get { return this.DataSet.Tables[tableName].DefaultView[i].Row; }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual System.Data.DataRow this[int tableIndex, int i]
        {
            get { return this.DataSet.Tables[tableIndex].DefaultView[i].Row; }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual int Count
        {
            get
            {
                // this.DataSet.Tables[0].DefaultView.RowStateFilter = DataViewRowState.Added | DataViewRowState.Unchanged | DataViewRowState.ModifiedCurrent;
                return this.DataSet.Tables[0].DefaultView.Count;
            }
        }

        public TableType TableId
        {
            get
            {
                // if (_tableId == TableType.None)
                //      throw new ArgumentNullException("TableID is nul");
                return _tableId;
            }
        }
        #endregion

        #region Methods

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void ResetAllParameters()
        {
            for (int i = 0; i < Adapter.SelectCommand.Parameters.Count; i++)
                Adapter.SelectCommand.Parameters[i].Value = null;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void AcceptChanges()
        {
            DataTable.AcceptChanges();
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void ResetChanges()
        {
            if (_copyDataSet != null)
            {
                DataSet.Clear();
                DataSet.Merge(_copyDataSet);
                _copyDataSet.Dispose();
                _copyDataSet = null;
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual void RejectChanges()
        {
            DataSet.RejectChanges();

            for (int i = 0; i < DataSet.Tables.Count; i++)
            {
                DataSet.Tables[i].RejectChanges();
                System.Data.DataRow[] dr = DataSet.Tables[i].GetErrors();
                if (dr != null && dr.Length > 0)
                    for (int j = 0; j < dr.Length; j++)
                    {
                        dr[j].RejectChanges();
                        dr[j].ClearErrors();
                        dr[j].EndEdit();
                    }
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual void SetFilter(string TableName, string Filter)
        {
            DataSet.Tables[TableName].DefaultView.RowFilter = Filter;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual void SetFilter(int TableIndex, string Filter)
        {
            DataSet.Tables[TableIndex].DefaultView.RowFilter = Filter;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual string GetFilter(string TableName)
        {
            return DataSet.Tables[TableName].DefaultView.RowFilter;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual string GetFilter(int TableIndex)
        {
            return DataSet.Tables[TableIndex].DefaultView.RowFilter;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual int GetCount(string tableName)
        {
            return this.DataSet.Tables[tableName].DefaultView.Count;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual int GetCount(int tableIndex)
        {
            return this.DataSet.Tables[tableIndex].DefaultView.Count;
        }


        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual int RealCount
        {
            get { return this.DataSet.Tables[0].Rows.Count; }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual int GetRealCount(string tableName)
        {
            return this.DataSet.Tables[tableName].Rows.Count;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public virtual int GetRealCount(int tableIndex)
        {
            return this.DataSet.Tables[tableIndex].Rows.Count;
        }


        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void SetDefColValue(string colName, object value)
        {
            DataSet.Tables[0].Columns[colName].DefaultValue = value;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void SetDefColValue(int colIndex, object value)
        {
            DataSet.Tables[0].Columns[colIndex].DefaultValue = value;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void SetDefColValue(string TableName, string colName, object value)
        {
            DataSet.Tables[TableName].Columns[colName].DefaultValue = value;

        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void SetDefColValue(int TableIndex, string colName, object value)
        {
            DataSet.Tables[TableIndex].Columns[colName].DefaultValue = value;

        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitConnection()
        {
            this._connection = new System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = DBManager.CnnStr;
            //this._connection.ConnectionTimeout = DBManager.DbConnectTimeout;
            this.InitAdapter();
            if (this._adapter.SelectCommand != null)
                this._adapter.SelectCommand.CommandTimeout = DBManager.DbConnectTimeout;
            if (this._adapter.InsertCommand != null)
                this._adapter.InsertCommand.CommandTimeout = DBManager.DbConnectTimeout;
            if (this._adapter.UpdateCommand != null)
                this._adapter.UpdateCommand.CommandTimeout = DBManager.DbConnectTimeout;
            if (this._adapter.DeleteCommand != null)
                this._adapter.DeleteCommand.CommandTimeout = DBManager.DbConnectTimeout;
            this.DataTable.AcceptChanges();
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public System.Data.DataRow NewRow()
        {
            return DataTable.NewRow();
        }


        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void AddRow(System.Data.DataRow dr)
        {
            DataTable.Rows.Add(dr);
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void SetRowsModified()
        {
            for (int i = 0; i < this.Count; i++)
                if (this[i].RowState == System.Data.DataRowState.Unchanged)
                    this[i].SetModified();
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill()
        {
            if ((this.ClearBeforeFill == true))
            {
                DataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(DataTable);
            return returnValue;
        }

        // [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual System.Data.DataTable GetData()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            this.Adapter.Fill(dt);
            return dt;
        }


        [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public int Save()
        {
            if (Transaction != null) _copyDataSet = DataSet.Copy();
            int ret = Adapter.Update(this.DataTable);
            if (this.Transaction == null)
            {
                DataTable.AcceptChanges();
            }
            return ret;
        }
        #endregion

        #region Abstracts
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        abstract protected void InitAdapter();

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public abstract System.Data.DataTable DataTable
        {
            get;
        }        
        #endregion

        internal static Permission GetUserPermission(int UserId, UserType ut, TableType tt)
        {
            Permission per = new Permission();
            per.CanEdit = per.CanNew = per.CanView = per.CanDelete = true;
            if (UserId == 999999)
            {
                per.CanDelete = per.CanNew = per.CanEdit = false;
                return per;
            }
            if (ut == UserType.Employee)
            {
                LoginManager LoginManager = new DataManager.LoginManager();
                LoginManager.FindByCode(UserId);
                if (LoginManager.Count != 1)
                {
                    per.CanDelete = per.CanNew = per.CanEdit = false;
                    return per;
                }
                Boolean IsAdmin = false;
                if (!Utility.IsDBNullOrNullValue(LoginManager[0]["IsAdmin"]) && Convert.ToBoolean(LoginManager[0]["IsAdmin"]))
                    IsAdmin = true;
                if (UserId == 1 || IsAdmin)
                {
                    per.CanDelete = per.CanNew = per.CanEdit = true;
                    return per;
                }

                UserRightManager userright = new UserRightManager();
                userright.FindByLoginIdAndTtCode(UserId, ((int)tt).ToString());
                userright.CurrentFilter = "TtCode='" + ((int)tt).ToString() + "'";
                if (userright.Count == 1)
                {
                    per.CanDelete = (bool)userright[0]["CanDelete"];
                    per.CanView = (bool)userright[0]["CanView"];
                    per.CanNew = (bool)userright[0]["CanNew"];
                    per.CanEdit = (bool)userright[0]["CanEdit"];

                }
                else
                {
                    per.CanEdit = per.CanNew = per.CanView = per.CanDelete = false;
                }

            }
            else
            {
                per.CanDelete = per.CanNew = per.CanEdit = false;
                return per;
            }

            return per;
        }

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            Permission per = new Permission();
            per.CanEdit = false;
            per.CanNew = false;
            per.CanView = false;
            per.CanDelete = false;

            return per;
        }
    }
}
