using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Amoozesh_ReportMemberPeriods : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cmbCourse.DataBind();
            cmbCourse.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            CmbGrdDes.DataBind();
            CmbGrdDes.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            CmbGrdImp.DataBind();
            CmbGrdImp.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            CmbGrdObs.DataBind();
            CmbGrdObs.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            CmbGrdMapping.DataBind();
            CmbGrdMapping.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            CmbGrdTraffic.DataBind();
            CmbGrdTraffic.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            CmbGrdUrbonism.DataBind();
            CmbGrdUrbonism.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
            CmbMjParents.DataBind();
            CmbMjParents.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));

            TSP.DataManager.Permission per = TSP.DataManager.PeriodRegisterManager.GetUserPermissionReportMemberPeriods(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnExportExcel.Enabled = per.CanView;
            GridViewReport.Visible = per.CanView;


        }

        string script = @" function CheckSearch() { var txtEndDateFrom = document.getElementById('" + txtEndDateFrom.ClientID + "').value;";
        script += "var txtEndDateTo = document.getElementById('" + txtEndDateTo.ClientID + "').value;";
        script += "var txtStartDateFrom = document.getElementById('" + txtStartDateFrom.ClientID + "').value;";
        script += "var txtStartDateTo = document.getElementById('" + txtStartDateTo.ClientID + "').value;";
        //script += "if (txtMeId.GetText()=='' && txtEndDateFrom=='' && txtEndDateTo=='' &&  txtStartDateFrom=='' && txtStartDateTo=='' && cmbCourse.GetSelectedIndex() == 0 && CmbGrdDes.GetSelectedIndex() == 0  && CmbGrdImp.GetSelectedIndex() == 0  && CmbGrdMapping.GetSelectedIndex() == 0  && CmbGrdObs.GetSelectedIndex() == 0  && CmbGrdTraffic.GetSelectedIndex() == 0  && CmbGrdUrbonism.GetSelectedIndex() == 0 ) return 0; else return 1;}";
        script += "if (txtMeId.GetText()=='' && txtEndDateFrom=='' && txtEndDateTo=='' &&  txtStartDateFrom=='' && txtStartDateTo=='' && cmbCourse.GetSelectedIndex() == 0 ) return 0; else return 1;}";
        script += @"function ClearSearch() {
        txtMeId.SetText('');
        cmbCourse.SetSelectedIndex(0);
        CmbGrdImp.SetSelectedIndex(0);
        CmbGrdDes.SetSelectedIndex(0);
        CmbGrdMapping.SetSelectedIndex(0);
        CmbGrdObs.SetSelectedIndex(0);
        CmbGrdTraffic.SetSelectedIndex(0);
        CmbGrdUrbonism.SetSelectedIndex(0);
        document.getElementById('" + txtEndDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtEndDateTo.ClientID + @"').value='';
        document.getElementById('" + txtStartDateFrom.ClientID + @"').value='';
        document.getElementById('" + txtStartDateTo.ClientID + @"').value='';}";
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script, true);
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Repot";
        GridViewExporter.WriteXlsToResponse(true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (Utility.IsDBNullOrNullValue(txtMeId.Text)&& cmbCourse.SelectedItem.Value == null&& Utility.IsDBNullOrNullValue(txtStartDateFrom.Text)&& Utility.IsDBNullOrNullValue(txtStartDateTo.Text)&& Utility.IsDBNullOrNullValue(txtEndDateFrom.Text)&& Utility.IsDBNullOrNullValue(txtEndDateTo.Text))
        {
            ShowMessage("تکمیل یکی از فیلدهای اطلاعاتی به غیر از فیلدهای مربوط به رشته و پایه الزامی است");
            return;
        }

        if (cmbCourse.SelectedItem.Value != null)
            OdbPeriodRegisterReport.SelectParameters["CrsId"].DefaultValue = cmbCourse.SelectedItem.Value.ToString();
        else
            OdbPeriodRegisterReport.SelectParameters["CrsId"].DefaultValue = "-1";
        if (!Utility.IsDBNullOrNullValue(txtMeId.Text))
            OdbPeriodRegisterReport.SelectParameters["MeId"].DefaultValue = txtMeId.Text;
        else
            OdbPeriodRegisterReport.SelectParameters["MeId"].DefaultValue = "-1";
        if (!Utility.IsDBNullOrNullValue(txtStartDateFrom.Text))
            OdbPeriodRegisterReport.SelectParameters["FromStartDate"].DefaultValue = txtStartDateFrom.Text;
        else
            OdbPeriodRegisterReport.SelectParameters["FromStartDate"].DefaultValue = "1";
        if (!Utility.IsDBNullOrNullValue(txtStartDateTo.Text))
            OdbPeriodRegisterReport.SelectParameters["ToStartDate"].DefaultValue = txtStartDateTo.Text;
        else
            OdbPeriodRegisterReport.SelectParameters["ToStartDate"].DefaultValue = "2";
        if (!Utility.IsDBNullOrNullValue(txtEndDateFrom.Text))
            OdbPeriodRegisterReport.SelectParameters["FromEndDate"].DefaultValue = txtEndDateFrom.Text;
        else
            OdbPeriodRegisterReport.SelectParameters["FromEndDate"].DefaultValue = "1";
        if (!Utility.IsDBNullOrNullValue(txtEndDateTo.Text))
            OdbPeriodRegisterReport.SelectParameters["ToEndDate"].DefaultValue = txtEndDateTo.Text;
        else
            OdbPeriodRegisterReport.SelectParameters["ToEndDate"].DefaultValue = "2";
        if (CmbGrdImp.SelectedItem.Value != null)
            OdbPeriodRegisterReport.SelectParameters["ImpGrdId"].DefaultValue = CmbGrdImp.SelectedItem.Value.ToString();
        else
            OdbPeriodRegisterReport.SelectParameters["ImpGrdId"].DefaultValue = "-1";
        if (CmbGrdObs.SelectedItem.Value != null)
            OdbPeriodRegisterReport.SelectParameters["ObsGrdId"].DefaultValue = CmbGrdObs.SelectedItem.Value.ToString();
        else
            OdbPeriodRegisterReport.SelectParameters["ObsGrdId"].DefaultValue = "-1";
        if (CmbGrdDes.SelectedItem.Value != null)
            OdbPeriodRegisterReport.SelectParameters["DesGrdId"].DefaultValue = CmbGrdDes.SelectedItem.Value.ToString();
        else
            OdbPeriodRegisterReport.SelectParameters["DesGrdId"].DefaultValue = "-1";
        if (CmbGrdUrbonism.SelectedItem.Value != null)
            OdbPeriodRegisterReport.SelectParameters["UrbanismGrdId"].DefaultValue = CmbGrdUrbonism.SelectedItem.Value.ToString();
        else
            OdbPeriodRegisterReport.SelectParameters["UrbanismGrdId"].DefaultValue = "-1";
        if (CmbGrdMapping.SelectedItem.Value != null)
            OdbPeriodRegisterReport.SelectParameters["MappingGrdId"].DefaultValue = CmbGrdMapping.SelectedItem.Value.ToString();
        else
            OdbPeriodRegisterReport.SelectParameters["MappingGrdId"].DefaultValue = "-1";
        if (CmbGrdTraffic.SelectedItem.Value != null)
            OdbPeriodRegisterReport.SelectParameters["TrafficGrdId"].DefaultValue = CmbGrdTraffic.SelectedItem.Value.ToString();
        else
            OdbPeriodRegisterReport.SelectParameters["TrafficGrdId"].DefaultValue = "-1";

        if (CmbMjParents.SelectedItem.Value != null)
            OdbPeriodRegisterReport.SelectParameters["MjParentId"].DefaultValue = CmbMjParents.SelectedItem.Value.ToString();
        else
            OdbPeriodRegisterReport.SelectParameters["MjParentId"].DefaultValue = "-1";

        GridViewReport.DataBind();
    }
    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
}