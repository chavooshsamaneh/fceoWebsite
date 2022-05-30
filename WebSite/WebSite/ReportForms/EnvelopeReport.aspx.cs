using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ReportForms_EnvelopeReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            if (!Utility.IsDBNullOrNullValue(Request.QueryString["LetId"]))
            {
                string LetterId = Utility.DecryptQS(Request.QueryString["LetId"]);
                XtraReportMainEnvelope XtraReportMainEnvelope = new XtraReportMainEnvelope(Convert.ToInt32(LetterId));
                ReportViewer1.Report = XtraReportMainEnvelope;
            }
            else if (!Utility.IsDBNullOrNullValue(Request.QueryString["FromMeSearch"]))
            {
                //string MeIdParameters = Utility.DecryptQS(Request.QueryString["MeIdParameters"]);
                if (Session["MeIdParametersForEnvPrint"] == null)
                    return;
                string MeIdParameters = Session["MeIdParametersForEnvPrint"].ToString();// Request.QueryString["MeIdParameters"];
                String strReceiverType = "";
                if (!Utility.IsDBNullOrNullValue(Request.QueryString["ReceiverType"]))
                    strReceiverType = Utility.DecryptQS(Request.QueryString["ReceiverType"]);
                int ReceiverType = -1;
                if (!Utility.IsDBNullOrNullValue(strReceiverType))
                    ReceiverType = Convert.ToInt32(strReceiverType);

                String strAddressType = "";
                if (!Utility.IsDBNullOrNullValue(Request.QueryString["AddressType"]))
                    strAddressType = Utility.DecryptQS(Request.QueryString["AddressType"]);
                int AddressType = -1;
                if (!Utility.IsDBNullOrNullValue(strAddressType))
                    AddressType = Convert.ToInt32(strAddressType);

                String strSId = "";
                if (!Utility.IsDBNullOrNullValue(Request.QueryString["SId"]))
                    strSId = Utility.DecryptQS(Request.QueryString["SId"]);
                int SId = 0;
                if (!Utility.IsDBNullOrNullValue(SId))
                    SId = Convert.ToInt32(strSId);

                bool PageBreak = Convert.ToBoolean(Utility.DecryptQS(Request.QueryString["PageBreak"]));

                String SenderAddress = String.Empty;
                TSP.DataManager.Automation.SecretariatManager secretariatManager = new TSP.DataManager.Automation.SecretariatManager();
                secretariatManager.FindById(SId);
                if (secretariatManager.Count == 1)
                    SenderAddress = secretariatManager[0]["Address"].ToString() + "-" + secretariatManager[0]["SName"].ToString();

                XtraReportEnvelope Envelope = new XtraReportEnvelope((TSP.DataManager.AutomationLetterRecieverTypes)ReceiverType,
                    (TSP.DataManager.AddressType)AddressType, PageBreak, SenderAddress, MeIdParameters);
                ReportViewer1.Report = Envelope;
            }
            else
            {
                #region Member's Search Print Envelope
                String strMeIdFrom = Utility.DecryptQS(Request.QueryString["MeIdFrom"]);
                String strMeIdTo = Utility.DecryptQS(Request.QueryString["MeIdTo"]);

                int MeIdFrom;
                if (!Utility.IsDBNullOrNullValue(strMeIdFrom))
                    MeIdFrom = Convert.ToInt32(strMeIdFrom);
                else
                    MeIdFrom = -1;

                int MeIdTo;
                if (!Utility.IsDBNullOrNullValue(strMeIdTo))
                    MeIdTo = Convert.ToInt32(strMeIdTo);
                else
                    MeIdTo = -1;


                String CreateDateFrom = Utility.DecryptQS(Request.QueryString["CreateDateFrom"]);
                if (Utility.IsDBNullOrNullValue(CreateDateFrom))
                    CreateDateFrom = String.Empty;

                String CreateDateTo = Utility.DecryptQS(Request.QueryString["CreateDateTo"]);
                if (Utility.IsDBNullOrNullValue(CreateDateTo))
                    CreateDateTo = String.Empty;

                String FileDateFrom = Utility.DecryptQS(Request.QueryString["FileDateFrom"]);
                if (Utility.IsDBNullOrNullValue(FileDateFrom))
                    FileDateFrom = String.Empty;

                String FileDateTo = Utility.DecryptQS(Request.QueryString["FileDateTo"]);
                if (Utility.IsDBNullOrNullValue(FileDateTo))
                    FileDateTo = String.Empty;

                String MjParam = Utility.DecryptQS(Request.QueryString["MjParam"]);
                if (Utility.IsDBNullOrNullValue(MjParam))
                    MjParam = String.Empty;

                String FirstName = Utility.DecryptQS(Request.QueryString["FirstName"]);
                if (Utility.IsDBNullOrNullValue(FirstName))
                    FirstName = String.Empty;

                String LastName = Utility.DecryptQS(Request.QueryString["LastName"]);
                if (Utility.IsDBNullOrNullValue(LastName))
                    LastName = String.Empty;

                String GrParam = Utility.DecryptQS(Request.QueryString["GrParam"]);
                if (Utility.IsDBNullOrNullValue(GrParam))
                    GrParam = String.Empty;

                String FilterExpression = Utility.DecryptQS(Request.QueryString["FilterExpression"]);

                String strComId = Utility.DecryptQS(Request.QueryString["ComId"]);
                int ComId = -1;
                if (!Utility.IsDBNullOrNullValue(strComId))
                    ComId = Convert.ToInt32(strComId);

                String strReceiverType = Utility.DecryptQS(Request.QueryString["ReceiverType"]);
                int ReceiverType = -1;
                if (!Utility.IsDBNullOrNullValue(strReceiverType))
                    ReceiverType = Convert.ToInt32(strReceiverType);

                String strAddressType = Utility.DecryptQS(Request.QueryString["AddressType"]);
                int AddressType = -1;
                if (!Utility.IsDBNullOrNullValue(strAddressType))
                    AddressType = Convert.ToInt32(strAddressType);

                String strSId = Utility.DecryptQS(Request.QueryString["SId"]);
                int SId = 0;
                if (!Utility.IsDBNullOrNullValue(SId))
                    SId = Convert.ToInt32(strSId);

                bool PageBreak = Convert.ToBoolean(Utility.DecryptQS(Request.QueryString["PageBreak"]));
                String SenderAddress = String.Empty;
                TSP.DataManager.Automation.SecretariatManager secretariatManager = new TSP.DataManager.Automation.SecretariatManager();
                secretariatManager.FindById(SId);
                if (secretariatManager.Count == 1)
                    SenderAddress = secretariatManager[0]["Address"].ToString() + " - " + secretariatManager[0]["SName"].ToString();

                XtraReportEnvelope Envelope = new XtraReportEnvelope(MeIdFrom, MeIdTo, CreateDateFrom, CreateDateTo,
                    FileDateFrom, FileDateTo, MjParam, FirstName, LastName, GrParam, ComId,
                    (TSP.DataManager.AutomationLetterRecieverTypes)ReceiverType,
                    (TSP.DataManager.AddressType)AddressType, FilterExpression, PageBreak, SenderAddress);
                ReportViewer1.Report = Envelope;
                #endregion
            }
            //Session["MeIdParametersForEnvPrint"]=null;        
    }
}
