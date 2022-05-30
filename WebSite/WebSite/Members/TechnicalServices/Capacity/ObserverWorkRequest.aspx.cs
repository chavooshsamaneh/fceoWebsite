using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Members_TechnicalServices_Capacity_ObserverWorkRequest : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            ObjectDataSourceWorkRequest.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {

        if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
        {
           if(!Utility.IsWorkRequestMainAgent())
            {
                ShowMessage("در این بازه زمانی امکان ثبت آماده بکاری برای اعضای نمایندگی شیراز وجود ندارد");
                return;
            }
        }
        else
        {
            if (!Utility.IsWorkRequestOtheAgentOpen())
            {
                ShowMessage("در این بازه زمانی امکان ثبت آماده بکاری برای اعضای دفاتر نمایندگی وجود ندارد");
                return;
            }
        }
        
        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();        
        if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
            CapacityAssignmentManager.SelectCurrentYearAndStage(1);
        else
            CapacityAssignmentManager.SelectCurrentYearAndStage(0);
        int _CurrentCapacityAssignmentId = -2;
        string _CurrentCapacityEndate = "";
        if (CapacityAssignmentManager.Count > 0)
        {
            _CurrentCapacityAssignmentId = Convert.ToInt32(CapacityAssignmentManager[0]["CapacityAssignmentId"]);
            _CurrentCapacityEndate = CapacityAssignmentManager[0]["EndDate"].ToString();
        }
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        System.Collections.ArrayList Result = ObserverWorkRequestManager.CheckConditionForNewObsWorkRequest(Utility.GetCurrentUser_MeId(), _CurrentCapacityEndate, _CurrentCapacityAssignmentId);
        if (!Convert.ToBoolean(Result[0]))
        {
            ShowMessage(Result[1].ToString());
            return;
        }
        else
            Response.Redirect("ObserverWorkRequestInsert.aspx?PgMd=" + Utility.EncryptQS("New")  + "&ObsWChangId=" + Utility.EncryptQS("-2"));
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (GridViewObserverWorkRequest.FocusedRowIndex <= -1)
        {
            ShowMessage("ابتدا یک ردیف از جدول را انتخاب نمایید");
            return;
        }
        System.Data.DataRow row = GridViewObserverWorkRequest.GetDataRow(GridViewObserverWorkRequest.FocusedRowIndex);

        Response.Redirect("ObserverWorkRequestInsert.aspx?PgMd=" + Utility.EncryptQS("View") + "&ObsWChangId=" + Utility.EncryptQS(Convert.ToInt32(row["ObsWorkReqChangeId"]).ToString()));
    }

    protected void btnOffDate_Click(object sender, EventArgs e)
    {

        if (GridViewObserverWorkRequest.FocusedRowIndex <= -1)
        {
            ShowMessage("ابتدا یک ردیف از جدول را انتخاب نمایید");
            return;
        }
        System.Data.DataRow row = GridViewObserverWorkRequest.GetDataRow(GridViewObserverWorkRequest.FocusedRowIndex);

        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        System.Data.DataTable dt = ObserverWorkRequestChangesManager.SelectLastRequest(Convert.ToInt32(row["ObsWorkReqId"]), -1);
        if (dt.Rows.Count > 0 && !Convert.ToBoolean(dt.Rows[0]["IsConfirm"]))
        {
            ShowMessage("به دلیل وجود درخواست تایید نشده قادر به ثبت درخواست جدید نمی باشید");
            return;
        }
        Response.Redirect("ObserverWorkRequestInsert.aspx?PgMd=" + Utility.EncryptQS("Off") 
            + "&ObsWChangId=" + Utility.EncryptQS(Convert.ToInt32(dt.Rows[0]["ObsWorkReqChangeId"]).ToString()));
    }
    protected void btnChangeRequest_Click(object sender, EventArgs e)
    {
        if (GridViewObserverWorkRequest.FocusedRowIndex <= -1)
        {
            ShowMessage("ابتدا یک ردیف از جدول را انتخاب نمایید");
            return;
        }
        System.Data.DataRow row = GridViewObserverWorkRequest.GetDataRow(GridViewObserverWorkRequest.FocusedRowIndex);

        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        System.Data.DataTable dt = ObserverWorkRequestChangesManager.SelectLastRequest(-1, Utility.GetCurrentUser_MeId());
        if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["IsConfirm"]) == 0)
        {
            ShowMessage("به دلیل وجود درخواست تایید نشده قادر به ثبت درخواست جدید نمی باشید");
            return;
        }
        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
            CapacityAssignmentManager.SelectCurrentYearAndStage(1);
        else
            CapacityAssignmentManager.SelectCurrentYearAndStage(0);
        int _CurrentCapacityAssignmentId = -2;
        string _CurrentCapacityEndate = "";
        if (CapacityAssignmentManager.Count > 0)
        {
            _CurrentCapacityAssignmentId = Convert.ToInt32(CapacityAssignmentManager[0]["CapacityAssignmentId"]);
            _CurrentCapacityEndate = CapacityAssignmentManager[0]["EndDate"].ToString();
        }
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        System.Collections.ArrayList Result = ObserverWorkRequestManager.CheckConditionForChangeWorkRequest(Utility.GetCurrentUser_MeId(), _CurrentCapacityEndate);
        if (!Convert.ToBoolean(Result[0]))
        {
            ShowMessage(Result[1].ToString());
            return;
        }
        else
            Response.Redirect("ObserverWorkRequestInsert.aspx?PgMd=" + Utility.EncryptQS("Change") 
            + "&ObsWChangId=" + Utility.EncryptQS(Convert.ToInt32(dt.Rows[0]["ObsWorkReqChangeId"]).ToString()));
    }
    #endregion

    #region Methods    
    void ShowMessage(string str)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = str;
    }
    #endregion   
}