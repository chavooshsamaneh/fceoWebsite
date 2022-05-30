using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace TSP.DataManager
{
    #region Public Parameters

    /// <summary>
    /// پیغام های خطایی که مربوط به درخواست های مختلف است
    /// Return Related Message in "FindRequestErrorMsg" function
    /// </summary>
    public enum ErrorRequest
    {
        LoseRequestInfo = -1,

        /// <summary>
        /// Errors type of MembersRequest
        /// </summary>
        RequestIsConfirmed = -2, MemberAcountNotBalance = -3, MemberInfoWasChanged = -4, NoActiveRequestFind = -5,

        /// <summary>
        /// Errors type of ProjectRequest Part1
        /// </summary>
        ArchitecturePlanNotConfirm = -7, StructuralPlanNotConfirm = -8, ElectricalInsPlanNotConfirm = -9, MechanicInsPlanNotConfirm = -10,
        NoMainRegisteredNo = -11, BlockNumIsNotMatch = -12, FoundationIsNotMatch = -13, NoImplementer = -14, NoImplementerAgent = -15,

        /// <summary>
        /// Errors type of SMSRequest
        /// </summary>
        SMSWasInActiveBySender = -16,


        /// <summary>
        /// Errors type of Office Request Part1
        /// </summary>
        NoMemberDefinedForOffice = -18, OfficManagerHasNotDesignResponsiblity = -19, OfficManagerHasNotObservationResponsiblity = -20,
        OfficeManagerHasNotJobCertificate = -21,
        OfficManagerHasNotImplementResponsiblity = -22, NoOneHasSuitableResponsiblityForOfficeRequest = -23,
        NonOfTheManagerHasSuitableResponsiblityForOfficeRequest = -24, AtLeastTwoMemberOfOfficeShouldHaveSuitableRes = -25,

        /// <summary>
        /// Errors type of Office Request Part2
        /// </summary>
        NoOfficeManager = -26, AtLeastTwoMemberOfOfficeShouldbeInBoard = -27, MFTypeIsNotRecognized = -28, AllOfficeMembersNotAcceptCompany = -29,
        SerialNoAndExpDateNotFilled = -30, OtherPersonDoesntAllowToBeManager = -31, AtLeastTwoMemberOfImpOfficeShouldHaveMemberFile = -32,
        AtLeastOneMemberOfDesOfficeShouldHaveMemberFile = -33, DesOfficeManagerShouldHaveMemberFile = -34,

        /// <summary>
        /// Errors type of DocMemberFile Request
        /// </summary>
        ExamInfoDoesNotSave = -35,

        //*****
        YouCanNotSentDocToNextStep = -36,

        /// <summary>
        /// Errors type of ProjectRequest Part2
        /// </summary>
        No5In1000Fiche = -37, AmountOf5In1000FicheNotMatch = -38, NoObserversFiche = -39, AmountOfObserversFicheNotMatch = -40,

        NotEnoughCapacityForObservers = -41, No2In1000Fiche = -42, AmountOf2In1000FicheNotMatch = -43, NoObserver = -44,
        NotEnoughCapacityForImplementer = -45, NoObserversMother = -46, NoOwnerAgent = -47, NoBlock = -48, NotMatchStepsWithGroup = -49,
        NotPay2In1000Fish = -50, NotPayObserverFish = -51, NoBuildingsLicense = -52, NoPay5In1000Fish = -53,
        /// <summary>
        /// Errors type of PlanInfo
        /// <summary>
        NoDesigner = -55, NoPlansMasterDesigner = -56, NotEnoughCapacityForDesigners = -57,
        No5PercentFiche = -58, AmountOf5PercentFicheNotMatch = -59, NoController = -60, NotEnoughCapacityForMemberDesigners = -61,
        JustEndingTaskForConfirmedPlan = -62, NoControllerConfirmation = -63, NoPay5PercentFiche = -64, NotAllDesignerInsertedForPlan = -65,

        //Session
        SessionNoPermissionForType = -70,

        /// <summary>
        /// Errors type of EngOffice Request
        /// </summary>
        EngOffManagerNotSave = -90, EngOffMFTypeIsNotRecognized = -91, AllEngOfficeMembersNotAcceptCompany = -92, EngOffSerialNoAndExpDateNotFilled = -93, OneOfMemberIsInAnotherOffice = -94,


        /// <summary>
        /// Errors type of Office Request Part3
        /// </summary> 
        OfficeMemberJobCertificateHasExpired = -200, OfficeManagerCantBeOtherPerson = -201,

        /// <summary>
        /// Errors type of DocMemberFile
        /// </summary>
        DocumentMajorIsNotDefined = -220,

        Error = -1000,

        /// <summary>
        /// Errors type of Office Request Part3
        /// </summary>
        AtLeastTwoMemberTypeOfOfficeShouldbeMember = -1001,
        MemberNumberOfLimitedJointStockShouldBeGreaterThanTwo = -1002
            , MemberNumberOfPrivateAndPublicJointStockShouldBeGreaterThanThree = -1003,
        AtLeastTwoMemberOfObserverDesignOfficeShouldHaveMemberFile = -1004
            ,
        YouAreNotTheRequestCreator = -1005
            , SMSExpired = -1006
        , PeriodRegisterMarksAreNotInserted = -1007, ErrorInUpdatingDesignerWorkCount = -1008
    }

    /// <summary>
    /// پیغام های خطا که در گردش کار ایجاد می شود
    /// Return Related Message in "FindRequestErrorMsg" function
    /// </summary>
    public enum ErrorWFNextStep
    {
        InsertParentAccId = 1, InsertMembershipEarningsAccId, MainBankAccId, InsertGetFirstMembershipCost,
        InsertAtLeastOneMemberLicence, AllMemberLicenceNotConfirmed, ChooseDefualtLicence, Error,
        YouCanNotSentDocToSelectedTask, SerialNoAndExpireDateCanNotbeNull, UserIsNotInNezamChart, KarshenasiLicenceIsNecessaryForMembership,
        MemberLicenceIsFake, MemberHasUnAcceptedRequest,
        TempMemberCanNotBeConfirmed
            , NoSaveOrder, NoSaveFinalOrder, NoSaveComplainSession,
        CanNotSetDocumentToSettelmentCuaseOfUnConfirmingMemberRequest, MemberHasWorkRequestAndAgentChangeInThisRequestSendToConfirmByTs, MeMberHasWorkRequestChangesFirstConfirmThat, CanNotFindeFileDetails, ErrorInUpdateWorkREquestCapacity
           , CanNotSetDocumentToSettelmentCauseMeTitleIdIsNull, CanNotConfirmProjectAllPlansNotConfirm
    }

    public class WFPermission
    {
        public bool BtnNew;
        public bool BtnEdit;
        public bool BtnSave;
        public bool BtnInactive;
        public bool BtnNewRequest;
    }

    public class WFParameters
    {
        public string UserTaskPageQueryStringName = "SrcPg";
        public string UserTaskPageQueryStringValue = "WF";
    }
    #endregion

    public class WorkFlowPermission
    {
        enum NextTaskType
        {
            ConfirmProccess = 1,
            RejectProccess = 2
        }

        public static String WFUserControlGridsCallbackName
        {
            get { return "WFUserControl"; }
        }


        #region Private Parameters

        #region Managers
        private TSP.DataManager.InstitueCertificateManager InstitueCertificateManager;
        private TSP.DataManager.TeacherManager TeacherManager;
        private TSP.DataManager.TrainingJudgmentManager TrainingJudgmentManager;
        private TSP.DataManager.SeminarRequestManager SeminarRequestManager;
        private TSP.DataManager.PeriodPresentRequestManager PeriodPresentRequestManager;
        private TSP.DataManager.SmsManager SmsManager;
        private TSP.DataManager.MemberManager MemberManager;
        private TSP.DataManager.MemberRequestManager MemberRequestManager;
        private TSP.DataManager.DocMemberFileManager DocMemberFileManager;
        private TSP.DataManager.OfficeRequestManager OfficeRequestManager;
        private TSP.DataManager.EngOffFileManager EngOffFileManager;
        private TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager;
        private TSP.DataManager.TechnicalServices.PlansManager PlansManager;
        private TSP.DataManager.EmployeeRequestManager EmployeeRequestManager;
        private TSP.DataManager.MemberCardsManager MemberCardsManager;
        private TSP.DataManager.TechnicianRequestManager TechnicianRequestManager;
        private TSP.DataManager.PeriodRegisterManager PeriodRegisterManager;
        private TSP.DataManager.Session.SessionRequestsManager SessionRequestsManager;
        private TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new DataManager.WorkFlowStateManager();
        private TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new DataManager.WorkFlowTaskManager();
        private TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TeacherCertificateManager();
        private TSP.DataManager.ExpertFileRequestManager ExpertFileRequestManager;
        private TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager;
        private TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager;
        private TSP.DataManager.TechnicalServices.CapacityReleaseManager CapacityReleaseManager;
        #endregion

        #endregion

        #region Constructors
        public WorkFlowPermission()
        {
            InstitueCertificateManager = new InstitueCertificateManager();
            TeacherManager = new TeacherManager();
            TrainingJudgmentManager = new TrainingJudgmentManager();
            SeminarRequestManager = new SeminarRequestManager();
            PeriodPresentRequestManager = new PeriodPresentRequestManager();
            SmsManager = new SmsManager();
            MemberManager = new MemberManager();
            MemberRequestManager = new MemberRequestManager();
            DocMemberFileManager = new DocMemberFileManager();
            OfficeRequestManager = new OfficeRequestManager();
            EngOffFileManager = new EngOffFileManager();
            ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
            EmployeeRequestManager = new EmployeeRequestManager();
            MemberCardsManager = new MemberCardsManager();
            TechnicianRequestManager = new TechnicianRequestManager();
            PeriodRegisterManager = new PeriodRegisterManager();
            SessionRequestsManager = new Session.SessionRequestsManager();
            WorkFlowStateManager = new WorkFlowStateManager();
            WorkFlowTaskManager = new WorkFlowTaskManager();
            ObserverWorkRequestChangesManager = new TechnicalServices.ObserverWorkRequestChangesManager();
            ObserverWorkRequestManager = new TechnicalServices.ObserverWorkRequestManager();
            CapacityReleaseManager = new TechnicalServices.CapacityReleaseManager();
        }

        public WorkFlowPermission(int WFCode, int CurrentAgentId, TSP.DataManager.TransactionManager TransactionManager)
        {
            InstitueCertificateManager = new InstitueCertificateManager(TransactionManager);
            TeacherManager = new TeacherManager();
            TeacherCertificateManager = new TeacherCertificateManager();
            TrainingJudgmentManager = new TrainingJudgmentManager();
            SeminarRequestManager = new SeminarRequestManager(TransactionManager);
            PeriodPresentRequestManager = new PeriodPresentRequestManager(TransactionManager);
            SmsManager = new SmsManager();
            MemberManager = new MemberManager();
            MemberRequestManager = new MemberRequestManager(TransactionManager, CurrentAgentId);
            DocMemberFileManager = new DocMemberFileManager(TransactionManager);
            OfficeRequestManager = new OfficeRequestManager(TransactionManager, CurrentAgentId);
            EngOffFileManager = new EngOffFileManager(TransactionManager);
            ProjectRequestManager = new TechnicalServices.ProjectRequestManager(TransactionManager);
            PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
            EmployeeRequestManager = new EmployeeRequestManager(TransactionManager);
            MemberCardsManager = new MemberCardsManager(TransactionManager);
            TechnicianRequestManager = new TechnicianRequestManager(TransactionManager);
            PeriodRegisterManager = new PeriodRegisterManager();
            SessionRequestsManager = new Session.SessionRequestsManager(TransactionManager);
            ObserverWorkRequestChangesManager = new TechnicalServices.ObserverWorkRequestChangesManager(TransactionManager);
            CapacityReleaseManager = new TechnicalServices.CapacityReleaseManager(TransactionManager);

            if (TransactionManager != null)
            {
                switch (WFCode)
                {
                    case (int)WorkFlows.CancelPeriod:

                        break;

                    case (int)WorkFlows.DocumentOfMemberConfirming:
                        TransactionManager.Add(DocMemberFileManager);
                        break;
                    case (int)WorkFlows.EngOfficeConfirming:
                        TransactionManager.Add(EngOffFileManager);
                        break;
                    case (int)WorkFlows.ImplementDocumentConfirming:
                        TransactionManager.Add(DocMemberFileManager);
                        break;

                    case (int)WorkFlows.InstitueConfirming:
                        TransactionManager.Add(InstitueCertificateManager);
                        break;

                    case (int)WorkFlows.MemberConfirming:
                        TransactionManager.Add(MemberRequestManager);
                        ObserverWorkRequestManager = new TechnicalServices.ObserverWorkRequestManager();
                        TransactionManager.Add(ObserverWorkRequestManager);
                        break;

                    case (int)WorkFlows.MemberResearchActivity:
                        TransactionManager.Add(TrainingJudgmentManager);
                        break;
                    case (int)WorkFlows.ObservationDocumentConfirming:
                        TransactionManager.Add(DocMemberFileManager);
                        break;
                    case (int)WorkFlows.TSWorkRequestConfirming:
                        TransactionManager.Add(ObserverWorkRequestChangesManager);
                        break;

                    case (int)WorkFlows.OfficeConfirming:
                        TransactionManager.Add(OfficeRequestManager);
                        break;

                    case (int)WorkFlows.OfficeMembershipConfirming:
                        TransactionManager.Add(OfficeRequestManager);
                        break;

                    case (int)WorkFlows.PeriodConfirming:
                    case (int)WorkFlows.PrindPeriodCertificates:
                        TransactionManager.Add(PeriodPresentRequestManager);
                        break;

                    case (int)WorkFlows.SeminarConfirming:
                        TransactionManager.Add(SeminarRequestManager);
                        break;

                    case (int)WorkFlows.SMSConfirming:
                        TransactionManager.Add(SmsManager);
                        break;

                    case (int)WorkFlows.TeachersConfirming:
                        TransactionManager.Add(TeacherManager);
                        TransactionManager.Add(TeacherCertificateManager);
                        break;

                    case (int)WorkFlows.TSProjectConfirming:
                        TransactionManager.Add(ProjectRequestManager);
                        break;

                    case (int)WorkFlows.TSBuildingsLicenseConfirming:
                        TransactionManager.Add(ProjectRequestManager);
                        break;

                    case (int)WorkFlows.TSObserverChangesConfirming:
                        TransactionManager.Add(ProjectRequestManager);
                        break;

                    case (int)WorkFlows.TSChangeImplementerConfirming:
                        TransactionManager.Add(ProjectRequestManager);
                        break;

                    case (int)WorkFlows.TSPlansConfirming:
                        TransactionManager.Add(PlansManager);
                        break;

                    case (int)WorkFlows.TSPlanRevisionConfirming:
                        TransactionManager.Add(PlansManager);
                        break;

                    case (int)WorkFlows.TSChangePlansAndDesigner:
                        TransactionManager.Add(PlansManager);
                        break;

                    case (int)WorkFlows.TSEndStructuralProjectLicenceConfirming:

                        break;

                    case (int)WorkFlows.TSPlanMethodsChangesConfirming:

                        break;

                    case (int)WorkFlows.EmployeeRequestConfirming:
                        TransactionManager.Add(EmployeeRequestManager);
                        break;

                    case (int)WorkFlows.MemberCardRequestConfirming:
                        TransactionManager.Add(MemberCardsManager);
                        break;
                    case (int)WorkFlows.TechnicianRequestConfirming:
                        TransactionManager.Add(TechnicianRequestManager);
                        break;

                    case (int)WorkFlows.PeriodRegisterLicenceOutOfTime:
                        TransactionManager.Add(PeriodRegisterManager);
                        break;
                    case (int)WorkFlows.MemberTransferConfirming:
                        TransactionManager.Add(MemberRequestManager);
                        break;
                    case (int)WorkFlows.Session:
                        TransactionManager.Add(SessionRequestsManager);
                        break;
                    case (int)WorkFlows.ExpertFileRequest:
                        ExpertFileRequestManager = new ExpertFileRequestManager();
                        TransactionManager.Add(ExpertFileRequestManager);
                        break;
                    case (int)WorkFlows.TSCapacityRelease:
                        TransactionManager.Add(CapacityReleaseManager);
                        break;
                }
            }

        }
        #endregion

        #region Utility
        private int FindNmcId(int CurrentUserId)
        {
            TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

            LoginManager.FindByCode(CurrentUserId);
            int NmcId = -1;
            if (LoginManager.Count > 0)
            {
                int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
                int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
                NezamChartManager.FindByEmpId(EmpId, UltId);
                if (NezamChartManager.Count > 0)
                {
                    NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
                }
                else
                {
                    return (-1);
                }
            }
            else
            {
                return (-1);
            }
            return (NmcId);
        }

        /// <summary>
        /// Return the suitable Message for specific ErrorCode
        /// </summary>
        /// <param name="ErrorCode"></param>
        /// <returns></returns>
        public string FindRequestErrorMsg(int ErrorCode)
        {
            string ErrorMsg = "";
            switch (ErrorCode)
            {
                #region Common Error
                case (int)TSP.DataManager.ErrorRequest.Error:
                    ErrorMsg = "خطایی در ذخیره ایجاد شده است.";
                    break;
                case (int)TSP.DataManager.ErrorRequest.LoseRequestInfo:
                    ErrorMsg = "اطلاعات مربوط به درخواست توسط کاربر دیگری تغییر یافته است.";
                    break;

                case (int)TSP.DataManager.ErrorRequest.RequestIsConfirmed:
                    ErrorMsg = "وضعیت درخواست مورد نظر مشخص شده است. امکان تغییر وضعیت وجود ندارد";
                    break;


                case (int)TSP.DataManager.ErrorRequest.YouCanNotSentDocToNextStep:
                    ErrorMsg = "امکان تغییر مرحله گردش کار پرونده برای شما وجود ندارد.";
                    break;

                case (int)TSP.DataManager.ErrorWFNextStep.YouCanNotSentDocToSelectedTask:
                    ErrorMsg = "شما قادر به ارسال پرونده به مرحله انتخاب شده نیستید.";
                    break;

                case (int)TSP.DataManager.ErrorWFNextStep.UserIsNotInNezamChart:
                    ErrorMsg = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
                    break;
                case (int)TSP.DataManager.ErrorRequest.YouAreNotTheRequestCreator:
                    ErrorMsg = "درخواست توسط نوع کاربری دیگری ثبت شده است.شما قادر به تغییر وضعیت آن نمی باشید.";
                    break;

                #endregion

                #region TechnicalService
                case (int)TSP.DataManager.ErrorRequest.ArchitecturePlanNotConfirm:
                    ErrorMsg = "بدلیل عدم تایید نقشه معماری پروژه امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.StructuralPlanNotConfirm:
                    ErrorMsg = "بدلیل عدم تایید نقشه سازه پروژه امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.ElectricalInsPlanNotConfirm:
                    ErrorMsg = "بدلیل عدم تایید نقشه تاسیسیات برق پروژه امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.MechanicInsPlanNotConfirm:
                    ErrorMsg = "بدلیل عدم تایید نقشه تاسیسات مکانیک پروژه امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NoMainRegisteredNo:
                    ErrorMsg = "به دلیل استفاده از پلاک ثبتی اصلی در گزارش طراح و ناظر به شهرداری ورود آن اجباری می باشد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.BlockNumIsNotMatch:
                    ErrorMsg = "بدلیل عدم تطبیق تعداد بلوک ثبت شده در دستور نقشه با بلوکهای ثبت شده، امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.FoundationIsNotMatch:
                    ErrorMsg = "بدلیل عدم تطبیق زیربنای ثبت شده در اطلاعات پایه پروژه با مجموع زیربنای بلوکهای ثبت شده، امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.No5In1000Fiche:
                    ErrorMsg = "بدلیل ثبت نشدن فیش 5*1000(پنج در هزار)، امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.AmountOf5In1000FicheNotMatch:
                    ErrorMsg = "بدلیل نادرست بودن مبلغ فیش 5*1000(پنج در هزار)، امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NoObserversFiche:
                    ErrorMsg = "بدلیل ثبت نشدن فیش دستمزد ناظر، امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.AmountOfObserversFicheNotMatch:
                    ErrorMsg = "بدلیل نادرست بودن مبلغ فیش دستمزد ناظر، امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NotEnoughCapacityForObservers:
                    ErrorMsg = "ظرفیت ناظر پروژه کافی نیست و امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.No2In1000Fiche:
                    ErrorMsg = "بدلیل ثبت نشدن فیش 2 * 1000(دو در هزار)، امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.AmountOf2In1000FicheNotMatch:
                    ErrorMsg = "بدلیل نادرست بودن مبلغ فیش 2 * 1000(دو در هزار)، امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NoImplementer:
                    ErrorMsg = "با توجه به شرایط پروژه ثبت مجریان پروژه الزامی می باشد، تنها امکان ارسال گردش کار پروژه به مرحله انتخاب شده نمی باشد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NoImplementerAgent:
                    ErrorMsg = "بدلیل ثبت نشدن نماینده مجری امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NoObserver:
                    ErrorMsg = "بدلیل ثبت نشدن ناظر، امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NoObserversMother:
                    ErrorMsg = "بدلیل ثبت نشدن ناظر هماهنگ کننده، امکان ارسال پرونده پروژه به مرحله بعد وجود ندارد.";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NotEnoughCapacityForImplementer:
                    ErrorMsg = "ظرفیت مجری پروژه کافی نیست و امکان ارسال پرونده پروژه به مراحل بعد وجود ندارد .";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NoOwnerAgent:
                    ErrorMsg = "بدلیل ثبت نشدن نماینده مالکین، امکان ارسال پرونده پروژه به مرحله بعد وجود ندارد.";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NoBlock:
                    ErrorMsg = "بدلیل ثبت نشدن بلوک، امکان ارسال پرونده پروژه به مرحله بعد وجود ندارد.";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NotMatchStepsWithGroup:
                    ErrorMsg = "بیشترین تعداد طبقات ثبت شده در بلوک ها، با محدوده طبقات گروه ساختمانی پروژه مطابقت ندارد.";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NotPayObserverFish:
                    ErrorMsg = "بدلیل عدم پرداخت فیش دستمزد ناظرین، امکان ارسال پرونده پروژه به مرحله بعد وجود ندارد.";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NotPay2In1000Fish:
                    ErrorMsg = "بدلیل عدم پرداخت فیش دو در هزار مجری، امکان ارسال پرونده پروژه به مرحله بعد وجود ندارد.";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NoBuildingsLicense:
                    ErrorMsg = "بدلیل ثبت نشدن پروانه ساخت، امکان ارسال پرونده پروژه به مرحله بعد وجود ندارد.";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NoPay5In1000Fish:
                    ErrorMsg = "بدلیل عدم پرداخت فیش پنج در هزار، امکان ارسال پرونده پروژه به مرحله بعد وجود ندارد.";
                    break;
                case (int)TSP.DataManager.ErrorRequest.ErrorInUpdatingDesignerWorkCount:
                    ErrorMsg = "خطا در بروزرسانی تعداد کار طراحان پروژه ایجاد شده است.";
                    break;
                case (int)TSP.DataManager.ErrorWFNextStep.ErrorInUpdateWorkREquestCapacity:
                    ErrorMsg = "خطا در بروزرسانی اطلاعات آماده بکاری ایجاد شده است.";
                    break;
                #endregion

                #region PlanInfo
                case (int)ErrorRequest.NoDesigner:
                    ErrorMsg = "بدلیل ثبت نشدن طراح، امکان ارسال پرونده به مراحل بعد وجود ندارد .";
                    break;

                case (int)ErrorRequest.NoPlansMasterDesigner:
                    ErrorMsg = "بدلیل ثبت نشدن نماینده طراحان، امکان ارسال پرونده به مراحل بعد وجود ندارد .";
                    break;

                case (int)ErrorRequest.NotEnoughCapacityForDesigners:
                    ErrorMsg = "ظرفیت طراحان نقشه کافی نیست و امکان ارسال پرونده به مراحل بعد وجود ندارد .";
                    break;

                case (int)ErrorRequest.No5PercentFiche:
                    ErrorMsg = "بدلیل ثبت نشدن فیش 5 درصد طراحی، امکان ارسال پرونده به مراحل بعد وجود ندارد .";
                    break;

                case (int)ErrorRequest.AmountOf5PercentFicheNotMatch:
                    ErrorMsg = "بدلیل نادرست بودن مبلغ فیش 5 درصد طراحی، امکان ارسال پرونده به مراحل بعد وجود ندارد .";
                    break;

                case (int)ErrorRequest.NoPay5PercentFiche:
                    ErrorMsg = "بدلیل عدم پرداخت فیش 5 درصد طراحی، امکان ارسال پرونده به مراحل بعد وجود ندارد .";
                    break;

                case (int)ErrorRequest.NotAllDesignerInsertedForPlan:
                    ErrorMsg = "طراح ثبت نشده است  یا متراژ کارکرد طراحان این نقشه با متراژ کل پروژه همخوانی ندارد، امکان ارسال پرونده به مراحل بعد وجود ندارد .";
                    break;

                case (int)ErrorRequest.NoController:
                    ErrorMsg = "بدلیل ثبت نشدن بازبین نقشه، امکان ارسال پرونده  به مراحل بعد وجود ندارد .";
                    break;

                case (int)ErrorRequest.NotEnoughCapacityForMemberDesigners:
                    ErrorMsg = "ظرفیت اعضای شرکت یا دفتر طراحی کافی نیست و امکان ارسال پرونده  به مراحل بعد وجود ندارد .";
                    break;

                case (int)ErrorRequest.JustEndingTaskForConfirmedPlan:
                    ErrorMsg = "بدلیل اینکه نقشه به صورت تایید شده وارد شده است، پرونده نقشه را فقط به مرحله پایانی می توان ارسال کرد.";
                    break;

                case (int)ErrorRequest.NoControllerConfirmation:
                    ErrorMsg = "بدلیل ثبت نشدن تایید یا عدم تایید بازبین نقشه، امکان ارسال پرونده به این مرحله وجود ندارد .";
                    break;

                #endregion

                #region MemebrReq
                case (int)TSP.DataManager.ErrorRequest.NoActiveRequestFind:
                    ErrorMsg = "وضعیت هیچ یک از درخواست های عضویت عضو مورد نظر معلق نمی باشد";
                    break;

                case (int)TSP.DataManager.ErrorRequest.MemberAcountNotBalance:
                    ErrorMsg = "امکان ارسال پرونده به مرحله بعد وجود ندارد.مانده حساب عضو مورد نظر صفر نمی باشد";
                    break;

                case (int)TSP.DataManager.ErrorRequest.MemberInfoWasChanged:
                    ErrorMsg = "اطلاعات عضو توسط کاربر دیگری تغییر یافته است";
                    break;


                case (int)TSP.DataManager.ErrorWFNextStep.InsertParentAccId:
                    ErrorMsg = "لطفا حساب جاری اعضا را در قسمت تنظیم حسابها انتخاب نمایید.";
                    break;

                case (int)TSP.DataManager.ErrorWFNextStep.InsertMembershipEarningsAccId:
                    ErrorMsg = "لطفا حساب درآمد عضویت را در قسمت تنظیم حسابها انتخاب نمایید";
                    break;
                case (int)TSP.DataManager.ErrorWFNextStep.MainBankAccId:
                    ErrorMsg = "لطفا حساب بانک اصلی را در قسمت تنظیم حسابها انتخاب نمایید";
                    break;

                case (int)TSP.DataManager.ErrorWFNextStep.InsertGetFirstMembershipCost:
                    ErrorMsg = "لطفا ورودیه عضویت را در قسمت تنظیم هزینه های دریافتی وارد نمایید";
                    break;

                case (int)TSP.DataManager.ErrorWFNextStep.InsertAtLeastOneMemberLicence:
                    ErrorMsg = "ثبت حداقل یک مدرک تحصیلی برای ثبت عضویت الزامی می باشد";
                    break;

                case (int)TSP.DataManager.ErrorWFNextStep.AllMemberLicenceNotConfirmed:
                    ErrorMsg = "برای ثبت عضویت باید وضعیت تایید تمامی مدارک تحصیلی مشخص باشند";
                    break;

                case (int)TSP.DataManager.ErrorWFNextStep.MemberLicenceIsFake:
                    ErrorMsg = "بدلیل وجود مدرک جعلی، امکان تایید وجود ندارد";
                    break;

                case (int)TSP.DataManager.ErrorWFNextStep.ChooseDefualtLicence:
                    ErrorMsg = "لطفاً ابتدا مدرک پیش فرض را انتخاب نمایید";
                    break;

                case (int)TSP.DataManager.ErrorWFNextStep.Error:
                    ErrorMsg = "خطایی در ذخیره رخ داده است.";
                    break;
                case (int)TSP.DataManager.ErrorWFNextStep.KarshenasiLicenceIsNecessaryForMembership:
                    ErrorMsg = "ثبت مدرک کارشناسی الزامی می باشد.";
                    break;
                case (int)TSP.DataManager.ErrorWFNextStep.TempMemberCanNotBeConfirmed:
                    ErrorMsg = "امکان تایید نهایی عضو موقت وجود ندارد.جهت تایید عضو موقت ابتدا بایستی مدرک تحصیلی وی استعلام شود.";
                    break;
                case (int)TSP.DataManager.ErrorWFNextStep.MemberHasWorkRequestAndAgentChangeInThisRequestSendToConfirmByTs:
                    ErrorMsg = "نمایندگی عضو در این درخواست تغییر کرده است و این عضو دارای آماده بکاری در نمایندگی دیگری می باشد.تنها قادر به ارسال گردش کار به مرحله تایید مدیریت خدمات مهندسی می باشید.";
                    break;
                case (int)TSP.DataManager.ErrorWFNextStep.MeMberHasWorkRequestChangesFirstConfirmThat:
                    ErrorMsg = "امکان تایید نهایی درخواست وجود ندارد.این عضو دارای درخواست آماده بکاری در جریان است. جهت تاثیر تغییرات این درخواست در آماده به کاری شخص بایستی ابتدا آن درخواست را تایید نمایید.";
                    break;
                case (int)TSP.DataManager.ErrorWFNextStep.CanNotFindeFileDetails:
                    ErrorMsg = "اطلاعات مربوط به صلاحیت های نظارت / نقشه برداری / طراحی / شهرسازی جهت بروزرسانی درخواست آماده بکاری یافت نشد.";
                    break;
                #endregion



                #region SMSRequest
                case (int)TSP.DataManager.ErrorRequest.SMSWasInActiveBySender:
                    ErrorMsg = "پیام کوتاه انتخاب شده توسط ارسال کننده پیام غیرفعال شده است.";
                    break;
                case (int)ErrorRequest.SMSExpired:
                    ErrorMsg = "تاریخ اعتبار پیام کوتاه مورد نظر به پایان رسیده است.";
                    break;
                #endregion

                #region EngOfficeRequest

                case (int)TSP.DataManager.ErrorRequest.EngOffManagerNotSave:
                    ErrorMsg = "امکان ارسال پرونده به مرحله بعد وجود ندارد. ثبت مدیر مسئول دفتر الزامی می باشد";
                    break;

                case (int)TSP.DataManager.ErrorRequest.AllEngOfficeMembersNotAcceptCompany:
                    ErrorMsg = "به دلیل عدم پاسخ تمامی اعضای دفتر،امکان تغییر وضعیت وجود ندارد";
                    break;

                case (int)TSP.DataManager.ErrorRequest.EngOffSerialNoAndExpDateNotFilled:
                    ErrorMsg = "شماره سریال و تاریخ اعتبار پروانه مشخص نشده است.امکان تغییر وضعیت وجود ندارد";
                    break;

                case (int)TSP.DataManager.ErrorRequest.OneOfMemberIsInAnotherOffice:
                    ErrorMsg = "امکان تایید وجود ندارد.یکی از اعضا در دفتر/شرکت دیگری عضو می باشد.";
                    break;

                #endregion

                #region OfficeRequest

                case (int)TSP.DataManager.ErrorRequest.NoMemberDefinedForOffice:
                    ErrorMsg = "امکان ارسال به مرحله بعد وجود ندارد.برای شرکت مورد نظر عضو تعریف نشده است ";
                    break;

                case (int)TSP.DataManager.ErrorRequest.OfficManagerHasNotDesignResponsiblity:
                    ErrorMsg = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.پروانه اشتغال مدیر عامل شرکت صلاحیت طراحی ندارد";
                    break;

                case (int)TSP.DataManager.ErrorRequest.OfficManagerHasNotObservationResponsiblity:
                    ErrorMsg = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.پروانه اشتغال مدیر عامل شرکت صلاحیت نظارت ندارد";
                    break;

                case (int)TSP.DataManager.ErrorRequest.OfficManagerHasNotImplementResponsiblity:
                    ErrorMsg = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.پروانه اشتغال مدیر عامل شرکت صلاحیت اجرا ندارد";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NoOneHasSuitableResponsiblityForOfficeRequest:
                    ErrorMsg = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.باید حداقل 1 نفر از اعضای هیئت مدیره دارای صلاحیت پروانه مورد تقاضا باشند";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NonOfTheManagerHasSuitableResponsiblityForOfficeRequest:
                    ErrorMsg = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.باید حداقل 1 نفر از اعضای هیئت مدیره دارای صلاحیت پروانه مورد تقاضا باشند";
                    break;

                case (int)TSP.DataManager.ErrorRequest.AtLeastTwoMemberOfOfficeShouldHaveSuitableRes:
                    ErrorMsg = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.باید حداقل 2 نفر از اعضای شرکت دارای صلاحیت پروانه مورد تقاضا باشند";
                    break;

                case (int)TSP.DataManager.ErrorRequest.NoOfficeManager:
                    ErrorMsg = "امکان ثبت شرکت مورد نظر وجود ندارد.مدیر عامل شرکت ثبت نشده است";
                    break;

                case (int)TSP.DataManager.ErrorRequest.AtLeastTwoMemberTypeOfOfficeShouldbeMember:
                    ErrorMsg = "امکان ثبت  شرکت مورد نظر وجود ندارد.باید حداقل 2 نفر از اعضای شرکت عضو سازمان باشند";
                    break;

                case (int)TSP.DataManager.ErrorRequest.MFTypeIsNotRecognized:
                    ErrorMsg = "برای شرکت انتخاب شده نوع پروانه (طراح و ناظر/مجری) مشخص نشده است";
                    break;
                case (int)TSP.DataManager.ErrorRequest.AllOfficeMembersNotAcceptCompany:
                    ErrorMsg = "به دلیل عدم پاسخ تمامی اعضای شرکت،امکان تغییر وضعیت وجود ندارد";
                    break;
                case (int)TSP.DataManager.ErrorRequest.SerialNoAndExpDateNotFilled:
                    ErrorMsg = "شماره سریال و تاریخ اعتبار پروانه مشخص نشده است.امکان تغییر وضعیت وجود ندارد";
                    break;
                //**********************************************************************************************************                
                case (int)TSP.DataManager.ErrorRequest.OfficeManagerHasNotJobCertificate:
                    ErrorMsg = "امکان ثبت پروانه برای شرکت مورد نظر وجود ندارد.براساس ماده 6 بند 6-5-1 بایستی مدیرعامل شرکت دارای پروانه اشتغال به کار مناسب با نوع شرکت(طراحی / اجرا) باشد";
                    break;
                case (int)TSP.DataManager.ErrorRequest.AtLeastOneMemberOfDesOfficeShouldHaveMemberFile:
                    ErrorMsg = "حداقل یک نفر از اعضای هیئت مدیره شرکت طراحی باید دارای پروانه اشتغال به کار باشند";
                    break;
                case (int)TSP.DataManager.ErrorRequest.DesOfficeManagerShouldHaveMemberFile:
                    ErrorMsg = "مدیرعامل شرکت طراحی باید دارای پروانه اشتغال به کار باشد";
                    break;
                case (int)TSP.DataManager.ErrorRequest.OfficeMemberJobCertificateHasExpired:
                    ErrorMsg = "مدت زمان اعتبار پروانه اشتغال یکی از اعضا به پایان رسیده است";
                    break;

                case (int)TSP.DataManager.ErrorRequest.DocumentMajorIsNotDefined:
                    ErrorMsg = "رشته موضوع پروانه مشخص نمی باشد";
                    break;

                case (int)TSP.DataManager.ErrorRequest.AtLeastTwoMemberOfOfficeShouldbeInBoard:
                    ErrorMsg = "امکان ثبت  شرکت مورد نظر وجود ندارد.طبق ماده 6 طراحان حقوقی و ماده 9 مجریان حقوقی کتاب مبحث دوم باید حداقل یک نفر از اعضای شرکت عضو هیئت مدیره باشند";
                    break;
                case (int)TSP.DataManager.ErrorRequest.OtherPersonDoesntAllowToBeManager:
                    ErrorMsg = "براساس ماده 6 بند 6-5-1 بایستی مدیرعامل شرکت طراح و ناظر دارای پروانه اشتغال به کار طراحی باشد و بنابراین بایستی عضو نظام مهندسی باشد.";
                    break;
                case (int)TSP.DataManager.ErrorRequest.OfficeManagerCantBeOtherPerson:
                    //    ErrorMsg = "مدیر عامل شرکت تنها می تواند از بین اعضای سازمان انتخاب شود";
                    ErrorMsg = "براساس ماده 6 بند 6-5-1 بایستی مدیرعامل شرکت طراح و ناظر دارای پروانه اشتغال به کار طراحی باشد و بنابراین بایستی عضو نظام مهندسی باشد.";
                    break;
                case (int)TSP.DataManager.ErrorRequest.AtLeastTwoMemberOfImpOfficeShouldHaveMemberFile:
                    ErrorMsg = "طبق ماده 9 بند 9-1-4 کتاب مبحث دوم حداقل دو نفر از اعضای شرکت اجرایی باید دارای پروانه اشتغال به کار باشند";
                    break;

                case (int)TSP.DataManager.ErrorRequest.AtLeastTwoMemberOfObserverDesignOfficeShouldHaveMemberFile:
                    ErrorMsg = "طبق ماده 6 بند 6-1-4 کتاب مبحث دوم حداقل دو نفر از اعضای شرکت طراح و ناظر باید دارای پروانه اشتغال به کار باشند";
                    break;
                case (int)TSP.DataManager.ErrorRequest.MemberNumberOfLimitedJointStockShouldBeGreaterThanTwo:
                    ErrorMsg = "بر اساس ماده 6 بند 6-1-1 و ماده 9 بند 9-1-1 کتاب مبحث دوم و طبق قانون تجارت ذکر شده توسط اداره ثبت شرکت ها و مالکیت صنعتی ، مجموع تعداد سهامداران و اعضای هیئت مدیره شرکت های مسئولیت محدود بایستی حداقل دو نفر باشد.";
                    break;

                case (int)TSP.DataManager.ErrorRequest.MemberNumberOfPrivateAndPublicJointStockShouldBeGreaterThanThree:
                    ErrorMsg = "بر اساس ماده 6 بند 6-1-1 و ماده 9 بند 9-1-1 کتاب مبحث دوم و طبق قانون تجارت ذکر شده توسط اداره ثبت شرکت ها و مالکیت صنعتی ، مجموع تعداد سهامداران و اعضای هیئت مدیره شرکت های سهامی خاص بایستی حداقل سه نفر باشد.";
                    break;

                #endregion

                #region DocMemberFile Request
                case (int)TSP.DataManager.ErrorRequest.ExamInfoDoesNotSave:
                    ErrorMsg = "اطلاعات مربوط به آزمون های پذیرفته شده وارد نشده است.";
                    break;

                case (int)TSP.DataManager.ErrorWFNextStep.SerialNoAndExpireDateCanNotbeNull:
                    ErrorMsg = "اطلاعات مربوط به شماره سریال و تاریخ اعتبار مجوز وارد نشده است.";
                    break;
                case (int)TSP.DataManager.ErrorWFNextStep.CanNotSetDocumentToSettelmentCuaseOfUnConfirmingMemberRequest:
                    ErrorMsg = "برای این عضو امکان ارسال پروانه اشتغال به کار به تایید مسکن وجود ندارد.این عضو درواحد عضویت درخواست درجریان دارد.چنانچه پس از 48 ساعت درخواست وی تایید نشد،با واحد عضویت سازمان تماس حاصل نمایید";
                    break;
                case (int)TSP.DataManager.ErrorWFNextStep.CanNotSetDocumentToSettelmentCauseMeTitleIdIsNull:
                    ErrorMsg = "عنوان شخص در گواهینامه ثبت نشده است.";
                    break;
                case (int)TSP.DataManager.ErrorWFNextStep.CanNotConfirmProjectAllPlansNotConfirm:
                    ErrorMsg = "بدلیل در جریان بودن نقشه امکان تایید درخواست پروژه وجود ندارد.";
                    break;
                #endregion

                #region MemberCard
                case (int)TSP.DataManager.ErrorWFNextStep.MemberHasUnAcceptedRequest:
                    ErrorMsg = "به دلیل وجود درخواست تغییر اطلاعات در واحد عضویت امکان تایید نهایی وجود ندارد";
                    break;
                #endregion

                #region Entezami
                case (int)TSP.DataManager.ErrorWFNextStep.NoSaveOrder:
                    ErrorMsg = "برای رفتن به مرحله بعد لازم است برای تمامی متشاکیان حکم تعریف شود";
                    break;
                case (int)TSP.DataManager.ErrorWFNextStep.NoSaveFinalOrder:
                    ErrorMsg = "برای رفتن به مرحله بعد لازم است برای تمامی متشاکیان حکم قطعی تعریف شود";
                    break;
                case (int)TSP.DataManager.ErrorWFNextStep.NoSaveComplainSession:
                    ErrorMsg = "برای رفتن به مرحله بعد لازم است برای پرونده دستورجلسه ثبت شود";
                    break;
                #endregion

                #region Session
                case (int)TSP.DataManager.ErrorRequest.SessionNoPermissionForType:
                    ErrorMsg = "شما سطح دسترسی این نوع جلسه را ندارید";
                    break;
                #endregion

                #region 
                case (int)TSP.DataManager.ErrorRequest.PeriodRegisterMarksAreNotInserted:
                    ErrorMsg = "نمرات شرکت کنندگان دوره ثبت نشده است";
                    break;
                    #endregion

            }

            return ErrorMsg;
        }
        #endregion

        #region Main Methods
        /// <summary>
        /// Filter the SendBackTask DataTable for specific WF on specific Condition
        /// This function Called in "SelectSendBackTask" That is in WFUserControl
        /// بر اساس گردش کار های مختلف لیست عملیات ها فیلتر می شود
        /// </summary>
        /// <param name="WFCode"></param>
        /// <param name="TableId"></param>
        /// <param name="dtSendBackTask"></param>
        /// <returns></returns>
        public DataTable FilterSendBackDataTable(int WFCode, int TableId, DataTable dtSendBackTask)
        {
            switch (WFCode)
            {
                //case (int)WorkFlows.MemberConfirming:
                //    DataTable dtMeReq = MemberRequestManager.FindByMemberId(TableId, 0, -1);
                //    if (dtMeReq.Rows.Count > 0)
                //    {
                //        if (dtMeReq.Rows[0]["MsId"].ToString() != "6")//بازگشت به سازمان
                //        {
                //            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
                //            int AccountingConfirmingTaskCode = (int)TSP.DataManager.WorkFlowTask.AccountingManagerConfirmingMember;
                //            WorkFlowTaskManager.FindByTaskCode(AccountingConfirmingTaskCode);
                //            if (WorkFlowTaskManager.Count == 1)
                //                AccountingConfirmingTaskCode = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                //            dtSendBackTask.DefaultView.RowFilter = "TaskId<>" + AccountingConfirmingTaskCode.ToString();
                //        }
                //    }
                //    break;
            }
            return dtSendBackTask;
        }

        public int FindTaskIdForFiltterSendBackDataTable(int WFCode, int TableId)
        {
            int TaskId = -1;
            switch (WFCode)
            {
                case (int)WorkFlows.MemberConfirming:
                    //DataTable dtMeReq = MemberRequestManager.FindByMemberId(TableId, 0, -1);
                    //if (dtMeReq.Rows.Count > 0)
                    //{
                    //    if (dtMeReq.Rows[0]["MsId"].ToString() != "6")//بازگشت به سازمان
                    //    {
                    //        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
                    //        int AccountingConfirmingTaskCode = (int)TSP.DataManager.WorkFlowTask.AccountingManagerConfirmingMember;
                    //        WorkFlowTaskManager.FindByTaskCode(AccountingConfirmingTaskCode);
                    //        if (WorkFlowTaskManager.Count == 1)
                    //            TaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);
                    //        // dtSendBackTask.DefaultView.RowFilter = "TaskId<>" + AccountingConfirmingTaskCode.ToString();
                    //    }
                    //}
                    break;
            }
            return TaskId;
        }

        /// <summary>
        /// Check the Condition that should be checked for the specific task of WF , befor select the SendBackTask
        /// This function Called in "SelectSendBackTask" That is in WFUserControl
        /// شرایط یک پرونده برای یک گردش کار خاص ، هنگام کلیک بر روی دکمه گردش کار و ایجاد لیست عملیات ها چک می کند       
        /// </summary>
        /// <param name="WFCode"></param>
        /// <param name="TableId">Id of current Request</param>
        /// <param name="CurrentTaskCode"></param>
        /// <returns></returns>
        public int CheckSelectSendBackTaskPermissionForSpecificWF(int WFCode, int TableId, int CurrentTaskCode, int CurrentUserId)
        {
            int Per = 0;

            switch (WFCode)
            {
                case (int)WorkFlows.CancelPeriod:
                    break;

                case (int)WorkFlows.DocumentOfMemberConfirming:
                    Per = DocMemberFileManager.CheckPermissionDocMemberFileConfirmingSendBackTask(TableId, CurrentTaskCode);
                    break;
                case (int)WorkFlows.EngOfficeConfirming:
                    Per = EngOffFileManager.CheckPermissionEngOffConfirmingSendBackTask(TableId, CurrentTaskCode);
                    break;
                case (int)WorkFlows.ImplementDocumentConfirming:
                    //***Does not have Method For checkPermissionSendBackTask
                    break;

                case (int)WorkFlows.InstitueConfirming:
                    //***Does not have Method For checkPermissionSendBackTask
                    break;

                case (int)WorkFlows.MemberConfirming:
                    Per = MemberRequestManager.CheckPermissionMemberConfirmingSendBackTask(TableId, CurrentTaskCode);
                    break;

                case (int)WorkFlows.MemberTransferConfirming:
                    Per = MemberRequestManager.CheckPermissionMemberTansferConfirmingSendBackTask(TableId, CurrentTaskCode);
                    break;

                case (int)WorkFlows.MemberResearchActivity:
                    //***Does not have Method For checkPermissionSendBackTask
                    break;
                case (int)WorkFlows.ObservationDocumentConfirming:
                    //***Does not have Method For checkPermissionSendBackTask
                    break;
                case (int)WorkFlows.TSWorkRequestConfirming:
                    //***Does not have Method For checkPermissionSendBackTask
                    break;
                case (int)WorkFlows.OfficeConfirming:
                    Per = OfficeRequestManager.CheckPermissionOfficeDocConfirmingSendBackTask(TableId, CurrentTaskCode);
                    break;

                case (int)WorkFlows.OfficeMembershipConfirming:
                    Per = OfficeRequestManager.CheckPermissionOfficeConfirmingSendBackTask(TableId, CurrentTaskCode);
                    break;

                case (int)WorkFlows.PeriodConfirming:
                    //***Does not have Method For checkPermissionSendBackTask
                    Per = PeriodPresentRequestManager.CheckPermissionPeriodConfirmingSendBackTask(TableId, CurrentTaskCode);
                    break;

                case (int)WorkFlows.SeminarConfirming:
                    //***Does not have Method For checkPermissionSendBackTask
                    break;

                case (int)WorkFlows.SMSConfirming:
                    Per = SmsManager.CheckPermissionSMSConfirmingSendBackTask(TableId, CurrentTaskCode);
                    break;

                case (int)WorkFlows.TeachersConfirming:
                    //***Does not have Method For checkPermissionSendBackTask
                    break;
                    
                case (int)WorkFlows.TSEndStructuralProjectLicenceConfirming:

                    break;         

                case (int)WorkFlows.TSPlanMethodsChangesConfirming:

                    break;
                case (int)WorkFlows.TSPlansConfirming:
                    Per = PlansManager.CheckPermissionPlansConfirmingSendBackTask(TableId, CurrentTaskCode, CurrentUserId);
                    break;

                case (int)WorkFlows.TSProjectConfirming:
                    //  Per = ProjectRequestManager.CheckPermissionProjectConfirmingSendBackTask(TableId, CurrentTaskCode);
                    break;


                case (int)WorkFlows.EmployeeRequestConfirming:

                    break;

                case (int)WorkFlows.Session:
                    Per = SessionRequestsManager.CheckPermissionOfConfirmingRelatedManager(TableId, CurrentTaskCode, CurrentUserId);
                    break;
            }

            return Per;
        }

        /// <summary>
        /// چک می شود آیا شروع کننده گردش کار کاربر جاری بوده است
        /// </summary>
        /// <param name="WFCode"></param>
        /// <param name="UltId">Of Current User</param>
        /// <param name="CurrentNmcIdType">Of Request</param>
        /// <param name="CurrentTaskCode">Of Request</param>
        /// <param name="DocMeFileSaveInfoTaskCode">Of Request</param>
        /// <returns></returns>
        public Boolean IsCurrentUserIsRequestStarter(int WFCode, int UltId, int CurrentNmcIdType, int CurrentTaskCode)
        {
            Boolean Per = true;
            //  int SaveInfoTaskCode = -1;
            switch (WFCode)
            {
                case (int)WorkFlows.DocumentOfMemberConfirming:
                    //SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
                    //if (UltId == (int)UserType.Employee)
                    //{
                    //    if (CurrentNmcIdType == (int)WorkFlowStateNmcIdType.MeId && CurrentTaskCode == SaveInfoTaskCode)
                    //    {
                    //        Per = false;
                    //    }
                    //}
                    break;
                case (int)WorkFlows.ImplementDocumentConfirming:
                    //SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
                    //if (UltId == (int)UserType.Employee)
                    //{
                    //    if (CurrentNmcIdType == (int)WorkFlowStateNmcIdType.MeId && CurrentTaskCode == SaveInfoTaskCode)
                    //    {
                    //        Per = false;
                    //    }
                    //}
                    break;
            }
            return Per;
        }

        #region CheckAndDoSendDocToNextStepConditions ******توابعی که در آنها شرایط پرونده نسبت به کاربر جاری/عملیات انتخاب شده و.... را چک می کند و عملیات های مورد نیاز پس از انتخاب یک عملیات خاص را انجام می دهد
        /// <summary>
        /// Check the condition of sending document to next step and do the next of sending doc to next step
        /// شرایط پرونده نسبت به کاربر جاری/عملیات انتخاب شده و.... را چک می کند و عملیات های مورد نیاز پس از انتخاب یک عملیات خاص را انجام می دهد
        ///Call in the SendDocToNextStep function in the WFUserControl
        /// </summary>
        /// <param name="WFCode"></param>
        /// <param name="TableId">Id of request table</param>
        /// <param name="SelectedTaskId"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="CurentUserUltId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <returns></returns>
        public int CheckAndDoSendDocToNextStepConditions(int WFCode, int TableId, int SelectedTaskId, int CurrentUserId, int CurentUserUltId, int CurrentUserAgentId, int CurrentCounId, ref ArrayList ArrayReturnValue, int CurrentTaskId, Int64 CurrentWFStateId)
        {
            int Per = (int)ErrorWFNextStep.Error;
            int RejectProccessTaskCode = -1;
            int ConfirmProccessTaskCode = -1;
            int NextTask = -1;

            int CurrentTaskCode = FindTaskCode(CurrentTaskId);
            int SelectedTaskCode = FindTaskCode(SelectedTaskId);
            switch (WFCode)
            {
                #region DocumentOfMemberConfirming
                case (int)WorkFlows.DocumentOfMemberConfirming:
                    switch (SelectedTaskCode)
                    {
                        case (int)WorkFlowTask.ConfirmDocumentOfMemberAndEndProccess:
                            if (CurentUserUltId == (int)UserType.Settlement)
                            {
                                Per = DocMemberFileManager.DoNextTaskOfMeDocConfirming(TableId, CurrentUserAgentId, CurrentUserId);
                            }
                            else if (CurentUserUltId == (int)UserType.Employee)
                            {
                                DocMemberFileManager.FindByCode(TableId, 0);
                                if (DocMemberFileManager.Count != 1)
                                {
                                    Per = (int)TSP.DataManager.ErrorWFNextStep.Error;
                                    break;
                                }
                                if (Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.TransferedMemberRequest)
                                    Per = DocMemberFileManager.DoNextTaskOfMeDocConfirming(TableId, CurrentUserAgentId, CurrentUserId);
                                else
                                    Per = (int)TSP.DataManager.ErrorWFNextStep.YouCanNotSentDocToSelectedTask;
                            }
                            else
                                Per = (int)TSP.DataManager.ErrorWFNextStep.YouCanNotSentDocToSelectedTask;


                            break;

                        case (int)WorkFlowTask.RejectDocumentOfMemberAndEndProcess:
                            Per = DocMemberFileManager.DoNextTaskOfMeDocRejecting(TableId, CurrentUserId);
                            break;

                        case (int)WorkFlowTask.CompleteMemebershipData:
                            Per = DocMemberFileManager.UpdateMeDocIncomplateStated(TableId, 1, -1);
                            break;

                        case (int)WorkFlowTask.DocumentOfMemberConfirmingSaveInfo:
                            Per = DocMemberFileManager.UpdateMeDocIncomplateStated(TableId, -1, 1);
                            break;
                        case (int)WorkFlowTask.settlementAgentConfirmingDocument:
                            DocMemberFileManager.FindByCode(TableId, 0);
                            if (DocMemberFileManager.Count != 1)
                            {
                                Per = (int)TSP.DataManager.ErrorWFNextStep.Error;
                                break;
                            }
                            if (Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MeTitleId"]))
                            {
                                Per = (int)TSP.DataManager.ErrorWFNextStep.CanNotSetDocumentToSettelmentCauseMeTitleIdIsNull;
                                break;
                            }
                            MemberRequestManager.FindByMemberId(Convert.ToInt32(DocMemberFileManager[0]["MeId"]), 0, -1);
                            if (MemberRequestManager.Count > 0)
                            {
                                //  ShowMessage ("امکان ثبت درخواست در پروانه اشتغال به کار برای این عضو وجود ندارد.این عضو درواحد عضویت درخواست درجریان دارید.چنانچه پس از 48 ساعت درخواست شما تایید نشد،با واحد عضویت سازمان تماس حاصل نمایید. ");
                                Per = (int)TSP.DataManager.ErrorWFNextStep.CanNotSetDocumentToSettelmentCuaseOfUnConfirmingMemberRequest;
                            }
                            else Per = 0;
                            break;
                        default:
                            Per = 0;
                            break;
                    }
                    if (Per == 0)
                        Per = DocMemberFileManager.UpdateRequestWFStateId(TableId, CurrentWFStateId);

                    break;
                #endregion

                #region EngOfficeConfirming
                case (int)WorkFlows.EngOfficeConfirming:
                    switch (SelectedTaskCode)
                    {
                        case (int)WorkFlowTask.settlementAgentConfiringDocumentEngOffice:
                            Per = EngOffFileManager.CheckPermissionEngOffConfirmingSendBackTask(TableId, FindTaskCode(CurrentTaskId));
                            break;
                        case (int)WorkFlowTask.ConfirmDocumentOfEngOfficeAndEndProccess:
                            #region
                            if (CurentUserUltId != (int)UserType.Settlement)
                            {
                                if (CurentUserUltId == (int)UserType.Employee)
                                {
                                    EngOffFileManager.FindByCode(TableId);
                                    if (EngOffFileManager.Count != 1)
                                        return (int)TSP.DataManager.ErrorWFNextStep.Error;
                                    if (((Convert.ToInt32(EngOffFileManager[0]["Type"]) != (int)TSP.DataManager.EngOffFileType.Invalid))
                                        && ((Convert.ToInt32(EngOffFileManager[0]["Type"]) != (int)TSP.DataManager.EngOffFileType.ChangeBaseInfo))
                                          && ((Convert.ToInt32(EngOffFileManager[0]["Type"]) != (int)TSP.DataManager.EngOffFileType.ConditionalAprrove)))
                                        return (int)TSP.DataManager.ErrorWFNextStep.YouCanNotSentDocToSelectedTask;
                                }
                                else
                                    return (int)TSP.DataManager.ErrorWFNextStep.YouCanNotSentDocToSelectedTask;
                            }
                            Per = EngOffFileManager.DoNextTaskOfEngOfficeDocConfirming(TableId, CurrentUserAgentId, CurrentUserId, ref ArrayReturnValue);
                            #endregion
                            break;
                        case (int)WorkFlowTask.RejectDocumentOfEngOfficeAndEndProcess:
                            Per = EngOffFileManager.DoNextTaskOfEngOfficeDocRejecting(TableId, CurrentUserId);
                            break;
                        default:
                            Per = 0;
                            break;

                    }
                    break;
                #endregion

                #region ImplementDocumentConfirming
                case (int)WorkFlows.ImplementDocumentConfirming:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectImplementDocAndEndProcess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmImplementDocAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);

                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            if (CurentUserUltId != (int)UserType.Settlement)
                            {
                                if (CurentUserUltId == (int)UserType.Employee)
                                {
                                    DocMemberFileManager.SelectImplementDoc(-1, TableId);
                                    if (DocMemberFileManager.Count != 1)
                                        return (int)TSP.DataManager.ErrorWFNextStep.Error;
                                    if (Convert.ToInt32(DocMemberFileManager[0]["Type"]) != (int)TSP.DataManager.DocumentOfMemberRequestType.InActive)
                                        return (int)TSP.DataManager.ErrorWFNextStep.YouCanNotSentDocToSelectedTask;
                                }
                                else
                                    return (int)TSP.DataManager.ErrorWFNextStep.YouCanNotSentDocToSelectedTask;
                            }
                            Per = DocMemberFileManager.DoNextTaskOfMeImpDocConfirming(TableId, CurrentUserAgentId, CurrentUserId);
                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = DocMemberFileManager.DoNextTaskOfMeImpDocRejecting(TableId, CurrentUserId);
                            break;

                        case 0:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion

                #region InstitueConfirming
                case (int)WorkFlows.InstitueConfirming:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectInstitueAndEndProccess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmInstitueAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);

                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = InstitueCertificateManager.DoNextTaskOfConfirming(TableId, CurrentUserId);
                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = InstitueCertificateManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                            break;

                        case 0:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion

                #region MemberConfirming
                case (int)WorkFlows.MemberConfirming:
                    switch (SelectedTaskCode)
                    {
                        case (int)WorkFlowTask.ConfirmMemberAndEndProccess:
                            int CurrentNmcId = FindNmcId(CurrentUserId);
                            if (CurrentNmcId > 0)
                            {
                                MemberRequestManager.FindByCode(TableId);
                                if (MemberRequestManager.Count < 0)
                                {
                                    Per = (int)ErrorWFNextStep.Error;
                                }

                                ObserverWorkRequestManager.SelectTSObserverWorkRequestFullInfoByMember(Convert.ToInt32(MemberRequestManager[0]["MeId"]), TSObserverWorkRequestStatus.Confirm);
                                if (ObserverWorkRequestManager.Count > 0 && Convert.ToInt32(ObserverWorkRequestManager[0]["WantedWorkType"]) != (int)TSP.DataManager.TSWorkRequestWantedWorkType.Design)
                                {
                                    if (Convert.ToInt32(ObserverWorkRequestManager[0]["MeAgentId"]) != Convert.ToInt32(MemberRequestManager[0]["AgentId"]))
                                        Per = (int)ErrorWFNextStep.MemberHasWorkRequestAndAgentChangeInThisRequestSendToConfirmByTs;
                                }
                                else
                                    Per = MemberRequestManager.DoNextTaskOfConfirming(TableId, CurrentUserAgentId, CurrentUserId, CurrentNmcId, ref ArrayReturnValue);
                            }
                            else
                            {
                                Per = (int)ErrorWFNextStep.UserIsNotInNezamChart;
                            }
                            break;
                        case (int)WorkFlowTask.RejectMemberAndEndProcess:
                            Per = MemberRequestManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                            break;
                        default:
                            int MemberLicenceInquiryAndConfirmingTaskId = FindTaskId((int)WorkFlowTask.MemberLicenceInquiryAndConfirming);
                            if (MemberLicenceInquiryAndConfirmingTaskId == SelectedTaskId)
                                Per = MemberRequestManager.DoNextTaskOfMemberLicenceInquiryAndConfirming(TableId, CurrentUserId);
                            else
                                Per = 0;
                            break;
                    }
                    if (Per == 0)
                        Per = MemberRequestManager.UpdateRequestTaskId(TableId, SelectedTaskId);
                    break;
                #endregion

                #region MemberResearchActivity
                case (int)WorkFlows.MemberResearchActivity:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectMemberResearchActAndEndProcess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmMemberResearchActAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);

                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = TrainingJudgmentManager.DoNextTaskOfConfirming(TableId, CurrentUserAgentId, CurrentUserId);
                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = TrainingJudgmentManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                            break;

                        case 0:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion
                #region ObservationDocumentConfirming
                case (int)WorkFlows.ObservationDocumentConfirming:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectObservationDocAndEndProcess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmObservationDocAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);

                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = DocMemberFileManager.DoNextTaskOfMeObsDocConfirming(TableId, CurrentUserAgentId, CurrentUserId);

                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = DocMemberFileManager.DoNextTaskOfMeObsDocRejecting(TableId, CurrentUserId);

                            break;

                        case 0:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion
                #region TSWorkRequestConfirming
                case (int)WorkFlows.TSWorkRequestConfirming:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectTSWorkRequestConfirminAndEndProcess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmTSWorkRequestConfirminAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);

                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = ObserverWorkRequestChangesManager.DoNextTaskOfMeObsDocConfirming(TableId, CurrentUserAgentId, CurrentUserId, CurrentWFStateId, SelectedTaskId);
                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = ObserverWorkRequestChangesManager.DoNextTaskOfMeObsDocRejecting(TableId, CurrentUserId, CurrentWFStateId, SelectedTaskId);
                            break;

                        case 0:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion
                #region OfficeMembershipConfirming
                case (int)WorkFlows.OfficeMembershipConfirming:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectOfficeAndEndProcess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmOfficeAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);



                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = OfficeRequestManager.DoNextTaskOfOfficeConfirming(TableId, CurrentUserAgentId, CurrentUserId, ref ArrayReturnValue);
                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = OfficeRequestManager.DoNextTaskOfOfficeRejecting(TableId, CurrentUserId);
                            break;

                        case 0:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion

                #region OfficeDocumentConfirming
                case (int)WorkFlows.OfficeConfirming:

                    switch (SelectedTaskCode)
                    {
                        case (int)WorkFlowTask.settlementAgentConfiringDocumentOff:
                            Per = OfficeRequestManager.CheckPermissionOfficeDocConfirmingSendBackTask(TableId, FindTaskCode(CurrentTaskId));
                            break;
                        case (int)WorkFlowTask.ConfirmDocumentOfOfficeAndEndProccess:
                            #region
                            if (CurentUserUltId != (int)UserType.Settlement)
                            {
                                if (CurentUserUltId == (int)UserType.Employee)
                                {
                                    OfficeRequestManager.FindByCode(TableId);
                                    if (OfficeRequestManager.Count != 1)
                                        return (int)TSP.DataManager.ErrorWFNextStep.Error;
                                    if ((Convert.ToInt32(OfficeRequestManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.Invalid)
                                         && (Convert.ToInt32(OfficeRequestManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo)
                                        && (Convert.ToInt32(OfficeRequestManager[0]["Type"]) != (int)TSP.DataManager.OfficeRequestType.DocumentInvalid))
                                        return (int)TSP.DataManager.ErrorWFNextStep.YouCanNotSentDocToSelectedTask;
                                }
                                else
                                    return (int)TSP.DataManager.ErrorWFNextStep.YouCanNotSentDocToSelectedTask;
                            }
                            Per = OfficeRequestManager.DoNextTaskOfDocConfirming(TableId, CurrentUserAgentId, CurrentUserId, ref ArrayReturnValue);
                            #endregion
                            break;
                        case (int)WorkFlowTask.RejectDocumentOfOfficeAndEndProcess:
                            #region
                            Per = OfficeRequestManager.DoNextTaskOfDocRejecting(TableId, CurrentUserId);
                            #endregion
                            break;
                        default:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion

                #region PeriodConfirming
                case (int)WorkFlows.PeriodConfirming:
                    switch (SelectedTaskCode)
                    {
                        case (int)WorkFlowTask.SavePeriodInfo:
                        case (int)WorkFlowTask.LearningExpertConfirmingPeriod:
                        case (int)WorkFlowTask.LearningManagerConfirmingPeriod:
                            Per = PeriodPresentRequestManager.UpdatePeriodStatus(TableId, CurrentUserId, PeriodPresentStatus.Inserting);
                            break;
                        case (int)WorkFlowTask.PeriodRegistration:
                        case (int)WorkFlowTask.RecordAbsenteeism:
                        case (int)WorkFlowTask.PeriodRegistrationJustExam:
                            Per = PeriodPresentRequestManager.UpdatePeriodStatus(TableId, CurrentUserId, PeriodPresentStatus.PeriodRegister);
                            break;
                        case (int)WorkFlowTask.PeriodSaveExamMinute:
                            Per = PeriodPresentRequestManager.UpdatePeriodStatus(TableId, CurrentUserId, PeriodPresentStatus.StartTest);
                            break;
                        case (int)WorkFlowTask.PeriodSavePointsByTeachers:
                            Per = PeriodPresentRequestManager.UpdatePeriodStatus(TableId, CurrentUserId, PeriodPresentStatus.AnnounceResultAndObjection);
                            break;
                        case (int)WorkFlowTask.PeriodConfirmPointsByLearningExpert:
                        case (int)WorkFlowTask.PeriodConfirmPointsByLearningManager:
                        case (int)WorkFlowTask.PeriodConfirmPointsByLearningAssistant:
                        case (int)WorkFlowTask.PeriodConfirmingByRiasatSazemanAndSign:
                        case (int)WorkFlowTask.PeriodConfirmingBysettlementAgent:
                        case (int)WorkFlowTask.PeriodConfirmingByNezamEmployeeInMaskan:
                        case (int)WorkFlowTask.PeriodConfirmingMoavenShahrsaziVaMemariEdareKoleRahoShahrsazi:
                        case (int)WorkFlowTask.PeriodRoadAndurbanismConfirmingAndSign:
                            Per = PeriodPresentRequestManager.UpdatePeriodStatus(TableId, CurrentUserId, PeriodPresentStatus.EndObjection);
                            break;
                        case (int)WorkFlowTask.ConfirmPeriodAndEndProccess:
                            //if (CurentUserUltId != (int)UserType.Settlement)
                            //    Per = (int)TSP.DataManager.ErrorWFNextStep.YouCanNotSentDocToSelectedTask;
                            //else
                            Per = PeriodPresentRequestManager.DoNextTaskOfConfirming(TableId, CurrentUserId);
                            break;

                        case (int)WorkFlowTask.RejectPeriodAndEndProccess:
                            Per = PeriodPresentRequestManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                            break;
                        default:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion

                #region PrindPeriodCertificatesConfirming
                case (int)WorkFlows.PrindPeriodCertificates:
                    switch (SelectedTaskCode)
                    {
                        case (int)WorkFlowTask.PrindPeriodCertificatesRequestConfirmAndEndProccess:
                            //if (CurentUserUltId != (int)UserType.Settlement)
                            //    Per = (int)TSP.DataManager.ErrorWFNextStep.YouCanNotSentDocToSelectedTask;
                            //else
                            Per = PeriodPresentRequestManager.DoNextTaskOfConfirmingPrintCertificatesWF(TableId, CurrentUserId);
                            break;

                        case (int)WorkFlowTask.PrindPeriodCertificatesRequestRejectAndEndProccess:
                            Per = PeriodPresentRequestManager.DoNextTaskOfRejectingPrintCertificatesWF(TableId, CurrentUserId);
                            break;
                        default:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion

                #region SeminarConfirming
                case (int)WorkFlows.SeminarConfirming:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectSeminarAndEndProccess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmSeminarAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);

                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = SeminarRequestManager.DoNextTaskOfConfirming(TableId, CurrentUserId);
                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = SeminarRequestManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                            break;

                        case 0:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion

                #region SMSConfirming
                case (int)WorkFlows.SMSConfirming:
                    //***related code of this part is in the WorkFlowUserTask.aspx                
                    RejectProccessTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectSMSAndEndProcess;
                    ConfirmProccessTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmSMSAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);

                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = SmsManager.DoNextTaskOfConfirming(TableId);
                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = 0;
                            break;

                        case 0:
                            Per = 0;
                            break;
                    }
                    //  Per = 0;
                    break;
                #endregion
                #region TeachersConfirming
                case (int)WorkFlows.TeachersConfirming:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectTeacherAndEndProccess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmTeacherAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);

                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = TeacherCertificateManager.DoNextTaskOfConfirming(TableId, CurrentUserId);
                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = TeacherCertificateManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                            break;

                        //case 0:
                        //    Per = 0;
                        default:
                            Per = 0;
                            break;
                    }
                    //  Per = 0;
                    break;
                #endregion

                #region TechnicalServise
                #region TSProjectConfirming
                case (int)WorkFlows.TSProjectConfirming:

                    RejectProccessTaskCode = (int)WorkFlowTask.RejectProjectAndEndProcess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmingProjectAndEndProccess;
                    Per = ProjectRequestManager.CheckConditionsForNextStepOfConfirming(SelectedTaskId, TableId);
                    if (Per != 0)
                    {
                        break;
                    }
                    int TaskOrder = CheckTaskOrder(CurrentTaskId, SelectedTaskId);
                    int ProjectId = -2;
                    if (TaskOrder == 1 && SelectedTaskCode != (int)TSP.DataManager.WorkFlowTask.RejectProjectAndEndProcess)//****در صورتی که به مراحل قبل ارسال کند نیازی به چک کردن شرایط نیست*******
                        Per = ProjectRequestManager.CheckPermissionProjectConfirmingSendBackTask(TableId, CurrentTaskCode, ref ProjectId);
                    if (Per == 0)
                    {
                        switch (SelectedTaskCode)
                        {
                            case (int)WorkFlowTask.ConfirmingProjectAndEndProccess:
                                Per = ProjectRequestManager.DoNextTaskOfConfirming(TableId, CurrentUserId, CurrentCounId, ProjectId);
                                break;

                            case (int)WorkFlowTask.RejectProjectAndEndProcess:
                                Per = ProjectRequestManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                                break;

                            case 0:
                                Per = 0;
                                break;
                        }
                    }
                    break;
                #endregion
                #region TSPlansConfirming
                case (int)WorkFlows.TSPlansConfirming:

                    int CurrentTskCodePlan = FindTaskCode(SelectedTaskId);
                    Per = PlansManager.CheckConditionsForNextStepOfConfirming(CurrentTskCodePlan, TableId, CurentUserUltId);
                    if (Per == 0)
                    {
                        switch (CurrentTskCodePlan)
                        {
                            case (int)WorkFlowTask.ConfirmingPlanAndEndProccess:
                                Per = PlansManager.DoNextTaskOfConfirming(TableId, CurrentUserId);
                                break;
                            case (int)WorkFlowTask.RejectPlanAndEndProcess:
                                Per = PlansManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                                break;
                        }

                        if (Per == 0)
                            Per = PlansManager.UpdateRequestTaskId(TableId, SelectedTaskId, CurrentWFStateId);
                    }

                    break;
                #endregion               
                #region TSCapacityReleaseConfirming
                case (int)WorkFlows.TSCapacityRelease:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectCapacityReleaseConfirminAndEndProcess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmCapacityReleaseConfirminAndEndProccess;
                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);
                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = CapacityReleaseManager.DoNextTaskOfWorkflowConfirming(TableId, CurrentUserAgentId, CurrentUserId, CurrentWFStateId, SelectedTaskId);
                            break;
                        case (int)NextTaskType.RejectProccess:
                            Per = CapacityReleaseManager.DoNextTaskOfWorkflowRejecting(TableId, CurrentUserAgentId, CurrentUserId, CurrentWFStateId, SelectedTaskId);
                            break;
                        case 0:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion             
                #endregion
                #region EmployeeRequestConfirming
                case (int)WorkFlows.EmployeeRequestConfirming:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectEmployeeChangingAndEndProcess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmingEmployeeChangingAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);

                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = EmployeeRequestManager.DoNextTaskOfConfirming(TableId, CurrentUserId);
                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = EmployeeRequestManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                            break;

                        case 0:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion

                #region MemberCardConfirming
                case (int)WorkFlows.MemberCardRequestConfirming:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectMemberCardAndEndProcess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmingMemberCardAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);

                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = MemberCardsManager.DoNextTaskOfConfirming(TableId, CurrentUserId);
                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = MemberCardsManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                            break;

                        case 0:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion

                #region TechnicianRequestConfirming
                case (int)WorkFlows.TechnicianRequestConfirming:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectTechnicianRequestChangingAndEndProcess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmingTechnicianRequestChangingAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);

                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = TechnicianRequestManager.DoNextTaskOfConfirming(TableId, CurrentUserId);
                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = TechnicianRequestManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                            break;

                        case 0:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion

                #region MemberTransferRequestConfirming
                case (int)WorkFlows.MemberTransferConfirming:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectMemberTransferAndEndProcess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmMemberTransferAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);

                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            int CurrentNmcId = FindNmcId(CurrentUserId);
                            if (CurrentNmcId > 0)
                            {
                                Per = MemberRequestManager.DoNextTaskOfMemberTransferConfirming(TableId, CurrentUserAgentId, CurrentUserId, CurrentNmcId);
                            }
                            else
                            {
                                Per = (int)ErrorWFNextStep.UserIsNotInNezamChart;
                            }
                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = MemberRequestManager.DoNextTaskOfMemberTransferRejecting(TableId, CurrentUserId);
                            break;

                        case 0:
                            Per = 0;
                            break;
                    }
                    if (Per == 0)
                        Per = MemberRequestManager.UpdateRequestTaskId(TableId, SelectedTaskId);
                    break;
                #endregion

                #region PeriodRegisterLicenceOutOfTime
                case (int)WorkFlows.PeriodRegisterLicenceOutOfTime:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectPeriodRegLicenceReqAndEndProcess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmingPeriodRegLicenceReqAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);

                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = PeriodRegisterManager.DoNextTaskOfConfirming(TableId, CurrentUserId);
                            break;

                        case (int)NextTaskType.RejectProccess:
                            Per = PeriodRegisterManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                            break;

                        case 0:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion

                #region Session
                case (int)WorkFlows.Session:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectingSession;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmingSession;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);
                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = SessionRequestsManager.DoNextTaskOfConfirming(TableId, CurrentUserId);
                            break;
                        case (int)NextTaskType.RejectProccess:
                            Per = SessionRequestsManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                            break;
                        default:
                            Per = 0;
                            break;
                    }
                    break;
                #endregion

                #region ExpertFile(27)
                case (int)WorkFlows.ExpertFileRequest:
                    RejectProccessTaskCode = (int)WorkFlowTask.RejectExpertFileRequestAndEndProcess;
                    ConfirmProccessTaskCode = (int)WorkFlowTask.ConfirmingExpertFileRequestAndEndProccess;

                    NextTask = GetNextTaskType(SelectedTaskId, RejectProccessTaskCode, ConfirmProccessTaskCode);
                    switch (NextTask)
                    {
                        case (int)NextTaskType.ConfirmProccess:
                            Per = ExpertFileRequestManager.DoNextTaskOfConfirming(TableId, CurrentUserId);
                            break;
                        case (int)NextTaskType.RejectProccess:
                            Per = ExpertFileRequestManager.DoNextTaskOfRejecting(TableId, CurrentUserId);
                            break;
                        default:
                            Per = 0;
                            break;
                    }
                    if (Per == 0)
                        Per = ExpertFileRequestManager.UpdateWfState(TableId, CurrentWFStateId);
                    break;
                    #endregion
            }

            return Per;
        }

        /// <summary>
        /// Check the condition of sending document to next step and do the next of sending doc to next step
        /// شرایط پرونده نسبت به کاربر جاری/عملیات انتخاب شده و.... را چک می کند و عملیات های مورد نیاز پس از انتخاب یک عملیات خاص را انجام می دهد
        ///Call in the SendDocToNextStep function in the WFUserControl
        /// </summary>
        /// <param name="WFCode"></param>
        /// <param name="TableId">Id of request table</param>
        /// <param name="SelectedTaskId"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="CurentUserUltId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <returns></returns>
        public int CheckAndDoSendDocToNextStepConditions(int WFCode, int TableId, int SelectedTaskId, int CurrentUserId, int CurentUserUltId, int CurrentUserAgentId, int CurrentCounId, ref ArrayList ArrayReturnValue, int CurrentTaskId)
        {
            return CheckAndDoSendDocToNextStepConditions(WFCode, TableId, SelectedTaskId, CurrentUserId, CurentUserUltId, CurrentUserAgentId, CurrentCounId, ref ArrayReturnValue, CurrentTaskId, -1);
        }
        /// <summary>
        /// Check the condition of sending document to next step and do the next of sending doc to next step
        /// شرایط پرونده نسبت به کاربر جاری/عملیات انتخاب شده و.... را چک می کند و عملیات های مورد نیاز پس از انتخاب یک عملیات خاص را انجام می دهد
        /// </summary>
        /// <param name="WFCode"></param>
        /// <param name="TableId">Id of request table</param>
        /// <param name="SelectedTaskId"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="CurentUserUltId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <returns></returns>
        public int CheckAndDoSendDocToNextStepConditions(int WFCode, int TableId, int SelectedTaskId, int CurrentUserId, int CurentUserUltId, int CurrentUserAgentId, int CurrentCounId, int CurrentTaskId)
        {
            ArrayList ArrayReturnValue = new ArrayList();
            return CheckAndDoSendDocToNextStepConditions(WFCode, TableId, SelectedTaskId, CurrentUserId, CurentUserUltId, CurrentUserAgentId, CurrentCounId, ref ArrayReturnValue, CurrentTaskId);
        }

        /// <summary>
        /// Check the condition of sending document to next step and do the next of sending doc to next step
        /// شرایط پرونده نسبت به کاربر جاری/عملیات انتخاب شده و.... را چک می کند و عملیات های مورد نیاز پس از انتخاب یک عملیات خاص را انجام می دهد
        /// </summary>
        /// <param name="WFCode"></param>
        /// <param name="TableId">Id of request table</param>
        /// <param name="SelectedTaskId"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="CurentUserUltId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <returns></returns>
        public int CheckAndDoSendDocToNextStepConditions(int WFCode, int TableId, int SelectedTaskId, int CurrentUserId, int CurentUserUltId, int CurrentUserAgentId, int CurrentCounId)
        {
            ArrayList ArrayReturnValue = new ArrayList();
            return CheckAndDoSendDocToNextStepConditions(WFCode, TableId, SelectedTaskId, CurrentUserId, CurentUserUltId, CurrentUserAgentId, CurrentCounId, ref ArrayReturnValue, -1);
        }
        #endregion
        /// <summary>
        /// زمان بازشدن پنجره گردش کار برای پرتا اعضا و پرتال اساتید و به صورت کلی برای پرتال هایی که کابران آن در چارت سازمانی وجود ندارند بایستی با استفاده از این تابع برای لیست مراحل گردش کار ، مرحله قبل/بعد مشخص شود.به عنوان مثال برای اساتید بعد از ثبت نمره مرحله بعد و قبل از طریق این تابع مشخص می گردد        
        /// </summary>
        /// <param name="WfCode"></param>
        /// <param name="CurrentTaskCode"></param>
        /// <param name="CurrentUserLoginType"></param>
        /// <param name="TableId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int GetSpecifiedSendBackTask(int WfCode, int CurrentTaskCode, int CurrentUserLoginType, int TableId, int CurrentUserId)
        {
            int SumSendBack = 0;

            switch (WfCode)
            {
                case (int)TSP.DataManager.WorkFlows.TSPlansConfirming:
                    #region TSPlansConfirming
                    if (CurrentUserLoginType == (int)UserType.Member)
                    {
                        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new WorkFlowTaskManager();
                        DataTable dtWfTask = WorkFlowTaskManager.SelectByWorkCode(WfCode);
                        dtWfTask.DefaultView.RowFilter = "TaskCode=-2";

                        if (CurrentTaskCode == (int)WorkFlowTask.SavePlanInfo)
                            dtWfTask.DefaultView.RowFilter = "TaskCode=" + ((int)TSP.DataManager.WorkFlowTask.ControlerConfirmingPlan).ToString();
                        else if (CurrentTaskCode == (int)WorkFlowTask.ControlerConfirmingPlan)
                        {
                            TSP.DataManager.TechnicalServices.Plans_ControlerManager PlansControlerManager = new TechnicalServices.Plans_ControlerManager();
                            DataTable dtPlansControler = PlansControlerManager.SelectTSPlansControlerByPlanInfoAndUserId(TableId, CurrentUserId, 0);
                            if (dtPlansControler.Rows.Count > 0)
                            {
                                dtWfTask.DefaultView.RowFilter = "TaskCode=" + ((int)TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess).ToString() + " OR "
                                                          + "TaskCode=" + ((int)TSP.DataManager.WorkFlowTask.SavePlanInfo).ToString();
                            }
                        }

                        for (int i = 0; i < dtWfTask.DefaultView.Count; i++)
                        {
                            int TCode = int.Parse(dtWfTask.DefaultView[i].Row["TCode"].ToString());

                            SumSendBack += TCode;
                        }
                    }
                    #endregion
                    break;
                case (int)TSP.DataManager.WorkFlows.PeriodConfirming:
                    #region PeriodConfirming
                    if (CurrentUserLoginType == (int)UserType.Teacher && CurrentTaskCode == (int)WorkFlowTask.PeriodSavePointsByTeachers)
                    {
                        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new WorkFlowTaskManager();
                        DataTable dtWfTask = WorkFlowTaskManager.SelectByWorkCode(WfCode);
                        dtWfTask.DefaultView.RowFilter = "TaskCode=" + ((int)TSP.DataManager.WorkFlowTask.PeriodSaveExamMinute).ToString() + " OR "
                                                  + "TaskCode=" + ((int)TSP.DataManager.WorkFlowTask.PeriodConfirmPointsByInstituteAndPrintPoint).ToString();
                        for (int i = 0; i < dtWfTask.DefaultView.Count; i++)
                        {
                            int TCode = int.Parse(dtWfTask.DefaultView[i].Row["TCode"].ToString());

                            SumSendBack += TCode;
                        }
                    }
                    #endregion
                    break;
                default:

                    break;
            }

            return SumSendBack;
        }

        #endregion

        #region Utility For Main Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SelectedTaskId"></param>
        /// <param name="RejectProccessTaskCode"></param>
        /// <param name="ConfirmProccessTaskCode"></param>
        /// <returns></returns>
        private int GetNextTaskType(int SelectedTaskId, int RejectProccessTaskCode, int ConfirmProccessTaskCode)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

            int ConfirmProccessTaskId = -1;
            int RejectProccessTaskId = -1;

            WorkFlowTaskManager.FindByTaskCode(ConfirmProccessTaskCode);
            if (WorkFlowTaskManager.Count == 1)
            {
                ConfirmProccessTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }

            WorkFlowTaskManager.FindByTaskCode(RejectProccessTaskCode);
            if (WorkFlowTaskManager.Count == 1)
            {
                RejectProccessTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }

            if (SelectedTaskId == ConfirmProccessTaskId)
            {
                return (int)NextTaskType.ConfirmProccess;
            }
            else if (SelectedTaskId == RejectProccessTaskId)
            {
                return (int)NextTaskType.RejectProccess;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TaskCode"></param>
        /// <returns>TaskId</returns>
        private int FindTaskId(int TaskCode)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count == 1)
            {
                return (Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]));
            }
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TaskCode"></param>
        /// <returns>TaskCode</returns>
        private int FindTaskCode(int TaskId)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

            WorkFlowTaskManager.FindByCode(TaskId);
            if (WorkFlowTaskManager.Count == 1)
            {
                return (Convert.ToInt32(WorkFlowTaskManager[0]["TaskCode"]));
            }
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TaskCode"></param>
        /// <returns>TaskOrder</returns>
        private int FindTaskOrder(int TaskId)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

            WorkFlowTaskManager.FindByCode(TaskId);
            if (WorkFlowTaskManager.Count == 1)
            {
                return (Convert.ToInt32(WorkFlowTaskManager[0]["TaskOrder"]));
            }
            return -1;
        }

        /// <summary>
        /// مشخص میکند که مرحله انتخاب شده قبل از مرحله جاری می باشد یا خیر
        /// </summary>
        /// <param name="TaskCode"></param>
        /// <returns>1 : Next , -1 : PreVious , 0 : The Same</returns>
        private int CheckTaskOrder(int CurrentTaskId, int SelectedTaskId)
        {
            int CurrentTaskOrder = FindTaskOrder(CurrentTaskId);
            int SelectedTaskOrder = FindTaskOrder(SelectedTaskId);
            if (SelectedTaskOrder > CurrentTaskOrder)
                return 1;
            else
                if (SelectedTaskOrder < CurrentTaskOrder)
                return -1;
            else return 0;
        }

        #endregion

        #region Permissions Methods
        /// <summary>
        /// Check WF Permission for editing data
        /// سطح دسترسی ویرایش اطلاعات پرونده (شامل اطلاعات اصلی و جزیی) را چک می کند
        /// </summary>
        /// <param name="EditingTaskCode">TaskCode of SaveInfo Task </param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="TableId"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="PageMode"></param>
        /// <returns>button's Enable</returns>
        public static WFPermission CheckPermissionForEdit(int EditingTaskCode, int WorkFlowCode, int TableId, int CurrentUserId, string PageMode)
        {
            TSP.DataManager.TaskDoerManager TaskDoerManager = new TaskDoerManager();
            WorkFlowTaskManager WorkFlowTaskManager = new WorkFlowTaskManager();
            WFPermission Per = new WFPermission();
            Per.BtnEdit = false;
            Per.BtnNew = false;
            Per.BtnSave = false;

            int TaskOrder = -1;
            WorkFlowTaskManager.FindByTaskCode(EditingTaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            }
            if (TaskOrder != 0)
            {

                int Permisssion = -1;
                Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WorkFlowCode, TableId, EditingTaskCode, CurrentUserId);
                if (Permisssion > 0)
                {
                    Per.BtnNew = true;

                    switch (PageMode)
                    {
                        case "Edit":
                            Per.BtnSave = true;
                            break;
                        case "View":
                            Per.BtnEdit = true;
                            break;
                        case "New":
                            Per.BtnSave = true;
                            break;

                        default:
                            Per.BtnSave = true;
                            Per.BtnNew = false;
                            break;
                    }


                }
            }
            return Per;
        }

        /// <summary>
        /// Check WF permission for Insert related Information Of Specific Document (Member's Jobhistory,Member's Licence,Member's Language,...)
        /// سطح دسترسی ویرایش اطلاعات پرونده (شامل اطلاعات اصلی و جزیی) را چک می کند
        /// </summary>
        /// <param name="EditingTaskCode"></param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="TableId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns>Button's Enable For Managements Pages</returns>
        public static WFPermission CheckWFPermissionForEditForManagementPage(int EditingTaskCode, int WorkFlowCode, int TableId, int CurrentUserId)
        {
            TSP.DataManager.TaskDoerManager TaskDoerManager = new TaskDoerManager();
            WorkFlowTaskManager WorkFlowTaskManager = new WorkFlowTaskManager();
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            WFPermission Per = new WFPermission();
            Per.BtnEdit = false;
            Per.BtnNew = false;
            Per.BtnSave = false;

            int TaskOrder = -1;
            WorkFlowTaskManager.FindByTaskCode(EditingTaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            }
            if (TaskOrder != 0)
            {
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WorkFlowCode, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    if (CurrentTaskCode == EditingTaskCode)
                    {
                        int Permisssion = -1;
                        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WorkFlowCode, TableId, EditingTaskCode, CurrentUserId);
                        if (Permisssion > 0)
                        {
                            Per.BtnNew = true;
                            Per.BtnEdit = true;
                            Per.BtnInactive = true;
                        }
                    }
                }
            }
            return Per;
        }

        /// <summary>
        ///سطح دسترسی تعریف  یک پرونده جدید ویا درخواست تغییرات جدید را چک میکند. از این تابع در صفحه اصلی مدیریت پرونده استفاده می شود
        /// </summary>
        /// <param name="SaveTaskCode"></param>
        /// <param name="TableType"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns>Button's Enable</returns>
        // CheckWFPermissionForNewChangeRequset
        public static WFPermission CheckWFPermissionForSaveNewForManagementPage(int SaveTaskCode, int TableType, int CurrentUserId)
        {
            TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
            WFPermission Per = new WFPermission();
            int Permission = -1;
            Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveTaskCode, CurrentUserId);
            if (Permission > 0)
            {
                Per.BtnNewRequest = true;
                Per.BtnNew = true;
            }
            return Per;
        }

        /// <summary>
        ///سطح دسترسی تعریف  یک پرونده جدید را چک میکند. از این تابع در صفحه اصلی مشخصات پرونده استفاده می شود
        /// </summary>
        /// <param name="SaveTaskCode"></param>
        /// <param name="TableType"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns>Button's Enable</returns>
        public static WFPermission CheckWFPermissionForSaveNew(int SaveTaskCode, int TableType, int CurrentUserId, string PageMode)
        {
            TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
            WFPermission Per = new WFPermission();
            int Permission = -1;
            Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveTaskCode, CurrentUserId);
            if (Permission > 0)
            {
                Per.BtnNewRequest = true;
                Per.BtnNew = true;

                switch (PageMode)
                {
                    case "New":
                        Per.BtnSave = true;

                        break;
                    case "View":
                        Per.BtnSave = false;
                        break;
                }
            }
            return Per;
        }

        public static Boolean CheckWFPermissionForDeleteRequest(int TableId, int WfCode, int TaskCode, int CurrentUserId, int CurrentNmcIdType)
        {
            // TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
            //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            // int TaskOrder = -1;

            //   WorkFlowTaskManager.FindByTaskCode(TaskCode);
            // if (WorkFlowTaskManager.Count > 0)
            // {
            //  TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            // if (TaskOrder != 0)
            // {
            DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
            dtState.DefaultView.RowFilter = "StateType=" + ((int)WorkFlowStateType.SendDocToNextStep).ToString();
            if (dtState.DefaultView.Count == 1)
            {
                int CurrentTaskCode = int.Parse(dtState.DefaultView[0]["TaskCode"].ToString());
                if (CurrentTaskCode == TaskCode)
                {
                    DataTable dtWorkFlowState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
                    if (dtWorkFlowState.Rows.Count > 0)
                    {
                        int FirstTaskCode = int.Parse(dtState.DefaultView[0]["TaskCode"].ToString());
                        int FirstUserId = int.Parse(dtState.DefaultView[0]["UserId"].ToString());
                        //int FirstNmcId = int.Parse(dtState.DefaultView[0]["NmcId"].ToString());
                        int FirstNmcIdType = int.Parse(dtState.DefaultView[0]["NmcIdType"].ToString());
                        if (FirstTaskCode == TaskCode)// && FirstUserId == CurrentUserId && FirstNmcIdType == CurrentNmcIdType)
                        {
                            return true;
                        }
                    }
                }
            }
            // }
            // }
            return false;
        }

        /// <summary>
        /// دسترسی ویرایش اطلاعات را براساس فرد ایجادکننده پرونده و مرحله جریان کار چک میکند.از این تابع هنگام کلیک دکمه ویرایش استفاده می شود
        /// </summary>
        /// <param name="TableId"></param>
        /// <param name="WfCode"></param>
        /// <param name="SaveInfoTaskCode"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="CurrentUserNmcIdType"></param>
        /// <returns></returns>
        public Boolean CheckPermissionForEditByUser(int TableId, int WfCode, int SaveInfoTaskCode, int CurrentUserId, int CurrentUserNmcIdType)
        {
            TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            WorkFlowStateManager.ClearBeforeFill = true;
            int TaskOrder = -1;
            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count == 0)
            {
                return false;
            }
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder == 0)
            {
                return false;
            }
            DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
            if (dtWorkFlowLastState.Rows.Count == 0)
            {
                return false;
            }
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());

            if (CurrentTaskCode != SaveInfoTaskCode)
            {
                return false;
            }
            DataTable dtWorkFlowState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
            if (dtWorkFlowState.Rows.Count == 0)
            {
                return false;
            }
            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
            if (FirstTaskCode != SaveInfoTaskCode)
            {
                return false;
            }
            if (CurrentNmcIdType != (int)WorkFlowStateNmcIdType.NmcId && FirstNmcIdType != CurrentNmcIdType)
            {
                return false;
            }
            int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, SaveInfoTaskCode, CurrentUserId);
            if (Permission > 0)
                return true;
            else
                return false;


        }

        /// <summary>
        /// دسترسی ارسال پرونده به مرحله بعد را براساس فرد ایجادکننده پرونده و مرحله جریان کار چک میکند.از این تابع هنگام کلیک دکمه گردش کار در پرتال های بجز کارمند استفاده می شود
        /// </summary>
        /// <param name="TableId"></param>
        /// <param name="WfCode"></param>
        /// <param name="SaveInfoTaskCode"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="CurrentUserNmcIdType"></param>
        /// <returns></returns>
        public Boolean CheckPermissionForSendDocToNextStepByUserForOtherPortals(int TableId, int WfCode, int SaveInfoTaskCode, int CurrentUserId, int CurrentUserNmcIdType)
        {
            TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            WorkFlowStateManager.ClearBeforeFill = true;
            int TaskOrder = -1;
            WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
            if (WorkFlowTaskManager.Count == 0)
            {
                return false;
            }
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder == 0)
            {
                return false;
            }
            DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
            if (dtWorkFlowLastState.Rows.Count == 0)
            {
                return false;
            }
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());

            if (CurrentTaskCode != SaveInfoTaskCode)
            {
                return false;
            }
            DataTable dtWorkFlowState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
            if (dtWorkFlowState.Rows.Count == 0)
            {
                return false;
            }
            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
            if (FirstTaskCode != SaveInfoTaskCode)
            {
                return false;
            }
            if (CurrentNmcIdType != (int)WorkFlowStateNmcIdType.NmcId && FirstNmcIdType != CurrentNmcIdType)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region UseFull Methods
        /// <summary>
        /// Checks if passed TaskCode is current Taskcode
        /// </summary>
        public bool CheckCurrentTaskCode(int TaskCode, int TableType, int ReqId)
        {
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, ReqId);
            if (dtWorkFlowState.Rows.Count > 0)
            {
                if (TaskCode == int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if passed TaskCode is current Taskcode
        /// </summary>
        public static bool CheckCurrentTaskCode_StaticFunc(int TaskCode, int TableType, int ReqId)
        {
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, ReqId);
            if (dtWorkFlowState.Rows.Count > 0)
            {
                if (TaskCode == int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString()))
                    return true;
            }
            return false;
        }

        public static int GetCurrentTaskCode_StaticFunc(int TableType, int ReqId)
        {
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastState(TableType, ReqId);
            if (dtWorkFlowState.Rows.Count > 0)
            {
                return int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
            }
            return -2;
        }

        public int GetNmcIdTypeByUserType(int UltId)
        {
            int NmcIdType = -1;
            switch (UltId)
            {
                case (int)UserType.Employee:
                    NmcIdType = (int)WorkFlowStateNmcIdType.NmcId;
                    break;
                case (int)UserType.Institute:
                    NmcIdType = (int)WorkFlowStateNmcIdType.NmcId;
                    break;
                case (int)UserType.Member:
                    NmcIdType = (int)WorkFlowStateNmcIdType.MeId;
                    break;
                case (int)UserType.Municipality:
                    NmcIdType = (int)WorkFlowStateNmcIdType.Munipulicity;
                    break;
                case (int)UserType.Office:
                    NmcIdType = (int)WorkFlowStateNmcIdType.OfficId;
                    break;
                case (int)UserType.TemporaryMembers:
                    NmcIdType = (int)WorkFlowStateNmcIdType.TempMember;
                    break;
                case (int)UserType.Settlement:
                    NmcIdType = (int)WorkFlowStateNmcIdType.NmcId;
                    break;
            }
            return NmcIdType;
        }

        public bool SetSMSControlsVisible(int CurrentTaskCode)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            WorkFlowTaskManager.FindByTaskCode(CurrentTaskCode);
            if (WorkFlowTaskManager.Count == 1)
            {
                if (Convert.ToBoolean(WorkFlowTaskManager[0]["IsSmsSend"]))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="WfCode"></param>
        /// <param name="TableId"></param>
        /// <param name="SelectedTaskId"></param>
        /// <param name="CurrentUserUltId"></param>
        /// <param name="AgentId">برای خدمات مهندسی نمایندگی پروژه می باشد</param>
        /// <returns></returns>
        public DataTable GetMemberInfoForSMSByWFCode(int WfCode, int TableId, int SelectedTaskId, int CurrentUserUltId)
        {
            DataTable dtMember = new DataTable();
            dtMember.Columns.Add("SMSMobileNo");
            dtMember.Columns.Add("SMSMeId");
            dtMember.Columns.Add("SMSUltId");
            dtMember.Columns.Add("ExtraInfo");
            dtMember.Columns.Add("SMSBody");

            try
            {
                switch (WfCode)
                {
                    case (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming:
                        DocMemberFileManager.FindByCode(TableId, 0);
                        if (DocMemberFileManager.Count == 1)
                        {
                            int MeId = Convert.ToInt32(DocMemberFileManager[0]["MeId"]);
                            MemberManager.FindByCode(MeId);
                            if (MemberManager.Count == 1)
                            {
                                DataRow dr = dtMember.NewRow();
                                dr["SMSMobileNo"] = MemberManager[0]["MobileNo"];
                                dr["SMSMeId"] = MeId;
                                dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Member;
                                dr["ExtraInfo"] = "کد عضویت " + MeId;
                                dr["SMSBody"] = "";
                                dtMember.Rows.Add(dr);
                                dtMember.AcceptChanges();
                            }
                        }
                        break;

                    case (int)TSP.DataManager.WorkFlows.TeachersConfirming:
                        TeacherManager.FindByCode(TableId);
                        if (TeacherManager.Count == 1)
                        {
                            int TeId = Convert.ToInt32(TeacherManager[0]["TeId"]);
                            DataRow dr = dtMember.NewRow();
                            dr["SMSMobileNo"] = TeacherManager[0]["MobileNo"];
                            dr["SMSMeId"] = TeId;
                            dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Teacher;
                            dr["ExtraInfo"] = "";
                            dr["SMSBody"] = "";
                            dtMember.Rows.Add(dr);
                            dtMember.AcceptChanges();
                        }
                        break;

                    case (int)TSP.DataManager.WorkFlows.InstitueConfirming:
                        InstitueManager InstitueManager = new InstitueManager();
                        InstitueManager.FindByCode(TableId);
                        if (InstitueManager.Count == 1)
                        {
                            int InsId = Convert.ToInt32(InstitueManager[0]["InsId"]);
                            DataRow dr = dtMember.NewRow();
                            dr["SMSMobileNo"] = InstitueManager[0]["MobileNo"];
                            dr["SMSMeId"] = InsId;
                            dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Institute;
                            dr["ExtraInfo"] = "";
                            dr["SMSBody"] = "";
                            dtMember.Rows.Add(dr);
                            dtMember.AcceptChanges();
                        }
                        break;

                    case (int)TSP.DataManager.WorkFlows.PeriodConfirming:
                        //  case (int)TSP.DataManager.WorkFlows.CancelPeriod:
                        InstitueManager InsPrManager = new InstitueManager();

                        PeriodPresentRequestManager.FindByCode(TableId);
                        if (PeriodPresentRequestManager.Count == 1)
                        {
                            int PPId = Convert.ToInt32(PeriodPresentRequestManager[0]["PPId"]);
                            int InsId = Convert.ToInt32(PeriodPresentRequestManager[0]["InsId"]);
                            InsPrManager.FindByCode(InsId);
                            DataRow dr = dtMember.NewRow();
                            dr["SMSMobileNo"] = InsPrManager[0]["MobileNo"];
                            dr["SMSMeId"] = PPId;
                            dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Institute;
                            dr["ExtraInfo"] = "";
                            dr["SMSBody"] = "";
                            dtMember.Rows.Add(dr);
                            dtMember.AcceptChanges();
                        }
                        break;

                    case (int)TSP.DataManager.WorkFlows.SeminarConfirming:
                        SeminarManager SeminarManager = new DataManager.SeminarManager();
                        InstitueManager InsSeManager = new InstitueManager();

                        SeminarManager.FindByCode(TableId);
                        if (SeminarManager.Count == 1)
                        {
                            int SeId = Convert.ToInt32(SeminarManager[0]["SeId"]);
                            int InsId = Convert.ToInt32(SeminarManager[0]["InsId"]);
                            InsSeManager.FindByCode(InsId);

                            DataRow dr = dtMember.NewRow();
                            dr["SMSMobileNo"] = InsSeManager[0]["MobileNo"];
                            dr["SMSMeId"] = SeId;
                            dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Institute;
                            dr["ExtraInfo"] = "";
                            dr["SMSBody"] = "";
                            dtMember.Rows.Add(dr);
                            dtMember.AcceptChanges();
                        }
                        break;

                    case (int)TSP.DataManager.WorkFlows.MemberConfirming:
                        MemberRequestManager.FindByCode(TableId);
                        if (MemberRequestManager.Count == 1)
                        {
                            int MeId = Convert.ToInt32(MemberRequestManager[0]["MeId"]);
                            DataRow dr = dtMember.NewRow();
                            dr["SMSMobileNo"] = MemberRequestManager[0]["MobileNo"];
                            dr["SMSMeId"] = MeId;
                            dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Member;
                            dr["ExtraInfo"] = "کد عضویت " + MeId;
                            dr["SMSBody"] = "";
                            dtMember.Rows.Add(dr);
                            dtMember.AcceptChanges();
                        }
                        break;

                    case (int)TSP.DataManager.WorkFlows.OfficeConfirming:
                    case (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming:
                        OfficeRequestManager.FindByCode(TableId);
                        if (OfficeRequestManager.Count == 1)
                        {
                            int OfId = Convert.ToInt32(OfficeRequestManager[0]["OfId"]);
                            DataRow dr = dtMember.NewRow();
                            dr["SMSMobileNo"] = OfficeRequestManager[0]["MobileNo"];
                            dr["SMSMeId"] = OfId;
                            dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Office;
                            dr["ExtraInfo"] = "کد شرکت " + OfId;
                            dr["SMSBody"] = "";
                            dtMember.Rows.Add(dr);
                            dtMember.AcceptChanges();
                        }
                        break;

                    case (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming:
                        DocMemberFileManager.SelectImplementDoc(-1, TableId);
                        if (DocMemberFileManager.Count == 1)
                        {
                            int MeId = Convert.ToInt32(DocMemberFileManager[0]["MeId"]);
                            MemberManager.FindByCode(MeId);
                            if (MemberManager.Count == 1)
                            {
                                DataRow dr = dtMember.NewRow();
                                dr["SMSMobileNo"] = MemberManager[0]["MobileNo"];
                                dr["SMSMeId"] = MeId;
                                dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Member;
                                dr["ExtraInfo"] = "";
                                dr["SMSBody"] = "";
                                dtMember.Rows.Add(dr);
                                dtMember.AcceptChanges();
                            }
                        }
                        break;

                    case (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming:
                        DocMemberFileManager.SelectObservationDoc(-1, TableId);
                        if (DocMemberFileManager.Count == 1)
                        {
                            int MeId = Convert.ToInt32(DocMemberFileManager[0]["MeId"]);
                            MemberManager.FindByCode(MeId);
                            if (MemberManager.Count == 1)
                            {
                                DataRow dr = dtMember.NewRow();
                                dr["SMSMobileNo"] = MemberManager[0]["MobileNo"];
                                dr["SMSMeId"] = MeId;
                                dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Member;
                                dr["ExtraInfo"] = "";
                                dr["SMSBody"] = "";
                                dtMember.Rows.Add(dr);
                                dtMember.AcceptChanges();
                            }
                        }
                        break;
                    case (int)TSP.DataManager.WorkFlows.TSObserverChangesConfirming:
                        ObserverWorkRequestChangesManager.FindByObsWorkReqChangeId(TableId);
                        if (ObserverWorkRequestChangesManager.Count == 1)
                        {
                            int MeId = Convert.ToInt32(ObserverWorkRequestChangesManager[0]["MeId"]);
                            MemberManager.FindByCode(MeId);
                            if (MemberManager.Count == 1)
                            {
                                DataRow dr = dtMember.NewRow();
                                dr["SMSMobileNo"] = MemberManager[0]["MobileNo"];
                                dr["SMSMeId"] = MeId;
                                dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Member;
                                dr["ExtraInfo"] = "";
                                dr["SMSBody"] = "";
                                dtMember.Rows.Add(dr);
                                dtMember.AcceptChanges();
                            }
                        }
                        break;
                    case (int)TSP.DataManager.WorkFlows.EngOfficeConfirming:
                        EngOffFileManager.FindByCode(TableId);
                        if (EngOffFileManager.Count == 1)
                        {
                            int EngOfId = Convert.ToInt32(EngOffFileManager[0]["EngOfId"]);
                            DataRow dr = dtMember.NewRow();
                            dr["SMSMobileNo"] = EngOffFileManager[0]["MobileNo"];
                            dr["SMSMeId"] = EngOfId;
                            dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.EngOffcie;
                            dr["ExtraInfo"] = "کد دفتر " + EngOfId;
                            dtMember.Rows.Add(dr);
                            dr["SMSBody"] = "";
                            dtMember.AcceptChanges();
                        }
                        break;

                    case (int)TSP.DataManager.WorkFlows.TSPlansConfirming:

                        switch (SelectedTaskId)
                        {
                            case (int)TSP.DataManager.WorkFlowTask.ControlerConfirmingPlan:
                                #region
                                TechnicalServices.Plans_ControlerManager PlansControlerManager = new TechnicalServices.Plans_ControlerManager();
                                PlansControlerManager.FindActiveControlerByPlansId(TableId);

                                for (int i = 0; i < PlansControlerManager.Count; i++)
                                {

                                    DataRow dr = dtMember.NewRow();
                                    dr["SMSMobileNo"] = PlansControlerManager[i]["MobileNo"];
                                    dr["SMSMeId"] = Convert.ToInt32(PlansControlerManager[i]["MeId"]);
                                    dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Member;
                                    switch (Convert.ToInt32(PlansControlerManager[i]["PlansTypeId"]))
                                    {
                                        case (int)TSPlansType.Memari:
                                            dr["ExtraInfo"] = "نقشه معماری با کد پروژه" + PlansControlerManager[i]["ProjectId"].ToString();
                                            break;

                                        case (int)TSPlansType.Sazeh:
                                            dr["ExtraInfo"] = "نقشه سازه با کد پروژه" + PlansControlerManager[i]["ProjectId"].ToString();
                                            break;

                                        case (int)TSPlansType.Shahrsazi:
                                            dr["ExtraInfo"] = "نقشه شهرسازی با کد پروژه" + PlansControlerManager[i]["ProjectId"].ToString();
                                            break;

                                        case (int)TSPlansType.TasisatBargh:
                                            dr["ExtraInfo"] = "نقشه تاسیسات برق با کد پروژه" + PlansControlerManager[i]["ProjectId"].ToString();
                                            break;

                                        case (int)TSPlansType.TasisatMechanic:
                                            dr["ExtraInfo"] = "نقشه تاسیسات مکانیک با کد پروژه" + PlansControlerManager[i]["ProjectId"].ToString();
                                            break;

                                    }
                                    if (CurrentUserUltId == (int)TSP.DataManager.UserType.Employee)
                                        dr["SMSBody"] = "بازبین محترم؛" + dr["ExtraInfo"].ToString() + " به منظور بازبینی در کارتابل شما قرار دارد. خواهشمند است در اسرع وقت با مراجعه به لینک زیر بررسی و اظهار نظر فرمایید." + "\n" + "https://fceo.ir/login.aspx";
                                    else if (CurrentUserUltId == (int)TSP.DataManager.UserType.Member)
                                        dr["SMSBody"] = "بازبین محترم؛اصلاحیه " + dr["ExtraInfo"].ToString() + " به منظور بازبینی مجدد در کارتابل شما قرار دارد. خواهشمند است در اسرع وقت بررسی و اظهار نظر فرمایید.";

                                    dtMember.Rows.Add(dr);
                                    dtMember.AcceptChanges();
                                }

                                #endregion
                                break;
                            case (int)TSP.DataManager.WorkFlowTask.SavePlanInfo:
                                TechnicalServices.Designer_PlansManager Designer_PlansManager = new TechnicalServices.Designer_PlansManager();
                                Designer_PlansManager.FindActivesByPlansId(TableId);
                                if (Designer_PlansManager.Count > 0)
                                {
                                    DataRow dr = dtMember.NewRow();
                                    dr["SMSMobileNo"] = Designer_PlansManager[0]["MobileNo"];
                                    dr["SMSMeId"] = Convert.ToInt32(Designer_PlansManager[0]["MeId"]);
                                    dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Member;
                                    switch (Convert.ToInt32(Designer_PlansManager[0]["PlansTypeId"]))
                                    {
                                        case (int)TSPlansType.Memari:
                                            dr["ExtraInfo"] = "نقشه معماری با کد پروژه" + Designer_PlansManager[0]["ProjectId"].ToString();
                                            break;

                                        case (int)TSPlansType.Sazeh:
                                            dr["ExtraInfo"] = "نقشه سازه با کد پروژه" + Designer_PlansManager[0]["ProjectId"].ToString();
                                            break;

                                        case (int)TSPlansType.Shahrsazi:
                                            dr["ExtraInfo"] = "نقشه شهرسازی با کد پروژه" + Designer_PlansManager[0]["ProjectId"].ToString();
                                            break;

                                        case (int)TSPlansType.TasisatBargh:
                                            dr["ExtraInfo"] = "نقشه تاسیسات برق با کد پروژه" + Designer_PlansManager[0]["ProjectId"].ToString();
                                            break;

                                        case (int)TSPlansType.TasisatMechanic:
                                            dr["ExtraInfo"] = "نقشه تاسیسات مکانیک با کد پروژه" + Designer_PlansManager[0]["ProjectId"].ToString();
                                            break;

                                    }
                                    dr["SMSBody"] = "طراح محترم؛" + dr["ExtraInfo"].ToString() + "  که توسط بازبین محترم بررسی گردیده به منظور اصلاح در کارتابل شما قرار دارد. خواهشمند است در اسرع وقت با مراجعه به پرتال خود بررسی و اقدام فرمایید.";
                                    dtMember.Rows.Add(dr);
                                    dtMember.AcceptChanges();
                                }
                                break;

                            case (int)TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess:
                                TechnicalServices.Designer_PlansManager DesignerPlansManager = new TechnicalServices.Designer_PlansManager();
                                DesignerPlansManager.FindActivesByPlansId(TableId);

                                for (int i = 0; i < DesignerPlansManager.Count; i++)
                                {
                                    DataRow dr = dtMember.NewRow();
                                    dr["SMSMobileNo"] = DesignerPlansManager[i]["MobileNo"];
                                    dr["SMSMeId"] = Convert.ToInt32(DesignerPlansManager[i]["MeId"]);
                                    dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Member;
                                    switch (Convert.ToInt32(DesignerPlansManager[i]["PlansTypeId"]))
                                    {
                                        case (int)TSPlansType.Memari:
                                            dr["ExtraInfo"] = "نقشه معماری با کد پروژه" + DesignerPlansManager[i]["ProjectId"].ToString();
                                            break;

                                        case (int)TSPlansType.Sazeh:
                                            dr["ExtraInfo"] = "نقشه سازه با کد پروژه" + DesignerPlansManager[i]["ProjectId"].ToString();
                                            break;

                                        case (int)TSPlansType.Shahrsazi:
                                            dr["ExtraInfo"] = "نقشه شهرسازی با کد پروژه" + DesignerPlansManager[i]["ProjectId"].ToString();
                                            break;

                                        case (int)TSPlansType.TasisatBargh:
                                            dr["ExtraInfo"] = "نقشه تاسیسات برق با کد پروژه" + DesignerPlansManager[i]["ProjectId"].ToString();
                                            break;

                                        case (int)TSPlansType.TasisatMechanic:
                                            dr["ExtraInfo"] = "نقشه تاسیسات مکانیک با کد پروژه" + DesignerPlansManager[i]["ProjectId"].ToString();
                                            break;

                                    }
                                    dr["SMSBody"] = "طراح محترم؛" + dr["ExtraInfo"].ToString() + " توسط بازبین محترم بررسی و مورد تایید قرار گرفت.";
                                    dtMember.Rows.Add(dr);
                                    dtMember.AcceptChanges();
                                }
                                break;
                        }

                        break;

                    case (int)TSP.DataManager.WorkFlows.TSProjectConfirming:
                        ProjectRequestManager.FindByCode(TableId);
                        if (ProjectRequestManager.Count == 1)
                        {
                            TechnicalServices.OwnerManager OwnerManager = new TechnicalServices.OwnerManager();
                            int ProjectId = Convert.ToInt32(ProjectRequestManager[0]["ProjectId"]);
                            string TraceCode = ProjectRequestManager[0]["TraceCode"].ToString();
                            OwnerManager.FindOwnerAgent(ProjectId);
                            int OwnerId = Convert.ToInt32(OwnerManager[0]["OwnerId"]);

                            DataRow dr = dtMember.NewRow();
                            dr["SMSMobileNo"] = OwnerManager[0]["MobileNo"];
                            dr["SMSMeId"] = OwnerId;
                            dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.OtherPerson;
                            dr["ExtraInfo"] = "";
                            dr["SMSBody"] = "";
                            switch (SelectedTaskId)
                            {
                                case (int)WorkFlowTask.SaveStructurePlanOfProject:
                                    if (Convert.ToInt32(ProjectRequestManager[0]["AgentId"]) == Utility.GetCurrentAgentCode())
                                        dr["SMSBody"] = "";
                                    else
                                        dr["SMSBody"] = "مالک محترم " + OwnerManager[0]["Name"].ToString() + " باتوجه به تایید نقشه معماری پروژه شما با کد " + ProjectId.ToString() + " به منظور اقدامات بعدی به طراح سازه پروژه مراجعه فرمایید." + "\n" + "شناسه دسترسی طراح" + TraceCode;

                                    break;
                                case (int)WorkFlowTask.SaveMechanicInstallationPlanOfProject:
                                    if (Convert.ToInt32(ProjectRequestManager[0]["AgentId"]) == Utility.GetCurrentAgentCode())
                                        dr["SMSBody"] = "";
                                    else
                                        dr["SMSBody"] = "مالک محترم " + OwnerManager[0]["Name"].ToString() + " باتوجه به تایید نقشه معماری پروژه شما با کد " + ProjectId.ToString() + " به منظور اقدامات بعدی به طراح تاسیسات پروژه مراجعه فرمایید." + "\n" + "شناسه دسترسی طراح" + TraceCode;

                                    break;
                                case (int)WorkFlowTask.SaveElectricalInstalationPlanOfProject:
                                    if (Convert.ToInt32(ProjectRequestManager[0]["AgentId"]) == Utility.GetCurrentAgentCode())
                                        dr["SMSBody"] = "";
                                    else
                                        dr["SMSBody"] = "مالک محترم " + OwnerManager[0]["Name"].ToString() + " باتوجه به تایید نقشه معماری پروژه شما با کد " + ProjectId.ToString() + " به منظور اقدامات بعدی به طراح تاسیسات پروژه مراجعه فرمایید." + "\n" + "شناسه دسترسی طراح" + TraceCode;
                                    break;
                            }

                            dtMember.Rows.Add(dr);
                            dtMember.AcceptChanges();
                        }
                        break;
                    case (int)TSP.DataManager.WorkFlows.TSEndStructuralProjectLicenceConfirming:
                        ProjectRequestManager.FindByCode(TableId);
                        if (ProjectRequestManager.Count == 1)
                        {
                            TechnicalServices.OwnerManager OwnerManager = new TechnicalServices.OwnerManager();
                            int ProjectId = Convert.ToInt32(ProjectRequestManager[0]["ProjectId"]);
                            OwnerManager.FindOwnerAgent(ProjectId);
                            int OwnerId = Convert.ToInt32(OwnerManager[0]["OwnerId"]);

                            DataRow dr = dtMember.NewRow();
                            dr["SMSMobileNo"] = OwnerManager[0]["MobileNo"];
                            dr["SMSMeId"] = OwnerId;
                            dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.OtherPerson;
                            dr["ExtraInfo"] = "";
                            dr["SMSBody"] = "";

                            dtMember.Rows.Add(dr);
                            dtMember.AcceptChanges();
                        }
                        break;
                    case (int)TSP.DataManager.WorkFlows.TSBuildingsLicenseConfirming:
                        ProjectRequestManager.FindByCode(TableId);
                        if (ProjectRequestManager.Count == 1)
                        {
                            TechnicalServices.OwnerManager OwnerManager = new TechnicalServices.OwnerManager();
                            int ProjectId = Convert.ToInt32(ProjectRequestManager[0]["ProjectId"]);
                            OwnerManager.FindOwnerAgent(ProjectId);
                            int OwnerId = Convert.ToInt32(OwnerManager[0]["OwnerId"]);

                            DataRow dr = dtMember.NewRow();
                            dr["SMSMobileNo"] = OwnerManager[0]["MobileNo"];
                            dr["SMSMeId"] = OwnerId;
                            dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.OtherPerson;
                            dr["ExtraInfo"] = "";
                            dr["SMSBody"] = "";

                            dtMember.Rows.Add(dr);
                            dtMember.AcceptChanges();
                        }
                        break;
                    case (int)TSP.DataManager.WorkFlows.TSPlanMethodsChangesConfirming:
                        ProjectRequestManager.FindByCode(TableId);
                        if (ProjectRequestManager.Count == 1)
                        {
                            TechnicalServices.OwnerManager OwnerManager = new TechnicalServices.OwnerManager();
                            int ProjectId = Convert.ToInt32(ProjectRequestManager[0]["ProjectId"]);
                            OwnerManager.FindOwnerAgent(ProjectId);
                            int OwnerId = Convert.ToInt32(OwnerManager[0]["OwnerId"]);

                            DataRow dr = dtMember.NewRow();
                            dr["SMSMobileNo"] = OwnerManager[0]["MobileNo"];
                            dr["SMSMeId"] = OwnerId;
                            dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.OtherPerson;
                            dr["ExtraInfo"] = "";
                            dr["SMSBody"] = "";

                            dtMember.Rows.Add(dr);
                            dtMember.AcceptChanges();
                        }
                        break;
                    case (int)TSP.DataManager.WorkFlows.MemberCardRequestConfirming:
                        MemberCardsManager.FindByCode(TableId);
                        if (MemberCardsManager.Count == 1)
                        {
                            int MeId = Convert.ToInt32(MemberCardsManager[0]["MeId"]);
                            DataRow dr = dtMember.NewRow();
                            dr["SMSMobileNo"] = MemberCardsManager[0]["MobileNo"];
                            dr["SMSMeId"] = MeId;
                            dr["SMSUltId"] = (int)SmsRecieverManager.RecieverTypes.Member;
                            dr["ExtraInfo"] = "کد عضویت " + MeId;
                            dtMember.Rows.Add(dr);
                            dtMember.AcceptChanges();
                        }
                        break;
                }
                return dtMember;
            }
            catch
            {
                return dtMember;
            }
        }
        #endregion
    }

    public interface WorkFlowMethods
    {
        int DoNextTaskOfWFConfirming(int TableId, int CurrentUserAgentId, int CurrentUserId, Int64 CurrentWFStateId, int SelectedTaskId);
        int DoNextTaskOfMeObsDocRejecting(int TableId, int CurrentUserId, Int64 CurrentWFStateId, int SelectedTaskId);
        int UpdateWFStateId(int TableId, int CurrentWFStateId);
    }
}
