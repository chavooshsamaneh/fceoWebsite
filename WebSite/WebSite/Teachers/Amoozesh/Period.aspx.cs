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
using DevExpress.Web;

public partial class Teachers_Amoozesh_Period : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = "-1";
            ObjdsWorkFlowTask.FilterExpression = "WorkFlowCode=" + ((int)TSP.DataManager.WorkFlows.PeriodConfirming).ToString()
            + " or " + "WorkFlowCode=" + ((int)TSP.DataManager.WorkFlows.PrindPeriodCertificates).ToString();
            OdbPeriod.SelectParameters[0].DefaultValue = Utility.GetCurrentUser_MeId().ToString(); //****TeId==Utility.GetCurrentUser_MeId()
            LoadWfHelpPeriodConfirm();
            LoadWfHelpPeriodPrint();
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int PPId = -1;
        int InsId = -1;
        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriods.GetDataRow(GridViewPeriods.FocusedRowIndex);
            PPId = (int)row["PkId"];
            InsId = (int)row["InsId"];

        }
        if (PPId == -1 || InsId == -1)
        {
            SetMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");

        }
        else
            Response.Redirect("PeriodView.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&InsId=" + Utility.EncryptQS(InsId.ToString()));
    }

    protected void btnMark_Click(object sender, EventArgs e)
    {
        int PPId = -1;
        int InsId = -1;
        Int16 Status = -1;
        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriods.GetDataRow(GridViewPeriods.FocusedRowIndex);
            PPId = (int)row["PkId"];
            InsId = (int)row["InsId"];
            Status = (Int16)row["Status"];

        }
        if (PPId == -1 || InsId == -1)
        {
            SetMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        TSP.DataManager.TrainingTeachersManager TrTeachersManager = new TSP.DataManager.TrainingTeachersManager();
        DataTable dtTr = TrTeachersManager.SelectPeriodTeachers(-1, PPId);
        if (dtTr.Rows.Count <= 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations);
            return;
        }
        int TeId = Utility.GetCurrentUser_MeId();
        DataRow[] dtSearchTe = dtTr.Select("TeId=" + TeId.ToString());
        if (dtSearchTe.Length <= 0)
        {
            SetMessage("امکان ثبت نمرات امتحانی برای شما وجود ندارد");
            return;
        }
        if (Status == (int)TSP.DataManager.PeriodPresentStatus.StartTest
             || Status == (int)TSP.DataManager.PeriodPresentStatus.AnnounceResultAndObjection
             || Status == (int)TSP.DataManager.PeriodPresentStatus.EndObjection)
        {
            Response.Redirect("PeriodTestMarks.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&InsId=" + Utility.EncryptQS(InsId.ToString()) + "&Mode=" + Utility.EncryptQS("Mark"));

        }
        else
        {
            SetMessage("با توجه به وضعیت دوره امکان ثبت نمرات امتحانی وجود ندارد");
        }
    }

    protected void btnObjection_Click(object sender, EventArgs e)
    {
        int PPId = -1;
        int InsId = -1;
        Int16 Status = -1;

        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriods.GetDataRow(GridViewPeriods.FocusedRowIndex);
            PPId = (int)row["PkId"];
            InsId = (int)row["InsId"];
            Status = (Int16)row["Status"];

        }
        if (PPId == -1 || InsId == -1)
        {
            SetMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        TSP.DataManager.PeriodPresentManager PPManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.TrainingTeachersManager TrTeachersManager = new TSP.DataManager.TrainingTeachersManager();

        DataTable dtTr = TrTeachersManager.SelectPeriodTeachers(-1, PPId);

        //*******************************
        int TrTeId = int.Parse(dtTr.Rows[0]["TeId"].ToString());
        int TeId = Utility.GetCurrentUser_MeId();
        if (TrTeId == TeId)
        {
            //if (Status == (int)TSP.DataManager.PeriodPresentStatus.StartTest || Status == (int)TSP.DataManager.PeriodPresentStatus.AnnounceResultAndObjection
            //    || Status == (int)TSP.DataManager.PeriodPresentStatus.EndObjection)
            //{
            Response.Redirect("PeriodTestMarks.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&InsId=" + Utility.EncryptQS(InsId.ToString()) + "&Mode=" + Utility.EncryptQS("Objection"));
            //}
            //else
            //{
            //    SetMessage("تنها در وضعیت اعلام نتایج و اعتراضات قادر پاسخ به اعتراضات می باشید");
            //}
        }
        else
        {
            SetMessage("امکان پاسخ به اعتراضات برای شما وجود ندارد.تنها اساتید دوره انتخاب شده امکان پاسخ به اعتراضات را دارند");
        }

    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewPeriods.FindDetailRowTemplateControl(GridViewPeriods.FocusedRowIndex, "GridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        int PPRId = int.Parse(GridRequest.GetDataRow(index0)["PPRId"].ToString());
                        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);

                        string GridFilterString = GridViewPeriods.FilterExpression;
                        String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                          "&PostId=" + Utility.EncryptQS(PPRId.ToString());
                        Response.Redirect("../MemberDocument/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString())
                            + "&TblId=" + Utility.EncryptQS(PPRId.ToString())
                              + "&UrlReferrer=" + Utility.EncryptQS(Url));
                    }
                }
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
    }

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewPeriods.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewPeriods.FindDetailRowTemplateControl(GridViewPeriods.FocusedRowIndex, "GridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        int PPRId = int.Parse(GridRequest.GetDataRow(index0)["PPRId"].ToString());
                        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
                        int WfCode = (int)TSP.DataManager.WorkFlows.PeriodConfirming;
                        WFUserControl.PerformCallback(PPRId, TableType, WfCode, e);
                        GridViewPeriods.DataBind();
                    }
                }
            }
        }
        else
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }
    }
    protected void GridViewPeriods_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "EndDate" || e.DataColumn.FieldName == "StartDate")
            e.Cell.Style["direction"] = "ltr";
    }

    protected void GridViewPeriods_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "EndDate" || e.Column.FieldName == "StartDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["PPId"] = (sender as ASPxGridView).GetMasterRowFieldValues("PkId");
        int Index = GridViewPeriods.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        GridViewPeriods.FocusedRowIndex = Index;
    }
    #endregion

    #region Methods
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
        void LoadWfHelpPeriodConfirm()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        DataTable dt = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.PeriodConfirming));
        DataTable dt1 = dt.Clone();
        DataTable dt2 = dt.Clone();
        DataTable dt3 = dt.Clone();
        int count = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (count % 3 == 0) { dt1.Rows.Add(dr.ItemArray); }
            if (count % 3 == 1) { dt2.Rows.Add(dr.ItemArray); }
            if (count % 3 == 2) { dt3.Rows.Add(dr.ItemArray); }
            count++;
        }
        if (dt1.Rows.Count != 0)
        {
            RepeaterWfHelp1.DataSource = dt1;
            RepeaterWfHelp1.DataBind();
        }
        if (dt2.Rows.Count != 0)
        {
            RepeaterWfHelp2.DataSource = dt2;
            RepeaterWfHelp2.DataBind();
        }
        if (dt3.Rows.Count != 0)
        {
            RepeaterWfHelp3.DataSource = dt3;
            RepeaterWfHelp3.DataBind();
        }
    }
    void LoadWfHelpPeriodPrint()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        DataTable dt = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.PrindPeriodCertificates));
        DataTable dt1 = dt.Clone();
        DataTable dt2 = dt.Clone();
        DataTable dt3 = dt.Clone();
        int count = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (count % 3 == 0) { dt1.Rows.Add(dr.ItemArray); }
            if (count % 3 == 1) { dt2.Rows.Add(dr.ItemArray); }
            if (count % 3 == 2) { dt3.Rows.Add(dr.ItemArray); }
            count++;
        }
        if (dt1.Rows.Count != 0)
        {
            RepeaterWfHepPrint1.DataSource = dt1;
            RepeaterWfHepPrint1.DataBind();
        }
        if (dt2.Rows.Count != 0)
        {
            RepeaterWfHepPrint2.DataSource = dt2;
            RepeaterWfHepPrint2.DataBind();
        }
        if (dt3.Rows.Count != 0)
        {
            RepeaterWfHepPrint3.DataSource = dt3;
            RepeaterWfHepPrint3.DataBind();
        }
    }
    #endregion
}
