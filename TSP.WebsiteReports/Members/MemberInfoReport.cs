using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
namespace TSP.WebsiteReports.Members
{
    public partial class MemberInfoReport : DevExpress.XtraReports.UI.XtraReport
    {
        public MemberInfoReport(int MeId)
        {
            InitializeComponent();
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count == 1)
            {
                TRowMeId.DataBindings.Add("Text", MemberManager.DataTable, "MeId");
                TRowMeName.DataBindings.Add("Text", MemberManager.DataTable, "MeName");
                TRowFatherName.DataBindings.Add("Text", MemberManager.DataTable, "FatherName");
                TRowIdNo.DataBindings.Add("Text", MemberManager.DataTable, "IdNo");
                TRowSSN.DataBindings.Add("Text", MemberManager.DataTable, "SSN");
                TRowAgentName.DataBindings.Add("Text", MemberManager.DataTable, "AgentName");

                TRowMeDocFileNo.DataBindings.Add("Text", MemberManager.DataTable, "FileNo");
                TRowMeDocFileDate.DataBindings.Add("Text", MemberManager.DataTable, "FileDate");
                TRowMeDocTrafficGrade.DataBindings.Add("Text", MemberManager.DataTable, "TrafficGrdName");
                TRowMeDocUrbenism.DataBindings.Add("Text", MemberManager.DataTable, "UrbanismGrdName");
                TRowMeDocMappingGrade.DataBindings.Add("Text", MemberManager.DataTable, "MappingGrdName");
                TRowMeDocGas.DataBindings.Add("Text", MemberManager.DataTable, "GasGrdName");
            }

            TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
            System.Data.DataTable dtMeLicence = MemberLicenceManager.SelectByMemberId(MeId, 0);
            TRowLiName.DataBindings.Add("Text", dtMeLicence, "LiName");
            TRowMjName.DataBindings.Add("Text", dtMeLicence, "MjName");
            TRowEndDate.DataBindings.Add("Text", dtMeLicence, "EndDate");
            TRowUnName.DataBindings.Add("Text", dtMeLicence, "UnName");
            TRowInquiry.DataBindings.Add("Text", dtMeLicence, "Inquiry");
            TRowconfirm.DataBindings.Add("Text", dtMeLicence, "confirm");
            TRowInquerySaveDate.DataBindings.Add("Text", dtMeLicence, "InquerySaveDate");      
            FillOfficeInfo(MeId);
            FillEngOfficeInfo(MeId);
        }

