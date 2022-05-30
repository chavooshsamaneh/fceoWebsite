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

public partial class Institue_Amoozesh_InstitueHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.PeriodPresentRequestManager PeriodPresentRequestManager = new TSP.DataManager.PeriodPresentRequestManager();
            DataTable dtSavePeriodInfo = PeriodPresentRequestManager.SelectPeriodPresentRequestCount(-1, (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo, Utility.GetCurrentUser_MeId());
            if (dtSavePeriodInfo.Rows.Count > 0)
            {
                divSavePeriodInfo.Visible = true;
                linkHyperLinkSavePeriodInfo.HRef = "Period.aspx?SrchFlt=" + Utility.EncryptQS("TaskId&" + dtSavePeriodInfo.Rows[0]["TaskId"].ToString() ) + "&GrdFlt=" + Utility.EncryptQS("");
                HyperLinkSavePeriodInfo.InnerHtml = dtSavePeriodInfo.Rows[0]["TaskName"].ToString() + ": " + dtSavePeriodInfo.Rows.Count.ToString();
                ImgSavePeriodInfo.Src = dtSavePeriodInfo.Rows[0]["WFImageURL"].ToString();
            }
            DataTable dtRecordAbsenteeism = PeriodPresentRequestManager.SelectPeriodPresentRequestCount(-1, (int)TSP.DataManager.WorkFlowTask.RecordAbsenteeism, Utility.GetCurrentUser_MeId());
            if (dtRecordAbsenteeism.Rows.Count > 0)
            {
                divLearningExpertConfirmingPeriod.Visible = true;
                linkHyperLinkLearningExpertConfirmingPeriod.HRef = "Period.aspx?SrchFlt=" + Utility.EncryptQS("TaskId&" + dtRecordAbsenteeism.Rows[0]["TaskId"].ToString() ) + "&GrdFlt=" + Utility.EncryptQS("");
                HyperLinkLearningExpertConfirmingPeriod.InnerHtml = dtRecordAbsenteeism.Rows[0]["TaskName"].ToString() + ": " + dtRecordAbsenteeism.Rows.Count.ToString();
                ImgLearningExpertConfirmingPeriod.Src = dtRecordAbsenteeism.Rows[0]["WFImageURL"].ToString();
            }
        }
    }

}
