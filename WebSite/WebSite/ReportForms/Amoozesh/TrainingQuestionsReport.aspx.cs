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

public partial class ReportForms_Amoozesh_TrainingQuestionsReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Utility.IsDBNullOrNullValue(Request.QueryString["Qs"]))
        {
            String Filter = GetFilterExpression(Utility.DecryptQS(Request.QueryString["Qs"]));
            XtraReportTrainingQuestions XtraReportTrainingQuestions = new XtraReportTrainingQuestions(Filter);
            ReportViewer1.Report = XtraReportTrainingQuestions;
            XtraReportTrainingAnswers XtraReportTrainingAnswers = new XtraReportTrainingAnswers(Filter);
            ReportViewer2.Report = XtraReportTrainingAnswers;
        }
    }

    String GetFilterExpression(String FilterExpression)
    {
        TSP.DataManager.TrainingQuestionsManager QuestionManager = new TSP.DataManager.TrainingQuestionsManager();
        QuestionManager.Fill();
        QuestionManager.CurrentFilter = FilterExpression;

        String Filter = "";
        for (int i = 0; i < QuestionManager.Count; i++)
        {
            if (i > 0)
                Filter += " or ";
            Filter += "TrQuId=" + QuestionManager[i]["TrQuId"];
        }
        return Filter;
    }
}
