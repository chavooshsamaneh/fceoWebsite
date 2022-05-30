using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.Document
{
    public partial class DocumentCountByMajorReport : DevExpress.XtraReports.UI.XtraReport
    {
        public DocumentCountByMajorReport()
        {
            Int64 SumWitOut = 0;
            Int64 SumMemariFemale = 0;  Int64 SumMemarimale = 0;
            Int64 SumOmranFemale = 0;  Int64 SumOmranmale = 0;
            Int64 SumMechanicFemale = 0;Int64 SumMechanicmale = 0;
            Int64 SumBarghFemale = 0; Int64 SumBarghmale = 0;
            Int64 SumShahrsaziFemale = 0;Int64 SumShahrsazimale = 0;
            Int64 SumTrafficeFemale = 0; Int64 SumTrafficemale = 0;
            Int64 SumMappingFemale = 0; Int64 SumMappingmale = 0;
            InitializeComponent();
            var WithoutDoc = TSP.DataManager.DocMemberFileManager.ReportMemberWithoutDocByMajor();
            withoutDocFemaleBargh.Text = WithoutDoc[0].BarchFemale.ToString();
            withoutDocFemaleMekanik.Text = WithoutDoc[0].MechanicFemale.ToString();
            withoutDocFemaleMemari.Text = WithoutDoc[0].MemariFemale.ToString();
            withoutDocFemaleNaghshe.Text = WithoutDoc[0].NaghshebardariFemale.ToString();
            withoutDocFemaleOmran.Text = WithoutDoc[0].OmranFemal.ToString();
            withoutDocFemaleShahrsazi.Text = WithoutDoc[0].ShahrsaziFemale.ToString();
            withoutDocFemaleTeraffice.Text = WithoutDoc[0].TerafficeFemale.ToString();
            withoutDocMaleBargh.Text = WithoutDoc[0].BarghMale.ToString();
            withoutDocMaleMekanik.Text = WithoutDoc[0].MechanicMale.ToString();
            withoutDocMaleMemari.Text = WithoutDoc[0].MemariMale.ToString();
            withoutDocMaleNaghshe.Text = WithoutDoc[0].NaghshebardariMale.ToString();
            withoutDocMaleOmran.Text = WithoutDoc[0].OmranMale.ToString();
            withoutDocMaleShahrsazi.Text = WithoutDoc[0].ShahrsaziMale.ToString();
            withoutDocMaleTraffice.Text = WithoutDoc[0].TerafficMale.ToString();
          
            SumWitOut += (int)WithoutDoc[0].BarchFemale + (int)WithoutDoc[0].MechanicFemale + (int)WithoutDoc[0].MemariFemale + (int)WithoutDoc[0].NaghshebardariFemale
                + (int)WithoutDoc[0].OmranFemal + (int)WithoutDoc[0].ShahrsaziFemale + (int)WithoutDoc[0].TerafficeFemale + (int)WithoutDoc[0].BarghMale
                + (int)WithoutDoc[0].MechanicMale + (int)WithoutDoc[0].MemariMale + (int)WithoutDoc[0].NaghshebardariMale + (int)WithoutDoc[0].OmranMale + (int)WithoutDoc[0].ShahrsaziMale + (int)WithoutDoc[0].TerafficMale;
            withoutDocSum.Text = SumWitOut.ToString();
            #region Memari
            var WithDocMemari = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Female, (int)TSP.DataManager.MainMajors.Architecture);
            MemariFemaleLinke3.Text = WithDocMemari[0].LinkedMajor3.ToString();
            MemariFemaleLinked2.Text = WithDocMemari[0].LinkedMajor2.ToString();
            MemariFemaleLinked1.Text = WithDocMemari[0].LinkedMajor1.ToString();
            MemariFemaleMain3.Text = WithDocMemari[0].MaindMajor3.ToString();
            MemariFemaleMain2.Text = WithDocMemari[0].MaindMajor2.ToString();
            MemariFemaleMain1.Text = WithDocMemari[0].MaindMajor1.ToString();
            SumMemariFemale +=(int)WithoutDoc[0].MemariFemale + (int)WithDocMemari[0].LinkedMajor3 + (int)WithDocMemari[0].LinkedMajor2 + (int)WithDocMemari[0].LinkedMajor1 + (int)WithDocMemari[0].MaindMajor3 + (int)WithDocMemari[0].MaindMajor2 + (int)WithDocMemari[0].MaindMajor1;
            MemariSumFemale.Text = SumMemariFemale.ToString();
            //---------
            var WithDocMemariMale = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Male, (int)TSP.DataManager.MainMajors.Architecture);
            MemariMaleLinked3.Text = WithDocMemariMale[0].LinkedMajor3.ToString();
            MemariMaleLinked2.Text = WithDocMemariMale[0].LinkedMajor2.ToString();
            MemariMaleLinked1.Text = WithDocMemariMale[0].LinkedMajor1.ToString();
            MemariMaleMain3.Text = WithDocMemariMale[0].MaindMajor3.ToString();
            MemariMaleMain2.Text = WithDocMemariMale[0].MaindMajor2.ToString();
            MemariMaleMain1.Text = WithDocMemariMale[0].MaindMajor1.ToString();
            SumMemarimale += (int)WithoutDoc[0].MemariMale + (int)WithDocMemariMale[0].LinkedMajor3 + (int)WithDocMemariMale[0].LinkedMajor2 + (int)WithDocMemariMale[0].LinkedMajor1 + (int)WithDocMemariMale[0].MaindMajor3 + (int)WithDocMemariMale[0].MaindMajor2 + (int)WithDocMemariMale[0].MaindMajor1;
            MemariSumMale.Text = SumMemarimale.ToString();
            #endregion

            #region Omran
            var WithDocOmran = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Female, (int)TSP.DataManager.MainMajors.Civil);
            OmranFemaleLinked3.Text = WithDocOmran[0].LinkedMajor3.ToString();
            OmranFemaleLinked2.Text = WithDocOmran[0].LinkedMajor2.ToString();
            OmranFemaleLinked1.Text = WithDocOmran[0].LinkedMajor1.ToString();
            OmranFemaleMain3.Text = WithDocOmran[0].MaindMajor3.ToString();
            OmranFemaleMain2.Text = WithDocOmran[0].MaindMajor2.ToString();
            OmranFemaleMain1.Text = WithDocOmran[0].MaindMajor1.ToString();
            SumOmranFemale += (int)WithoutDoc[0].OmranFemal + (int)WithDocOmran[0].LinkedMajor3 + (int)WithDocOmran[0].LinkedMajor2 + (int)WithDocOmran[0].LinkedMajor1 + (int)WithDocOmran[0].MaindMajor3 + (int)WithDocOmran[0].MaindMajor2 + (int)WithDocOmran[0].MaindMajor1;
            OmranSumFemale.Text = SumOmranFemale.ToString();
            //------
            var WithDocOmranMale = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Male, (int)TSP.DataManager.MainMajors.Civil);
            OmranMaleLinked3.Text = WithDocOmranMale[0].LinkedMajor3.ToString();
            OmranMaleLinked2.Text = WithDocOmranMale[0].LinkedMajor2.ToString();
            OmranMaleLinked1.Text = WithDocOmranMale[0].LinkedMajor1.ToString();
            OmranMaleMain3.Text = WithDocOmranMale[0].MaindMajor3.ToString();
            OmranMaleMain2.Text = WithDocOmranMale[0].MaindMajor2.ToString();
            OmranMaleMain1.Text = WithDocOmranMale[0].MaindMajor1.ToString();
            SumOmranmale += (int)WithoutDoc[0].OmranMale + (int)WithDocOmranMale[0].LinkedMajor3 + (int)WithDocOmranMale[0].LinkedMajor2 + (int)WithDocOmranMale[0].LinkedMajor1 + (int)WithDocOmranMale[0].MaindMajor3 + (int)WithDocOmranMale[0].MaindMajor2 + (int)WithDocOmranMale[0].MaindMajor1;
            OmranSumMale.Text = SumOmranmale.ToString();
            #endregion

            #region Mechanic
            var WithDocMechanic = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Female, (int)TSP.DataManager.MainMajors.Mechanic);
            MekanikFemaleLinked3.Text = WithDocMechanic[0].LinkedMajor3.ToString();
            MekanikFemaleLinked2.Text = WithDocMechanic[0].LinkedMajor2.ToString();
            MekanikedFemalelink1.Text = WithDocMechanic[0].LinkedMajor1.ToString();
            MekanikFemaleMain3.Text = WithDocMechanic[0].MaindMajor3.ToString();
            MekanikFemaleMain2.Text = WithDocMechanic[0].MaindMajor2.ToString();
            MekanikFemaleMain1.Text = WithDocMechanic[0].MaindMajor1.ToString();
            SumMechanicFemale += (int)WithoutDoc[0].MechanicFemale + (int)WithDocMechanic[0].LinkedMajor3 + (int)WithDocMechanic[0].LinkedMajor2 + (int)WithDocMechanic[0].LinkedMajor1 + (int)WithDocMechanic[0].MaindMajor3 + (int)WithDocMechanic[0].MaindMajor2 + (int)WithDocMechanic[0].MaindMajor1;
            MechanicSumFemale.Text = SumMechanicFemale.ToString();
            //------------------------------------------
            var WithDocMechanicMale = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Male, (int)TSP.DataManager.MainMajors.Mechanic);
            MekanikMaleLinked3.Text = WithDocMechanicMale[0].LinkedMajor3.ToString();
            MekanikMaleLinked2.Text = WithDocMechanicMale[0].LinkedMajor2.ToString();
            MekanikedMalelink1.Text = WithDocMechanicMale[0].LinkedMajor1.ToString();
            MekanikMaleMain3.Text = WithDocMechanicMale[0].MaindMajor3.ToString();
            MekanikMaleMain2.Text = WithDocMechanicMale[0].MaindMajor2.ToString();
            MekanikMaleMain1.Text = WithDocMechanicMale[0].MaindMajor1.ToString();
            SumMechanicmale += (int)WithoutDoc[0].MechanicMale + (int)WithDocMechanicMale[0].LinkedMajor3 + (int)WithDocMechanicMale[0].LinkedMajor2 + (int)WithDocMechanicMale[0].LinkedMajor1 + (int)WithDocMechanicMale[0].MaindMajor3 + (int)WithDocMechanicMale[0].MaindMajor2 + (int)WithDocMechanicMale[0].MaindMajor1;
            MechanicSumMale.Text = SumMechanicmale.ToString();
            #endregion

            #region Electronic
            var WithDocElectronic = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Female, (int)TSP.DataManager.MainMajors.Electronic);
            BarghfemaleLinked3.Text = WithDocElectronic[0].LinkedMajor3.ToString();
            BarghfemaleLinked2.Text = WithDocElectronic[0].LinkedMajor2.ToString();
            BarghfemaleLinked1.Text = WithDocElectronic[0].LinkedMajor1.ToString();
            BarghfemaleMain3.Text = WithDocElectronic[0].MaindMajor3.ToString();
            BarghfemaleMain2.Text = WithDocElectronic[0].MaindMajor2.ToString();
            BarghfemaleMain1.Text = WithDocElectronic[0].MaindMajor1.ToString();
            SumBarghFemale += (int)WithoutDoc[0].BarchFemale + (int)WithDocElectronic[0].LinkedMajor3 + (int)WithDocElectronic[0].LinkedMajor2 + (int)WithDocElectronic[0].LinkedMajor1 + (int)WithDocElectronic[0].MaindMajor3 + (int)WithDocElectronic[0].MaindMajor2 + (int)WithDocElectronic[0].MaindMajor1;
            BarghSumfemale.Text = SumBarghFemale.ToString();
            //------------------------------------------
            var WithDocElectronicMale = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Male, (int)TSP.DataManager.MainMajors.Electronic);
            BarghMaleLinked3.Text = WithDocElectronicMale[0].LinkedMajor3.ToString();
            BarghMaleLinked2.Text = WithDocElectronicMale[0].LinkedMajor2.ToString();
            BarghMaleLinked1.Text = WithDocElectronicMale[0].LinkedMajor1.ToString();
            BarghMaleMain3.Text = WithDocElectronicMale[0].MaindMajor3.ToString();
            BarghMaleMain2.Text = WithDocElectronicMale[0].MaindMajor2.ToString();
            BarghMaleMain1.Text = WithDocElectronicMale[0].MaindMajor1.ToString();
            SumBarghmale += (int)WithoutDoc[0].BarghMale + (int)WithDocElectronicMale[0].LinkedMajor3 + (int)WithDocElectronicMale[0].LinkedMajor2 + (int)WithDocElectronicMale[0].LinkedMajor1 + (int)WithDocElectronicMale[0].MaindMajor3 + (int)WithDocElectronicMale[0].MaindMajor2 + (int)WithDocElectronicMale[0].MaindMajor1;
            BarghSumMale.Text = SumBarghmale.ToString();
            #endregion

            #region Mapping
            var WithDocMapping = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Female, (int)TSP.DataManager.MainMajors.Mapping);
            Naghshefemalelinked3.Text = WithDocMapping[0].LinkedMajor3.ToString();
            Naghshefemalelinked2.Text = WithDocMapping[0].LinkedMajor2.ToString();
            Naghshefemalelinked1.Text = WithDocMapping[0].LinkedMajor1.ToString();
            NaghsheFemaleMain3.Text = WithDocMapping[0].MaindMajor3.ToString();
            NaghsheFemaleMain2.Text = WithDocMapping[0].MaindMajor2.ToString();
            NaghsheFemaleMain1.Text = WithDocMapping[0].MaindMajor1.ToString();
            SumMappingFemale += (int)WithoutDoc[0].NaghshebardariFemale + (int)WithDocMapping[0].LinkedMajor3 + (int)WithDocMapping[0].LinkedMajor2 + (int)WithDocMapping[0].LinkedMajor1 + (int)WithDocMapping[0].MaindMajor3 + (int)WithDocMapping[0].MaindMajor2 + (int)WithDocMapping[0].MaindMajor1;
            NaghsheSumFemale.Text = SumMappingFemale.ToString();
            //------------------------------------------
            var WithDocMappingMale = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Male, (int)TSP.DataManager.MainMajors.Mapping);
            Naghshemalelinked3.Text = WithDocMappingMale[0].LinkedMajor3.ToString();
            Naghshemalelinked2.Text = WithDocMappingMale[0].LinkedMajor2.ToString();
            Naghshemalelinked1.Text = WithDocMappingMale[0].LinkedMajor1.ToString();
            NaghshemaleMain3.Text = WithDocMappingMale[0].MaindMajor3.ToString();
            NaghshemaleMain2.Text = WithDocMappingMale[0].MaindMajor2.ToString();
            NaghshemaleMain1.Text = WithDocMappingMale[0].MaindMajor1.ToString();
            SumMappingmale += (int)WithoutDoc[0].NaghshebardariMale + (int)WithDocMappingMale[0].LinkedMajor3 + (int)WithDocMappingMale[0].LinkedMajor2 + (int)WithDocMappingMale[0].LinkedMajor1 + (int)WithDocMappingMale[0].MaindMajor3 + (int)WithDocMappingMale[0].MaindMajor2 + (int)WithDocMappingMale[0].MaindMajor1;
            NaghsheSumMale.Text = SumMappingmale.ToString();
            #endregion

            #region Urbanism
            var WithDocUrbanism = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Female, (int)TSP.DataManager.MainMajors.Urbanism);
            ShahrsaziFemalelinked3.Text = WithDocUrbanism[0].LinkedMajor3.ToString();
            ShahrsaziFemalelinked2.Text = WithDocUrbanism[0].LinkedMajor2.ToString();
            Shahrsazifemalelinked1.Text = WithDocUrbanism[0].LinkedMajor1.ToString();
            ShahrsaziFemaleMain3.Text = WithDocUrbanism[0].MaindMajor3.ToString();
            ShahrsaziFemaleMain2.Text = WithDocUrbanism[0].MaindMajor2.ToString();
            ShahrsaziFemaleMain1.Text = WithDocUrbanism[0].MaindMajor1.ToString();
            SumShahrsaziFemale += (int)WithoutDoc[0].ShahrsaziFemale + (int)WithDocUrbanism[0].LinkedMajor3 + (int)WithDocUrbanism[0].LinkedMajor2 + (int)WithDocUrbanism[0].LinkedMajor1 + (int)WithDocUrbanism[0].MaindMajor3 + (int)WithDocUrbanism[0].MaindMajor2 + (int)WithDocUrbanism[0].MaindMajor1;
            ShahrsaziSumFemale.Text = SumShahrsaziFemale.ToString();
            //------------------------------------------
            var WithDocUrbanismMale = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Male, (int)TSP.DataManager.MainMajors.Urbanism);
            ShahrsaziMalelinked3.Text = WithDocUrbanismMale[0].LinkedMajor3.ToString();
            Shahrsazimalelinked2.Text = WithDocUrbanismMale[0].LinkedMajor2.ToString();
            Shahrsazimalelinked1.Text = WithDocUrbanismMale[0].LinkedMajor1.ToString();
            ShahrsaziMaleMain3.Text = WithDocUrbanismMale[0].MaindMajor3.ToString();
            ShahrsazimaleMain2.Text = WithDocUrbanismMale[0].MaindMajor2.ToString();
            ShahrsazimaleMain1.Text = WithDocUrbanismMale[0].MaindMajor1.ToString();
            SumShahrsazimale += (int)WithoutDoc[0].ShahrsaziMale + (int)WithDocUrbanismMale[0].LinkedMajor3 + (int)WithDocUrbanismMale[0].LinkedMajor2 + (int)WithDocUrbanismMale[0].LinkedMajor1 + (int)WithDocUrbanismMale[0].MaindMajor3 + (int)WithDocUrbanismMale[0].MaindMajor2 + (int)WithDocUrbanismMale[0].MaindMajor1;
            ShahrsaziSummale.Text = SumShahrsazimale.ToString();
            #endregion

            #region Traffic
            var WithDocTraffic = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Female, (int)TSP.DataManager.MainMajors.Traffic);
            Trafficefemalelinked3.Text = WithDocTraffic[0].LinkedMajor3.ToString();
            Trafficefemalelinked2.Text = WithDocTraffic[0].LinkedMajor2.ToString();
            Trafficefemalelinked1.Text = WithDocTraffic[0].LinkedMajor1.ToString();
            TrafficeFemaleMain3.Text = WithDocTraffic[0].MaindMajor3.ToString();
            TrafficeFemaleMain2.Text = WithDocTraffic[0].MaindMajor2.ToString();
            TrafficeFemaleMain1.Text = WithDocTraffic[0].MaindMajor1.ToString();
            SumTrafficeFemale += (int)WithoutDoc[0].TerafficeFemale + (int)WithDocTraffic[0].LinkedMajor3 + (int)WithDocTraffic[0].LinkedMajor2 + (int)WithDocTraffic[0].LinkedMajor1 + (int)WithDocTraffic[0].MaindMajor3 + (int)WithDocTraffic[0].MaindMajor2 + (int)WithDocTraffic[0].MaindMajor1;
            TrafficeSumFemale.Text = SumTrafficeFemale.ToString();
            //------------------------------------------
            var WithDocTrafficMale = TSP.DataManager.DocMemberFileManager.ReportMemberWithDocByMajor((int)TSP.DataManager.SexManager.Sex.Male, (int)TSP.DataManager.MainMajors.Traffic);
            Trafficemalelinked3.Text = WithDocTrafficMale[0].LinkedMajor3.ToString();
            Trafficemalelinked2.Text = WithDocTrafficMale[0].LinkedMajor2.ToString();
            Trafficemalelinked1.Text = WithDocTrafficMale[0].LinkedMajor1.ToString();
            TrafficemaleMain3.Text = WithDocTrafficMale[0].MaindMajor3.ToString();
            TrafficemaleMain2.Text = WithDocTrafficMale[0].MaindMajor2.ToString();
            TrafficemaleMain1.Text = WithDocTrafficMale[0].MaindMajor1.ToString();
            SumTrafficemale += (int)WithoutDoc[0].TerafficMale + (int)WithDocTrafficMale[0].LinkedMajor3 + (int)WithDocTrafficMale[0].LinkedMajor2 + (int)WithDocTrafficMale[0].LinkedMajor1 + (int)WithDocTrafficMale[0].MaindMajor3 + (int)WithDocTrafficMale[0].MaindMajor2 + (int)WithDocTrafficMale[0].MaindMajor1;
            TrafficeSumMale.Text = SumTrafficemale.ToString();
            #endregion
        }

    }
}