        public int FillOfficeInfo(int MemberId)
        {
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocMemberFileDetailManager DocMeFiDetail = new TSP.DataManager.DocMemberFileDetailManager();
            OfMeManager.FindOffMemberByPersonId(MemberId, 2, (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed);
            if (OfMeManager.Count > 0)
            {
                TRowOfId.DataBindings.Add("Text", OfMeManager.DataTable, "OfId");
                //TRowOfName.DataBindings.Add("Text", OfMeManager.DataTable, "OfName");
                TRowOfName.DataBindings.Add("Text", OfMeManager.DataTable, "OfName");
                int OfId = Convert.ToInt32(OfMeManager[0]["OfId"]);
                string OfGrade = "";
                if (OfMeManager[0]["MfId"] != null)
                {
                    DataTable dtRes = DocMeFiDetail.SelectById(Convert.ToInt32(OfMeManager[0]["MfId"]), MemberId, 0);
                    DataRow[] drDes = dtRes.Select("ResId=" + ((int)TSP.DataManager.DocumentResponsibilityType.Design).ToString());
                    DataRow[] drObs = dtRes.Select("ResId=" + ((int)TSP.DataManager.DocumentResponsibilityType.Observation).ToString());
                    DataRow[] drImp = dtRes.Select("ResId=" + ((int)TSP.DataManager.DocumentResponsibilityType.Implement).ToString());
                    for (int i = 0; i < drDes.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(OfGrade))
                            OfGrade = " و ";
                        OfGrade += drDes[i]["GMRName"].ToString();
                    }

                    for (int i = 0; i < drObs.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(OfGrade))
                            OfGrade += " و ";
                        OfGrade += drObs[i]["GMRName"].ToString();
                    }
                    for (int i = 0; i < drImp.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(OfGrade))
                            OfGrade += " و ";
                        OfGrade += drImp[i]["GMRName"].ToString();
                    }
                }
                DataTable dtOfGrade = new DataTable();
                dtOfGrade.Columns.Add("OfGrade");
                DataRow dr = dtOfGrade.NewRow();
                dr["OfGrade"] = OfGrade;
                dtOfGrade.Rows.Add(dr);
                TRowOfMeGrade.DataBindings.Add("Text", dtOfGrade, "OfGrade");
                DataTable dtOfficeFileDate = FindOfficeExpireDateAndFile(OfId);
                TRowOfFileDate.DataBindings.Add("Text", dtOfficeFileDate, "FileDate");
                TRowOfFileNo.DataBindings.Add("Text", dtOfficeFileDate, "FileNo");
                OfMeManager.selectActiveEngOfficemanager(OfId);
                if (OfMeManager.Count > 0)
                {
                    TRowOfManagerName.DataBindings.Add("Text", OfMeManager.DataTable, "MeName");
                }
                TRowOfGrade.DataBindings.Add("Text", OfMeManager.DataTable, "");
                return OfId;
            }
            return -2;
        }

        private DataTable FindOfficeExpireDateAndFile(int OfId)
        {
            TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
            OfficeManager.FindByCode(OfId);
            return OfficeManager.DataTable;
        }

        public void FillEngOfficeInfo(int MeId)
        {
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            TSP.DataManager.DocMemberFileDetailManager DocMeFiDetail = new TSP.DataManager.DocMemberFileDetailManager();
            OfMeManager.FindEngOfficeMemberByPersonId(MeId, (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed, 1);

            if (OfMeManager.Count > 0)
            {
                int OfId = Convert.ToInt32(OfMeManager[0]["OfId"]);
                TRowEngOfId.DataBindings.Add("Text", OfMeManager.DataTable, "OfId");
                TRowEngOfName.DataBindings.Add("Text", OfMeManager.DataTable, "EngOffName");

                string GradeName = "";
                if (OfMeManager[0]["MfId"] != null)
                {
                    DataTable dtRes = DocMeFiDetail.SelectById(Convert.ToInt32(OfMeManager[0]["MfId"]), MeId, 0);
                    DataRow[] drDes = dtRes.Select("ResId=" + ((int)TSP.DataManager.DocumentResponsibilityType.Design).ToString());
                    DataRow[] drObs = dtRes.Select("ResId=" + ((int)TSP.DataManager.DocumentResponsibilityType.Observation).ToString());

                    for (int i = 0; i < drDes.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(GradeName))
                            GradeName += " و ";
                        GradeName += drDes[i]["GMRName"].ToString();
                    }

                    for (int i = 0; i < drObs.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(GradeName))
                            GradeName += " و ";
                        GradeName += drObs[i]["GMRName"].ToString();
                    }
                }
                DataTable dtOfGrade = new DataTable();
                dtOfGrade.Columns.Add("OfGrade");
                DataRow dr = dtOfGrade.NewRow();
                dr["OfGrade"] = GradeName;
                dtOfGrade.Rows.Add(dr);
                TRowEngOfMeGrade.DataBindings.Add("Text", dtOfGrade, "OfGrade");
                DataTable dtEngOf = FindEngOfficeExpireDateAndFileNo(OfId);
                TRowEngOfDate.DataBindings.Add("Text", dtEngOf, "ExpireDate");
                TRowEngOfFileNo.DataBindings.Add("Text", dtEngOf, "FileNo");
            
                OfMeManager.selectActiveEngOfficemanager(OfId);
                if (OfMeManager.Count > 0)
                {
                    TRowEngOfManger.DataBindings.Add("Text", OfMeManager.DataTable, "MeName");
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EngOfId"></param>
        /// <returns>ArrayInfo[0]:ExpireDate ; ArrayInfo[1]: FileNo</returns>
        private DataTable FindEngOfficeExpireDateAndFileNo(int EngOfId)
        {
            TSP.DataManager.EngOfficeManager EngOfficeManager = new TSP.DataManager.EngOfficeManager();
            EngOfficeManager.FindByCode(EngOfId);
            //if (EngOfficeManager.Count == 1)
            //{
            //    if (!Utility.IsDBNullOrNullValue(EngOfficeManager[0]["ExpireDate"]))
            //        ArrayInfo[0] = EngOfficeManager[0]["ExpireDate"].ToString();
            //    if (!Utility.IsDBNullOrNullValue(EngOfficeManager[0]["FileNo"]))
            //        ArrayInfo[1] = EngOfficeManager[0]["FileNo"].ToString();
            //    //return EngOfficeManager[0]["ExpireDate"].ToString();
            //}
            return EngOfficeManager.DataTable;
        }

    }
}
