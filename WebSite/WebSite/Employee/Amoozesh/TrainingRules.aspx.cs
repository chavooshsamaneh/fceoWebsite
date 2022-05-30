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

public partial class Employee_Amoozesh_TrainingRules : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                string WorkFlowId = WorkFlowTaskManager[0]["WorkFlowId"].ToString();
                ObjdsWorkFlowTask.SelectParameters[0].DefaultValue = WorkFlowId;
            }

            TSP.DataManager.Permission per = TSP.DataManager.TrainingRulesManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnView.Enabled = per.CanView;
            btnView1.Enabled = per.CanView;
          
            CustomAspxDevGridView1.Visible = per.CanView;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddTrainingRule.aspx?TrId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int TrId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            TrId = (int)row["TrId"];
        }
        if (TrId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("AddTrainingRule.aspx?TrId=" + Utility.EncryptQS(TrId.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AmoozeshHome.aspx");
    }
    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        
    }
}
