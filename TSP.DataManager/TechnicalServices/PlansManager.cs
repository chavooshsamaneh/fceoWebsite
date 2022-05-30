using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TSP.DataManager.TechnicalServices
{
    public class PlansManager : BaseObject
    {
        public struct DesignerInfo
        {
            public int Wage;
            public int PricaArchiveId;
            public string PricaArchiveName;
        }
        #region Private Members
        ProjectRequestManager ProjectRequestManager;
        #endregion

        #region Constructors
        public PlansManager()
            : base()
        {

        }

        public PlansManager(TSP.DataManager.TransactionManager Transact)
        {
            ProjectRequestManager = new ProjectRequestManager();
            if (Transact != null)
            {
                Transact.Add(ProjectRequestManager);
            }
        }

        #endregion

        #region WF Mehtods
        public int UpdateRequestTaskId(int PlanId, int TaskId, Int64 CurrentWFStateId)
        {
            int Per = 0;
            this.FindByPlansId(PlanId);
            if (this.Count <= 0)
                return (int)ErrorRequest.RequestIsConfirmed;
            this[0].BeginEdit();
            this[0]["currentTaskId"] = TaskId;
            this[0]["CurrentStateId"] = CurrentWFStateId;
            this[0].EndEdit();
            this.Save();
            this.DataTable.AcceptChanges();
            return Per;
        }
        #region SendBackTask

        public int CheckPermissionPlansConfirmingSendBackTask(int PlansId, int CurrentTaskCode, int CurrentUserId)
        {
            int Per = 0;
            this.FindByPlansId(PlansId);
            if (this.Count != 1)
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            int ProjectId = (int)this[0]["ProjectId"];
            int PlansTypeId = (int)this[0]["PlansTypeId"];
            int PrjReqId = (int)this[0]["PrjReId"];
            switch (CurrentTaskCode)
            {
                case (int)TSP.DataManager.WorkFlowTask.SavePlanInfo:
                    //Per = CheckDesigner(PlansId, ProjectId);
                    //if (Per == 0)
                    //    Per = Check5PercentFiche(PlansId, ProjectId, PlansTypeId, PrjReqId);
                    DataManager.LoginManager LoginManager = new DataManager.LoginManager();

                    LoginManager.FindByCode(CurrentUserId);
                    if (LoginManager.Count > 0)
                    {
                        int MeId = int.Parse(LoginManager[0]["MeId"].ToString());
                        Plans_ControlerManager PlansControlerManager = new Plans_ControlerManager();
                        PlansControlerManager.FindPlanOfControler(MeId, PlansId);
                        if (PlansControlerManager.Count > 0)
                            Per = (int)ErrorRequest.YouCanNotSentDocToNextStep;
                    }
                    break;

                case (int)TSP.DataManager.WorkFlowTask.AssignControlerToPlan:
                    //if (Convert.ToInt32(this[0]["IsDesAccepted"]) != (int)TSDesignerAcceptance.ConfirmedWithoutSaveController)
                    //    Per = CheckController(PlansId);
                    break;

                case (int)TSP.DataManager.WorkFlowTask.TSEmployeeConfirmDesigners:
                    break;

                case (int)TSP.DataManager.WorkFlowTask.FishPaymentByMember:
                    Per = ProjectRequestManager.CheckArchitecturePlanCondition(ProjectId, PrjReqId);
                    break;

                case (int)TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess:
                    Per = (int)ErrorRequest.RequestIsConfirmed;
                    break;

                case (int)TSP.DataManager.WorkFlowTask.RejectPlanAndEndProcess:
                    Per = (int)ErrorRequest.RequestIsConfirmed;
                    break;
            }

            return Per;
        }

        public int CheckDesigner(int PlansId, int ProjectId)
        {
            Designer_PlansManager DesignerPlansManager = new Designer_PlansManager();
            ProjectManager ProjectManager = new ProjectManager();

            int Per = 0;
            ProjectManager.FindByProjectId(ProjectId);
            if (!((Convert.ToInt32(ProjectManager[0]["GroupId"]) == (int)TSStructureGroups.A || Convert.ToInt32(ProjectManager[0]["GroupId"]) == (int)TSStructureGroups.B) && ProjectManager.CheckIfBrickSkeleton(ProjectId)))
            {
                DesignerPlansManager.FindActivesByPlansId(PlansId);
                if (DesignerPlansManager.Count == 0)
                    Per = (int)ErrorRequest.NoDesigner;
            }

            return Per;
        }

        public int CheckIfAllDesingerForPlanInserted(int PlansId, int PrjReId)
        {
            int Per = 0;
            int SumDecreament = 0;
            int Foundation;
            int FoundationMixSkeletonSaze = 0;
            int DecrementPercent = 100;
            ProjectRequestManager ProjectRequestManager = new TechnicalServices.ProjectRequestManager();
            ProjectRequestManager.FindByCode(PrjReId);
            if (ProjectRequestManager.Count != 1)
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            DecrementPercent = Convert.ToInt32(ProjectRequestManager[0]["DecrementPercent"]);
            Foundation =Convert.ToInt32( Math.Round(Convert.ToDouble(ProjectRequestManager[0]["Foundation"]) * DecrementPercent / 100, MidpointRounding.AwayFromZero));
            if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["FoundationMixSkeleton"]))
                FoundationMixSkeletonSaze = Convert.ToInt32(ProjectRequestManager[0]["FoundationMixSkeleton"]);
            int GroupId = Convert.ToInt32(ProjectRequestManager[0]["GroupId"]);
            int StructureSkeletonId = Convert.ToInt32(ProjectRequestManager[0]["StructureSkeletonId"]);
            int ProjectId = Convert.ToInt32(ProjectRequestManager[0]["ProjectId"]);
            Designer_PlansManager DesignerPlansManager = new Designer_PlansManager();
            DataTable dtDesinger = DesignerPlansManager.SelectActiveTSDesignerPlansForByPlanId(PlansId);
            int PlansTypeId = -1;
            for (int i = 0; i < dtDesinger.Rows.Count; i++)
            {
                SumDecreament += Convert.ToInt32(dtDesinger.Rows[i]["CapacityDecrement"]);
                PlansTypeId = Convert.ToInt32(dtDesinger.Rows[i]["PlansTypeId"]);
            }
            ///////////////
            int FundationDifference = 0;
            DataTable dtPreRrjRest = ProjectRequestManager.SelectPreviousProjectRequestStageAndFoundation(ProjectId, PrjReId);
            if (dtPreRrjRest.Rows.Count == 1)
            {
                FundationDifference = Foundation - Convert.ToInt32( Math.Round(Convert.ToDouble(dtPreRrjRest.Rows[0]["Foundation"]) * DecrementPercent / 100, MidpointRounding.AwayFromZero));
                //Math.Round(Convert.ToInt32(ProjectRequestManager[0]["Foundation"]) * DecrementPercent / 100, MidpointRounding.AwayFromZero)
            }
            //////////////
            if (GroupId != (int)TSStructureGroups.A && (PlansTypeId == (int)TSPlansType.TasisatBargh || PlansTypeId == (int)TSPlansType.TasisatMechanic))
            {
                if (SumDecreament != (FundationDifference > 0 ? FundationDifference / 2 : Foundation / 2) 
                  && SumDecreament != (FundationDifference > 0 ? Math.Round(FundationDifference / 2.0, MidpointRounding.AwayFromZero) : Math.Round(Foundation / 2.0, MidpointRounding.AwayFromZero))
                  && SumDecreament != (FundationDifference > 0 ? Math.Round(FundationDifference / 2.0, MidpointRounding.ToEven) : Math.Round(Foundation / 2.0, MidpointRounding.ToEven)))
                {
                    Per = (int)ErrorRequest.NotAllDesignerInsertedForPlan;
                }
            }
            else if ((GroupId == (int)TSP.DataManager.TSStructureGroups.A || GroupId == (int)TSP.DataManager.TSStructureGroups.B) && PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh && StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Mix)
            {
                if (SumDecreament != FoundationMixSkeletonSaze)
                {
                    Per = (int)ErrorRequest.NotAllDesignerInsertedForPlan;
                }
            }
            else
            {
                if (SumDecreament != (FundationDifference > 0 ? FundationDifference : Foundation))
                {
                    Per = (int)ErrorRequest.NotAllDesignerInsertedForPlan;
                }

            }

            return Per;
        }

        /// <summary>
        /// Used in PrjWF & PlanWF
        /// </summary>
        /// <param name="PlansId"></param>
        /// <param name="ProjectId"></param>
        /// <param name="PlansTypeId"></param>
        /// <param name="PrjReId"></param>
        /// <returns></returns>
        public int Check5PercentFiche(int PlansId, int ProjectId, int PlansTypeId, int PrjReId)
        {
            int Per = 0;
            ProjectRequestManager ProjectRequestManager = new TechnicalServices.ProjectRequestManager();
            ProjectRequestManager.FindByCode(PrjReId);
            if (ProjectRequestManager.Count != 1)
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            if (!Convert.ToBoolean(ProjectRequestManager[0]["DesignerSaved"]))
                return Per;
            Designer_PlansManager DesignerPlansManager = new Designer_PlansManager();

            DataTable dtDesignerPlansManager = DesignerPlansManager.SelectProjectDesignerByPlansId(PlansId);
            if (dtDesignerPlansManager.Rows.Count <= 0)
                return (int)ErrorRequest.NoPlansMasterDesigner;
            int AccType = -2;
            switch (PlansTypeId)
            {
                case (int)TSPlansType.Memari:
                    AccType = (int)TSP.DataManager.TSAccountingAccType.Designing5Percent;
                    break;
                case (int)TSPlansType.Sazeh:
                    AccType = (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure;
                    break;
                case (int)TSPlansType.TasisatBargh:
                case (int)TSPlansType.TasisatMechanic:
                    AccType = (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation;
                    break;
            }
            for (int i = 0; i < dtDesignerPlansManager.Rows.Count; i++)
            {
                decimal Amount = 0;
                AccountingManager AccountingManager = new AccountingManager();

                AccountingManager.FindByTableTypeIdAndAccType(Convert.ToInt32(dtDesignerPlansManager.Rows[i]["PrjDesignerId"]),
                    (int)TSP.DataManager.TableCodes.TSProject_Designer, AccType);
                if (AccountingManager.Count == 0)
                    return (int)ErrorRequest.No5PercentFiche;

                for (int j = 0; j < AccountingManager.Count; j++)
                {
                    if (Convert.ToInt32(AccountingManager[j]["Status"]) != (int)TSAccountingStatus.Payment)
                        return (int)ErrorRequest.NoPay5PercentFiche;
                    Amount += Convert.ToDecimal(AccountingManager[j]["Amount"]);
                }

                decimal DesPrice;
                int DesignerPlansId = Convert.ToInt32(dtDesignerPlansManager.Rows[i]["DesignerPlansId"]);
                DesPrice = this.Get5PercentPriceByStep(ProjectId, PlansTypeId, PrjReId, DesignerPlansId);

                if (Amount.ToString("#,#") != DesPrice.ToString("#,#") && Convert.ToInt32(Amount) != Convert.ToInt32(DesPrice))
                    return (int)ErrorRequest.AmountOf5PercentFicheNotMatch;
            }


            return Per;
        }

        /// <summary>
        /// محاسبه هزینه پنج درصد طراحی بر اساس تعداد طبقات پروژه
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PlansTypeId"></param>
        /// <param name="PrjReId"></param>
        /// <param name="DesignerPlansId"></param>
        /// <returns></returns>
        public decimal Get5PercentPriceByStep(int ProjectId, int PlansTypeId, int PrjReId, int DesignerPlansId, ref DesignerInfo DesgInfo)
        {
            PriceArchiveManager PriceArchiveManager = new PriceArchiveManager();
            PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new PriceArchiveStructureItemsManager();
            PriceArchiveStructureItemDetailManager PriceArchiveStructureItemDetailManager = new PriceArchiveStructureItemDetailManager();
            PriceArchiveStructureItemDetailTypeManager PriceArchiveStructureItemDetailTypeManager = new PriceArchiveStructureItemDetailTypeManager();
            if (ProjectRequestManager == null)
                ProjectRequestManager = new ProjectRequestManager();

            ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new ProjectCapacityDecrementManager();
            Designer_PlansManager DesignerPlansManager = new Designer_PlansManager();

            decimal Price = 0;
            ProjectRequestManager.FindByCode(PrjReId);
            if (ProjectRequestManager.Count > 0)
            {
                double Foundation = 0;
                int GroupId = Convert.ToInt32(ProjectRequestManager[0]["GroupId"]);
                int Step = Convert.ToInt32(ProjectRequestManager[0]["MaxStageNum"]);
                #region نوع اسکلت
                int StructureSkeletonId = -1;
                //*****

                if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["StructureSkeletonId"]))
                    StructureSkeletonId = Convert.ToInt32(ProjectRequestManager[0]["StructureSkeletonId"]);

                //*****
                if (GroupId == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonId == -1)
                {
                    return 0;
                }
                if (GroupId != (int)TSP.DataManager.TSStructureGroups.A || StructureSkeletonId != (int)TSStructureSkeleton.Ajory)
                    StructureSkeletonId = -1;
                #endregion               
                this.SelectMaxVersionForFish(ProjectId, 0, PlansTypeId);
                if (this.Count > 0)
                {
                    int PlansId = Convert.ToInt32(this[0]["PlansId"]);
                    int MjId = GetMjId(PlansTypeId);
                    int DiscountPercentId = Convert.ToInt32(ProjectRequestManager[0]["DiscountPercentId"]);

                    DesignerPlansManager.FindByDesignerPlansId(DesignerPlansId);
                    if (DesignerPlansManager.Count == 1)
                    {
                        if (Convert.ToInt32(DesignerPlansManager[0]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.OtherPerson)
                            Foundation = Convert.ToDouble(DesignerPlansManager[0]["Wage"]) * 0.06;
                        else
                            Foundation = Convert.ToDouble(DesignerPlansManager[0]["Wage"]) * 0.05;
                    }
                    DesgInfo.Wage = Convert.ToInt32(DesignerPlansManager[0]["Wage"]);
                    PriceArchiveManager.FindById(Convert.ToInt32(DesignerPlansManager[0]["PriceArchiveId"]));
                    if (PriceArchiveManager.Count > 0)
                    {
                        DesgInfo.PricaArchiveId = Convert.ToInt32(PriceArchiveManager[0]["PriceArchiveId"]);
                        DesgInfo.PricaArchiveName = PriceArchiveManager[0]["YearName"].ToString();
                        int PriceArchiveId = Convert.ToInt32(PriceArchiveManager[0]["PriceArchiveId"]);
                        PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, Step, StructureSkeletonId);
                        if (PriceArchiveStructureItemsManager.Count == 0)
                        {
                            PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, StructureSkeletonId);
                            if (PriceArchiveStructureItemsManager.Count == 0)
                                PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, -1);

                        }
                        if (PriceArchiveStructureItemsManager.Count > 0)
                        {
                            PriceArchiveStructureItemDetailManager.FindByItemIdAndTypeIdAndMjId(Convert.ToInt32(PriceArchiveStructureItemsManager[0]["ItemId"]), (int)PriceArchiveStructureItemDetailTypeManager.Types.Design, MjId);
                            if (PriceArchiveStructureItemDetailManager.Count > 0)
                                Price = Convert.ToDecimal(PriceArchiveStructureItemDetailManager[0]["Price"]) * Convert.ToDecimal(Foundation);
                        }
                    }
                }

            }

            return Price;
        }
        /// <summary>
        /// محاسبه هزینه پنج درصد طراحی بر اساس تعداد طبقات پروژه
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PlansTypeId"></param>
        /// <param name="PrjReId"></param>
        /// <param name="DesignerPlansId"></param>
        /// <returns></returns>
        public decimal Get5PercentPriceByStepForMemberEpayment(int ProjectId, int PlansTypeId, int PrjReId, int PriceArchiveId, double Wage)
        {
            PriceArchiveManager PriceArchiveManager = new PriceArchiveManager();
            PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new PriceArchiveStructureItemsManager();
            PriceArchiveStructureItemDetailManager PriceArchiveStructureItemDetailManager = new PriceArchiveStructureItemDetailManager();
            //PriceArchiveStructureItemDetailTypeManager PriceArchiveStructureItemDetailTypeManager = new PriceArchiveStructureItemDetailTypeManager();
            if (ProjectRequestManager == null)
                ProjectRequestManager = new ProjectRequestManager();


            decimal Price = 0;
            ProjectRequestManager.FindByCode(PrjReId);
            if (ProjectRequestManager.Count > 0)
            {
                double Foundation = 0;
                int GroupId = Convert.ToInt32(ProjectRequestManager[0]["GroupId"]);
                int Step = Convert.ToInt32(ProjectRequestManager[0]["MaxStageNum"]);
                #region نوع اسکلت
                int StructureSkeletonId = -1;
                //*****               
                if (!Utility.IsDBNullOrNullValue(ProjectRequestManager[0]["StructureSkeletonId"]))
                    StructureSkeletonId = Convert.ToInt32(ProjectRequestManager[0]["StructureSkeletonId"]);
                //*****
                if (GroupId == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonId == -1)
                {
                    return 0;
                }
                if (GroupId != (int)TSP.DataManager.TSStructureGroups.A || StructureSkeletonId != (int)TSStructureSkeleton.Ajory)
                    StructureSkeletonId = -1;
                #endregion
                int MjId = GetMjId(PlansTypeId);
                int DiscountPercentId = Convert.ToInt32(ProjectRequestManager[0]["DiscountPercentId"]);
                Foundation = Wage * 0.05;
                PriceArchiveManager.FindById(PriceArchiveId);
                if (PriceArchiveManager.Count > 0)
                {
                    PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, Step, StructureSkeletonId);
                    if (PriceArchiveStructureItemsManager.Count == 0)
                    {
                        PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, StructureSkeletonId);
                        if (PriceArchiveStructureItemsManager.Count == 0)
                            PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, -1);
                    }
                    if (PriceArchiveStructureItemsManager.Count > 0)
                    {
                        PriceArchiveStructureItemDetailManager.FindByItemIdAndTypeIdAndMjId(Convert.ToInt32(PriceArchiveStructureItemsManager[0]["ItemId"]), (int)PriceArchiveStructureItemDetailTypeManager.Types.Design, MjId);
                        if (PriceArchiveStructureItemDetailManager.Count > 0)
                            Price = Convert.ToDecimal(PriceArchiveStructureItemDetailManager[0]["Price"]) * Convert.ToDecimal(Foundation);
                    }
                }

            }

            return Price;
        }

        /// <summary>
        /// محاسبه هزینه پنج درصد طراحی بر اساس تعداد طبقات پروژه
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PlansTypeId"></param>
        /// <param name="PrjReId"></param>
        /// <param name="DesignerPlansId"></param>
        /// <returns></returns>
        public decimal Get5PercentPriceByStep(int ProjectId, int PlansTypeId, int PrjReId, int DesignerPlansId)
        {
            DesignerInfo DesgInfo = new DesignerInfo();
            return Get5PercentPriceByStep(ProjectId, PlansTypeId, PrjReId, DesignerPlansId, ref DesgInfo);

        }

        private int GetMjId(int PlansTypeId)
        {
            switch (PlansTypeId)
            {
                case (int)TSPlansType.Memari:
                    return (int)MainMajors.Architecture;

                case (int)TSPlansType.Sazeh:
                    return (int)MainMajors.Civil;

                case (int)TSPlansType.Shahrsazi:
                    return (int)MainMajors.Urbanism;

                case (int)TSPlansType.TasisatBargh:
                    return (int)MainMajors.Electronic;

                case (int)TSPlansType.TasisatMechanic:
                    return (int)MainMajors.Mechanic;
            }
            return -2;
        }

        private int CheckController(int PlansId)
        {
            int Per = 0;

            Plans_ControlerManager PlansControlerManager = new Plans_ControlerManager();
            PlansControlerManager.FindActiveControlerByPlansId(PlansId);
            if (PlansControlerManager.Count == 0)
                Per = (int)ErrorRequest.NoController;
            return Per;
        }

        #endregion

        #region  SendDocToNextStep

        public int DoNextTaskOfConfirming(int PlansId, int CurrentUserId)
        {
            int Per = UpdatePlansConfirmingStatus(PlansId, CurrentUserId, (int)TSPlansConfirming.Confirmed);
            return Per;
        }

        public int DoNextTaskOfRejecting(int PlansId, int CurrentUserId)
        {
            int Per = UpdatePlansConfirmingStatus(PlansId, CurrentUserId, (int)TSPlansConfirming.NotConfirmed);
            return Per;
        }

        private int UpdatePlansConfirmingStatus(int PlansId, int CurrentUserId, int ConfirmingStatus)
        {
            int Per = 0;
            this.FindByPlansId(PlansId);
            if (this.Count > 0)
            {
                this[0].BeginEdit();
                this[0]["IsConfirmed"] = ConfirmingStatus;
                if (Convert.ToInt32(this[0]["IsDesAccepted"]) != (int)TSDesignerAcceptance.ConfirmedWithoutSaveController)
                    this[0]["IsDesAccepted"] = (int)TSDesignerAcceptance.Accepted;
                this[0]["UserId"] = CurrentUserId;
                this[0].EndEdit();
                if (this.Save() > 0)
                {
                    Per = 0;
                }
                else
                {
                    Per = (int)ErrorWFNextStep.Error;
                }
            }
            else
            {
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }

        public int CheckConditionsForNextStepOfConfirming(int SelectedTaskCode, int PlansId, int CurentUserUltId)
        {
            int Per = 0;
            //int TaskCode = -1;
            this.FindByPlansId(PlansId);
            if (this.Count == 1)
            {
                if (SelectedTaskCode != (int)TSP.DataManager.WorkFlowTask.RejectPlanAndEndProcess)
                    Per = CheckDesigner(PlansId, Convert.ToInt32(this[0]["ProjectId"]));
                if (Per != 0)
                    return Per;
                int IsDesAccepted = Convert.ToInt32(this[0]["IsDesAccepted"]);

                switch (SelectedTaskCode)
                {
                    case (int)TSP.DataManager.WorkFlowTask.SavePlanInfo:

                        break;
                    case (int)TSP.DataManager.WorkFlowTask.TSEmployeeConfirmDesigners:
                        break;
                    case (int)TSP.DataManager.WorkFlowTask.FishPaymentByMember:
                        break;

                    case (int)TSP.DataManager.WorkFlowTask.AssignControlerToPlan:
                        if (IsDesAccepted == (int)TSDesignerAcceptance.ConfirmedWithoutSaveController)
                            Per = (int)ErrorRequest.JustEndingTaskForConfirmedPlan;
                        if (Per == 0)
                            Per = Check5PercentFiche(PlansId, Convert.ToInt32(this[0]["ProjectId"]), Convert.ToInt32(this[0]["PlansTypeId"]), Convert.ToInt32(this[0]["PrjReId"]));
                        if (Per == 0)
                            Per = CheckIfAllDesingerForPlanInserted(PlansId, Convert.ToInt32(this[0]["PrjReId"]));
                        break;
                    case (int)TSP.DataManager.WorkFlowTask.ControlerConfirmingPlan:
                        if (IsDesAccepted == (int)TSDesignerAcceptance.ConfirmedWithoutSaveController)
                            Per = (int)ErrorRequest.JustEndingTaskForConfirmedPlan;
                        if (Per == 0)
                            Per = CheckController(PlansId);
                        if (Per == 0)
                            Per = Check5PercentFiche(PlansId, Convert.ToInt32(this[0]["ProjectId"]), Convert.ToInt32(this[0]["PlansTypeId"]), Convert.ToInt32(this[0]["PrjReId"]));
                        if (Per == 0)
                            Per = CheckIfAllDesingerForPlanInserted(PlansId, Convert.ToInt32(this[0]["PrjReId"]));
                        break;


                    case (int)TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess:
                        if (CurentUserUltId != (int)UserType.Member)
                        {
                            if (IsDesAccepted != (int)TSDesignerAcceptance.Accepted && IsDesAccepted != (int)TSDesignerAcceptance.ConfirmedWithoutSaveController)
                                Per = (int)ErrorRequest.NoControllerConfirmation;
                        }
                        if (Per == 0)
                            Per = Check5PercentFiche(PlansId, Convert.ToInt32(this[0]["ProjectId"]), Convert.ToInt32(this[0]["PlansTypeId"]), Convert.ToInt32(this[0]["PrjReId"]));
                        if (Per == 0)
                            Per = CheckIfAllDesingerForPlanInserted(PlansId, Convert.ToInt32(this[0]["PrjReId"]));
                        break;

                    case (int)TSP.DataManager.WorkFlowTask.RejectPlanAndEndProcess:
                        break;
                }
            }
            else
            {
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }

        #endregion
        #endregion

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSPlans);
        }
        public static Permission GetUserPermissionTSReportPlans(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSReportPlans);
        }

        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSPlans";
            tableMapping.ColumnMappings.Add("PlansId", "PlansId");
            tableMapping.ColumnMappings.Add("PrjReId", "PrjReId");
            tableMapping.ColumnMappings.Add("ProjectId", "ProjectId");
            tableMapping.ColumnMappings.Add("Status", "Status");
            tableMapping.ColumnMappings.Add("PlansTypeId", "PlansTypeId");
            tableMapping.ColumnMappings.Add("No", "No");
            tableMapping.ColumnMappings.Add("PlanVersion", "PlanVersion");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("Date", "Date");
            tableMapping.ColumnMappings.Add("IsConfirmed", "IsConfirmed");
            tableMapping.ColumnMappings.Add("MailNo", "MailNo");
            tableMapping.ColumnMappings.Add("MailDate", "MailDate");
            tableMapping.ColumnMappings.Add("IsDesAccepted", "IsDesAccepted");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("FollowCode", "FollowCode");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("currentTaskId", "currentTaskId");
            tableMapping.ColumnMappings.Add("CurrentStateId", "CurrentStateId");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSPlans";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@PlansId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@InActive", System.Data.SqlDbType.Int);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSPlans";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PlansId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PlansId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSPlans";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Status", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PlansTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PlansTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@No", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "No", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PlanVersion", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PlanVersion", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirmed", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirmed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDesAccepted", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsDesAccepted", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FollowCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FollowCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CurrentStateId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CurrentStateId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@currentTaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "currentTaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSPlans";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrjReId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrjReId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ProjectId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Status", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PlansTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PlansTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@No", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "No", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PlanVersion", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PlanVersion", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Date", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Date", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirmed", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirmed", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDesAccepted", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsDesAccepted", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FollowCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FollowCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PlansId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PlansId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PlansId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "PlansId", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CurrentStateId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "CurrentStateId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@currentTaskId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "currentTaskId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSPlansDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public void FindByPlansId(int PlansId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.Parameters["@PlansId"].Value = PlansId;
            Fill();
        }

        public DataTable FindActivesByProjectId(int ProjectId)
        {
            ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@InActive"].Value = 0;
            Fill();
            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectById(int PlansId, int ProjectId, int ProjectAgentId, int TaskCode)
        {
            if (PlansId == -1 && ProjectId == -1 && ProjectAgentId == -1 && TaskCode == -1)
                return new DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSPlansByProject", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableTypeManager.FindTtId(TableType.TSPlans);
            adapter.SelectCommand.Parameters.Add("@PlansId", SqlDbType.Int, 4, "PlansId").Value = PlansId;
            adapter.SelectCommand.Parameters.Add("@ControlerId", SqlDbType.Int, 4, "ControlerId").Value = -1;
            adapter.SelectCommand.Parameters.Add("@ProjectId", SqlDbType.Int, 4, "ProjectId").Value = ProjectId;
            adapter.SelectCommand.Parameters.AddWithValue("@PlansTypeId", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@IsConfirmed", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectAgentId", ProjectAgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@TaskCode", TaskCode);

            adapter.Fill(dt);
            return (dt);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSPlansByProjectForEpaymentConfirm(int PlansId, int TaskCode)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSPlansByProjectForEpaymentConfirm", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableTypeManager.FindTtId(TableType.TSPlans);
            adapter.SelectCommand.Parameters.Add("@PlansId", SqlDbType.Int, 4, "PlansId").Value = PlansId;
            adapter.SelectCommand.Parameters.AddWithValue("@TaskCode", TaskCode);

            adapter.Fill(dt);
            return (dt);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSPlansByProjectForControlerManagmentPage(int ProjectAgentId, int TaskCode, string WfStateDateFrom, string WfStateDateTo)
        {
            if (ProjectAgentId == -1 && TaskCode == -1 && WfStateDateFrom == "1" && WfStateDateTo == "2")
                return new DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSPlansByProjectForControlerManagmentPage", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableTypeManager.FindTtId(TableType.TSPlans);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectAgentId", ProjectAgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@TaskCode", TaskCode);
            adapter.SelectCommand.Parameters.AddWithValue("@WfStateDateFrom", WfStateDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@WfStateDateTo", WfStateDateTo);

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectById(int PlansId, int ProjectId)
        {
            return SelectById(PlansId, ProjectId, -1, -1);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable CountTSPlansByTaskCode(int ProjectAgentId, TSP.DataManager.WorkFlowTask WorkFlowTask, string WfStateDateFrom, string WfStateDateTo)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("CountTSPlansByTaskCode", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableTypeManager.FindTtId(TableType.TSPlans);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectAgentId", ProjectAgentId);
            adapter.SelectCommand.Parameters.AddWithValue("@TaskCode", (int)WorkFlowTask);
            adapter.SelectCommand.Parameters.AddWithValue("@WfStateDateFrom", WfStateDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@WfStateDateTo", WfStateDateTo);

            adapter.Fill(dt);
            return (dt);
        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSPlansForMember(int MeId, int DesignerInAcive)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectTSPlansForMember";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.TSPlans));
            adapter.SelectCommand.Parameters.AddWithValue("@TableTypeProject_Designer", TableTypeManager.FindTtId(TableType.TSProject_Designer));
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@DesignerInAcive", DesignerInAcive);

            adapter.Fill(dt);
            return (dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PlansTypeId"></param>
        /// <param name="IsConfirmed">0:UnNoun , 1:Confirm , 2:NotConfirm</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPlanSubRequest(int ProjectId, int PlansId, int PlansTypeId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.CommandText = "spSelectTSPlansSubRequest";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@PlansId", SqlDbType.Int, 4, "PlansId").Value = PlansId;
            this.Adapter.SelectCommand.Parameters.Add("@ProjectId", SqlDbType.Int, 4, "ProjectId").Value = ProjectId;
            this.Adapter.SelectCommand.Parameters.Add("@PlansTypeId", SqlDbType.Int, 4, "PlansTypeId").Value = PlansTypeId;
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirmed", SqlDbType.Int, 4, "IsConfirmed").Value = -1;

            adapter.Fill(dt);
            return (dt);
        }

        /// <summary>
        /// مرتب سازی نقشه ها بر اساس آخرین نقشه به اولین است و اگر کد درخواست ارسال شود نقشه های از درخواست پروژه خاص به قبل را بر می گرداند
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PlansTypeId"></param>
        /// <param name="IsConfirmed">0:UnNoun , 1:Confirm , 2:NotConfirm</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void SelectByPlanType(int ProjectId, int PlansTypeId, int IsConfirmed, int PrjReId)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.CommandText = "spSelectTSPlansByProjectByPlanType";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableTypeManager.FindTtId(TableType.TSPlans);
            this.Adapter.SelectCommand.Parameters.Add("@PlansId", SqlDbType.Int, 4, "PlansId").Value = -1;
            this.Adapter.SelectCommand.Parameters.Add("@ControlerId", SqlDbType.Int, 4, "ControlerId").Value = -1;
            this.Adapter.SelectCommand.Parameters.Add("@ProjectId", SqlDbType.Int, 4, "ProjectId").Value = ProjectId;
            this.Adapter.SelectCommand.Parameters.Add("@PlansTypeId", SqlDbType.Int, 4, "PlansTypeId").Value = PlansTypeId;
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirmed", SqlDbType.Int, 4, "IsConfirmed").Value = IsConfirmed;
            this.Adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            //

            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void SelectTSPlansByProjectAndRequest(int ProjectId, int PlansTypeId, int IsConfirmed, int PrjReId)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.Transaction = this.Transaction;
            this.Adapter.SelectCommand.CommandText = "spSelectTSPlansByProjectAndRequest";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableTypeManager.FindTtId(TableType.TSPlans);
            this.Adapter.SelectCommand.Parameters.Add("@PlansId", SqlDbType.Int, 4, "PlansId").Value = -1;
            this.Adapter.SelectCommand.Parameters.Add("@ControlerId", SqlDbType.Int, 4, "ControlerId").Value = -1;
            this.Adapter.SelectCommand.Parameters.Add("@ProjectId", SqlDbType.Int, 4, "ProjectId").Value = ProjectId;
            this.Adapter.SelectCommand.Parameters.Add("@PlansTypeId", SqlDbType.Int, 4, "PlansTypeId").Value = PlansTypeId;
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirmed", SqlDbType.Int, 4, "IsConfirmed").Value = IsConfirmed;
            this.Adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);

            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable ReportTSPlansControler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "ReportTSPlansControler";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableTypePlanControler", SqlDbType.Int, 4, "TableTypePlanControler").Value = TableTypeManager.FindTtId(TableType.TSPlans_Controler);
            adapter.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PlansTypeId"></param>
        /// <param name="IsConfirmed">0:UnNoun , 1:Confirm , 2:NotConfirm</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void SelectByPlanType(int ProjectId, int PlansTypeId, int IsConfirmed)
        {
            SelectByPlanType(ProjectId, PlansTypeId, IsConfirmed, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSPlansForControler(int MeId, int InActiveControler)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectTSPlansForControler";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = (int)TableTypeManager.FindTtId(TableType.TSPlans);
            adapter.SelectCommand.Parameters.AddWithValue("@TableTypePlanControler", (int)TableTypeManager.FindTtId(TableType.TSPlans_Controler));
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4, "MeId").Value = MeId;
            adapter.SelectCommand.Parameters.AddWithValue("@InActiveControler", InActiveControler);


            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSPlansForControler(int MeId)
        {
            return SelectTSPlansForControler(MeId, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void SelectLastVerionPlan(int ProjectId, int PlansTypeId, int IsConfirmed)
        {
            // DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectTSPlansLastVersion";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@ProjectId", SqlDbType.Int, 4, "ProjectId").Value = ProjectId;
            adapter.SelectCommand.Parameters.Add("@PlansTypeId", SqlDbType.Int, 4, "PlansTypeId").Value = PlansTypeId;
            adapter.SelectCommand.Parameters.Add("@IsConfirmed", SqlDbType.Int, 4, "IsConfirmed").Value = IsConfirmed;

            adapter.Fill(DataTable);
            // return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMaxVersion(string No, int ProjectId, int InActive, int PlansTypeId)
        {
            //DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSPlansMaxVersion", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@No", No);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.SelectCommand.Parameters.AddWithValue("@PlansTypeId", PlansTypeId);
            adapter.Fill(this.DataTable);

            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMaxVersion(int ProjectId, int InActive, int PlansTypeId, int IsControlerConfirm)
        {
            //DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSPlansMaxVersion", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.SelectCommand.Parameters.AddWithValue("@PlansTypeId", PlansTypeId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsControlerConfirm", IsControlerConfirm);
            adapter.Fill(this.DataTable);

            return this.DataTable;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMaxVersion(int ProjectId, int InActive, int PlansTypeId)
        {
            return SelectMaxVersion(ProjectId, InActive, PlansTypeId, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMaxVersionForFish(int ProjectId, int InActive, int PlansTypeId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSPlansMaxVersionForFish", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;

            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.SelectCommand.Parameters.AddWithValue("@PlansTypeId", PlansTypeId);
            adapter.Fill(this.DataTable);

            return this.DataTable;
        }

    }
}
