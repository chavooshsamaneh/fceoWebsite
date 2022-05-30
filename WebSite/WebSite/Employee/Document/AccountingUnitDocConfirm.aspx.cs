using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Document_AccountingUnitDocConfirm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionAccountingUnitConfirmatDocument(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            //btnConfirm.Enabled = per.CanEdit;
            SetWarningLableDisable();
            ///////ObjdsMemberFileMainRequest.SelectParameters["TaskCodeAccConf"].DefaultValue = ((int)TSP.DataManager.WorkFlowTask.AccountingUnitEmployeeConfirmingDocument).ToString();
        }
    }


    protected void btnConfirm_OnClick(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex <= -1)
        {
            SetMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        System.Data.DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        if (MeFileRow == null)
        {
            SetMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        int MFId = int.Parse(MeFileRow["MaxMfId"].ToString());
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument);
        if (WorkFlowTaskManager.Count != 1)
        {
            SetMessage("خطایی در ذخیره انجام گرفته است.");
            return;
        }        
        int CurrentNmcId = FindNmcId(Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]));
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        if (WorkFlowStateManager.SendDocToNextStep(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile), MFId, Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]), "تایید سند تسویه حساب توسط واحد مالی", CurrentNmcId, Utility.GetCurrentUser_UserId(), "", "", -1, "") <= 0)
        {
            SetMessage("خطایی در ذخیره انجام گرفته است");
            return;
        }
               
        SetMessage("ذخیره انجام شد.");
        GridViewMemberFile.DataBind();
    }


    protected void btnSaveReject_OnClick(object sender, EventArgs e)
    {
        if (GridViewMemberFile.FocusedRowIndex <= -1)
        {
            SetMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        System.Data.DataRow MeFileRow = GridViewMemberFile.GetDataRow(GridViewMemberFile.FocusedRowIndex);
        if (MeFileRow == null)
        {
            SetMessage("ابتدا یک ردیف را انتخاب نمایید");
            return;
        }
        int MFId = int.Parse(MeFileRow["MaxMfId"].ToString());
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo);
        if (WorkFlowTaskManager.Count != 1)
        {
            SetMessage("خطایی در ذخیره انجام گرفته است.");
            return;
        }        
        int CurrentNmcId = FindNmcId(Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]));
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        if (WorkFlowStateManager.SendDocToNextStep(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile), MFId, Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]), txtDescription.Text, CurrentNmcId, Utility.GetCurrentUser_UserId(), "", "", -1, "") <= 0)
        {
            SetMessage("خطایی در ذخیره انجام گرفته است");
            return;
        }
               
        SetMessage("ذخیره انجام شد.");
        txtDescription.Text = "";
        GridViewMemberFile.DataBind();
    }
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetWarningLableDisable()
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    private int FindNmcId(int TaskId)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            DivReport.Visible = true;
            LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            return (-1);
        }
    }
}