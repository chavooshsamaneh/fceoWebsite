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
/// <summary>
/// توابع مربوط به ظرفیتها
/// </summary>

public    class OldCapacity
    {

        private enum CapacityErr
        {
            NotEnoughRmainCapacity = 1,
            MaxJobIsTaken = 2,
            NotEnoughCapacityAndMaxJobIsTaken = 3,
            NotEnoughStep = 4
        }


        #region بدست آوردن ظرفیت شرکت یا دفتر
        /// <summary>
        /// اطلاعات ظرفیت فرد، شرکت یا یک دفتر را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: TotalCapacity(int), ArrayList[1]:UsedCapacity(int) , ArrayList[2]: RemainCapacity(int), ArrayList[3]:ReservedCapacity(int) , ArrayList[4]: ProjectNum(int), ArrayList[5]: MaxJoubCount(string), ArrayList[6]: MaxFloor(string), ArrayList[7]: ConditionalCapacity(int)
        /// </summary>
        /// <param name="ProjectIngridientTypeId">نوع عامل پروژه: طراح، ناظر ،مجری،مالک</param>
        /// <param name="MemberTypeId"></param>
        /// <param name="MeOfficeEngOId"></param>
        /// <returns></returns>
        public ArrayList GetCapacityInformationPerStage(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId)
        {
            ArrayList Temp = GetCapacityInfoPerStage(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId);
            if (Temp.Count > 0)
            {
                if (Convert.ToInt32(Temp[6]) == -1)
                    Temp[6] = "بدون محدودیت";
                if (Convert.ToInt32(Temp[5]) == -1)
                    Temp[5] = "بدون محدودیت";
            }
            return Temp;
        }

        /// <summary>
        /// اطلاعات ظرفیت فرد، شرکت یا یک دفتر را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: TotalCapacity, ArrayList[1]:UsedCapacity , ArrayList[2]: RemainCapacity, ArrayList[3]:ReservedCapacity , ArrayList[4]: ProjectNum, ArrayList[5]: MaxJoubCount, ArrayList[6]: MaxFloor, ArrayList[7]: ConditionalCapacity
        /// </summary>
        private ArrayList GetCapacityInfoPerStage(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId)
        {
            ArrayList CapacityArr = new ArrayList();
            ArrayList TempArray = new ArrayList();

            int TotalCapacity = 0;
            int UsedCapacity = 0;
            int RemainCapacity = 0;
            int ReservedCapacity = 0;
            int MaxJoubCount = 0;
            int MaxFloor = 0;
            int ConditionalCapacity = 0;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    TempArray = GetDsgObsTotalCapacityPerStage(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId);
                    if (TempArray.Count != 0)
                    {
                        TotalCapacity = Convert.ToInt32(TempArray[1]);
                        MaxJoubCount = Convert.ToInt32(TempArray[0]);
                        ConditionalCapacity = Convert.ToInt32(TempArray[3]);
                    }
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    TempArray = GetDsgObsTotalCapacityPerStage(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId);
                    if (TempArray.Count != 0)
                    {
                        TotalCapacity = Convert.ToInt32(TempArray[2]);
                        MaxJoubCount = Convert.ToInt32(TempArray[0]);
                        ConditionalCapacity = Convert.ToInt32(TempArray[3]);
                    }
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TempArray = GetImpTotalCapacityPerStage(MemberTypeId, MeOfficeEngOId);
                    if (TempArray.Count != 0)
                    {
                        TotalCapacity = Convert.ToInt32(TempArray[1]);
                        MaxJoubCount = Convert.ToInt32(TempArray[2]);
                        MaxFloor = Convert.ToInt32(TempArray[0]);
                        ConditionalCapacity = Convert.ToInt32(TempArray[3]);
                    }
                    break;
            }

            UsedCapacity = GetTotalUsedCapacityPerStage(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
            RemainCapacity = TotalCapacity - UsedCapacity;
            ReservedCapacity = GetTotalReservedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
            int ProjectNum = GetTotalProjectNum(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);

            CapacityArr.Add(TotalCapacity);
            CapacityArr.Add(UsedCapacity);
            CapacityArr.Add(RemainCapacity);
            CapacityArr.Add(ReservedCapacity);
            CapacityArr.Add(ProjectNum);
            CapacityArr.Add(MaxJoubCount);
            CapacityArr.Add(MaxFloor);
            CapacityArr.Add(ConditionalCapacity);

            return CapacityArr;
        }

        /// <summary>
        /// کل ظرفیت و تعداد کار مجاز فرد، شرکت یا یک دفتر طراحی و نظارت را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: MaxJobCount(int), ArrayList[1]: MaxJobCapacity(int), ArrayList[2]: ObservationCapacity, ArrayList[3]: ConditionalCapacity
        /// </summary>
        public ArrayList GetDsgObsTotalCapacityPerStage(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId)
        {
            ArrayList CapacityArr = new ArrayList();
            ArrayList CapArr = new ArrayList();

            switch (MemberTypeId)
            {
                case (int)TSP.DataManager.TSMemberType.Member:
                    CapArr = GetMemberDsgObsCapacityPerStage(MeOfficeEngOId, ProjectIngridientTypeId);
                    break;

                case (int)TSP.DataManager.TSMemberType.Office:
                    CapArr = GetOfficeDsgObsCapacityPerStage(MeOfficeEngOId, ProjectIngridientTypeId, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office);
                    break;

                case (int)TSP.DataManager.TSMemberType.EngOffice:
                    CapArr = GetOfficeDsgObsCapacityPerStage(MeOfficeEngOId, ProjectIngridientTypeId, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice);
                    break;
            }

            if (CapArr.Count != 0)
            {
                CapacityArr.Add(Convert.ToInt32(CapArr[0]));
                CapacityArr.Add(Convert.ToInt32(CapArr[1]));
                if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
                {
                    CapacityArr.Add(Convert.ToInt32(CapArr[3]));
                    CapacityArr.Add(Convert.ToInt32(CapArr[14]));
                }
                else
                {
                    CapacityArr.Add(Convert.ToInt32(CapArr[2]));
                    CapacityArr.Add(Convert.ToInt32(CapArr[3]));
                }
            }

            return CapacityArr;
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationCapacity, ArrayList[3]: ConditionalCapacity
        /// </summary>
        private ArrayList GetOfficeDsgObsCapacityPerStage(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType)
        {
            // MajorArr-----> ArrayList[0]: MainMajorNum, ArrayList[1]: SecondaryMajorNum, ArrayList[2]: TotalMajorNum

            // MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, 
            //                     ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc,
            //                     ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName
            //                     ArrayList[14]: ConditionalCapacity

            ArrayList MembersArr = new ArrayList();
            TSP.DataManager.DocOffMajorNum DocOffMajorNum = new TSP.DataManager.DocOffMajorNum();
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocOffIncreaseJobCapacityManager IncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();
            ArrayList CapacityArr = new ArrayList();

            int ConditionalCapacity = GetConditionalCapacity(OfficeEngoId, ProjectIngridientTypeId);

            OfficeMemberManager = GetOfficeMembers(OfficeEngoId, DocOffIncreaseJobCapacityType);

            int k = 0;
            for (int i = 0; i < OfficeMemberManager.Count; i++)
            {
                if (Convert.ToInt32(OfficeMemberManager[i]["OfmType"]) == (int)TSP.DataManager.OfficeMemberType.Member)
                {
                    ArrayList Member = GetMemberDsgObsCapacityPerStage(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId);
                    if (Member.Count != 0)
                    {
                        MembersArr.Add(Member);
                        ((ArrayList)MembersArr[k])[6] = GetGradeByMFId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId).ToString();
                        k++;
                    }
                }
            }

            int MaxJobCount = 0;
            int MaxJobCapacity = 0;
            int ObservationCapacity = 0;

            if (MembersArr.Count != 0)
            {
                ArrayList MajorArr = GetMajorNum(MembersArr);

                DocOffMajorNum.FindByMajorsNum((int)MajorArr[0], (int)MajorArr[1], (int)MajorArr[2]);
                IncreaseJobCapacityManager.FindByMNumId(Convert.ToInt32(DocOffMajorNum[0]["MNumId"]), DocOffIncreaseJobCapacityType);

                for (int i = 0; i < MembersArr.Count; i++)
                {
                    bool SameGradeInc = false;
                    bool MajorInc = false;
                    //****DesignIncPer:درصد افزایش طراحی
                    //DesignInc=MaxJobCapacity*DesignIncPer/100     
                    ((ArrayList)MembersArr[i])[7] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["DesignIncPer"]) / 100).ToString();
                    for (int j = 0; j < MembersArr.Count; j++)
                    {
                        if (i != j)
                        {
                            if (((ArrayList)MembersArr[i])[5].ToString() == ((ArrayList)MembersArr[j])[5].ToString())
                            {
                                MajorInc = true;
                                if (((ArrayList)MembersArr[i])[6].ToString() == ((ArrayList)MembersArr[j])[6].ToString())
                                {
                                    if (!MajorInc)
                                        SameGradeInc = true;
                                }
                                else
                                    SameGradeInc = false;
                            }

                        }
                    }
                    if (SameGradeInc)
                        ((ArrayList)MembersArr[i])[8] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["SameGradeIncPer"]) / 100).ToString();

                    if (MajorInc)
                        ((ArrayList)MembersArr[i])[9] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["MajorIncPer"]) / 100).ToString();
                    //*************TotalDsgCapacity=MaxJobCapacity+DesignInc+SameGradeInc+MajorInc
                    ((ArrayList)MembersArr[i])[10] = Convert.ToInt32(((ArrayList)MembersArr[i])[1]) + Convert.ToInt32(((ArrayList)MembersArr[i])[7]) + Convert.ToInt32(((ArrayList)MembersArr[i])[8]) + Convert.ToInt32(((ArrayList)MembersArr[i])[9]);
                    //*************TotalObsCapacity=ObservationPercent*(MaxJobCapacity+DesignInc+SameGradeInc+MajorInc)=ObservationPercent*TotalDsgCapacity
                    ((ArrayList)MembersArr[i])[11] = Convert.ToInt32(Convert.ToDouble(((ArrayList)MembersArr[i])[2]) * (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) + Convert.ToInt32(((ArrayList)MembersArr[i])[7]) + Convert.ToInt32(((ArrayList)MembersArr[i])[8]) + Convert.ToInt32(((ArrayList)MembersArr[i])[9])));

                    MaxJobCount += Convert.ToInt32(((ArrayList)MembersArr[i])[0]);
                    MaxJobCapacity += Convert.ToInt32(((ArrayList)MembersArr[i])[10]);
                    ObservationCapacity += Convert.ToInt32(((ArrayList)MembersArr[i])[11]);
                }
            }

            MaxJobCapacity += Convert.ToInt32(ConditionalCapacity);
            ObservationCapacity += Convert.ToInt32(ConditionalCapacity);

            if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
                MaxJobCount = MaxJobCount / 2;

            CapacityArr.Add(MaxJobCount);
            CapacityArr.Add(MaxJobCapacity);
            CapacityArr.Add(ObservationCapacity);
            CapacityArr.Add(ConditionalCapacity);

            return CapacityArr;

        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت را بر می گرداند
        /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationCapacity, ArrayList[3]: ConditionalCapacity
        /// </summary>
        private ArrayList GetOfficeDsgObsCapacity(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType)
        {
            // MajorArr-----> ArrayList[0]: MainMajorNum, ArrayList[1]: SecondaryMajorNum, ArrayList[2]: TotalMajorNum

            // MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, 
            //                     ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc,
            //                     ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName,
            //                     ArrayList[14]: ConditionalCapacity

            ArrayList MembersArr = new ArrayList();
            TSP.DataManager.DocOffMajorNum DocOffMajorNum = new TSP.DataManager.DocOffMajorNum();
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocOffIncreaseJobCapacityManager IncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();
            ArrayList CapacityArr = new ArrayList();

            int ConditionalCapacity = GetConditionalCapacity(OfficeEngoId, ProjectIngridientTypeId);
            //****لیست اعضای دفتر / شرکت را بر بدست می آورد
            OfficeMemberManager = GetOfficeMembers(OfficeEngoId, DocOffIncreaseJobCapacityType);

            int k = 0;
            for (int i = 0; i < OfficeMemberManager.Count; i++)
            {
                if (Convert.ToInt32(OfficeMemberManager[i]["OfmType"]) == (int)TSP.DataManager.OfficeMemberType.Member)
                {
                    ArrayList Member = GetMemberDsgObsCapacity(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId);
                    if (Member.Count != 0)
                    {
                        MembersArr.Add(Member);
                        //****پایه عضو را بدست آورده
                        ((ArrayList)MembersArr[k])[6] = GetGradeByMFId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId).ToString();
                        k++;
                    }
                }
            }

            int MaxJobCount = 0;
            int MaxJobCapacity = 0;
            int ObservationCapacity = 0;

            if (MembersArr.Count != 0)
            {
                ArrayList MajorArr = GetMajorNum(MembersArr);

                DocOffMajorNum.FindByMajorsNum((int)MajorArr[0], (int)MajorArr[1], (int)MajorArr[2]);
                IncreaseJobCapacityManager.FindByMNumId(Convert.ToInt32(DocOffMajorNum[0]["MNumId"]), DocOffIncreaseJobCapacityType);

                for (int i = 0; i < MembersArr.Count; i++)
                {
                    bool SameGradeInc = false;
                    bool MajorInc = false;

                    ((ArrayList)MembersArr[i])[7] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["DesignIncPer"]) / 100).ToString();
                    for (int j = 0; j < MembersArr.Count; j++)
                    {
                        if (i != j)
                        {
                            if (((ArrayList)MembersArr[i])[5].ToString() == ((ArrayList)MembersArr[j])[5].ToString())
                            {
                                if (((ArrayList)MembersArr[i])[6].ToString() == ((ArrayList)MembersArr[j])[6].ToString())
                                {
                                    if (!MajorInc)
                                        SameGradeInc = true;
                                }
                                else
                                    SameGradeInc = false;

                                MajorInc = true;
                            }

                        }
                    }
                    if (SameGradeInc)
                        ((ArrayList)MembersArr[i])[8] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["SameGradeIncPer"]) / 100).ToString();

                    if (MajorInc)
                        ((ArrayList)MembersArr[i])[9] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["MajorIncPer"]) / 100).ToString();

                    ((ArrayList)MembersArr[i])[10] = Convert.ToInt32(((ArrayList)MembersArr[i])[1]) + Convert.ToInt32(((ArrayList)MembersArr[i])[7]) + Convert.ToInt32(((ArrayList)MembersArr[i])[8]) + Convert.ToInt32(((ArrayList)MembersArr[i])[9]);
                    ((ArrayList)MembersArr[i])[11] = Convert.ToInt32(Convert.ToDouble(((ArrayList)MembersArr[i])[2]) * (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) + Convert.ToInt32(((ArrayList)MembersArr[i])[7]) + Convert.ToInt32(((ArrayList)MembersArr[i])[8]) + Convert.ToInt32(((ArrayList)MembersArr[i])[9])));

                    MaxJobCount += Convert.ToInt32(((ArrayList)MembersArr[i])[0]);
                    MaxJobCapacity += Convert.ToInt32(((ArrayList)MembersArr[i])[10]);
                    ObservationCapacity += Convert.ToInt32(((ArrayList)MembersArr[i])[11]);
                }
            }

            MaxJobCapacity += Convert.ToInt32(ConditionalCapacity);
            ObservationCapacity += Convert.ToInt32(ConditionalCapacity);

            if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
                MaxJobCount = MaxJobCount / 2;

            CapacityArr.Add(MaxJobCount);
            CapacityArr.Add(MaxJobCapacity);
            CapacityArr.Add(ObservationCapacity);
            CapacityArr.Add(ConditionalCapacity);

            return CapacityArr;

        }

        #endregion

        #region TotalCurrent

        #region Private-Methods

        /// <summary>
        /// رشته اصلی پروانه یک عضو را بر می گرداند
        /// ArrayList[0]: MjId, ArrayList[1]: MjName
        /// </summary>
        private ArrayList GetMajor(int MeId)
        {
            ArrayList MajorArr = new ArrayList();
            TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
            DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
            if (DocMemberFileMajorManager.Count != 0)
            {
                MajorArr.Add(DocMemberFileMajorManager[0]["MjId"]);
                MajorArr.Add(DocMemberFileMajorManager[0]["MjName"]);
            }
            return MajorArr;
        }

        /// <summary>
        /// پایه یک عضو را بر می گرداند
        /// </summary>
        private int GetGrade(int MeId, int ProjectIngridientTypeId)
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
            if (GradeArr.Count != 0)
                return Convert.ToInt32(GradeArr[0]);
            else
                return 0;
        }

        #region بدست آوردن پایه
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
        /// ArrayList[0]: GradeId, ArrayList[1]: Type, ArrayList[2]: CivilGrdId, ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId
        /// </summary>
        private ArrayList GetOfficeImpGrade(int OfficeId)
        {
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            ArrayList GradeArr = OfficeMemberManager.FindOfficeImpGrade(OfficeId);
            return GradeArr;
        }
        #endregion

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

        /// <summary>
        /// ظرفیت اضافی یا کم شده یک شخص یا شرکت یا دفتر را بر می گرداند
        /// </summary>
        private int GetConditionalCapacity(int MeOfficeEngOId, int ProjectIngridientTypeId)
        {
            int ConditionalCapacity = 0;
            TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();
            ConditionalCapacityManager.FindByMeOfficeEngOId(MeOfficeEngOId, Utility.GetDateOfToday(), ProjectIngridientTypeId);
            for (int i = 0; i < ConditionalCapacityManager.Count; i++)
                ConditionalCapacity += Convert.ToInt32(ConditionalCapacityManager[0]["Capacity"]);

            return ConditionalCapacity;
        }

        /// <summary>
        /// اعضای فعال شرکت یا دفتر را بر می گرداند
        /// </summary>
        private TSP.DataManager.OfficeMemberManager GetOfficeMembers(int OfficeEngoId, int DocOffIncreaseJobCapacityType)
        {
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
                OfficeMemberManager.FindOfficeActiveMembers(OfficeEngoId, (int)TSP.DataManager.OfficeMemberType.Member, 0, 1);
            else if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice)
                OfficeMemberManager.FindEngOfficeActiveMembers(OfficeEngoId, 0, 1);

            return OfficeMemberManager;
        }

        /// <summary>
        /// اعضا و کاردان و معمارهای فعال شرکت را بر می گرداند
        /// </summary>
        private TSP.DataManager.OfficeMemberManager GetOfficeAllMembers(int OfficeId)
        {
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            OfficeMemberManager.FindOfficeActiveMembers(OfficeId, -1, 0, 1);

            return OfficeMemberManager;
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک عضو را بر می گرداند
        /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[12]: MeId, ArrayList[13]: MeName, ArrayList[14]: ConditionalCapacity
        /// </summary>
        private ArrayList GetMemberDsgObsCapacity(int MeId, int ProjectIngridientTypeId)
        {
            ArrayList MemberArr = new ArrayList();
            int Grade = GetGrade(MeId, ProjectIngridientTypeId);
            if (Grade != 0)
            {
                int ConditionalCapacity = GetConditionalCapacity(MeId, ProjectIngridientTypeId);
                int MjId = 0;
                ArrayList MjArray = GetMajor(MeId);
                if (MjArray.Count > 0)
                    MjId = Convert.ToInt32(MjArray[0]);

                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                MemberManager.FindByCode(MeId);
                //if (MemberManager.Count > 0)
                //    int.TryParse(MemberManager[0]["LastMjId"].ToString(), out MjId);

                TSP.DataManager.DocOffMemberCapacityManager MemberCapacityManager = new TSP.DataManager.DocOffMemberCapacityManager();
                MemberCapacityManager.FindByGrdId(Grade);

                if (MemberCapacityManager.Count > 0)
                {
                    MemberArr.Add(MemberCapacityManager[0]["MaxJobCount"].ToString());
                    MemberArr.Add((Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]) + ConditionalCapacity).ToString());
                    MemberArr.Add(MemberCapacityManager[0]["ObservationPercent"].ToString());
                    MemberArr.Add((Convert.ToInt32(Convert.ToDouble(MemberArr[1]) * Convert.ToDouble(MemberArr[2]))) + ConditionalCapacity);
                    MemberArr.Add(Grade.ToString());
                    MemberArr.Add(MjId.ToString());
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add(MeId.ToString());
                    MemberArr.Add(MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString());
                    MemberArr.Add(ConditionalCapacity);
                }
            }
            return MemberArr;
        }

      

        /// <summary>
        /// ترکیب رشته شرکا را بدست می آورد
        /// ArrayList[0]: MainMajorNum, ArrayList[1]: SecondaryMajorNum, ArrayList[2]: TotalMajorNum
        /// </summary>
        private ArrayList GetMajorNum(ArrayList MembersArr)
        {
            TSP.DataManager.MajorParentsManager MajorManager = new TSP.DataManager.MajorParentsManager();
            MajorManager.FindMjParents();

            int MainMajorNum = 0;
            int SecondaryMajorNum = 0;
            int TotalMajorNum = 0;
            int MajorIncrement = 0;

            bool Architecture = false;
            bool Urbanism = false;
            bool Civil = false;
            bool Mechanic = false;
            bool Electronic = false;
            bool Mapping = false;
            bool Traffic = false;
            #region //******چک کردن داشتن یک رشته از رشته های هفتگانه
            for (int j = 0; j < MembersArr.Count; j++)
            {
                switch (Convert.ToInt32(((ArrayList)MembersArr[j])[5]))
                {
                    case (int)TSP.DataManager.MainMajors.Architecture:
                        Architecture = true;
                        //ArchitectureNum += 1;
                        break;

                    case (int)TSP.DataManager.MainMajors.Civil:
                        Civil = true;
                        //CivilNum += 1;
                        break;

                    case (int)TSP.DataManager.MainMajors.Electronic:
                        Electronic = true;
                        //ElectronicNum += 1;
                        break;

                    case (int)TSP.DataManager.MainMajors.Mechanic:
                        Mechanic = true;
                        //MechanicNum += 1;
                        break;

                    case (int)TSP.DataManager.MainMajors.Mapping:
                        Mapping = true;
                        //MappingNum += 1;
                        break;

                    case (int)TSP.DataManager.MainMajors.Urbanism:
                        Urbanism = true;
                        //UrbanismNum += 1;
                        break;

                    case (int)TSP.DataManager.MainMajors.Traffic:
                        Traffic = true;
                        //TrafficNum += 1;
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
                TotalMajorNum = 1;
                MainMajorNum = 0;
                SecondaryMajorNum = 0;
            }

            else if (MainMajorNum < 4)
            {
                SecondaryMajorNum = 0;
                TotalMajorNum = 0;
            }

            ArrayList MajorArr = new ArrayList();
            MajorArr.Add(MainMajorNum);
            MajorArr.Add(SecondaryMajorNum);
            MajorArr.Add(TotalMajorNum);

            return MajorArr;
        }

        /// <summary>
        /// افراد یک دفتر یا شرکت و ظرفیت طراحی و نظارت آنها را بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc, ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName, ArrayList[14]: ConditionalCapacity
        /// </summary>
        private ArrayList GetOfficeMembers(int OfficeId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType)
        {
            // MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, 
            //                     ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc,
            //                     ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName,
            //                     ArrayList[14]: ConditionalCapacity

            ArrayList MembersArr = new ArrayList();
            TSP.DataManager.DocOffMajorNum DocOffMajorNum = new TSP.DataManager.DocOffMajorNum();
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocOffIncreaseJobCapacityManager IncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();
            //*****اعضای شرکت را بدست می آورد
            OfficeMemberManager = GetOfficeMembers(OfficeId, DocOffIncreaseJobCapacityType);
            //****برای هر عضو تعداد کار  و حداکثر ظرفیت اشتغال در یک سال را براساس پایه آن بدست می آورد / جدول 1 صفحه 26   
            int k = 0;
            for (int i = 0; i < OfficeMemberManager.Count; i++)
            {
                if (Convert.ToInt32(OfficeMemberManager[i]["OfmType"]) == (int)TSP.DataManager.OfficeMemberType.Member)
                {
                    ArrayList Member = GetMemberDsgObsCapacity(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId);
                    if (Member.Count != 0)
                    {
                        MembersArr.Add(Member);
                        ((ArrayList)MembersArr[k])[6] = GetGradeByMFId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId).ToString();
                        k++;
                    }
                }
            }
            //****************************************************
            if (MembersArr.Count != 0)
            {
                //*******ترکیب رشته شرکا را بدست می آورد
                ArrayList MajorArr = GetMajorNum(MembersArr);
                //*******************************************      
                DocOffMajorNum.FindByMajorsNum((int)MajorArr[0], (int)MajorArr[1], (int)MajorArr[2]);
                //*******بر اساس ترکیب رشته شرکا درصد افزایش را پیدا می کند
                if (DocOffMajorNum.Count > 0)
                {
                    IncreaseJobCapacityManager.FindByMNumId(Convert.ToInt32(DocOffMajorNum[0]["MNumId"]), DocOffIncreaseJobCapacityType);

                    //********بدست آوردن درصد افزایش بر اساس هم پایه بودن شرکا و حضور بیش از یک نفر در هر رشته
                    for (int i = 0; i < MembersArr.Count; i++)
                    {
                        bool SameGradeInc = false;
                        bool MajorInc = false;

                        ((ArrayList)MembersArr[i])[7] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["DesignIncPer"]) / 100).ToString();
                        for (int j = 0; j < MembersArr.Count; j++)
                        {
                            if (i != j)
                            {
                                if (((ArrayList)MembersArr[i])[5].ToString() == ((ArrayList)MembersArr[j])[5].ToString())
                                {
                                    if (((ArrayList)MembersArr[i])[6].ToString() == ((ArrayList)MembersArr[j])[6].ToString())
                                    {
                                        if (!MajorInc)
                                            SameGradeInc = true;
                                    }
                                    else
                                        SameGradeInc = false;
                                    //***هم رشته بودن شرکا
                                    MajorInc = true;
                                }

                            }
                        }
                        if (SameGradeInc)
                            ((ArrayList)MembersArr[i])[8] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["SameGradeIncPer"]) / 100).ToString();

                        if (MajorInc)
                            ((ArrayList)MembersArr[i])[9] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["MajorIncPer"]) / 100).ToString();

                        ((ArrayList)MembersArr[i])[10] = Convert.ToInt32(((ArrayList)MembersArr[i])[1]) + Convert.ToInt32(((ArrayList)MembersArr[i])[7]) + Convert.ToInt32(((ArrayList)MembersArr[i])[8]) + Convert.ToInt32(((ArrayList)MembersArr[i])[9]);
                        ((ArrayList)MembersArr[i])[11] = Convert.ToInt32(Convert.ToDouble(((ArrayList)MembersArr[i])[2]) * (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) + Convert.ToInt32(((ArrayList)MembersArr[i])[7]) + Convert.ToInt32(((ArrayList)MembersArr[i])[8]) + Convert.ToInt32(((ArrayList)MembersArr[i])[9])));
                    }
                }
            }
            return MembersArr;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک عضو را بر می گرداند
        /// ArrayList[0]: MaxFloor, ArrayList[1]: MaxJobCapacity, ArrayList[2]: MaxUnitCount, ArrayList[3]: Grade, ArrayList[4]: ConditionalCapacity
        /// </summary>
        private ArrayList GetMemberImpCapacity(int MeId)
        {
            ArrayList MemberArr = new ArrayList();
            int Grade = GetGrade(MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
            if (Grade != 0)
            {
                int ConditionalCapacity = GetConditionalCapacity(MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);

                TSP.DataManager.DocOffEngOfficeImpQualificationManager EngOfficeImpQualificationManager = new TSP.DataManager.DocOffEngOfficeImpQualificationManager();
                EngOfficeImpQualificationManager.FindByGrdId(Grade);

                if (EngOfficeImpQualificationManager.Count > 0)
                {
                    MemberArr.Add(EngOfficeImpQualificationManager[0]["MaxFloor"].ToString());
                    MemberArr.Add((Convert.ToInt32(EngOfficeImpQualificationManager[0]["MaxJobCapacity"]) + ConditionalCapacity).ToString());
                    MemberArr.Add(EngOfficeImpQualificationManager[0]["MaxUnitCount"].ToString());
                    MemberArr.Add(Grade.ToString());
                    MemberArr.Add(ConditionalCapacity);
                }
            }
            return MemberArr;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک شرکت را بر می گرداند
        /// ArrayList[0]: MaxFloor, ArrayList[1]:MaxCapacity , ArrayList[2]: MaxJobCount, ArrayList[3]: ConditionalCapacity, ArrayList[4]: GradeId, ArrayList[5]: GrdType
        /// </summary>
        private ArrayList GetOfficeImpCapacity(int OfficeId)
        {
            // GradeArr-----> ArrayList[0]: GradeId, ArrayList[1]: Type, ArrayList[2]: CivilGrdId, ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId

            ArrayList GradeArr = GetOfficeImpGrade(OfficeId);
            TSP.DataManager.DocOffOfficeMembersQualificationManager OfficeMembersQualificationManager = new TSP.DataManager.DocOffOfficeMembersQualificationManager();

            ArrayList CapacityArr = new ArrayList();

            if (GradeArr.Count != 0)
            {
                int ConditionalCapacity = GetConditionalCapacity(OfficeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);

                if ((int)GradeArr[1] == (int)TSP.DataManager.DocOffOfficeMembersQualificationType.Kardan_Kardan)
                    OfficeMembersQualificationManager.FindByGrdId((int)GradeArr[0], (int)GradeArr[1], (int)GradeArr[2]);
                else
                    OfficeMembersQualificationManager.FindByGrdId((int)GradeArr[0], (int)GradeArr[1], null);
                if (OfficeMembersQualificationManager.Count > 0)
                {
                    CapacityArr.Add(Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxFloor"]));
                    CapacityArr.Add(Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxCapacity"]) + ConditionalCapacity + GetCapacityOfPoints(OfficeId, Convert.ToInt32(GradeArr[3]), Convert.ToInt32(GradeArr[4]), Convert.ToInt32(OfficeMembersQualificationManager[0]["PointsLimitation"])));
                    CapacityArr.Add(Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxJobCount"]));
                    CapacityArr.Add(ConditionalCapacity);
                    CapacityArr.Add(GradeArr[0]);
                    CapacityArr.Add(GradeArr[1]);
                }
            }

            return CapacityArr;
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

        /// <summary>
        /// اعضا یک شرکت اجرا را بر می گرداند
        /// </summary>
        private DataTable GetImpOfficeMembers(int OfficeId)
        {
            // GradeArr-----> ArrayList[0]: GradeId, ArrayList[1]: Type, ArrayList[2]: CivilGrdId, ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId

            ArrayList GradeArr = GetOfficeImpGrade(OfficeId);
            DataTable OfficeMembersDT = GetImpOfficeMembersDT();

            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocOffGradeValuesManager DocOffGradeValuesManager = new TSP.DataManager.DocOffGradeValuesManager();

            OfficeMemberManager = GetOfficeAllMembers(OfficeId);

            for (int i = 0; i < OfficeMemberManager.Count; i++)
            {
                DataRow drOfficeMembers = OfficeMembersDT.NewRow();

                int GradeId = 0;
                int GrdTypeId = 0;
                string GrdType = "";
                string MeName = "";
                int MjId = 0;
                int Value = 0;
                bool MainMember = false;

                int OfmType = Convert.ToInt32(OfficeMemberManager[i]["OfmType"]);

                if (OfmType == (int)TSP.DataManager.OfficeMemberType.Member)
                {
                    GradeId = GetGrade(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                    GrdTypeId = (int)TSP.DataManager.DocOffGradeValuesGrdType.Engineer;
                    GrdType = "مهندس";
                    MeName = GetMeName(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]));
                    //MjId = GetMjId(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]));
                    ArrayList MjArray = GetMajor(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]));
                    if (MjArray.Count > 0)
                        MjId = Convert.ToInt32(MjArray[0]);
                }
                else if (OfmType == (int)TSP.DataManager.OfficeMemberType.Kardan)
                {
                    GradeId = GetTechnicianGrade(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                    GrdTypeId = (int)TSP.DataManager.DocOffGradeValuesGrdType.Technician;
                    GrdType = "كاردان";
                    ArrayList Temp = GetOthPersonName(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]));
                    MeName = Temp[0].ToString();
                    MjId = Convert.ToInt32(Temp[1]);
                }
                else if (OfmType == (int)TSP.DataManager.OfficeMemberType.Memar)
                {
                    GradeId = GetTechnicianGrade(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                    GrdTypeId = (int)TSP.DataManager.DocOffGradeValuesGrdType.ExperimentalArchitect;
                    GrdType = "معمار";
                    ArrayList Temp = GetOthPersonName(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]));
                    MeName = Temp[0].ToString();
                    MjId = Convert.ToInt32(Temp[1]);
                }

                if (Convert.ToInt32(OfficeMemberManager[i]["PersonId"]) != Convert.ToInt32(GradeArr[3]) && Convert.ToInt32(OfficeMemberManager[i]["PersonId"]) != Convert.ToInt32(GradeArr[4]))
                {
                    DocOffGradeValuesManager.FindByGrdId(GradeId, GrdTypeId);
                    if (DocOffGradeValuesManager.Count > 0)
                        Value = Convert.ToInt32(DocOffGradeValuesManager[0]["Value"]) * Convert.ToInt32(DocOffGradeValuesManager[0]["CapacityPerValue"]);
                }
                else
                    MainMember = true;

                drOfficeMembers["MeId"] = OfficeMemberManager[i]["PersonId"];
                drOfficeMembers["MeName"] = MeName;
                drOfficeMembers["MjId"] = MjId;
                drOfficeMembers["GrdId"] = GradeId;
                if (Value != 0)
                    drOfficeMembers["Value"] = Value;
                else
                    drOfficeMembers["Value"] = " --- ";
                drOfficeMembers["GrdTypeId"] = GrdTypeId;
                drOfficeMembers["GrdType"] = GrdType;
                drOfficeMembers["OfpName"] = OfficeMemberManager[i]["OfpName"];
                drOfficeMembers["MainMember"] = MainMember;

                OfficeMembersDT.Rows.Add(drOfficeMembers);
            }

            return OfficeMembersDT;
        }

        /// <summary>
        /// MeId, MeName, MjId, GrdId, Value, GrdTypeId, GrdType, OfpName, MainMember
        /// </summary>
        private DataTable GetImpOfficeMembersDT()
        {
            DataTable OfficeMembersDT = new DataTable();

            OfficeMembersDT.Columns.Add("MeId");
            OfficeMembersDT.Columns.Add("MeName");
            OfficeMembersDT.Columns.Add("MjId");
            OfficeMembersDT.Columns.Add("GrdId");
            OfficeMembersDT.Columns.Add("Value");
            OfficeMembersDT.Columns.Add("GrdTypeId");
            OfficeMembersDT.Columns.Add("GrdType");
            OfficeMembersDT.Columns.Add("OfpName");
            OfficeMembersDT.Columns.Add("MainMember");

            return OfficeMembersDT;
        }

        /// <summary>
        /// ظرفیت مصرف شده فرد، شرکت یا یک دفتر را بر می گرداند
        /// </summary>
        private int GetTotalUsedCapacity(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId)
        {
            int CapacityDecrement = 0;

            ArrayList TempArray = new ArrayList();
            int TotalDsg = 0;
            int TotalObs = 0;
            int UsedDsg = 0;
            int UsedObs = 0;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    TempArray = GetDsgObsTotalCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId);
                    if (TempArray.Count != 0)
                    {
                        TotalDsg = Convert.ToInt32(TempArray[1]);
                        TotalObs = Convert.ToInt32(TempArray[2]);
                        UsedDsg = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOfficeEngOId, MemberTypeId);
                        if (TotalObs != 0)
                            UsedObs = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOfficeEngOId, MemberTypeId) * TotalDsg / TotalObs;
                        CapacityDecrement = UsedDsg + UsedObs;
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    TempArray = GetDsgObsTotalCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId);
                    if (TempArray.Count != 0)
                    {
                        TotalDsg = Convert.ToInt32(TempArray[1]);
                        TotalObs = Convert.ToInt32(TempArray[2]);
                        if (TotalDsg != 0)
                            UsedDsg = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOfficeEngOId, MemberTypeId) * TotalObs / TotalDsg;
                        UsedObs = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOfficeEngOId, MemberTypeId);
                        CapacityDecrement = UsedDsg + UsedObs;
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    CapacityDecrement = UsedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
                    break;
            }
            return CapacityDecrement;
        }

        private int UsedCapacity(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId)
        {
            int CapacityDecrement = 0;
            TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
            ProjectCapacityDecrementManager.FindUsedCapacity(MeOfficeEngOId, ProjectIngridientTypeId, MemberTypeId);
            for (int i = 0; i < ProjectCapacityDecrementManager.Count; i++)
                CapacityDecrement += Convert.ToInt32(ProjectCapacityDecrementManager[i]["CapacityDecrement"]);
            return CapacityDecrement;
        }

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

        /// <summary>
        /// ظرفیت باقیمانده فرد، شرکت یا یک دفتر را بر می گرداند
        /// </summary>
        private int GetTotalRemainCapacity(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId)
        {
            int TotalCapacity = 0;
            int UsedCapacity = 0;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    TotalCapacity = Convert.ToInt32((ArrayList)GetDsgObsTotalCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId)[1]);
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    TotalCapacity = Convert.ToInt32((ArrayList)GetDsgObsTotalCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId)[2]);
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TotalCapacity = Convert.ToInt32((ArrayList)GetImpTotalCapacity(MemberTypeId, MeOfficeEngOId)[1]);
                    break;
            }

            UsedCapacity = GetTotalUsedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);

            return TotalCapacity - UsedCapacity;
        }

        /// <summary>
        /// اطلاعات ظرفیت فرد، شرکت یا یک دفتر را بر می گرداند
        /// ArrayList[0]: TotalCapacity, ArrayList[1]:UsedCapacity , ArrayList[2]: RemainCapacity, ArrayList[3]:ReservedCapacity , ArrayList[4]: ProjectNum, ArrayList[5]: MaxJoubCount, ArrayList[6]: MaxFloor, ArrayList[7]: ConditionalCapacity
        /// </summary>
        private ArrayList GetCapacityInfo(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId)
        {
            ArrayList CapacityArr = new ArrayList();
            ArrayList TempArray = new ArrayList();

            int TotalCapacity = 0;
            int UsedCapacity = 0;
            int RemainCapacity = 0;
            int ReservedCapacity = 0;
            int MaxJoubCount = 0;
            int MaxFloor = 0;
            int ConditionalCapacity = 0;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    TempArray = GetDsgObsTotalCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId);
                    if (TempArray.Count != 0)
                    {
                        TotalCapacity = Convert.ToInt32(TempArray[1]);
                        MaxJoubCount = Convert.ToInt32(TempArray[0]);
                        ConditionalCapacity = Convert.ToInt32(TempArray[3]);
                    }
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    TempArray = GetDsgObsTotalCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId);
                    if (TempArray.Count != 0)
                    {
                        TotalCapacity = Convert.ToInt32(TempArray[2]);
                        MaxJoubCount = Convert.ToInt32(TempArray[0]);
                        ConditionalCapacity = Convert.ToInt32(TempArray[3]);
                    }
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TempArray = GetImpTotalCapacity(MemberTypeId, MeOfficeEngOId);
                    if (TempArray.Count != 0)
                    {
                        TotalCapacity = Convert.ToInt32(TempArray[1]);
                        MaxJoubCount = Convert.ToInt32(TempArray[2]);
                        MaxFloor = Convert.ToInt32(TempArray[0]);
                        ConditionalCapacity = Convert.ToInt32(TempArray[3]);
                    }
                    break;
            }

            UsedCapacity = GetTotalUsedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
            RemainCapacity = TotalCapacity - UsedCapacity;
            ReservedCapacity = GetTotalReservedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
            int ProjectNum = GetTotalProjectNum(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);

            CapacityArr.Add(TotalCapacity);
            CapacityArr.Add(UsedCapacity);
            CapacityArr.Add(RemainCapacity);
            CapacityArr.Add(ReservedCapacity);
            CapacityArr.Add(ProjectNum);
            CapacityArr.Add(MaxJoubCount);
            CapacityArr.Add(MaxFloor);
            CapacityArr.Add(ConditionalCapacity);

            return CapacityArr;
        }

        /// <summary>
        /// اطلاعات ظرفیت اعضا یک شرکت یا یک دفتر را بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MeId, ArrayList[1]: MaxJobCapacity,ArrayList[2]: MaxJobCount, ArrayList[3]: UsedCapacity, ArrayList[4]: RemainCapacity, ArrayList[5]:ReservedCapacity , ArrayList[6]: ProjectNum, ArrayList[7]: MaxFloor, ArrayList[8]: ConditionalCapacity
        /// </summary>
        private ArrayList GetOfficeMembersCapacityInfo(int ProjectIngridientTypeId, int OfficeEngOId, int MemberTypeId)
        {
            // MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, 
            //                     ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc,
            //                     ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName
            //                     ArrayList[14]: ConditionalCapacity

            int CapacityDecrement = 0;


            ArrayList MemberArray = new ArrayList();
            ArrayList UsedCapacityArray = new ArrayList();
            int TotalDsg = 0;
            int TotalObs = 0;
            int UsedDsg = 0;
            int UsedObs = 0;

            int DocOffIncreaseJobCapacityType = 0;
            if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Office)
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office;
            else if (MemberTypeId == (int)TSP.DataManager.TSMemberType.EngOffice)
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    MemberArray = GetOfficeMembers(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType);
                    for (int i = 0; i < MemberArray.Count; i++)
                    {
                        TotalDsg = Convert.ToInt32(MemberArray[10]);
                        TotalObs = Convert.ToInt32(MemberArray[11]);
                        UsedDsg = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member);
                        if (TotalObs != 0)
                            UsedObs = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member) * TotalDsg / TotalObs;
                        CapacityDecrement = UsedDsg + UsedObs;
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member);
                        int ReservedCapacity = GetOfficeMembersTotalReservedCapacity(ProjectIngridientTypeId, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member);

                        ArrayList TempArray = new ArrayList();
                        TempArray.Add(Convert.ToInt32(MemberArray[12]));
                        TempArray.Add(TotalDsg);
                        TempArray.Add(Convert.ToInt32(MemberArray[0]));
                        TempArray.Add(CapacityDecrement);
                        TempArray.Add(TotalDsg - CapacityDecrement);
                        TempArray.Add(ReservedCapacity);
                        TempArray.Add(ProjectNum);
                        TempArray.Add(0);
                        TempArray.Add(Convert.ToInt32(MemberArray[14]));

                        UsedCapacityArray.Add(TempArray);
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    MemberArray = GetOfficeMembers(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType);
                    for (int i = 0; i < MemberArray.Count; i++)
                    {
                        TotalDsg = Convert.ToInt32(MemberArray[10]);
                        TotalObs = Convert.ToInt32(MemberArray[11]);
                        if (TotalDsg != 0)
                            UsedDsg = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member) * TotalObs / TotalDsg;
                        UsedObs = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member);
                        CapacityDecrement = UsedDsg + UsedObs;
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member);
                        int ReservedCapacity = GetOfficeMembersTotalReservedCapacity(ProjectIngridientTypeId, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member);

                        ArrayList TempArray = new ArrayList();
                        TempArray.Add(Convert.ToInt32(MemberArray[12]));
                        TempArray.Add(TotalObs);
                        TempArray.Add(Convert.ToInt32(MemberArray[0]));
                        TempArray.Add(CapacityDecrement);
                        TempArray.Add(TotalObs - CapacityDecrement);
                        TempArray.Add(ReservedCapacity);
                        TempArray.Add(ProjectNum);
                        TempArray.Add(0);
                        TempArray.Add(Convert.ToInt32(MemberArray[14]));

                        UsedCapacityArray.Add(TempArray);
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
                    OfficeMemberManager = GetOfficeMembers(OfficeEngOId, DocOffIncreaseJobCapacityType);

                    for (int i = 0; i < OfficeMemberManager.Count; i++)
                    {
                        // MemberArray -----> ArrayList[0]: MaxFloor(int), ArrayList[1]: MaxJobCapacity(int), ArrayList[2]: MaxUnitCount OR MaxJobCount(int), ArrayList[3]: ConditionalCapacity

                        MemberArray = GetImpTotalCapacity((int)TSP.DataManager.TSMemberType.Member, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]));
                        int TotalCapacity = Convert.ToInt32(MemberArray[1]);
                        CapacityDecrement = OfficeMembersUsedCapacity(ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), (int)TSP.DataManager.TSMemberType.Member);
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), (int)TSP.DataManager.TSMemberType.Member);
                        int ReservedCapacity = GetOfficeMembersTotalReservedCapacity(ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), (int)TSP.DataManager.TSMemberType.Member);
                        int ConditionalCapacity = GetConditionalCapacity(Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), ProjectIngridientTypeId);

                        ArrayList TempArray = new ArrayList();
                        TempArray.Add(Convert.ToInt32(OfficeMemberManager[0]["PersonId"]));
                        TempArray.Add("-----"); //(TotalCapacity);
                        TempArray.Add("-----"); //(Convert.ToInt32(MemberArray[2]));
                        TempArray.Add(CapacityDecrement);
                        TempArray.Add("-----"); //(TotalCapacity - CapacityDecrement);
                        TempArray.Add("-----"); //(ReservedCapacity);
                        TempArray.Add(ProjectNum);
                        TempArray.Add("-----"); //(Convert.ToInt32(MemberArray[0]));
                        TempArray.Add("-----"); //(Convert.ToInt32(MemberArray[3]));

                        UsedCapacityArray.Add(TempArray);
                    }
                    break;
            }
            return UsedCapacityArray;
        }

        /// <summary>
        /// ظرفیت مصرف شده یکی از اعضا یک شرکت یا یک دفتر را بر می گرداند
        /// </summary>
        private int OfficeMembersUsedCapacity(int ProjectIngridientTypeId, int MeOthPId, int MemberTypeId)
        {
            int CapacityDecrement = 0;
            TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
            ProjectOfficeMembersManager.FindUsedCapacity(MeOthPId, ProjectIngridientTypeId, MemberTypeId);
            for (int i = 0; i < ProjectOfficeMembersManager.Count; i++)
                CapacityDecrement += Convert.ToInt32(ProjectOfficeMembersManager[i]["CapacityDecrement"]);
            return CapacityDecrement;
        }

        /// <summary>
        /// تعداد پروژه های در دست اجرا عضوی از یک شرکت یا یک دفتر را بر می گرداند
        /// </summary>
        private int GetOfficeMembersTotalProjectNum(int ProjectIngridientTypeId, int MeOthPId, int MemberTypeId)
        {
            int ProjectNum = 0;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    ProjectNum += OfficeMembersCurrentProjectNum((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOthPId, MemberTypeId);
                    ProjectNum += OfficeMembersCurrentProjectNum((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOthPId, MemberTypeId);
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    ProjectNum += OfficeMembersCurrentProjectNum(ProjectIngridientTypeId, MeOthPId, MemberTypeId);
                    break;
            }

            return ProjectNum;
        }

        private int OfficeMembersCurrentProjectNum(int ProjectIngridientTypeId, int MeOthPId, int MemberTypeId)
        {
            TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
            ProjectOfficeMembersManager.FindUsedCapacity(MeOthPId, ProjectIngridientTypeId, MemberTypeId);
            return ProjectOfficeMembersManager.Count;
        }

        /// <summary>
        /// ظرفیت رزرو شده عضوی از یک شرکت یا یک دفتر را بر می گرداند
        /// </summary>
        private int GetOfficeMembersTotalReservedCapacity(int ProjectIngridientTypeId, int MeOthpId, int MemberTypeId)
        {
            int CapacityDecrement = 0;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    CapacityDecrement = OfficeMembersReservedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOthpId, MemberTypeId);
                    CapacityDecrement = OfficeMembersReservedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOthpId, MemberTypeId);
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    CapacityDecrement = OfficeMembersReservedCapacity(ProjectIngridientTypeId, MeOthpId, MemberTypeId);
                    break;
            }

            return CapacityDecrement;
        }

        private int OfficeMembersReservedCapacity(int ProjectIngridientTypeId, int MeOthpId, int MemberTypeId)
        {
            int CapacityDecrement = 0;
            TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
            ProjectOfficeMembersManager.FindReservedCapacity(MeOthpId, ProjectIngridientTypeId, MemberTypeId);
            for (int i = 0; i < ProjectOfficeMembersManager.Count; i++)
                CapacityDecrement += Convert.ToInt32(ProjectOfficeMembersManager[i]["CapacityDecrement"]);
            return CapacityDecrement;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// پایه یک عضو را بر می گرداند
        /// </summary>
        public int GetMemGrade(int MeId, int ProjectIngridientTypeId)
        {
            return GetGrade(MeId, ProjectIngridientTypeId);
        }

        /// <summary>
        /// پایه یک کاردان یا معمار تجربی را بر می گرداند
        /// </summary>
        public int GetTechniciansGrade(int OtpId, int ProjectIngridientTypeId)
        {
            return GetTechnicianGrade(OtpId, ProjectIngridientTypeId);

        }

        /// <summary>
        /// اعضای فعال شرکت یا دفتر را بر می گرداند
        /// </summary>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable GetOffMembers(int OfficeEngoId, int DocOffIncreaseJobCapacityType)
        {
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = GetOfficeMembers(OfficeEngoId, DocOffIncreaseJobCapacityType);
            return OfficeMemberManager.DataTable;
        }

        /// <summary>
        /// ظرفیت اضافی یا کم شده یک شخص یا شرکت یا دفتر را بر می گرداند
        /// </summary>
        public int GetCIncDecCapacity(int MeOfficeEngOId, int ProjectIngridientTypeId)
        {
            return GetConditionalCapacity(MeOfficeEngOId, ProjectIngridientTypeId);
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک عضو را بر می گرداند
        /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[12]: MeId, ArrayList[13]: MeName, ArrayList[14]: ConditionalCapacity
        /// </summary>
        public ArrayList GetMembersDsgObsCapacity(int MeId, int ProjectIngridientTypeId)
        {
            return GetMemberDsgObsCapacity(MeId, ProjectIngridientTypeId);
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت را بر می گرداند
        /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationCapacity, ArrayList[3]: ConditionalCapacity
        /// </summary>
        public ArrayList GetOfficeDsgCapacity(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType)
        {
            return GetOfficeDsgObsCapacity(OfficeEngoId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType);
        }

        /// <summary>
        /// کل ظرفیت و تعداد کار مجاز فرد، شرکت یا یک دفتر طراحی و نظارت را بر می گرداند
        /// ArrayList[0]: MaxJobCount(int), ArrayList[1]: MaxJobCapacity(int), ArrayList[2]: ObservationCapacity, ArrayList[3]: ConditionalCapacity
        /// </summary>
        public ArrayList GetDsgObsTotalCapacity(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId)
        {
            ArrayList CapacityArr = new ArrayList();
            ArrayList CapArr = new ArrayList();

            switch (MemberTypeId)
            {
                case (int)TSP.DataManager.TSMemberType.Member:
                    CapArr = GetMemberDsgObsCapacity(MeOfficeEngOId, ProjectIngridientTypeId);
                    break;

                case (int)TSP.DataManager.TSMemberType.Office:
                    CapArr = GetOfficeDsgObsCapacity(MeOfficeEngOId, ProjectIngridientTypeId, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office);
                    break;

                case (int)TSP.DataManager.TSMemberType.EngOffice:
                    CapArr = GetOfficeDsgObsCapacity(MeOfficeEngOId, ProjectIngridientTypeId, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice);
                    break;
            }

            if (CapArr.Count != 0)
            {
                CapacityArr.Add(Convert.ToInt32(CapArr[0]));
                CapacityArr.Add(Convert.ToInt32(CapArr[1]));
                if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
                {
                    CapacityArr.Add(Convert.ToInt32(CapArr[3]));
                    CapacityArr.Add(Convert.ToInt32(CapArr[14]));
                }
                else
                {
                    CapacityArr.Add(Convert.ToInt32(CapArr[2]));
                    CapacityArr.Add(Convert.ToInt32(CapArr[3]));
                }
            }

            return CapacityArr;
        }

        /// <summary>
        /// افراد یک دفتر یا شرکت و ظرفیت طراحی و نظارت آنها را بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc, ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName, ArrayList[14]: ConditionalCapacity
        /// </summary>
        public ArrayList GetOfficeMembersDsgObsCapacity(int OfficeId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType)
        {
            return GetOfficeMembers(OfficeId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType);
        }

        /// <summary>
        /// ظرفیت کل اجرا یک عضو را بر می گرداند
        /// ArrayList[0]: MaxFloor, ArrayList[1]: MaxJobCapacity, ArrayList[2]: MaxUnitCount, ArrayList[3]: Grade, ArrayList[4]: ConditionalCapacity
        /// </summary>
        public ArrayList GetMembersImpCapacity(int MeId)
        {
            ArrayList Temp = GetMemberImpCapacity(MeId);
            if (Temp.Count > 0)
            {
                if (Convert.ToInt32(Temp[0]) == -1)
                    Temp[0] = "بدون محدودیت";
            }
            return Temp;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک شرکت را بر می گرداند
        /// ArrayList[0]: MaxFloor, ArrayList[1]:MaxCapacity , ArrayList[2]: MaxJobCount, ArrayList[3]: ConditionalCapacity, ArrayList[4]: GradeId, ArrayList[5]: GrdType
        /// </summary>
        public ArrayList GetOfficesImpCapacity(int OfficeId)
        {
            ArrayList Temp = GetOfficeImpCapacity(OfficeId);
            if (Temp.Count > 0)
            {
                if (Convert.ToInt32(Temp[0]) == -1)
                    Temp[0] = "بدون محدودیت";
                if (Convert.ToInt32(Temp[2]) == -1)
                    Temp[2] = "بدون محدودیت";
            }
            return Temp;
        }

        /// <summary>
        /// کل ظرفیت و تعداد کار و تعداد طبقات مجاز فرد، شرکت یا یک دفتر اجرایی را بر می گرداند
        /// ArrayList[0]: MaxFloor(string), ArrayList[1]: MaxJobCapacity(string), ArrayList[2]: MaxUnitCount OR MaxJobCount(int), ArrayList[3]: ConditionalCapacity
        /// </summary>
        public ArrayList GetImpTotalCapacity(int MemberTypeId, int MeOfficeEngOId)
        {
            ArrayList CapacityArr = new ArrayList();
            ArrayList CapArr = new ArrayList();

            switch (MemberTypeId)
            {
                case (int)TSP.DataManager.TSMemberType.Member:
                    CapArr = GetMemberImpCapacity(MeOfficeEngOId);
                    break;

                case (int)TSP.DataManager.TSMemberType.Office:
                    CapArr = GetOfficeImpCapacity(MeOfficeEngOId);
                    break;
            }

            if (CapArr.Count != 0)
            {
                if (Convert.ToInt32(CapArr[0]) == -1)
                    CapArr[0] = "بدون محدودیت";

                if (Convert.ToInt32(CapArr[2]) == -1)
                    CapArr[2] = "بدون محدودیت";

                CapacityArr.Add(Convert.ToInt32(CapArr[0]));
                CapacityArr.Add(Convert.ToInt32(CapArr[1]));
                CapacityArr.Add(Convert.ToInt32(CapArr[2]));

                if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
                    CapacityArr.Add(Convert.ToInt32(CapArr[4]));
                else
                    CapacityArr.Add(Convert.ToInt32(CapArr[3]));

            }

            return CapacityArr;
        }

        /// <summary>
        /// اعضا یک شرکت اجرا را بر می گرداند
        /// </summary>
        public DataTable GetImpOffMembers(int OfficeId)
        {
            return GetImpOfficeMembers(OfficeId);
        }

        /// <summary>
        /// ظرفیت مصرف شده فرد، شرکت یا یک دفتر را بر می گرداند
        /// </summary>
        public int GetUsedCapacity(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId)
        {
            return GetTotalUsedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
        }

        /// <summary>
        /// تعداد پروژه های در دست اجرا فرد، شرکت یا یک دفتر را بر می گرداند
        /// </summary>
        public int GetProjectNum(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId)
        {
            return GetTotalProjectNum(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
        }

        /// <summary>
        /// ظرفیت رزرو شده فرد، شرکت یا یک دفتر را بر می گرداند
        /// </summary>
        public int GetReservedCapacity(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId)
        {
            return GetTotalReservedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
        }

        /// <summary>
        /// ظرفیت باقیمانده فرد، شرکت یا یک دفتر را بر می گرداند
        /// </summary>
        public int GetRemainCapacity(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId)
        {
            return GetTotalRemainCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId);
        }

        /// <summary>
        /// اطلاعات ظرفیت فرد، شرکت یا یک دفتر را بر می گرداند
        /// ArrayList[0]: TotalCapacity(int), ArrayList[1]:UsedCapacity(int) , ArrayList[2]: RemainCapacity(int), ArrayList[3]:ReservedCapacity(int) , ArrayList[4]: ProjectNum(int), ArrayList[5]: MaxJoubCount(string), ArrayList[6]: MaxFloor(string), ArrayList[7]: ConditionalCapacity(int)
        /// </summary>
        public ArrayList GetCapacityInformation(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId)
        {
            ArrayList Temp = GetCapacityInfo(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId);
            if (Temp.Count > 0)
            {
                if (Convert.ToInt32(Temp[6]) == -1)
                    Temp[6] = "بدون محدودیت";
                if (Convert.ToInt32(Temp[5]) == -1)
                    Temp[5] = "بدون محدودیت";
            }
            return Temp;
        }

        /// <summary>
        /// اطلاعات ظرفیت اعضا یک شرکت یا یک دفتر را بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MeId, ArrayList[1]: MaxJobCapacity,ArrayList[2]: MaxJobCount, ArrayList[3]: UsedCapacity, ArrayList[4]: RemainCapacity, ArrayList[5]:ReservedCapacity, ArrayList[6]: ProjectNum, ArrayList[7]: MaxFloor, ArrayList[8]: ConditionalCapacity(int)
        /// </summary>
        public ArrayList GetOfficeMembersCapacityInformation(int ProjectIngridientTypeId, int OfficeEngOId, int MemberTypeId)
        {
            ArrayList Temp = new ArrayList();
            Temp = GetOfficeMembersCapacityInfo(ProjectIngridientTypeId, OfficeEngOId, MemberTypeId);
            if (Temp.Count > 0)
            {
                if (Convert.ToInt32(Temp[2]) == -1)
                    Temp[2] = "بدون محدودیت";

                if (Convert.ToInt32(Temp[7]) == -1)
                    Temp[7] = "بدون محدودیت";
            }
            return Temp;
        }

        #endregion

        #endregion

        #region CurrentCapacityAssignment

        #region Private-Methods

        /// <summary>
        /// اختصاص ظرفیت مرحله جاری را بر می گرداند
        /// ArrayList[0] = Year, ArrayList[1] = StageText, ArrayList[0] = CapacityPrcnt, ArrayList[1] = JobCountPrcnt
        /// </summary>
        private ArrayList GetCurrentYearAndStage()
        {
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            CapacityAssignmentManager.SelectCurrentYearAndStage();

            ArrayList CurrentPrcntsSumArr = GetCurrentPrcntsSum();

            ArrayList CurrentStageArr = new ArrayList();
            if (CapacityAssignmentManager.Count > 0 && CurrentPrcntsSumArr.Count != 0)
            {
                CurrentStageArr.Add(CapacityAssignmentManager[0]["Year"]);
                CurrentStageArr.Add(CapacityAssignmentManager[0]["StageText"]);
                CurrentStageArr.Add(CurrentPrcntsSumArr[0]);
                CurrentStageArr.Add(CurrentPrcntsSumArr[1]);
            }

            return CurrentStageArr;
        }

        /// <summary>
        /// درصد اختصاص ظرفیت جاری را بر می گرداند
        /// ArrayList[0] = CapacityPrcntSum, ArrayList[1] = JobCountPrcntSum
        /// </summary>
        private ArrayList GetCurrentPrcntsSum()
        {
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            return CapacityAssignmentManager.GetCurrentPrcntsSum();
        }

        /// <summary>
        /// مقدار ظرفیت را بر اساس درصد اختصاص ظرفیت جاری محاسبه می کند
        /// ArrayList[0] = MaxJobCount, ArrayList[1] = MaxJobCapacity
        /// </summary>
        private ArrayList CalculateMaxJobCount(int MaxJobCount, int MaxJobCapacity)
        {
            ArrayList CapacityAssArr = GetCurrentPrcntsSum();

            int CapacityPrcntSum = Convert.ToInt32(CapacityAssArr[0]);
            int JobCountPrcntSum = Convert.ToInt32(CapacityAssArr[1]);

            CapacityAssArr[0] = Math.Ceiling(Convert.ToDouble(JobCountPrcntSum * MaxJobCount) / 100);
            CapacityAssArr[1] = Math.Ceiling(Convert.ToDouble(CapacityPrcntSum * MaxJobCapacity) / 100);

            return CapacityAssArr;
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک عضو را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[12]: MeId, ArrayList[13]: MeName, ArrayList[14]: ConditionalCapacity
        /// </summary>
        private ArrayList GetMemberDsgObsCapacityPerStage(int MeId, int ProjectIngridientTypeId)
        {
            ArrayList MemberArr = new ArrayList();
            int Grade = GetGrade(MeId, ProjectIngridientTypeId);
            if (Grade != 0)
            {
                int ConditionalCapacity = GetConditionalCapacity(MeId, ProjectIngridientTypeId);
                int MjId = 0;
                ArrayList MjArray = GetMajor(MeId);
                if (MjArray.Count > 0)
                    MjId = Convert.ToInt32(MjArray[0]);

                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                MemberManager.FindByCode(MeId);

                TSP.DataManager.DocOffMemberCapacityManager MemberCapacityManager = new TSP.DataManager.DocOffMemberCapacityManager();
                MemberCapacityManager.FindByGrdId(Grade);

                if (MemberCapacityManager.Count > 0)
                {
                    //CapacityAssArr ----> ArrayList[0] = MaxJobCount, ArrayList[1] = MaxJobCapacity
                    ArrayList CapacityAssArr = CalculateMaxJobCount(Convert.ToInt32(MemberCapacityManager[0]["MaxJobCount"]), Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]));

                    MemberArr.Add(CapacityAssArr[0].ToString());//MemberArr[0]
                    MemberArr.Add((Convert.ToInt32(CapacityAssArr[1]) + ConditionalCapacity).ToString());//MemberArr[1]
                    MemberArr.Add(MemberCapacityManager[0]["ObservationPercent"].ToString());//MemberArr[2]
                    MemberArr.Add((Convert.ToInt32(Convert.ToDouble(MemberArr[1]) * Convert.ToDouble(MemberArr[2]))) + ConditionalCapacity);//MemberArr[3]
                    MemberArr.Add(Grade.ToString());
                    MemberArr.Add(MjId.ToString());
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add(MeId.ToString());
                    MemberArr.Add(MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString());
                    MemberArr.Add(ConditionalCapacity);
                }
            }
            return MemberArr;
        }

        /// <summary>
        /// افراد یک دفتر یا شرکت و ظرفیت طراحی و نظارت آنها را بر اساس اختصاص ظرفیت بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc, ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName, ArrayList[14]: ConditionalCapacity
        /// </summary>
        private ArrayList GetOfficeMembersPerStage(int OfficeId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType)
        {
            // MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, 
            //                     ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc,
            //                     ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName
            //                     ArrayList[14]: ConditionalCapacity

            ArrayList MembersArr = new ArrayList();
            TSP.DataManager.DocOffMajorNum DocOffMajorNum = new TSP.DataManager.DocOffMajorNum();
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocOffIncreaseJobCapacityManager IncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();
            //*****اعضای شرکت را بدست می آورد
            OfficeMemberManager = GetOfficeMembers(OfficeId, DocOffIncreaseJobCapacityType);

            //****برای هر عضو تعداد کار  و حداکثر ظرفیت اشتغال در یک سال را براساس پایه آن بدست می آورد / جدول 1 صفحه 26   
            int k = 0;
            for (int i = 0; i < OfficeMemberManager.Count; i++)
            {
                if (Convert.ToInt32(OfficeMemberManager[i]["OfmType"]) == (int)TSP.DataManager.OfficeMemberType.Member)
                {
                    ArrayList Member = GetMemberDsgObsCapacityPerStage(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId);
                    if (Member.Count != 0)
                    {
                        MembersArr.Add(Member);
                        ((ArrayList)MembersArr[k])[6] = GetGradeByMFId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId).ToString();
                        k++;
                    }
                }
            }
            //****************************************************
            if (MembersArr.Count != 0)
            {
                //*******ترکیب رشته شرکا را بدست می آورد
                ArrayList MajorArr = GetMajorNum(MembersArr);
                //*******************************************            
                DocOffMajorNum.FindByMajorsNum((int)MajorArr[0], (int)MajorArr[1], (int)MajorArr[2]);
                //*******بر اساس ترکیب رشته شرکا درصد افزایش را پیدا می کند
                IncreaseJobCapacityManager.FindByMNumId(Convert.ToInt32(DocOffMajorNum[0]["MNumId"]), DocOffIncreaseJobCapacityType);

                for (int i = 0; i < MembersArr.Count; i++)
                {
                    bool SameGradeInc = false;
                    bool MajorInc = false;

                    ((ArrayList)MembersArr[i])[7] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["DesignIncPer"]) / 100).ToString();
                    for (int j = 0; j < MembersArr.Count; j++)
                    {
                        if (i != j)
                        {
                            if (((ArrayList)MembersArr[i])[5].ToString() == ((ArrayList)MembersArr[j])[5].ToString())
                            {
                                if (((ArrayList)MembersArr[i])[6].ToString() == ((ArrayList)MembersArr[j])[6].ToString())
                                {
                                    if (!MajorInc)
                                        SameGradeInc = true;
                                }
                                else
                                    SameGradeInc = false;

                                MajorInc = true;
                            }

                        }
                    }
                    if (SameGradeInc)
                        ((ArrayList)MembersArr[i])[8] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["SameGradeIncPer"]) / 100).ToString();

                    if (MajorInc)
                        ((ArrayList)MembersArr[i])[9] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["MajorIncPer"]) / 100).ToString();

                    ((ArrayList)MembersArr[i])[10] = Convert.ToInt32(((ArrayList)MembersArr[i])[1]) + Convert.ToInt32(((ArrayList)MembersArr[i])[7]) + Convert.ToInt32(((ArrayList)MembersArr[i])[8]) + Convert.ToInt32(((ArrayList)MembersArr[i])[9]);
                    ((ArrayList)MembersArr[i])[11] = Convert.ToInt32(Convert.ToDouble(((ArrayList)MembersArr[i])[2]) * (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) + Convert.ToInt32(((ArrayList)MembersArr[i])[7]) + Convert.ToInt32(((ArrayList)MembersArr[i])[8]) + Convert.ToInt32(((ArrayList)MembersArr[i])[9])));
                }
            }
            return MembersArr;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک عضو را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: MaxFloor, ArrayList[1]: MaxJobCapacity, ArrayList[2]: MaxUnitCount, ArrayList[3]: Grade, ArrayList[4]: ConditionalCapacity
        /// </summary>
        private ArrayList GetMemberImpCapacityPerStage(int MeId)
        {
            ArrayList MemberArr = new ArrayList();
            int Grade = GetGrade(MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
            if (Grade != 0)
            {
                int ConditionalCapacity = GetConditionalCapacity(MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);

                TSP.DataManager.DocOffEngOfficeImpQualificationManager EngOfficeImpQualificationManager = new TSP.DataManager.DocOffEngOfficeImpQualificationManager();
                EngOfficeImpQualificationManager.FindByGrdId(Grade);

                if (EngOfficeImpQualificationManager.Count > 0)
                {
                    //CapacityAssArr ----> ArrayList[0] = MaxJobCount, ArrayList[1] = MaxJobCapacity
                    ArrayList CapacityAssArr = CalculateMaxJobCount(Convert.ToInt32(EngOfficeImpQualificationManager[0]["MaxUnitCount"]), Convert.ToInt32(EngOfficeImpQualificationManager[0]["MaxJobCapacity"]));

                    MemberArr.Add(EngOfficeImpQualificationManager[0]["MaxFloor"].ToString());
                    MemberArr.Add((Convert.ToInt32(CapacityAssArr[1]) + ConditionalCapacity).ToString());
                    MemberArr.Add(CapacityAssArr[0].ToString());
                    MemberArr.Add(Grade.ToString());
                    MemberArr.Add(ConditionalCapacity);
                }
            }
            return MemberArr;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک شرکت را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: MaxFloor, ArrayList[1]:MaxCapacity , ArrayList[2]: MaxJobCount, ArrayList[3]: ConditionalCapacity, ArrayList[4]: GradeId, ArrayList[5]: GrdType
        /// </summary>
        private ArrayList GetOfficeImpCapacityPerStage(int OfficeId)
        {
            // GradeArr-----> ArrayList[0]: GradeId, ArrayList[1]: Type, ArrayList[2]: CivilGrdId, ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId

            ArrayList GradeArr = GetOfficeImpGrade(OfficeId);
            TSP.DataManager.DocOffOfficeMembersQualificationManager OfficeMembersQualificationManager = new TSP.DataManager.DocOffOfficeMembersQualificationManager();

            ArrayList CapacityArr = new ArrayList();

            if (GradeArr.Count != 0)
            {
                int ConditionalCapacity = GetConditionalCapacity(OfficeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);

                if ((int)GradeArr[1] == (int)TSP.DataManager.DocOffOfficeMembersQualificationType.Kardan_Kardan)
                    OfficeMembersQualificationManager.FindByGrdId((int)GradeArr[0], (int)GradeArr[1], (int)GradeArr[2]);
                else
                    OfficeMembersQualificationManager.FindByGrdId((int)GradeArr[0], (int)GradeArr[1], null);

                if (OfficeMembersQualificationManager.Count > 0)
                {
                    //CapacityAssArr ----> ArrayList[0] = MaxJobCount, ArrayList[1] = MaxJobCapacity
                    ArrayList CapacityAssArr = CalculateMaxJobCount(Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxJobCount"]), Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxCapacity"]) + GetCapacityOfPoints(OfficeId, Convert.ToInt32(GradeArr[3]), Convert.ToInt32(GradeArr[4]), Convert.ToInt32(OfficeMembersQualificationManager[0]["PointsLimitation"])));

                    CapacityArr.Add(Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxFloor"]));
                    CapacityArr.Add(Convert.ToInt32(CapacityAssArr[1]) + ConditionalCapacity);
                    CapacityArr.Add(Convert.ToInt32(CapacityAssArr[0]));
                    CapacityArr.Add(ConditionalCapacity);
                    CapacityArr.Add(GradeArr[0]);
                    CapacityArr.Add(GradeArr[1]);
                }
            }

            return CapacityArr;
        }

        /// <summary>
        /// ظرفیت مصرف شده فرد، شرکت یا یک دفتر را بر اساس اختصاص ظرفیت بر می گرداند
        /// </summary>
        private int GetTotalUsedCapacityPerStage(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId)
        {
            int CapacityDecrement = 0;

            ArrayList TempArray = new ArrayList();
            int TotalDsg = 0;
            int TotalObs = 0;
            int UsedDsg = 0;
            int UsedObs = 0;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    TempArray = GetDsgObsTotalCapacityPerStage(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId);
                    if (TempArray.Count != 0)
                    {
                        TotalDsg = Convert.ToInt32(TempArray[1]);
                        TotalObs = Convert.ToInt32(TempArray[2]);
                        UsedDsg = UsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOfficeEngOId, MemberTypeId);
                        if (TotalObs != 0)
                            UsedObs = UsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOfficeEngOId, MemberTypeId) * TotalDsg / TotalObs;
                        CapacityDecrement = UsedDsg + UsedObs;
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    TempArray = GetDsgObsTotalCapacityPerStage(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId);
                    if (TempArray.Count != 0)
                    {
                        TotalDsg = Convert.ToInt32(TempArray[1]);
                        TotalObs = Convert.ToInt32(TempArray[2]);
                        if (TotalDsg != 0)
                            UsedDsg = UsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOfficeEngOId, MemberTypeId) * TotalObs / TotalDsg;
                        UsedObs = UsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOfficeEngOId, MemberTypeId);
                        CapacityDecrement = UsedDsg + UsedObs;
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    CapacityDecrement = UsedCapacityPerStage(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
                    break;
            }
            return CapacityDecrement;
        }

        private int UsedCapacityPerStage(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId)
        {
            int CapacityDecrement = 0;
            TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
            ProjectCapacityDecrementManager.FindUsedCapacityPerStage(MeOfficeEngOId, ProjectIngridientTypeId, MemberTypeId);
            for (int i = 0; i < ProjectCapacityDecrementManager.Count; i++)
                CapacityDecrement += Convert.ToInt32(ProjectCapacityDecrementManager[i]["CapacityDecrement"]);
            return CapacityDecrement;
        }
        //********************
        /// <summary>
        /// ظرفیت مصرف شده فرد، شرکت یا یک دفتر را در تاریخ خاص بر می گرداند
        /// </summary>
        private int GetTotalUsedCapacity(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId, int? TotalDsg, int? TotalObs, string StartDate, string EndDate)
        {
            int CapacityDecrement = 0;
            int UsedDsg = 0;
            int UsedObs = 0;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    UsedDsg = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOfficeEngOId, MemberTypeId, StartDate, EndDate);
                    if (TotalObs != 0)
                        UsedObs = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOfficeEngOId, MemberTypeId, StartDate, EndDate) * (int)TotalDsg / (int)TotalObs;
                    CapacityDecrement = UsedDsg + UsedObs;
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    if (TotalDsg != 0)
                        UsedDsg = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOfficeEngOId, MemberTypeId, StartDate, EndDate) * (int)TotalObs / (int)TotalDsg;
                    UsedObs = UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOfficeEngOId, MemberTypeId, StartDate, EndDate);
                    CapacityDecrement = UsedDsg + UsedObs;
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    CapacityDecrement = UsedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId, StartDate, EndDate);
                    break;
            }
            return CapacityDecrement;
        }

        private int UsedCapacity(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId, string StartDate, string EndDate)
        {
            int CapacityDecrement = 0;
            TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
            ProjectCapacityDecrementManager.FindUsedCapacityByDate(MeOfficeEngOId, ProjectIngridientTypeId, MemberTypeId, StartDate, EndDate);
            for (int i = 0; i < ProjectCapacityDecrementManager.Count; i++)
                CapacityDecrement += Convert.ToInt32(ProjectCapacityDecrementManager[i]["CapacityDecrement"]);
            return CapacityDecrement;
        }
        //*******************

        /// <summary>
        /// اطلاعات ظرفیت اعضا یک شرکت یا یک دفتر را بر اساس اختصاص ظرفیت بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MeId, ArrayList[1]: MaxJobCapacity,ArrayList[2]: MaxJobCount, ArrayList[3]: UsedCapacity, ArrayList[4]: RemainCapacity, ArrayList[5]:ReservedCapacity , ArrayList[6]: ProjectNum, ArrayList[7]: MaxFloor, ArrayList[8]: ConditionalCapacity
        /// </summary>
        private ArrayList GetOfficeMembersCapacityInfoPerStage(int ProjectIngridientTypeId, int OfficeEngOId, int MemberTypeId)
        {
            // MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, 
            //                     ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc,
            //                     ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName
            //                     ArrayList[14]: ConditionalCapacity

            int CapacityDecrement = 0;


            ArrayList MemberArray = new ArrayList();
            ArrayList UsedCapacityArray = new ArrayList();
            int TotalDsg = 0;
            int TotalObs = 0;
            int UsedDsg = 0;
            int UsedObs = 0;

            int DocOffIncreaseJobCapacityType = 0;
            if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Office)
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office;
            else if (MemberTypeId == (int)TSP.DataManager.TSMemberType.EngOffice)
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    MemberArray = GetOfficeMembersPerStage(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType);
                    for (int i = 0; i < MemberArray.Count; i++)
                    {
                        TotalDsg = Convert.ToInt32(((ArrayList)MemberArray[i])[10]);
                        TotalObs = Convert.ToInt32(((ArrayList)MemberArray[i])[11]);
                        UsedDsg = OfficeMembersUsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Designer, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member);
                        if (TotalObs != 0)
                            UsedObs = OfficeMembersUsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Observer, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member) * TotalDsg / TotalObs;
                        CapacityDecrement = UsedDsg + UsedObs;
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member);
                        int ReservedCapacity = GetOfficeMembersTotalReservedCapacity(ProjectIngridientTypeId, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member);

                        ArrayList TempArray = new ArrayList();
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[12]));
                        TempArray.Add(TotalDsg);
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[0]));
                        TempArray.Add(CapacityDecrement);
                        TempArray.Add(TotalDsg - CapacityDecrement);
                        TempArray.Add(ReservedCapacity);
                        TempArray.Add(ProjectNum);
                        TempArray.Add(0);
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[14]));

                        UsedCapacityArray.Add(TempArray);
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    MemberArray = GetOfficeMembersPerStage(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType);
                    for (int i = 0; i < MemberArray.Count; i++)
                    {
                        TotalDsg = Convert.ToInt32(((ArrayList)MemberArray[i])[10]);
                        TotalObs = Convert.ToInt32(((ArrayList)MemberArray[i])[11]);
                        if (TotalDsg != 0)
                            UsedDsg = OfficeMembersUsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Designer, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member) * TotalObs / TotalDsg;
                        UsedObs = OfficeMembersUsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Observer, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member);
                        CapacityDecrement = UsedDsg + UsedObs;
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member);
                        int ReservedCapacity = GetOfficeMembersTotalReservedCapacity(ProjectIngridientTypeId, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member);

                        ArrayList TempArray = new ArrayList();
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[12]));
                        TempArray.Add(TotalObs);
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[0]));
                        TempArray.Add(CapacityDecrement);
                        TempArray.Add(TotalObs - CapacityDecrement);
                        TempArray.Add(ReservedCapacity);
                        TempArray.Add(ProjectNum);
                        TempArray.Add(0);
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[14]));

                        UsedCapacityArray.Add(TempArray);
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
                    OfficeMemberManager = GetOfficeMembers(OfficeEngOId, DocOffIncreaseJobCapacityType);

                    for (int i = 0; i < OfficeMemberManager.Count; i++)
                    {
                        // MemberArray -----> ArrayList[0]: MaxFloor(int), ArrayList[1]: MaxJobCapacity(int), ArrayList[2]: MaxUnitCount OR MaxJobCount(int), ArrayList[3]: ConditionalCapacity

                        MemberArray = GetImpTotalCapacityPerStage((int)TSP.DataManager.TSMemberType.Member, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]));
                        int TotalCapacity = Convert.ToInt32(MemberArray[1]);
                        CapacityDecrement = OfficeMembersUsedCapacityPerStage(ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), (int)TSP.DataManager.TSMemberType.Member);
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), (int)TSP.DataManager.TSMemberType.Member);
                        int ReservedCapacity = GetOfficeMembersTotalReservedCapacity(ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), (int)TSP.DataManager.TSMemberType.Member);
                        int ConditionalCapacity = GetConditionalCapacity(Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), ProjectIngridientTypeId);

                        ArrayList TempArray = new ArrayList();
                        TempArray.Add(Convert.ToInt32(OfficeMemberManager[0]["PersonId"]));
                        TempArray.Add("-----"); //(TotalCapacity);
                        TempArray.Add("-----"); //(Convert.ToInt32(MemberArray[2]));
                        TempArray.Add(CapacityDecrement);
                        TempArray.Add("-----"); //(TotalCapacity - CapacityDecrement);
                        TempArray.Add("-----"); //(ReservedCapacity);
                        TempArray.Add(ProjectNum);
                        TempArray.Add("-----"); //(Convert.ToInt32(MemberArray[0]));
                        TempArray.Add("-----"); //(Convert.ToInt32(MemberArray[3]));

                        UsedCapacityArray.Add(TempArray);
                    }
                    break;
            }
            return UsedCapacityArray;
        }

        /// <summary>
        /// اطلاعات ظرفیت اعضا یک شرکت یا یک دفتر طراحی و نظارت را بر اساس اختصاص ظرفیت بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MeId, ArrayList[1]: MaxJobCapacity,ArrayList[2]: MaxJobCount, ArrayList[3]: UsedCapacity, ArrayList[4]: RemainCapacity, ArrayList[5]:ReservedCapacity , ArrayList[6]: ProjectNum, ArrayList[7]: MaxFloor, ArrayList[8]: ConditionalCapacity, ArrayList[9]: MeName, ArrayList[10]: Grade, ArrayList[11]: MjId
        /// </summary>
        private ArrayList GetDsgnOfficeMembersCapacityInfoPerStage(int ProjectIngridientTypeId, int OfficeEngOId, int MemberTypeId)
        {
            // MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, 
            //                     ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc,
            //                     ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName
            //                     ArrayList[14]: ConditionalCapacity

            int CapacityDecrement = 0;


            ArrayList MemberArray = new ArrayList();
            ArrayList UsedCapacityArray = new ArrayList();
            int TotalDsg = 0;
            int TotalObs = 0;
            int UsedDsg = 0;
            int UsedObs = 0;

            int DocOffIncreaseJobCapacityType = 0;
            if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Office)
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office;
            else if (MemberTypeId == (int)TSP.DataManager.TSMemberType.EngOffice)
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    #region Designer
                    MemberArray = GetOfficeMembersPerStage(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType);
                    for (int i = 0; i < MemberArray.Count; i++)
                    {
                       // ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity
                        TotalDsg = Convert.ToInt32(((ArrayList)MemberArray[i])[10]);
                        TotalObs = Convert.ToInt32(((ArrayList)MemberArray[i])[11]);
                        UsedDsg = OfficeMembersUsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Designer, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member);
                        if (TotalObs != 0)
                            UsedObs = OfficeMembersUsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Observer, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member) * TotalDsg / TotalObs;
                        CapacityDecrement = UsedDsg + UsedObs;
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member);
                        int ReservedCapacity = GetOfficeMembersTotalReservedCapacity(ProjectIngridientTypeId, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member);

                        ArrayList TempArray = new ArrayList();
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[12]));
                        TempArray.Add(TotalDsg);
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[0]));
                        TempArray.Add(CapacityDecrement);
                        TempArray.Add(TotalDsg - CapacityDecrement);
                        TempArray.Add(ReservedCapacity);
                        TempArray.Add(ProjectNum);
                        TempArray.Add(0);
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[14]));
                        TempArray.Add(((ArrayList)MemberArray[i])[13]);
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[4]));
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[5]));

                        UsedCapacityArray.Add(TempArray);
                    }
                    #endregion
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    #region Observer
                    MemberArray = GetOfficeMembersPerStage(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType);
                    for (int i = 0; i < MemberArray.Count; i++)
                    {
                        TotalDsg = Convert.ToInt32(((ArrayList)MemberArray[i])[10]);
                        TotalObs = Convert.ToInt32(((ArrayList)MemberArray[i])[11]);
                        if (TotalDsg != 0)
                            UsedDsg = OfficeMembersUsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Designer, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member) * TotalObs / TotalDsg;
                        UsedObs = OfficeMembersUsedCapacityPerStage((int)TSP.DataManager.TSProjectIngridientType.Observer, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member);
                        CapacityDecrement = UsedDsg + UsedObs;
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member);
                        int ReservedCapacity = GetOfficeMembersTotalReservedCapacity(ProjectIngridientTypeId, Convert.ToInt32(((ArrayList)MemberArray[i])[12]), (int)TSP.DataManager.TSMemberType.Member);

                        ArrayList TempArray = new ArrayList();
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[12]));
                        TempArray.Add(TotalObs);
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[0]));
                        TempArray.Add(CapacityDecrement);
                        TempArray.Add(TotalObs - CapacityDecrement);
                        TempArray.Add(ReservedCapacity);
                        TempArray.Add(ProjectNum);
                        TempArray.Add(0);
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[14]));
                        TempArray.Add(((ArrayList)MemberArray[i])[13]);
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[4]));
                        TempArray.Add(Convert.ToInt32(((ArrayList)MemberArray[i])[5]));

                        UsedCapacityArray.Add(TempArray);
                    }
                    #endregion
                    break;
            }
            return UsedCapacityArray;
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

        /// <summary>
        /// اطلاعات ظرفیت یکی از اعضا یک شرکت یا یک دفتر طراحی و نظارت را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: MeId, ArrayList[1]: MaxJobCapacity,ArrayList[2]: MaxJobCount, ArrayList[3]: UsedCapacity, ArrayList[4]: RemainCapacity, ArrayList[5]:ReservedCapacity , ArrayList[6]: ProjectNum, ArrayList[7]: MaxFloor, ArrayList[8]: ConditionalCapacity, ArrayList[9]: MeName, ArrayList[10]: Grade, ArrayList[11]: MjId
        /// </summary>
        private ArrayList GetDsgnOfficeMembersInfoPerStage(int ProjectIngridientTypeId, int OfficeEngOId, int MeId, int MemberTypeId)
        {
            ArrayList MemArr = GetDsgOfficeMembersCapacityInformationPerStage(ProjectIngridientTypeId, OfficeEngOId, MemberTypeId);
            for (int i = 0; i < MemArr.Count; i++)
                if (Convert.ToInt32(((ArrayList)MemArr[i])[0]) == MeId)
                    return (ArrayList)MemArr[i];

            ArrayList Temp = new ArrayList();
            return Temp;
        }


        #endregion

        #region Public-Methods

        /// <summary>
        /// اختصاص ظرفیت مرحله جاری را بر می گرداند
        /// ArrayList[0] = Year, ArrayList[1] = StageText, ArrayList[0] = CapacityPrcnt, ArrayList[1] = JobCountPrcnt
        /// </summary>
        public ArrayList GetCurrentStage()
        {
            return GetCurrentYearAndStage();
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک عضو را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[12]: MeId, ArrayList[13]: MeName, ArrayList[14]: ConditionalCapacity
        /// </summary>            
        public ArrayList GetMembersDsgObsCapacityPerStage(int MeId, int ProjectIngridientTypeId)
        {
            return GetMemberDsgObsCapacityPerStage(MeId, ProjectIngridientTypeId);
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationCapacity, ArrayList[3]: ConditionalCapacity
        /// </summary>
        public ArrayList GetOfficeDsgCapacityPerStage(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType)
        {
            return GetOfficeDsgObsCapacityPerStage(OfficeEngoId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType);
        }



        /// <summary>
        /// افراد یک دفتر یا شرکت و ظرفیت طراحی و نظارت آنها را بر اساس اختصاص ظرفیت بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc, ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName, ArrayList[14]: ConditionalCapacity
        /// </summary>
        public ArrayList GetOfficeMembersDsgObsCapacityPerStage(int OfficeId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType)
        {
            return GetOfficeMembersPerStage(OfficeId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType);
        }

        /// <summary>
        /// ظرفیت کل اجرا یک عضو را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: MaxFloor, ArrayList[1]: MaxJobCapacity, ArrayList[2]: MaxUnitCount, ArrayList[3]: Grade, ArrayList[4]: ConditionalCapacity
        /// </summary>
        public ArrayList GetMembersImpCapacityPerStage(int MeId)
        {
            ArrayList Temp = GetMemberImpCapacityPerStage(MeId);
            if (Temp.Count > 0)
            {
                if (Convert.ToInt32(Temp[0]) == -1)
                    Temp[0] = "بدون محدودیت";
            }
            return Temp;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک شرکت را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: MaxFloor, ArrayList[1]:MaxCapacity , ArrayList[2]: MaxJobCount, ArrayList[3]: ConditionalCapacity, ArrayList[4]: GradeId, ArrayList[5]: GrdType
        /// </summary>
        public ArrayList GetOfficesImpCapacityPerStage(int OfficeId)
        {
            ArrayList Temp = GetOfficeImpCapacityPerStage(OfficeId);
            if (Temp.Count > 0)
            {
                if (Convert.ToInt32(Temp[0]) == -1)
                    Temp[0] = "بدون محدودیت";
                if (Convert.ToInt32(Temp[2]) == -1)
                    Temp[2] = "بدون محدودیت";
            }
            return Temp;
        }

        /// <summary>
        /// کل ظرفیت و تعداد کار و تعداد طبقات مجاز فرد، شرکت یا یک دفتر اجرایی را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: MaxFloor(string), ArrayList[1]: MaxJobCapacity(string), ArrayList[2]: MaxUnitCount OR MaxJobCount(int), ArrayList[3]: ConditionalCapacity
        /// </summary>
        public ArrayList GetImpTotalCapacityPerStage(int MemberTypeId, int MeOfficeEngOId)
        {
            ArrayList CapacityArr = new ArrayList();
            ArrayList CapArr = new ArrayList();

            switch (MemberTypeId)
            {
                case (int)TSP.DataManager.TSMemberType.Member:
                    CapArr = GetMemberImpCapacityPerStage(MeOfficeEngOId);
                    break;

                case (int)TSP.DataManager.TSMemberType.Office:
                    CapArr = GetOfficeImpCapacityPerStage(MeOfficeEngOId);
                    break;
            }

            if (CapArr.Count != 0)
            {
                if (Convert.ToInt32(CapArr[0]) == -1)
                    CapArr[0] = "بدون محدودیت";

                if (Convert.ToInt32(CapArr[2]) == -1)
                    CapArr[2] = "بدون محدودیت";

                CapacityArr.Add(Convert.ToInt32(CapArr[0]));
                CapacityArr.Add(Convert.ToInt32(CapArr[1]));
                CapacityArr.Add(Convert.ToInt32(CapArr[2]));
                if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
                    CapacityArr.Add(Convert.ToInt32(CapArr[4]));
                else
                    CapacityArr.Add(Convert.ToInt32(CapArr[3]));
            }

            return CapacityArr;
        }

        /// <summary>
        /// ظرفیت مصرف شده فرد، شرکت یا یک دفتر را بر اساس اختصاص ظرفیت بر می گرداند
        /// </summary>
        public int GetUsedCapacityPerStage(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId)
        {
            return GetTotalUsedCapacityPerStage(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
        }



        /// <summary>
        /// اطلاعات ظرفیت اعضا یک شرکت یا یک دفتر را بر اساس اختصاص ظرفیت بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MeId, ArrayList[1]: MaxJobCapacity,ArrayList[2]: MaxJobCount, ArrayList[3]: UsedCapacity, ArrayList[4]: RemainCapacity, ArrayList[5]:ReservedCapacity, ArrayList[6]: ProjectNum, ArrayList[7]: MaxFloor, ArrayList[8]: ConditionalCapacity(int)
        /// </summary>
        public ArrayList GetOfficeMembersCapacityInformationPerStage(int ProjectIngridientTypeId, int OfficeEngOId, int MemberTypeId)
        {
            ArrayList Temp = new ArrayList();
            Temp = GetOfficeMembersCapacityInfoPerStage(ProjectIngridientTypeId, OfficeEngOId, MemberTypeId);
            if (Temp.Count > 0)
            {
                for (int i = 0; i < Temp.Count; i++)
                {
                    if (Convert.ToInt32(((ArrayList)Temp[i])[2]) == -1)
                        Temp[2] = "بدون محدودیت";

                    if (Convert.ToInt32(((ArrayList)Temp[i])[7]) == -1)
                        Temp[7] = "بدون محدودیت";
                }
            }
            return Temp;
        }

        /// <summary>
        /// اطلاعات ظرفیت اعضا یک شرکت یا یک دفتر طراحی و نظارت را بر اساس اختصاص ظرفیت بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MeId, ArrayList[1]: MaxJobCapacity,ArrayList[2]: MaxJobCount, ArrayList[3]: UsedCapacity, ArrayList[4]: RemainCapacity, ArrayList[5]:ReservedCapacity, ArrayList[6]: ProjectNum, ArrayList[7]: MaxFloor, ArrayList[8]: ConditionalCapacity(int), ArrayList[9]: MeName, ArrayList[10]: Grade, ArrayList[11]: MjId
        /// </summary>
        public ArrayList GetDsgOfficeMembersCapacityInformationPerStage(int ProjectIngridientTypeId, int OfficeEngOId, int MemberTypeId)
        {
            ArrayList Temp = new ArrayList();
            Temp = GetDsgnOfficeMembersCapacityInfoPerStage(ProjectIngridientTypeId, OfficeEngOId, MemberTypeId);
            if (Temp.Count > 0)
            {
                for (int i = 0; i < Temp.Count; i++)
                {
                    if (Convert.ToInt32(((ArrayList)Temp[i])[2]) == -1)
                        Temp[2] = "بدون محدودیت";

                    if (Convert.ToInt32(((ArrayList)Temp[i])[7]) == -1)
                        Temp[7] = "بدون محدودیت";
                }
            }
            return Temp;
        }

        /// <summary>
        /// اطلاعات ظرفیت یکی از اعضا یک شرکت یا یک دفتر طراحی و نظارت را بر اساس اختصاص ظرفیت بر می گرداند
        /// ArrayList[0]: MeId, ArrayList[1]: MaxJobCapacity,ArrayList[2]: MaxJobCount, ArrayList[3]: UsedCapacity, ArrayList[4]: RemainCapacity, ArrayList[5]:ReservedCapacity , ArrayList[6]: ProjectNum, ArrayList[7]: MaxFloor, ArrayList[8]: ConditionalCapacity, ArrayList[9]: MeName, ArrayList[10]: Grade, ArrayList[11]: MjId
        /// </summary>
        public ArrayList GetDsgnOfficeMembersInformationPerStage(int ProjectIngridientTypeId, int OfficeEngOId, int MeId, int MemberTypeId)
        {
            return GetDsgnOfficeMembersInfoPerStage(ProjectIngridientTypeId, OfficeEngOId, MeId, MemberTypeId);
        }

        #endregion

        #endregion

        #region Check

        #region Private-Methods

        /// <summary>
        /// تعداد کار مجاز و ظرفیت عضوی از یک شرکت یا یک دفتر را چک می کند
        /// </summary>
        private int CheckOfficeMembersCapacity(int ProjectIngridientTypeId, int MemberTypeId, int OfficeEngOId, int Capacity, int MeOthPId, int Step)
        {
            // CapacityArr -----> ArrayList[0]: MeId, ArrayList[1]: MaxJobCapacity, ArrayList[2]: MaxJobCount, ArrayList[3]: UsedCapacity,
            //                    ArrayList[4]: RemainCapacity, ArrayList[5]:ReservedCapacity , ArrayList[6]: ProjectNum, ArrayList[7]: MaxFloor

            ArrayList CapacityArr = GetOfficeMembersCapacityInfoPerStage(ProjectIngridientTypeId, OfficeEngOId, MemberTypeId);
            int i = 0;
            if (CapacityArr.Count > 0)
            {
                while (Convert.ToInt32(((ArrayList)CapacityArr[i])[0]) != MeOthPId)
                    i++;

                if (Convert.ToInt32(((ArrayList)CapacityArr[i])[0]) == MeOthPId)
                {

                    if (Convert.ToInt32(((ArrayList)CapacityArr[i])[1]) < Capacity && Convert.ToInt32(((ArrayList)CapacityArr[i])[2]) <= Convert.ToInt32(((ArrayList)CapacityArr[i])[6]))
                        return (int)CapacityErr.NotEnoughCapacityAndMaxJobIsTaken;

                    if (Convert.ToInt32(((ArrayList)CapacityArr[i])[1]) < Capacity)
                        return (int)CapacityErr.NotEnoughRmainCapacity;

                    if (Convert.ToInt32(((ArrayList)CapacityArr[i])[2]) <= Convert.ToInt32(((ArrayList)CapacityArr[i])[6]))
                        return (int)CapacityErr.MaxJobIsTaken;

                    // CapacityArr[7]) = 0 ----> ندارد  CapacityArr[7]) = -1 ----> بدون محدودیت
                    if (Step != -1 && Convert.ToInt32(CapacityArr[7]) != 0 && Convert.ToInt32(CapacityArr[7]) != -1 && Convert.ToInt32(CapacityArr[7]) < Step)
                        return (int)CapacityErr.NotEnoughStep;

                    return 0;
                }
            }

            return -1;
        }

        /// <summary>
        /// چک می کند که یک شخص حقیقی عضو شرکت هست یا نه
        /// </summary>
        private int CheckOffice(int MeId, int MFType)
        {
            // MFType = TSP.DataManager.DocumentOfficeResponsibilityType

            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
            OfMeManager.FindOffMemberByPersonId(MeId, 2);
            if (OfMeManager.Count > 0)
            {
                OfficeManager.FindByCode(Convert.ToInt32(OfMeManager[0]["OfId"]));
                if (OfficeManager.Count > 0)
                {
                    if (Convert.ToInt32(OfficeManager[0]["MFType"]) == MFType)
                        return Convert.ToInt32(OfficeManager[0]["OfId"]);
                }
            }
            return -1;
        }

        /// <summary>
        /// چک می کند که یک شخص حقیقی عضو شرکت هست یا نه
        /// </summary>
        private int CheckOffice(int MeId)
        {
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
            OfMeManager.FindOffMemberByPersonId(MeId, 2);
            if (OfMeManager.Count > 0)
            {
                OfficeManager.FindByCode(Convert.ToInt32(OfMeManager[0]["OfId"]));
                if (OfficeManager.Count > 0)
                {
                    return Convert.ToInt32(OfficeManager[0]["OfId"]);
                }
            }
            return -1;
        }

        /// <summary>
        /// چک می کند که یک شخص حقیقی عضو دفتر هست یا نه
        /// </summary>
        private int CheckEngOffice(int MeId, int EngOfficeType)
        {
            // EngOfficeType = TSP.DataManager.EngOfficeType

            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.EngOfficeManager EngOfficeManager = new TSP.DataManager.EngOfficeManager();
            OfMeManager.FindEngOfficeMemberByPersonId(MeId);
            if (OfMeManager.Count > 0)
            {
                EngOfficeManager.FindByCode(Convert.ToInt32(OfMeManager[0]["OfId"]));
                if (EngOfficeManager.Count > 0)
                {
                    if (Convert.ToInt32(EngOfficeManager[0]["EOfTId"]) == EngOfficeType)
                        return Convert.ToInt32(OfMeManager[0]["OfId"]);
                }
            }

            return -1;
        }

        /// <summary>
        /// چک می کند که یک شخص حقیقی عضو دفتر هست یا نه
        /// </summary>
        private int CheckEngOffice(int MeId)
        {
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.EngOfficeManager EngOfficeManager = new TSP.DataManager.EngOfficeManager();
            OfMeManager.FindEngOfficeMemberByPersonId(MeId);
            if (OfMeManager.Count > 0)
            {
                EngOfficeManager.FindByCode(Convert.ToInt32(OfMeManager[0]["OfId"]));
                if (EngOfficeManager.Count > 0)
                {
                    return Convert.ToInt32(OfMeManager[0]["OfId"]);
                }
            }

            return -1;
        }

        #endregion

        #region چک کردن تعداد کار مجاز و ظرفیت
        /// <summary>
        /// تعداد کار مجاز و ظرفیت یک فرد، شرکت یا یک دفتر را چک می کند
        /// </summary>
        private int CheckCapacity(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, int Capacity, int Step)
        {
            // CapacityArr -----> ArrayList[0]: TotalCapacity, ArrayList[1]:UsedCapacity , ArrayList[2]: RemainCapacity, 
            //                    ArrayList[3]:ReservedCapacity , ArrayList[4]: ProjectNum, ArrayList[5]: MaxJoubCount,
            //                    ArrayList[6]: MaxFloor

            //*****************اطلاعات ظرفیت فرد، شرکت یا یک دفتر را بر اساس اختصاص ظرفیت بر می گرداند
            ArrayList CapacityArr = GetCapacityInfoPerStage(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId);

            if (Convert.ToInt32(CapacityArr[2]) < Capacity && Convert.ToInt32(CapacityArr[5]) <= Convert.ToInt32(CapacityArr[4]))
                return (int)CapacityErr.NotEnoughCapacityAndMaxJobIsTaken;

            if (Convert.ToInt32(CapacityArr[2]) < Capacity)
                return (int)CapacityErr.NotEnoughRmainCapacity;

            if (Convert.ToInt32(CapacityArr[5]) <= Convert.ToInt32(CapacityArr[4]))
                return (int)CapacityErr.MaxJobIsTaken;

            // CapacityArr[6]) = 0 ----> ندارد  CapacityArr[6]) = -1 ----> بدون محدودیت
            if (Step != -1 && Convert.ToInt32(CapacityArr[6]) != 0 && Convert.ToInt32(CapacityArr[6]) != -1 && Convert.ToInt32(CapacityArr[6]) < Step)
                return (int)CapacityErr.NotEnoughStep;

            return 0;
        }

        /// <summary>
        /// تعداد کار مجاز و ظرفیت یک فرد، شرکت یا یک دفتر را چک می کند
        /// </summary>
        public string CheckCapacityAndJobCount(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, int Capacity)
        {
            int Err = CheckCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, Capacity, -1);
            switch (Err)
            {
                case (int)CapacityErr.NotEnoughCapacityAndMaxJobIsTaken:
                    return "ظرفیت باقیمانده عضو مورد نظر کافی نمی باشد و حداکثر تعداد کار مجاز نیز گرفته شده است";
                    break;

                case (int)CapacityErr.NotEnoughRmainCapacity:
                    return "ظرفیت باقیمانده عضو مورد نظر کافی نمی باشد.";
                    break;

                case (int)CapacityErr.MaxJobIsTaken:
                    return "حداکثر تعداد کار مجاز برای عضو مورد نظر گرفته شده است.";
                    break;

                default:
                    return "";
            }
        }

        /// <summary>
        /// تعداد کار و طبقات مجاز و ظرفیت یک مجری را چک می کند
        /// </summary>
        public string CheckCapacityAndJobCount(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, int Capacity, int Step)
        {
            int Err = CheckCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, Capacity, Step);
            switch (Err)
            {
                case (int)CapacityErr.NotEnoughCapacityAndMaxJobIsTaken:
                    return "ظرفیت باقیمانده عضو مورد نظر کافی نمی باشد و حداکثر تعداد کار مجاز نیز گرفته شده است";
                    break;

                case (int)CapacityErr.NotEnoughRmainCapacity:
                    return "ظرفیت باقیمانده عضو مورد نظر کافی نمی باشد.";
                    break;

                case (int)CapacityErr.MaxJobIsTaken:
                    return "حداکثر تعداد کار مجاز برای عضو مورد نظر گرفته شده است.";
                    break;

                case (int)CapacityErr.NotEnoughStep:
                    return "تعداد طبقات پروژه از حداکثر تعداد طبقات مجاز برای عضو مورد نظر بیشتر است.";
                    break;

                default:
                    return "";
            }
        }
        #endregion

        #region Public-Methods


        /// <summary>
        /// تعداد کار مجاز و ظرفیت عضوی از یک شرکت یا یک دفتر را چک می کند
        /// </summary>
        public string CheckOfficecMembersCapacityAndJobCount(int ProjectIngridientTypeId, int MemberTypeId, int OfficeEngOId, int Capacity, int MeOthPId)
        {
            int Err = CheckOfficeMembersCapacity(ProjectIngridientTypeId, MemberTypeId, OfficeEngOId, Capacity, MeOthPId, -1);
            switch (Err)
            {
                case (int)CapacityErr.NotEnoughCapacityAndMaxJobIsTaken:
                    return "ظرفیت باقیمانده عضو مورد نظر کافی نمی باشد و حداکثر تعداد کار مجاز نیز گرفته شده است";
                    break;

                case (int)CapacityErr.NotEnoughRmainCapacity:
                    return "ظرفیت باقیمانده عضو مورد نظر کافی نمی باشد.";
                    break;

                case (int)CapacityErr.MaxJobIsTaken:
                    return "حداکثر تعداد کار مجاز برای عضو مورد نظر گرفته شده است.";
                    break;

                case -1:
                    return "اطلاعات عضو مورد نظر یافت نشد.";
                    break;

                default:
                    return "";
            }
        }

        /// <summary>
        /// تعداد کار مجاز و ظرفیت عضوی از یک شرکت یا یک دفتر اجرا را چک می کند
        /// </summary>
        public string CheckOfficecMembersCapacityAndJobCount(int ProjectIngridientTypeId, int MemberTypeId, int OfficeEngOId, int Capacity, int MeOthPId, int Step)
        {
            int Err = CheckOfficeMembersCapacity(ProjectIngridientTypeId, MemberTypeId, OfficeEngOId, Capacity, MeOthPId, Step);
            switch (Err)
            {
                case (int)CapacityErr.NotEnoughCapacityAndMaxJobIsTaken:
                    return "ظرفیت باقیمانده عضو مورد نظر کافی نمی باشد و حداکثر تعداد کار مجاز نیز گرفته شده است";
                    break;

                case (int)CapacityErr.NotEnoughRmainCapacity:
                    return "ظرفیت باقیمانده عضو مورد نظر کافی نمی باشد.";
                    break;

                case (int)CapacityErr.MaxJobIsTaken:
                    return "حداکثر تعداد کار مجاز برای عضو مورد نظر گرفته شده است.";
                    break;

                case (int)CapacityErr.NotEnoughStep:
                    return "تعداد طبقات پروژه از حداکثر تعداد طبقات مجاز برای عضو مورد نظر بیشتر است.";
                    break;

                case -1:
                    return "اطلاعات عضو مورد نظر یافت نشد.";
                    break;

                default:
                    return "";
            }
        }

        /// <summary>
        /// چک می کند که یک شرکت می تواند کار نظارت بگیرد یا نه
        /// </summary>
        public bool CheckOfficeForObservation(int OfficeId)
        {
            TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
            ProjectCapacityDecrementManager.FindByOfficeId(OfficeId, (int)TSP.DataManager.TSProjectIngridientType.Observer);
            if (ProjectCapacityDecrementManager.Count > 0)
                return false;
            return true;
        }

        /// <summary>
        /// چک می کند که یک شخص حقیقی می تواند کار نظارت بگیرد یا نه
        /// </summary>
        public bool CheckPersonForObservation(int MeId)
        {
            int OfficeId = CheckOffice(MeId, (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign);
            if (OfficeId != -1)
            {
                TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
                ProjectCapacityDecrementManager.FindOfficeCapacity(OfficeId, (int)TSP.DataManager.TSProjectIngridientType.Observer);
                if (ProjectCapacityDecrementManager.Count > 0)
                    return false;
                return true;
            }

            return true;
        }

        /// <summary>
        /// چک می کند که یک شخص حقیقی عضو شرکت هست یا نه
        /// </summary>
        public int CheckIsInOffice(int MeId)
        {
            return CheckOffice(MeId);
        }

        /// <summary>
        /// چک می کند که یک شخص حقیقی عضو دفتر هست یا نه
        /// </summary>
        public int CheckIsInEngOffice(int MeId)
        {
            return CheckEngOffice(MeId);
        }

        #endregion

        #endregion

        #region Actions توابع کسر کردن/آزاد سازی ظرفیت

        #region Private-Methods

        /// <summary>
        /// ظرفیت اعضا شرکت اجرا یک پروژه خاص را آزاد می کند
        /// </summary>
        private void ToFreeOfficeMembersImpCapacity(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, int ProjetId)
        {
            ProjectOfficeMembersManager.FindByProjectIdAndIngridientTypeId(ProjetId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
            for (int i = 0; i < ProjectOfficeMembersManager.Count; i++)
            {
                ProjectOfficeMembersManager[i].BeginEdit();
                ProjectOfficeMembersManager[i]["IsFree"] = 1;
                ProjectOfficeMembersManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                ProjectOfficeMembersManager[i]["ModifiedDate"] = DateTime.Now;
                ProjectOfficeMembersManager[i].EndEdit();
            }
            ProjectOfficeMembersManager.Save();
            ProjectOfficeMembersManager.DataTable.AcceptChanges();
        }

        /// <summary>
        /// ظرفیت اعضا شرکتهای نظارت را آزاد می کند
        /// </summary>
        private void ToFreeOfficeMembersObsCapacity(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager)
        {
            ProjectOfficeMembersManager.FindNotFreeByProjectIngridientTypeId((int)TSP.DataManager.TSProjectIngridientType.Observer);
            for (int i = 0; i < ProjectOfficeMembersManager.Count; i++)
            {
                ProjectOfficeMembersManager[i].BeginEdit();
                ProjectOfficeMembersManager[i]["IsFree"] = 1;
                ProjectOfficeMembersManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                ProjectOfficeMembersManager[i]["ModifiedDate"] = DateTime.Now;
                ProjectOfficeMembersManager[i].EndEdit();
            }
            ProjectOfficeMembersManager.Save();
            ProjectOfficeMembersManager.DataTable.AcceptChanges();
        }

        /// <summary>
        /// ظرفیت اعضا شرکتهای طراحی را آزاد می کند
        /// </summary>
        private void ToFreeOfficeMembersDsgnCapacity(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager)
        {
            ProjectOfficeMembersManager.FindNotFreeByProjectIngridientTypeId((int)TSP.DataManager.TSProjectIngridientType.Designer);
            for (int i = 0; i < ProjectOfficeMembersManager.Count; i++)
            {
                ProjectOfficeMembersManager[i].BeginEdit();
                ProjectOfficeMembersManager[i]["IsFree"] = 1;
                ProjectOfficeMembersManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                ProjectOfficeMembersManager[i]["ModifiedDate"] = DateTime.Now;
                ProjectOfficeMembersManager[i].EndEdit();
            }
            ProjectOfficeMembersManager.Save();
            ProjectOfficeMembersManager.DataTable.AcceptChanges();
        }

        /// <summary>
        /// ظرفیت اعضا شرکت یا یک دفتر مربوط به یک پروژه را کم می کند
        /// </summary>
        private void OfficeMembersCapacityDecrement(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, int ProjetId, int PrjImpObsDsgnId)
        {
            ProjectOfficeMembersManager.FindByProjectIdAndPrjImpObsDsgnId(ProjetId, PrjImpObsDsgnId);
            if (ProjectOfficeMembersManager.Count > 0)
            {
                ProjectOfficeMembersManager[0].BeginEdit();
                ProjectOfficeMembersManager[0]["IsDecreased"] = 1;
                ProjectOfficeMembersManager[0]["DecreasedDate"] = Utility.GetDateOfToday();
                ProjectOfficeMembersManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ProjectOfficeMembersManager[0]["ModifiedDate"] = DateTime.Now;
                ProjectOfficeMembersManager[0].EndEdit();
            }
            ProjectOfficeMembersManager.Save();
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// ظرفیت مجری های یک پروژه خاص را آزاد می کند
        /// A Transaction Is Needed
        /// </summary>
        public void ToFreeImpCapacity(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, int ProjetId)
        {
            ProjectCapacityDecrementManager.FindByProjectIdAndIngridientTypeId(ProjetId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
            for (int i = 0; i < ProjectCapacityDecrementManager.Count; i++)
            {
                ProjectCapacityDecrementManager[i].BeginEdit();
                ProjectCapacityDecrementManager[i]["IsFree"] = 1;
                ProjectCapacityDecrementManager[i]["FreeDate"] = Utility.GetDateOfToday();
                ProjectCapacityDecrementManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                ProjectCapacityDecrementManager[i]["ModifiedDate"] = DateTime.Now;
                ProjectCapacityDecrementManager[i].EndEdit();
            }
            ProjectCapacityDecrementManager.Save();
            ProjectCapacityDecrementManager.DataTable.AcceptChanges();

            ToFreeOfficeMembersImpCapacity(ProjectOfficeMembersManager, ProjetId);
        }

        /// <summary>
        /// ظرفیت ناظرین را آزاد می کند
        /// A Transaction Is Needed
        /// </summary>
        public void ToFreeObsCapacity(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager)
        {
            ProjectCapacityDecrementManager.FindNotFreeByProjectIngridientTypeId((int)TSP.DataManager.TSProjectIngridientType.Observer);
            for (int i = 0; i < ProjectCapacityDecrementManager.Count; i++)
            {
                ProjectCapacityDecrementManager[i].BeginEdit();
                ProjectCapacityDecrementManager[i]["IsFree"] = 1;
                ProjectCapacityDecrementManager[i]["FreeDate"] = Utility.GetDateOfToday();
                ProjectCapacityDecrementManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                ProjectCapacityDecrementManager[i]["ModifiedDate"] = DateTime.Now;
                ProjectCapacityDecrementManager[i].EndEdit();
            }
            ProjectCapacityDecrementManager.Save();
            ProjectCapacityDecrementManager.DataTable.AcceptChanges();

            ToFreeOfficeMembersObsCapacity(ProjectOfficeMembersManager);
        }

        /// <summary>
        /// ظرفیت طراحان را آزاد می کند
        /// A Transaction Is Needed
        /// </summary>
        public void ToFreeDsgnCapacity(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager)
        {
            ProjectCapacityDecrementManager.FindNotFreeByProjectIngridientTypeId((int)TSP.DataManager.TSProjectIngridientType.Designer);
            for (int i = 0; i < ProjectCapacityDecrementManager.Count; i++)
            {
                ProjectCapacityDecrementManager[i].BeginEdit();
                ProjectCapacityDecrementManager[i]["IsFree"] = 1;
                ProjectCapacityDecrementManager[i]["FreeDate"] = Utility.GetDateOfToday();
                ProjectCapacityDecrementManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                ProjectCapacityDecrementManager[i]["ModifiedDate"] = DateTime.Now;
                ProjectCapacityDecrementManager[i].EndEdit();
            }
            ProjectCapacityDecrementManager.Save();
            ProjectCapacityDecrementManager.DataTable.AcceptChanges();

            ToFreeOfficeMembersDsgnCapacity(ProjectOfficeMembersManager);
        }

        /// <summary>
        /// ظرفیت فرد، شرکت یا یک دفتر مربوط به یک پروژه را کم می کند
        /// A Transaction Is Needed
        /// </summary>
        public void CapacityDecrement(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, int ProjectId, int PrjImpObsDsgnId, int ProjectIngridientTypeId)
        {
            ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(ProjectId, PrjImpObsDsgnId, ProjectIngridientTypeId);
            if (ProjectCapacityDecrementManager.Count > 0)
            {
                ProjectCapacityDecrementManager[0].BeginEdit();
                ProjectCapacityDecrementManager[0]["IsDecreased"] = 1;
                ProjectCapacityDecrementManager[0]["DecreasedDate"] = Utility.GetDateOfToday();
                ProjectCapacityDecrementManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ProjectCapacityDecrementManager[0]["ModifiedDate"] = DateTime.Now;
                ProjectCapacityDecrementManager[0].EndEdit();
            }
            ProjectCapacityDecrementManager.Save();

            OfficeMembersCapacityDecrement(ProjectOfficeMembersManager, ProjectId, PrjImpObsDsgnId);
        }

        /// <summary>
        /// ظرفیت مجری های مربوط به یک پروژه را کم می کند
        /// A Transaction Is Needed
        /// </summary>
        public void ImplementersCapacityDecrement(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImplementerManager, int ProjectId)
        {
            ProjectImplementerManager.FindActivesByProjectId(ProjectId);
            for (int i = 0; i < ProjectImplementerManager.Count; i++)
            {
                CapacityDecrement(ProjectCapacityDecrementManager, ProjectOfficeMembersManager, ProjectId, Convert.ToInt32(ProjectImplementerManager[i]["PrjImpId"]), (int)TSP.DataManager.TSProjectIngridientType.Implementer);
            }
        }

        /// <summary>
        /// ظرفیت ناظران مربوط به یک پروژه را کم می کند
        /// A Transaction Is Needed
        /// </summary>
        public void ObserversCapacityDecrement(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObserversManager, int ProjectId)
        {
            ProjectObserversManager.FindActivesByProjectId(ProjectId);
            for (int i = 0; i < ProjectObserversManager.Count; i++)
            {
                CapacityDecrement(ProjectCapacityDecrementManager, ProjectOfficeMembersManager, ProjectId, Convert.ToInt32(ProjectObserversManager[i]["ProjectObserversId"]), (int)TSP.DataManager.TSProjectIngridientType.Observer);
            }
        }

        /// <summary>
        /// ظرفیت طراحان مربوط به یک پروژه را کم می کند
        /// A Transaction Is Needed
        /// </summary>
        public void DesignersCapacityDecrement(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager, int ProjectId)
        {
            ProjectDesignerManager.FindActivesByProjectId(ProjectId);
            for (int i = 0; i < ProjectDesignerManager.Count; i++)
            {
                CapacityDecrement(ProjectCapacityDecrementManager, ProjectOfficeMembersManager, ProjectId, Convert.ToInt32(ProjectDesignerManager[i]["PrjDesignerId"]), (int)TSP.DataManager.TSProjectIngridientType.Designer);
            }
        }
        #endregion

        #endregion

        #region InDate

        #region Private-Methods

        /// <summary>
        /// کلیه پروانه های یک شخص که در یک بازه زمانی فعال بوده اند را بر می گرداند
        /// ((DataRow)ArrayList[i]) : ["MfId"], ["MFNO"]:شماره پروانه, ["Date"]:تاریخ تایید , ["ExpireDate"]:تاریخ پایان اعتبار پروانه
        /// </summary>
        private ArrayList GetDocMemberFile(int MeId, string StartDate, string EndDate)
        {
            TSP.DataManager.DocMemberFileManager DMemFileManager = new TSP.DataManager.DocMemberFileManager();
            return DMemFileManager.FindActiveDocMemberFileByDate(MeId, StartDate, EndDate);
        }

        /// <summary>
        /// کلیه پروانه های یک شرکت که در یک بازه زمانی فعال بوده اند را بر می گرداند
        /// ((DataRow)ArrayList[i]) : ["OfReId"], ["MFNO"]:شماره پروانه, ["AnswerDate"]:تاریخ تایید , ["ExpireDate"]:تاریخ پایان اعتبار پروانه
        /// </summary>
        private ArrayList GetDocOfficeFile(int OfId, string StartDate, string EndDate)
        {
            TSP.DataManager.OfficeRequestManager OfficeRequestManager = new TSP.DataManager.OfficeRequestManager();
            return OfficeRequestManager.FindActiveOfficeRequestByDate(OfId, StartDate, EndDate);
        }

        /// <summary>
        /// کلیه پروانه های یک دفتر که در یک بازه زمانی فعال بوده اند را بر می گرداند
        /// ((DataRow)ArrayList[i]) : ["EOfId"], ["FileNo"]:شماره پروانه, ["AnswerDate"]:تاریخ تایید , ["ExpireDate"]:تاریخ پایان اعتبار پروانه
        /// </summary>
        private ArrayList GetDocEngOfficeFile(int EngOfId, string StartDate, string EndDate)
        {
            TSP.DataManager.EngOffFileManager EngOffFileManager = new TSP.DataManager.EngOffFileManager();
            return EngOffFileManager.FindActiveEngOffFileByDate(EngOfId, StartDate, EndDate);
        }

        /// <summary>
        /// رشته اصلی پروانه یک عضو را بر اساس یک پروانه خاص بر می گرداند
        /// ArrayList[0]: MjId, ArrayList[1]: MjName
        /// </summary>
        private ArrayList GetMajorByMFId(int MFId, int MeId)
        {
            ArrayList MajorArr = new ArrayList();
            TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
            DataTable dt = DocMemberFileMajorManager.SelectMemberFileById(MFId, MeId, 0, 1);
            if (dt.Rows.Count != 0)
            {
                MajorArr.Add(dt.Rows[0]["MjId"]);
                MajorArr.Add(dt.Rows[0]["MjName"]);
            }
            return MajorArr;
        }

        /// <summary>
        /// پایه یک عضو را در تاریخ خاص بر می گرداند
        /// </summary>
        private int GetGrade(int MeId, int ProjectIngridientTypeId, string Date)
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

            ArrayList GradeArr = DocMemberFileDetailManager.FindActiveResByResponsibility(MeId, ResponsibilityType, Date);
            if (GradeArr.Count != 0)
                return Convert.ToInt32(GradeArr[0]);
            else
                return 0;
        }

        /// <summary>
        /// پایه یک کاردان یا معمار تجربی را بر اساس یک پروانه خاص بر می گرداند
        /// </summary>
        private int GetTechnicianGradeByTnReId(int ProjectIngridientTypeId, int TnReId)
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

            MemberAcceptedGradeManager.FindByTnReIdAndResId(TnReId, ResponsibilityType, 0);

            if (MemberAcceptedGradeManager.Count > 0)
                return Convert.ToInt32(MemberAcceptedGradeManager[0]["GrdId"]);
            else
                return -2;
        }

        /// <summary>
        /// رشته یک کاردان یا معمار تجربی را بر اساس یک پروانه خاص بر می گرداند
        /// </summary>
        private int GetTechnicianMajorByTnReId(int TnReId)
        {
            TSP.DataManager.TechnicianRequestManager TechnicianRequestManager = new TSP.DataManager.TechnicianRequestManager();
            TechnicianRequestManager.FindByCode(TnReId);

            if (TechnicianRequestManager.Count > 0)
                return Convert.ToInt32(TechnicianRequestManager[0]["MjId"]);
            else
                return -1;
        }

        /// <summary>
        /// پایه یک مجری حقوقی را در تاریخ خاص بر می گرداند
        /// ArrayList[0]: GradeId, ArrayList[1]: Type, ArrayList[2]: CivilGrdId, ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId
        /// </summary>
        private ArrayList GetOfficeImpGrade(int OfficeId, string Date)
        {
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            ArrayList GradeArr = OfficeMemberManager.FindOfficeImpGrade(OfficeId, Date);
            return GradeArr;
        }

        /// <summary>
        /// پایه یک مجری حقوقی را بر اساس یک پروانه خاص بر می گرداند
        /// ArrayList[0]: GradeId, ArrayList[1]: Type, ArrayList[2]: CivilGrdId, ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId
        /// </summary>
        private ArrayList GetOfficeImpGradeByOfReId(int OfficeId, int OfReId)
        {
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            ArrayList GradeArr = OfficeMemberManager.FindOfficeImpGradebyOfReId(OfficeId, OfReId);
            return GradeArr;
        }

        /// <summary>
        /// ظرفیت اضافی یا کم شده یک شخص یا شرکت یا دفتر را در تاریخ خاص بر می گرداند
        /// </summary>
        private int GetConditionalCapacity(int MeOfficeEngOId, int ProjectIngridientTypeId, string StartDate, string EndDate)
        {
            int ConditionalCapacity = 0;
            TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();
            ConditionalCapacityManager.FindByMeOfficeEngOId(MeOfficeEngOId, StartDate, EndDate, ProjectIngridientTypeId);
            for (int i = 0; i < ConditionalCapacityManager.Count; i++)
                ConditionalCapacity += Convert.ToInt32(ConditionalCapacityManager[0]["Capacity"]);

            return ConditionalCapacity;
        }

        /// <summary>
        /// اعضای فعال شرکت یا دفتر را در تاریخ خاص بر می گرداند
        /// </summary>
        private TSP.DataManager.OfficeMemberManager GetOfficeMembers(int OfficeEngoId, int DocOffIncreaseJobCapacityType, string Date)
        {
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
                OfficeMemberManager.FindOfficeActiveMembersByDate(OfficeEngoId, (int)TSP.DataManager.OfficeMemberType.Member, Date);
            else if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice)
                OfficeMemberManager.FindEngOfficeActiveMembersByDate(OfficeEngoId, Date);

            return OfficeMemberManager;
        }

        /// <summary>
        /// اعضا و کاردان و معمارهای فعال شرکت را در تاریخ خاص بر می گرداند
        /// </summary>
        private TSP.DataManager.OfficeMemberManager GetOfficeAllMembers(int OfficeId, string Date)
        {
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            OfficeMemberManager.FindOfficeActiveMembersByDate(OfficeId, -1, Date);

            return OfficeMemberManager;
        }

        /// <summary>
        /// اعضای فعال شرکت یا دفتر را بر اساس یک پروانه خاص بر می گرداند
        /// </summary>
        private TSP.DataManager.OfficeMemberManager GetOfficeMembersByOfReId(int OfficeEngoId, int DocOffIncreaseJobCapacityType, int OfReId)
        {
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
                OfficeMemberManager.FindOfficeActiveMembersByOfReId(OfficeEngoId, OfReId, (int)TSP.DataManager.OfficeMemberType.Member, 0, 1);
            else if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice)
                OfficeMemberManager.FindEngOfficeActiveMembersByEOfId(OfficeEngoId, OfReId);

            return OfficeMemberManager;
        }

        /// <summary>
        /// اعضا و کاردان و معمارهای فعال شرکت را بر اساس یک پروانه خاص بر می گرداند
        /// </summary>
        private TSP.DataManager.OfficeMemberManager GetOfficeAllMembersByOfReId(int OfficeId, int OfReId)
        {
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            OfficeMemberManager.FindOfficeActiveMembersByOfReId(OfficeId, OfReId, -1, 0, 1);

            return OfficeMemberManager;
        }

        /// <summary>
        /// تاریخ ابتدا و انتها را از روی تاریخ ورودی و تاریخ پروانه عضو بر می گرداند
        /// ArrayList[0]: StartDate, ArrayList[1]: EndDate
        /// </summary>
        private ArrayList GetSDateAndEDateFormDocFile(ArrayList DocMemberFileArr, string StartDate, string EndDate, int i)
        {
            // ((DataRow)ArrayList[i]) : ["MfId"], ["MFNO"]:شماره پروانه, ["Date"]:تاریخ تایید , ["ExpireDate"]:تاریخ پایان اعتبار پروانه

            ArrayList Date = new ArrayList();
            string SDate = "";
            string EDate = "";

            if (i == 0)
                SDate = StartDate;
            else
                SDate = ((DataRow)DocMemberFileArr[i])["Date"].ToString();

            if (i == DocMemberFileArr.Count - 1)
                EDate = EndDate;
            else
                EDate = ((DataRow)DocMemberFileArr[i + 1])["Date"].ToString();

            if (String.Compare(((DataRow)DocMemberFileArr[i])["ExpireDate"].ToString(), EDate) < 0)
                EDate = ((DataRow)DocMemberFileArr[i])["ExpireDate"].ToString();

            Date.Add(SDate);
            Date.Add(EDate);

            return Date;
        }

        /// <summary>
        /// تاریخ ابتدا و انتها را از روی تاریخ ورودی و تاریخ پروانه شرکت یا دفتر بر می گرداند
        /// ArrayList[0]: StartDate, ArrayList[1]: EndDate
        /// </summary>
        private ArrayList GetSDateAndEDateFormDocOfficeFile(ArrayList DocOfficeFileArr, string StartDate, string EndDate, int i)
        {
            // ((DataRow)ArrayList[i]) : ["OfReId"], ["MFNO"]:شماره پروانه, ["AnswerDate"]:تاریخ تایید , ["ExpireDate"]:تاریخ پایان اعتبار پروانه
            // ((DataRow)ArrayList[i]) : ["EOfId"], ["FileNo"]:شماره پروانه, ["AnswerDate"]:تاریخ تایید , ["ExpireDate"]:تاریخ پایان اعتبار پروانه

            ArrayList Date = new ArrayList();
            string SDate = "";
            string EDate = "";

            if (i == 0)
                SDate = StartDate;
            else
                SDate = ((DataRow)DocOfficeFileArr[i])["AnswerDate"].ToString();

            if (i == DocOfficeFileArr.Count - 1)
                EDate = EndDate;
            else
                EDate = ((DataRow)DocOfficeFileArr[i + 1])["AnswerDate"].ToString();

            if (String.Compare(((DataRow)DocOfficeFileArr[i])["ExpireDate"].ToString(), EDate) < 0)
                EDate = ((DataRow)DocOfficeFileArr[i])["ExpireDate"].ToString();

            Date.Add(SDate);
            Date.Add(EDate);

            return Date;
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک عضو را در تاریخ خاص بر اساس پروانه ها بر می گرداند
        /// MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity, Grade, MjId, MeId, MeName, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetMemberDsgObsCapacity(int MeId, int ProjectIngridientTypeId, string StartDate, string EndDate)
        {
            DataTable DsgObsCapacityDT = GetMemberDsgObsCapacityDT();
            ArrayList DocMemberFileArr = GetDocMemberFile(MeId, StartDate, EndDate);

            for (int i = 0; i < DocMemberFileArr.Count; i++)
            {
                ArrayList DateArr = GetSDateAndEDateFormDocFile(DocMemberFileArr, StartDate, EndDate, i);

                if (DateArr.Count > 0)
                {
                    int ConditionalCapacity = GetConditionalCapacity(MeId, ProjectIngridientTypeId, DateArr[0].ToString(), DateArr[1].ToString());

                    int MFId = Convert.ToInt32(((DataRow)DocMemberFileArr[i])["MfId"]);
                    int Grade = GetGradeByMFId(MFId, MeId, ProjectIngridientTypeId);
                    if (Grade != 0)
                    {
                        int MjId = 0;
                        ArrayList MjArray = GetMajorByMFId(MFId, MeId);
                        if (MjArray.Count > 0)
                            MjId = Convert.ToInt32(MjArray[0]);

                        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                        MemberManager.FindByCode(MeId);

                        TSP.DataManager.DocOffMemberCapacityManager MemberCapacityManager = new TSP.DataManager.DocOffMemberCapacityManager();
                        MemberCapacityManager.FindByGrdIdAndDate(Grade, DateArr[0].ToString());

                        if (MemberCapacityManager.Count > 0)
                        {
                            DataRow dr = DsgObsCapacityDT.NewRow();

                            dr["MaxJobCount"] = MemberCapacityManager[0]["MaxJobCount"].ToString();
                            dr["TotalCapacity"] = (Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]) + ConditionalCapacity).ToString();
                            dr["ObservationPercent"] = MemberCapacityManager[0]["ObservationPercent"].ToString();
                            dr["ObservationCapacity"] = (Convert.ToInt32(Convert.ToDouble(dr["TotalCapacity"]) * Convert.ToDouble(dr["ObservationPercent"]))) + ConditionalCapacity;
                            dr["Grade"] = Grade.ToString();
                            dr["MjId"] = MjId.ToString();
                            dr["GradeInOfficeLicense"] = 0;
                            dr["DesignInc"] = 0;
                            dr["SameGradeInc"] = 0;
                            dr["MajorInc"] = 0;
                            dr["TotalDsgCapacity"] = 0;
                            dr["TotalObsCapacity"] = 0;
                            dr["MeId"] = MeId;
                            dr["MeName"] = MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
                            dr["ConditionalCapacity"] = ConditionalCapacity;
                            dr["StartDate"] = DateArr[0].ToString();
                            dr["EndDate"] = DateArr[1].ToString();
                            dr["FId"] = ((DataRow)DocMemberFileArr[i])["MfId"];
                            dr["FNO"] = ((DataRow)DocMemberFileArr[i])["MFNO"];
                            dr["ConfirmDate"] = ((DataRow)DocMemberFileArr[i])["Date"];
                            dr["ExpireDate"] = ((DataRow)DocMemberFileArr[i])["ExpireDate"];

                            DsgObsCapacityDT.Rows.Add(dr);
                        }
                    }
                }
            }
            return DsgObsCapacityDT;
        }

        /// <summary>
        /// MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity, Grade, MjId, GradeInOfficeLicense, DesignInc, SameGradeInc,MajorInc, TotalDsgCapacity, TotalObsCapacity, MeId, MeName, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate, Factor
        /// </summary>
        private DataTable GetMemberDsgObsCapacityDT()
        {
            DataTable DsgObsCapacityDT = new DataTable();

            DsgObsCapacityDT.Columns.Add("MaxJobCount");
            DsgObsCapacityDT.Columns.Add("TotalCapacity");
            DsgObsCapacityDT.Columns.Add("ObservationPercent");
            DsgObsCapacityDT.Columns.Add("ObservationCapacity");
            DsgObsCapacityDT.Columns.Add("Grade");
            DsgObsCapacityDT.Columns.Add("MjId");
            DsgObsCapacityDT.Columns.Add("GradeInOfficeLicense");
            DsgObsCapacityDT.Columns.Add("DesignInc");
            DsgObsCapacityDT.Columns.Add("SameGradeInc");
            DsgObsCapacityDT.Columns.Add("MajorInc");
            DsgObsCapacityDT.Columns.Add("TotalDsgCapacity");
            DsgObsCapacityDT.Columns.Add("TotalObsCapacity");
            DsgObsCapacityDT.Columns.Add("MeId");
            DsgObsCapacityDT.Columns.Add("MeName");
            DsgObsCapacityDT.Columns.Add("ConditionalCapacity");
            DsgObsCapacityDT.Columns.Add("StartDate");
            DsgObsCapacityDT.Columns.Add("EndDate");
            DsgObsCapacityDT.Columns.Add("FId");
            DsgObsCapacityDT.Columns.Add("FNO");
            DsgObsCapacityDT.Columns.Add("ConfirmDate");
            DsgObsCapacityDT.Columns.Add("ExpireDate");
            DsgObsCapacityDT.Columns.Add("Factor");

            return DsgObsCapacityDT;
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک عضو را بر اساس یک پروانه خاص بر می گرداند
        /// MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity, Grade, MjId, MeId, MeName, ConditionalCapacity, StartDate, EndDate
        /// </summary>
        private DataTable GetMemberDsgObsCapacityByMFId(int MeId, int ProjectIngridientTypeId, int MFId, string StartDate, string EndDate)
        {
            DataTable DsgObsCapacityDT = GetMemberDsgObsCapacityDT();
            ArrayList MemberArr = new ArrayList();

            int ConditionalCapacity = GetConditionalCapacity(MeId, ProjectIngridientTypeId, StartDate, EndDate);

            int Grade = GetGradeByMFId(MFId, MeId, ProjectIngridientTypeId);
            if (Grade != 0)
            {
                int MjId = 0;
                ArrayList MjArray = GetMajorByMFId(MFId, MeId);
                if (MjArray.Count > 0)
                    MjId = Convert.ToInt32(MjArray[0]);

                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                MemberManager.FindByCode(MeId);

                TSP.DataManager.DocOffMemberCapacityManager MemberCapacityManager = new TSP.DataManager.DocOffMemberCapacityManager();
                MemberCapacityManager.FindByGrdIdAndDate(Grade, StartDate);

                if (MemberCapacityManager.Count > 0)
                {
                    DataRow dr = DsgObsCapacityDT.NewRow();

                    dr["MaxJobCount"] = MemberCapacityManager[0]["MaxJobCount"].ToString();
                    dr["TotalCapacity"] = (Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]) + ConditionalCapacity).ToString();
                    dr["ObservationPercent"] = MemberCapacityManager[0]["ObservationPercent"].ToString();
                    dr["ObservationCapacity"] = (Convert.ToInt32(Convert.ToDouble(dr["TotalCapacity"]) * Convert.ToDouble(dr["ObservationPercent"]))) + ConditionalCapacity;
                    dr["Grade"] = Grade.ToString();
                    dr["MjId"] = MjId.ToString();
                    dr["GradeInOfficeLicense"] = 0;
                    dr["DesignInc"] = 0;
                    dr["SameGradeInc"] = 0;
                    dr["MajorInc"] = 0;
                    dr["TotalDsgCapacity"] = 0;
                    dr["TotalObsCapacity"] = 0;
                    dr["MeId"] = MeId;
                    dr["MeName"] = MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
                    dr["ConditionalCapacity"] = ConditionalCapacity;
                    dr["StartDate"] = StartDate;
                    dr["EndDate"] = EndDate;

                    DsgObsCapacityDT.Rows.Add(dr);
                }
            }
            return DsgObsCapacityDT;
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت را در تاریخ خاص بر اساس پروانه ها بر می گرداند
        /// MaxJobCount, TotalCapacity, ObservationCapacity, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetOfficeDsgObsCapacity(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, string StartDate, string EndDate)
        {
            // MajorArr -----> ArrayList[0]: MainMajorNum, ArrayList[1]: SecondaryMajorNum, ArrayList[2]: TotalMajorNum

            // MembersDT -----> MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity,Grade, MjId, GradeInOfficeLicense, DesignInc, 
            //                  SameGradeInc,MajorInc, TotalDsgCapacity, TotalObsCapacity, MeId, MeName, ConditionalCapacity

            TSP.DataManager.DocOffMajorNum DocOffMajorNum = new TSP.DataManager.DocOffMajorNum();
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocOffIncreaseJobCapacityManager IncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();

            DataTable MembersDT = GetMemberDsgObsCapacityDT();
            DataTable DsgObsCapacityDT = GetOfficeDsgObsCapacityDT();
            ArrayList DocOfficeFileArr = new ArrayList();

            if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
                DocOfficeFileArr = GetDocOfficeFile(OfficeEngoId, StartDate, EndDate);
            else if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice)
                DocOfficeFileArr = GetDocEngOfficeFile(OfficeEngoId, StartDate, EndDate);

            for (int k = 0; k < DocOfficeFileArr.Count; k++)
            {
                ArrayList DateArr = GetSDateAndEDateFormDocOfficeFile(DocOfficeFileArr, StartDate, EndDate, k);

                if (DateArr.Count > 0)
                {
                    int ConditionalCapacity = GetConditionalCapacity(OfficeEngoId, ProjectIngridientTypeId, DateArr[0].ToString(), DateArr[1].ToString());

                    int FileId = -1;
                    string MFNo = "";

                    if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
                    {
                        FileId = Convert.ToInt32(((DataRow)DocOfficeFileArr[k])["OfReId"]);
                        MFNo = ((DataRow)DocOfficeFileArr[k])["MFNO"].ToString();
                    }
                    else if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice)
                    {
                        FileId = Convert.ToInt32(((DataRow)DocOfficeFileArr[k])["EofId"]);
                        MFNo = ((DataRow)DocOfficeFileArr[k])["FileNo"].ToString();
                    }

                    OfficeMemberManager = GetOfficeMembersByOfReId(OfficeEngoId, DocOffIncreaseJobCapacityType, FileId);

                    for (int i = 0; i < OfficeMemberManager.Count; i++)
                    {
                        if (Convert.ToInt32(OfficeMemberManager[i]["OfmType"]) == (int)TSP.DataManager.OfficeMemberType.Member)
                        {
                            DataTable Member = GetMemberDsgObsCapacityByMFId(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[i]["MfId"]), DateArr[0].ToString(), DateArr[1].ToString());
                            if (Member.Rows.Count != 0)
                            {
                                Member.Rows[0]["GradeInOfficeLicense"] = GetGradeByMFId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId).ToString();
                                DataRow drow = MembersDT.NewRow();

                                drow["MaxJobCount"] = Member.Rows[0]["MaxJobCount"];
                                drow["TotalCapacity"] = Member.Rows[0]["TotalCapacity"];
                                drow["ObservationPercent"] = Member.Rows[0]["ObservationPercent"];
                                drow["ObservationCapacity"] = Member.Rows[0]["ObservationCapacity"];
                                drow["Grade"] = Member.Rows[0]["Grade"];
                                drow["MjId"] = Member.Rows[0]["MjId"];
                                drow["GradeInOfficeLicense"] = Member.Rows[0]["GradeInOfficeLicense"];
                                drow["MeId"] = Member.Rows[0]["MeId"];
                                drow["MeName"] = Member.Rows[0]["MeName"];
                                drow["ConditionalCapacity"] = Member.Rows[0]["ConditionalCapacity"];
                                drow["StartDate"] = Member.Rows[0]["StartDate"];
                                drow["EndDate"] = Member.Rows[0]["EndDate"];

                                MembersDT.Rows.Add(drow);
                            }
                        }
                    }

                    int MaxJobCount = 0;
                    int MaxJobCapacity = 0;
                    int ObservationCapacity = 0;

                    if (MembersDT.Rows.Count != 0)
                    {
                        ArrayList MajorArr = GetMajorNumFromDT(MembersDT);

                        DocOffMajorNum.FindByMajorsNum((int)MajorArr[0], (int)MajorArr[1], (int)MajorArr[2]);
                        IncreaseJobCapacityManager.FindByMNumIdAndDate(Convert.ToInt32(DocOffMajorNum[0]["MNumId"]), DocOffIncreaseJobCapacityType, DateArr[0].ToString());

                        for (int i = 0; i < MembersDT.Rows.Count; i++)
                        {
                            bool SameGradeInc = false;
                            bool MajorInc = false;

                            MembersDT.Rows[i]["DesignInc"] = (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["DesignIncPer"]) / 100).ToString();
                            for (int j = 0; j < MembersDT.Rows.Count; j++)
                            {
                                if (i != j)
                                {
                                    if (MembersDT.Rows[i]["MjId"].ToString() == MembersDT.Rows[j]["MjId"].ToString())
                                    {
                                        if (MembersDT.Rows[i]["GradeInOfficeLicense"].ToString() == MembersDT.Rows[j]["GradeInOfficeLicense"].ToString())
                                        {
                                            if (!MajorInc)
                                                SameGradeInc = true;
                                        }
                                        else
                                            SameGradeInc = false;

                                        MajorInc = true;
                                    }

                                }
                            }
                            if (SameGradeInc)
                                MembersDT.Rows[i]["SameGradeInc"] = (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["SameGradeIncPer"]) / 100).ToString();
                            else
                                MembersDT.Rows[i]["SameGradeInc"] = 0;

                            if (MajorInc)
                                MembersDT.Rows[i]["MajorInc"] = (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["MajorIncPer"]) / 100).ToString();
                            else
                                MembersDT.Rows[i]["MajorInc"] = 0;

                            MembersDT.Rows[i]["TotalDsgCapacity"] = Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) + Convert.ToInt32(MembersDT.Rows[i]["DesignInc"]) + Convert.ToInt32(MembersDT.Rows[i]["SameGradeInc"]) + Convert.ToInt32(MembersDT.Rows[i]["MajorInc"]);
                            MembersDT.Rows[i]["TotalObsCapacity"] = Convert.ToInt32(Convert.ToDouble(MembersDT.Rows[i]["ObservationPercent"]) * (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) + Convert.ToInt32(MembersDT.Rows[i]["DesignInc"]) + Convert.ToInt32(MembersDT.Rows[i]["SameGradeInc"]) + Convert.ToInt32(MembersDT.Rows[i]["MajorInc"])));

                            MaxJobCount += Convert.ToInt32(MembersDT.Rows[i]["MaxJobCount"]);
                            MaxJobCapacity += Convert.ToInt32(MembersDT.Rows[i]["TotalDsgCapacity"]);
                            ObservationCapacity += Convert.ToInt32(MembersDT.Rows[i]["TotalObsCapacity"]);
                        }
                    }

                    MaxJobCapacity += Convert.ToInt32(ConditionalCapacity);
                    ObservationCapacity += Convert.ToInt32(ConditionalCapacity);

                    if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
                        MaxJobCount = MaxJobCount / 2;

                    DataRow dr = DsgObsCapacityDT.NewRow();

                    dr["MaxJobCount"] = MaxJobCount;
                    dr["TotalCapacity"] = MaxJobCapacity;
                    dr["ObservationCapacity"] = ObservationCapacity;
                    dr["ConditionalCapacity"] = ConditionalCapacity;
                    dr["StartDate"] = DateArr[0].ToString();
                    dr["EndDate"] = DateArr[1].ToString();
                    dr["FId"] = FileId;
                    dr["FNO"] = MFNo;
                    dr["ConfirmDate"] = ((DataRow)DocOfficeFileArr[k])["AnswerDate"];
                    dr["ExpireDate"] = ((DataRow)DocOfficeFileArr[k])["ExpireDate"];


                    DsgObsCapacityDT.Rows.Add(dr);
                }
            }
            return DsgObsCapacityDT;
        }

        /// <summary>
        /// MaxJobCount, TotalCapacity, ObservationCapacity, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetOfficeDsgObsCapacityDT()
        {
            DataTable DsgObsCapacityDT = new DataTable();

            DsgObsCapacityDT.Columns.Add("MaxJobCount");
            DsgObsCapacityDT.Columns.Add("TotalCapacity");
            DsgObsCapacityDT.Columns.Add("ObservationCapacity");
            DsgObsCapacityDT.Columns.Add("ConditionalCapacity");
            DsgObsCapacityDT.Columns.Add("StartDate");
            DsgObsCapacityDT.Columns.Add("EndDate");
            DsgObsCapacityDT.Columns.Add("FId");
            DsgObsCapacityDT.Columns.Add("FNO");
            DsgObsCapacityDT.Columns.Add("ConfirmDate");
            DsgObsCapacityDT.Columns.Add("ExpireDate");

            return DsgObsCapacityDT;
        }

        /// <summary>
        /// ArrayList[0]: MainMajorNum, ArrayList[1]: SecondaryMajorNum, ArrayList[2]: TotalMajorNum
        /// </summary>
        private ArrayList GetMajorNumFromDT(DataTable MembersDT)
        {
            TSP.DataManager.MajorParentsManager MajorManager = new TSP.DataManager.MajorParentsManager();
            MajorManager.FindMjParents();

            int MainMajorNum = 0;
            int SecondaryMajorNum = 0;
            int TotalMajorNum = 0;
            int MajorIncrement = 0;

            bool Architecture = false;
            bool Urbanism = false;
            bool Civil = false;
            bool Mechanic = false;
            bool Electronic = false;
            bool Mapping = false;
            bool Traffic = false;

            for (int j = 0; j < MembersDT.Rows.Count; j++)
            {
                switch (Convert.ToInt32(MembersDT.Rows[j]["MjId"]))
                {
                    case (int)TSP.DataManager.MainMajors.Architecture:
                        Architecture = true;
                        //ArchitectureNum += 1;
                        break;

                    case (int)TSP.DataManager.MainMajors.Civil:
                        Civil = true;
                        //CivilNum += 1;
                        break;

                    case (int)TSP.DataManager.MainMajors.Electronic:
                        Electronic = true;
                        //ElectronicNum += 1;
                        break;

                    case (int)TSP.DataManager.MainMajors.Mechanic:
                        Mechanic = true;
                        //MechanicNum += 1;
                        break;

                    case (int)TSP.DataManager.MainMajors.Mapping:
                        Mapping = true;
                        //MappingNum += 1;
                        break;

                    case (int)TSP.DataManager.MainMajors.Urbanism:
                        Urbanism = true;
                        //UrbanismNum += 1;
                        break;

                    case (int)TSP.DataManager.MainMajors.Traffic:
                        Traffic = true;
                        //TrafficNum += 1;
                        break;
                }
            }

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

            TotalMajorNum = MainMajorNum + SecondaryMajorNum;
            if ((MainMajorNum <= 1 && SecondaryMajorNum != 0) || TotalMajorNum == 1)
            {
                TotalMajorNum = 1;
                MainMajorNum = 0;
                SecondaryMajorNum = 0;
            }

            else if (MainMajorNum < 4)
            {
                SecondaryMajorNum = 0;
                TotalMajorNum = 0;
            }

            ArrayList MajorArr = new ArrayList();
            MajorArr.Add(MainMajorNum);
            MajorArr.Add(SecondaryMajorNum);
            MajorArr.Add(TotalMajorNum);

            return MajorArr;
        }

        /// <summary>
        /// افراد یک دفتر یا شرکت و ظرفیت طراحی و نظارت آنها را بر اساس یک پروانه خاص بر می گرداند
        /// MembersDT -----> MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity,Grade, MjId, GradeInOfficeLicense, DesignInc, SameGradeInc,MajorInc, TotalDsgCapacity, TotalObsCapacity, MeId, MeName, ConditionalCapacity, StartDate, EndDate, Factor
        /// </summary>
        private DataTable GetOfficeMembersByFileId(int OfficeId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, int FileId, string StartDate, string EndDate)
        {
            // MembersDT -----> MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity,Grade, MjId, GradeInOfficeLicense, DesignInc, 
            //                  SameGradeInc,MajorInc, TotalDsgCapacity, TotalObsCapacity, MeId, MeName, ConditionalCapacity

            TSP.DataManager.DocOffMajorNum DocOffMajorNum = new TSP.DataManager.DocOffMajorNum();
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocOffIncreaseJobCapacityManager IncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();

            DataTable MembersDT = GetMemberDsgObsCapacityDT();

            OfficeMemberManager = GetOfficeMembersByOfReId(OfficeId, DocOffIncreaseJobCapacityType, FileId);

            for (int i = 0; i < OfficeMemberManager.Count; i++)
            {
                if (Convert.ToInt32(OfficeMemberManager[i]["OfmType"]) == (int)TSP.DataManager.OfficeMemberType.Member)
                {
                    DataTable Member = GetMemberDsgObsCapacityByMFId(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[i]["MfId"]), StartDate, EndDate);
                    if (Member.Rows.Count != 0)
                    {
                        Member.Rows[0]["GradeInOfficeLicense"] = GetGradeByMFId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId).ToString();
                        DataRow drow = MembersDT.NewRow();

                        drow["MaxJobCount"] = Member.Rows[0]["MaxJobCount"];
                        drow["TotalCapacity"] = Member.Rows[0]["TotalCapacity"];
                        drow["ObservationPercent"] = Member.Rows[0]["ObservationPercent"];
                        drow["ObservationCapacity"] = Member.Rows[0]["ObservationCapacity"];
                        drow["Grade"] = Member.Rows[0]["Grade"];
                        drow["MjId"] = Member.Rows[0]["MjId"];
                        drow["GradeInOfficeLicense"] = Member.Rows[0]["GradeInOfficeLicense"];
                        drow["MeId"] = Member.Rows[0]["MeId"];
                        drow["MeName"] = Member.Rows[0]["MeName"];
                        drow["ConditionalCapacity"] = Member.Rows[0]["ConditionalCapacity"];
                        drow["StartDate"] = Member.Rows[0]["StartDate"];
                        drow["EndDate"] = Member.Rows[0]["EndDate"];

                        MembersDT.Rows.Add(drow);
                    }
                }
            }

            if (MembersDT.Rows.Count != 0)
            {
                ArrayList MajorArr = GetMajorNumFromDT(MembersDT);

                DocOffMajorNum.FindByMajorsNum((int)MajorArr[0], (int)MajorArr[1], (int)MajorArr[2]);
                IncreaseJobCapacityManager.FindByMNumIdAndDate(Convert.ToInt32(DocOffMajorNum[0]["MNumId"]), DocOffIncreaseJobCapacityType, StartDate);

                for (int i = 0; i < MembersDT.Rows.Count; i++)
                {
                    bool SameGradeInc = false;
                    bool MajorInc = false;

                    MembersDT.Rows[i]["DesignInc"] = (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["DesignIncPer"]) / 100).ToString();
                    for (int j = 0; j < MembersDT.Rows.Count; j++)
                    {
                        if (i != j)
                        {
                            if (MembersDT.Rows[i]["MjId"].ToString() == MembersDT.Rows[j]["MjId"].ToString())
                            {
                                if (MembersDT.Rows[i]["GradeInOfficeLicense"].ToString() == MembersDT.Rows[j]["GradeInOfficeLicense"].ToString())
                                {
                                    if (!MajorInc)
                                        SameGradeInc = true;
                                }
                                else
                                    SameGradeInc = false;

                                MajorInc = true;
                            }

                        }
                    }
                    if (SameGradeInc)
                        MembersDT.Rows[i]["SameGradeInc"] = (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["SameGradeIncPer"]) / 100).ToString();
                    else
                        MembersDT.Rows[i]["SameGradeInc"] = 0;

                    if (MajorInc)
                        MembersDT.Rows[i]["MajorInc"] = (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["MajorIncPer"]) / 100).ToString();
                    else
                        MembersDT.Rows[i]["MajorInc"] = 0;

                    MembersDT.Rows[i]["TotalDsgCapacity"] = Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) + Convert.ToInt32(MembersDT.Rows[i]["DesignInc"]) + Convert.ToInt32(MembersDT.Rows[i]["SameGradeInc"]) + Convert.ToInt32(MembersDT.Rows[i]["MajorInc"]);
                    MembersDT.Rows[i]["TotalObsCapacity"] = Convert.ToInt32(Convert.ToDouble(MembersDT.Rows[i]["ObservationPercent"]) * (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) + Convert.ToInt32(MembersDT.Rows[i]["DesignInc"]) + Convert.ToInt32(MembersDT.Rows[i]["SameGradeInc"]) + Convert.ToInt32(MembersDT.Rows[i]["MajorInc"])));
                    MembersDT.Rows[i]["Factor"] = Convert.ToDouble(MembersDT.Rows[i]["TotalDsgCapacity"]) / Convert.ToDouble(MembersDT.Rows[i]["TotalCapacity"]);
                }
            }
            return MembersDT;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک عضو را در تاریخ خاص بر اساس پروانه ها بر می گرداند
        /// MaxFloor, TotalCapacity, MaxUnitCount, Grade, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetMemberImpCapacity(int MeId, string StartDate, string EndDate)
        {
            ArrayList MemberCapArr = new ArrayList();
            ArrayList DocMemberFileArr = GetDocMemberFile(MeId, StartDate, EndDate);
            DataTable ImpCapacityDT = GetMemberImpCapacityDT();

            for (int i = 0; i < DocMemberFileArr.Count; i++)
            {
                ArrayList MemberArr = new ArrayList();
                int MFId = Convert.ToInt32(((DataRow)DocMemberFileArr[i])["MfId"]);

                int Grade = GetGradeByMFId(MFId, MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                if (Grade != 0)
                {
                    ArrayList DateArr = GetSDateAndEDateFormDocFile(DocMemberFileArr, StartDate, EndDate, i);
                    int ConditionalCapacity = 0;

                    if (DateArr.Count > 0)
                    {
                        ConditionalCapacity = GetConditionalCapacity(MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer, DateArr[0].ToString(), DateArr[1].ToString());

                        TSP.DataManager.DocOffEngOfficeImpQualificationManager EngOfficeImpQualificationManager = new TSP.DataManager.DocOffEngOfficeImpQualificationManager();
                        EngOfficeImpQualificationManager.FindByGrdIdAndDate(Grade, DateArr[0].ToString());

                        if (EngOfficeImpQualificationManager.Count > 0)
                        {
                            DataRow dr = ImpCapacityDT.NewRow();

                            dr["MaxFloor"] = EngOfficeImpQualificationManager[0]["MaxFloor"].ToString();
                            dr["TotalCapacity"] = (Convert.ToInt32(EngOfficeImpQualificationManager[0]["MaxJobCapacity"]) + ConditionalCapacity).ToString();
                            dr["MaxUnitCount"] = EngOfficeImpQualificationManager[0]["MaxUnitCount"].ToString();
                            dr["Grade"] = Grade.ToString();
                            dr["ConditionalCapacity"] = ConditionalCapacity;
                            dr["StartDate"] = DateArr[0].ToString();
                            dr["EndDate"] = DateArr[1].ToString();
                            dr["FId"] = ((DataRow)DocMemberFileArr[i])["MfId"];
                            dr["FNO"] = ((DataRow)DocMemberFileArr[i])["MFNO"];
                            dr["ConfirmDate"] = ((DataRow)DocMemberFileArr[i])["Date"];
                            dr["ExpireDate"] = ((DataRow)DocMemberFileArr[i])["ExpireDate"];

                            ImpCapacityDT.Rows.Add(dr);
                        }
                    }
                }
            }
            return ImpCapacityDT;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک عضو را در تاریخ خاص بر اساس یک پروانه خاص بر می گرداند
        /// MaxFloor, TotalCapacity, MaxUnitCount, Grade, ConditionalCapacity, StartDate, EndDate
        /// </summary>
        private DataTable GetMemberImpCapacityByMFId(int MeId, int MFId, string StartDate, string EndDate)
        {
            ArrayList MemberCapArr = new ArrayList();
            DataTable ImpCapacityDT = GetMemberImpCapacityDT();

            ArrayList MemberArr = new ArrayList();

            int Grade = GetGradeByMFId(MFId, MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
            if (Grade != 0)
            {
                int ConditionalCapacity = GetConditionalCapacity(MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer, StartDate, EndDate);

                TSP.DataManager.DocOffEngOfficeImpQualificationManager EngOfficeImpQualificationManager = new TSP.DataManager.DocOffEngOfficeImpQualificationManager();
                EngOfficeImpQualificationManager.FindByGrdIdAndDate(Grade, StartDate);

                if (EngOfficeImpQualificationManager.Count > 0)
                {
                    DataRow dr = ImpCapacityDT.NewRow();

                    dr["MaxFloor"] = EngOfficeImpQualificationManager[0]["MaxFloor"].ToString();
                    dr["TotalCapacity"] = (Convert.ToInt32(EngOfficeImpQualificationManager[0]["MaxJobCapacity"]) + ConditionalCapacity).ToString();
                    dr["MaxUnitCount"] = EngOfficeImpQualificationManager[0]["MaxUnitCount"].ToString();
                    dr["Grade"] = Grade.ToString();
                    dr["ConditionalCapacity"] = ConditionalCapacity;
                    dr["StartDate"] = StartDate;
                    dr["EndDate"] = EndDate;

                    ImpCapacityDT.Rows.Add(dr);
                }
            }
            return ImpCapacityDT;
        }

        /// <summary>
        /// MaxFloor, TotalCapacity, MaxUnitCount, Grade, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetMemberImpCapacityDT()
        {
            DataTable ImpCapacityDT = new DataTable();

            ImpCapacityDT.Columns.Add("MaxFloor");
            ImpCapacityDT.Columns.Add("TotalCapacity");
            ImpCapacityDT.Columns.Add("MaxUnitCount");
            ImpCapacityDT.Columns.Add("Grade");
            ImpCapacityDT.Columns.Add("ConditionalCapacity");
            ImpCapacityDT.Columns.Add("StartDate");
            ImpCapacityDT.Columns.Add("EndDate");
            ImpCapacityDT.Columns.Add("FId");
            ImpCapacityDT.Columns.Add("FNO");
            ImpCapacityDT.Columns.Add("ConfirmDate");
            ImpCapacityDT.Columns.Add("ExpireDate");

            return ImpCapacityDT;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک شرکت را در تاریخ خاص بر اساس پروانه بر می گرداند
        /// MaxFloor, TotalCapacity, MaxUnitCount, ConditionalCapacity, Grade, GrdType, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetOfficeImpCapacity(int OfficeId, string StartDate, string EndDate)
        {
            // GradeArr-----> ArrayList[0]: GradeId, ArrayList[1]: Type, ArrayList[2]: CivilGrdId, ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId

            DataTable ImpCapacityDT = GetOfficeImpCapacityDT();
            ArrayList DocMemberFileArr = GetDocOfficeFile(OfficeId, StartDate, EndDate);
            TSP.DataManager.DocOffOfficeMembersQualificationManager OfficeMembersQualificationManager = new TSP.DataManager.DocOffOfficeMembersQualificationManager();

            for (int i = 0; i < DocMemberFileArr.Count; i++)
            {
                int OfReId = Convert.ToInt32(((DataRow)DocMemberFileArr[i])["OfReId"]);

                ArrayList GradeArr = GetOfficeImpGradeByOfReId(OfficeId, OfReId);
                ArrayList DateArr = GetSDateAndEDateFormDocFile(DocMemberFileArr, StartDate, EndDate, i);

                if (GradeArr.Count != 0 && DateArr.Count != 0)
                {
                    int ConditionalCapacity = GetConditionalCapacity(OfficeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer, DateArr[0].ToString(), DateArr[1].ToString());

                    if ((int)GradeArr[1] == (int)TSP.DataManager.DocOffOfficeMembersQualificationType.Kardan_Kardan)
                        OfficeMembersQualificationManager.FindByGrdIdAndDate((int)GradeArr[0], (int)GradeArr[1], (int)GradeArr[2], DateArr[0].ToString());
                    else
                        OfficeMembersQualificationManager.FindByGrdIdAndDate((int)GradeArr[0], (int)GradeArr[1], null, DateArr[0].ToString());

                    if (OfficeMembersQualificationManager.Count > 0)
                    {
                        DataRow dr = ImpCapacityDT.NewRow();

                        dr["MaxFloor"] = Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxFloor"]);
                        dr["TotalCapacity"] = Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxCapacity"]) + ConditionalCapacity + GetCapacityOfPointsByOfReId(OfficeId, Convert.ToInt32(GradeArr[3]), Convert.ToInt32(GradeArr[4]), Convert.ToInt32(OfficeMembersQualificationManager[0]["PointsLimitation"]), OfReId, DateArr[0].ToString());
                        dr["MaxUnitCount"] = Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxJobCount"]);
                        dr["ConditionalCapacity"] = ConditionalCapacity;
                        dr["Grade"] = GradeArr[0];
                        dr["GrdType"] = GradeArr[1];
                        dr["StartDate"] = DateArr[0].ToString();
                        dr["EndDate"] = DateArr[1].ToString();
                        dr["FId"] = OfReId;
                        dr["FNO"] = ((DataRow)DocMemberFileArr[i])["MFNo"];
                        dr["ConfirmDate"] = ((DataRow)DocMemberFileArr[i])["AnswerDate"];
                        dr["ExpireDate"] = ((DataRow)DocMemberFileArr[i])["ExpireDate"];

                        ImpCapacityDT.Rows.Add(dr);
                    }
                }
            }
            return ImpCapacityDT;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک شرکت را در تاریخ خاص بر اساس یک پروانه خاص بر می گرداند
        /// MaxFloor, TotalCapacity, MaxUnitCount, ConditionalCapacity, Grade, GrdType, StartDate, EndDate
        /// </summary>
        private DataTable GetOfficeImpCapacityByFileId(int OfficeId, int FileId, string StartDate, string EndDate)
        {
            // GradeArr-----> ArrayList[0]: GradeId, ArrayList[1]: Type, ArrayList[2]: CivilGrdId, ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId

            DataTable ImpCapacityDT = GetOfficeImpCapacityDT();
            TSP.DataManager.DocOffOfficeMembersQualificationManager OfficeMembersQualificationManager = new TSP.DataManager.DocOffOfficeMembersQualificationManager();

            ArrayList GradeArr = GetOfficeImpGradeByOfReId(OfficeId, FileId);

            if (GradeArr.Count != 0)
            {
                int ConditionalCapacity = GetConditionalCapacity(OfficeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer, StartDate, EndDate);

                if ((int)GradeArr[1] == (int)TSP.DataManager.DocOffOfficeMembersQualificationType.Kardan_Kardan)
                    OfficeMembersQualificationManager.FindByGrdIdAndDate((int)GradeArr[0], (int)GradeArr[1], (int)GradeArr[2], StartDate);
                else
                    OfficeMembersQualificationManager.FindByGrdIdAndDate((int)GradeArr[0], (int)GradeArr[1], null, StartDate);

                if (OfficeMembersQualificationManager.Count > 0)
                {
                    DataRow dr = ImpCapacityDT.NewRow();

                    dr["MaxFloor"] = Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxFloor"]);
                    dr["TotalCapacity"] = Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxCapacity"]) + ConditionalCapacity + GetCapacityOfPointsByOfReId(OfficeId, Convert.ToInt32(GradeArr[3]), Convert.ToInt32(GradeArr[4]), Convert.ToInt32(OfficeMembersQualificationManager[0]["PointsLimitation"]), FileId, StartDate);
                    dr["MaxUnitCount"] = Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxJobCount"]);
                    dr["ConditionalCapacity"] = ConditionalCapacity;
                    dr["Grade"] = GradeArr[0];
                    dr["GrdType"] = GradeArr[1];
                    dr["StartDate"] = StartDate;
                    dr["EndDate"] = EndDate;

                    ImpCapacityDT.Rows.Add(dr);
                }
            }
            return ImpCapacityDT;
        }

        /// <summary>
        /// MaxFloor, TotalCapacity, MaxUnitCount, ConditionalCapacity, Grade, GrdType, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetOfficeImpCapacityDT()
        {
            DataTable ImpCapacityDT = new DataTable();

            ImpCapacityDT.Columns.Add("MaxFloor");
            ImpCapacityDT.Columns.Add("TotalCapacity");
            ImpCapacityDT.Columns.Add("MaxUnitCount");
            ImpCapacityDT.Columns.Add("ConditionalCapacity");
            ImpCapacityDT.Columns.Add("Grade");
            ImpCapacityDT.Columns.Add("GrdType");
            ImpCapacityDT.Columns.Add("StartDate");
            ImpCapacityDT.Columns.Add("EndDate");
            ImpCapacityDT.Columns.Add("FId");
            ImpCapacityDT.Columns.Add("FNO");
            ImpCapacityDT.Columns.Add("ConfirmDate");
            ImpCapacityDT.Columns.Add("ExpireDate");

            return ImpCapacityDT;
        }

        /// <summary>
        /// مجموع امتیاز اعضا یک شرکت اجرا را در تاریخ خاص بر اساس یک پروانه خاص بر می گرداند
        /// </summary>
        private int GetCapacityOfPointsByOfReId(int OfficeId, int CivilMeId, int SecondMeId, int PointsLimitation, int OfReId, string StartDate)
        {
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocOffGradeValuesManager DocOffGradeValuesManager = new TSP.DataManager.DocOffGradeValuesManager();
            int Capacity = 0;

            OfficeMemberManager = GetOfficeAllMembersByOfReId(OfficeId, OfReId);
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
                    GradeId = GetTechnicianGradeByTnReId((int)TSP.DataManager.TSProjectIngridientType.Implementer, Convert.ToInt32(OfficeMemberManager[i]["MfId"]));
                    GrdType = (int)TSP.DataManager.DocOffGradeValuesGrdType.Technician;
                }
                else if (OfmType == (int)TSP.DataManager.OfficeMemberType.Memar)
                {
                    GradeId = GetTechnicianGradeByTnReId((int)TSP.DataManager.TSProjectIngridientType.Implementer, Convert.ToInt32(OfficeMemberManager[i]["MfId"]));
                    GrdType = (int)TSP.DataManager.DocOffGradeValuesGrdType.ExperimentalArchitect;
                }

                DocOffGradeValuesManager.FindByGrdIdAndDate(GradeId, GrdType, StartDate);
                if (DocOffGradeValuesManager.Count > 0)
                    Capacity += Convert.ToInt32(DocOffGradeValuesManager[0]["Value"]) * Convert.ToInt32(DocOffGradeValuesManager[0]["CapacityPerValue"]);
            }

            if (Capacity > PointsLimitation)
                Capacity = PointsLimitation;

            return Capacity;
        }

        /// <summary>
        /// اعضا یک شرکت اجرا را بر اساس یک پروانه خاص بر می گرداند
        /// MeId, MeName, MjId, GrdId, Value, GrdTypeId, GrdType, OfpName, MainMember
        /// </summary>
        private DataTable GetImpOfficeMembersByOfReId(int OfficeId, int OfReId)
        {
            // GradeArr-----> ArrayList[0]: GradeId, ArrayList[1]: Type, ArrayList[2]: CivilGrdId, ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId

            ArrayList GradeArr = GetOfficeImpGradeByOfReId(OfficeId, OfReId);
            DataTable OfficeMembersDT = GetImpOfficeMembersDT();

            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocOffGradeValuesManager DocOffGradeValuesManager = new TSP.DataManager.DocOffGradeValuesManager();

            OfficeMemberManager = GetOfficeAllMembersByOfReId(OfficeId, OfReId);

            for (int i = 0; i < OfficeMemberManager.Count; i++)
            {
                DataRow drOfficeMembers = OfficeMembersDT.NewRow();

                int GradeId = 0;
                int GrdTypeId = 0;
                string GrdType = "";
                string MeName = "";
                int MjId = 0;
                int Value = 0;
                bool MainMember = false;

                int OfmType = Convert.ToInt32(OfficeMemberManager[i]["OfmType"]);

                if (OfmType == (int)TSP.DataManager.OfficeMemberType.Member)
                {
                    GradeId = GetGradeByMFId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                    GrdTypeId = (int)TSP.DataManager.DocOffGradeValuesGrdType.Engineer;
                    GrdType = "مهندس";
                    MeName = GetMeName(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]));
                    //MjId = GetMjId(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]));
                    ArrayList MjArray = GetMajorByMFId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]));
                    if (MjArray.Count > 0)
                        MjId = Convert.ToInt32(MjArray[0]);
                }
                else if (OfmType == (int)TSP.DataManager.OfficeMemberType.Kardan)
                {
                    GradeId = GetTechnicianGradeByTnReId((int)TSP.DataManager.TSProjectIngridientType.Implementer, Convert.ToInt32(OfficeMemberManager[i]["MfId"]));
                    GrdTypeId = (int)TSP.DataManager.DocOffGradeValuesGrdType.Technician;
                    GrdType = "كاردان";
                    ArrayList Temp = GetOthPersonName(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]));
                    MeName = Temp[0].ToString();
                    MjId = GetTechnicianMajorByTnReId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]));
                }
                else if (OfmType == (int)TSP.DataManager.OfficeMemberType.Memar)
                {
                    GradeId = GetTechnicianGradeByTnReId((int)TSP.DataManager.TSProjectIngridientType.Implementer, Convert.ToInt32(OfficeMemberManager[i]["MfId"]));
                    GrdTypeId = (int)TSP.DataManager.DocOffGradeValuesGrdType.ExperimentalArchitect;
                    GrdType = "معمار";
                    ArrayList Temp = GetOthPersonName(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]));
                    MeName = Temp[0].ToString();
                    MjId = GetTechnicianMajorByTnReId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]));
                }

                if (Convert.ToInt32(OfficeMemberManager[i]["PersonId"]) != Convert.ToInt32(GradeArr[3]) && Convert.ToInt32(OfficeMemberManager[i]["PersonId"]) != Convert.ToInt32(GradeArr[4]))
                {
                    DocOffGradeValuesManager.FindByGrdId(GradeId, GrdTypeId);
                    if (DocOffGradeValuesManager.Count > 0)
                        Value = Convert.ToInt32(DocOffGradeValuesManager[0]["Value"]) * Convert.ToInt32(DocOffGradeValuesManager[0]["CapacityPerValue"]);
                }
                else
                    MainMember = true;

                drOfficeMembers["MeId"] = OfficeMemberManager[i]["PersonId"];
                drOfficeMembers["MeName"] = MeName;
                drOfficeMembers["MjId"] = MjId;
                drOfficeMembers["GrdId"] = GradeId;
                if (Value != 0)
                    drOfficeMembers["Value"] = Value;
                else
                    drOfficeMembers["Value"] = " --- ";
                drOfficeMembers["GrdTypeId"] = GrdTypeId;
                drOfficeMembers["GrdType"] = GrdType;
                drOfficeMembers["MainMember"] = MainMember;

                OfficeMembersDT.Rows.Add(drOfficeMembers);
            }

            return OfficeMembersDT;
        }

     

        /// <summary>
        /// تعداد پروژه های در دست اجرا فرد، شرکت یا یک دفتر را در تاریخ خاص بر می گرداند
        /// </summary>
        private int GetTotalProjectNum(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId, string StartDate, string EndDate)
        {
            int ProjectNum = 0;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    ProjectNum += CurrentProjectNum((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOfficeEngOId, MemberTypeId, StartDate, EndDate);
                    ProjectNum += CurrentProjectNum((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOfficeEngOId, MemberTypeId, StartDate, EndDate);
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    ProjectNum += CurrentProjectNum(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId, StartDate, EndDate);
                    break;
            }

            return ProjectNum;
        }

        private int CurrentProjectNum(int ProjectIngridientTypeId, int MeOfficeEngOId, int MemberTypeId, string StartDate, string EndDate)
        {
            TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
            return ProjectCapacityDecrementManager.FindProjectNumByDate(MeOfficeEngOId, ProjectIngridientTypeId, MemberTypeId, StartDate, EndDate);
        }

        /// <summary>
        /// اطلاعات ظرفیت فرد، شرکت یا یک دفتر را در تاریخ خاص بر می گرداند
        /// TotalCapacity, TotalUsed , RemainCapacity, ProjectCount, MaxJoubCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetCapacityInfo(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, string StartDate, string EndDate)
        {
            DataTable CapacityInfoDT = GetCapacityInfoDT();
            DataTable TempDT = new DataTable();

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    TempDT = GetDsgObsTotalCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, StartDate, EndDate);
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    TempDT = GetDsgObsTotalCapacity(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, StartDate, EndDate);
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TempDT = GetImpTotalCapacity(MemberTypeId, MeOfficeEngOId, StartDate, EndDate);
                    break;
            }

            for (int i = 0; i < TempDT.Rows.Count; i++)
            {
                string SDate = TempDT.Rows[i]["StartDate"].ToString();
                string EDate = TempDT.Rows[i]["EndDate"].ToString();

                DataRow dr = CapacityInfoDT.NewRow();

                if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Observer)
                    dr["TotalCapacity"] = TempDT.Rows[i]["ObservationCapacity"];
                else
                    dr["TotalCapacity"] = TempDT.Rows[i]["TotalCapacity"];

                if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Implementer)
                    dr["TotalUsed"] = GetTotalUsedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId, null, null, SDate, EDate);
                else
                    dr["TotalUsed"] = GetTotalUsedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId, Convert.ToInt32(TempDT.Rows[i]["TotalCapacity"]), Convert.ToInt32(TempDT.Rows[i]["ObservationCapacity"]), SDate, EDate);

                dr["RemainCapacity"] = Convert.ToInt32(dr["TotalCapacity"]) - Convert.ToInt32(dr["TotalUsed"]);
                dr["ProjectCount"] = GetTotalProjectNum(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId, SDate, EDate);
                if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Implementer)
                {
                    dr["MaxJobCount"] = TempDT.Rows[i]["MaxUnitCount"];
                    dr["MaxFloor"] = TempDT.Rows[i]["MaxFloor"];
                    dr["ObservationPercent"] = "-----";
                }
                else
                {
                    dr["MaxJobCount"] = TempDT.Rows[i]["MaxJobCount"];
                    dr["MaxFloor"] = "-----";
                    dr["ObservationPercent"] = TempDT.Rows[i]["ObservationPercent"];
                }

                dr["Grade"] = TempDT.Rows[i]["Grade"];
                dr["ConditionalCapacity"] = TempDT.Rows[i]["ConditionalCapacity"];
                dr["StartDate"] = SDate;
                dr["EndDate"] = EDate;
                dr["FId"] = TempDT.Rows[i]["FId"];
                dr["FNO"] = TempDT.Rows[i]["FNO"];
                dr["ConfirmDate"] = TempDT.Rows[i]["ConfirmDate"];
                dr["ExpireDate"] = TempDT.Rows[i]["ExpireDate"];

                CapacityInfoDT.Rows.Add(dr);
            }

            return CapacityInfoDT;
        }

        /// <summary>
        /// TotalCapacity, UsedCapacity, RemainCapacity, ProjectNum, MaxJoubCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetCapacityInfoDT()
        {
            DataTable CapacityInfoDT = new DataTable();

            CapacityInfoDT.Columns.Add("TotalCapacity");
            CapacityInfoDT.Columns.Add("TotalUsed");
            CapacityInfoDT.Columns.Add("RemainCapacity");
            CapacityInfoDT.Columns.Add("ProjectCount");
            CapacityInfoDT.Columns.Add("MaxJobCount");
            CapacityInfoDT.Columns.Add("MaxFloor");
            CapacityInfoDT.Columns.Add("ConditionalCapacity");
            CapacityInfoDT.Columns.Add("ObservationPercent");
            CapacityInfoDT.Columns.Add("Grade");
            CapacityInfoDT.Columns.Add("StartDate");
            CapacityInfoDT.Columns.Add("EndDate");
            CapacityInfoDT.Columns.Add("FId");
            CapacityInfoDT.Columns.Add("FNO");
            CapacityInfoDT.Columns.Add("ConfirmDate");
            CapacityInfoDT.Columns.Add("ExpireDate");

            return CapacityInfoDT;
        }

        /// <summary>
        /// اطلاعات ظرفیت اعضا یک شرکت یا یک دفتر را بر اساس یک پروانه خاص بر می گرداند
        /// MeId, TotalCapacity, MaxJobCount, TotalUsed, RemainCapacity, ProjectCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, MjId, StartDate, EndDate
        /// </summary>
        private DataTable GetOfficeMembersCapacityInfo(int ProjectIngridientTypeId, int OfficeEngOId, int MemberTypeId, int OfReId, string StartDate, string EndDate)
        {
            // MembersDT -----> MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity,Grade, MjId, GradeInOfficeLicense, DesignInc, 
            //                  SameGradeInc,MajorInc, TotalDsgCapacity, TotalObsCapacity, MeId, MeName, ConditionalCapacity, StartDate, EndDate

            int CapacityDecrement = 0;

            DataTable MembersDT = new DataTable();
            DataTable CapacityInfoDT = GetOfficeMembersCapacityInfoDT();

            int TotalDsg = 0;
            int TotalObs = 0;
            int UsedDsg = 0;
            int UsedObs = 0;

            int DocOffIncreaseJobCapacityType = 0;
            if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Office)
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office;
            else if (MemberTypeId == (int)TSP.DataManager.TSMemberType.EngOffice)
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    MembersDT = GetOfficeMembersByFileId(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, OfReId, StartDate, EndDate);
                    for (int i = 0; i < MembersDT.Rows.Count; i++)
                    {
                        TotalDsg = Convert.ToInt32(MembersDT.Rows[0]["TotalDsgCapacity"]);
                        TotalObs = Convert.ToInt32(MembersDT.Rows[0]["TotalObsCapacity"]);
                        UsedDsg = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, Convert.ToInt32(MembersDT.Rows[0]["MeId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate);
                        if (TotalDsg != 0)
                            UsedObs = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, Convert.ToInt32(MembersDT.Rows[0]["MeId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate) * TotalDsg / TotalObs;
                        CapacityDecrement = UsedDsg + UsedObs;
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(MembersDT.Rows[0]["MeId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate);

                        DataRow dr = CapacityInfoDT.NewRow();

                        dr["MeId"] = MembersDT.Rows[i]["MeId"];
                        dr["TotalCapacity"] = TotalDsg;
                        dr["MaxJobCount"] = MembersDT.Rows[i]["MaxJobCount"];
                        dr["TotalUsed"] = CapacityDecrement;
                        dr["RemainCapacity"] = TotalDsg - CapacityDecrement;
                        dr["ProjectCount"] = ProjectNum;
                        dr["MaxFloor"] = "-----";
                        dr["ConditionalCapacity"] = MembersDT.Rows[i]["ConditionalCapacity"];
                        dr["ObservationPercent"] = MembersDT.Rows[i]["ObservationPercent"];
                        dr["Grade"] = MembersDT.Rows[i]["Grade"];
                        dr["MjId"] = MembersDT.Rows[i]["MjId"];
                        dr["StartDate"] = MembersDT.Rows[i]["StartDate"];
                        dr["EndDate"] = MembersDT.Rows[i]["EndDate"];

                        CapacityInfoDT.Rows.Add(dr);
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    MembersDT = GetOfficeMembersByFileId(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, OfReId, StartDate, EndDate);
                    for (int i = 0; i < MembersDT.Rows.Count; i++)
                    {
                        TotalDsg = Convert.ToInt32(MembersDT.Rows[0]["TotalDsgCapacity"]);
                        TotalObs = Convert.ToInt32(MembersDT.Rows[0]["TotalObsCapacity"]);
                        if (TotalObs != 0)
                            UsedDsg = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, Convert.ToInt32(MembersDT.Rows[0]["MeId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate) * TotalObs / TotalDsg;
                        UsedObs = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, Convert.ToInt32(MembersDT.Rows[0]["MeId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate);
                        CapacityDecrement = UsedDsg + UsedObs;
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(MembersDT.Rows[0]["MeId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate);

                        DataRow dr = CapacityInfoDT.NewRow();

                        dr["MeId"] = MembersDT.Rows[i]["MeId"];
                        dr["TotalCapacity"] = TotalObs;
                        dr["MaxJobCount"] = MembersDT.Rows[i]["MaxJobCount"];
                        dr["TotalUsed"] = CapacityDecrement;
                        dr["RemainCapacity"] = TotalObs - CapacityDecrement;
                        dr["ProjectCount"] = ProjectNum;
                        dr["MaxFloor"] = "-----";
                        dr["ConditionalCapacity"] = MembersDT.Rows[i]["ConditionalCapacity"];
                        dr["ObservationPercent"] = MembersDT.Rows[i]["ObservationPercent"];
                        dr["Grade"] = MembersDT.Rows[i]["Grade"];
                        dr["MjId"] = MembersDT.Rows[i]["MjId"];
                        dr["StartDate"] = MembersDT.Rows[i]["StartDate"];
                        dr["EndDate"] = MembersDT.Rows[i]["EndDate"];

                        CapacityInfoDT.Rows.Add(dr);
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
                    OfficeMemberManager = GetOfficeMembersByOfReId(OfficeEngOId, DocOffIncreaseJobCapacityType, OfReId);

                    for (int i = 0; i < OfficeMemberManager.Count; i++)
                    {
                        CapacityDecrement = OfficeMembersUsedCapacity(ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate);
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate);
                        int ConditionalCapacity = GetConditionalCapacity(Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), ProjectIngridientTypeId);

                        DataRow dr = CapacityInfoDT.NewRow();

                        dr["MeId"] = OfficeMemberManager[0]["PersonId"];
                        dr["TotalCapacity"] = "-----";
                        dr["MaxJobCount"] = "-----";
                        dr["TotalUsed"] = CapacityDecrement;
                        dr["RemainCapacity"] = "-----"; ;
                        dr["ProjectCount"] = ProjectNum;
                        dr["MaxFloor"] = "-----";
                        dr["ConditionalCapacity"] = ConditionalCapacity;
                        dr["ObservationPercent"] = MembersDT.Rows[i]["ObservationPercent"];
                        dr["Grade"] = "-----";
                        dr["MjId"] = "-----";
                        dr["StartDate"] = "-----";
                        dr["EndDate"] = EndDate;

                        CapacityInfoDT.Rows.Add(dr);
                    }
                    break;
            }
            return CapacityInfoDT;
        }

        /// <summary>
        /// MeId, TotalCapacity, MaxJoubCount, TotalUsed, RemainCapacity, ProjectCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, MjId, StartDate, EndDate
        /// </summary>
        private DataTable GetOfficeMembersCapacityInfoDT()
        {
            DataTable CapacityInfoDT = new DataTable();

            CapacityInfoDT.Columns.Add("MeId");
            CapacityInfoDT.Columns.Add("TotalCapacity");
            CapacityInfoDT.Columns.Add("MaxJobCount");
            CapacityInfoDT.Columns.Add("TotalUsed");
            CapacityInfoDT.Columns.Add("RemainCapacity");
            CapacityInfoDT.Columns.Add("ProjectCount");
            CapacityInfoDT.Columns.Add("MaxFloor");
            CapacityInfoDT.Columns.Add("ConditionalCapacity");
            CapacityInfoDT.Columns.Add("ObservationPercent");
            CapacityInfoDT.Columns.Add("Grade");
            CapacityInfoDT.Columns.Add("MjId");
            CapacityInfoDT.Columns.Add("StartDate");
            CapacityInfoDT.Columns.Add("EndDate");

            return CapacityInfoDT;
        }

        /// <summary>
        /// ظرفیت مصرف شده یکی از اعضا یک شرکت یا یک دفتر را در تاریخ خاص بر می گرداند
        /// </summary>
        private int OfficeMembersUsedCapacity(int ProjectIngridientTypeId, int MeOthPId, int MemberTypeId, string StartDate, string EndDate)
        {
            int CapacityDecrement = 0;
            TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
            ProjectOfficeMembersManager.FindUsedCapacityByDate(MeOthPId, ProjectIngridientTypeId, MemberTypeId, StartDate, EndDate);
            for (int i = 0; i < ProjectOfficeMembersManager.Count; i++)
                CapacityDecrement += Convert.ToInt32(ProjectOfficeMembersManager[i]["CapacityDecrement"]);
            return CapacityDecrement;
        }

        /// <summary>
        /// تعداد پروژه های در دست اجرا عضوی از یک شرکت یا یک دفتر را در تاریخ خاص بر می گرداند
        /// </summary>
        private int GetOfficeMembersTotalProjectNum(int ProjectIngridientTypeId, int MeOthPId, int MemberTypeId, string StartDate, string EndDate)
        {
            int ProjectNum = 0;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    ProjectNum += OfficeMembersCurrentProjectNum((int)TSP.DataManager.TSProjectIngridientType.Designer, MeOthPId, MemberTypeId, StartDate, EndDate);
                    ProjectNum += OfficeMembersCurrentProjectNum((int)TSP.DataManager.TSProjectIngridientType.Observer, MeOthPId, MemberTypeId, StartDate, EndDate);
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    ProjectNum += OfficeMembersCurrentProjectNum(ProjectIngridientTypeId, MeOthPId, MemberTypeId, StartDate, EndDate);
                    break;
            }

            return ProjectNum;
        }

        private int OfficeMembersCurrentProjectNum(int ProjectIngridientTypeId, int MeOthPId, int MemberTypeId, string StartDate, string EndDate)
        {
            TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
            return ProjectOfficeMembersManager.FindProjectNumByDate(MeOthPId, ProjectIngridientTypeId, MemberTypeId, StartDate, EndDate);
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// کلیه پروانه های یک شخص که در یک بازه زمانی فعال بوده اند را بر می گرداند
        /// ["MfId"], ["MFNO"]:شماره پروانه, ["Date"]:تاریخ تایید , ["ExpireDate"]:تاریخ پایان اعتبار پروانه
        /// </summary>
        public DataTable GetDocMemberFiles(int MeId, string StartDate, string EndDate)
        {
            ArrayList Temp = GetDocMemberFile(MeId, StartDate, EndDate);
            DataTable DT = new DataTable();
            for (int i = 0; i < Temp.Count; i++)
                DT.Rows.Add((DataRow)Temp[i]);
            return DT;
        }

        /// <summary>
        /// کلیه پروانه های یک شرکت که در یک بازه زمانی فعال بوده اند را بر می گرداند
        /// ["OfReId"], ["MFNO"]:شماره پروانه, ["AnswerDate"]:تاریخ تایید , ["ExpireDate"]:تاریخ پایان اعتبار پروانه
        /// </summary>
        public DataTable GetDocOfficeFiles(int OfId, string StartDate, string EndDate)
        {
            ArrayList Temp = GetDocOfficeFile(OfId, StartDate, EndDate);
            DataTable DT = new DataTable();
            for (int i = 0; i < Temp.Count; i++)
                DT.Rows.Add((DataRow)Temp[i]);
            return DT;
        }

        /// <summary>
        /// کلیه پروانه های یک دفتر که در یک بازه زمانی فعال بوده اند را بر می گرداند
        /// ["EOfId"], ["FileNo"]:شماره پروانه, ["AnswerDate"]:تاریخ تایید , ["ExpireDate"]:تاریخ پایان اعتبار پروانه
        /// </summary>
        public DataTable GetDocEngOfficeFiles(int EngOfId, string StartDate, string EndDate)
        {
            ArrayList Temp = GetDocEngOfficeFile(EngOfId, StartDate, EndDate);
            DataTable DT = new DataTable();
            for (int i = 0; i < Temp.Count; i++)
                DT.Rows.Add((DataRow)Temp[i]);
            return DT;
        }

        /// <summary>
        /// ظرفیت اضافی یا کم شده یک شخص یا شرکت یا دفتر را در تاریخ خاص بر می گرداند
        /// </summary>
        public int GetCIncDecCapacity(int MeOfficeEngOId, int ProjectIngridientTypeId, string StartDate, string EndDate)
        {
            return GetConditionalCapacity(MeOfficeEngOId, ProjectIngridientTypeId, StartDate, EndDate);
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک عضو را در تاریخ خاص بر می گرداند
        /// MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity, Grade, MjId, MeId, MeName, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetMembersDsgObsCapacity(int MeId, int ProjectIngridientTypeId, string StartDate, string EndDate)
        {
            return GetMemberDsgObsCapacity(MeId, ProjectIngridientTypeId, StartDate, EndDate);
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک عضو را بر اساس یک پروانه خاص بر می گرداند
        /// MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity, Grade, MjId, MeId, MeName, ConditionalCapacity, StartDate, EndDate
        /// </summary>
        public DataTable GetMembersDsgObsCapacityByMFId(int MeId, int ProjectIngridientTypeId, int MFId, string StartDate, string EndDate)
        {
            return GetMemberDsgObsCapacityByMFId(MeId, ProjectIngridientTypeId, MFId, StartDate, EndDate);
        }

        /// <summary>
        /// کل ظرفیت و تعداد کار مجاز فرد، شرکت یا یک دفتر طراحی و نظارت را در تاریخ خاص بر می گرداند
        /// MaxJobCount(int), TotalCapacity(int), ObservationCapacity(int), ConditionalCapacity(int), StartDate(string), EndDate(string),ObservationPercent, Grade, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetDsgObsTotalCapacity(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, string StartDate, string EndDate)
        {
            DataTable CapacityDT = GetOfficeDsgObsCapacityDT();
            DataTable CapDT = new DataTable();

            CapacityDT.Columns.Add("ObservationPercent");
            CapacityDT.Columns.Add("Grade");

            switch (MemberTypeId)
            {
                case (int)TSP.DataManager.TSMemberType.Member:
                    CapDT = GetMemberDsgObsCapacity(MeOfficeEngOId, ProjectIngridientTypeId, StartDate, EndDate);
                    break;

                case (int)TSP.DataManager.TSMemberType.Office:
                    CapDT = GetOfficeDsgObsCapacity(MeOfficeEngOId, ProjectIngridientTypeId, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office, StartDate, EndDate);
                    break;

                case (int)TSP.DataManager.TSMemberType.EngOffice:
                    CapDT = GetOfficeDsgObsCapacity(MeOfficeEngOId, ProjectIngridientTypeId, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice, StartDate, EndDate);
                    break;
            }

            for (int i = 0; i < CapDT.Rows.Count; i++)
            {
                DataRow dr = CapacityDT.NewRow();

                dr["MaxJobCount"] = CapDT.Rows[i]["MaxJobCount"];
                dr["TotalCapacity"] = CapDT.Rows[i]["TotalCapacity"];
                dr["ObservationCapacity"] = CapDT.Rows[i]["ObservationCapacity"];
                dr["ConditionalCapacity"] = CapDT.Rows[i]["ConditionalCapacity"];
                dr["StartDate"] = CapDT.Rows[i]["StartDate"];
                dr["EndDate"] = CapDT.Rows[i]["EndDate"];
                dr["FId"] = CapDT.Rows[i]["FId"];
                dr["FNO"] = CapDT.Rows[i]["FNO"];
                dr["ConfirmDate"] = CapDT.Rows[i]["ConfirmDate"];
                dr["ExpireDate"] = CapDT.Rows[i]["ExpireDate"];

                if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
                {
                    dr["ObservationPercent"] = CapDT.Rows[i]["ObservationPercent"];
                    dr["Grade"] = CapDT.Rows[i]["Grade"];
                }
                else
                {
                    dr["ObservationPercent"] = "-----";
                    dr["Grade"] = "-----";
                }

                CapacityDT.Rows.Add(dr);
            }

            return CapacityDT;
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت در تاریخ خاص را بر می گرداند
        /// MaxJobCount, TotalCapacity, ObservationCapacity, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetOfficeDsgCapacity(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, string StartDate, string EndDate)
        {
            return GetOfficeDsgObsCapacity(OfficeEngoId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, StartDate, EndDate);
        }

        /// <summary>
        /// افراد یک دفتر یا شرکت و ظرفیت طراحی و نظارت آنها را بر اساس یک پروانه خاص بر می گرداند
        /// MembersDT -----> MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity,Grade, MjId, GradeInOfficeLicense, DesignInc, SameGradeInc,MajorInc, TotalDsgCapacity, TotalObsCapacity, MeId, MeName, ConditionalCapacity, StartDate, EndDate, Factor
        /// </summary>            
        public DataTable GetOfficeMembersDsgObsCapacity(int OfficeId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, int FileId, string StartDate, string EndDate)
        {
            return GetOfficeMembersByFileId(OfficeId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, FileId, StartDate, EndDate);
        }

        /// <summary>
        /// کل ظرفیت و تعداد کار و تعداد طبقات مجاز فرد، شرکت یا یک دفتر اجرایی را در تاریخ خاص بر می گرداند
        /// MaxFloor(int), TotalCapacity(int), MaxUnitCount(int), ConditionalCapacity(int), StartDate(string), EndDate(string), FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetImpTotalCapacity(int MemberTypeId, int MeOfficeEngOId, string StartDate, string EndDate)
        {
            DataTable ImpCapacityDT = GetMemberImpCapacityDT();
            DataTable CapDT = new DataTable();

            ArrayList CapacityArr = new ArrayList();
            ArrayList CapArr = new ArrayList();

            switch (MemberTypeId)
            {
                case (int)TSP.DataManager.TSMemberType.Member:
                    CapDT = GetMemberImpCapacity(MeOfficeEngOId, StartDate, EndDate);
                    break;

                case (int)TSP.DataManager.TSMemberType.Office:
                    CapDT = GetOfficeImpCapacity(MeOfficeEngOId, StartDate, EndDate);
                    break;
            }

            for (int i = 0; i < CapDT.Rows.Count; i++)
            {
                DataRow dr = ImpCapacityDT.NewRow();

                dr["MaxFloor"] = CapDT.Rows[i]["MaxFloor"];
                dr["TotalCapacity"] = CapDT.Rows[i]["TotalCapacity"];
                dr["MaxUnitCount"] = CapDT.Rows[i]["MaxUnitCount"];
                dr["Grade"] = CapDT.Rows[i]["Grade"];
                dr["ConditionalCapacity"] = CapDT.Rows[i]["ConditionalCapacity"];
                dr["StartDate"] = CapDT.Rows[i]["StartDate"];
                dr["EndDate"] = CapDT.Rows[i]["EndDate"];
                dr["FId"] = CapDT.Rows[i]["FId"];
                dr["FNO"] = CapDT.Rows[i]["FNO"];
                dr["ConfirmDate"] = CapDT.Rows[i]["ConfirmDate"];
                dr["ExpireDate"] = CapDT.Rows[i]["ExpireDate"];

                if (Convert.ToInt32(dr["MaxFloor"]) == -1)
                    dr["MaxFloor"] = "بدون محدودیت";

                if (Convert.ToInt32(dr["MaxUnitCount"]) == -1)
                    dr["MaxUnitCount"] = "بدون محدودیت";

                ImpCapacityDT.Rows.Add(dr);
            }

            return ImpCapacityDT;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک عضو را در تاریخ خاص را بر می گرداند
        /// MaxFloor, TotalCapacity, MaxUnitCount, Grade, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetMembersImpCapacity(int MeId, string StartDate, string EndDate)
        {
            DataTable Temp = GetMemberImpCapacity(MeId, StartDate, EndDate);

            for (int i = 0; i < Temp.Rows.Count; i++)
            {
                if (Convert.ToInt32(Temp.Rows[i]["MaxFloor"]) == -1)
                    Temp.Rows[i]["MaxFloor"] = "بدون محدودیت";
            }
            return Temp;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک عضو را در تاریخ خاص بر اساس یک پروانه خاص بر می گرداند
        /// MaxFloor, TotalCapacity, MaxUnitCount, Grade, ConditionalCapacity, StartDate, EndDate
        /// </summary>
        public DataTable GetMembersImpCapacityByMFId(int MeId, int MFId, string StartDate, string EndDate)
        {
            return GetMemberImpCapacityByMFId(MeId, MFId, StartDate, EndDate);
        }

        /// <summary>
        /// ظرفیت کل اجرا یک شرکت را در تاریخ خاص را بر می گرداند
        /// MaxFloor, TotalCapacity, MaxUnitCount, ConditionalCapacity, Grade, GrdType, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetOfficesImpCapacity(int OfficeId, string StartDate, string EndDate)
        {
            DataTable Temp = GetOfficeImpCapacity(OfficeId, StartDate, EndDate);

            for (int i = 0; i < Temp.Rows.Count; i++)
            {
                if (Convert.ToInt32(Temp.Rows[i]["MaxFloor"]) == -1)
                    Temp.Rows[i]["MaxFloor"] = "بدون محدودیت";

                if (Convert.ToInt32(Temp.Rows[i]["MaxUnitCount"]) == -1)
                    Temp.Rows[i]["MaxUnitCount"] = "بدون محدودیت";
            }

            return Temp;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک شرکت را در تاریخ خاص بر اساس یک پروانه خاص بر می گرداند
        /// MaxFloor, TotalCapacity, MaxUnitCount, ConditionalCapacity, Grade, GrdType, StartDate, EndDate
        /// </summary>
        public DataTable GetOfficesImpCapacityByFileId(int OfficeId, int FileId, string StartDate, string EndDate)
        {
            return GetOfficeImpCapacityByFileId(OfficeId, FileId, StartDate, EndDate);
        }

        /// <summary>
        /// اعضا یک شرکت اجرا را بر اساس یک پروانه خاص بر می گرداند
        /// MeId, MeName, MjId, GrdId, Value, GrdTypeId, GrdType, OfpName, MainMember
        /// </summary>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable GetImpOffMembers(int OfficeId, int OfReId)
        {
            return GetImpOfficeMembersByOfReId(OfficeId, OfReId);
        }

        /// <summary>
        /// اطلاعات ظرفیت فرد، شرکت یا یک دفتر را در تاریخ خاص بر می گرداند
        /// TotalCapacity, UsedCapacity , RemainCapacity, ProjectNum, MaxJoubCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetCapacityInformation(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, string StartDate, string EndDate)
        {
            DataTable Temp = GetCapacityInfo(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, StartDate, EndDate);

            for (int i = 0; i < Temp.Rows.Count; i++)
            {
                if (Convert.ToInt32(Temp.Rows[i]["MaxFloor"]) == -1)
                    Temp.Rows[i]["MaxFloor"] = "بدون محدودیت";

                if (Convert.ToInt32(Temp.Rows[i]["MaxJoubCount"]) == -1)
                    Temp.Rows[i]["MaxJoubCount"] = "بدون محدودیت";
            }
            return Temp;
        }

        /// <summary>
        /// اطلاعات ظرفیت اعضا یک شرکت یا یک دفتر را بر اساس یک پروانه خاص بر می گرداند
        /// MeId, TotalCapacity, MaxJobCount, TotalUsed, RemainCapacity, ProjectCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, MjId, StartDate, EndDate
        /// </summary>
        public DataTable GetOfficeMembersCapacityInformation(int ProjectIngridientTypeId, int OfficeEngOId, int MemberTypeId, int OfReId, string StartDate, string EndDate)
        {
            DataTable Temp = GetOfficeMembersCapacityInfo(ProjectIngridientTypeId, OfficeEngOId, MemberTypeId, OfReId, StartDate, EndDate);

            for (int i = 0; i < Temp.Rows.Count; i++)
            {
                if (Convert.ToInt32(Temp.Rows[i]["MaxFloor"]) == -1)
                    Temp.Rows[i]["MaxFloor"] = "بدون محدودیت";

                if (Convert.ToInt32(Temp.Rows[i]["MaxJoubCount"]) == -1)
                    Temp.Rows[i]["MaxJoubCount"] = "بدون محدودیت";
            }
            return Temp;
        }

        #endregion

        #endregion

        #region CapacityAssignmentInDate

        #region Private-Methods

        /// <summary>
        /// درصد اختصاص ظرفیت جاری را در تاریخ خاص بر می گرداند
        /// ArrayList[0] = CapacityPrcntSum, ArrayList[1] = JobCountPrcntSum
        /// </summary>
        private ArrayList GetCurrentPrcntsSum(string Date)
        {
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            return CapacityAssignmentManager.GetPrcntsSumByDate(Date);
        }

        /// <summary>
        /// مقدار ظرفیت را بر اساس درصد اختصاص ظرفیت در تاریخ خاص محاسبه می کند
        /// ArrayList[0] = MaxJobCount, ArrayList[1] = MaxJobCapacity
        /// </summary>
        private ArrayList CalculateMaxJobCount(int MaxJobCount, int MaxJobCapacity, string Date)
        {
            ArrayList CapacityAssArr = GetCurrentPrcntsSum(Date);

            int CapacityPrcntSum = Convert.ToInt32(CapacityAssArr[0]);
            int JobCountPrcntSum = Convert.ToInt32(CapacityAssArr[1]);

            CapacityAssArr[0] = Math.Ceiling(Convert.ToDouble(JobCountPrcntSum * MaxJobCount) / 100);
            CapacityAssArr[1] = Math.Ceiling(Convert.ToDouble(CapacityPrcntSum * MaxJobCapacity) / 100);

            return CapacityAssArr;
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک عضو را در تاریخ خاص بر اساس اختصاص ظرفیت و پروانه ها بر می گرداند
        /// MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity, Grade, MjId, MeId, MeName, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetMemberDsgObsCapacityPerStage(int MeId, int ProjectIngridientTypeId, string StartDate, string EndDate)
        {
            DataTable DsgObsCapacityDT = GetMemberDsgObsCapacityDT();
            ArrayList DocMemberFileArr = GetDocMemberFile(MeId, StartDate, EndDate);

            for (int i = 0; i < DocMemberFileArr.Count; i++)
            {
                ArrayList DateArr = GetSDateAndEDateFormDocFile(DocMemberFileArr, StartDate, EndDate, i);

                if (DateArr.Count > 0)
                {
                    int ConditionalCapacity = GetConditionalCapacity(MeId, ProjectIngridientTypeId, DateArr[0].ToString(), DateArr[1].ToString());
                    int MFId = Convert.ToInt32(((DataRow)DocMemberFileArr[i])["MfId"]);

                    int Grade = GetGradeByMFId(MFId, MeId, ProjectIngridientTypeId);
                    if (Grade != 0)
                    {
                        int MjId = 0;
                        ArrayList MjArray = GetMajor(MeId);
                        if (MjArray.Count > 0)
                            MjId = Convert.ToInt32(MjArray[0]);

                        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                        MemberManager.FindByCode(MeId);

                        TSP.DataManager.DocOffMemberCapacityManager MemberCapacityManager = new TSP.DataManager.DocOffMemberCapacityManager();
                        MemberCapacityManager.FindByGrdIdAndDate(Grade, DateArr[0].ToString());

                        if (MemberCapacityManager.Count > 0)
                        {
                            //CapacityAssArr ----> ArrayList[0] = MaxJobCount, ArrayList[1] = MaxJobCapacity
                            ArrayList CapacityAssArr = CalculateMaxJobCount(Convert.ToInt32(MemberCapacityManager[0]["MaxJobCount"]), Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]), DateArr[0].ToString());

                            DataRow dr = DsgObsCapacityDT.NewRow();

                            dr["MaxJobCount"] = CapacityAssArr[0].ToString();
                            dr["TotalCapacity"] = (Convert.ToInt32(CapacityAssArr[1]) + ConditionalCapacity).ToString();
                            dr["ObservationPercent"] = MemberCapacityManager[0]["ObservationPercent"].ToString();
                            dr["ObservationCapacity"] = (Convert.ToInt32(Convert.ToDouble(dr["TotalCapacity"]) * Convert.ToDouble(dr["ObservationPercent"]))) + ConditionalCapacity;
                            dr["Grade"] = Grade.ToString();
                            dr["MjId"] = MjId.ToString();
                            dr["GradeInOfficeLicense"] = 0;
                            dr["DesignInc"] = 0;
                            dr["SameGradeInc"] = 0;
                            dr["MajorInc"] = 0;
                            dr["TotalDsgCapacity"] = 0;
                            dr["TotalObsCapacity"] = 0;
                            dr["MeId"] = MeId;
                            dr["MeName"] = MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
                            dr["ConditionalCapacity"] = ConditionalCapacity;
                            dr["StartDate"] = DateArr[0].ToString();
                            dr["EndDate"] = DateArr[1].ToString();
                            dr["FId"] = ((DataRow)DocMemberFileArr[i])["MfId"];
                            dr["FNO"] = ((DataRow)DocMemberFileArr[i])["MFNO"];
                            dr["ConfirmDate"] = ((DataRow)DocMemberFileArr[i])["Date"];
                            dr["ExpireDate"] = ((DataRow)DocMemberFileArr[i])["ExpireDate"];

                            DsgObsCapacityDT.Rows.Add(dr);
                        }
                    }
                }
            }
            return DsgObsCapacityDT;
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک عضو را در تاریخ خاص بر اساس اختصاص ظرفیت و یک پروانه خاص بر می گرداند
        /// MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity, Grade, MjId, MeId, MeName, ConditionalCapacity, StartDate, EndDate
        /// </summary>
        private DataTable GetMemberDsgObsCapacityPerStageByMFId(int MeId, int ProjectIngridientTypeId, int MFId, string StartDate, string EndDate)
        {
            DataTable DsgObsCapacityDT = GetMemberDsgObsCapacityDT();

            int ConditionalCapacity = GetConditionalCapacity(MeId, ProjectIngridientTypeId, StartDate, EndDate);

            int Grade = GetGradeByMFId(MFId, MeId, ProjectIngridientTypeId);
            if (Grade != 0)
            {
                int MjId = 0;
                ArrayList MjArray = GetMajor(MeId);
                if (MjArray.Count > 0)
                    MjId = Convert.ToInt32(MjArray[0]);

                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                MemberManager.FindByCode(MeId);

                TSP.DataManager.DocOffMemberCapacityManager MemberCapacityManager = new TSP.DataManager.DocOffMemberCapacityManager();
                MemberCapacityManager.FindByGrdIdAndDate(Grade, StartDate);

                if (MemberCapacityManager.Count > 0)
                {
                    //CapacityAssArr ----> ArrayList[0] = MaxJobCount, ArrayList[1] = MaxJobCapacity
                    ArrayList CapacityAssArr = CalculateMaxJobCount(Convert.ToInt32(MemberCapacityManager[0]["MaxJobCount"]), Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]), StartDate);

                    DataRow dr = DsgObsCapacityDT.NewRow();

                    dr["MaxJobCount"] = CapacityAssArr[0].ToString();
                    dr["TotalCapacity"] = (Convert.ToInt32(CapacityAssArr[1]) + ConditionalCapacity).ToString();
                    dr["ObservationPercent"] = MemberCapacityManager[0]["ObservationPercent"].ToString();
                    dr["ObservationCapacity"] = (Convert.ToInt32(Convert.ToDouble(dr["TotalCapacity"]) * Convert.ToDouble(dr["ObservationPercent"]))) + ConditionalCapacity;
                    dr["Grade"] = Grade.ToString();
                    dr["MjId"] = MjId.ToString();
                    dr["GradeInOfficeLicense"] = 0;
                    dr["DesignInc"] = 0;
                    dr["SameGradeInc"] = 0;
                    dr["MajorInc"] = 0;
                    dr["TotalDsgCapacity"] = 0;
                    dr["TotalObsCapacity"] = 0;
                    dr["MeId"] = MeId;
                    dr["MeName"] = MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
                    dr["ConditionalCapacity"] = ConditionalCapacity;
                    dr["StartDate"] = StartDate;
                    dr["EndDate"] = EndDate;

                    DsgObsCapacityDT.Rows.Add(dr);
                }
            }
            return DsgObsCapacityDT;
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت را در تاریخ خاص بر اساس اختصاص ظرفیت و پروانه ها بر می گرداند
        /// MaxJobCount, TotalCapacity, ObservationCapacity, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetOfficeDsgObsCapacityPerStage(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, string StartDate, string EndDate)
        {
            // MajorArr -----> ArrayList[0]: MainMajorNum, ArrayList[1]: SecondaryMajorNum, ArrayList[2]: TotalMajorNum

            // MembersDT -----> MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity,Grade, MjId, GradeInOfficeLicense, DesignInc, 
            //                  SameGradeInc,MajorInc, TotalDsgCapacity, TotalObsCapacity, MeId, MeName, ConditionalCapacity

            TSP.DataManager.DocOffMajorNum DocOffMajorNum = new TSP.DataManager.DocOffMajorNum();
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocOffIncreaseJobCapacityManager IncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();

            DataTable MembersDT = GetMemberDsgObsCapacityDT();
            DataTable DsgObsCapacityDT = GetOfficeDsgObsCapacityDT();
            ArrayList DocOfficeFileArr = new ArrayList();

            if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
                DocOfficeFileArr = GetDocOfficeFile(OfficeEngoId, StartDate, EndDate);
            else if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice)
                DocOfficeFileArr = GetDocEngOfficeFile(OfficeEngoId, StartDate, EndDate);

            for (int k = 0; k < DocOfficeFileArr.Count; k++)
            {
                ArrayList DateArr = GetSDateAndEDateFormDocOfficeFile(DocOfficeFileArr, StartDate, EndDate, k);

                if (DateArr.Count > 0)
                {
                    int ConditionalCapacity = GetConditionalCapacity(OfficeEngoId, ProjectIngridientTypeId, DateArr[0].ToString(), DateArr[1].ToString());

                    int FileId = -1;
                    string MFNo = "";

                    if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
                    {
                        FileId = Convert.ToInt32(((DataRow)DocOfficeFileArr[k])["OfReId"]);
                        MFNo = ((DataRow)DocOfficeFileArr[k])["MFNO"].ToString();
                    }
                    else if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice)
                    {
                        FileId = Convert.ToInt32(((DataRow)DocOfficeFileArr[k])["EofId"]);
                        MFNo = ((DataRow)DocOfficeFileArr[k])["FileNo"].ToString();
                    }

                    OfficeMemberManager = GetOfficeMembersByOfReId(OfficeEngoId, DocOffIncreaseJobCapacityType, FileId);

                    for (int i = 0; i < OfficeMemberManager.Count; i++)
                    {
                        if (Convert.ToInt32(OfficeMemberManager[i]["OfmType"]) == (int)TSP.DataManager.OfficeMemberType.Member)
                        {
                            DataTable Member = GetMemberDsgObsCapacityPerStageByMFId(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[i]["MfId"]), DateArr[0].ToString(), DateArr[1].ToString());
                            if (Member.Rows.Count != 0)
                            {
                                Member.Rows[0]["GradeInOfficeLicense"] = GetGradeByMFId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId).ToString();
                                DataRow drow = MembersDT.NewRow();

                                drow["MaxJobCount"] = Member.Rows[0]["MaxJobCount"];
                                drow["TotalCapacity"] = Member.Rows[0]["TotalCapacity"];
                                drow["ObservationPercent"] = Member.Rows[0]["ObservationPercent"];
                                drow["ObservationCapacity"] = Member.Rows[0]["ObservationCapacity"];
                                drow["Grade"] = Member.Rows[0]["Grade"];
                                drow["MjId"] = Member.Rows[0]["MjId"];
                                drow["GradeInOfficeLicense"] = Member.Rows[0]["GradeInOfficeLicense"];
                                drow["MeId"] = Member.Rows[0]["MeId"];
                                drow["MeName"] = Member.Rows[0]["MeName"];
                                drow["ConditionalCapacity"] = Member.Rows[0]["ConditionalCapacity"];
                                drow["StartDate"] = Member.Rows[0]["StartDate"];
                                drow["EndDate"] = Member.Rows[0]["EndDate"];

                                MembersDT.Rows.Add(drow);
                            }
                        }
                    }

                    int MaxJobCount = 0;
                    int MaxJobCapacity = 0;
                    int ObservationCapacity = 0;

                    if (MembersDT.Rows.Count != 0)
                    {
                        ArrayList MajorArr = GetMajorNumFromDT(MembersDT);

                        DocOffMajorNum.FindByMajorsNum((int)MajorArr[0], (int)MajorArr[1], (int)MajorArr[2]);
                        IncreaseJobCapacityManager.FindByMNumIdAndDate(Convert.ToInt32(DocOffMajorNum[0]["MNumId"]), DocOffIncreaseJobCapacityType, DateArr[0].ToString());

                        for (int i = 0; i < MembersDT.Rows.Count; i++)
                        {
                            bool SameGradeInc = false;
                            bool MajorInc = false;

                            MembersDT.Rows[i]["DesignInc"] = (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["DesignIncPer"]) / 100).ToString();
                            for (int j = 0; j < MembersDT.Rows.Count; j++)
                            {
                                if (i != j)
                                {
                                    if (MembersDT.Rows[i]["MjId"].ToString() == MembersDT.Rows[j]["MjId"].ToString())
                                    {
                                        if (MembersDT.Rows[i]["GradeInOfficeLicense"].ToString() == MembersDT.Rows[j]["GradeInOfficeLicense"].ToString())
                                        {
                                            if (!MajorInc)
                                                SameGradeInc = true;
                                        }
                                        else
                                            SameGradeInc = false;

                                        MajorInc = true;
                                    }

                                }
                            }
                            if (SameGradeInc)
                                MembersDT.Rows[i]["SameGradeInc"] = (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["SameGradeIncPer"]) / 100).ToString();
                            else
                                MembersDT.Rows[i]["SameGradeInc"] = 0;

                            if (MajorInc)
                                MembersDT.Rows[i]["MajorInc"] = (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["MajorIncPer"]) / 100).ToString();
                            else
                                MembersDT.Rows[i]["MajorInc"] = 0;

                            MembersDT.Rows[i]["TotalDsgCapacity"] = Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) + Convert.ToInt32(MembersDT.Rows[i]["DesignInc"]) + Convert.ToInt32(MembersDT.Rows[i]["SameGradeInc"]) + Convert.ToInt32(MembersDT.Rows[i]["MajorInc"]);
                            MembersDT.Rows[i]["TotalObsCapacity"] = Convert.ToInt32(Convert.ToDouble(MembersDT.Rows[i]["ObservationPercent"]) * (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) + Convert.ToInt32(MembersDT.Rows[i]["DesignInc"]) + Convert.ToInt32(MembersDT.Rows[i]["SameGradeInc"]) + Convert.ToInt32(MembersDT.Rows[i]["MajorInc"])));

                            MaxJobCount += Convert.ToInt32(MembersDT.Rows[i]["MaxJobCount"]);
                            MaxJobCapacity += Convert.ToInt32(MembersDT.Rows[i]["TotalDsgCapacity"]);
                            ObservationCapacity += Convert.ToInt32(MembersDT.Rows[i]["TotalObsCapacity"]);
                        }
                    }

                    MaxJobCapacity += Convert.ToInt32(ConditionalCapacity);
                    ObservationCapacity += Convert.ToInt32(ConditionalCapacity);

                    if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
                        MaxJobCount = MaxJobCount / 2;

                    DataRow dr = DsgObsCapacityDT.NewRow();

                    dr["MaxJobCount"] = MaxJobCount;
                    dr["TotalCapacity"] = MaxJobCapacity;
                    dr["ObservationCapacity"] = ObservationCapacity;
                    dr["ConditionalCapacity"] = ConditionalCapacity;
                    dr["StartDate"] = DateArr[0].ToString();
                    dr["EndDate"] = DateArr[1].ToString();
                    dr["FId"] = FileId;
                    dr["FNO"] = MFNo;
                    dr["ConfirmDate"] = ((DataRow)DocOfficeFileArr[k])["AnswerDate"];
                    dr["ExpireDate"] = ((DataRow)DocOfficeFileArr[k])["ExpireDate"];

                    DsgObsCapacityDT.Rows.Add(dr);
                }
            }
            return DsgObsCapacityDT;
        }

        /// <summary>
        /// افراد یک دفتر یا شرکت و ظرفیت طراحی و نظارت آنها را در تاریخ خاص بر اساس اختصاص ظرفیت و یک پروانه خاص بر می گرداند
        /// MembersDT -----> MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity,Grade, MjId, GradeInOfficeLicense, DesignInc, SameGradeInc,MajorInc, TotalDsgCapacity, TotalObsCapacity, MeId, MeName, ConditionalCapacity, StartDate, EndDate, Factor
        /// </summary>
        private DataTable GetOfficeMembersPerStage(int OfficeId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, int FileId, string StartDate, string EndDate)
        {
            // MembersDT -----> MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity,Grade, MjId, GradeInOfficeLicense, DesignInc, 
            //                  SameGradeInc,MajorInc, TotalDsgCapacity, TotalObsCapacity, MeId, MeName, ConditionalCapacity

            TSP.DataManager.DocOffMajorNum DocOffMajorNum = new TSP.DataManager.DocOffMajorNum();
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocOffIncreaseJobCapacityManager IncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();

            DataTable MembersDT = GetMemberDsgObsCapacityDT();

            OfficeMemberManager = GetOfficeMembersByOfReId(OfficeId, DocOffIncreaseJobCapacityType, FileId);

            for (int i = 0; i < OfficeMemberManager.Count; i++)
            {
                if (Convert.ToInt32(OfficeMemberManager[i]["OfmType"]) == (int)TSP.DataManager.OfficeMemberType.Member)
                {
                    DataTable Member = GetMemberDsgObsCapacityPerStageByMFId(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[i]["MfId"]), StartDate, EndDate);
                    if (Member.Rows.Count != 0)
                    {
                        Member.Rows[0]["GradeInOfficeLicense"] = GetGradeByMFId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId).ToString();
                        DataRow drow = MembersDT.NewRow();

                        drow["MaxJobCount"] = Member.Rows[0]["MaxJobCount"];
                        drow["TotalCapacity"] = Member.Rows[0]["TotalCapacity"];
                        drow["ObservationPercent"] = Member.Rows[0]["ObservationPercent"];
                        drow["ObservationCapacity"] = Member.Rows[0]["ObservationCapacity"];
                        drow["Grade"] = Member.Rows[0]["Grade"];
                        drow["MjId"] = Member.Rows[0]["MjId"];
                        drow["GradeInOfficeLicense"] = Member.Rows[0]["GradeInOfficeLicense"];
                        drow["MeId"] = Member.Rows[0]["MeId"];
                        drow["MeName"] = Member.Rows[0]["MeName"];
                        drow["ConditionalCapacity"] = Member.Rows[0]["ConditionalCapacity"];
                        drow["StartDate"] = Member.Rows[0]["StartDate"];
                        drow["EndDate"] = Member.Rows[0]["EndDate"];

                        MembersDT.Rows.Add(drow);
                    }
                }
            }

            if (MembersDT.Rows.Count != 0)
            {
                ArrayList MajorArr = GetMajorNumFromDT(MembersDT);

                DocOffMajorNum.FindByMajorsNum((int)MajorArr[0], (int)MajorArr[1], (int)MajorArr[2]);
                IncreaseJobCapacityManager.FindByMNumIdAndDate(Convert.ToInt32(DocOffMajorNum[0]["MNumId"]), DocOffIncreaseJobCapacityType, StartDate);

                for (int i = 0; i < MembersDT.Rows.Count; i++)
                {
                    bool SameGradeInc = false;
                    bool MajorInc = false;

                    MembersDT.Rows[i]["DesignInc"] = (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["DesignIncPer"]) / 100).ToString();
                    for (int j = 0; j < MembersDT.Rows.Count; j++)
                    {
                        if (i != j)
                        {
                            if (MembersDT.Rows[i]["MjId"].ToString() == MembersDT.Rows[j]["MjId"].ToString())
                            {
                                if (MembersDT.Rows[i]["GradeInOfficeLicense"].ToString() == MembersDT.Rows[j]["GradeInOfficeLicense"].ToString())
                                {
                                    if (!MajorInc)
                                        SameGradeInc = true;
                                }
                                else
                                    SameGradeInc = false;

                                MajorInc = true;
                            }

                        }
                    }
                    if (SameGradeInc)
                        MembersDT.Rows[i]["SameGradeInc"] = (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["SameGradeIncPer"]) / 100).ToString();
                    else
                        MembersDT.Rows[i]["SameGradeInc"] = 0;

                    if (MajorInc)
                        MembersDT.Rows[i]["MajorInc"] = (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["MajorIncPer"]) / 100).ToString();
                    else
                        MembersDT.Rows[i]["MajorInc"] = 0;

                    MembersDT.Rows[i]["TotalDsgCapacity"] = Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) + Convert.ToInt32(MembersDT.Rows[i]["DesignInc"]) + Convert.ToInt32(MembersDT.Rows[i]["SameGradeInc"]) + Convert.ToInt32(MembersDT.Rows[i]["MajorInc"]);
                    MembersDT.Rows[i]["TotalObsCapacity"] = Convert.ToInt32(Convert.ToDouble(MembersDT.Rows[i]["ObservationPercent"]) * (Convert.ToInt32(MembersDT.Rows[i]["TotalCapacity"]) + Convert.ToInt32(MembersDT.Rows[i]["DesignInc"]) + Convert.ToInt32(MembersDT.Rows[i]["SameGradeInc"]) + Convert.ToInt32(MembersDT.Rows[i]["MajorInc"])));
                    MembersDT.Rows[i]["Factor"] = Convert.ToDouble(MembersDT.Rows[i]["TotalDsgCapacity"]) / Convert.ToDouble(MembersDT.Rows[i]["TotalCapacity"]);
                }
            }
            return MembersDT;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک عضو را در تاریخ خاص بر اساس اختصاص ظرفیت و پروانه ها بر می گرداند
        /// MaxFloor, TotalCapacity, MaxUnitCount, Grade, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetMemberImpCapacityPerStage(int MeId, string StartDate, string EndDate)
        {
            ArrayList MemberCapArr = new ArrayList();
            ArrayList DocMemberFileArr = GetDocMemberFile(MeId, StartDate, EndDate);
            DataTable ImpCapacityDT = GetMemberImpCapacityDT();

            for (int i = 0; i < DocMemberFileArr.Count; i++)
            {
                ArrayList MemberArr = new ArrayList();
                int MFId = Convert.ToInt32(((DataRow)DocMemberFileArr[i])["MfId"]);

                int Grade = GetGradeByMFId(MFId, MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                if (Grade != 0)
                {
                    ArrayList DateArr = GetSDateAndEDateFormDocFile(DocMemberFileArr, StartDate, EndDate, i);
                    int ConditionalCapacity = 0;

                    if (DateArr.Count > 0)
                    {
                        ConditionalCapacity = GetConditionalCapacity(MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer, DateArr[0].ToString(), DateArr[1].ToString());

                        TSP.DataManager.DocOffEngOfficeImpQualificationManager EngOfficeImpQualificationManager = new TSP.DataManager.DocOffEngOfficeImpQualificationManager();
                        EngOfficeImpQualificationManager.FindByGrdIdAndDate(Grade, DateArr[0].ToString());

                        if (EngOfficeImpQualificationManager.Count > 0)
                        {
                            //CapacityAssArr ----> ArrayList[0] = MaxJobCount, ArrayList[1] = MaxJobCapacity
                            ArrayList CapacityAssArr = CalculateMaxJobCount(Convert.ToInt32(EngOfficeImpQualificationManager[0]["MaxUnitCount"]), Convert.ToInt32(EngOfficeImpQualificationManager[0]["MaxJobCapacity"]), DateArr[0].ToString());

                            DataRow dr = ImpCapacityDT.NewRow();

                            dr["MaxFloor"] = EngOfficeImpQualificationManager[0]["MaxFloor"].ToString();
                            dr["TotalCapacity"] = (Convert.ToInt32(CapacityAssArr[1]) + ConditionalCapacity).ToString();
                            dr["MaxUnitCount"] = CapacityAssArr[0].ToString();
                            dr["Grade"] = Grade.ToString();
                            dr["ConditionalCapacity"] = ConditionalCapacity;
                            dr["StartDate"] = DateArr[0].ToString();
                            dr["EndDate"] = DateArr[1].ToString();
                            dr["FId"] = ((DataRow)DocMemberFileArr[i])["MfId"];
                            dr["FNO"] = ((DataRow)DocMemberFileArr[i])["MFNO"];
                            dr["ConfirmDate"] = ((DataRow)DocMemberFileArr[i])["Date"];
                            dr["ExpireDate"] = ((DataRow)DocMemberFileArr[i])["ExpireDate"];

                            ImpCapacityDT.Rows.Add(dr);
                        }
                    }
                }
            }
            return ImpCapacityDT;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک شرکت را در تاریخ خاص بر اساس اختصاص ظرفیت و پروانه ها بر می گرداند
        /// MaxFloor, TotalCapacity, MaxUnitCount, ConditionalCapacity, Grade, GrdType, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetOfficeImpCapacityPerStage(int OfficeId, string StartDate, string EndDate)
        {
            /// GradeArr-----> ArrayList[0]: GradeId, ArrayList[1]: Type, ArrayList[2]: CivilGrdId, ArrayList[3]: CivilMeId, ArrayList[4]: SecondMeId

            DataTable ImpCapacityDT = GetOfficeImpCapacityDT();
            ArrayList DocMemberFileArr = GetDocOfficeFile(OfficeId, StartDate, EndDate);
            TSP.DataManager.DocOffOfficeMembersQualificationManager OfficeMembersQualificationManager = new TSP.DataManager.DocOffOfficeMembersQualificationManager();

            for (int i = 0; i < DocMemberFileArr.Count; i++)
            {
                int OfReId = Convert.ToInt32(((DataRow)DocMemberFileArr[i])["OfReId"]);

                ArrayList GradeArr = GetOfficeImpGradeByOfReId(OfficeId, OfReId);
                ArrayList DateArr = GetSDateAndEDateFormDocFile(DocMemberFileArr, StartDate, EndDate, i);

                if (GradeArr.Count != 0 && DateArr.Count != 0)
                {
                    int ConditionalCapacity = GetConditionalCapacity(OfficeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer, DateArr[0].ToString(), DateArr[1].ToString());

                    if ((int)GradeArr[1] == (int)TSP.DataManager.DocOffOfficeMembersQualificationType.Kardan_Kardan)
                        OfficeMembersQualificationManager.FindByGrdIdAndDate((int)GradeArr[0], (int)GradeArr[1], (int)GradeArr[2], DateArr[0].ToString());
                    else
                        OfficeMembersQualificationManager.FindByGrdIdAndDate((int)GradeArr[0], (int)GradeArr[1], null, DateArr[0].ToString());

                    if (OfficeMembersQualificationManager.Count > 0)
                    {
                        //CapacityAssArr ----> ArrayList[0] = MaxJobCount, ArrayList[1] = MaxJobCapacity
                        ArrayList CapacityAssArr = CalculateMaxJobCount(Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxJobCount"]), Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxCapacity"]) + GetCapacityOfPointsByOfReId(OfficeId, Convert.ToInt32(GradeArr[3]), Convert.ToInt32(GradeArr[4]), Convert.ToInt32(OfficeMembersQualificationManager[0]["PointsLimitation"]), OfReId, DateArr[0].ToString()), DateArr[0].ToString());

                        DataRow dr = ImpCapacityDT.NewRow();

                        dr["MaxFloor"] = Convert.ToInt32(OfficeMembersQualificationManager[0]["MaxFloor"]);
                        dr["TotalCapacity"] = Convert.ToInt32(CapacityAssArr[1]) + ConditionalCapacity;
                        dr["MaxUnitCount"] = Convert.ToInt32(CapacityAssArr[0]);
                        dr["ConditionalCapacity"] = ConditionalCapacity;
                        dr["Grade"] = GradeArr[0];
                        dr["GrdType"] = GradeArr[1];
                        dr["StartDate"] = DateArr[0].ToString();
                        dr["EndDate"] = DateArr[1].ToString();
                        dr["FId"] = OfReId;
                        dr["FNO"] = ((DataRow)DocMemberFileArr[i])["MFNo"];
                        dr["ConfirmDate"] = ((DataRow)DocMemberFileArr[i])["AnswerDate"];
                        dr["ExpireDate"] = ((DataRow)DocMemberFileArr[i])["ExpireDate"];

                        ImpCapacityDT.Rows.Add(dr);
                    }
                }
            }
            return ImpCapacityDT;
        }

        /// <summary>
        /// اطلاعات ظرفیت فرد، شرکت یا یک دفتر را در تاریخ خاص بر اساس اختصاص ظرفیت بر می گرداند
        /// TotalCapacity, TotalUsed , RemainCapacity, ProjectCount, MaxJoubCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetCapacityInfoPerStage(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, string StartDate, string EndDate)
        {
            DataTable CapacityInfoDT = GetCapacityInfoDT();
            DataTable TempDT = new DataTable();

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    TempDT = GetDsgObsTotalCapacityPerStage(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, StartDate, EndDate);
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    TempDT = GetDsgObsTotalCapacityPerStage(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, StartDate, EndDate);
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TempDT = GetImpTotalCapacityPerStage(MemberTypeId, MeOfficeEngOId, StartDate, EndDate);
                    break;
            }

            for (int i = 0; i < TempDT.Rows.Count; i++)
            {
                string SDate = TempDT.Rows[i]["StartDate"].ToString();
                string EDate = TempDT.Rows[i]["EndDate"].ToString();

                DataRow dr = CapacityInfoDT.NewRow();

                if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Observer)
                    dr["TotalCapacity"] = TempDT.Rows[i]["ObservationCapacity"];
                else
                    dr["TotalCapacity"] = TempDT.Rows[i]["TotalCapacity"];

                if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Implementer)
                    dr["TotalUsed"] = GetTotalUsedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId, null, null, SDate, EDate);
                else
                    dr["TotalUsed"] = GetTotalUsedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId, Convert.ToInt32(TempDT.Rows[i]["TotalCapacity"]), Convert.ToInt32(TempDT.Rows[i]["ObservationCapacity"]), SDate, EDate);

                dr["RemainCapacity"] = Convert.ToInt32(dr["TotalCapacity"]) - Convert.ToInt32(dr["TotalUsed"]);
                dr["ProjectCount"] = GetTotalProjectNum(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId, SDate, EDate);
                if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Implementer)
                {
                    dr["MaxJobCount"] = TempDT.Rows[i]["MaxUnitCount"];
                    dr["MaxFloor"] = TempDT.Rows[i]["MaxFloor"];
                    dr["ObservationPercent"] = "-----";
                }
                else
                {
                    dr["MaxJobCount"] = TempDT.Rows[i]["MaxJobCount"];
                    dr["MaxFloor"] = "-----";
                    dr["ObservationPercent"] = TempDT.Rows[i]["ObservationPercent"];
                }

                dr["Grade"] = TempDT.Rows[i]["Grade"];
                dr["ConditionalCapacity"] = TempDT.Rows[i]["ConditionalCapacity"];
                dr["StartDate"] = SDate;
                dr["EndDate"] = EDate;
                dr["FId"] = TempDT.Rows[i]["FId"];
                dr["FNO"] = TempDT.Rows[i]["FNO"];
                dr["ConfirmDate"] = TempDT.Rows[i]["ConfirmDate"];
                dr["ExpireDate"] = TempDT.Rows[i]["ExpireDate"];

                CapacityInfoDT.Rows.Add(dr);
            }

            return CapacityInfoDT;
        }

        /// <summary>
        /// اطلاعات ظرفیت اعضا یک شرکت یا یک دفتر را در تاریخ خاص بر اساس اختصاص ظرفیت و یک پروانه خاص بر می گرداند
        /// MeId, TotalCapacity, MaxJobCount, TotalUsed, RemainCapacity, ProjectCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, MjId, StartDate, EndDate
        /// </summary>
        private DataTable GetOfficeMembersCapacityInfoPerStage(int ProjectIngridientTypeId, int OfficeEngOId, int MemberTypeId, int OfReId, string StartDate, string EndDate)
        {
            // MembersDT -----> MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity,Grade, MjId, GradeInOfficeLicense, DesignInc, 
            //                  SameGradeInc,MajorInc, TotalDsgCapacity, TotalObsCapacity, MeId, MeName, ConditionalCapacity, StartDate, EndDate

            int CapacityDecrement = 0;

            DataTable MembersDT = new DataTable();
            DataTable CapacityInfoDT = GetOfficeMembersCapacityInfoDT();

            int TotalDsg = 0;
            int TotalObs = 0;
            int UsedDsg = 0;
            int UsedObs = 0;

            int DocOffIncreaseJobCapacityType = 0;
            if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Office)
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office;
            else if (MemberTypeId == (int)TSP.DataManager.TSMemberType.EngOffice)
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    MembersDT = GetOfficeMembersPerStage(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, OfReId, StartDate, EndDate);
                    for (int i = 0; i < MembersDT.Rows.Count; i++)
                    {
                        TotalDsg = Convert.ToInt32(MembersDT.Rows[0]["TotalDsgCapacity"]);
                        TotalObs = Convert.ToInt32(MembersDT.Rows[0]["TotalObsCapacity"]);
                        UsedDsg = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, Convert.ToInt32(MembersDT.Rows[0]["MeId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate);
                        if (TotalDsg != 0)
                            UsedObs = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, Convert.ToInt32(MembersDT.Rows[0]["MeId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate) * TotalDsg / TotalObs;
                        CapacityDecrement = UsedDsg + UsedObs;
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(MembersDT.Rows[0]["MeId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate);

                        DataRow dr = CapacityInfoDT.NewRow();

                        dr["MeId"] = MembersDT.Rows[i]["MeId"];
                        dr["TotalCapacity"] = TotalDsg;
                        dr["MaxJobCount"] = MembersDT.Rows[i]["MaxJobCount"];
                        dr["TotalUsed"] = CapacityDecrement;
                        dr["RemainCapacity"] = TotalDsg - CapacityDecrement;
                        dr["ProjectCount"] = ProjectNum;
                        dr["MaxFloor"] = "-----";
                        dr["ConditionalCapacity"] = MembersDT.Rows[i]["ConditionalCapacity"];
                        dr["ObservationPercent"] = MembersDT.Rows[i]["ObservationPercent"];
                        dr["Grade"] = MembersDT.Rows[i]["Grade"];
                        dr["MjId"] = MembersDT.Rows[i]["MjId"];
                        dr["StartDate"] = MembersDT.Rows[i]["StartDate"];
                        dr["EndDate"] = MembersDT.Rows[i]["EndDate"];

                        CapacityInfoDT.Rows.Add(dr);
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    MembersDT = GetOfficeMembersPerStage(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, OfReId, StartDate, EndDate);
                    for (int i = 0; i < MembersDT.Rows.Count; i++)
                    {
                        TotalDsg = Convert.ToInt32(MembersDT.Rows[0]["TotalDsgCapacity"]);
                        TotalObs = Convert.ToInt32(MembersDT.Rows[0]["TotalObsCapacity"]);
                        if (TotalObs != 0)
                            UsedDsg = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, Convert.ToInt32(MembersDT.Rows[0]["MeId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate) * TotalObs / TotalDsg;
                        UsedObs = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, Convert.ToInt32(MembersDT.Rows[0]["MeId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate);
                        CapacityDecrement = UsedDsg + UsedObs;
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(MembersDT.Rows[0]["MeId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate);

                        DataRow dr = CapacityInfoDT.NewRow();

                        dr["MeId"] = MembersDT.Rows[i]["MeId"];
                        dr["TotalCapacity"] = TotalObs;
                        dr["MaxJobCount"] = MembersDT.Rows[i]["MaxJobCount"];
                        dr["TotalUsed"] = CapacityDecrement;
                        dr["RemainCapacity"] = TotalObs - CapacityDecrement;
                        dr["ProjectCount"] = ProjectNum;
                        dr["MaxFloor"] = "-----";
                        dr["ConditionalCapacity"] = MembersDT.Rows[i]["ConditionalCapacity"];
                        dr["ObservationPercent"] = MembersDT.Rows[i]["ObservationPercent"];
                        dr["Grade"] = MembersDT.Rows[i]["Grade"];
                        dr["MjId"] = MembersDT.Rows[i]["MjId"];
                        dr["StartDate"] = MembersDT.Rows[i]["StartDate"];
                        dr["EndDate"] = MembersDT.Rows[i]["EndDate"];

                        CapacityInfoDT.Rows.Add(dr);
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
                    OfficeMemberManager = GetOfficeMembersByOfReId(OfficeEngOId, DocOffIncreaseJobCapacityType, OfReId);

                    for (int i = 0; i < OfficeMemberManager.Count; i++)
                    {
                        CapacityDecrement = OfficeMembersUsedCapacity(ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate);
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), (int)TSP.DataManager.TSMemberType.Member, StartDate, EndDate);
                        int ConditionalCapacity = GetConditionalCapacity(Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), ProjectIngridientTypeId);

                        DataRow dr = CapacityInfoDT.NewRow();

                        dr["MeId"] = OfficeMemberManager[0]["PersonId"];
                        dr["TotalCapacity"] = "-----";
                        dr["MaxJobCount"] = "-----";
                        dr["TotalUsed"] = CapacityDecrement;
                        dr["RemainCapacity"] = "-----"; ;
                        dr["ProjectCount"] = ProjectNum;
                        dr["MaxFloor"] = "-----";
                        dr["ConditionalCapacity"] = ConditionalCapacity;
                        dr["ObservationPercent"] = "-----";
                        dr["Grade"] = "-----";
                        dr["MjId"] = "-----";
                        dr["StartDate"] = StartDate;
                        dr["EndDate"] = EndDate;

                        CapacityInfoDT.Rows.Add(dr);
                    }
                    break;
            }
            return CapacityInfoDT;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک عضو را در تاریخ خاص بر اساس اختصاص ظرفیت بر می گرداند
        /// MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity, Grade, MjId, MeId, MeName, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetMembersDsgObsCapacityPerStage(int MeId, int ProjectIngridientTypeId, string StartDate, string EndDate)
        {
            return GetMemberDsgObsCapacityPerStage(MeId, ProjectIngridientTypeId, StartDate, EndDate);
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت در تاریخ خاص را بر اساس اختصاص ظرفیت بر می گرداند
        /// MaxJobCount, TotalCapacity, ObservationCapacity, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetOfficeDsgCapacityPerStage(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, string StartDate, string EndDate)
        {
            return GetOfficeDsgObsCapacityPerStage(OfficeEngoId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, StartDate, EndDate);
        }

        /// <summary>
        /// افراد یک دفتر یا شرکت و ظرفیت طراحی و نظارت آنها را در تاریخ خاص بر اساس اختصاص ظرفیت و یک پروانه خاص بر می گرداند
        /// MembersDT -----> MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity,Grade, MjId, GradeInOfficeLicense, DesignInc, SameGradeInc,MajorInc, TotalDsgCapacity, TotalObsCapacity, MeId, MeName, ConditionalCapacity, StartDate, EndDate, Factor
        /// </summary>
        public DataTable GetOfficeMembersDsgObsCapacityPerStage(int OfficeId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, int FileId, string StartDate, string EndDate)
        {
            return GetOfficeMembersPerStage(OfficeId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, FileId, StartDate, EndDate);
        }

        /// <summary>
        /// کل ظرفیت و تعداد کار مجاز فرد، شرکت یا یک دفتر طراحی و نظارت را در تاریخ خاص بر اساس اختصاص ظرفیت بر می گرداند
        /// MaxJobCount(int), TotalCapacity(int), ObservationCapacity(int), ConditionalCapacity(int), StartDate(string), EndDate(string), ObservationPercent, Grade, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetDsgObsTotalCapacityPerStage(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, string StartDate, string EndDate)
        {
            DataTable CapacityDT = GetOfficeDsgObsCapacityDT();
            DataTable CapDT = new DataTable();

            CapacityDT.Columns.Add("ObservationPercent");
            CapacityDT.Columns.Add("Grade");

            switch (MemberTypeId)
            {
                case (int)TSP.DataManager.TSMemberType.Member:
                    CapDT = GetMemberDsgObsCapacityPerStage(MeOfficeEngOId, ProjectIngridientTypeId, StartDate, EndDate);
                    break;

                case (int)TSP.DataManager.TSMemberType.Office:
                    CapDT = GetOfficeDsgObsCapacityPerStage(MeOfficeEngOId, ProjectIngridientTypeId, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office, StartDate, EndDate);
                    break;

                case (int)TSP.DataManager.TSMemberType.EngOffice:
                    CapDT = GetOfficeDsgObsCapacityPerStage(MeOfficeEngOId, ProjectIngridientTypeId, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice, StartDate, EndDate);
                    break;
            }

            for (int i = 0; i < CapDT.Rows.Count; i++)
            {
                DataRow dr = CapacityDT.NewRow();

                dr["MaxJobCount"] = CapDT.Rows[i]["MaxJobCount"];
                dr["TotalCapacity"] = CapDT.Rows[i]["TotalCapacity"];
                dr["ObservationCapacity"] = CapDT.Rows[i]["ObservationCapacity"];
                dr["ConditionalCapacity"] = CapDT.Rows[i]["ConditionalCapacity"];
                dr["StartDate"] = CapDT.Rows[i]["StartDate"];
                dr["EndDate"] = CapDT.Rows[i]["EndDate"];
                dr["FId"] = CapDT.Rows[i]["FId"];
                dr["FNO"] = CapDT.Rows[i]["FNO"];
                dr["ConfirmDate"] = CapDT.Rows[i]["ConfirmDate"];
                dr["ExpireDate"] = CapDT.Rows[i]["ExpireDate"];

                if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
                {
                    dr["ObservationPercent"] = CapDT.Rows[i]["ObservationPercent"];
                    dr["Grade"] = CapDT.Rows[i]["Grade"];
                }
                else
                {
                    dr["ObservationPercent"] = "-----";
                    dr["Grade"] = "-----";
                }

                CapacityDT.Rows.Add(dr);
            }

            return CapacityDT;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک عضو را در تاریخ خاص را بر اساس اختصاص ظرفیت بر می گرداند
        /// MaxFloor, TotalCapacity, MaxUnitCount, Grade, ConditionalCapacity, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetMembersImpCapacityPerStage(int MeId, string StartDate, string EndDate)
        {
            DataTable Temp = GetMemberImpCapacityPerStage(MeId, StartDate, EndDate);

            for (int i = 0; i < Temp.Rows.Count; i++)
            {
                if (Convert.ToInt32(Temp.Rows[i]["MaxFloor"]) == -1)
                    Temp.Rows[i]["MaxFloor"] = "بدون محدودیت";
            }
            return Temp;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک شرکت را در تاریخ خاص را بر اساس اختصاص ظرفیت بر می گرداند
        /// MaxFloor, TotalCapacity, MaxUnitCount, ConditionalCapacity, Grade, GrdType, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetOfficesImpCapacityPerStage(int OfficeId, string StartDate, string EndDate)
        {
            DataTable Temp = GetOfficeImpCapacityPerStage(OfficeId, StartDate, EndDate);

            for (int i = 0; i < Temp.Rows.Count; i++)
            {
                if (Convert.ToInt32(Temp.Rows[i]["MaxFloor"]) == -1)
                    Temp.Rows[i]["MaxFloor"] = "بدون محدودیت";

                if (Convert.ToInt32(Temp.Rows[i]["MaxUnitCount"]) == -1)
                    Temp.Rows[i]["MaxUnitCount"] = "بدون محدودیت";
            }

            return Temp;
        }

        /// <summary>
        /// کل ظرفیت و تعداد کار و تعداد طبقات مجاز فرد، شرکت یا یک دفتر اجرایی را در تاریخ خاص بر اساس اختصاص ظرفیت بر می گرداند
        /// MaxFloor(int), TotalCapacity(int), MaxUnitCount(int), ConditionalCapacity(int), StartDate(string), EndDate(string), FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetImpTotalCapacityPerStage(int MemberTypeId, int MeOfficeEngOId, string StartDate, string EndDate)
        {
            DataTable ImpCapacityDT = GetMemberImpCapacityDT();
            DataTable CapDT = new DataTable();

            ArrayList CapacityArr = new ArrayList();
            ArrayList CapArr = new ArrayList();

            switch (MemberTypeId)
            {
                case (int)TSP.DataManager.TSMemberType.Member:
                    CapDT = GetMemberImpCapacityPerStage(MeOfficeEngOId, StartDate, EndDate);
                    break;

                case (int)TSP.DataManager.TSMemberType.Office:
                    CapDT = GetOfficeImpCapacityPerStage(MeOfficeEngOId, StartDate, EndDate);
                    break;
            }

            for (int i = 0; i < CapDT.Rows.Count; i++)
            {
                DataRow dr = ImpCapacityDT.NewRow();

                dr["MaxFloor"] = CapDT.Rows[i]["MaxFloor"];
                dr["TotalCapacity"] = CapDT.Rows[i]["TotalCapacity"];
                dr["MaxUnitCount"] = CapDT.Rows[i]["MaxUnitCount"];
                dr["Grade"] = CapDT.Rows[i]["Grade"];
                dr["ConditionalCapacity"] = CapDT.Rows[i]["ConditionalCapacity"];
                dr["StartDate"] = CapDT.Rows[i]["StartDate"];
                dr["EndDate"] = CapDT.Rows[i]["EndDate"];
                dr["FId"] = CapDT.Rows[i]["FId"];
                dr["FNO"] = CapDT.Rows[i]["FNO"];
                dr["ConfirmDate"] = CapDT.Rows[i]["ConfirmDate"];
                dr["ExpireDate"] = CapDT.Rows[i]["ExpireDate"];

                if (Convert.ToInt32(dr["MaxFloor"]) == -1)
                    dr["MaxFloor"] = "بدون محدودیت";

                if (Convert.ToInt32(dr["MaxUnitCount"]) == -1)
                    dr["MaxUnitCount"] = "بدون محدودیت";

                ImpCapacityDT.Rows.Add(dr);
            }

            return ImpCapacityDT;
        }

        /// <summary>
        /// اطلاعات ظرفیت فرد، شرکت یا یک دفتر را در تاریخ خاص بر اساس اختصاص ظرفیت بر می گرداند
        /// TotalCapacity, TotalUsed , RemainCapacity, ProjectCount, MaxJoubCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetCapacityInformationPerStage(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, string StartDate, string EndDate)
        {
            DataTable Temp = GetCapacityInfoPerStage(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, StartDate, EndDate);

            for (int i = 0; i < Temp.Rows.Count; i++)
            {
                if (Convert.ToInt32(Temp.Rows[i]["MaxFloor"]) == -1)
                    Temp.Rows[i]["MaxFloor"] = "بدون محدودیت";

                if (Convert.ToInt32(Temp.Rows[i]["MaxJoubCount"]) == -1)
                    Temp.Rows[i]["MaxJoubCount"] = "بدون محدودیت";
            }
            return Temp;
        }

        /// <summary>
        /// اطلاعات ظرفیت اعضا یک شرکت یا یک دفتر را در تاریخ خاص بر اساس اختصاص ظرفیت و یک پروانه خاص بر می گرداند
        /// MeId, TotalCapacity, MaxJobCount, TotalUsed, RemainCapacity, ProjectCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, MjId, StartDate, EndDate
        /// </summary>
        public DataTable GetOfficeMembersCapacityInformationPerStage(int ProjectIngridientTypeId, int OfficeEngOId, int MemberTypeId, int OfReId, string StartDate, string EndDate)
        {
            DataTable Temp = GetOfficeMembersCapacityInfoPerStage(ProjectIngridientTypeId, OfficeEngOId, MemberTypeId, OfReId, StartDate, EndDate);

            for (int i = 0; i < Temp.Rows.Count; i++)
            {
                if (Convert.ToInt32(Temp.Rows[i]["MaxFloor"]) == -1)
                    Temp.Rows[i]["MaxFloor"] = "بدون محدودیت";

                if (Convert.ToInt32(Temp.Rows[i]["MaxJoubCount"]) == -1)
                    Temp.Rows[i]["MaxJoubCount"] = "بدون محدودیت";
            }
            return Temp;
        }

        #endregion

        #endregion

        #region InYear

        #region Private-Methods

        /// <summary>
        /// تاریخ انتهای سال را بر می گرداند
        /// </summary>
        private string GetEndDate(string Year)
        {
            Year = (Convert.ToInt32(Year) + 1).ToString();
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            CapacityAssignmentManager.FindByYearAndStage(Year, 1);
            if (CapacityAssignmentManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(CapacityAssignmentManager[0]["AssignmentDate"]))
                {
                    Utility.Date Date = new Utility.Date(CapacityAssignmentManager[0]["AssignmentDate"].ToString());
                    return Date.AddDays(-1);
                }
                else
                    GetEndDate((Convert.ToInt32(Year) + 1).ToString());
            }

            return Utility.GetDateOfToday();
        }

        /// <summary>
        /// تاریخ ابتدای سال را بر می گرداند
        /// </summary>
        private string GetStartDate(string Year)
        {
            string StartDate;

            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();

            if (CapacityAssignmentManager.ThisAndPerevExist(Year))
            {
                CapacityAssignmentManager.FindByYearAndStage(Year, 1);
                if (CapacityAssignmentManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(CapacityAssignmentManager[0]["AssignmentDate"]))
                        StartDate = CapacityAssignmentManager[0]["AssignmentDate"].ToString();
                    else
                        StartDate = GetStartDate((Convert.ToInt32(Year) - 1).ToString());
                }
                else
                    StartDate = GetStartDate((Convert.ToInt32(Year) - 1).ToString());
            }
            else
                StartDate = "-1";

            return StartDate;
        }

        /// <summary>
        /// اطلاعات ظرفیت سال را بر می گرداند
        /// TotalCapacity, TotalUsed , RemainCapacity, ProjectCount, MaxJoubCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetYearInfo(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, string Year)
        {
            string StartDate = GetStartDate(Year);
            string EndDate = GetEndDate(Year);

            return GetCapacityInfo(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, StartDate, EndDate);
        }

        /// <summary>
        /// آخرین مرحله سال را بر می گرداند
        /// </summary>
        private int GetLastStageInYear(string Year)
        {
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            CapacityAssignmentManager.FindLastStageInYear(Year);
            if (CapacityAssignmentManager.Count > 0)
                return Convert.ToInt32(CapacityAssignmentManager[0]["Stage"]);
            else
                return -1;
        }

        /// <summary>
        /// تاریخ انتهای یک مرحله از سال را بر می گرداند
        /// </summary>
        private string GetEndDate(string Year, int Stage)
        {
            int LastStage = GetLastStageInYear(Year);
            if (Stage == LastStage)
            {
                Year = (Convert.ToInt32(Year) + 1).ToString();
                Stage = 1;
            }
            else
                Stage += 1;

            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            CapacityAssignmentManager.FindByYearAndStage(Year, Stage);
            if (CapacityAssignmentManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(CapacityAssignmentManager[0]["AssignmentDate"]))
                {
                    Utility.Date Date = new Utility.Date(CapacityAssignmentManager[0]["AssignmentDate"].ToString());
                    return Date.AddDays(-1);
                }
                else
                    return GetEndDate(Year, Stage);
            }
            if (CapacityAssignmentManager.NextExist(Year))
                return GetEndDate((Convert.ToInt32(Year) + 1).ToString(), 1);
            else
                return Utility.GetDateOfToday();
        }

        /// <summary>
        /// تاریخ ابتدای یک مرحله از سال را بر می گرداند
        /// </summary>
        private string GetStartDate(string Year, int Stage)
        {
            string StartDate;

            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();

            CapacityAssignmentManager.FindByYearAndStage(Year, Stage);
            if (CapacityAssignmentManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(CapacityAssignmentManager[0]["AssignmentDate"]))
                    StartDate = CapacityAssignmentManager[0]["AssignmentDate"].ToString();
                else
                {
                    if (Stage > 1)
                        StartDate = GetStartDate(Year, Stage - 1);
                    else
                    {
                        Year = ((Convert.ToInt32(Year) - 1).ToString());
                        Stage = GetLastStageInYear(Year);
                        StartDate = GetStartDate(Year, Stage);
                    }
                }
            }
            else
            {
                if (Stage == -1 || Stage == 1)
                {
                    Year = (Convert.ToInt32(Year) - 1).ToString();
                    Stage = GetLastStageInYear(Year);
                }
                else
                    Stage -= 1;

                if (CapacityAssignmentManager.ThisAndPerevExist(Year))
                    StartDate = GetStartDate(Year, Stage);
                else
                    StartDate = "-1";
            }

            return StartDate;
        }

        /// <summary>
        /// مراحل سال را بر می گرداند
        /// </summary>
        private TSP.DataManager.TechnicalServices.CapacityAssignmentManager GetYearStages(string Year)
        {
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            CapacityAssignmentManager.FindByYear(Year);
            return CapacityAssignmentManager;
        }

        /// <summary>
        /// اطلاعات یک مرحله از سال را بر می گرداند
        /// TotalCapacity, TotalUsed , RemainCapacity, ProjectCount, MaxJoubCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        private DataTable GetStageInfo(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, string Year, int Stage)
        {
            string StartDate = GetStartDate(Year, Stage);
            string EndDate = GetEndDate(Year, Stage);

            return GetCapacityInfoPerStage(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, StartDate, EndDate);
        }

        /// <summary>
        /// اطلاعات مراحل سال را بر می گرداند
        /// CapacityAssignmentId, Year, StageText, Stage, CapacityPrcnt, JobCountPrcnt, TotalCapacity, MaxJobCount, TotalUsed, RemainCapacity, ProjectCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade
        /// </summary>
        private DataTable GetYearStagesInfo(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, string Year)
        {
            DataTable YearStagesDT = GetYearStagesDT();

            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            CapacityAssignmentManager.FindByYear(Year);

            for (int i = 0; i < CapacityAssignmentManager.Count; i++)
            {
                string StartDate, EndDate;

                StartDate = GetStartDate(Year, Convert.ToInt32(CapacityAssignmentManager[i]["Stage"]));
                EndDate = GetEndDate(Year, Convert.ToInt32(CapacityAssignmentManager[i]["Stage"]));

                //Temp ----> TotalCapacity, TotalUsed , RemainCapacity, ProjectCount, MaxJoubCount, MaxFloor, ConditionalCapacity, 
                //           ObservationPercent, Grade, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate

                DataTable Temp = GetCapacityInfoPerStage(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, StartDate, EndDate);

                for (int j = 0; j < Temp.Rows.Count; j++)
                {
                    DataRow dr = YearStagesDT.NewRow();
                    dr["CapacityAssignmentId"] = CapacityAssignmentManager[i]["CapacityAssignmentId"];
                    dr["Year"] = CapacityAssignmentManager[i]["Year"];
                    dr["StageText"] = CapacityAssignmentManager[i]["StageText"];
                    dr["Stage"] = CapacityAssignmentManager[i]["Stage"];
                    dr["CapacityPrcnt"] = CapacityAssignmentManager[i]["CapacityPrcnt"];
                    dr["JobCountPrcnt"] = CapacityAssignmentManager[i]["JobCountPrcnt"];

                    dr["TotalCapacity"] = Temp.Rows[j]["TotalCapacity"];
                    dr["MaxJobCount"] = Temp.Rows[j]["MaxJoubCount"];
                    dr["TotalUsed"] = Temp.Rows[j]["TotalUsed"];
                    dr["RemainCapacity"] = Temp.Rows[j]["RemainCapacity"];
                    dr["ProjectCount"] = Temp.Rows[j]["ProjectCount"];
                    dr["MaxFloor"] = Temp.Rows[j]["MaxFloor"];
                    if (dr["MaxFloor"].ToString() == "-1")
                        dr["MaxFloor"] = "بدون محدودیت";
                    dr["ConditionalCapacity"] = Temp.Rows[j]["ConditionalCapacity"];
                    dr["ObservationPercent"] = Temp.Rows[j]["ObservationPercent"];
                    dr["Grade"] = Temp.Rows[j]["Grade"];

                    YearStagesDT.Rows.Add(dr);
                }
            }
            return YearStagesDT;
        }

        /// <summary>
        /// CapacityAssignmentId, Year, StageText, Stage, CapacityPrcnt, JobCountPrcnt, TotalCapacity, MaxJobCount, TotalUsed, RemainCapacity, ProjectCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade
        /// </summary>
        private DataTable GetYearStagesDT()
        {
            DataTable YearStagesDT = new DataTable();

            YearStagesDT.Columns.Add("CapacityAssignmentId");
            YearStagesDT.Columns.Add("Year");
            YearStagesDT.Columns.Add("StageText");
            YearStagesDT.Columns.Add("Stage");
            YearStagesDT.Columns.Add("CapacityPrcnt");
            YearStagesDT.Columns.Add("JobCountPrcnt");
            YearStagesDT.Columns.Add("TotalCapacity");
            YearStagesDT.Columns.Add("MaxJobCount");
            YearStagesDT.Columns.Add("TotalUsed");
            YearStagesDT.Columns.Add("RemainCapacity");
            YearStagesDT.Columns.Add("ProjectCount");
            YearStagesDT.Columns.Add("MaxFloor");
            YearStagesDT.Columns.Add("ConditionalCapacity");
            YearStagesDT.Columns.Add("ObservationPercent");
            YearStagesDT.Columns.Add("Grade");

            return YearStagesDT;
        }

        /// <summary>
        /// اطلاعات مراحل سال اعضا یک شرکت یا یک دفتر را بر اساس یک پروانه خاص بر می گرداند
        /// CapacityAssignmentId, Year, StageText, Stage, CapacityPrcnt, JobCountPrcnt, TotalCapacity, MaxJobCount, TotalUsed, RemainCapacity, ProjectCount, ObservationPercent, Grade, MaxFloor, MeId, MeName, MjId, ConditionalCapacity 
        /// </summary>
        private DataTable GetOfficeMembersYearStagesInfo(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, int OfReId, string Year)
        {
            DataTable YearStagesDT = GetOfficeMembersYearStagesDT();

            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            CapacityAssignmentManager.FindByYear(Year);
            for (int i = 0; i < CapacityAssignmentManager.Count; i++)
            {
                string StartDate, EndDate;

                StartDate = GetStartDate(Year, Convert.ToInt32(CapacityAssignmentManager[i]["Stage"]));
                EndDate = GetEndDate(Year, Convert.ToInt32(CapacityAssignmentManager[i]["Stage"]));

                // Temp ----> MeId, TotalCapacity, MaxJobCount, TotalUsed, RemainCapacity, ProjectCount, MaxFloor, ConditionalCapacity, 
                //            ObservationPercent, Grade, MjId, StartDate, EndDate

                DataTable Temp = GetOfficeMembersCapacityInfoPerStage(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId, OfReId, StartDate, EndDate);

                for (int j = 0; j < Temp.Rows.Count; j++)
                {
                    DataRow dr = YearStagesDT.NewRow();

                    dr["CapacityAssignmentId"] = CapacityAssignmentManager[i]["CapacityAssignmentId"];
                    dr["Year"] = CapacityAssignmentManager[i]["Year"];
                    dr["StageText"] = CapacityAssignmentManager[i]["StageText"];
                    dr["Stage"] = CapacityAssignmentManager[i]["Stage"];
                    dr["CapacityPrcnt"] = CapacityAssignmentManager[i]["CapacityPrcnt"];
                    dr["JobCountPrcnt"] = CapacityAssignmentManager[i]["JobCountPrcnt"];

                    dr["MeId"] = Temp.Rows[j]["MeId"];
                    dr["TotalCapacity"] = Temp.Rows[j]["TotalCapacity"];
                    dr["MaxJobCount"] = Temp.Rows[j]["MaxJoubCount"];
                    dr["TotalUsed"] = Temp.Rows[j]["TotalUsed"];
                    dr["RemainCapacity"] = Temp.Rows[j]["RemainCapacity"];
                    dr["ProjectCount"] = Temp.Rows[j]["ProjectCount"];
                    dr["MaxFloor"] = Temp.Rows[j]["MaxFloor"];
                    if (dr["MaxFloor"].ToString() == "-1")
                        dr["MaxFloor"] = "بدون محدودیت";
                    dr["MeName"] = GetMeName(Convert.ToInt32(Temp.Rows[j]["MeId"]));
                    dr["ConditionalCapacity"] = Temp.Rows[j]["ConditionalCapacity"];
                    dr["ObservationPercent"] = Temp.Rows[j]["ObservationPercent"];
                    dr["Grade"] = Temp.Rows[j]["Grade"];
                    dr["MjId"] = Temp.Rows[j]["MjId"];

                    YearStagesDT.Rows.Add(dr);
                }
            }
            return YearStagesDT;
        }

        /// <summary>
        /// CapacityAssignmentId, Year, StageText, Stage, CapacityPrcnt, JobCountPrcnt, TotalCapacity, MaxJobCount, TotalUsed, RemainCapacity, ProjectCount, ObservationPercent, Grade, MaxFloor, MeId, MeName, MjId, ConditionalCapacity 
        /// </summary>
        private DataTable GetOfficeMembersYearStagesDT()
        {
            DataTable YearStagesDT = new DataTable();

            YearStagesDT.Columns.Add("CapacityAssignmentId");
            YearStagesDT.Columns.Add("Year");
            YearStagesDT.Columns.Add("StageText");
            YearStagesDT.Columns.Add("Stage");
            YearStagesDT.Columns.Add("CapacityPrcnt");
            YearStagesDT.Columns.Add("JobCountPrcnt");
            YearStagesDT.Columns.Add("TotalCapacity");
            YearStagesDT.Columns.Add("MaxJobCount");
            YearStagesDT.Columns.Add("TotalUsed");
            YearStagesDT.Columns.Add("RemainCapacity");
            YearStagesDT.Columns.Add("ProjectCount");
            YearStagesDT.Columns.Add("ObservationPercent");
            YearStagesDT.Columns.Add("Grade");
            YearStagesDT.Columns.Add("MaxFloor");
            YearStagesDT.Columns.Add("MeId");
            YearStagesDT.Columns.Add("MeName");
            YearStagesDT.Columns.Add("MjId");
            YearStagesDT.Columns.Add("ConditionalCapacity");

            return YearStagesDT;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// اطلاعات ظرفیت سال را بر می گرداند
        /// TotalCapacity, TotalUsed , RemainCapacity, ProjectCount, MaxJoubCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        public DataTable GetYearInformation(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, string Year)
        {
            return GetYearInfo(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, Year);
        }

        /// <summary>
        /// مراحل سال را بر می گرداند
        /// </summary>
        public TSP.DataManager.TechnicalServices.CapacityAssignmentManager GetYearsStages(string Year)
        {
            return GetYearStages(Year);
        }

        /// <summary>
        /// اطلاعات یک مرحله از سال را بر می گرداند
        /// TotalCapacity, TotalUsed , RemainCapacity, ProjectCount, MaxJoubCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade, StartDate, EndDate, FId, FNO, ConfirmDate, ExpireDate
        /// </summary>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable GetStageInformation(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, string Year, int Stage)
        {
            return GetStageInfo(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, Year, Stage);
        }

        /// <summary>
        /// اطلاعات مراحل یک سال را بر می گرداند
        /// CapacityAssignmentId, Year, StageText, Stage, CapacityPrcnt, JobCountPrcnt, TotalCapacity, MaxJobCount, TotalUsed, RemainCapacity, ProjectCount, MaxFloor, ConditionalCapacity, ObservationPercent, Grade
        /// </summary>
        public DataTable GetYearStagesInformation(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, string Year)
        {
            return GetYearStagesInfo(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, Year);
        }

        /// <summary>
        /// اطلاعات مراحل یک سال اعضا یک شرکت یا یک دفتر را بر می گرداند
        /// CapacityAssignmentId, Year, StageText, Stage, CapacityPrcnt, JobCountPrcnt, TotalCapacity, MaxJobCount, TotalUsed, RemainCapacity, ProjectCount, ObservationPercent, Grade, MaxFloor, MeId, MeName, MjId, ConditionalCapacity 
        /// </summary>
        public DataTable GetGetOfficeMembersYearStagesInformation(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, int OfReId, string Year)
        {
            return GetOfficeMembersYearStagesInfo(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, OfReId, Year);
        }

        /// <summary>
        /// افراد یک دفتر یا شرکت و ظرفیت طراحی و نظارت آنها را بر اساس سال و یک پروانه خاص بر می گرداند
        /// MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity,Grade, MjId, GradeInOfficeLicense, DesignInc, SameGradeInc,MajorInc, TotalDsgCapacity, TotalObsCapacity, MeId, MeName, ConditionalCapacity, StartDate, EndDate, Factor
        /// </summary>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable GetOfficeMembersDsgObsCapacityInYear(int OfficeId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, int FileId, string Year)
        {
            string StartDate, EndDate;
            StartDate = GetStartDate(Year);
            EndDate = GetEndDate(Year);

            return GetOfficeMembersByFileId(OfficeId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, FileId, StartDate, EndDate);
        }

        /// <summary>
        /// افراد یک دفتر یا شرکت و ظرفیت طراحی و نظارت آنها را بر اساس یک مرحله از سال و یک پروانه خاص بر می گرداند
        /// MaxJobCount, TotalCapacity, ObservationPercent, ObservationCapacity,Grade, MjId, GradeInOfficeLicense, DesignInc, SameGradeInc,MajorInc, TotalDsgCapacity, TotalObsCapacity, MeId, MeName, ConditionalCapacity, StartDate, EndDate, Factor
        /// </summary>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable GetOfficeMembersDsgObsCapacityInYearPerStage(int OfficeId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, int FileId, string Year, int Stage)
        {
            string StartDate, EndDate;
            StartDate = GetStartDate(Year, Stage);
            EndDate = GetEndDate(Year, Stage);

            return GetOfficeMembersPerStage(OfficeId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, FileId, StartDate, EndDate);
        }


        #endregion

        #endregion

        #region TotalCurrentByMajor

        #region Private-Methods

        /// <summary>
        /// پایه یک عضو را بر اساس رشته بر می گرداند
        /// </summary>
        private int GetGradeByMajor(int MeId, int ProjectIngridientTypeId, int MjId)
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

            ArrayList GradeArr = DocMemberFileDetailManager.FindActiveResByResponsibilityAndMajor(MeId, ResponsibilityType, MjId);
            if (GradeArr.Count != 0)
                return Convert.ToInt32(GradeArr[0]);
            else
                return 0;
        }

        /// <summary>
        /// ???????????????????????????????????
        /// پایه یک عضو را بر اساس یک پروانه خاص بر اساس رشته بر می گرداند
        /// </summary>
        private int GetGradeByMFIdByMajor(int MFId, int MeId, int ProjectIngridientTypeId, int MjId)
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

            // ??????? بر اساس رشته
            DataTable dt = DocMemberFileDetailManager.FindByResponsibility(MFId, MeId, ResponsibilityType);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["GrdId"]);
            else
                return 0;
        }

        /// <summary>
        /// ???????????????????????????????????
        /// پایه یک کاردان یا معمار تجربی را بر اساس رشته بر می گرداند
        /// </summary>
        private int GetTechnicianGradeByMajor(int OtpId, int ProjectIngridientTypeId, int MjId)
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

            // ??????? بر اساس رشته
            return MemberAcceptedGradeManager.GetGradeId(OtpId, ResponsibilityType);
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک عضو را بر اساس رشته بر می گرداند
        /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[12]: MeId, ArrayList[13]: MeName, ArrayList[14]: ConditionalCapacity
        /// </summary>
        private ArrayList GetMemberDsgObsCapacityByMajor(int MeId, int ProjectIngridientTypeId, int MjrId)
        {
            ArrayList MemberArr = new ArrayList();
            int Grade = GetGradeByMajor(MeId, ProjectIngridientTypeId, MjrId);
            if (Grade != 0)
            {
                int ConditionalCapacity = GetConditionalCapacity(MeId, ProjectIngridientTypeId);
                int MjId = 0;
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                // ?????????? باید رشته اصلی باشد یا رشته ورودی
                ArrayList MjArray = GetMajor(MeId);
                if (MjArray.Count > 0)
                    MjId = Convert.ToInt32(MjArray[0]);

                TSP.DataManager.DocOffMemberCapacityManager MemberCapacityManager = new TSP.DataManager.DocOffMemberCapacityManager();
                MemberCapacityManager.FindByGrdId(Grade);

                if (MemberCapacityManager.Count > 0)
                {
                    MemberArr.Add(MemberCapacityManager[0]["MaxJobCount"].ToString());
                    MemberArr.Add((Convert.ToInt32(MemberCapacityManager[0]["MaxJobCapacity"]) + ConditionalCapacity).ToString());
                    MemberArr.Add(MemberCapacityManager[0]["ObservationPercent"].ToString());
                    MemberArr.Add((Convert.ToInt32(Convert.ToDouble(MemberArr[1]) * Convert.ToDouble(MemberArr[2]))) + ConditionalCapacity);
                    MemberArr.Add(Grade.ToString());
                    MemberArr.Add(MjId.ToString());
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add("0");
                    MemberArr.Add(MeId.ToString());
                    MemberArr.Add(MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString());
                    MemberArr.Add(ConditionalCapacity);
                }
            }
            return MemberArr;
        }

        /// <summary>
        /// ?????????????????????????????????
        /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت را بر اساس رشته بر می گرداند
        /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationCapacity, ArrayList[3]: ConditionalCapacity
        /// </summary>
        private ArrayList GetOfficeDsgObsCapacityByMajor(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, int MjrId)
        {
            //????????????????????????????????? اگر عضو رشته اصلی را بر گرداند نیازی به این تابع نیست

            // MajorArr-----> ArrayList[0]: MainMajorNum, ArrayList[1]: SecondaryMajorNum, ArrayList[2]: TotalMajorNum

            // MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, 
            //                     ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc,
            //                     ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName,
            //                     ArrayList[14]: ConditionalCapacity

            ArrayList MembersArr = new ArrayList();
            TSP.DataManager.DocOffMajorNum DocOffMajorNum = new TSP.DataManager.DocOffMajorNum();
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocOffIncreaseJobCapacityManager IncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();
            ArrayList CapacityArr = new ArrayList();

            int ConditionalCapacity = GetConditionalCapacity(OfficeEngoId, ProjectIngridientTypeId);

            OfficeMemberManager = GetOfficeMembers(OfficeEngoId, DocOffIncreaseJobCapacityType);

            int k = 0;
            for (int i = 0; i < OfficeMemberManager.Count; i++)
            {
                if (Convert.ToInt32(OfficeMemberManager[i]["OfmType"]) == (int)TSP.DataManager.OfficeMemberType.Member)
                {
                    //???????????????????????????
                    //ArrayList Member = GetMemberDsgObsCapacityByMajor(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId,MjrId);
                    ArrayList Member = GetMemberDsgObsCapacity(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId);
                    if (Member.Count != 0)
                    {
                        MembersArr.Add(Member);
                        //???????????????????????????
                        //((ArrayList)MembersArr[i])[6] = GetGradeForOfficeMembersByMajor(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId,MjrId).ToString();
                        ((ArrayList)MembersArr[k])[6] = GetGradeByMFId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId).ToString();
                        k++;
                    }
                }
            }

            int MaxJobCount = 0;
            int MaxJobCapacity = 0;
            int ObservationCapacity = 0;

            if (MembersArr.Count != 0)
            {
                ArrayList MajorArr = GetMajorNum(MembersArr);

                DocOffMajorNum.FindByMajorsNum((int)MajorArr[0], (int)MajorArr[1], (int)MajorArr[2]);
                IncreaseJobCapacityManager.FindByMNumId(Convert.ToInt32(DocOffMajorNum[0]["MNumId"]), DocOffIncreaseJobCapacityType);

                for (int i = 0; i < MembersArr.Count; i++)
                {
                    bool SameGradeInc = false;
                    bool MajorInc = false;

                    ((ArrayList)MembersArr[i])[7] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["DesignIncPer"]) / 100).ToString();
                    for (int j = 0; j < MembersArr.Count; j++)
                    {
                        if (i != j)
                        {
                            if (((ArrayList)MembersArr[i])[5].ToString() == ((ArrayList)MembersArr[j])[5].ToString())
                            {
                                if (((ArrayList)MembersArr[i])[6].ToString() == ((ArrayList)MembersArr[j])[6].ToString())
                                {
                                    if (!MajorInc)
                                        SameGradeInc = true;
                                }
                                else
                                    SameGradeInc = false;

                                MajorInc = true;
                            }

                        }
                    }
                    if (SameGradeInc)
                        ((ArrayList)MembersArr[i])[8] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["SameGradeIncPer"]) / 100).ToString();

                    if (MajorInc)
                        ((ArrayList)MembersArr[i])[9] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["MajorIncPer"]) / 100).ToString();

                    ((ArrayList)MembersArr[i])[10] = Convert.ToInt32(((ArrayList)MembersArr[i])[1]) + Convert.ToInt32(((ArrayList)MembersArr[i])[7]) + Convert.ToInt32(((ArrayList)MembersArr[i])[8]) + Convert.ToInt32(((ArrayList)MembersArr[i])[9]);
                    ((ArrayList)MembersArr[i])[11] = Convert.ToInt32(Convert.ToDouble(((ArrayList)MembersArr[i])[2]) * (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) + Convert.ToInt32(((ArrayList)MembersArr[i])[7]) + Convert.ToInt32(((ArrayList)MembersArr[i])[8]) + Convert.ToInt32(((ArrayList)MembersArr[i])[9])));

                    MaxJobCount += Convert.ToInt32(((ArrayList)MembersArr[i])[0]);
                    MaxJobCapacity += Convert.ToInt32(((ArrayList)MembersArr[i])[10]);
                    ObservationCapacity += Convert.ToInt32(((ArrayList)MembersArr[i])[11]);
                }
            }

            MaxJobCapacity += Convert.ToInt32(ConditionalCapacity);
            ObservationCapacity += Convert.ToInt32(ConditionalCapacity);

            if (DocOffIncreaseJobCapacityType == (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office)
                MaxJobCount = MaxJobCount / 2;

            CapacityArr.Add(MaxJobCount);
            CapacityArr.Add(MaxJobCapacity);
            CapacityArr.Add(ObservationCapacity);
            CapacityArr.Add(ConditionalCapacity);

            return CapacityArr;

        }

        /// <summary>
        /// /// ?????????????????????????????????
        /// افراد یک دفتر یا شرکت و ظرفیت طراحی و نظارت آنها را بر اساس رشته بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc, ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName, ArrayList[14]: ConditionalCapacity
        /// </summary>
        private ArrayList GetOfficeMembersByMajor(int OfficeId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, int MjrId)
        {
            //????????????????????????????????? اگر عضو رشته اصلی را بر گرداند نیازی به این تابع نیست

            // MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, 
            //                     ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc,
            //                     ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName,
            //                     ArrayList[14]: ConditionalCapacity

            ArrayList MembersArr = new ArrayList();
            TSP.DataManager.DocOffMajorNum DocOffMajorNum = new TSP.DataManager.DocOffMajorNum();
            TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocOffIncreaseJobCapacityManager IncreaseJobCapacityManager = new TSP.DataManager.DocOffIncreaseJobCapacityManager();

            OfficeMemberManager = GetOfficeMembers(OfficeId, DocOffIncreaseJobCapacityType);

            int k = 0;
            for (int i = 0; i < OfficeMemberManager.Count; i++)
            {
                if (Convert.ToInt32(OfficeMemberManager[i]["OfmType"]) == (int)TSP.DataManager.OfficeMemberType.Member)
                {
                    //???????????????????????????
                    //ArrayList Member = GetMemberDsgObsCapacityByMajor(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId,MjrId);
                    ArrayList Member = GetMemberDsgObsCapacity(Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId);
                    if (Member.Count != 0)
                    {
                        MembersArr.Add(Member);
                        //???????????????????????????
                        //((ArrayList)MembersArr[i])[6] = GetGradeForOfficeMembersByMajor(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId,MjrId).ToString();
                        ((ArrayList)MembersArr[k])[6] = GetGradeByMFId(Convert.ToInt32(OfficeMemberManager[i]["MfId"]), Convert.ToInt32(OfficeMemberManager[i]["PersonId"]), ProjectIngridientTypeId).ToString();
                        k++;
                    }
                }
            }

            if (MembersArr.Count != 0)
            {
                ArrayList MajorArr = GetMajorNum(MembersArr);

                DocOffMajorNum.FindByMajorsNum((int)MajorArr[0], (int)MajorArr[1], (int)MajorArr[2]);
                IncreaseJobCapacityManager.FindByMNumId(Convert.ToInt32(DocOffMajorNum[0]["MNumId"]), DocOffIncreaseJobCapacityType);

                for (int i = 0; i < MembersArr.Count; i++)
                {
                    bool SameGradeInc = false;
                    bool MajorInc = false;

                    ((ArrayList)MembersArr[i])[7] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["DesignIncPer"]) / 100).ToString();
                    for (int j = 0; j < MembersArr.Count; j++)
                    {
                        if (i != j)
                        {
                            if (((ArrayList)MembersArr[i])[5].ToString() == ((ArrayList)MembersArr[j])[5].ToString())
                            {
                                if (((ArrayList)MembersArr[i])[6].ToString() == ((ArrayList)MembersArr[j])[6].ToString())
                                {
                                    if (!MajorInc)
                                        SameGradeInc = true;
                                }
                                else
                                    SameGradeInc = false;

                                MajorInc = true;
                            }

                        }
                    }
                    if (SameGradeInc)
                        ((ArrayList)MembersArr[i])[8] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["SameGradeIncPer"]) / 100).ToString();

                    if (MajorInc)
                        ((ArrayList)MembersArr[i])[9] = (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) * Convert.ToInt32(IncreaseJobCapacityManager[0]["MajorIncPer"]) / 100).ToString();

                    ((ArrayList)MembersArr[i])[10] = Convert.ToInt32(((ArrayList)MembersArr[i])[1]) + Convert.ToInt32(((ArrayList)MembersArr[i])[7]) + Convert.ToInt32(((ArrayList)MembersArr[i])[8]) + Convert.ToInt32(((ArrayList)MembersArr[i])[9]);
                    ((ArrayList)MembersArr[i])[11] = Convert.ToInt32(Convert.ToDouble(((ArrayList)MembersArr[i])[2]) * (Convert.ToInt32(((ArrayList)MembersArr[i])[1]) + Convert.ToInt32(((ArrayList)MembersArr[i])[7]) + Convert.ToInt32(((ArrayList)MembersArr[i])[8]) + Convert.ToInt32(((ArrayList)MembersArr[i])[9])));
                }
            }
            return MembersArr;
        }

        /// <summary>
        /// ظرفیت کل اجرا یک عضو را بر اساس رشته بر می گرداند
        /// ArrayList[0]: MaxFloor, ArrayList[1]: MaxJobCapacity, ArrayList[2]: MaxUnitCount, ArrayList[3]: Grade, ArrayList[4]: ConditionalCapacity
        /// </summary>
        private ArrayList GetMemberImpCapacityByMajor(int MeId, int MjId)
        {
            ArrayList MemberArr = new ArrayList();
            int Grade = GetGradeByMajor(MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer, MjId);
            if (Grade != 0)
            {
                int ConditionalCapacity = GetConditionalCapacity(MeId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);

                TSP.DataManager.DocOffEngOfficeImpQualificationManager EngOfficeImpQualificationManager = new TSP.DataManager.DocOffEngOfficeImpQualificationManager();
                EngOfficeImpQualificationManager.FindByGrdId(Grade);

                if (EngOfficeImpQualificationManager.Count > 0)
                {
                    MemberArr.Add(EngOfficeImpQualificationManager[0]["MaxFloor"].ToString());
                    MemberArr.Add((Convert.ToInt32(EngOfficeImpQualificationManager[0]["MaxJobCapacity"]) + ConditionalCapacity).ToString());
                    MemberArr.Add(EngOfficeImpQualificationManager[0]["MaxUnitCount"].ToString());
                    MemberArr.Add(Grade.ToString());
                    MemberArr.Add(ConditionalCapacity);
                }
            }
            return MemberArr;
        }

        /// <summary>
        /// اطلاعات ظرفیت فرد، شرکت یا یک دفتر را بر اساس رشته بر می گرداند
        /// ArrayList[0]: TotalCapacity, ArrayList[1]:UsedCapacity , ArrayList[2]: RemainCapacity, ArrayList[3]:ReservedCapacity , ArrayList[4]: ProjectNum, ArrayList[5]: MaxJoubCount, ArrayList[6]: MaxFloor, ArrayList[7]: ConditionalCapacity
        /// </summary>
        private ArrayList GetCapacityInfoByMajor(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, int MjId)
        {
            ArrayList CapacityArr = new ArrayList();
            ArrayList TempArray = new ArrayList();

            int TotalCapacity = 0;
            int UsedCapacity = 0;
            int RemainCapacity = 0;
            int ReservedCapacity = 0;
            int MaxJoubCount = 0;
            int MaxFloor = 0;
            int ConditionalCapacity = 0;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    TempArray = GetDsgObsTotalCapacityByMajor(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, MjId);
                    if (TempArray.Count != 0)
                    {
                        TotalCapacity = Convert.ToInt32(TempArray[1]);
                        MaxJoubCount = Convert.ToInt32(TempArray[0]);
                        ConditionalCapacity = Convert.ToInt32(TempArray[3]);
                    }
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    TempArray = GetDsgObsTotalCapacityByMajor(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, MjId);
                    if (TempArray.Count != 0)
                    {
                        TotalCapacity = Convert.ToInt32(TempArray[2]);
                        MaxJoubCount = Convert.ToInt32(TempArray[0]);
                        ConditionalCapacity = Convert.ToInt32(TempArray[3]);
                    }
                    break;
                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TempArray = GetImpTotalCapacityByMajor(MemberTypeId, MeOfficeEngOId, MjId);
                    if (TempArray.Count != 0)
                    {
                        TotalCapacity = Convert.ToInt32(TempArray[1]);
                        MaxJoubCount = Convert.ToInt32(TempArray[2]);
                        MaxFloor = Convert.ToInt32(TempArray[0]);
                        ConditionalCapacity = Convert.ToInt32(TempArray[3]);
                    }
                    break;
            }

            UsedCapacity = GetTotalUsedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
            RemainCapacity = TotalCapacity - UsedCapacity;
            ReservedCapacity = GetTotalReservedCapacity(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);
            int ProjectNum = GetTotalProjectNum(ProjectIngridientTypeId, MeOfficeEngOId, MemberTypeId);

            CapacityArr.Add(TotalCapacity);
            CapacityArr.Add(UsedCapacity);
            CapacityArr.Add(RemainCapacity);
            CapacityArr.Add(ReservedCapacity);
            CapacityArr.Add(ProjectNum);
            CapacityArr.Add(MaxJoubCount);
            CapacityArr.Add(MaxFloor);
            CapacityArr.Add(ConditionalCapacity);

            return CapacityArr;
        }

        /// <summary>
        /// اطلاعات ظرفیت اعضا یک شرکت یا یک دفتر را بر اساس رشته بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MeId, ArrayList[1]: MaxJobCapacity,ArrayList[2]: MaxJobCount, ArrayList[3]: UsedCapacity, ArrayList[4]: RemainCapacity, ArrayList[5]:ReservedCapacity , ArrayList[6]: ProjectNum, ArrayList[7]: MaxFloor, ArrayList[8]: ConditionalCapacity
        /// </summary>
        private ArrayList GetOfficeMembersCapacityInfoByMajor(int ProjectIngridientTypeId, int OfficeEngOId, int MemberTypeId, int MjId)
        {
            // MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, 
            //                     ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc,
            //                     ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName
            //                     ArrayList[14]: ConditionalCapacity

            int CapacityDecrement = 0;


            ArrayList MemberArray = new ArrayList();
            ArrayList UsedCapacityArray = new ArrayList();
            int TotalDsg = 0;
            int TotalObs = 0;
            int UsedDsg = 0;
            int UsedObs = 0;

            int DocOffIncreaseJobCapacityType = 0;
            if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Office)
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office;
            else if (MemberTypeId == (int)TSP.DataManager.TSMemberType.EngOffice)
                DocOffIncreaseJobCapacityType = (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    MemberArray = GetOfficeMembersByMajor(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, MjId);
                    for (int i = 0; i < MemberArray.Count; i++)
                    {
                        TotalDsg = Convert.ToInt32(MemberArray[10]);
                        TotalObs = Convert.ToInt32(MemberArray[11]);
                        UsedDsg = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member);
                        if (TotalObs != 0)
                            UsedObs = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member) * TotalDsg / TotalObs;
                        CapacityDecrement = UsedDsg + UsedObs;
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member);
                        int ReservedCapacity = GetOfficeMembersTotalReservedCapacity(ProjectIngridientTypeId, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member);

                        ArrayList TempArray = new ArrayList();
                        TempArray.Add(Convert.ToInt32(MemberArray[12]));
                        TempArray.Add(TotalDsg);
                        TempArray.Add(Convert.ToInt32(MemberArray[0]));
                        TempArray.Add(CapacityDecrement);
                        TempArray.Add(TotalDsg - CapacityDecrement);
                        TempArray.Add(ReservedCapacity);
                        TempArray.Add(ProjectNum);
                        TempArray.Add(0);
                        TempArray.Add(Convert.ToInt32(MemberArray[14]));

                        UsedCapacityArray.Add(TempArray);
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    MemberArray = GetOfficeMembersByMajor(OfficeEngOId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, MjId);
                    for (int i = 0; i < MemberArray.Count; i++)
                    {
                        TotalDsg = Convert.ToInt32(MemberArray[10]);
                        TotalObs = Convert.ToInt32(MemberArray[11]);
                        if (TotalDsg != 0)
                            UsedDsg = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member) * TotalObs / TotalDsg;
                        UsedObs = OfficeMembersUsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member);
                        CapacityDecrement = UsedDsg + UsedObs;
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member);
                        int ReservedCapacity = GetOfficeMembersTotalReservedCapacity(ProjectIngridientTypeId, Convert.ToInt32(MemberArray[12]), (int)TSP.DataManager.TSMemberType.Member);

                        ArrayList TempArray = new ArrayList();
                        TempArray.Add(Convert.ToInt32(MemberArray[12]));
                        TempArray.Add(TotalObs);
                        TempArray.Add(Convert.ToInt32(MemberArray[0]));
                        TempArray.Add(CapacityDecrement);
                        TempArray.Add(TotalObs - CapacityDecrement);
                        TempArray.Add(ReservedCapacity);
                        TempArray.Add(ProjectNum);
                        TempArray.Add(0);
                        TempArray.Add(Convert.ToInt32(MemberArray[14]));

                        UsedCapacityArray.Add(TempArray);
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
                    OfficeMemberManager = GetOfficeMembers(OfficeEngOId, DocOffIncreaseJobCapacityType);

                    for (int i = 0; i < OfficeMemberManager.Count; i++)
                    {
                        // MemberArray -----> ArrayList[0]: MaxFloor(int), ArrayList[1]: MaxJobCapacity(int), ArrayList[2]: MaxUnitCount OR MaxJobCount(int), ArrayList[3]: ConditionalCapacity

                        MemberArray = GetImpTotalCapacityByMajor((int)TSP.DataManager.TSMemberType.Member, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), MjId);
                        int TotalCapacity = Convert.ToInt32(MemberArray[1]);
                        CapacityDecrement = OfficeMembersUsedCapacity(ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), (int)TSP.DataManager.TSMemberType.Member);
                        int ProjectNum = GetOfficeMembersTotalProjectNum(ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), (int)TSP.DataManager.TSMemberType.Member);
                        int ReservedCapacity = GetOfficeMembersTotalReservedCapacity(ProjectIngridientTypeId, Convert.ToInt32(OfficeMemberManager[0]["PersonId"]), (int)TSP.DataManager.TSMemberType.Member);

                        ArrayList TempArray = new ArrayList();
                        TempArray.Add(Convert.ToInt32(OfficeMemberManager[0]["PersonId"]));
                        TempArray.Add(TotalCapacity);
                        TempArray.Add(Convert.ToInt32(MemberArray[2]));
                        TempArray.Add(CapacityDecrement);
                        TempArray.Add(TotalCapacity - CapacityDecrement);
                        TempArray.Add(ReservedCapacity);
                        TempArray.Add(ProjectNum);
                        TempArray.Add(Convert.ToInt32(MemberArray[0]));
                        TempArray.Add(Convert.ToInt32(MemberArray[3]));

                        UsedCapacityArray.Add(TempArray);
                    }
                    break;
            }
            return UsedCapacityArray;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک عضو را بر اساس رشته بر می گرداند
        /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[12]: MeId, ArrayList[13]: MeName, ArrayList[14]: ConditionalCapacity
        /// </summary>
        public ArrayList GetMembersDsgObsCapacityByMajor(int MeId, int ProjectIngridientTypeId, int MjId)
        {
            return GetMemberDsgObsCapacityByMajor(MeId, ProjectIngridientTypeId, MjId);
        }

        /// <summary>
        /// ظرفیت کل طراحی و نظارت یک دفتر یا شرکت را بر اساس رشته بر می گرداند
        /// ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationCapacity, ArrayList[3]: ConditionalCapacity
        /// </summary>
        public ArrayList GetOfficeDsgCapacityByMajor(int OfficeEngoId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, int MjId)
        {
            return GetOfficeDsgObsCapacityByMajor(OfficeEngoId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, MjId);
        }

        /// <summary>
        /// ????????????????????????????
        /// کل ظرفیت و تعداد کار مجاز فرد، شرکت یا یک دفتر طراحی و نظارت را بر اساس رشته بر می گرداند
        /// ArrayList[0]: MaxJobCount(int), ArrayList[1]: MaxJobCapacity(int), ArrayList[2]: ObservationCapacity, ArrayList[3]: ConditionalCapacity
        /// </summary>
        public ArrayList GetDsgObsTotalCapacityByMajor(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, int MjId)
        {
            // ????????? رشته ای که بر می گرداند؟
            ArrayList CapacityArr = new ArrayList();
            ArrayList CapArr = new ArrayList();

            switch (MemberTypeId)
            {
                case (int)TSP.DataManager.TSMemberType.Member:
                    CapArr = GetMemberDsgObsCapacityByMajor(MeOfficeEngOId, ProjectIngridientTypeId, MjId);
                    break;

                case (int)TSP.DataManager.TSMemberType.Office:
                    CapArr = GetOfficeDsgObsCapacityByMajor(MeOfficeEngOId, ProjectIngridientTypeId, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office, MjId);
                    break;

                case (int)TSP.DataManager.TSMemberType.EngOffice:
                    CapArr = GetOfficeDsgObsCapacityByMajor(MeOfficeEngOId, ProjectIngridientTypeId, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.EngOffice, MjId);
                    break;
            }

            if (CapArr.Count != 0)
            {
                CapacityArr.Add(Convert.ToInt32(CapArr[0]));
                CapacityArr.Add(Convert.ToInt32(CapArr[1]));
                if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
                {
                    CapacityArr.Add(Convert.ToInt32(CapArr[3]));
                    CapacityArr.Add(Convert.ToInt32(CapArr[14]));
                }
                else
                {
                    CapacityArr.Add(Convert.ToInt32(CapArr[2]));
                    CapacityArr.Add(Convert.ToInt32(CapArr[3]));
                }
            }

            return CapacityArr;
        }

        /// <summary>
        /// افراد یک دفتر یا شرکت و ظرفیت طراحی و نظارت آنها را بر اساس رشته بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MaxJobCount, ArrayList[1]: MaxJobCapacity, ArrayList[2]: ObservationPercent, ArrayList[3]: ObservationCapacity, ArrayList[4]: Grade, ArrayList[5]: MjId, ArrayList[6]: GradeInOfficeLicense, ArrayList[7]: DesignInc, ArrayList[8]: SameGradeInc, ArrayList[9]: MajorInc, ArrayList[10]: TotalDsgCapacity, ArrayList[11]: TotalObsCapacity, ArrayList[12]: MeId, ArrayList[13]: MeName, ArrayList[14]: ConditionalCapacity
        /// </summary>
        public ArrayList GetOfficeMembersDsgObsCapacityByMajor(int OfficeId, int ProjectIngridientTypeId, int DocOffIncreaseJobCapacityType, int MjId)
        {
            return GetOfficeMembersByMajor(OfficeId, ProjectIngridientTypeId, DocOffIncreaseJobCapacityType, MjId);
        }

        /// <summary>
        /// ظرفیت کل اجرا یک عضو را بر اساس رشته بر می گرداند
        /// ArrayList[0]: MaxFloor, ArrayList[1]: MaxJobCapacity, ArrayList[2]: MaxUnitCount, ArrayList[3]: Grade, ArrayList[4]: ConditionalCapacity
        /// </summary>
        public ArrayList GetMembersImpCapacityByMajor(int MeId, int MjId)
        {
            ArrayList Temp = GetMemberImpCapacityByMajor(MeId, MjId);
            if (Temp.Count > 0)
            {
                if (Convert.ToInt32(Temp[0]) == -1)
                    Temp[0] = "بدون محدودیت";
            }
            return Temp;
        }

        /// <summary>
        /// کل ظرفیت و تعداد کار و تعداد طبقات مجاز فرد، شرکت یا یک دفتر اجرایی را بر اساس رشته بر می گرداند
        /// ArrayList[0]: MaxFloor(string), ArrayList[1]: MaxJobCapacity(string), ArrayList[2]: MaxUnitCount OR MaxJobCount(int), ArrayList[3]: ConditionalCapacity
        /// </summary>
        public ArrayList GetImpTotalCapacityByMajor(int MemberTypeId, int MeOfficeEngOId, int MjId)
        {
            ArrayList CapacityArr = new ArrayList();
            ArrayList CapArr = new ArrayList();

            switch (MemberTypeId)
            {
                case (int)TSP.DataManager.TSMemberType.Member:
                    CapArr = GetMemberImpCapacityByMajor(MeOfficeEngOId, MjId);
                    break;

                case (int)TSP.DataManager.TSMemberType.Office:
                    CapArr = GetOfficeImpCapacity(MeOfficeEngOId);
                    break;
            }

            if (CapArr.Count != 0)
            {
                if (Convert.ToInt32(CapArr[0]) == -1)
                    CapArr[0] = "بدون محدودیت";

                if (Convert.ToInt32(CapArr[2]) == -1)
                    CapArr[2] = "بدون محدودیت";

                CapacityArr.Add(Convert.ToInt32(CapArr[0]));
                CapacityArr.Add(Convert.ToInt32(CapArr[1]));
                CapacityArr.Add(Convert.ToInt32(CapArr[2]));

                if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
                    CapacityArr.Add(Convert.ToInt32(CapArr[4]));
                else
                    CapacityArr.Add(Convert.ToInt32(CapArr[3]));

            }

            return CapacityArr;
        }

        /// <summary>
        /// اطلاعات ظرفیت فرد، شرکت یا یک دفتر را بر اساس رشته بر می گرداند
        /// ArrayList[0]: TotalCapacity(int), ArrayList[1]:UsedCapacity(int) , ArrayList[2]: RemainCapacity(int), ArrayList[3]:ReservedCapacity(int) , ArrayList[4]: ProjectNum(int), ArrayList[5]: MaxJoubCount(string), ArrayList[6]: MaxFloor(string), ArrayList[7]: ConditionalCapacity(int)
        /// </summary>
        public ArrayList GetCapacityInformationByMajor(int ProjectIngridientTypeId, int MemberTypeId, int MeOfficeEngOId, int MjId)
        {
            ArrayList Temp = GetCapacityInfoByMajor(ProjectIngridientTypeId, MemberTypeId, MeOfficeEngOId, MjId);
            if (Temp.Count > 0)
            {
                if (Convert.ToInt32(Temp[6]) == -1)
                    Temp[6] = "بدون محدودیت";
                if (Convert.ToInt32(Temp[5]) == -1)
                    Temp[5] = "بدون محدودیت";
            }
            return Temp;
        }

        /// <summary>
        /// اطلاعات ظرفیت اعضا یک شرکت یا یک دفتر را بر اساس رشته بر می گرداند
        /// MembersArr[i]-----> ArrayList[0]: MeId, ArrayList[1]: MaxJobCapacity,ArrayList[2]: MaxJobCount, ArrayList[3]: UsedCapacity, ArrayList[4]: RemainCapacity, ArrayList[5]:ReservedCapacity, ArrayList[6]: ProjectNum, ArrayList[7]: MaxFloor, ArrayList[8]: ConditionalCapacity(int)
        /// </summary>
        public ArrayList GetOfficeMembersCapacityInformationByMajor(int ProjectIngridientTypeId, int OfficeEngOId, int MemberTypeId, int MjId)
        {
            ArrayList Temp = new ArrayList();
            Temp = GetOfficeMembersCapacityInfoByMajor(ProjectIngridientTypeId, OfficeEngOId, MemberTypeId, MjId);
            if (Temp.Count > 0)
            {
                if (Convert.ToInt32(Temp[2]) == -1)
                    Temp[2] = "بدون محدودیت";

                if (Convert.ToInt32(Temp[7]) == -1)
                    Temp[7] = "بدون محدودیت";
            }
            return Temp;
        }

        #endregion

        #endregion

        private string GetMeName(int MeId)
        {
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count > 0)
                return MemberManager[0]["FirstName"].ToString() + " " + MemberManager[0]["LastName"].ToString();
            else
                return "";
        }

        private int GetMjId(int MeId)
        {
            int MjId = 0;
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count > 0)
                int.TryParse(MemberManager[0]["LastMjId"].ToString(), out MjId);
            return MjId;
        }

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

        #region  Public Methods Insert-Update
        /****************************************************************************************************************************
     *                                                                                                                          *
     *                                                                                                                          * 
     *                                                        Public Methods                                                    * 
     *                                                                                                                          * 
     *                                                                                                                          * 
     ****************************************************************************************************************************/

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
        public int InsertProjectCapacityDecrement(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, string CapacityDecrement, string Wage, int ProjectIngridientTypeId, int PrjImpObsDsgnId, Nullable<int> OfficeId)
        {
            int ProjectCapacityDecrementId;
            DataRow rowProjectCapacityDecrement = ProjectCapacityDecrementManager.NewRow();


            rowProjectCapacityDecrement.BeginEdit();
            rowProjectCapacityDecrement["CapacityDecrement"] = CapacityDecrement;
            rowProjectCapacityDecrement["Wage"] = Wage;
            rowProjectCapacityDecrement["ProjectIngridientTypeId"] = ProjectIngridientTypeId;
            rowProjectCapacityDecrement["PrjImpObsDsgnId"] = PrjImpObsDsgnId;
            rowProjectCapacityDecrement["IsFree"] = 0;
            rowProjectCapacityDecrement["IsDecreased"] = 0;
            if (OfficeId.HasValue)
                rowProjectCapacityDecrement["OfficeId"] = OfficeId.Value;
            rowProjectCapacityDecrement["UserId"] = Utility.GetCurrentUser_UserId();
            rowProjectCapacityDecrement["ModifiedDate"] = DateTime.Now;
            rowProjectCapacityDecrement.EndEdit();

            ProjectCapacityDecrementManager.AddRow(rowProjectCapacityDecrement);

            ProjectCapacityDecrementManager.Save();

            ProjectCapacityDecrementManager.DataTable.AcceptChanges();
            ProjectCapacityDecrementId = Convert.ToInt32(ProjectCapacityDecrementManager[0]["PrjCapacityDecrementId"]);
            return ProjectCapacityDecrementId;
        }

        /// <summary>
        /// ProjectCapacityDecrementManager: Is Added To Transaction
        /// </summary>    
        public void UpdateProjectCapacityDecrement(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, string CapacityDecrement, string Wage, int PrjCapacityDecrementId, Nullable<int> OfficeId)
        {
            ProjectCapacityDecrementManager.FindByPrjCapacityDecrementId(PrjCapacityDecrementId);
            if (ProjectCapacityDecrementManager.Count > 0)
            {
                ProjectCapacityDecrementManager[0].BeginEdit();
                ProjectCapacityDecrementManager[0]["CapacityDecrement"] = CapacityDecrement;
                ProjectCapacityDecrementManager[0]["Wage"] = Wage;
                if (OfficeId.HasValue)
                    ProjectCapacityDecrementManager[0]["OfficeId"] = OfficeId.Value;
                ProjectCapacityDecrementManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ProjectCapacityDecrementManager[0]["ModifiedDate"] = DateTime.Now;
                ProjectCapacityDecrementManager[0].EndEdit();

                ProjectCapacityDecrementManager.Save();
            }
        }

        /// <summary>
        /// ProjectCapacityDecrementManager: Is Added To Transaction And Has The Record Taht Will Be Updated
        /// </summary>    
        public void UpdateProjectCapacityDecrement(TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, string CapacityDecrement, string Wage, Nullable<int> OfficeId)
        {
            if (ProjectCapacityDecrementManager.Count > 0)
            {
                ProjectCapacityDecrementManager[0].BeginEdit();
                ProjectCapacityDecrementManager[0]["CapacityDecrement"] = CapacityDecrement;
                ProjectCapacityDecrementManager[0]["Wage"] = Wage;
                if (OfficeId.HasValue)
                    ProjectCapacityDecrementManager[0]["OfficeId"] = OfficeId.Value;
                ProjectCapacityDecrementManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ProjectCapacityDecrementManager[0]["ModifiedDate"] = DateTime.Now;
                ProjectCapacityDecrementManager[0].EndEdit();

                ProjectCapacityDecrementManager.Save();
            }
        }

        /// <summary>
        /// ProjectOfficeMembersManager: Is Added To Transaction
        /// ReturnValue: PK
        /// </summary>
        public int InsertPrjOffMembersForOfficeMembers(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, string CapacityDecrement, string Wage, int ProjectIngridientTypeId, int PrjImpObsDsgnId, int MeId)
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
            rowProjectOfficeMembers["UserId"] = Utility.GetCurrentUser_UserId();
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
        public int InsertPrjOffMembersForOfficeTechnicians(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, string CapacityDecrement, string Wage, int ProjectIngridientTypeId, int PrjImpObsDsgnId, int OthPId)
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
            rowProjectOfficeMembers["UserId"] = Utility.GetCurrentUser_UserId();
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
        public int InsertPrjOffMembersForEngOfficeMembers(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, string CapacityDecrement, string Wage, int ProjectIngridientTypeId, int PrjImpObsDsgnId, int MeId)
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
            rowProjectOfficeMembers["UserId"] = Utility.GetCurrentUser_UserId();
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
        public int InsertPrjOffMembersForEngOfficeTechnicians(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, string CapacityDecrement, string Wage, int ProjectIngridientTypeId, int PrjImpObsDsgnId, int OthPId)
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
            rowProjectOfficeMembers["UserId"] = Utility.GetCurrentUser_UserId();
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
        public void UpdateProjectOfficeMembers(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, string CapacityDecrement, string Wage, int ProjectOfficeMembersId)
        {
            ProjectOfficeMembersManager.FindByProjectOfficeMembersId(ProjectOfficeMembersId);
            if (ProjectOfficeMembersManager.Count > 0)
            {
                ProjectOfficeMembersManager[0].BeginEdit();
                ProjectOfficeMembersManager[0]["CapacityDecrement"] = CapacityDecrement;
                ProjectOfficeMembersManager[0]["Wage"] = Wage;
                ProjectOfficeMembersManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ProjectOfficeMembersManager[0]["ModifiedDate"] = DateTime.Now;
                ProjectOfficeMembersManager[0].EndEdit();

                ProjectOfficeMembersManager.Save();
            }
        }

        /// <summary>
        /// ProjectOfficeMembersManager: Is Added To Transaction And Has The Record Taht Will Be Updated
        /// </summary>    
        public void UpdateProjectOfficeMembers(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, string CapacityDecrement, string Wage)
        {
            if (ProjectOfficeMembersManager.Count > 0)
            {
                ProjectOfficeMembersManager[0].BeginEdit();
                ProjectOfficeMembersManager[0]["CapacityDecrement"] = CapacityDecrement;
                ProjectOfficeMembersManager[0]["Wage"] = Wage;
                ProjectOfficeMembersManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ProjectOfficeMembersManager[0]["ModifiedDate"] = DateTime.Now;
                ProjectOfficeMembersManager[0].EndEdit();

                ProjectOfficeMembersManager.Save();
            }
        }
        #endregion



      
    }
