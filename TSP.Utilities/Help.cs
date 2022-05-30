using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Utility
{
    /// <summary>
    /// Summary description for Help
    /// </summary>
    public class Help
    {
        public enum HelpFiles
        {
            Teachers = 1
                //*****************Wizard Member
           , WizardMember = 2, WizardMemberlicence = 3, WizardMemberJob = 4, WizardMemberActivit = 5,
            WizardMemberLanguage = 6
                //**************Member Employee side
                ,
            Members = 7
                , Periods = 8, Seminar = 9,
            Institue = 10
                , MemberCards = 11,
            MemberPortal = 12
                , Office = 13,
            MemberRequest = 14,
            //*****************Wizard Office
            WizardOffice = 15, WizardOfficeAgent = 16, WizardOfficeMember = 17, WizardOfficeJob = 18, WizardOfficeLetters = 19,
            WizardOfficeSummary = 20, WizardOfficeFinish = 21,
            //****************Documents
            MeFileDocument = 22, OfficeDocument = 23, EngOffice = 24, MemberFileImp = 25, MemberFileObs = 26,

            //*********Member Member side
            MemberLicence = 27, MemberInfoRequest=28,

            WizardMemberRegister=29,
            PortalImplementDocument = 30,
            PortalObserverDocument = 31,
            PortalDocumentMemberFile = 32,
            PortalEngOfficeDocument = 33
        }

        private String _HelpFileUrl;
        public String HelpFileUrl
        {
            get { return _HelpFileUrl; }
        }

        private String _DownloadFileUrl;
        public String DownloadFileUrl
        {
            get { return _DownloadFileUrl; }
        }

        private String _Title;
        public String Title
        {
            get { return _Title; }
        }

        public Help(HelpFiles HelpFile)
        {
            Load_Data((int)HelpFile);
        }

        private void Load_Data(int Id)
        {
            System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
            XmlDoc.Load(HttpContext.Current.Request.MapPath("~/App_Data/HelpFiles.xml"));
            System.Xml.XmlNodeList Nodes = XmlDoc.GetElementsByTagName("Node");

            int Index = FindIndexOfData(Nodes, Id.ToString());
            if (Index == -1)
            {
                return;
            }

            _Title = Nodes[Index].Attributes["Title"].Value;
            _HelpFileUrl = "HtmlFiles/" + Nodes[Index].Attributes["FileUrl"].Value;
            _DownloadFileUrl = "DocumentFiles/" + Nodes[Index].Attributes["DownloadUrl"].Value;
        }

        private int FindIndexOfData(System.Xml.XmlNodeList Nodes, String Id)
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Attributes["HelpId"].Value == Id)
                    return i;
            }
            return -1;
        }
    }
}